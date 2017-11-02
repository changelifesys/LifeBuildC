using ADO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// 取得答案項目
/// </summary>
namespace LifeBuildC.Api
{
    public partial class GetAnsItem : System.Web.UI.Page
    {
        AnswerItemADO ansitem = new AnswerItemADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageStart();
        }

        private void PageStart()
        {
            string eclass = "C212";

            eclass = CheckStringByRequest("eclass");

            PageData PageData = new PageData();
            DataTable dt = ansitem.QueryAnswerItem(eclass);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataInfo dinfo = new DataInfo();
                    dinfo.ItemName = dr["ItemName"].ToString();
                    PageData.DataInfo.Add(dinfo);
                }

                Response.Write(JsonConvert.SerializeObject(PageData));

            }
        }

        /// <summary>
        /// 檢查 Request.Form 傳入的值是否異常，若異常回傳空值。
        /// </summary>
        /// <param name="RequestForm_Name">Request.Form 的名稱</param>
        /// <returns>回傳 Request.Form 為 string 的值</returns>
        private string CheckStringByRequest(string RequestForm_Name)
        {
            string _value = string.Empty;
            if (Request.Form[RequestForm_Name] != null && Request.Form[RequestForm_Name].ToString().Length > 0)
                _value = Request.Form[RequestForm_Name].ToString();

            return _value;
        }

        public class PageData
        {
            public List<DataInfo> DataInfo = new List<DataInfo>();
        }

        public class DataInfo
        {
            public string ItemName;
        }

    }
}