using ADO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin
{
    public partial class SubjectC1Add : System.Web.UI.Page
    {
        //課程資訊
        SubjectInfoADO SubjectInfo = new SubjectInfoADO();
        //上課日期
        SubjectDateADO SubjectDate = new SubjectDateADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //報名條件
                string y = DateTime.Now.Year.ToString();
                txtSUCondition.Text = y + "年1月~" + y + "年12月來的新朋友，或是還沒上過的會友。";

                //上課時間
                //C1 一、二課
                txtSDate12.Text = DateTime.Now.ToString("yyyy/MM/dd");
                dropSubTime12.SelectedIndex = 1;
                txtSubTime12.Text = "14:30~17:30";

                //C1 三、四課
                txtSDate34.Text = DateTime.Now.ToString("yyyy/MM/dd");
                dropSubTime34.SelectedIndex = 1;
                txtSubTime34.Text = "14:30~17:30";

                //地點
                txtSubLocation.Text = "江子翠行道會主會堂";

                //報名日期
                txtSubStrDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

                //報名截止
                txtSubEndDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string SubName = "C1課程報名";
            string SUCondition = txtSUCondition.Text.Trim();
            string SubLocation = txtSubLocation.Text.Trim();
            string SubStrDate = txtSubStrDate.Text.Trim(); //txtSubStrDate
            string SubEndDate = txtSubEndDate.Text.Trim();

            SubjectInfo.InsSubjectInfo(SubName, SUCondition, SubLocation,
                                                              SubStrDate, SubEndDate);

            DataTable dtSubjectInfo = SubjectInfo.QueryMaxSIDBySubjectInfo("C1");
            int SID = int.Parse(dtSubjectInfo.Rows[0]["SID"].ToString());

            //C1 一、二課
            if (txtSDate12.Text.Trim() != "")
            {
                SubjectDate.InsSubjectDate(SID, "C112", txtSDate12.Text.Trim(),
                                                                     dropSubTime12.Text + " " + txtSubTime12.Text.Trim());
            }


            //C1 三、四課
            if (txtSDate34.Text.Trim() != "")
            {
                SubjectDate.InsSubjectDate(SID, "C134", txtSDate34.Text.Trim(),
                                                                      dropSubTime34.Text + " " + txtSubTime34.Text.Trim());
            }

            Response.Write("<script>alert('C1 課程新增成功!');location.href='SubjectList.aspx';</script>");

        }

        protected void btnCel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubjectList.aspx");
        }
    }
}