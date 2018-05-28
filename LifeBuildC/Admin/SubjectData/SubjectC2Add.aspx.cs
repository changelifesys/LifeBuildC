using ADO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin.SubjectData
{
    public partial class SubjectC2Add : System.Web.UI.Page
    {
        SubjectInfoADO SubjectInfo = new SubjectInfoADO();
        SubjectDateADO SubjectDate = new SubjectDateADO();

        // Define request parameters.
        String spreadsheetId = "1HCRBI2C_cVl0fH5576PEX7UULWsgcxx1sbYdRQ6FcF8";
        String sheetName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //報名條件
                string y = DateTime.UtcNow.AddHours(8).Year.ToString();
                txtSUCondition.Text = y + "年1月~" + y + "年12月來的新朋友，或是還沒上過的會友。";

                //上課時間
                //C2 一、二課
                txtSDate12.Text = DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd");
                dropSubTime12.SelectedIndex = 1;
                txtSubTime12.Text = "14:30~17:30";

                //C2 三、四課
                txtSDate34.Text = DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd");
                dropSubTime34.SelectedIndex = 1;
                txtSubTime34.Text = "14:30~17:30";

                //C2 五課
                txtSDate5.Text = DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd");
                dropSubTime5.SelectedIndex = 1;
                txtSubTime5.Text = "14:30~17:30";

                //地點
                txtSubLocation.Text = "江子翠行道會主會堂";

                //報名日期
                txtSubStrDate.Text = DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd");

                //報名截止
                txtSubEndDate.Text = DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd");

            }
        }

        /// <summary>
        /// 新增課程
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string SubName = "生命建造程序 C2 課程報名";
            string SUCondition = txtSUCondition.Text.Trim();
            string SubLocation = txtSubLocation.Text.Trim();
            string SubStrDate = txtSubStrDate.Text.Trim();
            string SubEndDate = txtSubEndDate.Text.Trim();

            SubjectInfo.InsSubjectInfo("C2", SubName, SUCondition, SubLocation,
                                                              SubStrDate, SubEndDate);

            DataTable dtSubjectInfo = SubjectInfo.QueryMaxSIDBySubjectInfo("C2");
            int SID = int.Parse(dtSubjectInfo.Rows[0]["SID"].ToString());

            //C2 一、二課
            if (txtSDate12.Text.Trim() != "")
            {
                SubjectDate.InsSubjectDate(SID, "C212", txtSDate12.Text.Trim(),
                                                                     dropSubTime12.Text + " " + txtSubTime12.Text.Trim());
            }

            //C2 三、四課
            if (txtSDate34.Text.Trim() != "")
            {
                SubjectDate.InsSubjectDate(SID, "C234", txtSDate34.Text.Trim(),
                                                                      dropSubTime34.Text + " " + txtSubTime34.Text.Trim());
            }

            //C2 五課
            if (txtSDate5.Text.Trim() != "")
            {
                SubjectDate.InsSubjectDate(SID, "C25", txtSDate5.Text.Trim(),
                                                                     dropSubTime5.Text + " " + txtSubTime5.Text.Trim());
            }

            sheetName = "C2報名";

            //先創建工作表, 若有存在就清空資料
            try
            {
                CreateV4Sheets();

                #region Header

                PageData PageData = new PageData();
                PageData.CreateTime = "時間戳記";
                PageData.Ename = "姓名";
                PageData.group_1 = "所屬牧區/小組：家庭弟兄";
                PageData.group_2 = "所屬牧區/小組：家庭姊妹";
                PageData.group_3 = "所屬牧區/小組：社青";
                PageData.group_4 = "所屬牧區/小組：學青";
                PageData.SubDate = "上課時段";

                AddDataByV4Sheets(PageData);

                #endregion

                Response.Write("<script>alert('C2課程新增成功!');location.href='SubjectList.aspx';</script>");
            }
            catch
            {

            }

            

        }


        /// <summary>
        /// 創建一張工作表
        /// </summary>
        private void CreateV4Sheets()
        {
            SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = GetCredential(),
                ApplicationName = "Get Google SheetData with Google Sheets API",
            });

            // Add new Sheet
            var addSheetRequest = new AddSheetRequest();
            addSheetRequest.Properties = new SheetProperties();
            addSheetRequest.Properties.Title = sheetName;
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            batchUpdateSpreadsheetRequest.Requests = new List<Request>();
            batchUpdateSpreadsheetRequest.Requests.Add(new Request
            {
                AddSheet = addSheetRequest
            });

            var batchUpdateRequest = sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, spreadsheetId);

            batchUpdateRequest.Execute();

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
                    PageData.CreateTime,
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

        public class PageData
        {
            /// <summary>
            /// 時間戳記
            /// </summary>
            public string CreateTime { get; set; }
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
            /// 上課時段
            /// </summary>
            public string SubDate { get; set; }
        }

        protected void btnCel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubjectList.aspx");
        }

    }
}