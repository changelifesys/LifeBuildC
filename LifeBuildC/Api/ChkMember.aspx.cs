/*
 * API功用：檢查會友資料是否輸入正確
 * 
 * 引用此API的頁面：
 * SubjectSignUp.aspx
 * 
 */
using ADO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class ChkMember : System.Web.UI.Page
    {
        //會友資訊
        ChcMemberADO ChcMember = new ChcMemberADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            #region API邏輯判斷解說

            /* API邏輯判斷解說
            
            資料表內容：
           手機                     小組                   姓名
            0919963322     彥伯小組           流大丹    >>正確的資料
            0919xxxxxxx     彥伯小組           流大丹    >>手機輸錯
            0919963322     xx小組               流大丹    >>小組輸錯
            0919963322     彥伯小組           xxx          >>姓名輸錯
            -------------------------------------------------
            先用小組, 姓名取得資料
            若取不到資料用手機, 姓名取資料
            若取不到資料用手機, 小組取資料
            -------------------------------------------------
            若都取不到資料就新增資料
            -------------------------------------------------
            若取的到資料就比對輸入的資料

            當用小組, 姓名可以取得資料時
            若判斷手機有錯(比對DB和輸入的資料不一樣)
            回傳「手機號碼是否更換為0919xxxxxx」的訊息

            當用手機, 姓名可以取得資料時
            若判斷小組有錯(比對DB和輸入的資料不一樣)
            回傳「是否有更換小組為xx小組」的訊息

            當用手機, 小組可以取得資料時
            若判斷姓名有錯(比對DB和輸入的資料不一樣)
            回傳「是否有更改姓名為xxx」的訊息

             */

            #endregion

            if (!IsPostBack)
            {
                #region Test 電文

                string strChkMember =
                @" 
                {
                  GroupName: ""CA202.信豪牧區-彥伯小組"",
                  Ename: ""流大丹"",
                  Phone: ""0919963322""
                }
                ";

                #endregion

                using (Stream receiveStream = Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        strChkMember = readStream.ReadToEnd();
                    }
                }

                PageData PageData = JsonConvert.DeserializeObject<PageData>(strChkMember);

                PageData.ChkMsg = "";

                //若都取不到資料就新增資料

                //ChkStatus=4 新增會友資料
                PageData.ChkStatus = 4;

                PageData.MID = 0;

                DataTable dtMem = ChcMember.QueryAllByChcMember();
                if (dtMem.Rows.Count > 0)
                {
                    //先用小組, 姓名取得資料
                    DataRow[] drChk1 = dtMem.Select("GroupName='" + PageData.GroupName.Split('.')[1] + "' AND Ename='" + PageData.Ename + "'");
                    //若取不到資料用手機, 姓名取資料
                    DataRow[] drChk2 = dtMem.Select("Phone='" + PageData.Phone + "' AND Ename='" + PageData.Ename + "'");
                    //若取不到資料用手機, 小組取資料
                    DataRow[] drChk3 = dtMem.Select("Phone='" + PageData.Phone + "' AND GroupName='" + PageData.GroupName.Split('.')[1] + "'");

                    if (drChk1.Count() > 0)
                    { //當用小組, 姓名可以取得資料時

                        //若判斷手機有錯(比對DB和輸入的資料不一樣)
                        //回傳「手機號碼是否更換為0919xxxxxx」的訊息
                        if (PageData.Phone != drChk1[0]["Phone"].ToString())
                        {
                            PageData.ChkMsg = "手機號碼是否更換為「" + PageData.Phone + "」";
                            
                            //ChkStatus=1 更新手機號碼
                            PageData.ChkStatus = 0;
                        }
                        else
                        {
                            //ChkStatus=0 保持原有資料
                            PageData.ChkStatus = 0;
                        }

                        PageData.MID = int.Parse(drChk1[0]["MID"].ToString());
                    }
                    else if (drChk2.Count() > 0)
                    { //當用手機, 姓名可以取得資料時

                        //若判斷小組有錯(比對DB和輸入的資料不一樣)
                        //回傳「是否有更換小組為xx小組」的訊息
                        if (PageData.GroupName != drChk2[0]["GroupName"].ToString())
                        {
                            PageData.ChkMsg = "是否有更換小組為「" + PageData.GroupName + "」";

                            //ChkStatus=2 更新小組資料
                            PageData.ChkStatus = 2;
                        }
                        else
                        {
                            //ChkStatus=0 保持原有資料
                            PageData.ChkStatus = 0;
                        }

                        PageData.MID = int.Parse(drChk2[0]["MID"].ToString());

                    }
                    else if (drChk3.Count() > 0)
                    { //當用手機, 小組可以取得資料時

                        //若判斷姓名有錯(比對DB和輸入的資料不一樣)
                        //回傳「是否有更改姓名為xxx」的訊息
                        if (PageData.Ename != drChk3[0]["Ename"].ToString())
                        {
                            PageData.ChkMsg = "是否有更改姓名為「" + PageData.Ename + "」";

                            //ChkStatus=3 更新會友姓名
                            PageData.ChkStatus = 3;
                        }
                        else
                        {
                            //ChkStatus=0 保持原有資料
                            PageData.ChkStatus = 0;
                        }

                        PageData.MID = int.Parse(drChk3[0]["MID"].ToString());

                    }
                    else
                    {
                        //若都取不到資料就新增資料

                        //ChkStatus=4 新增會友資料
                        PageData.ChkStatus = 4;

                        PageData.MID = 0;
                    }
                }

                Response.Write(JsonConvert.SerializeObject(PageData));

            }

        }

        public class PageData
        {
            public string GroupName { get; set; }
            public string Ename { get; set; }
            public string Phone { get; set; }

            //回傳的訊息
            //若為空值，表示資料沒有問題
            public string ChkMsg { get; set; }

            //ChkStatus=0 保持原有資料
            //ChkStatus=1 更新手機號碼
            //ChkStatus=2 更新小組資料
            //ChkStatus=3 更新會友姓名
            //ChkStatus=4 新增會友資料
            public int ChkStatus { get; set; }
            public int MID { get; set; }
        }

    }
}