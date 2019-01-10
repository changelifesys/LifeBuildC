/*
 用途：
   生命建造-課程報名

 流程：
   [View]SubjectSignUp.aspx?id=c1 > 報名 > [API]AddSubSign

 系統安控設定：
   . C1 沒有審核的限制.
   . C2 沒有通過 C1 就不能上.
   . 上過更深經歷神等同於上過 C1, 審核可以上 C2 
   . 上過領袖1等同於上過 C2, 不用再考試, 審核可以上 C3
   . 上過領袖1沒上過更深經歷神, C2 仍算過關, 審核可以上 C3


 */

using ADO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
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
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class AddSubSign : System.Web.UI.Page
    {
        static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        GoogleSheetApi Google_Sheet_Api;
        AdoInfo Ado_Info = new AdoInfo();
        ApiInfo Api_Info = new ApiInfo();
        ApiData.AddSubSign Api_Data = new ApiData.AddSubSign();

        protected void Page_Load(object sender, EventArgs e)
        {
            XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/LogConfig/AddSubSign.config")));

            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    Api_Info.strJsonData = readStream.ReadToEnd();
                }
            }

            Api_Data = JsonConvert.DeserializeObject<ApiData.AddSubSign>(Api_Info.strJsonData);

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
            Api_Data.CategoryID = (Api_Data.CategoryID).ToUpper();
            Api_Info.GetGroupData(Api_Data.group, Api_Data.gcroup); //小組

            if (Api_Data.CategoryID == "C1")
            { //C1課程沒有限制報名資格

                Google_Sheet_Api = null;
                Google_Sheet_Api = new GoogleSheetApi("1VEAnTd3wfTKub_ANu9te06YGHnG1jvPsTskUJTxudCQ", "報名");
                AddSubSignProcess();
                Api_Data.ApiMsg = "C1 課程報名成功";

            }
            else if (Api_Data.CategoryID == "C2QT")
            {
                Google_Sheet_Api = null;
                Google_Sheet_Api = new GoogleSheetApi("1J0-a1pdcMHMKuIVgi1rKacOjYiLr-PrBNCye_NO6K_g", "報名");

                if ((Api_Data.MID.Split(',').Count() > 1 && Api_Data.MID.Split(',')[1] != "") ||
                    Ado_Info.SubjectInfo_ADO.ChecklimitBySubjectInfo(Api_Data.SID, Api_Data.CategoryID))
                {
                    AddSubSignProcess();
                    Api_Data.ApiMsg = "C2 QT研習營課程報名成功";
                }
                else
                {
                    Api_Data.IsApiError = true;
                    Api_Data.ApiMsg = "C2 QT研習營課程報名人數已經額滿";
                }

            }
            else if (Api_Data.CategoryID == "C2" || 
                         Api_Data.CategoryID == "C2M" || Api_Data.CategoryID == "C2W" ||
                         Api_Data.CategoryID == "C3N" || Api_Data.CategoryID == "C3P")
            {
                //C2課程需上完C1才可報名
                //C2 榮耀男人&C2 幸福女人 需上完C1才可報名
                #region

                //取得MID值
                string _MID = "0";
                if (Api_Data.MID.IndexOf(',') > 0)
                {
                    _MID = Api_Data.MID.Split(',')[0];
                }

                DataTable dtMem = Ado_Info.ChcMember_ADO.QueryChcMemberByMID(_MID);
                if (dtMem != null && dtMem.Rows.Count > 0)
                { //一定要是會友

                    bool IsC112 = bool.Parse(dtMem.Rows[0]["IsC112"].ToString());
                    bool IsC134 = bool.Parse(dtMem.Rows[0]["IsC134"].ToString());
                    bool IsC1God = bool.Parse(dtMem.Rows[0]["IsC1God"].ToString());
                    bool IsC212 = bool.Parse(dtMem.Rows[0]["IsC212"].ToString());
                    bool IsC234 = bool.Parse(dtMem.Rows[0]["IsC234"].ToString());
                    bool IsC25 = bool.Parse(dtMem.Rows[0]["IsC25"].ToString());
                    int C1_Score = int.Parse(dtMem.Rows[0]["C1_Score"].ToString());
                    int C212_Score = int.Parse(dtMem.Rows[0]["C212_Score"].ToString());
                    int C234_Score = int.Parse(dtMem.Rows[0]["C234_Score"].ToString());
                    bool Iswitness = bool.Parse(dtMem.Rows[0]["Iswitness"].ToString());
                    bool IsC2L1 = bool.Parse(dtMem.Rows[0]["IsC2L1"].ToString());
                    bool IsC2MW = bool.Parse(dtMem.Rows[0]["IsC2MW"].ToString());

                    if (Api_Data.CategoryID == "C2" || Api_Data.CategoryID == "C2M" || Api_Data.CategoryID == "C2W")
                    {
                        if ((IsC112 && IsC134) || IsC1God)
                        {
                            Google_Sheet_Api = null;

                            switch (Api_Data.CategoryID)
                            {
                                case "C2":
                                    Google_Sheet_Api = new GoogleSheetApi("1x2gGzbGMC-FloPxG5FAbvF6kcB0tzWLfnQfHMzyKE8A", "報名");
                                    break;
                                case "C2M":
                                    Google_Sheet_Api = new GoogleSheetApi("1xX-rEZCXGiR6zbxyS0LXtj9nFclgG-J19tzT-PYFRZc", "報名");
                                    break;
                                case "C2W":
                                    Google_Sheet_Api = new GoogleSheetApi("1axSXC65UFPc-SC2lO5XQoDky5VXLXSf4Of20YVfBTmk", "報名");
                                    break;

                            }

                            AddSubSignProcess();

                            switch (Api_Data.CategoryID)
                            {
                                case "C2":
                                    Api_Data.ApiMsg = "C2 課程報名成功";
                                    break;
                                case "C2M":
                                    Api_Data.ApiMsg = "C2 榮耀男人課程報名成功";
                                    break;
                                case "C2W":
                                    Api_Data.ApiMsg = "C2 幸福女人課程報名成功";
                                    break;
                            }
                        }
                        else
                        {
                            Api_Data.IsApiError = true;
                            Api_Data.ApiMsg = "(166)您沒有符合上課資格，請點連結自行查詢是否完成 C1 課程";
                            Api_Data.GoLink = "http://changelifesys.org/MemSubQuery.aspx";
                        }
                    }
                    else if (Api_Data.CategoryID == "C3N" || Api_Data.CategoryID == "C3P")
                    {
                        if ((IsC212 && IsC234 && IsC25 && C1_Score >= 70 && C212_Score >= 70 && C234_Score >= 70 && Iswitness) || IsC2L1)
                        {
                            Google_Sheet_Api = null;

                            switch (Api_Data.CategoryID)
                            {
                                case "C3N":
                                    Google_Sheet_Api = new GoogleSheetApi("129gyeEaXnD9CFiT4t8PKH_m_sxDpIg8pFvJtoJjAQqQ", "報名");
                                    break;
                                case "C3P":
                                    Google_Sheet_Api = new GoogleSheetApi("18mKhOcoKrUDDf8MZtNAPfddt7qR7yxfM4rYdXWJn0Ds", "報名");
                                    break;
                            }

                            AddSubSignProcess();

                            switch (Api_Data.CategoryID)
                            {
                                case "C3N":
                                    Api_Data.ApiMsg = "C3 九型人格課程報名成功";
                                    break;
                                case "C3P":
                                    Api_Data.ApiMsg = "C3 人際關係課程報名成功";
                                    break;
                            }
                        }
                        else
                        {
                            Api_Data.IsApiError = true;
                            Api_Data.ApiMsg = "您沒有符合上課資格，請點連結自行查詢是否完成 C2 課程";
                            Api_Data.GoLink = "http://changelifesys.org/MemSubQuery.aspx";
                        }
                    }

                }
                else
                {
                    Api_Data.IsApiError = true;
                    Api_Data.ApiMsg = "(210)您沒有符合上課資格，請點連結自行查詢是否完成 C1 課程";
                    Api_Data.GoLink = "http://changelifesys.org/MemSubQuery.aspx";
                }

                #endregion

            }
        }

        private void AddSubSignProcess()
        {
            //取出資料變更的訊息
            string Memo = string.Empty;
            if (Api_Data.DataChangeMsg != null && Api_Data.DataChangeMsg.Count > 0)
            {
                foreach (string s in Api_Data.DataChangeMsg)
                {
                    Memo += s + ",";
                }
            }

            DataTable dtSub = Ado_Info.SubjectInfo_ADO.GetSubjectDateBySubjectInfo(Api_Data.SID);
            foreach (DataRow drSub in dtSub.Rows)
            {
                Api_Data.SubDate += DateTime.Parse(drSub["SDate"].ToString()).Month.ToString("00") + "/" + DateTime.Parse(drSub["SDate"].ToString()).Day.ToString("00") + ",";
            }


            if (Api_Data.MID.Split(',').Count() > 1 && //MID, No
                Api_Data.MID.Split(',')[1] != "") //No
            { //UPDATE 報名資訊

                //api.MID.Split(',')[0] 為會友MID流水編號
                //陣列索引從 1 開始
                for (int i = 1; i < Api_Data.MID.Split(',').Count(); i++)
                {
                    Ado_Info.ChcMemberSub_Temp_ADO.UpdChcMemberSub_TempByNo(Api_Info.GroupCName, Api_Info.GroupName, Api_Info.GroupClass, Api_Data.Ename, Api_Data.Phone,
                                                                                         Api_Data.Gmail, Memo, Api_Data.MID.Split(',')[i]);
                }
            }
            else
            { //INSERT 報名資訊

                foreach (DataRow drSub in dtSub.Rows)
                {
                    Ado_Info.ChcMemberSub_Temp_ADO.InsChcMemberSub_Temp_2(
                        Api_Data.SID, drSub["CategoryID2"].ToString(), Api_Info.GroupCName, Api_Info.GroupName, Api_Info.GroupClass,
                        Api_Data.Ename, Api_Data.Phone, Api_Data.Gmail, Api_Data.Church, "0", DateTime.Parse(drSub["SDate"].ToString()), Memo, Api_Data.MID.Replace(",", ""), "0");
                    
                }

            }
            
            //去尾「,」號
            if (Api_Data.SubDate != "" && Api_Data.SubDate != null)
            {
                Api_Data.SubDate = Api_Data.SubDate.Substring(0, Api_Data.SubDate.Length - 1);
            }

            //Google Excel Header Data
            switch (Api_Data.gcroup)
            {
                case "家庭組弟兄":
                    Api_Data.group_1 = Api_Data.group;
                    Api_Data.group_2 = "";
                    Api_Data.group_3 = "";
                    Api_Data.group_4 = "";
                    break;
                case "家庭組姊妹":
                    Api_Data.group_1 = "";
                    Api_Data.group_2 = Api_Data.group;
                    Api_Data.group_3 = "";
                    Api_Data.group_4 = "";
                    break;
                case "社青":
                    Api_Data.group_1 = "";
                    Api_Data.group_2 = "";
                    Api_Data.group_3 = Api_Data.group;
                    Api_Data.group_4 = "";
                    break;
                case "學生":
                    Api_Data.group_1 = "";
                    Api_Data.group_2 = "";
                    Api_Data.group_3 = "";
                    Api_Data.group_4 = Api_Data.group;
                    break;
            }

            //Add Google Form Data
            Google_Sheet_Api.AddV4SheetsBychangelifesys(
                new List<object>() {
                                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                                Api_Data.Ename,
                                Api_Data.group_1,
                                Api_Data.group_2,
                                Api_Data.group_3,
                                Api_Data.group_4,
                                Api_Data.SubDate
                            }
            );

        }

    }
}