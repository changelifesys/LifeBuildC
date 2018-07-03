using ADO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin.SubjectData
{
    public partial class SubjectC1Edit : System.Web.UI.Page
    {
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
                //報名條件
                txtSUCondition.Text = dt.Rows[0]["SUCondition"].ToString();

                //上課時間
                //C1 一、二課
                if (dt.Select("CategoryID2='C112'").Count() > 0)
                {
                    txtSDate12.Text = DateTime.Parse(dt.Select("CategoryID2='C112'")[0]["SDate"].ToString()).ToString("yyyy/MM/dd");
                    string[] arrDate12 = dt.Select("CategoryID2='C112'")[0]["SubTime"].ToString().Split(' ');
                    dropSubTime12.SelectedValue = arrDate12[0];
                    txtSubTime12.Text = arrDate12[1];
                }

                //C1 三、四課
                if (dt.Select("CategoryID2='C134'").Count() > 0)
                {
                    txtSDate34.Text = DateTime.Parse(dt.Select("CategoryID2='C134'")[0]["SDate"].ToString()).ToString("yyyy/MM/dd");
                    string[] arrDate34 = dt.Select("CategoryID2='C134'")[0]["SubTime"].ToString().Split(' ');
                    dropSubTime34.SelectedValue = arrDate34[0];
                    txtSubTime34.Text = arrDate34[1];
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

            SubjectInfo.UpdSubjectInfo(SUCondition, SubLocation, SubStrDate, SubEndDate, SID, Memo);

            #endregion

            #region Update SubjectDate

            //上課時間
            //C1 一、二課
            if (txtSDate12.Text.Trim() != "")
            { //新增課程
                string SDate12 = txtSDate12.Text.Trim();
                string SubTime12 = dropSubTime12.Text + " " + txtSubTime12.Text.Trim();
                SubjectDate.UpdSubjectDate(SDate12, SubTime12, SID, "C112");
            }
            else
            { //刪除課程
                SubjectDate.DelSubjectDate(SID, "C112");
            }


            //C1 三、四課
            if (txtSDate34.Text.Trim() != "")
            { //新增課程
                string SDate34 = txtSDate34.Text.Trim();
                string SubTime34 = dropSubTime34.Text + " " + txtSubTime34.Text.Trim();
                SubjectDate.UpdSubjectDate(SDate34, SubTime34, SID, "C134");
            }
            else
            { //刪除課程
                SubjectDate.DelSubjectDate(SID, "C134");
            }


            #endregion

            Response.Write("<script>alert('C1 課程儲存成功!');location.href='SubjectList.aspx';</script>");

        }

        /// <summary>
        /// 返回
        /// </summary>
        protected void btnCel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubjectList.aspx");
        }

    }
}