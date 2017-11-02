//API 說明
//取得SID用
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
    public partial class GetSubSign : System.Web.UI.Page
    {
        //上課日期
        SubjectDateADO SubjectDate = new SubjectDateADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 測試電文

            string ReqGetSubSign =
@"
{
  ""Category_ID"" : ""C1""
}
";

            #endregion

            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    ReqGetSubSign = readStream.ReadToEnd();
                }
            }

            PageData PageData = JsonConvert.DeserializeObject<PageData>(ReqGetSubSign);

            DataTable dtSID = SubjectDate.QuerySIDBySubjectDate(PageData.Category_ID, DateTime.Now.ToString("yyyy/MM/dd"));
            if (dtSID.Rows.Count > 0)
            { //表示可以簽到
                PageData.S_ID = int.Parse(dtSID.Rows[0]["SID"].ToString());
                PageData.IsPass = true;
            }
            else
            { //表示不可簽到
                PageData.IsPass = false;
                PageData.Msg = PageData.Msg + "課程還未開放報到";
            }

            Response.Write(JsonConvert.SerializeObject(PageData));

        }

        public class PageData
        {
            public int S_ID { get; set; } //2 課程編號
            public string Category_ID { get; set; } //課程類別

            //IsPass = true 表示可以簽到 || IsPass = false 表示關閉簽到
            public bool IsPass { get; set; }

            public string Msg { get; set; }

        }

    }
}