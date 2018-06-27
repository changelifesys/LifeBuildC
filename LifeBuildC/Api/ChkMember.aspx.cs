﻿/*
 用途：
   生命建造-課程報名

 流程：
   [View]SubjectSignUp.aspx?id=c1 > 報名 > [API]ChkMember

 API演算法：

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
        ApiData api = new ApiData();
        ChcMemberADO ChcMember = new ChcMemberADO();

        /// <summary>
        /// StreamReader電文
        /// </summary>
        string strChkMember = string.Empty;
        /// <summary>
        /// 牧區
        /// </summary>
        string GroupCName = string.Empty;
        /// <summary>
        /// 小組
        /// </summary>
        string GroupName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["test"] != null &&
                Request.QueryString["test"].ToString() == "true")
                {
                    api.group = "CA202.信豪牧區-彥伯小組";
                    api.Ename = "劉彥伯";
                    api.Phone = "0919963327";
                }
                else
                {
                    using (Stream receiveStream = Request.InputStream)
                    {
                        using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                        {
                            strChkMember = readStream.ReadToEnd();
                        }
                    }

                    api = JsonConvert.DeserializeObject<ApiData>(strChkMember);
                }

                if (api != null)
                {
                    api.IsChgShow = false;
                    api.MID = "";

                    //小組
                    string[] arrg = api.group.Split('.');
                    GroupCName = arrg[1].Split('-')[0];
                    GroupName = arrg[1].Split('-')[1];

                    DataTable dtMem = ChcMember.QueryAllByChcMember();
                    if (dtMem.Rows.Count > 0)
                    {
                        //先用小組, 姓名取得資料
                        DataRow[] drChk1 = dtMem.Select("GroupCName='" + GroupCName + "' AND GroupName='" + GroupName + "' AND Ename='" + api.Ename + "'");
                        //若取不到資料用手機, 姓名取資料
                        DataRow[] drChk2 = dtMem.Select("Phone='" + api.Phone + "' AND Ename='" + api.Ename + "'");
                        //若取不到資料用手機, 小組取資料
                        DataRow[] drChk3 = dtMem.Select("Phone='" + api.Phone + "' AND GroupName='" + GroupName + "' AND GroupCName='" + GroupCName + "'");

                        if (drChk1.Count() > 0)
                        { //當用小組, 姓名可以取得資料時

                            //若判斷手機有錯(比對DB和輸入的資料不一樣)
                            //回傳「手機號碼是否更換為0919xxxxxx」的訊息
                            if (api.Phone != drChk1[0]["Phone"].ToString())
                            {
                                api.ApiTitle = "您有變更手機號碼？";
                                api.DataChangeMsg.Add("原手機號碼 " + drChk1[0]["Phone"].ToString() + " 是否變更為 「" + api.Phone + "」");
                                api.IsChgShow = true;
                            }

                            api.MID = drChk1[0]["MID"].ToString();

                        }
                        else if (drChk2.Count() > 0)
                        { //當用手機, 姓名可以取得資料時

                            //若判斷小組有錯(比對DB和輸入的資料不一樣)
                            //回傳「是否有更換小組為xx小組」的訊息
                            if (arrg[1] != (drChk2[0]["GroupCName"].ToString() + "-" + drChk2[0]["GroupName"].ToString()))
                            {
                                api.ApiTitle = "您有變更小組？";
                                api.DataChangeMsg.Add("原 " + drChk2[0]["GroupCName"].ToString() + "-" + drChk2[0]["GroupName"].ToString() +
                                                               "是否變更為 「" + api.group + "」");
                                api.IsChgShow = true;
                            }

                            api.MID = drChk2[0]["MID"].ToString();

                        }
                        else if (drChk3.Count() > 0)
                        { //當用手機, 小組可以取得資料時

                            //若判斷姓名有錯(比對DB和輸入的資料不一樣)
                            //回傳「是否有更改姓名為xxx」的訊息
                            if (api.Ename != drChk3[0]["Ename"].ToString())
                            {
                                api.ApiTitle = "您有改名字？";
                                api.DataChangeMsg.Add("原 姓名 " + drChk3[0]["Ename"].ToString() + " 是否變更為 「" + api.Ename + "」");
                                api.IsChgShow = true;
                            }

                            api.MID = drChk3[0]["MID"].ToString();

                        }

                    }

                }

                Response.Write(JsonConvert.SerializeObject(api));

            }

        }

        /// <summary>
        /// API參數
        /// </summary>
        public class ApiData
        {
            /// <summary>
            /// 小組
            /// </summary>
            public string group { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string Ename { get; set; }
            /// <summary>
            /// 手機
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// 會友流水號
            /// </summary>
            public string MID { get; set; }
            /// <summary>
            /// API Title
            /// </summary>
            public string ApiTitle { get; set; }
            /// <summary>
            /// 資料變更訊息
            /// </summary>
            public List<string> DataChangeMsg = new List<string>();
            /// <summary>
            /// 是否要秀出資料變更訊息(true: 要秀出來; false: 不要秀出來)
            /// </summary>
            public bool IsChgShow { get; set; }
        }

    }
}