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
    public partial class SubjectC2Edit : System.Web.UI.Page
    {
        //課程資訊
        SubjectInfoADO SubjectInfo = new SubjectInfoADO();
        //上課日期
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

            SubjectInfo.UpdSubjectInfo(SUCondition, SubLocation, SubStrDate, SubEndDate, SID);

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