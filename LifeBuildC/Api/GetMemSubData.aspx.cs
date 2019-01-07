/*
 用途：
   生命建造-線上查詢 

 流程：
   [View]MemSubQuery > 查詢 > [API]GetMemSubData

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
    public partial class GetMemSubData : System.Web.UI.Page
    {
        static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        AdoInfo Ado_Info = new AdoInfo();
        ApiInfo Api_Info = new ApiInfo();
        ApiData.ApiGetMemSubData Api_Data = new ApiData.ApiGetMemSubData();

        ChcMemberADO chcmember = new ChcMemberADO();
        ClassStatusADO cstatus = new ClassStatusADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/LogConfig/GetMemSubData.config")));

            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    Api_Info.strJsonData = readStream.ReadToEnd();
                }
            }

            Api_Data = JsonConvert.DeserializeObject<ApiData.ApiGetMemSubData>(Api_Info.strJsonData);

            if (Api_Data != null)
            {
                logger.Info("StreamR=[" + Api_Info.strJsonData + "]");

                try
                {
                    ApiProcess();
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

        private void ApiProcess()
        {
            string[] arrg = Api_Data.group.Split('.');
            string GroupCName = arrg[1].Split('-')[0];
            string GroupName = arrg[1].Split('-')[1];

            DataTable dt = chcmember.GetChcMemberByGroup(GroupCName, GroupName, Api_Data.Ename);
            DataTable dtStatus = cstatus.QueryByClassStatus();
            if (dt != null && dt.Rows.Count > 0)
            {
                #region C1 第一、二課

                //C1 第一、二課
                Api_Data.IsC112 = bool.Parse(dt.Rows[0]["IsC112"].ToString());

                if (Api_Data.IsC112)
                {
                    Api_Data.TxtC112 = "已上課";
                }

                #endregion

                #region C1 第三、四課

                Api_Data.IsC134 = bool.Parse(dt.Rows[0]["IsC134"].ToString());

                if (Api_Data.IsC134)
                {
                    Api_Data.TxtC134 = "已上課";
                }

                #endregion

                #region C1 更深經歷神

                Api_Data.IsC1God = bool.Parse(dt.Rows[0]["IsC1God"].ToString());

                if (Api_Data.IsC1God)
                {
                    Api_Data.TxtC1God = "已上課";
                }

                #endregion

                #region 經歷神營會

                Api_Data.IsExpGod = bool.Parse(dt.Rows[0]["IsExpGod"].ToString());

                if (Api_Data.IsExpGod)
                {
                    Api_Data.TxtIsExpGod = "已上課";
                }

                #endregion

                #region C2 第一、二課

                Api_Data.IsC212 = bool.Parse(dt.Rows[0]["IsC212"].ToString());
                if (Api_Data.IsC212)
                {
                    Api_Data.TxtIsC212 = "已上課";
                }

                #endregion

                #region C2 第三、四課

                Api_Data.IsC234 = bool.Parse(dt.Rows[0]["IsC234"].ToString());
                if (Api_Data.IsC234)
                {
                    Api_Data.TxtIsC234 = "已上課";
                }

                #endregion

                #region C2 第五課

                Api_Data.IsC25 = bool.Parse(dt.Rows[0]["IsC25"].ToString());
                if (Api_Data.IsC25)
                {
                    Api_Data.TxtIsC25 = "已上課";
                }

                #endregion

                #region C2 領袖訓練一

                Api_Data.IsC2L1 = bool.Parse(dt.Rows[0]["IsC2L1"].ToString());
                if (Api_Data.IsC2L1)
                {
                    Api_Data.TxtC2L1 = "已上課";
                }

                #endregion

                #region C1 成績

                Api_Data.TxtC1_Score = dt.Rows[0]["C1_Score"].ToString() + "分";
                if (int.Parse(dt.Rows[0]["C1_Score"].ToString()) >= 70)
                {
                    Api_Data.IsC1_Score = true;
                }

                #endregion

                #region C2 一、二課成績

                Api_Data.TxtC212_Score = dt.Rows[0]["C212_Score"].ToString() + "分";
                if (int.Parse(dt.Rows[0]["C212_Score"].ToString()) >= 70)
                {
                    Api_Data.IsC212_Score = true;
                }

                #endregion

                #region C2 三、四課成績

                Api_Data.TxtC234_Score = dt.Rows[0]["C234_Score"].ToString() + "分";
                if (int.Parse(dt.Rows[0]["C234_Score"].ToString()) >= 70)
                {
                    Api_Data.IsC234_Score = true;
                }

                #endregion

                #region 見證

                Api_Data.Iswitness = bool.Parse(dt.Rows[0]["Iswitness"].ToString());
                if (Api_Data.Iswitness)
                {
                    Api_Data.Txtwitness = "已交";
                }

                #endregion

                #region C2 QT研習營

                Api_Data.IsC2QT = bool.Parse(dt.Rows[0]["IsC2QT"].ToString());
                if (Api_Data.IsC2QT)
                {
                    Api_Data.TxtIsC2QT = "已上課";
                }

                #endregion

                #region C2 榮耀男人&幸福女人

                Api_Data.IsC2MW = bool.Parse(dt.Rows[0]["IsC2MW"].ToString());
                if (Api_Data.IsC2MW)
                {
                    Api_Data.TxtIsC2MW = "已上課";
                }

                #endregion

                #region 九型人格

                Api_Data.IsC3N = bool.Parse(dt.Rows[0]["IsC3N"].ToString());
                if (Api_Data.IsC3N)
                {
                    Api_Data.TxtIsC3N = "已上課";
                }

                #endregion

                #region 人際關係

                Api_Data.IsC3P = bool.Parse(dt.Rows[0]["IsC3P"].ToString());
                if (Api_Data.IsC3P)
                {
                    Api_Data.TxtIsC3P = "已上課";
                }

                #endregion

                #region C1 判定

                Api_Data.TxtC1_Status = "C1 判定" + dt.Rows[0]["C1_Status"].ToString();

                if (dt.Rows[0]["C1_Status"].ToString() == dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString())
                {
                    Api_Data.IsC1_Status = true;
                }

                #endregion

                #region C2 判定

                Api_Data.TxtC2_Status = "C2 判定" + dt.Rows[0]["C2_Status"].ToString();

                if (dt.Rows[0]["C2_Status"].ToString() == dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString())
                {
                    Api_Data.IsC2_Status = true;
                }

                #endregion

                #region C3 判定

                Api_Data.TxtIsC3_Status = "C3 判定" + dt.Rows[0]["C3_Status"].ToString();

                if (dt.Rows[0]["C2_Status"].ToString() == dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString())
                {
                    Api_Data.IsC3_Status = true;
                }

                #endregion

            }
        }

    }
}