/*
  
 用途：
   生命建造-課程報到

 流程：
   [View]SubjectCheck.aspx?id=c1 > [按鈕]報到 > [API]UpdSubSign

 API條件：
   1. C1 可以現場報到. (C1 有沒有報名都沒有差別)
   2. C2 沒有報名就不能上.
   3. C2 若有中央同工的權限可現場報名.
   
 */
using ADO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class UpdSubSign : System.Web.UI.Page
    {
        ApiData api = new ApiData();
        ChcMemberSub_TempADO ChcMemberSub_Temp = new ChcMemberSub_TempADO();

        /// <summary>
        /// StreamReader電文
        /// </summary>
        string strUpdSubSign = string.Empty;
        /// <summary>
        /// 牧區
        /// </summary>
        string GroupCName = string.Empty;
        /// <summary>
        /// 小組
        /// </summary>
        string GroupName = string.Empty;
        /// <summary>
        /// 組別
        /// </summary>
        string GroupClass = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageStart();
            }
        }

        private void PageStart()
        {
            if (Request.QueryString["test"] != null &&
                Request.QueryString["test"].ToString() == "true")
            {
                api.CategoryID = "C1";
                api.EStatus = 1;
                api.gcroup = "社青";
                api.group = "CA202.信豪牧區-彥伯小組";
                api.Ename = "TEST";
                api.Phone = "0919963322";
            }
            else
            {
                using (Stream receiveStream = Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        strUpdSubSign = readStream.ReadToEnd();
                    }
                }

                api = JsonConvert.DeserializeObject<ApiData>(strUpdSubSign);
            }


            if (api != null)
            {

                //小組
                string[] arrg = api.group.Split('.');
                GroupCName = arrg[1].Split('-')[0];
                GroupName = arrg[1].Split('-')[1];
                GroupClass = api.gcroup;

                if ((api.CategoryID).ToUpper() == "C1")
                {
                    if (ChcMemberSub_Temp.Get_bool_Ename(api.SID, api.CategoryID, GroupCName, GroupName, GroupClass, api.Ename))
                    {
                        //已報名
                        ChcMemberSub_Temp.Upd_EStatus(api.EStatus, api.SID, api.CategoryID, GroupCName, GroupName, GroupClass, api.Ename);
                    }
                    else
                    {
                        //現場報名

                    }

                }
                else if ((api.CategoryID).ToUpper() == "C2" && ChcMemberSub_Temp.Get_bool_Ename(api.SID, api.CategoryID, GroupCName, GroupName, GroupClass, api.Ename))
                {
                    //已報名
                    //C2 課程若沒有中央同工的權限不能現場報名
                    ChcMemberSub_Temp.Upd_EStatus(api.EStatus, api.SID, api.CategoryID, GroupCName, GroupName, GroupClass, api.Ename);
                }

            }




        }

        /// <summary>
        /// API參數
        /// </summary>
        public class ApiData
        {
            /// <summary>
            /// 課程ID
            /// </summary>
            public int SID { get; set; }
            /// <summary>
            /// 上課狀態   0: 報名 1:上課簽到
            /// </summary>
            public int EStatus { get; set; }
            /// <summary>
            ///  課程類別
            /// </summary>
            public string CategoryID { get; set; }
            /// <summary>
            /// 組別
            /// </summary>
            public string gcroup { get; set; }
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
        }

    }



}