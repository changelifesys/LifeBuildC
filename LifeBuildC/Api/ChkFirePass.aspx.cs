using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class ChkFirePass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Test 電文

                string strChkFirePass =
                @" 
                {
                   FirePass: ""12345""
                }
                ";

                #endregion

                //using (Stream receiveStream = Request.InputStream)
                //{
                //    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                //    {
                //        strChkFirePass = readStream.ReadToEnd();
                //    }
                //}

                PageData PageData = JsonConvert.DeserializeObject<PageData>(strChkFirePass);

                if (PageData.FirePass == "12345")
                {
                    PageData.IsPass = true;
                    PageData.ChkMsg = "";
                }
                else
                {
                    PageData.ChkMsg = "請輸入正確的密碼!";
                }

                Response.Write(JsonConvert.SerializeObject(PageData));
            }
        }

        public class PageData
        {
            //傳入的密碼
            public string FirePass { get; set; }
            //True: 通過驗證
            //False: 沒有通過驗證
            public bool IsPass { get; set; }
            //回傳的訊息
            public string ChkMsg { get; set; }
        }

    }
}