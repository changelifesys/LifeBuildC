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
    public partial class UpdSubSign : System.Web.UI.Page
    {
        //小組資訊
        ChcGroupADO ChcGroup = new ChcGroupADO();
        //會友資訊
        ChcMemberADO ChcMember = new ChcMemberADO();
        //報名資訊
        SubSignInfoADO SubSignInfo = new SubSignInfoADO();
        //上課日期
        SubjectDateADO SubjectDate = new SubjectDateADO();
        //會友簽到記錄
        SubSignUpDateADO SubSignUpDate = new SubSignUpDateADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageStart();
        }

        private void PageStart()
        {
            #region 測試電文

            string ReqUpdSubSign =
@"
{
  S_ID: ""2"",
  gcroup: ""社青"",
  group: ""彥伯小組"",
  name: ""流大丹""
}
";

            #endregion

            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    ReqUpdSubSign = readStream.ReadToEnd();
                }
            }

            PageData PageData = JsonConvert.DeserializeObject<PageData>(ReqUpdSubSign);

            #region 取得會友ID

            //取得小組GID
            int GID = ChcGroup.GetGIDByChcGroup(PageData.group, PageData.gcroup);
            //查詢會友資料
            DataTable dtMem = ChcMember.QueryByChcMember(PageData.name, GID);
            int MID = int.Parse(dtMem.Rows[0]["MID"].ToString());

            #endregion

            #region 課程報到

            //查詢報到課程
            DataTable dtSignInfo = SubSignInfo.QuerySubSignInfo(MID, PageData.S_ID);
            int SUID = int.Parse(dtSignInfo.Rows[0]["SUID"].ToString());

            //查詢當日課程
            DataTable dtCategoryID = SubjectDate.QueryCategoryIDBySubjectDate(PageData.S_ID, DateTime.Now.ToString("yyyy/MM/dd"));
            if (dtCategoryID.Rows.Count > 0)
            { //當日有上課
                PageData.CategoryID = dtCategoryID.Rows[0]["CategoryID"].ToString();

                //更新上課資訊
                SubSignUpDate.UpdDateBySubSignUpDate(SUID, PageData.CategoryID, DateTime.Now.ToString("yyyy/MM/dd"));

                PageData.Msg = "報到成功";
            }
            else
            { //沒有查詢到課程

                PageData.Msg = "報到失敗，系統沒有對應到該課程";

            }

            //傳送上課資訊給google excel
            //SendGoogleExcel(PageData);

            #endregion

        }

        /// <summary>
        /// 傳送上課資訊給google excel
        /// </summary>
        private void SendGoogleExcel(PageData PageData)
        {
            string[] Scopes = { SheetsService.Scope.Spreadsheets };

            //應用程式的名字需要英文
            string ApplicationName = "Get Google SheetData with Google Sheets API";

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

            // Create Google Sheets API service.

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

            // Define request parameters.
            String spreadsheetId = "11kkw-I0uhmaoam7HYAz1JLHmwgsuJMpo_vzcwbaZUIE";

            //String range = "工作表1!A:B";
            String range = "上課報到";

            var valueRange = new ValueRange();
            //報到課程, 報到日期, 小組, 姓名, 系統回應
            var oblist = new List<object>() {
                PageData.CategoryID,
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                PageData.group,
                PageData.name,
                PageData.Msg
            };

            valueRange.Values = new List<IList<object>> { oblist };
            valueRange.MajorDimension = "Rows"; //Rows or Columns

            SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            var appendReponse = request.Execute();
        }

    }

    public class PageData
    {
        public int S_ID { get; set; } //2
        public string gcroup { get; set; } //社青
        public string group { get; set; } //彥伯小組
        public string name { get; set; } //流大丹
        public string CategoryID { get; set; }
        public string Msg { get; set; } //訊息顯示
    }

}