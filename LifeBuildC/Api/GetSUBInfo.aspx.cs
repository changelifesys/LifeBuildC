/*
 用途：
   生命建造-課程報名&簽到

 API 流程：
   [View]SubjectSignUp.aspx?id=c1 > 頁面資料載入 > [API]GetSUBInfo

   [View]SubjectCheck.aspx?id=c1 > 頁面資料載入 > [API]GetSUBInfo

 */
using ADO;
using libLifeBuildC;
using log4net;
using log4net.Config;
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
    public partial class GetSUBInfo : System.Web.UI.Page
    {
        static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ApiData.GetSUBInfo Api_Data = new ApiData.GetSUBInfo();
        AdoInfo Ado_Info = new AdoInfo();
        ApiInfo Api_Info = new ApiInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/LogConfig/GetSUBInfo.config")));

            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    Api_Info.strJsonData = readStream.ReadToEnd();
                }
            }

            Api_Data = JsonConvert.DeserializeObject<ApiData.GetSUBInfo>(Api_Info.strJsonData);

            if (Api_Data != null)
            {
                logger.Info("StreamR=[" + Api_Info.strJsonData + "]");
                ApiProcess();
            }

            logger.Info("Recv=[" + JsonConvert.SerializeObject(Api_Data) + "]");
            logger.Info("\r\n");
            Response.Write(JsonConvert.SerializeObject(Api_Data));
            Response.End();
        }

        private void ApiProcess()
        {
            try
            {
                DataTable dt = null;

                switch (Api_Data.PageName)
                {
                    case "SubjectSignUp":
                        dt = Ado_Info.SubjectInfo_ADO.GetSubjectInfo
                                (DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd"),
                                Api_Data.CategoryID);
                        break;
                    case "SubjectCheck":
                        dt = Ado_Info.SubjectInfo_ADO.GetSubjectInfoSDate
                                (DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd"),
                                Api_Data.CategoryID);
                        break;
                    default:
                        dt = Ado_Info.SubjectInfo_ADO.GetSubjectInfo
                                (DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd"),
                                Api_Data.CategoryID);
                        break;
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    Api_Data.SID = int.Parse(dt.Rows[0]["SID"].ToString());
                   
                    if (Api_Data.PageName == "SubjectSignUp")
                    {
                        Api_Data.SubName = dt.Rows[0]["SubName"].ToString();
                        Api_Data.HtmlSubDesc = dt.Rows[0]["HtmlSubDesc"].ToString().Replace("\"", "'");
                    }
                    else
                    {
                        Api_Data.SubName = dt.Rows[0]["SubName"].ToString().Replace("報名", "簽到");
                    }

                }
                else
                {
                    Api_Data.IsApiError = true;

                    switch (Api_Data.PageName)
                    {
                        case "SubjectSignUp":
                            Api_Data.ApiMsg = "尚未開放" + Api_Data.CategoryID + "報名時間";
                            break;
                        case "SubjectCheck":
                            Api_Data.ApiMsg = "尚未開放" + Api_Data.CategoryID + "簽到時間";
                            break;
                        default:
                            Api_Data.ApiMsg = "尚未開放" + Api_Data.CategoryID + "報名時間";
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                Api_Data.IsApiError = true;
                Api_Data.ApiMsg = "請確認網路是否斷線";
            }


        }

    }
}