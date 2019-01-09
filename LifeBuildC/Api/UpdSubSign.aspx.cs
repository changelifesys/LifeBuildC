/*
  
 用途：
   生命建造-課程報到

 流程：
   [View]SubjectCheck.aspx?id=c1 > [按鈕]報到 > [API]UpdSubSign

 API條件：
   1. C1 可以現場報到. (C1 有沒有報名都沒有差別)
   2. C2 沒有報名就不能上.
   3. C2 若有中央同工的權限可現場報名.
   
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
    public partial class UpdSubSign : System.Web.UI.Page
    {
        static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        GoogleSheetApi Google_Sheet_Api;
        AdoInfo Ado_Info = new AdoInfo();
        ApiInfo Api_Info = new ApiInfo();
        ApiData.ApiUpdSubSign Api_Data = new ApiData.ApiUpdSubSign();

        protected void Page_Load(object sender, EventArgs e)
        {
            XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/LogConfig/UpdSubSign.config")));

            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    Api_Info.strJsonData = readStream.ReadToEnd();
                }
            }

            Api_Data = JsonConvert.DeserializeObject<ApiData.ApiUpdSubSign>(Api_Info.strJsonData);

            if (Api_Data != null)
            {
                logger.Info("StreamR=[" + Api_Info.strJsonData + "]");
                Api_Info.ClassStatus_C001 = Ado_Info.ClassStatus_ADO.GetClassStatusByStatusID("C001");

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
            switch (Api_Data.CategoryID)
            {
                case "C1":

                    //C1 可以現場報名

                    Google_Sheet_Api = null;
                    Google_Sheet_Api = new GoogleSheetApi("1VEAnTd3wfTKub_ANu9te06YGHnG1jvPsTskUJTxudCQ", "簽到");
                    UpdSubSignProcess();
                    Api_Data.ApiMsg = "C1 課程簽到成功";
                    break;

                case "C2QT":

                    //C2 QT 不能現場報名
                    //C2 QT 報名規則同 C1

                    if (Api_Data.MID.Split(',').Count() > 1 && //MID,No
                        Api_Data.MID.Split(',')[1] != "") //No
                    {
                        Google_Sheet_Api = null;
                        Google_Sheet_Api = new GoogleSheetApi("1J0-a1pdcMHMKuIVgi1rKacOjYiLr-PrBNCye_NO6K_g", "簽到");
                        UpdSubSignProcess();
                        Api_Data.ApiMsg = "C2 QT研習營課程簽到成功";
                    }
                    else
                    {
                        Api_Data.IsApiError = true;
                        Api_Data.ApiMsg = "(109)無法簽到，查詢您並沒有報名上課";
                        Api_Data.GoLink = "http://changelifesys.org/MemSubQuery.aspx";
                    }

                    break;

                case "C2":
                case "C2M":
                case "C2W":
                case "C3N":
                case "C3P":

                    if (Api_Data.MID != "")
                    {
                        //C2 需通過 C1 才可報名
                        //C2 不能現場報名

                        //C2 榮耀男人&C2 幸福女人 需上完C1才可報名

                        if (Ado_Info.ChcMemberSub_Temp_ADO.ChkChcMemberSub_TempByMID(int.Parse(Api_Data.MID.Split(',')[0]), Api_Data.SID))
                        {
                            Google_Sheet_Api = null;

                            switch (Api_Data.CategoryID)
                            {
                                case "C2":
                                    Google_Sheet_Api = new GoogleSheetApi("1x2gGzbGMC-FloPxG5FAbvF6kcB0tzWLfnQfHMzyKE8A", "簽到");
                                    break;
                                case "C2M":
                                    Google_Sheet_Api = new GoogleSheetApi("1xX-rEZCXGiR6zbxyS0LXtj9nFclgG-J19tzT-PYFRZc", "簽到");
                                    break;
                                case "C2W":
                                    Google_Sheet_Api = new GoogleSheetApi("1axSXC65UFPc-SC2lO5XQoDky5VXLXSf4Of20YVfBTmk", "簽到");
                                    break;
                                case "C3N":
                                    Google_Sheet_Api = new GoogleSheetApi("129gyeEaXnD9CFiT4t8PKH_m_sxDpIg8pFvJtoJjAQqQ", "簽到");
                                    break;
                                case "C3P":
                                    Google_Sheet_Api = new GoogleSheetApi("18mKhOcoKrUDDf8MZtNAPfddt7qR7yxfM4rYdXWJn0Ds", "簽到");
                                    break;
                            }

                            UpdSubSignProcess();

                            switch (Api_Data.CategoryID)
                            {
                                case "C2":
                                    Api_Data.ApiMsg = "C2 課程簽到成功";
                                    break;
                                case "C2M":
                                    Api_Data.ApiMsg = "C2 榮耀男人課程簽到成功";
                                    break;
                                case "C2W":
                                    Api_Data.ApiMsg = "C2 幸福女人課程簽到成功";
                                    break;
                                case "C3N":
                                    Api_Data.ApiMsg = "C3 九型人格課程簽到成功";
                                    break;
                                case "C3P":
                                    Api_Data.ApiMsg = "C3 人際關係課程簽到成功";
                                    break;
                            }

                        }
                        else
                        {
                            Api_Data.IsApiError = true;
                            Api_Data.ApiMsg = "(116)無法簽到，查詢您並沒有報名上課";
                            Api_Data.GoLink = "http://changelifesys.org/MemSubQuery.aspx";
                        }
                    }
                    else
                    {
                        Api_Data.IsApiError = true;
                        Api_Data.ApiMsg = "(123)無法簽到，查詢您並沒有報名上課";
                        Api_Data.GoLink = "http://changelifesys.org/MemSubQuery.aspx";
                    }
                    break;
            }
        }

        private void UpdSubSignProcess()
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

            string strMake = ""; //是否補課(是: 補課 ; 空值: 不用補課)
            int IsPass = 0; //是否這次通過(0: 尚有缺課; 1: 這次上完課就通過)
            if (Api_Data.MID.Split(',').Count() > 1 && //MID,No
                Api_Data.MID.Split(',')[1] != "") //No
            { //UPDATE 報名資訊

                //INSERT LOG
                DataTable dtMem = Ado_Info.ChcMember_ADO.QueryChcMemberByMID(Api_Data.MID.Split(',')[0]); //MID
                if (dtMem != null && dtMem.Rows.Count > 0)
                {
                    Ado_Info.ChcMember_Log_ADO.InsLogDataByChcMember_Log
                    (
                        dtMem.Rows[0]["MID"].ToString(),
                        dtMem.Rows[0]["GroupName"].ToString(),
                        dtMem.Rows[0]["GroupName"].ToString(),
                        dtMem.Rows[0]["GroupClass"].ToString(),
                        dtMem.Rows[0]["Ename"].ToString(),
                        dtMem.Rows[0]["Phone"].ToString(),
                        dtMem.Rows[0]["Gmail"].ToString(),
                        dtMem.Rows[0]["Church"].ToString(),
                        dtMem.Rows[0]["C1_Status"].ToString(),
                        dtMem.Rows[0]["C2_Status"].ToString(),
                        bool.Parse(dtMem.Rows[0]["IsC112"].ToString()),
                        bool.Parse(dtMem.Rows[0]["IsC134"].ToString()),
                        bool.Parse(dtMem.Rows[0]["IsC212"].ToString()),
                        bool.Parse(dtMem.Rows[0]["IsC234"].ToString()),
                        bool.Parse(dtMem.Rows[0]["IsC25"].ToString()),
                        int.Parse(dtMem.Rows[0]["C1_Score"].ToString()),
                        int.Parse(dtMem.Rows[0]["C212_Score"].ToString()),
                        int.Parse(dtMem.Rows[0]["C234_Score"].ToString()),
                        dtMem.Rows[0]["witness"].ToString(),
                        bool.Parse(dtMem.Rows[0]["Iswitness"].ToString()),
                        dtMem.Rows[0]["Memo"].ToString()
                    );
                }

                //處理No報名資料流水號
                string strNo = string.Empty;
                for (int i = 1; i < Api_Data.MID.Split(',').Count(); i++)
                {
                    strNo += "'" + Api_Data.MID.Split(',')[i] + "',";
                }

                strNo = strNo.Substring(0, strNo.Length - 1);
                DataTable dtNo = Ado_Info.ChcMemberSub_Temp_ADO.QueryChcMemberSub_TempByNo(strNo);


                //填寫補課資訊
                //DataTable dtSubDate = Ado_Info.SubjectDate_ADO.GetCategoryIDBySubjectDate(Api_Data.SID);
                DataRow[] drCategoryID = dtNo.Select("SubDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "'");
                if (drCategoryID.Count() > 0)
                {
                    switch (drCategoryID[0]["CategoryID"].ToString())
                    {
                        case "C112":

                            if (dtMem != null && dtMem.Rows.Count > 0 &&
                                bool.Parse(dtMem.Rows[0]["IsC134"].ToString()))
                            {
                                if (dtMem.Rows[0]["C1_Status"].ToString() == Api_Info.ClassStatus_C001)
                                {
                                    IsPass = 2;
                                }
                                else
                                {
                                    IsPass = 1;
                                }

                                
                            }
                            else if (drCategoryID != null && drCategoryID.Count() > 0 &&
                                dtNo.Select("CategoryID='C134' AND EStatus='True'").Count() > 0)
                            {
                                IsPass = 1;
                            }
                            else
                            {
                                strMake = "未修C1 三、四課";
                            }

                            break;
                        case "C134":

                            if (dtMem != null && dtMem.Rows.Count > 0 &&
                                bool.Parse(dtMem.Rows[0]["IsC112"].ToString()))
                            {
                                if (dtMem.Rows[0]["C1_Status"].ToString() == Api_Info.ClassStatus_C001)
                                {
                                    IsPass = 2;
                                }
                                else
                                {
                                    IsPass = 1;
                                }
                            }
                            else if (drCategoryID != null && drCategoryID.Count() > 0 &&
                                        dtNo.Select("CategoryID='C112' AND EStatus='True'").Count() > 0)
                            {
                                IsPass = 1;
                            }
                            else
                            {
                                strMake = "未修C1 一、二課";
                            }

                            break;
                        case "C212":

                            if (dtMem != null && dtMem.Rows.Count > 0 &&
                                bool.Parse(dtMem.Rows[0]["IsC234"].ToString()) &&
                                bool.Parse(dtMem.Rows[0]["IsC25"].ToString()) &&
                                int.Parse(dtMem.Rows[0]["C1_Score"].ToString()) >= 70 &&
                                int.Parse(dtMem.Rows[0]["C212_Score"].ToString()) >= 70 &&
                                int.Parse(dtMem.Rows[0]["C234_Score"].ToString()) >= 70)
                            {
                                if (dtMem.Rows[0]["C2_Status"].ToString() == Api_Info.ClassStatus_C001)
                                {
                                    IsPass = 2;
                                }
                                else
                                {
                                    IsPass = 1;
                                }
                            }
                            else
                            {
                                if (dtMem != null && dtMem.Rows.Count > 0 &&
                                    bool.Parse(dtMem.Rows[0]["IsC234"].ToString()) == false)
                                {
                                    strMake += "未修C2 三、四課,";
                                }

                                if (dtMem != null && dtMem.Rows.Count > 0 &&
                                    bool.Parse(dtMem.Rows[0]["IsC25"].ToString()) == false)
                                {
                                    strMake += "未修C2 第五課,";
                                }

                                try
                                {
                                    if (int.Parse(dtMem.Rows[0]["C1_Score"].ToString()) < 70)
                                    {
                                        strMake += "C1 考試未通過,";
                                    }
                                }
                                catch
                                {
                                    strMake += "C1 尚未考試,";
                                }

                                try
                                {
                                    if (int.Parse(dtMem.Rows[0]["C212_Score"].ToString()) < 70)
                                    {
                                        strMake += "C2 一、二課考試未通過,";
                                    }
                                }
                                catch
                                {
                                    strMake += "C2 一、二課尚未考試,";
                                }

                                try
                                {
                                    if (int.Parse(dtMem.Rows[0]["C234_Score"].ToString()) < 70)
                                    {
                                        strMake += "C2 三、四課考試未通過,";
                                    }
                                }
                                catch
                                {
                                    strMake += "C2 三、四課尚未考試,";
                                }

                            }

                            break;
                        case "C234":

                            if (dtMem != null && dtMem.Rows.Count > 0 &&
                                bool.Parse(dtMem.Rows[0]["IsC212"].ToString()) &&
                                bool.Parse(dtMem.Rows[0]["IsC25"].ToString()) &&
                                int.Parse(dtMem.Rows[0]["C1_Score"].ToString()) >= 70 &&
                                int.Parse(dtMem.Rows[0]["C212_Score"].ToString()) >= 70 &&
                                int.Parse(dtMem.Rows[0]["C234_Score"].ToString()) >= 70)
                            {
                                if (dtMem.Rows[0]["C2_Status"].ToString() == Api_Info.ClassStatus_C001)
                                {
                                    IsPass = 2;
                                }
                                else
                                {
                                    IsPass = 1;
                                }
                            }
                            else
                            {
                                if (dtMem != null && dtMem.Rows.Count > 0 &&
                                    bool.Parse(dtMem.Rows[0]["IsC212"].ToString()) == false)
                                {
                                    strMake += "未修C2 一、二課,";
                                }

                                if (dtMem != null && dtMem.Rows.Count > 0 &&
                                    bool.Parse(dtMem.Rows[0]["IsC25"].ToString()) == false)
                                {
                                    strMake += "未修C2 第五課,";
                                }

                                try
                                {
                                    if (int.Parse(dtMem.Rows[0]["C1_Score"].ToString()) < 70)
                                    {
                                        strMake += "C1 考試未通過,";
                                    }
                                }
                                catch
                                {
                                    strMake += "C1 尚未考試,";
                                }

                                try
                                {
                                    if (int.Parse(dtMem.Rows[0]["C212_Score"].ToString()) < 70)
                                    {
                                        strMake += "C2 一、二課考試未通過,";
                                    }
                                }
                                catch
                                {
                                    strMake += "C2 一、二課尚未考試,";
                                }

                                try
                                {
                                    if (int.Parse(dtMem.Rows[0]["C234_Score"].ToString()) < 70)
                                    {
                                        strMake += "C2 三、四課考試未通過,";
                                    }
                                }
                                catch
                                {
                                    strMake += "C2 三、四課尚未考試,";
                                }

                            }

                            break;
                        case "C25":

                            if (dtMem != null && dtMem.Rows.Count > 0 &&
                                bool.Parse(dtMem.Rows[0]["IsC212"].ToString()) &&
                                bool.Parse(dtMem.Rows[0]["IsC234"].ToString()) &&
                                int.Parse(dtMem.Rows[0]["C1_Score"].ToString()) >= 70 &&
                                int.Parse(dtMem.Rows[0]["C212_Score"].ToString()) >= 70 &&
                                int.Parse(dtMem.Rows[0]["C234_Score"].ToString()) >= 70)
                            {
                                if (dtMem.Rows[0]["C2_Status"].ToString() == Api_Info.ClassStatus_C001)
                                {
                                    IsPass = 2;
                                }
                                else
                                {
                                    IsPass = 1;
                                }
                            }
                            else
                            {
                                if (dtMem != null && dtMem.Rows.Count > 0 &&
                                    bool.Parse(dtMem.Rows[0]["IsC212"].ToString()) == false)
                                {
                                    strMake += "未修C2 一、二課,";
                                }

                                if (dtMem != null && dtMem.Rows.Count > 0 &&
                                    bool.Parse(dtMem.Rows[0]["IsC234"].ToString()) == false)
                                {
                                    strMake += "未修C2 三、四課,";
                                }

                                try
                                {
                                    if (int.Parse(dtMem.Rows[0]["C1_Score"].ToString()) < 70)
                                    {
                                        strMake += "C1 考試未通過,";
                                    }
                                }
                                catch
                                {
                                    strMake += "C1 尚未考試,";
                                }

                                try
                                {
                                    if (int.Parse(dtMem.Rows[0]["C212_Score"].ToString()) < 70)
                                    {
                                        strMake += "C2 一、二課考試未通過,";
                                    }
                                }
                                catch
                                {
                                    strMake += "C2 一、二課尚未考試,";
                                }

                                try
                                {
                                    if (int.Parse(dtMem.Rows[0]["C234_Score"].ToString()) < 70)
                                    {
                                        strMake += "C2 三、四課考試未通過,";
                                    }
                                }
                                catch
                                {
                                    strMake += "C2 三、四課尚未考試,";
                                }

                            }

                            break;

                        case "C2M":
                        case "C2W":
                            IsPass = 1;
                            break;
                    }
                }
                else
                {
                    strMake = "修課查詢有誤(請洽資訊同工)";
                }
                

                //api.MID.Split(',')[0] 為會友MID流水編號
                //api.MID.Split(',')[i] 大於0以上為No流水號
                //故陣列索引從 1 開始
                for (int i = 1; i < Api_Data.MID.Split(',').Count(); i++)
                {
                    //UPDATE 基本資料
                    Ado_Info.ChcMemberSub_Temp_ADO.UpdChcMemberSub_TempByNo
                    (
                        Api_Info.GroupCName,
                        Api_Info.GroupName,
                        Api_Info.GroupClass,
                        Api_Data.Ename,
                        Api_Data.Phone,
                        Api_Data.Gmail, 
                        Memo,
                        Api_Data.MID.Split(',')[i] //No
                    );

                    //UPDATE 上課狀態
                    Ado_Info.ChcMemberSub_Temp_ADO.UpdEStatusByChcMemberSub_Temp
                    (
                        Api_Data.MID.Split(',')[i], //No
                        IsPass,
                        strMake
                    );
                }

            }
            else
            { //INSERT 報名資訊, 只有C1才能現場報名

                DataTable dtMem = new DataTable();

                if (Api_Data.MID.Split(',').Count() > 0)
                {
                    //INSERT LOG
                    dtMem = Ado_Info.ChcMember_ADO.QueryChcMemberByMID(Api_Data.MID.Split(',')[0]);
                    if (dtMem != null && dtMem.Rows.Count > 0)
                    {
                        Ado_Info.ChcMember_Log_ADO.InsLogDataByChcMember_Log
                        (
                            dtMem.Rows[0]["MID"].ToString(),
                            dtMem.Rows[0]["GroupName"].ToString(),
                            dtMem.Rows[0]["GroupName"].ToString(),
                            dtMem.Rows[0]["GroupClass"].ToString(),
                            dtMem.Rows[0]["Ename"].ToString(),
                            dtMem.Rows[0]["Phone"].ToString(),
                            dtMem.Rows[0]["Gmail"].ToString(),
                            dtMem.Rows[0]["Church"].ToString(),
                            dtMem.Rows[0]["C1_Status"].ToString(),
                            dtMem.Rows[0]["C2_Status"].ToString(),
                            bool.Parse(dtMem.Rows[0]["IsC112"].ToString()),
                            bool.Parse(dtMem.Rows[0]["IsC134"].ToString()),
                            bool.Parse(dtMem.Rows[0]["IsC212"].ToString()),
                            bool.Parse(dtMem.Rows[0]["IsC234"].ToString()),
                            bool.Parse(dtMem.Rows[0]["IsC25"].ToString()),
                            int.Parse(dtMem.Rows[0]["C1_Score"].ToString()),
                            int.Parse(dtMem.Rows[0]["C212_Score"].ToString()),
                            int.Parse(dtMem.Rows[0]["C234_Score"].ToString()),
                            dtMem.Rows[0]["witness"].ToString(),
                            bool.Parse(dtMem.Rows[0]["Iswitness"].ToString()),
                            dtMem.Rows[0]["Memo"].ToString()
                        );
                    }
                }

                //填寫補課資訊
                DataTable dtCategoryID = Ado_Info.SubjectDate_ADO.GetCategoryIDBySubjectDate(Api_Data.SID);
                if (dtCategoryID != null && dtCategoryID.Rows.Count > 0)
                {
                    switch (dtCategoryID.Rows[0]["CategoryID"].ToString())
                    {
                        case "C112":

                            if (dtMem != null && dtMem.Rows.Count > 0 &&
                                bool.Parse(dtMem.Rows[0]["IsC134"].ToString()))
                            {
                                if (dtMem.Rows[0]["C1_Status"].ToString() == Api_Info.ClassStatus_C001)
                                {
                                    IsPass = 2;
                                }
                                else
                                {
                                    IsPass = 1;
                                }
                            }
                            else
                            {
                                strMake = "未修C1 三、四課";
                            }

                            break;

                        case "C134":

                            if (dtMem != null && dtMem.Rows.Count > 0 &&
    bool.Parse(dtMem.Rows[0]["IsC112"].ToString()))
                            {
                                if (dtMem.Rows[0]["C1_Status"].ToString() == Api_Info.ClassStatus_C001)
                                {
                                    IsPass = 2;
                                }
                                else
                                {
                                    IsPass = 1;
                                }
                            }
                            else
                            {
                                strMake = "未修C1 一、二課";
                            }

                            break;
                    }

                    DataTable dtSub = Ado_Info.SubjectInfo_ADO.GetSDateBySubjectJoin(Api_Data.SID, DateTime.Now.ToString("yyyy/MM/dd"));
                    if (dtSub != null && dtSub.Rows.Count > 0)
                    {
                        Ado_Info.ChcMemberSub_Temp_ADO.InsChcMemberSub_TempByUpdSubSignToC1
                        (
                            Api_Data.SID,
                            dtCategoryID.Rows[0]["CategoryID"].ToString(),
                            Api_Info.GroupCName,
                            Api_Info.GroupName,
                            Api_Info.GroupClass,
                            Api_Data.Ename,
                            Api_Data.Phone,
                            Api_Data.Gmail,
                            Api_Data.Church,
                            "1", //EStatus
                            DateTime.Now.ToString("yyyy/MM/dd"), //SubDate
                            Memo,
                            Api_Data.MID.Replace(",", ""), //MID
                            IsPass,
                            strMake
                        );

                    }

                }



            }

            Api_Data.SubDate = DateTime.Now.ToString("MM/dd");

            //google Excel 組別分類
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

            Google_Sheet_Api.AddV4SheetsBychangelifesys(
                new List<object>() {
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    Api_Data.Ename,
                    Api_Data.group_1,
                    Api_Data.group_2,
                    Api_Data.group_3,
                    Api_Data.group_4,
                    Api_Data.SubDate,
                    IsPass,
                    strMake
                }
            );

        }



    }



}