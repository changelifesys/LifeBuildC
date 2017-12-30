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

namespace LifeBuildC.Admin.FireClass
{
    public partial class FireMem : System.Web.UI.Page
    {
        FireMemberADO firemem = new FireMemberADO();

        // Define request parameters.
        String spreadsheetId = "106Y2tmI4RV3tJN_Ri4Xc91R3CZ1158GBJstlhfExjew";
        String sheetName = "工作表1";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = firemem.QueryFireMember();
                gvExcel.DataSource = dt;
                gvExcel.DataBind();

                lblCount.Text = "查詢共 " + dt.Rows.Count.ToString() + " 筆資料";
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {

            #region google Sheets

            //先創建工作表, 若有存在就清空資料
            try
            {
                CreateV4Sheets();
            }
            catch
            {
                ClearV4Sheets();
            }

            #endregion

            #region Header

            PageData PageData = new PageData();
            PageData.Time = "報名時間";
            PageData.gcroup = "組別";
            PageData.group = "小組";
            PageData.Ename = "姓名";
            PageData.Phone = "電話";
            PageData.Gmail = "Email";
            PageData.gender = "姓別";
            PageData.Birthday = "生日";
            PageData.ClothesSize = "衣服尺寸";
            PageData.Course = "下午場講座";

            AddDataByV4Sheets(PageData);

            #endregion

            #region Data

            DataTable dt = firemem.QueryFireMember();
            foreach (DataRow dr in dt.Rows)
            {
                PageData.Time = dr["CreateTime"].ToString();
                PageData.gcroup = dr["GroupClass"].ToString();
                PageData.group = dr["group2"].ToString();
                PageData.Ename = dr["Ename"].ToString();
                PageData.Phone = dr["Phone2"].ToString();
                PageData.Gmail = dr["Gmail"].ToString();
                PageData.gender = dr["gender2"].ToString();
                PageData.Birthday = DateTime.Parse(dr["Birthday"].ToString()).ToString("yyyy/MM/dd");
                PageData.ClothesSize = dr["ClothesSize"].ToString();
                PageData.Course = dr["Course2"].ToString();
                AddDataByV4Sheets(PageData);
            }

            #endregion

            Response.Redirect("https://docs.google.com/spreadsheets/d/106Y2tmI4RV3tJN_Ri4Xc91R3CZ1158GBJstlhfExjew/edit#gid=548600737");

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

        /// <summary>
        /// 清空資料表內容
        /// </summary>
        private void ClearV4Sheets()
        {
            SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = GetCredential(),
                ApplicationName = "Get Google SheetData with Google Sheets API",
            });

            // TODO: Assign values to desired properties of `requestBody`:
            ClearValuesRequest requestBody = new ClearValuesRequest();

            SpreadsheetsResource.ValuesResource.ClearRequest request = sheetsService.Spreadsheets.Values.Clear(requestBody, spreadsheetId, sheetName);

            // To execute asynchronously in an async method, replace `request.Execute()` as shown:
            ClearValuesResponse response = request.Execute();
            // Data.ClearValuesResponse response = await request.ExecuteAsync();


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
                PageData.Time,
                PageData.gcroup,
                PageData.group,
                PageData.Ename,
                PageData.Phone,
                PageData.Gmail,
                PageData.gender,
                PageData.Birthday,
                PageData.ClothesSize,
                PageData.Course
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

        public class PageData
        {

            //社青
            public string gcroup { get; set; }
            //CA202.信豪牧區-彥伯小組
            public string group { get; set; }
            //流大丹
            public string Ename { get; set; }
            //0919963322
            public string Phone { get; set; }
            //dennis866@gmail.com
            public string Gmail { get; set; }
            //男生
            public string gender { get; set; }
            public string Birthday { get; set; }
            //S
            public string ClothesSize { get; set; }
            //生命突破
            public string Course { get; set; }
            public string Time { get; set; }
        }

        protected void gvExcel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //滑鼠移入移出效果
            if (e.Row.RowType == DataControlRowType.DataRow &&
                ((e.Row.RowState & DataControlRowState.Edit) <= 0))
            {
                ((LinkButton)e.Row.FindControl("btnEdit")).PostBackUrl = "FireMemEdit.aspx?p=" + DataBinder.Eval(e.Row.DataItem, "PassKey").ToString();
            }
        }

        /// <summary>
        /// 修改會友資料
        /// </summary>
        protected void btnEditData_Click(object sender, EventArgs e)
        {
            Response.Redirect("FireMemEdit.aspx?p=" + txtPassKey.Text.Trim().ToUpper());
        }

        protected void gvExcel_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string PassKey = ((LinkButton)gvExcel.Rows[e.RowIndex].FindControl("btnDel")).Attributes["Pass"].ToString();

            firemem.DelFireMember(PassKey);


            DataTable dt = firemem.QueryFireMember();
            gvExcel.DataSource = dt;
            gvExcel.DataBind();

            lblCount.Text = "查詢共 " + dt.Rows.Count.ToString() + " 筆資料";
        }
    }
}