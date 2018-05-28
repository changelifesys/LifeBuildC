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

        PageData pgdata = new PageData();
        string strAddSubSign = string.Empty;
        string GroupCName = string.Empty;
        string GroupName = string.Empty;
        string GroupClass = string.Empty;

        String sheetName = "";
        String spreadsheetId = "1HCRBI2C_cVl0fH5576PEX7UULWsgcxx1sbYdRQ6FcF8";

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

                        pgdata.SID = 1;
                        pgdata.CategoryID = "C1";
                        pgdata.gcroup = "社青";
                        pgdata.group = "CA202.信豪牧區-彥伯小組";
                        pgdata.Ename = "TEST";
                        pgdata.Phone = "0919963322";
                        pgdata.Gmail = "";
                        pgdata.Church = "";
                        break;
                    case "C2":

                        pgdata.SID = 2;
                        pgdata.CategoryID = "C2";
                        pgdata.gcroup = "社青";
                        pgdata.group = "CA202.信豪牧區-彥伯小組";
                        pgdata.Ename = "劉彥伯";
                        pgdata.Phone = "0919963322";
                        pgdata.Gmail = "";
                        pgdata.Church = "";
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

                pgdata = JsonConvert.DeserializeObject<PageData>(strAddSubSign);
            }

            if (pgdata != null)
            {
                try
                {
                    pgdata.IsApiError = false;

                    //小組
                    string[] arrg = pgdata.group.Split('.');
                    GroupCName = arrg[1].Split('-')[0];
                    GroupName = arrg[1].Split('-')[1];
                    GroupClass = pgdata.gcroup;

                    if (pgdata.CategoryID == "C1")
                    { //C1課程沒有限制報名資格

                        //sheetName = "[" + pgdata.SID + "]" + DateTime.Parse(dtSub.Rows[0]["SubEndDate"].ToString()).Year.ToString("0000") + DateTime.Parse(dtSub.Rows[0]["SubEndDate"].ToString()).Month.ToString("00");
                        sheetName = "C1報名";
                        InsChcMemberSub_Temp();
                        pgdata.ApiMsg = "C1 課程報名成功！";

                    }
                    else if (pgdata.CategoryID == "C2")
                    { //C2課程需上完C1才可報名

                        DataTable dtMem = chcmember.GetChcMemberByGroup(GroupCName, GroupName, pgdata.Ename);
                        if (dtMem != null && dtMem.Rows.Count > 0)
                        {
                            bool IsC112 = bool.Parse(dtMem.Rows[0]["IsC112"].ToString());
                            bool IsC134 = bool.Parse(dtMem.Rows[0]["IsC134"].ToString());
                            if (IsC112 && IsC134)
                            {
                                //sheetName = "[" + pgdata.SID + "]" + DateTime.Parse(dtSub.Rows[0]["SubEndDate"].ToString()).Year.ToString("0000") + DateTime.Parse(dtSub.Rows[0]["SubEndDate"].ToString()).Month.ToString("00");
                                sheetName = "C2報名";
                                InsChcMemberSub_Temp();
                                pgdata.ApiMsg = "C2 課程報名成功！";

                            }
                            else
                            {
                                pgdata.IsApiError = true;
                                pgdata.ApiMsg = "您沒有符合上C2的資格！請向小組長或區長確認，或是自行查詢是否完成C1課程";
                            }


                        }
                        else
                        {
                            pgdata.IsApiError = true;
                            pgdata.ApiMsg = "您沒有符合上C2的資格！請向小組長或區長確認，或是自行查詢是否完成C1課程";
                        }

                    }


                }
                catch (Exception e)
                {
                    pgdata.IsApiError = true;
                    pgdata.ApiMsg = "網路斷線或填寫的資料內容有誤！請重新填寫報名資料";
                }

            }

            Response.Write(JsonConvert.SerializeObject(pgdata));

        }

        private void InsChcMemberSub_Temp()
        {

            DataTable dtSub = subjectinfo.GetSubjectDateBySubjectInfo(pgdata.SID);

            foreach (DataRow drSub in dtSub.Rows)
            {
                chcmembersubtemp.InsChcMemberSub_Temp_2(pgdata.SID, drSub["CategoryID2"].ToString(), GroupCName, GroupName, GroupClass,
                     pgdata.Ename, pgdata.Phone, pgdata.Gmail, pgdata.Church, "0", DateTime.Parse(drSub["SDate"].ToString()), "");
                pgdata.SubDate += DateTime.Parse(drSub["SDate"].ToString()).Month.ToString("00") + "/" + DateTime.Parse(drSub["SDate"].ToString()).Day.ToString("00") + ",";
            }

            if (pgdata.SubDate != "" && pgdata.SubDate != null)
            {
                pgdata.SubDate = pgdata.SubDate.Substring(0, pgdata.SubDate.Length - 1);
            }

            switch (pgdata.gcroup)
            {
                case "家庭組弟兄":
                    pgdata.group_1 = pgdata.group;
                    pgdata.group_2 = "";
                    pgdata.group_3 = "";
                    pgdata.group_4 = "";
                    break;
                case "家庭組姊妹":
                    pgdata.group_1 = "";
                    pgdata.group_2 = pgdata.group;
                    pgdata.group_3 = "";
                    pgdata.group_4 = "";
                    break;
                case "社青":
                    pgdata.group_1 = "";
                    pgdata.group_2 = "";
                    pgdata.group_3 = pgdata.group;
                    pgdata.group_4 = "";
                    break;
                case "學生":
                    pgdata.group_1 = "";
                    pgdata.group_2 = "";
                    pgdata.group_3 = "";
                    pgdata.group_4 = pgdata.group;
                    break;
            }

            AddDataByV4Sheets(pgdata);
        }

        /// <summary>
        /// 新增一筆資料
        /// </summary>
        private void AddDataByV4Sheets(PageData PageData)
        {
            SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = GetCredential(),
                ApplicationName = "Get Google SheetData with Google Sheets API",
            });

            var valueRange = new ValueRange();

            var oblist = new List<object>() {
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    PageData.Ename,
                    PageData.group_1,
                    PageData.group_2,
                    PageData.group_3,
                    PageData.group_4,
                    PageData.SubDate
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

        /// <summary>
        /// API參數
        /// </summary>
        public class PageData
        {
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
            /// 手機(ChcMemberSub_Temp.Phone)
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// Mail
            /// </summary>
            public string Gmail { get; set; }
            /// <summary>
            /// 所屬教會(ChcMemberSub_Temp.Church)
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
        }

    }
}