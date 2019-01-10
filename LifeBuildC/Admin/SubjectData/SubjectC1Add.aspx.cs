using ADO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using libLifeBuildC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin.SubjectData
{
    public partial class SubjectC1Add : System.Web.UI.Page
    {
        ApiInfo Api_Info = new ApiInfo();
        SubjectInfoADO SubjectInfo = new SubjectInfoADO();
        SubjectDateADO SubjectDate = new SubjectDateADO();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageDataLoad();

            }
        }

        /// <summary>
        /// 取得頁面資料
        /// </summary>
        private void PageDataLoad()
        {
            DataTable dt = SubjectInfo.Get_SubjectInfo_MaxSID_WHERE_CategoryID("C1");

            if (dt != null && dt.Rows.Count > 0)
            {
                //第幾次上課
                txtSubCount1.Text = dt.Rows[0]["SubCount"].ToString().Substring(0, 4);
                txtSubCount2.Text = (int.Parse(dt.Rows[0]["SubCount"].ToString().Substring(4, 2)) + 1).ToString();


                //報名條件
                txtSUCondition.Text = dt.Rows[0]["SUCondition"].ToString();

                //上課時間
                //C1 一、二課
                txtSDate12.Text = DateTime.UtcNow.AddHours(8).AddDays(7).ToString("yyyy/MM/dd");
                dropSubTime12.SelectedIndex = 1;
                txtSubTime12.Text = "14:30~17:30";

                //C1 三、四課
                txtSDate34.Text = DateTime.UtcNow.AddHours(8).AddDays(14).ToString("yyyy/MM/dd");
                dropSubTime34.SelectedIndex = 1;
                txtSubTime34.Text = "14:30~17:30";

                //地點
                txtSubLocation.Text = dt.Rows[0]["SubLocation"].ToString();

                //報名日期
                txtSubStrDate.Text = DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd");

                //報名截止
                txtSubEndDate.Text = DateTime.UtcNow.AddHours(8).AddDays(30).ToString("yyyy/MM/dd");
            }
            else
            {

                //第幾次上課
                txtSubCount1.Text = DateTime.Now.Year.ToString();
                txtSubCount2.Text = "1";

                //報名條件
                string y = DateTime.UtcNow.AddHours(8).Year.ToString();
                txtSUCondition.Text = y + "年1月~" + y + "年12月來的新朋友，或是還沒上過的會友。";

                //上課時間
                //C1 一、二課
                txtSDate12.Text = DateTime.UtcNow.AddHours(8).AddDays(7).ToString("yyyy/MM/dd");
                dropSubTime12.SelectedIndex = 1;
                txtSubTime12.Text = "14:30~17:30";

                //C1 三、四課
                txtSDate34.Text = DateTime.UtcNow.AddHours(8).AddDays(14).ToString("yyyy/MM/dd");
                dropSubTime34.SelectedIndex = 1;
                txtSubTime34.Text = "14:30~17:30";

                //地點
                txtSubLocation.Text = "江子翠行道會主會堂";

                //報名日期
                txtSubStrDate.Text = DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd");

                //報名截止
                txtSubEndDate.Text = DateTime.UtcNow.AddHours(8).AddDays(30).ToString("yyyy/MM/dd");
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //第幾次上課
            string SubCount = txtSubCount1.Text.Trim() + int.Parse(txtSubCount2.Text).ToString("00").Trim();

            string SubName = "生命建造程序 C1 課程報名";
            string SUCondition = txtSUCondition.Text.Trim();
            string SubLocation = txtSubLocation.Text.Trim();
            string SubStrDate = txtSubStrDate.Text.Trim(); //txtSubStrDate
            string SubEndDate = txtSubEndDate.Text.Trim();
            string Memo = txtMemo.Text.Trim();

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='class-info'>");
            sb.Append("<div role='alert' class='el - alert el - alert--info'>");
            sb.Append("<!---->");
            sb.Append("<div class='el - alert__content'>");
            sb.Append("<!---->");
            sb.Append("<p class='el - alert__description'>");
            sb.Append("本表個人資料將作建檔處理，並於日後會務活動運作之目的內，由教會及同工作為聯絡通訊、關懷及相關合理運用。我們會盡善良管理人責任，妥善保管資料，避免外洩或不當用途之使用。");
            sb.Append("</p>");
            sb.Append("<i class='el - alert__closebtn el - icon - close' style='display: none; '>");
            sb.Append("</i>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<ul class='class-detail'>");
            sb.Append("<li>");

            sb.Append("<div class='class-detail-title'>");
            sb.Append("報名條件："); //SUCondition
            sb.Append("</div>");
            sb.Append("<div class='class-detail-text'>");
            //2018年1月~2018年12月來的新朋友，或是還沒上過的會友。
            sb.Append(SUCondition);
            sb.Append("</div>");
            sb.Append("</li>");
            sb.Append("<li>");

            sb.Append("<div class='class-detail-title'>");
            sb.Append("上課日期："); //SDate, SubTime
            sb.Append("</div>");
            sb.Append("<div class='class-detail-text'>");

            //08/05(日)、08/12(日) 下午 14:30~17:30
            if (ckbIsSub12.Checked)
            {
                sb.Append("C1 一、二課：" + txtSDate12.Text.Trim().Replace(DateTime.UtcNow.AddHours(8).Year.ToString() + "/", "") +
                "(" + Api_Info.GetDayOfWeek(DateTime.Parse(txtSDate12.Text.Trim())) + ") " + " ");
                sb.Append(dropSubTime12.Text + " " + txtSubTime12.Text.Trim());
            }

            if (ckbIsSub34.Checked)
            {
                sb.Append("<br/>");
                sb.Append("C1 三、四課：" + txtSDate34.Text.Trim().Replace(DateTime.UtcNow.AddHours(8).Year.ToString() + "/", "") +
"(" + Api_Info.GetDayOfWeek(DateTime.Parse(txtSDate34.Text.Trim())) + ") " + " ");
                sb.Append(dropSubTime34.Text + " " + txtSubTime34.Text.Trim());
            }

            sb.Append("</div>");
            sb.Append("</li>");
            sb.Append("<li>");

            sb.Append("<div class='class-detail-title'>");
            sb.Append("地點："); //SubLocation
            sb.Append("</div>");
            sb.Append("<div class='class-detail-text'>");
            //江子翠行道會主會堂
            sb.Append(SubLocation.Replace("\r\n", "<br/>"));
            sb.Append("</div>");
            sb.Append("</li>");
            sb.Append("<li>");

            sb.Append("<div class='class-detail-title'>");
            sb.Append("報名日期："); //SubEndDate
            sb.Append("</div>");
            sb.Append("<div class='class-detail-text'>");
            //即日起~07/31(二) 截止報名，之後請現場報名。
            sb.Append("即日起~" +
                        SubEndDate.Replace(DateTime.UtcNow.AddHours(8).Year.ToString() + "/", "") +
                            "(" + Api_Info.GetDayOfWeek(DateTime.Parse(SubEndDate)) + ") " +
                        "截止報名，之後請現場報名。");
            sb.Append("</div>");

            sb.Append("<div class='class-detail-title'>");
            sb.Append("備註："); //Memo
            sb.Append("</div>");
            sb.Append("<div class='class-detail-text'>");
            sb.Append(Memo.Replace("\r\n", "<br/>"));
            sb.Append("</div>");


            sb.Append("</li>");
            sb.Append("</ul>");
            sb.Append("</div>");
            string HtmlSubDesc = sb.ToString();

            SubjectInfo.InsSubjectInfo(SubCount, "C1", SubName, SUCondition, SubLocation,
                                                              SubStrDate, SubEndDate, Memo, HtmlSubDesc, 99999);

            DataTable dtSubjectInfo = SubjectInfo.QueryMaxSIDBySubjectInfo("C1");
            int SID = int.Parse(dtSubjectInfo.Rows[0]["SID"].ToString());

            //C1 一、二課
            if (ckbIsSub12.Checked)
            {
                SubjectDate.InsSubjectDate(SID, "C112", txtSDate12.Text.Trim(),
                                                                     dropSubTime12.Text + " " + txtSubTime12.Text.Trim());
            }


            //C1 三、四課
            if (ckbIsSub34.Checked)
            {
                SubjectDate.InsSubjectDate(SID, "C134", txtSDate34.Text.Trim(),
                                                                      dropSubTime34.Text + " " + txtSubTime34.Text.Trim());
            }

            btnSave.PostBackUrl = "~/Admin/SubjectData/SubjectList.aspx";
            ScriptManager.RegisterStartupScript(Page, GetType(), "Save", "<script>clickSave()</script>", false);
            //Response.Write("<script>alert('C1 課程新增成功!');location.href='SubjectList.aspx?id=C1';</script>");

        }

        protected void ckbIsSub12_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbIsSub12.Checked)
            { //有開課
                txtSDate12.Visible = true;
                dropSubTime12.Visible = true;
                txtSubTime12.Visible = true;
            }
            else
            {
                txtSDate12.Visible = false;
                dropSubTime12.Visible = false;
                txtSubTime12.Visible = false;
            }
        }

        protected void ckbIsSub34_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbIsSub34.Checked)
            { //有開課
                txtSDate34.Visible = true;
                dropSubTime34.Visible = true;
                txtSubTime34.Visible = true;
            }
            else
            {
                txtSDate34.Visible = false;
                dropSubTime34.Visible = false;
                txtSubTime34.Visible = false;
            }
        }
    }
}