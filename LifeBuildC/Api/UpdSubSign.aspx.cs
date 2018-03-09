/*
 用途：
   生命建造-課程報到

 流程：
   [View]SubjectCheck.aspx?id=c1 > 報到 > [API]UpdSubSign

 API演算法：
   

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
        PageData pgdata = new PageData();
        string strUpdSubSign = string.Empty;

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
                Request.QueryString["test"].ToString() == "1")
            {

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

                pgdata = JsonConvert.DeserializeObject<PageData>(strUpdSubSign);
            }


            if (pgdata != null)
            {

                if (pgdata.Category_ID == "C1")
                {

                }

                if (pgdata.Category_ID == "C2")
                {

                }

            }




        }

        /// <summary>
        /// API參數
        /// </summary>
        public class PageData
        {
            /// <summary>
            /// 課程ID(SubjectInfo.SID)
            /// </summary>
            public int S_ID { get; set; }
            /// <summary>
            /// 課程類別(ChcMemberSub_Temp.CategoryID)
            /// </summary>
            public string Category_ID { get; set; }
            /// <summary>
            /// 組別(ChcMemberSub_Temp.GroupClass)
            /// </summary>
            public string gcroup { get; set; }
            /// <summary>
            /// 小組(ChcMemberSub_Temp.GroupCName+ChcMemberSub_Temp.GroupName)
            /// </summary>
            public string group { get; set; }
            /// <summary>
            /// 姓名(ChcMemberSub_Temp.Ename)
            /// </summary>
            public string Ename { get; set; }
            /// <summary>
            /// API 訊息
            /// </summary>
            public string ApiMsg { get; set; }
            /// <summary>
            /// API 有錯(true: 有錯; false: 沒有錯)
            /// </summary>
            public bool IsApiError { get; set; }
        }

    }



}