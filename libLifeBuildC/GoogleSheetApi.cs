using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace libLifeBuildC
{
    public class GoogleSheetApi
    {
        /// <summary>
        /// google工作表ID
        /// </summary>
        private string spreadsheetId = string.Empty;
        /// <summary>
        /// 工作表名稱
        /// </summary>
        private string sheetName = string.Empty;

        public GoogleSheetApi(string _spreadsheetId, string _sheetName)
        {
            spreadsheetId = _spreadsheetId;
            sheetName = _sheetName;
        }

        #region 帳號：dennis866

        /// <summary>
        /// 新增一筆Row
        /// </summary>
        public void AddV4SheetsBydennis866(List<object> _oblist)
        {
            SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = GetCredentialBydennis866(),
                ApplicationName = "Get Google SheetData with Google Sheets API",
            });

            var valueRange = new ValueRange();

            var oblist = _oblist;

            valueRange.Values = new List<IList<object>> { oblist };

            valueRange.MajorDimension = "Rows"; //Rows or Columns

            SpreadsheetsResource.ValuesResource.AppendRequest request = sheetsService.Spreadsheets.Values.Append(valueRange, spreadsheetId, sheetName);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = request.Execute();

        }

        /// <summary>
        /// 取得google認證
        /// </summary>
        private UserCredential GetCredentialBydennis866()
        {
            string[] Scopes = {
                SheetsService.Scope.Spreadsheets
            };

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

        #endregion

        #region 帳號：changelifesys

        /// <summary>
        /// 新增一筆Row
        /// </summary>
        public void AddV4SheetsBychangelifesys(List<object> _oblist)
        {
            SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = GetCredentialBychangelifesys(),
                ApplicationName = "Get Google SheetData with Google Sheets API",
            });

            var valueRange = new ValueRange();

            var oblist = _oblist;

            valueRange.Values = new List<IList<object>> { oblist };

            valueRange.MajorDimension = "Rows"; //Rows or Columns

            SpreadsheetsResource.ValuesResource.AppendRequest request = sheetsService.Spreadsheets.Values.Append(valueRange, spreadsheetId, sheetName);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = request.Execute();

        }

        /// <summary>
        /// 取得google認證
        /// </summary>
        private UserCredential GetCredentialBychangelifesys()
        {
            string[] Scopes = {
                SheetsService.Scope.Spreadsheets
            };

            UserCredential credential;
            var folder = System.Web.HttpContext.Current.Server.MapPath("/App_Data/changelifesys");

            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "991601219925-cp46gu07hfh48fkcipm8g0f9kippfh1l.apps.googleusercontent.com",
                    ClientSecret = "xT2rOiW6kN6SjMJAOPPVl1et"
                },
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(folder)).Result;


            return credential;
        }

        #endregion


    }
}
