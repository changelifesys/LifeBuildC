using ADO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class GetScore : System.Web.UI.Page
    {

        ExamQuestionsADO examq = new ExamQuestionsADO();
        UserScoreADO uscore = new UserScoreADO();
        //GoogleSheetsADO gsheets = new GoogleSheetsADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageStart();
        }

        private void PageStart()
        {
            string strAnswer =

"{" +
  "\"user\": [" +
    "{" +
      "\"group\": \"AA403.昭榮牧區-顯宗小組\"," +
      "\"name\": \"test\"," +
      "\"mobile\": \"test\"," +
      "\"eclass\": \"C1\"" +
    "}" +
  "]," +
  "\"exam\": [" +
    "{" +
      "\"id\": \"44\"," +
      "\"answer\": [" +
        "\"耶穌基督\"" +
      "]" +
    "}," +
    "{" +
      "\"id\": \"46\"," +
      "\"answer\": [" +
        "\"耶穌基督\"" +
      "]" +
    "}," +
    "{" +
      "\"id\": \"45\"," +
      "\"answer\": [" +
        "\"耶穌基督\"" +
      "]" +
    "}," +
    "{" +
      "\"id\": \"47\"," +
      "\"answer\": [" +
        "\"耶穌基督\"" +
      "]" +
    "}," +
    "{" +
      "\"id\": \"48\"," +
      "\"answer\": [" +
        "\"耶穌基督\"" +
      "]" +
    "}," +
    "{" +
      "\"id\": \"49\"," +
      "\"answer\": [" +
        "\"耶穌基督\"" +
      "]" +
    "}," +
    "{" +
      "\"id\": \"50\"," +
      "\"answer\": [" +
        "\"耶穌基督\"" +
      "]" +
    "}," +
    "{" +
      "\"id\": \"51\"," +
      "\"answer\": [" +
        "\"耶穌基督\"" +
      "]" +
    "}," +
    "{" +
      "\"id\": \"52\"," +
      "\"answer\": [" +
        "\"耶穌基督\"" +
      "]" +
    "}," +
    "{" +
      "\"id\": \"53\"," +
      "\"answer\": [" +
        "\"耶穌基督\"" +
      "]" +
    "}" +
  "]" +
"}";

            strAnswer = CheckStringByRequest("Answer");

            PageData PageData = new PageData();
            ScoreData ScoreData = new ScoreData();

            AnswerData AnswerData = JsonConvert.DeserializeObject<AnswerData>(strAnswer);

            int _Score = 0; //分數

            #region 分數計算

            foreach (var i in AnswerData.exam)
            {
                DataTable dt = examq.QueryExamQuestionsByID(i.id);

                int _cntAns = 0; //計算答案格
                bool _chkAns = false; //true:答對題目
                string _anshtml = "<p>"; //HTML答案
                string _AnsCopy = ""; //用於判斷一題當中是否有重覆的答案

                for (int f = 1; f <= 10; f++)
                {
                    #region DataTable Field

                    if (bool.Parse(dt.Rows[0]["IsField" + f].ToString()))
                    { //答案

                        _chkAns = false;
                        foreach (string s in i.answer)
                        { //檢查所有答案
                            #region answer

                            if (s == dt.Rows[0]["Field" + f].ToString() &&
                                _AnsCopy != dt.Rows[0]["Field" + f].ToString())
                            { //答對
                                _AnsCopy = dt.Rows[0]["Field" + f].ToString();
                                //_Score = _Score + 10; //答對+10分
                                _Score++;

                                //_anshtml += "<del class='text - danger'>";
                                //_anshtml += s; //寫的答案
                                //_anshtml += "</del>";

                                _chkAns = true;
                            }

                            #endregion

                        }

                        if (!_chkAns)
                        { //答錯
                            _anshtml += "<del class='text - danger'>";
                            _anshtml += i.answer[_cntAns]; //寫的答案
                            _anshtml += "</del>";
                            _cntAns++;
                        }

                        _anshtml += "<ins class='text - success'>";
                        _anshtml += dt.Rows[0]["Field" + f].ToString(); //正確答案
                        _anshtml += "</ins>";

                    }
                    else
                    { //題目
                        _anshtml += dt.Rows[0]["Field" + f].ToString();
                    }

                    #endregion
                }

                _anshtml += "</p>";
                ScoreData.answer.Add(HttpUtility.HtmlEncode(_anshtml));

            }

            #endregion

            if (_Score != 0)
            {
                //計算總題數
                int _FieldCnt = 0;
                foreach (var i in AnswerData.exam)
                {
                    foreach (string s in i.answer)
                    {
                        _FieldCnt++;
                    }
                }

                //答錯每格扣幾分
                //100分 - [(總題數 - 答對題數) * (每題幾分)]
                _Score = 100 - ((_FieldCnt - _Score) * (100 / _FieldCnt));
            }

            ScoreData.score = _Score;

            //分數小於70表示不通過
            if (_Score < 70)
                PageData.IsPass = false;

            PageData.DataInfo.Add(ScoreData);
            
            #region 分數登記

            //Access DataBase
            uscore.InsUserScore(AnswerData.user[0].eclass,
                                                   AnswerData.user[0].group,
                                                   AnswerData.user[0].name,
                                                   AnswerData.user[0].mobile,
                                                   _Score.ToString());

            //Google Excel
            //SendGoogleExcel(AnswerData, _Score.ToString());

            #endregion

            //查詢接下來可以考哪些科目(全部三科)
            DataTable dtScore = uscore.QueryUserScoreNoneExamCategory(AnswerData.user[0].group, AnswerData.user[0].name, DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd"));
            if (dtScore.Rows.Count > 0)
            {
                int _c1 = 0;
                int _c212 = 0;
                int _c234 = 0;
                foreach (DataRow drECD in dtScore.Rows)
                {
                    switch (drECD["ExamCategory"].ToString())
                    {
                        case "C1":
                            _c1++;
                            break;
                        case "C212":
                            _c212++;
                            break;
                        case "C234":
                            _c234++;
                            break;
                    }
                }

                if (_c1 == 0)
                    PageData.EClassData.Add("C1 測驗卷");
                if (_c212 == 0)
                    PageData.EClassData.Add("C2 一、二課測驗卷");
                if (_c234 == 0)
                    PageData.EClassData.Add("C2 三、四課測驗卷");

            }
            else
            {
                PageData.EClassData.Add("C1 測驗卷");
                PageData.EClassData.Add("C2 一、二課測驗卷");
                PageData.EClassData.Add("C2 三、四課測驗卷");
            }

            Response.Write(JsonConvert.SerializeObject(PageData));

        }

        /// <summary>
        /// 傳送考卷給google excel
        /// </summary>
        private void SendGoogleExcel(AnswerData AnswerData, string _Score)
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
            var oblist = new List<object>() {
                AnswerData.user[0].eclass,
                AnswerData.user[0].group,
                AnswerData.user[0].name,
                AnswerData.user[0].mobile,
                _Score,
                DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd HH:mm:ss") };

            valueRange.Values = new List<IList<object>> { oblist };
            valueRange.MajorDimension = "Rows"; //Rows or Columns

            SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            var appendReponse = request.Execute();
        }

        /// <summary>
        /// 檢查 Request.Form 傳入的值是否異常，若異常回傳空值。
        /// </summary>
        /// <param name="RequestForm_Name">Request.Form 的名稱</param>
        /// <returns>回傳 Request.Form 為 string 的值</returns>
        private string CheckStringByRequest(string RequestForm_Name)
        {
            string _value = string.Empty;
            if (Request.Form[RequestForm_Name] != null && Request.Form[RequestForm_Name].ToString().Length > 0)
                _value = Request.Form[RequestForm_Name].ToString();

            return _value;
        }

        public class AnswerData
        {
            public List<user> user = new List<user>();
            public List<exam> exam = new List<exam>();
        }

        public class user
        {
            public string eclass;
            public string group;
            public string name;
            public string mobile;
        }

        public class exam
        {
            public string id;
            public List<string> answer = new List<string>();
        }

        public class PageData
        {
            public bool IsError = false;
            public string ErrorMessage;
            public bool IsPass = true;
            public List<ScoreData> DataInfo = new List<ScoreData>();
            public List<string> EClassData = new List<string>();
        }

        public class ScoreData
        {
            public int score;
            public List<string> answer = new List<string>();
        }

    }
}