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
    public partial class AddFireSubSign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 測試電文

                string strAddFireSubSign =
                @"
                {
                  ""gcroup"": ""社青"",
                  ""group"": ""CA202.信豪牧區-彥伯小組"",
                  ""Ename"": ""大丹"",
                  ""Gmail"": ""dennis866@gmail.com"",
                  ""Phone"": ""0919963322"",
                  ""gender"": ""男生"",
                  ""ClothesSize"": ""S"",
                  ""Course"": ""生命突破""
                }
                ";

                #endregion

                //using (Stream receiveStream = Request.InputStream)
                //{
                //    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                //    {
                //        strAddFireSubSign = readStream.ReadToEnd();
                //    }
                //}

                PageData PageData = JsonConvert.DeserializeObject<PageData>(strAddFireSubSign);

                PageData.ApiMsg = "烈火特會報名成功!";

                Response.Write(JsonConvert.SerializeObject(PageData));
            }
        }

        public class PageData
        {
            //社青
            public string gcroup { get; set; }
            //CA202.信豪牧區-彥伯小組
            public string group { get; set; }
            public string Ename { get; set; }
            public string Phone { get; set; }
            public string Gmail { get; set; }
            public string gender { get; set; }
            public string ClothesSize { get; set; }
            public string Course { get; set; }
            //api功能訊息
            public string ApiMsg { get; set; }
        }

    }
}