/*
 生命建造成績查詢 - 問題反應
 - 資料填寫
 */
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class GetSubjectLine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageData PageData = new PageData();
                string strGetSubjectLine = string.Empty;

                DataInfo dtainfo = new DataInfo();
                dtainfo.CategoryID = "C1";
                dtainfo.CategoryName = "C1課程";
                PageData.DataInfo.Add(dtainfo);

                dtainfo = new DataInfo();
                dtainfo.CategoryID = "C2";
                dtainfo.CategoryName = "C2課程";
                PageData.DataInfo.Add(dtainfo);

                Response.Write(JsonConvert.SerializeObject(PageData));

            }
        }

        public class PageData
        {
            public List<DataInfo> DataInfo = new List<DataInfo>();
        }

        public class DataInfo
        {
            public string CategoryID { get; set; }
            public string CategoryName { get; set; }
        }

    }
}