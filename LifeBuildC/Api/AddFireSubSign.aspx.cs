using ADO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class AddFireSubSign : System.Web.UI.Page
    {
        FireMemberADO firemem = new FireMemberADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 測試電文

                string strAddFireSubSign =
                @"
                {
                  ""gcroup"": ""社青"",
                  ""group"": ""CA202.信豪牧區-彥伯小組"",
                  ""Ename"": ""大丹"",
                  ""Gmail"": ""dennis866@gmail.com"",
                  ""Phone"": ""0919963322"",
                  ""gender"": ""男生"",
                  ""ClothesSize"": ""S"",
                  ""Course"": ""生命突破"",
                  ""FirePass"": ""A12345"",
                  ""IsMemData"": true,
                  ""Birthday"": ""1984/09/11""
                }
                ";

                #endregion

                using (Stream receiveStream = Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        strAddFireSubSign = readStream.ReadToEnd();
                    }
                }

                PageData PageData = JsonConvert.DeserializeObject<PageData>(strAddFireSubSign);

                try
                {
                    //SendGoogleExcel(PageData);

                    //小組
                    string[] arrg = PageData.group.Split('.');
                    string GroupCName = arrg[1].Split('-')[0];
                    string GroupName = arrg[1].Split('-')[1];

                    bool gender = true; //男生
                    if (PageData.gender == "女生")
                    {
                        gender = false;
                    }

                    bool Course = false; //生命突破
                    if (PageData.Course == "教會突破")
                    {
                        Course = true;
                    }

                    if (PageData.IsMemData)
                    {
                        firemem.UpdFireMember(PageData.Phone,
    PageData.Gmail, gender, PageData.ClothesSize, Course, PageData.Birthday, PageData.FirePass);
                    }
                    else
                    {
                        firemem.InsFireMember(GroupCName, GroupName, PageData.gcroup, PageData.Ename, PageData.Phone,
PageData.Gmail, gender, PageData.ClothesSize, Course);
                    }

                    PageData.ApiMsg = "烈火特會報名成功!";
                }
                catch (Exception ex)
                {
                    PageData.ApiMsg = "報名失敗，資料填寫可能有錯誤!";
                }

                Response.Write(JsonConvert.SerializeObject(PageData));
            }
        }

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
            String spreadsheetId = "106Y2tmI4RV3tJN_Ri4Xc91R3CZ1158GBJstlhfExjew";

            //String range = "工作表1!A:B";
            String range = "工作表1";

            var valueRange = new ValueRange();

            var oblist = new List<object>() {
                DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd HH:mm:ss"), //時間
                PageData.gcroup, //社青
                PageData.group, //CA202.信豪牧區-彥伯小組
                PageData.Ename, //流大丹
                PageData.Phone, //0919963322
                PageData.Gmail, //dennis866@gmail.com
                PageData.gender, //男生
                PageData.ClothesSize, //S
                PageData.Course //生命突破
            };

            valueRange.Values = new List<IList<object>> { oblist };
            valueRange.MajorDimension = "Rows"; //Rows or Columns

            SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            var appendReponse = request.Execute();
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
            //S
            public string ClothesSize { get; set; }
            //生命突破
            public string Course { get; set; }
            //api功能訊息
            public string ApiMsg { get; set; }

            //傳入的密碼
            public string FirePass { get; set; }
            //True: 有會友資料
            //False: 沒有會友資料
            public bool IsMemData { get; set; }
            //生日
            public string Birthday { get; set; }
        }

    }
}