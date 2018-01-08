using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class GetMemSubData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageStart();
        }

        private void PageStart()
        {
            #region Test 電文

            string strGetMemSubData =
            @" 
                {
                   ""gcroup"": ""社青"",
                   ""group"": ""CA202.信豪牧區-彥伯小組"",
                   ""Ename"": ""流大丹"",
                   ""IsC112"": ""true"",
                   ""TxtC112"": ""已上課"",
                   ""IsC134"": ""true"",
                   ""TxtC134"": ""已上課"",
                   ""IsC1_Score"": ""true"",
                   ""TxtC1_Score"": ""100分"",
                   ""IsC212"": ""true"",
                   ""TxtIsC212"": ""已上課"",
                   ""IsC234"": ""true"",
                   ""TxtIsC234"": ""已上課"",
                   ""IsC25"": ""false"",
                   ""TxtIsC25"": ""未上課"",
                   ""Iswitness"": ""false"",
                   ""Txtwitness"": ""未交"",
                   ""IsC212_Score"": ""true"",
                   ""TxtC212_Score"": ""100分"",
                   ""IsC234_Score"": ""false"",
                   ""TxtC234_Score"": ""60分"",
                   ""IsC1_Status"": ""true"",
                   ""TxtC1_Status"": ""C1 判定通過"",
                   ""IsC2_Status"": ""false"",
                   ""TxtC2_Status"": ""C2 判定不通過""
                }
                ";

            #endregion

            PageData PageData = JsonConvert.DeserializeObject<PageData>(strGetMemSubData);
            Response.Write(JsonConvert.SerializeObject(PageData));
        }

        public class PageData
        {
            public string gcroup { get; set; }
            public string group { get; set; }
            public string Ename { get; set; }
            public bool IsC112 { get; set; }
            public string TxtC112 { get; set; }
            public bool IsC134 { get; set; }
            public string TxtC134 { get; set; }
            public bool IsC1_Score { get; set; }
            public string TxtC1_Score { get; set; }
            public bool IsC212 { get; set; }
            public string TxtIsC212 { get; set; }
            public bool IsC234 { get; set; }
            public string TxtIsC234 { get; set; }
            public bool IsC25 { get; set; }
            public string TxtIsC25 { get; set; }
            public bool Iswitness { get; set; }
            public string Txtwitness { get; set; }
            public bool IsC212_Score { get; set; }
            public string TxtC212_Score { get; set; }
            public bool IsC234_Score { get; set; }
            public string TxtC234_Score { get; set; }
            public bool IsC1_Status { get; set; }
            public string TxtC1_Status { get; set; }
            public bool IsC2_Status { get; set; }
            public string TxtC2_Status { get; set; }
        }

    }
}