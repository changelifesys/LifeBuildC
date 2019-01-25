using libLifeBuildC;
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
    public partial class ChkUserInfo : System.Web.UI.Page
    {
        ApiData.ChkUserInfo Api_Data = new ApiData.ChkUserInfo();
        AdoInfo Ado_Info = new AdoInfo();
        ApiInfo Api_Info = new ApiInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    Api_Info.strJsonData = readStream.ReadToEnd();
                }
            }

            Api_Data = JsonConvert.DeserializeObject<ApiData.ChkUserInfo>(Api_Info.strJsonData);

            if (Api_Data != null)
            {
                ApiProcess();
            }

            Response.Write(JsonConvert.SerializeObject(Api_Data));
            Response.End();
        }

        private void ApiProcess()
        {
            //從基本資料查詢所有會友
            DataTable dt_Mem = Ado_Info.ChcMember_ADO.QueryAllByChcMember();
            //從暫存資料中查詢所有會友
            DataTable dt_Mem_Temp = Ado_Info.ChcMemberApp_Temp_ADO.QueryAllDataWhereuptyn();

            if (!UpdMember(dt_Mem_Temp))
            {
                //UpdMember(dt_Mem_Temp)=false: 沒有該名會友的資料
                UpdMember(dt_Mem);
            }

            //若 MID 為空值表示為新會友

        }

        /// <summary>
        /// true: 有該名會友的資料; false: 沒有該名會友的資料
        /// </summary>
        private bool UpdMember(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {

                //先用小組, 姓名取得資料
                DataRow[] drChk1 = dt.Select("GroupCName='" + Api_Info.GroupCName + "' AND GroupName='" + Api_Info.GroupName + "' AND Ename='" + Api_Data.Ename + "'");

                //若取不到資料就用手機, 姓名取資料
                DataRow[] drChk2 = dt.Select("Phone='" + Api_Data.Phone + "' AND Ename='" + Api_Data.Ename + "'");

                //若取不到資料就用手機, 小組取資料
                DataRow[] drChk3 = dt.Select("Phone='" + Api_Data.Phone + "' AND GroupName='" + Api_Info.GroupName + "' AND GroupCName='" + Api_Info.GroupCName + "'");

                //若取不到資料就用姓名取資料
                DataRow[] drChk4 = dt.Select("Ename='" + Api_Data.Ename + "'");

                if (drChk1.Count() > 0)
                { //若用小組, 姓名可以取得資料時

                    //若判斷手機有錯(比對DB和輸入的資料不一樣)
                    //回傳「手機號碼是否更換為0919xxxxxx」的訊息
                    if (Api_Data.Phone != drChk1[0]["Phone"].ToString())
                    {
                        Api_Data.ApiTitle = "您有變更手機號碼？";
                        Api_Data.DataChangeMsg.Add("原手機號碼 「" + drChk1[0]["Phone"].ToString() + "」 是否變更為 「" + Api_Data.Phone + "」");
                        Api_Data.IsChgShow = true;
                    }

                    Api_Data.MID = drChk1[0]["MID"].ToString();

                    return true;
                }
                else if (drChk2.Count() > 0)
                { //若用手機, 姓名可以取得資料時

                    //若判斷小組有錯(比對DB和輸入的資料不一樣)
                    //回傳「是否有更換小組為xx小組」的訊息
                    //if (arr_group[1] != (drChk2[0]["GroupCName"].ToString() + "-" + drChk2[0]["GroupName"].ToString()))
                    if (Api_Data.group.Split('.')[1] != (drChk2[0]["GroupCName"].ToString() + "-" + drChk2[0]["GroupName"].ToString()))
                    {
                        Api_Data.ApiTitle = "您有變更小組？";
                        Api_Data.DataChangeMsg.Add("原 「" + drChk2[0]["GroupCName"].ToString() + "-" + drChk2[0]["GroupName"].ToString() + "」 " +
                                                       "是否變更小組為 「" + Api_Data.group + "」");
                        Api_Data.IsChgShow = true;
                    }

                    Api_Data.MID = drChk2[0]["MID"].ToString();

                    return true;
                }
                else if (drChk3.Count() > 0)
                { //若用手機, 小組可以取得資料時

                    //若判斷姓名有錯(比對DB和輸入的資料不一樣)
                    //回傳「是否有更改姓名為xxx」的訊息
                    if (Api_Data.Ename != drChk3[0]["Ename"].ToString())
                    {
                        Api_Data.ApiTitle = "您有改名字？";
                        Api_Data.DataChangeMsg.Add("原 「" + drChk3[0]["Ename"].ToString() + "」 之後是否改名為 「" + Api_Data.Ename + "」");
                        Api_Data.IsChgShow = true;
                    }

                    Api_Data.MID = drChk3[0]["MID"].ToString();

                    return true;
                }
                else if (drChk4.Count() == 1)
                { //若用姓名, 且資料只有一筆時(不能有其他同名同姓的會友)

                    Api_Data.MID = drChk4[0]["MID"].ToString();

                    return true;
                }
            }

            return false;

        }

    }
}