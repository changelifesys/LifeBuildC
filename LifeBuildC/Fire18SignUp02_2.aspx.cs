using ADO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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
                    GroupClass = "兒童";

                    //小組
                    string[] arrg = dropGroupName.SelectedItem.Text.Split('.');
                    string GroupCName = arrg[1].Split('-')[0];
                    string GroupName = arrg[1].Split('-')[1];

                    //姓名
                    string Ename = txtEname1.Text.Trim() + "-" + txtEname2.Text.Trim();

                    string PassKey = Ename;
                    Session["PassKey"] = Ename;
                    string Birthday = txtBirthday.Text.Trim();

                    try
                    {
                        fireMem.InsFireMember(GroupCName, GroupName, GroupClass, Ename, "",
    "", true, "", true, PassKey, Birthday);

                        Response.Write("<script>location.href='Fire18SignUp03.aspx'</script>");
                    }
                    catch
                    {
                        Response.Write("<script>alert('已經報名過了，不得重覆報名');location.href='Fire18SignUp.aspx'</script>");
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

    }
}