using ADO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin.Api
{
    public partial class DelUserScore : System.Web.UI.Page
    {

        UserScoreADO uscore = new UserScoreADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageStart();
        }

        private void PageStart()
        {
            string USID = CheckStringByRequest("id"); ;

            DataTable dtChk = uscore.ChkUserScoreByUSID(USID);
            if (dtChk != null && dtChk.Rows.Count > 0)
            {
                uscore.DelUserScoreByUSID(USID);

                var DataInfo = new
                {
                    msg = "success"
                };

                Response.Write(JsonConvert.SerializeObject(DataInfo));

            }
            else
            {
                var DataInfo = new
                {
                    msg = "error"
                };

                Response.Write(JsonConvert.SerializeObject(DataInfo));
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

    }
}