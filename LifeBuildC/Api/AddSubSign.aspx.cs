/*
 用途：
   生命建造-課程報名

 流程：
   [View]SubjectSignUp.aspx?id=c1 > 報名 > [API]AddSubSign

 API演算法：
   1. C1 沒有審核的限制.
   2. C2 沒有通過 C1 就不能上.

 */

using ADO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
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
        SubjectInfoADO subjectinfo = new SubjectInfoADO();
        ChcMemberSub_TempADO chcmembersubtemp = new ChcMemberSub_TempADO();
        ChcMemberADO chcmember = new ChcMemberADO();
        ApiData api = new ApiData();

        /// <summary>
        /// StreamReader電文
        /// </summary>
        string strAddSubSign = string.Empty;
        /// <summary>
        /// 牧區
        /// </summary>
        string GroupCName = string.Empty;
        /// <summary>
        /// 小組
        /// </summary>
        string GroupName = string.Empty;
        /// <summary>
        /// 組別
        /// </summary>
        string GroupClass = string.Empty;

        String sheetName = "";
        String spreadsheetId = "";

        /// <summary>
        /// API參數
        /// </summary>
        public class ApiData
        {
            /// <summary>
            /// 資料變更訊息
            /// </summary>
            public List<string> DataChangeMsg = new List<string>();
            /// <summary>
            /// 會友流水號
            /// </summary>
            public string MID { get; set; }
            /// <summary>
            ///  課程類別
            /// </summary>
            public string CategoryID { get; set; }
            /// <summary>
            /// 課程ID
            /// </summary>
            public int SID { get; set; }
            /// <summary>
            /// 組別
            /// </summary>
            public string gcroup { get; set; }
            /// <summary>
            /// 小組
            /// </summary>
            public string group { get; set; }
            /// <summary>
            /// 所屬牧區/小組：家庭弟兄
            /// </summary>
            public string group_1 { get; set; }
            /// <summary>
            /// 所屬牧區/小組：家庭姊妹
            /// </summary>
            public string group_2 { get; set; }
            /// <summary>
            /// 所屬牧區/小組：社青
            /// </summary>
            public string group_3 { get; set; }
            /// <summary>
            /// 所屬牧區/小組：學青
            /// </summary>
            public string group_4 { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string Ename { get; set; }
            /// <summary>
            /// 手機
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// Mail
            /// </summary>
            public string Gmail { get; set; }
            /// <summary>
            /// 所屬教會
            /// </summary>
            public string Church { get; set; }
            /// <summary>
            /// API 訊息
            /// </summary>
            public string ApiMsg { get; set; }
            /// <summary>
            /// API 有錯(true: 有錯; false: 沒有錯)
            /// </summary>
            public bool IsApiError { get; set; }
            /// <summary>
            /// 上課時段
            /// </summary>
            public string SubDate { get; set; }
            /// <summary>
            /// 導向的連結
            /// </summary>
            public string GoLink = string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageStart();
            }

        }

        private void PageStart()
        {
            if (Request.QueryString["test"] != null &&
                (Request.QueryString["test"].ToString() == "C1" ||
                Request.QueryString["test"].ToString() == "C2"))
            {


                switch (Request.QueryString["test"].ToString())
                {
                    case "C1":
                        api.DataChangeMsg = null;
                        api.MID = "758";
                        api.SID = 2;
                        api.CategoryID = "C1";
                        api.gcroup = "社青";
                        api.group = "CA202.信豪牧區-彥伯小組";
                        api.Ename = "劉彥伯";
                        api.Phone = "0919963322";
                        api.Gmail = "";
                        api.Church = "";
                        break;
                    case "C2":

                        api.DataChangeMsg = null;
                        api.MID = "758";
                        api.SID = 2;
                        api.CategoryID = "C2";
                        api.gcroup = "社青";
                        api.group = "CA202.信豪牧區-彥伯小組";
                        api.Ename = "劉彥伯";
                        api.Phone = "0919963322";
                        api.Gmail = "";
                        api.Church = "";
                        break;
                }

                
            }
            else
            {
                using (Stream receiveStream = Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        strAddSubSign = readStream.ReadToEnd();
                    }
                }

                api = JsonConvert.DeserializeObject<ApiData>(strAddSubSign);
            }

            if (api != null)
            {
                try
                {
                    api.IsApiError = false;

                    //小組
                    string[] arrg = api.group.Split('.');
                    GroupCName = arrg[1].Split('-')[0];
                    GroupName = arrg[1].Split('-')[1];
                    GroupClass = api.gcroup;

                    if (api.CategoryID == "C1")
                    { //C1課程沒有限制報名資格

                        sheetName = "C1報名";
                        spreadsheetId = "1HCRBI2C_cVl0fH5576PEX7UULWsgcxx1sbYdRQ6FcF8";
                        InsChcMemberSub_Temp();
                        api.ApiMsg = "C1 課程報名成功！";

                    }
                    else if (api.CategoryID == "C2")
                    { //C2課程需上完C1才可報名

                        //DataTable dtMem = chcmember.GetChcMemberByGroup(GroupCName, GroupName, api.Ename);

                        string _MID = "0";
                        if (api.MID.IndexOf(',') > 0)
                        {
                            _MID = api.MID.Split(',')[0];
                        }

                        DataTable dtMem = chcmember.Query_MID(_MID);
                        if (dtMem != null && dtMem.Rows.Count > 0)
                        { //一定要是會友

                            bool IsC112 = bool.Parse(dtMem.Rows[0]["IsC112"].ToString());
                            bool IsC134 = bool.Parse(dtMem.Rows[0]["IsC134"].ToString());
                            if (IsC112 && IsC134)
                            {
                                sheetName = "C2報名";
                                spreadsheetId = "1bKwnh_2XTYvR1bezOnzKEeA66Kyxlj0WAsN3LcL3FBs";
                                InsChcMemberSub_Temp();
                                api.ApiMsg = "C2 課程報名成功！";

                            }
                            else
                            {
                                api.IsApiError = true;
                                api.ApiMsg = "您沒有符合上C2的資格！請向小組長確認或自行查詢是否完成C1課程";
                                api.GoLink = "http://changelifesys.org/MemSubQuery.aspx";
                            }


                        }
                        else
                        {
                            api.IsApiError = true;
                            api.ApiMsg = "您沒有符合上C2的資格！請向小組長確認或自行查詢是否完成C1課程";
                            api.GoLink = "http://changelifesys.org/MemSubQuery.aspx";
                        }

                    }


                }
                catch (Exception ex)
                {
                    api.IsApiError = true;
                    api.ApiMsg = "請確認網路是否斷線或填寫的資料內容有誤";
                }

            }

            Response.Write(JsonConvert.SerializeObject(api));

        }

        private void InsChcMemberSub_Temp()
        {
            //取出資料變更的訊息
            string Memo = string.Empty;
            if (api.DataChangeMsg != null && api.DataChangeMsg.Count > 0)
            {
                foreach (string s in api.DataChangeMsg)
                {
                    Memo += s + ",";
                }
            }

            DataTable dtSub = subjectinfo.GetSubjectDateBySubjectInfo(api.SID);
            foreach (DataRow drSub in dtSub.Rows)
            {
                api.SubDate += DateTime.Parse(drSub["SDate"].ToString()).Month.ToString("00") + "/" + DateTime.Parse(drSub["SDate"].ToString()).Day.ToString("00") + ",";
            }


            if (api.MID.Split(',').Count() > 1 && api.MID.Split(',')[1] != "")
            { //UPDATE 報名資訊

                //api.MID.Split(',')[0] 為會友MID流水編號
                //陣列索引從 1 開始
                for (int i = 1; i < api.MID.Split(',').Count(); i++)
                {
                    chcmembersubtemp.UpdChcMemberSub_TempByNo(GroupCName, GroupName, GroupClass, api.Ename, api.Phone,
                                                                                         api.Gmail, Memo, api.MID.Split(',')[i]);
                }
            }
            else
            { //INSERT 報名資訊


                foreach (DataRow drSub in dtSub.Rows)
                {
                    chcmembersubtemp.InsChcMemberSub_Temp_2(
                        api.SID, drSub["CategoryID2"].ToString(), GroupCName, GroupName, GroupClass,
                        api.Ename, api.Phone, api.Gmail, api.Church, "0", DateTime.Parse(drSub["SDate"].ToString()), Memo, api.MID.Replace(",", ""), "0");
                    
                }

            }
            


            //去尾「,」號
            if (api.SubDate != "" && api.SubDate != null)
            {
                api.SubDate = api.SubDate.Substring(0, api.SubDate.Length - 1);
            }

            //google Excel 組別分類
            switch (api.gcroup)
            {
                case "家庭組弟兄":
                    api.group_1 = api.group;
                    api.group_2 = "";
                    api.group_3 = "";
                    api.group_4 = "";
                    break;
                case "家庭組姊妹":
                    api.group_1 = "";
                    api.group_2 = api.group;
                    api.group_3 = "";
                    api.group_4 = "";
                    break;
                case "社青":
                    api.group_1 = "";
                    api.group_2 = "";
                    api.group_3 = api.group;
                    api.group_4 = "";
                    break;
                case "學生":
                    api.group_1 = "";
                    api.group_2 = "";
                    api.group_3 = "";
                    api.group_4 = api.group;
                    break;
            }

            AddDataByV4Sheets();
        }

        /// <summary>
        /// 新增一筆資料
        /// </summary>
        private void AddDataByV4Sheets()
        {
            SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = GetCredential(),
                ApplicationName = "Get Google SheetData with Google Sheets API",
            });

            var valueRange = new ValueRange();

            var oblist = new List<object>() {
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    api.Ename,
                    api.group_1,
                    api.group_2,
                    api.group_3,
                    api.group_4,
                    api.SubDate
                };

            valueRange.Values = new List<IList<object>> { oblist };

            valueRange.MajorDimension = "Rows"; //Rows or Columns

            SpreadsheetsResource.ValuesResource.AppendRequest request = sheetsService.Spreadsheets.Values.Append(valueRange, spreadsheetId, sheetName);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = request.Execute();
        }

        private UserCredential GetCredential()
        {
            // TODO: Change placeholder below to generate authentication credentials. See:
            // https://developers.google.com/sheets/quickstart/dotnet#step_3_set_up_the_sample
            //
            // Authorize using one of the following scopes:
            //     "https://www.googleapis.com/auth/drive"
            //     "https://www.googleapis.com/auth/drive.file"
            //     "https://www.googleapis.com/auth/spreadsheets"

            string[] Scopes = { SheetsService.Scope.Spreadsheets };
            UserCredential credential;
            var folder = System.Web.HttpContext.Current.Server.MapPath("/App_Data/MyGoogleStorage");

            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "117990626740-rptck4cro3bpbu3u7da3t4qlr20i3rsl.apps.googleusercontent.com",
                    ClientSecret = "zcFr6UCqdX-jo29QFogCcyf1"
                },
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(folder)).Result;


            return credential;
        }



    }
}