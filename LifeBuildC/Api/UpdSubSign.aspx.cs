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
        AdoInfo Ado_Info = new AdoInfo();
        ApiData Api_Info = new ApiData();
        ApiData.ApiUpdSubSign Api_Data = new ApiData.ApiUpdSubSign();
        ApiDataTest.ApiUpdSubSign Api_Test = new ApiDataTest.ApiUpdSubSign();

        String sheetName = "";
        String spreadsheetId = "";

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
                Request.QueryString["test"].ToString() == "true")
            {
            }
            else
            {
                using (Stream receiveStream = Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        Api_Info.strStreamReader = readStream.ReadToEnd();
                    }
                }

                Api_Data = JsonConvert.DeserializeObject<ApiData.ApiUpdSubSign>(Api_Info.strStreamReader);
            }


            if (Api_Data != null)
            {

                //小組
                Api_Info.GetGroupData(Api_Data.group, Api_Data.gcroup);


                switch ((Api_Data.CategoryID).ToUpper())
                {
                    case "C1":

                        sheetName = "C1報到";
                        spreadsheetId = "1HCRBI2C_cVl0fH5576PEX7UULWsgcxx1sbYdRQ6FcF8";
                        //上課簽到, 現場簽到, 新朋友簽到
                        ExecSubjectSingIn();

                        break;
                    case "C2":

                        if (Api_Data.MID != "" && Ado_Info.ChcMemberSub_Temp_ADO.CheckMID(int.Parse(Api_Data.MID), Api_Data.SID))
                        { //C2 不能現場報名

                            sheetName = "C2報到";
                            spreadsheetId = "1bKwnh_2XTYvR1bezOnzKEeA66Kyxlj0WAsN3LcL3FBs";
                            ExecSubjectSingIn();
                        }

                        break;
                }

            }

            Response.Write(JsonConvert.SerializeObject(Api_Data));
            Response.End();

        }

        private void ExecSubjectSingIn()
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


            if (Api_Data.MID.Split(',').Count() > 1 && Api_Data.MID.Split(',')[1] != "")
            { //UPDATE 報名資訊

                //api.MID.Split(',')[0] 為會友MID流水編號
                //陣列索引從 1 開始
                for (int i = 1; i < Api_Data.MID.Split(',').Count(); i++)
                {
                    //chcmembersubtemp.UpdChcMemberSub_TempByNo(GroupCName, GroupName, GroupClass, api.Ename, api.Phone,
                    //                                                                     api.Gmail, Memo, api.MID.Split(',')[i]);
                }
            }
            else
            { //INSERT 報名資訊


                //foreach (DataRow drSub in dtSub.Rows)
                //{
                //    chcmembersubtemp.InsChcMemberSub_Temp_2(
                //        api.SID, drSub["CategoryID2"].ToString(), GroupCName, GroupName, GroupClass,
                //        api.Ename, api.Phone, api.Gmail, api.Church, "0", DateTime.Parse(drSub["SDate"].ToString()), Memo, api.MID.Replace(",", ""), "0");

                //}

            }

            Ado_Info.ChcMemberSub_Temp_ADO.ExecSubjectSingIn(
                Api_Data.SID, Api_Data.CategoryID, Api_Info.GroupCName, Api_Info.GroupName,
                Api_Info.GroupClass, Api_Data.Ename, DateTime.Now.ToString("yyyy/MM/dd"), Memo);

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
                    Api_Data.Ename,
                    Api_Data.group_1,
                    Api_Data.group_2,
                    Api_Data.group_3,
                    Api_Data.group_4,
                    Api_Data.SubDate
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