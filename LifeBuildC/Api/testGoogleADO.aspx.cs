using ADO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class testGoogleADO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            test2();

        }

        protected void test1()
        {
            //static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
            string[] Scopes = { SheetsService.Scope.Spreadsheets };

            //應用程式的名字需要英文
            string ApplicationName = "Get Google SheetData with Google Sheets API";

            UserCredential credential;

            //d:\DZHosts\LocalUser\liutan\www.churchexam.somee.com\client_secret.json
            //using (var stream = new FileStream("C:\\client_secret.json", FileMode.Open, FileAccess.Read))
            using (var stream = new FileStream("d:\\DZHosts\\LocalUser\\liutan\\www.churchexam.somee.com\\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                //string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                //credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

                //credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                //    GoogleClientSecrets.Load(stream).Secrets,
                //    Scopes,
                //    "user",
                //    CancellationToken.None,
                //    new FileDataStore(credPath, true)).Result;

                //credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                //   GoogleClientSecrets.Load(stream).Secrets,
                //   Scopes,
                //   "user",
                //   CancellationToken.None).Result;

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore("Google.Apis.Auth")).Result;

            }

            //credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //   new ClientSecrets
            //   {
            //       //請依照申請的client Ikey填寫
            //       //ClientId = "117990626740-rptck4cro3bpbu3u7da3t4qlr20i3rsl.apps.googleusercontent.com",
            //       //ClientSecret = "zcFr6UCqdX-jo29QFogCcyf1",
            //       ClientId = "117990626740-et7rvnrhpk8vcu0ni00td1fgpdsl0o7i.apps.googleusercontent.com",
            //       ClientSecret = "CU0UTdsR7gT1Dv9Oxzqc9mf9",
            //   },
            //   Scopes,
            //   "user",
            //   CancellationToken.None).Result;

            // Create Google Sheets API service.

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

            // Define request parameters.
            String spreadsheetId = "1gH0Gn-Qlt-VTbfLnqMccLclUqx9kbGztLEANNaC6EpY";

            //String range = "工作表1!A:B";
            String range = "工作表1";

            var valueRange = new ValueRange();
            //科目, 小組, 姓名, 手機, 分數, 交卷時間
            var oblist = new List<object>() { "C212", "彥伯", "流大丹", "0919963322", "100", DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd HH:mm:ss") };
            valueRange.Values = new List<IList<object>> { oblist };
            valueRange.MajorDimension = "Rows"; //Rows or Columns

            SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            var appendReponse = request.Execute();
        }

        protected void test2()
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
            String spreadsheetId = "1gH0Gn-Qlt-VTbfLnqMccLclUqx9kbGztLEANNaC6EpY";

            //String range = "工作表1!A:B";
            String range = "工作表1";

            var valueRange = new ValueRange();
            //科目, 小組, 姓名, 手機, 分數, 交卷時間
            var oblist = new List<object>() { "C212", "彥伯", "流大丹", "0919963322", "100", DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd HH:mm:ss") };
            valueRange.Values = new List<IList<object>> { oblist };
            valueRange.MajorDimension = "Rows"; //Rows or Columns

            SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            var appendReponse = request.Execute();
        }

    }
}