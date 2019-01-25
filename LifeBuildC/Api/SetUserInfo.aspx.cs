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
    public partial class SetUserInfo : System.Web.UI.Page
    {
        static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        AdoInfo Ado_Info = new AdoInfo();
        ApiInfo Api_Info = new ApiInfo();
        ApiData.SetUserInfo Api_Data = new ApiData.SetUserInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/LogConfig/SetUserInfo.config")));

            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    Api_Info.strJsonData = readStream.ReadToEnd();
                }
            }

            Api_Data = JsonConvert.DeserializeObject<ApiData.SetUserInfo>(Api_Info.strJsonData);

            if (Api_Data != null)
            {
                logger.Info("StreamR=[" + Api_Info.strJsonData + "]");

                try
                {
                    if (Request.QueryString["ac"] != null && Request.QueryString["ac"].ToString() != "")
                    {

                        switch(Request.QueryString["ac"].ToString())
                        {
                            case "1": //查詢資料
                                ApiProcess1();
                                break;
                            case "2": //儲存資料
                                ApiProcess2();
                                break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                    Api_Data.IsApiError = true;
                    Api_Data.ApiMsg = "請確認網路是否斷線或填寫的資料內容有誤";
                }

            }

            logger.Info("Recv=[" + JsonConvert.SerializeObject(Api_Data) + "]");
            logger.Info("\r\n");
            Response.Write(JsonConvert.SerializeObject(Api_Data));
            Response.End();

        }

        private void ApiProcess1()
        {
            DataTable dt = new DataTable();
            dt = Ado_Info.ChcMemberApp_Temp_ADO.QueryDataWhereUUID_uptyn(Api_Data.UUID);
            if (!(dt != null && dt.Rows.Count > 0))
            { //ChcMemberApp_Temp 沒有資料就查詣 ChcMember
                dt = null;
                dt = Ado_Info.ChcMember_ADO.QueryChcMemberByUUID(Api_Data.UUID);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                Api_Data.MID = dt.Rows[0]["MID"].ToString();
                Api_Data.gcroup = dt.Rows[0]["GroupClass"].ToString();
                Api_Data.group = Api_Info.Get_group(dt.Rows[0]["GroupCName"].ToString(), dt.Rows[0]["GroupName"].ToString(), dt.Rows[0]["GroupClass"].ToString());
                Api_Data.Ename = dt.Rows[0]["Ename"].ToString();
                Api_Data.Phone = dt.Rows[0]["Phone"].ToString();
                Api_Data.Gmail = dt.Rows[0]["Gmail"].ToString();
                Api_Data.TithingNo = dt.Rows[0]["TithingNo"].ToString();
            }

        }

        private void ApiProcess2()
        {

        }

    }
}