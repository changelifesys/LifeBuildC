using ADO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC
{
    public partial class Fire18SignUp02_1 : System.Web.UI.Page
    {
        ChcGroupADO group = new ChcGroupADO();
        FirePassWADO firePass = new FirePassWADO();
        FireMemberADO fireMem = new FireMemberADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassKey"] != null && firePass.CheckPassKey(Session["PassKey"].ToString()))
                {
                    DataTable dt = fireMem.GetFireMemberWherePassKey(Session["PassKey"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    { //修改表單
                        btnSend.Visible = false;
                        btnSave.Visible = true;
                        btnSaveMail.Visible = true;

                        dropGroupClass.Enabled = false;
                        dropGroupName.Enabled = false;
                        txtEname.Enabled = false;

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

                        txtEname.Text = dt.Rows[0]["Ename"].ToString();
                        txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                        txtGmail.Text = dt.Rows[0]["Gmail"].ToString();

                        if (bool.Parse(dt.Rows[0]["gender"].ToString()))
                        {
                            rdogender1.Checked = true;
                        }
                        else
                        {
                            rdogender0.Checked = false;
                        }

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

                        #endregion

                    }
                    else
                    { //新表單
                        btnSend.Visible = true;
                        btnSave.Visible = false;
                        btnSaveMail.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("Fire18SignUp.aspx");
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
                    string Ename = txtEname.Text.Trim();

                    //手機
                    string Phone = txtPhone.Text.Trim();

                    //Email
                    string Email = txtGmail.Text.Trim();

                    bool gender = true; //男生
                    if (rdogender0.Checked)
                    {
                        gender = false;
                    }

                    //大會T恤尺寸
                    string ClothesSize = dropClothesSize.SelectedItem.Text;

                    bool Course = false; //生命突破
                    if (dropCourse.SelectedItem.Text == "教會突破")
                    {
                        Course = true;
                    }

                    string PassKey = Session["PassKey"].ToString();
                    string Birthday = txtBirthday.Text.Trim();

                    fireMem.InsFireMember(GroupCName, GroupName, GroupClass, Ename, Phone,
                        Email, gender, ClothesSize, Course, PassKey, Birthday);

                    try
                    {
                        if (Email != "")
                        {
                            SendEmail(Email);
                        }

                        //Response.Redirect("Fire18SignUp03.aspx");
                        //Server.Transfer("Fire18SignUp03.aspx");
                        Response.Write("<script>location.href='Fire18SignUp03.aspx'</script>");

                    }
                    catch {
                        Response.Write("<script>alert('您可能會在Mail收不到報名資訊，請重掃QRCode輸入密碼後檢查您的Mail是否輸入正確');location.href='Fire18SignUp03.aspx'</script>");
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
            PageDataSave(false);
        }

        /// <summary>
        /// 儲存後重寄Mail
        /// </summary>
        protected void btnSaveMail_Click(object sender, EventArgs e)
        {
            if (txtGmail.Text.Trim() == "")
            {
                Response.Write("<script>alert('請輸入Email');</script>");
            }
            else
            {
                PageDataSave(true);
            }
        }

        private void PageDataSave(bool IsSendMail)
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
                    string Ename = txtEname.Text.Trim();

                    //手機
                    string Phone = txtPhone.Text.Trim();

                    //Email
                    string Email = txtGmail.Text.Trim();

                    bool gender = true; //男生
                    if (rdogender0.Checked)
                    {
                        gender = false;
                    }

                    //大會T恤尺寸
                    string ClothesSize = dropClothesSize.SelectedItem.Text;

                    bool Course = false; //生命突破
                    if (dropCourse.SelectedItem.Text == "教會突破")
                    {
                        Course = true;
                    }

                    string PassKey = Session["PassKey"].ToString();
                    string Birthday = txtBirthday.Text.Trim();

                    fireMem.UpdFireMember(Phone,
                    Email, gender, ClothesSize, Course, Birthday, PassKey);

                    if (IsSendMail)
                    {
                        try
                        {
                            SendEmail(Email);

                            //Response.Redirect("Fire18SignUp03.aspx");
                            //Server.Transfer("Fire18SignUp03.aspx");
                            Response.Write("<script>location.href='Fire18SignUp03.aspx'</script>");
                        }
                        catch
                        {
                            Response.Write("<script>alert('您可能會在Mail收不到報名資訊，請重掃QRCode輸入密碼後檢查您的Mail是否輸入正確');location.href='Fire18SignUp03.aspx'</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>location.href='Fire18SignUp03.aspx'</script>");
                    }

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
            if (!IsMobile(txtPhone.Text.Trim()))
            {
                IsCheck = false;
                ErrMsg = "手機格式輸入不正確";
                return;
            }

            if (txtGmail.Text.Trim() != "" && !IsEmail(txtGmail.Text.Trim()))
            {
                IsCheck = false;
                ErrMsg = "Email格式輸入不正確";
                return;
            }

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

            if (txtEname.Text.Trim() == "")
            {
                IsCheck = false;
                ErrMsg = "請輸入姓名";
                return;
            }

            if (txtPhone.Text.Trim() == "")
            {
                IsCheck = false;
                ErrMsg = "請輸入手機";
                return;
            }

            //if (txtGmail.Text.Trim() == "")
            //{
            //    IsCheck = false;
            //    ErrMsg = "請輸入Email";
            //    return;
            //}

            if (rdogender1.Checked == false && rdogender0.Checked == false)
            {
                IsCheck = false;
                ErrMsg = "請選取姓別";
                return;
            }

            if (txtBirthday.Text.Trim() == "")
            {
                IsCheck = false;
                ErrMsg = "請輸入生日";
                return;
            }

            if (dropClothesSize.SelectedValue == "")
            {
                IsCheck = false;
                ErrMsg = "請選擇大會T恤尺寸";
                return;
            }

            if (dropCourse.SelectedValue == "")
            {
                IsCheck = false;
                ErrMsg = "請選擇下午場講座";
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

        public void SendEmail(string emailTo)
        {
            //設定smtp主機
            string smtpAddress = "smtp.gmail.com";
            //設定Port
            int portNumber = 587;
            bool enableSSL = true;
            //填入寄送方email和密碼
            string emailFrom = "changelifesys@gmail.com";
            string password = "@Chc1225";
            //收信方email
            //string emailTo = "dennis866@gmail.com";
            //主旨
            string subject = "2018 烈火特會報名成功";
            //內容
            string body = @"恭喜您完成烈火特會報名。<p/>
                                           特會流程及資訊請參見附檔。<p/>
                                           行前通知將於開始前一週發送。<p/>
                                           如有問題請與<font style='color: blue;'>信豪傳道聯繫</font><p/>
                                           <font style='color: blue;'>hsinhao.huang@twclc.org</font><p/><p/>
                                           <font style='color: red;'>該mail為CLC資訊系統所發，請勿直接回覆</font>";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                // 若你的內容是HTML格式，則為True
                mail.IsBodyHtml = true;

                //夾帶檔案
                //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));
                mail.Attachments.Add(new Attachment(System.Web.HttpContext.Current.Server.MapPath("/img/fire02.jpg")));
                mail.Attachments.Add(new Attachment(System.Web.HttpContext.Current.Server.MapPath("/img/fire01.png")));

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }


    }
}