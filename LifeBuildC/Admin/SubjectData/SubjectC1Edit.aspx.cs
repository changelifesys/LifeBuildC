using ADO;
using libLifeBuildC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin.SubjectData
{
    public partial class SubjectC1Edit : System.Web.UI.Page
    {
        ApiInfo Api_Info = new ApiInfo();
        SubjectInfoADO SubjectInfo = new SubjectInfoADO();
        SubjectDateADO SubjectDate = new SubjectDateADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CheckStringByRequest("id") != "")
                {
                    PageDataLoad();
                }
            }
        }

        /// <summary>
        /// 取得頁面資料
        /// </summary>
        private void PageDataLoad()
        {
            DataTable dt = SubjectInfo.GetSubjectDateBySubjectInfo(int.Parse(Request.QueryString["id"].ToString()));

            if (dt.Rows.Count > 0)
            {
                //第幾次上課
                txtSubCount1.Text = dt.Rows[0]["SubCount"].ToString().Substring(0, 4);
                txtSubCount2.Text = dt.Rows[0]["SubCount"].ToString().Substring(4, 2);

                //關閉簽到
                ckbIsCheckOpen.Checked = bool.Parse(dt.Rows[0]["IsCheckOpen"].ToString());

                //報名條件
                txtSUCondition.Text = dt.Rows[0]["SUCondition"].ToString();

                //上課時間
                //C1 一、二課
                ckbIsSub12.Checked = false;
                if (dt.Select("CategoryID2='C112'").Count() > 0)
                {
                    ckbIsSub12.Checked = true;
                    txtSDate12.Text = DateTime.Parse(dt.Select("CategoryID2='C112'")[0]["SDate"].ToString()).ToString("yyyy/MM/dd");
                    string[] arrDate12 = dt.Select("CategoryID2='C112'")[0]["SubTime"].ToString().Split(' ');
                    dropSubTime12.SelectedValue = arrDate12[0];
                    txtSubTime12.Text = arrDate12[1];
                }
                else
                {
                    txtSDate12.Text = DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd");
                    dropSubTime12.SelectedIndex = 1;
                    txtSubTime12.Text = "14:30~17:30";

                    txtSDate12.Visible = false;
                    dropSubTime12.Visible = false;
                    txtSubTime12.Visible = false;

                }

                //C1 三、四課
                ckbIsSub34.Checked = false;
                if (dt.Select("CategoryID2='C134'").Count() > 0)
                {
                    ckbIsSub34.Checked = true;
                    txtSDate34.Text = DateTime.Parse(dt.Select("CategoryID2='C134'")[0]["SDate"].ToString()).ToString("yyyy/MM/dd");
                    string[] arrDate34 = dt.Select("CategoryID2='C134'")[0]["SubTime"].ToString().Split(' ');
                    dropSubTime34.SelectedValue = arrDate34[0];
                    txtSubTime34.Text = arrDate34[1];
                }
                else
                {
                    txtSDate34.Text = DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd");
                    dropSubTime34.SelectedIndex = 1;
                    txtSubTime34.Text = "14:30~17:30";

                    txtSDate34.Visible = false;
                    dropSubTime34.Visible = false;
                    txtSubTime34.Visible = false;
                }

                //地點
                txtSubLocation.Text = dt.Rows[0]["SubLocation"].ToString();

                //報名日期
                txtSubStrDate.Text = DateTime.Parse(dt.Rows[0]["SubStrDate"].ToString()).ToString("yyyy/MM/dd");

                //報名截止
                txtSubEndDate.Text = DateTime.Parse(dt.Rows[0]["SubEndDate"].ToString()).ToString("yyyy/MM/dd");

                //備註
                txtMemo.Text = dt.Rows[0]["Memo"].ToString();

            }


        }

        /// <summary>
        /// 檢查get參數
        /// </summary>
        /// <param name="QueryString_Name"></param>
        /// <returns></returns>
        private string CheckStringByRequest(string QueryString_Name)
        {
            string _value = string.Empty;
            if (Request.QueryString[QueryString_Name] != null && Request.QueryString[QueryString_Name].ToString().Length > 0)
                _value = Request.QueryString[QueryString_Name].ToString();

            return _value;
        }

        /// <summary>
        /// 儲存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int SID = int.Parse(Request.QueryString["id"].ToString());

            #region Update SubjectInfo

            //第幾次上課
            string SubCount = txtSubCount1.Text.Trim() + int.Parse(txtSubCount2.Text).ToString("00").Trim();

            //報名條件
            string SUCondition = txtSUCondition.Text;

            //地點
            string SubLocation = txtSubLocation.Text;

            //報名日期
            string SubStrDate = txtSubStrDate.Text;

            //報名截止
            string SubEndDate = txtSubEndDate.Text;

            //備註
            string Memo = txtMemo.Text;

            //HTML
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
            sb.Append(SubLocation.Replace("\r\n","<br/>"));
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


            SubjectInfo.Update_SubjectInfo(SubCount, SUCondition, SubLocation, SubStrDate, SubEndDate, SID, Memo, HtmlSubDesc, ckbIsCheckOpen.Checked, 99999);

            #endregion

            #region Update SubjectDate

            //上課時間
            //C1 一、二課
            SubjectDate.DelSubjectDate(SID, "C112");
            if (ckbIsSub12.Checked)
            { 
                //更新課程
                SubjectDate.InsSubjectDate(
                    SID, 
                    "C112", 
                    txtSDate12.Text.Trim(),
                    dropSubTime12.Text + " " + txtSubTime12.Text.Trim()
                );

            }


            //C1 三、四課
            SubjectDate.DelSubjectDate(SID, "C134");
            if (ckbIsSub34.Checked)
            { 
                //更新課程
                SubjectDate.InsSubjectDate(
                    SID, 
                    "C134", 
                    txtSDate34.Text.Trim(),
                    dropSubTime34.Text + " " + txtSubTime34.Text.Trim()
                );
            }

            SubjectInfo.sp_Delete_SubjectInfo(SID);

            #endregion

            btnSave.PostBackUrl = "~/Admin/SubjectData/SubjectList.aspx";
            ScriptManager.RegisterStartupScript(Page, GetType(), "Save", "<script>clickSave()</script>", false);
            //Response.Write("<script>alert('C1 課程儲存成功!');location.href='SubjectList.aspx';</script>");

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