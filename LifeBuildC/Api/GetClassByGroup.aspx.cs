/*
 用途：
   生命建造-線上查詢 (個人查詢)

 流程：
   [View]MemSubQuery > 頁面資料載入 > [API]GetGroupByEname > 繼續 > [API]GetClassByGroup

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
    public partial class GetClassByGroup : System.Web.UI.Page
    {
        ChcMemberADO chcmember = new ChcMemberADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageStart();
        }

        private void PageStart()
        {
            PageData PageData = new PageData();
            string strGetClassByGroup = string.Empty;

            if (Request.QueryString["test"] != null &&
                Request.QueryString["test"].ToString() == "1")
            {
                PageData.group = "";
                PageData.ApiMsg = "";
                PageData.IsApiError = false;
            }
            else
            {
                using (Stream receiveStream = Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        strGetClassByGroup = readStream.ReadToEnd();
                    }
                }

                PageData = JsonConvert.DeserializeObject<PageData>(strGetClassByGroup);
            }

            if (PageData != null)
            {
                PageData.IsApiError = false;

                DataTable dtGroup = chcmember.QueryChcMemberByGroupName(PageData.group);
                if (dtGroup != null && dtGroup.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGroup.Rows)
                    {

                    }
                }




            }

            Response.Write(JsonConvert.SerializeObject(PageData));

        }

        public class PageData
        {
            /// <summary>
            /// 小組
            /// </summary>
            public string group { get; set; }
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