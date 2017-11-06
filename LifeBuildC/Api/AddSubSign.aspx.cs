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

/// <summary>
/// <API>新增上課資訊
/// </summary>
namespace LifeBuildC.Api
{
    public partial class AddSubSign : System.Web.UI.Page
    {
        //小組資訊
        ChcGroupADO ChcGroup = new ChcGroupADO();
        //會友資訊
        ChcMemberADO ChcMember = new ChcMemberADO();
        //報名資訊
        SubSignInfoADO SubSignInfo = new SubSignInfoADO();
        //上課日期
        SubjectDateADO SubjectDate = new SubjectDateADO();
        //課程資訊
        SubjectInfoADO SubjectInfo = new SubjectInfoADO();
        //會友簽到記錄
        SubSignUpDateADO SubSignUpDate = new SubSignUpDateADO();
        //課程類別
        SubCategoryADO SubCategory = new SubCategoryADO();

        //foreach count
        int _Cnt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageStart();
        }

        private void PageStart()
        {
            #region 測試電文
            
            string strAddSubSign =
@"
{
  ""S_ID"": 7,
  ""Category"": ""C1"",
  ""gcroup"": ""社青"",
  ""group"": ""CA202.信豪牧區-彥伯小組"",
  ""name"": ""丹丹"",
  ""G_mail"": """",
  ""Church"": ""江子翠行道會會友"",
  ""Phone"": ""0919963322"",
  ""ChkStatus"": 0,
  ""MID"": 45
}
";

            #endregion

            //strAddSubSign = CheckStringByRequest("SubSign");
            //string[] _ReqKeys = Request.Form.AllKeys;

            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    strAddSubSign = readStream.ReadToEnd();
                }
            }

            PageData PageData = JsonConvert.DeserializeObject<PageData>(strAddSubSign);
            SignSuccessInfo SignSuccessInfo = new SignSuccessInfo();
            List<MakeUpInfo> listMakeUpInfo = new List<MakeUpInfo>();

            #region 檢查會友資訊

            //查詢會友資料
            DataTable dtMem = new DataTable();

            switch (PageData.ChkStatus)
            {
                case 0: //保持原有資料
                    dtMem = ChcMember.QueryMIDByChcMember(PageData.MID);
                    PageData.name = dtMem.Rows[0]["Ename"].ToString();
                    PageData.group = dtMem.Rows[0]["GroupName"].ToString();
                    PageData.Phone = dtMem.Rows[0]["Phone"].ToString();
                    break;
                case 1: //更新手機號碼
                    ChcMember.UpdPhoneByChcMember(PageData.name, PageData.group.Split('.')[1], PageData.Phone);
                    //dtMem = ChcMember.QueryNotPhoneByChcMember(PageData.group.Split('.')[1], PageData.name);
                    break;
                case 2: //更新小組資料
                    ChcMember.UpdGroupNameByChcMember(PageData.name, PageData.group.Split('.')[1], PageData.Phone);
                    //dtMem = ChcMember.QueryNotGroupNameByChcMember(PageData.Phone, PageData.name);
                    break;
                case 3: //更新會友姓名
                    ChcMember.UpdEnameByChcMember(PageData.name, PageData.group.Split('.')[1], PageData.Phone);
                    //dtMem = ChcMember.QueryNotEnameByChcMember(PageData.Phone, PageData.group.Split('.')[1]);
                    break;
                case 4: //新增會友資料
                    if (PageData.Category == "C1" || PageData.Category == "c1")
                    {
                        ChcMember.InsChcMember2(PageData.group.Split('.')[1], PageData.name, PageData.G_mail, PageData.Church, "初次上課", "", PageData.Phone);
                    }
                    else if (PageData.Category == "C2" || PageData.Category == "c2")
                    {
                        ChcMember.InsChcMember2(PageData.group.Split('.')[1], PageData.name, PageData.G_mail, PageData.Church, "", "初次上課", PageData.Phone);
                    }

                    dtMem = ChcMember.QueryNotPhoneByChcMember(PageData.group.Split('.')[1], PageData.name);
                    PageData.MID = int.Parse(dtMem.Rows[0]["MID"].ToString());
                    break;
            }

            /*
            //取得小組GID
            int GID = ChcGroup.GetGIDByChcGroup(PageData.group, PageData.gcroup);
            
            //查詢會友資料
            DataTable dtMem = ChcMember.QueryByChcMember(PageData.name, GID);
            if (dtMem.Rows.Count == 0)
            { //新增會友資訊
                //若需要新增會友資訊，表示一定是初次上課
                ChcMember.InsChcMember(GID, PageData.name, PageData.G_mail, PageData.Church, "初次上課", "初次上課", PageData.Phone);

                dtMem = null;
                dtMem = ChcMember.QueryByChcMember(PageData.name, GID);
            }
            else
            { //更新會友資訊
                ChcMember.UpdChcMember(PageData.Phone, PageData.G_mail, PageData.Church, int.Parse(dtMem.Rows[0]["MID"].ToString()));
            }
            */

            //int MID = int.Parse(dtMem.Rows[0]["MID"].ToString());
            int MID = PageData.MID;

            #endregion

            #region 檢查補課狀況

            //true表示第一次上課
            bool chkfirst = SubSignInfo.QueryFirstBySubSignInfo(MID, PageData.Category);

            //預設
            string C1_Status = "第一次上 C1 課程"; //C1修課狀態
            string C2_Status = "第一次上 C2 課程"; //C2修課狀態
            PageData.C_Pass = "未通過";

            if (!chkfirst)
            { //不是第一次上課，檢查補課狀況

                #region

                DataTable dtSubC = SubCategory.QueryCategoryID_DataBySubCategory(MID, PageData.Category);
                if (dtSubC != null && dtSubC.Rows.Count > 0)
                { //缺課

                    #region

                    //初始字串
                    C1_Status = "缺 ";
                    C2_Status = "缺 ";
                    PageData.C_Pass = "未通過";

                    foreach (DataRow dr in dtSubC.Rows)
                    {
                        switch (dr["CategoryID"].ToString().Substring(0, 2))
                        {
                            case "C1":
                                C1_Status += "(" + dr["CategoryName"].ToString() + ")";
                                break;
                            case "C2":
                                C2_Status += "(" + dr["CategoryName"].ToString() + ")";
                                break;
                        }

                        //存入補課資訊
                        MakeUpInfo MakeUpInfo = new MakeUpInfo();
                        MakeUpInfo.CategoryID = dr["CategoryID"].ToString();
                        MakeUpInfo.CategoryName = dr["CategoryName"].ToString();
                        listMakeUpInfo.Add(MakeUpInfo);

                    }

                    #endregion

                }
                else
                { //已通過，但想再上一次
                    C1_Status = "";
                    C2_Status = "";
                    PageData.C_Pass = "已通過";
                }

                #endregion

            }

            //更新會友補課狀況
            switch (PageData.Category)
            {
                case "C1":
                    PageData.C_Status = C1_Status;
                    ChcMember.UpdC1_StatusByChcMember(C1_Status, MID);
                    break;
                case "C2":
                    PageData.C_Status = C2_Status;
                    ChcMember.UpdC1_StatusByChcMember(C2_Status, MID);
                    break;
            }

            #endregion

            #region 新增上課資訊

            //新增上課資訊
            SubSignInfo.InsSubSignInfo(PageData.S_ID, MID, DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd HH:mm:ss"));
            int SUID = SubSignInfo.QuerySUIDBySubSignInfo(MID);

            //新增簽到表
            foreach (DataRow dr in SubjectDate.QueryCategoryIDBySubjectDate(MID).Rows)
            {
                SubSignUpDate.InsSubSignUpDate(SUID, dr["CategoryID"].ToString());
            }

            //傳送上課資訊給google excel
            //SendGoogleExcel(PageData);

            #endregion

            #region 課程報名成功(表單列印)

            //Guid g;
            //g = Guid.NewGuid();

            /*
              {
                    "IsMakeUp": "false",
                    "SBName": "C1課程報名成功",
                    "gcname": "彥伯小組-流大丹",
                    "SBInfo": [
                    {
                    "SBDate": "8/10(日) 下午14:30~17:30",
                    "SBClass": "第一、二課"
                    },
                    {
                    "SBDate": "8/23(日) 下午14:30~17:30",
                    "SBClass": "第三、四課"
                    }
                    ],
                    "cbook": "上課講義請到江子翠行道會改變書房購買"
              }
             */

            SignSuccessInfo.SBName = PageData.Category + "課程報名成功";
            SignSuccessInfo.gcname = PageData.group + "-" + PageData.name;


            DataTable dtSubInfo = new DataTable();

            dtSubInfo = SubjectInfo.QuerySIDBySubjectInfo(PageData.S_ID);

            foreach (DataRow dr in dtSubInfo.Rows)
            {
                SBInfo SBInfo = new SBInfo();
                SBInfo.SBDate = dr["SDate"].ToString().Replace(DateTime.UtcNow.AddHours(8).Year.ToString() + "/", "") + "(" + GetDayOfWeek(DateTime.Parse(dr["SDate"].ToString())) + ") " + dr["SubTime"].ToString();
                SBInfo.SBClass = dr["CategoryName"].ToString();
                #region SBInfo.status

                if (PageData.C_Pass == "已通過")
                {
                    SBInfo.status = "已通過";
                }
                else
                {
                    if (chkfirst)
                    { //第一次上課
                        SBInfo.status = "修課中";
                    }
                    else
                    { //顯示補課通知
                        _Cnt = 0;
                        for (int i = 0; i < listMakeUpInfo.Count(); i++)
                        {
                            if (dr["CategoryID"].ToString() == listMakeUpInfo[i].CategoryID)
                            {
                                SBInfo.status = "需補課";
                                _Cnt++;
                            }
                        }

                        if (_Cnt == 0)
                        {
                            SBInfo.status = "已通過";
                        }

                    }
                }

                #endregion
                SignSuccessInfo.SBInfo.Add(SBInfo);
            }

            SignSuccessInfo.cbook = "上課講義請到江子翠行道會改變書房購買";

            Response.Write(JsonConvert.SerializeObject(SignSuccessInfo));

            #endregion

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
            String range = "工作表1";

            var valueRange = new ValueRange();
            //報名課程, 報名日期, 修課狀況, 小組,	姓名,	所屬教會, 補課狀況
            var oblist = new List<object>() {
                PageData.Category,
                DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd HH:mm:ss"),
                PageData.C_Pass,
                PageData.group,
                PageData.name,
                PageData.Church,
                PageData.C_Status
            };

            valueRange.Values = new List<IList<object>> { oblist };
            valueRange.MajorDimension = "Rows"; //Rows or Columns

            SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            var appendReponse = request.Execute();
        }

        private string GetDayOfWeek(DateTime dtime)
        {
            switch (dtime.DayOfWeek.ToString("d"))
            {
                case "0":
                    return "日";
                case "1":
                    return "一";
                case "2":
                    return "二";
                case "3":
                    return "三";
                case "4":
                    return "四";
                case "5":
                    return "五";
                case "6":
                    return "六";
                default:
                    return "";
            }
        }

        public class PageData
        {
            public int S_ID; //2
            public string Category; //C1
            public string gcroup; //
            public string group;
            public string name;
            public string G_mail;
            public string Church;
            public string Phone;
            public string C_Status; //補課狀況
            public string C_Pass; //修課狀況

            //ChkStatus=0 保持原有資料
            //ChkStatus=1 更新手機號碼
            //ChkStatus=2 更新小組資料
            //ChkStatus=3 更新會友姓名
            //ChkStatus=4 新增會友資料
            public int ChkStatus;
            public int MID;
        }

        public class SignSuccessInfo
        {
            public string SBName; //C1課程報名成功
            public string gcname; //彥伯小組-流大丹
            public List<SBInfo> SBInfo = new List<SBInfo>();
            public string cbook;
        }

        public class SBInfo
        {
            public string SBDate; //8/10(日) 下午14:30~17:30
            public string SBClass; //第一、二課
            public string status;
        }

        /// <summary>
        /// 補課資訊
        /// </summary>
        public class MakeUpInfo
        {
            public string CategoryID;
            public string CategoryName;
        }

    }
}