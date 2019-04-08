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
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC
{
    public partial class Fire18SignUp02_2 : System.Web.UI.Page
    {

        ChcGroupADO group = new ChcGroupADO();
        FirePassWADO firePass = new FirePassWADO();
        FireMemberADO fireMem = new FireMemberADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["gc"] != null && Request.QueryString["gc"].ToString() != "" &&
                   Request.Form["txtPassKey"] != null && Request.Form["txtPassKey"].ToString() != "" &&
                   firePass.CheckPassKey(Request.Form["txtPassKey"].ToString(), Request.QueryString["gc"].ToString()))
                {
                    hidPassKey.Value = Request.Form["txtPassKey"].ToString();

                    string GroupClass = Request.QueryString["gc"].ToString();
                    switch (GroupClass)
                    {
                        case "L": //成人報名
                            lblTitle.Text = "2020 烈火特會成人報名";
                            break;
                        case "S": //學青報名
                            lblTitle.Text = "2020 烈火特會學青報名";
                            break;
                        case "C": //12歲以下報名(含12歲)
                            lblTitle.Text = "2020 烈火特會兒童報名";
                            break;
                    }

                    DataTable dt = fireMem.GetFireMemberWherePassKey(Request.Form["txtPassKey"].ToString());

                    if (dt != null && dt.Rows.Count > 0)
                    { //修改表單

                        btnSend.Visible = false;
                        btnSave.Visible = true;

                        dropGroupClass.Enabled = false;
                        dropGroupName.Enabled = false;
                        txtEname1.Enabled = false;
                        txtEname2.Enabled = false;

                        ListItem li = new ListItem();

                        dropGroupClass.Items.Clear();

                        li = null;
                        li = new ListItem();
                        li.Text = dt.Rows[0]["GroupClass"].ToString();
                        li.Value = "0";
                        dropGroupClass.Items.Add(li);
                        dropGroupClass.SelectedIndex = 0;


                        //出輸格式
                        //AA101.永健牧區-永健小組
                        dropGroupName.Items.Clear();

                        li = null;
                        li = new ListItem();
                        li.Text = dt.Rows[0]["group2"].ToString();
                        li.Value = "0";
                        dropGroupName.Items.Add(li);
                        dropGroupName.SelectedIndex = 0;

                        txtEname1.Text = dt.Rows[0]["Ename"].ToString().Split('-')[0];
                        txtEname2.Text = dt.Rows[0]["Ename"].ToString().Split('-')[1];

                        txtBirthday.Text = DateTime.Parse(dt.Rows[0]["Birthday"].ToString()).ToString("yyyy/MM/dd");

                        #region 大會T恤尺寸

                        dropClothesSize.Items.Clear();

                        li = null;
                        li = new ListItem();
                        li.Text = "S";
                        li.Value = "0";
                        dropClothesSize.Items.Add(li);

                        li = null;
                        li = new ListItem();
                        li.Text = "M";
                        li.Value = "1";
                        dropClothesSize.Items.Add(li);

                        li = null;
                        li = new ListItem();
                        li.Text = "L";
                        li.Value = "2";
                        dropClothesSize.Items.Add(li);

                        li = null;
                        li = new ListItem();
                        li.Text = "XL";
                        li.Value = "3";
                        dropClothesSize.Items.Add(li);

                        li = null;
                        li = new ListItem();
                        li.Text = "XXL";
                        li.Value = "4";
                        dropClothesSize.Items.Add(li);

                        switch (dt.Rows[0]["ClothesSize"].ToString())
                        {
                            case "S":
                                dropClothesSize.SelectedIndex = 0;
                                break;
                            case "M":
                                dropClothesSize.SelectedIndex = 1;
                                break;
                            case "L":
                                dropClothesSize.SelectedIndex = 2;
                                break;
                            case "XL":
                                dropClothesSize.SelectedIndex = 3;
                                break;
                            case "XXL":
                                dropClothesSize.SelectedIndex = 4;
                                break;
                        }

                        //dropClothesSize.SelectedItem.Text = dt.Rows[0]["ClothesSize"].ToString();

                        #endregion

                        #region 下午場講座

                        /*
                        dropCourse.Items.Clear();

                        li = null;
                        li = new ListItem();
                        li.Text = "生命突破";
                        li.Value = "0";
                        dropCourse.Items.Add(li);

                        li = null;
                        li = new ListItem();
                        li.Text = "教會突破";
                        li.Value = "1";
                        dropCourse.Items.Add(li);

                        if (bool.Parse(dt.Rows[0]["Course"].ToString()))
                        {
                            //dropCourse.SelectedItem.Text = "教會突破";
                            dropCourse.SelectedIndex = 1;
                        }
                        else
                        {
                            //dropCourse.SelectedItem.Text = "生命突破";
                            dropCourse.SelectedIndex = 0;
                        }
                        */

                        #endregion

                    }
                    else
                    { //新表單
                        btnSend.Visible = true;
                        btnSave.Visible = false;
                    }


                }
                else
                {
                    if (Request.QueryString["gc"] != null && Request.QueryString["gc"].ToString() != "")
                    {
                        Response.Write("<script>alert('密碼錯誤');location.href='Fire18SignUp.aspx?gc=" + Request.QueryString["gc"].ToString() + "';</script>");
                    }
                    else
                    {
                        Response.Redirect("Fire18SignUp.aspx");
                    }
                }


            }
        }

        protected void dropGroupClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = group.QueryGroupNameByChcGroup(dropGroupClass.SelectedItem.Text);

            dropGroupName.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListItem li = new ListItem();
                //出輸格式
                //AA101.永健牧區-永健小組
                li.Text = dr["GroupID"].ToString() + "." + dr["GroupCName"].ToString() + "-" + dr["GroupName"].ToString();
                li.Value = dr["GSort"].ToString();
                dropGroupName.Items.Add(li);
            }


        }


        /// <summary>
        /// 報名
        /// </summary>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsCheck = true;
                string ErrMsg = string.Empty;
                ChkPageData(ref IsCheck, ref ErrMsg);

                if (IsCheck)
                {

                    //組別
                    string GroupClass = dropGroupClass.SelectedItem.Text;

                    //小組
                    string[] arrg = dropGroupName.SelectedItem.Text.Split('.');
                    string GroupCName = arrg[1].Split('-')[0];
                    string GroupName = arrg[1].Split('-')[1];

                    //姓名
                    string Ename = txtEname1.Text.Trim().Replace("-", "") + "-" + txtEname2.Text.Trim().Replace("-", "");

                    //string PassKey = Ename;
                    string Birthday = txtBirthday.Text.Trim();

                    //大會T恤尺寸
                    string ClothesSize = dropClothesSize.SelectedItem.Text;

                    try
                    {
                        fireMem.InsFireMember(GroupCName, GroupName, GroupClass, Ename, "",
    "", true, ClothesSize, true, hidPassKey.Value, Birthday);

                        SendGoogleExcel();
                        SendGoogleExcelByClass();

                        Response.Write("<script>location.href='Fire18SignUp03.aspx?pk=" + hidPassKey.Value + "&gc=" + Request.QueryString["gc"].ToString() + "'</script>");

                    }
                    catch
                    {
                        Response.Write("<script>alert('已經報名過了，不得重覆報名');location.href='Fire18SignUp.aspx?gc=" + Request.QueryString["gc"].ToString() + "';</script>");
                    }

                }
                else
                {
                    Response.Write("<script>alert('" + ErrMsg + "');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('無法報名，請洽櫃檯重新領取一組密碼輸入');</script>");
                Response.Redirect("Fire18SignUp.aspx");
            }


        }

        /// <summary>
        /// 儲存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsCheck = true;
                string ErrMsg = string.Empty;
                ChkPageData(ref IsCheck, ref ErrMsg);

                if (IsCheck)
                {

                    //組別
                    string GroupClass = dropGroupClass.SelectedItem.Text;

                    //小組
                    string[] arrg = dropGroupName.SelectedItem.Text.Split('.');
                    string GroupCName = arrg[1].Split('-')[0];
                    string GroupName = arrg[1].Split('-')[1];

                    //姓名
                    string Ename = txtEname1.Text.Trim().Replace("-", "") + "-" + txtEname2.Text.Trim().Replace("-", "");

                    //大會T恤尺寸
                    string ClothesSize = dropClothesSize.SelectedItem.Text;

                    bool Course = false; //生命突破
                    /*
                    if (dropCourse.SelectedItem.Text == "教會突破")
                    {
                        Course = true;
                    }
                    */

                    //string PassKey = Request.Form["txtPassKey"].ToString();
                    string Birthday = txtBirthday.Text.Trim();

                    fireMem.UpdFireMember("",
                    "", true, ClothesSize, Course, Birthday, hidPassKey.Value);

                    Response.Write("<script>location.href='Fire18SignUp03.aspx?pk=" + hidPassKey.Value + "&gc=" + Request.QueryString["gc"].ToString() + "';</script>");

                }
                else
                {
                    Response.Write("<script>alert('" + ErrMsg + "');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('資料錯誤導致報名失敗，請洽櫃檯咨詢');</script>");
                Response.Redirect("Fire18SignUp.aspx");
            }
        }

        /// <summary>
        /// 資料檢查
        /// </summary>
        private void ChkPageData(ref bool IsCheck, ref string ErrMsg)
        {

            if (!IsDate(txtBirthday.Text.Trim().Replace("/", "-")))
            {
                IsCheck = false;
                ErrMsg = "生日格式輸入不正確";
                return;
            }

            if (dropGroupClass.SelectedValue == "")
            {
                IsCheck = false;
                ErrMsg = "請選擇組別";
                return;
            }

            if (dropGroupName.SelectedValue == "")
            {
                IsCheck = false;
                ErrMsg = "請選擇小組";
                return;
            }

            if (txtEname1.Text.Trim() == "")
            {
                IsCheck = false;
                ErrMsg = "父母姓名(有報名的)";
                return;
            }

            if (txtEname2.Text.Trim() == "")
            {
                IsCheck = false;
                ErrMsg = "小孩姓名";
                return;
            }

            if (dropClothesSize.SelectedValue == "")
            {
                IsCheck = false;
                ErrMsg = "請選擇大會T恤尺寸";
                return;
            }

        }


        /// <summary>
        /// 驗證手機號碼
        /// </summary>
        /// <param name="input">要驗證的字串</param>
        /// <returns>驗證通過返回true</returns>
        public static bool IsMobile(string input)
        {
            return Regex.IsMatch(input, RegularExp.Mobile);
        }

        public static bool IsEmail(string input)
        {
            return Regex.IsMatch(input, RegularExp.Email);
        }

        public static bool IsDate(string input)
        {
            return Regex.IsMatch(input, RegularExp.Birthday);
        }

        public struct RegularExp
        {
            public const string Chinese = @"^[\u4E00-\u9FA5\uF900-\uFA2D]+$";
            public const string Color = "^#[a-fA-F0-9]{6}";
            public const string Date = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";
            public const string DateTime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$";
            public const string Email = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            public const string Float = @"^(-?\d+)(\.\d+)?$";
            public const string ImageFormat = @"\.(?i:jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$";
            public const string Integer = @"^-?\d+$";
            public const string IP = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
            public const string Letter = "^[A-Za-z]+$";
            public const string LowerLetter = "^[a-z]+$";
            public const string MinusFloat = @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$";
            public const string MinusInteger = "^-[0-9]*[1-9][0-9]*$";
            public const string Mobile = @"^09\d{2}-?\d{3}-?\d{3}$";
            public const string NumbericOrLetterOrChinese = @"^[A-Za-z0-9\u4E00-\u9FA5\uF900-\uFA2D]+$";
            public const string Numeric = "^[0-9]+$";
            public const string NumericOrLetter = "^[A-Za-z0-9]+$";
            public const string NumericOrLetterOrUnderline = @"^\w+$";
            public const string PlusFloat = @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
            public const string PlusInteger = "^[0-9]*[1-9][0-9]*$";
            public const string Telephone = @"(\d+-)?(\d{4}-?\d{7}|\d{3}-?\d{8}|^\d{7,8})(-\d+)?";
            public const string UnMinusFloat = @"^\d+(\.\d+)?$";
            public const string UnMinusInteger = @"\d+$";
            public const string UnPlusFloat = @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$";
            public const string UnPlusInteger = @"^((-\d+)|(0+))$";
            public const string UpperLetter = "^[A-Z]+$";
            public const string Url = @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
            public const string Birthday = @"^(19|20)\d{2}-(1[0-2]|0?[1-9])-(0?[1-9]|[1-2][0-9]|3[0-1])$";
        }

        //大會用
        private void SendGoogleExcel()
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
                "兒童", //社青
                dropGroupName.SelectedItem.Text, //CA202.信豪牧區-彥伯小組
                txtEname2.Text.Trim(), //流大丹
                "", //手機
                "", //gmail
                "", //男生
                "", //生日
                "", //S
                "" //生命突破
            };

            valueRange.Values = new List<IList<object>> { oblist };
            valueRange.MajorDimension = "Rows"; //Rows or Columns

            SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            var appendReponse = request.Execute();
        }

        //小組長用
        private void SendGoogleExcelByClass()
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
            String spreadsheetId = "1IzuEudxNvaO44mTZgXbvcxAfQ21FfxnPeW12PB3jW2M";

            //String range = "工作表1!A:B";
            String range = "工作表1";

            var valueRange = new ValueRange();

            var oblist = new List<object>() {
                DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd HH:mm:ss"), //時間
                "兒童", //社青
                dropGroupName.SelectedItem.Text, //CA202.信豪牧區-彥伯小組
                txtEname2.Text.Trim(), //流大丹
                "", //男生
                "", //S
                "" //生命突破
            };

            valueRange.Values = new List<IList<object>> { oblist };
            valueRange.MajorDimension = "Rows"; //Rows or Columns

            SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            var appendReponse = request.Execute();
        }

    }
}