﻿using ADO;
using LifeBuildC.PrjMethod;
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
    public partial class SubjectC2Edit : System.Web.UI.Page
    {
        PrjDate pjdate = new PrjDate();
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

        private void PageDataLoad()
        {
            DataTable dt = SubjectInfo.GetSubjectDateBySubjectInfo(int.Parse(Request.QueryString["id"].ToString()));

            if (dt.Rows.Count > 0)
            {
                //報名條件
                txtSUCondition.Text = dt.Rows[0]["SUCondition"].ToString();

                //上課時間
                //C2 一、二課
                if (dt.Select("CategoryID='C212'").Count() > 0)
                {
                    txtSDate12.Text = dt.Select("CategoryID='C212'")[0]["SDate"].ToString();
                    string[] arrDate12 = dt.Select("CategoryID='C212'")[0]["SubTime"].ToString().Split(' ');
                    dropSubTime12.SelectedValue = arrDate12[0];
                    txtSubTime12.Text = arrDate12[1];
                }

                //C2 三、四課
                if (dt.Select("CategoryID='C234'").Count() > 0)
                {
                    txtSDate34.Text = dt.Select("CategoryID='C234'")[0]["SDate"].ToString();
                    string[] arrDate34 = dt.Select("CategoryID='C234'")[0]["SubTime"].ToString().Split(' ');
                    dropSubTime34.SelectedValue = arrDate34[0];
                    txtSubTime34.Text = arrDate34[1];
                }

                //C2 五課
                if (dt.Select("CategoryID='C25'").Count() > 0)
                {
                    txtSDate5.Text = dt.Select("CategoryID='C25'")[0]["SDate"].ToString();
                    string[] arrDate5 = dt.Select("CategoryID='C25'")[0]["SubTime"].ToString().Split(' ');
                    dropSubTime5.SelectedValue = arrDate5[0];
                    txtSubTime5.Text = arrDate5[1];
                }

                //地點
                txtSubLocation.Text = dt.Rows[0]["SubLocation"].ToString();

                //報名日期
                txtSubStrDate.Text = dt.Rows[0]["SubStrDate"].ToString();

                //報名截止
                txtSubEndDate.Text = dt.Rows[0]["SubEndDate"].ToString();

            }
        }

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
                sb.Append(txtSDate12.Text.Trim().Replace(DateTime.UtcNow.AddHours(8).Year.ToString() + "/", "") +
                "(" + pjdate.GetDayOfWeek(DateTime.Parse(txtSDate12.Text.Trim())) + ") " + " ");
                sb.Append(dropSubTime12.Text + " " + txtSubTime12.Text.Trim());
            }

            if (ckbIsSub34.Checked)
            {
                sb.Append("<br/>");
                sb.Append(txtSDate34.Text.Trim().Replace(DateTime.UtcNow.AddHours(8).Year.ToString() + "/", "") +
"(" + pjdate.GetDayOfWeek(DateTime.Parse(txtSDate34.Text.Trim())) + ") " + " ");
                sb.Append(dropSubTime34.Text + " " + dropSubTime34.Text.Trim());
            }

            sb.Append("</div>");
            sb.Append("</li>");
            sb.Append("<li>");

            sb.Append("<div class='class-detail-title'>");
            sb.Append("地點："); //SubLocation
            sb.Append("</div>");
            sb.Append("<div class='class-detail-text'>");
            //江子翠行道會主會堂
            sb.Append(SubLocation);
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
                            "(" + pjdate.GetDayOfWeek(DateTime.Parse(SubEndDate)) + ") " +
                        "截止報名，之後請現場報名。");
            sb.Append("</div>");

            sb.Append("<div class='class-detail-title'>");
            sb.Append("備註："); //Memo
            sb.Append("</div>");
            sb.Append("<div class='class-detail-text'>");
            sb.Append(Memo);
            sb.Append("</div>");


            sb.Append("</li>");
            sb.Append("</ul>");
            sb.Append("</div>");
            string HtmlSubDesc = sb.ToString();

            SubjectInfo.Update_SubjectInfo(SUCondition, SubLocation, SubStrDate, SubEndDate, SID, Memo, HtmlSubDesc);

            #endregion

            #region Update SubjectDate

            //上課時間
            //C2 一、二課
            if (txtSDate12.Text.Trim() != "")
            { //新增課程
                string SDate12 = txtSDate12.Text.Trim();
                string SubTime12 = dropSubTime12.Text + " " + txtSubTime12.Text.Trim();
                SubjectDate.UpdSubjectDate(SDate12, SubTime12, SID, "C212");
            }
            else
            { //刪除課程
                SubjectDate.DelSubjectDate(SID, "C212");
            }


            //C2 三、四課
            if (txtSDate34.Text.Trim() != "")
            { //新增課程
                string SDate34 = txtSDate34.Text.Trim();
                string SubTime34 = dropSubTime34.Text + " " + txtSubTime34.Text.Trim();
                SubjectDate.UpdSubjectDate(SDate34, SubTime34, SID, "C234");
            }
            else
            { //刪除課程
                SubjectDate.DelSubjectDate(SID, "C234");
            }

            if (txtSDate5.Text.Trim() != "")
            { //新增課程
                string SDate5 = txtSDate5.Text.Trim();
                string SubTime5 = dropSubTime5.Text + " " + txtSubTime5.Text.Trim();
                SubjectDate.UpdSubjectDate(SDate5, SubTime5, SID, "C25");
            }
            else
            { //刪除課程
                SubjectDate.DelSubjectDate(SID, "C25");
            }

            #endregion

            Response.Write("<script>alert('C2 課程儲存成功!');location.href='SubjectList.aspx';</script>");

        }

        protected void btnCel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubjectList.aspx");
        }

    }
}