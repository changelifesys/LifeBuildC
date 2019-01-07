using ADO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin.SubjectData
{
    public partial class SubjectList : System.Web.UI.Page
    {
        SubjectDateADO SubjectDate = new SubjectDateADO();
        SubjectInfoADO SubjectInfo = new SubjectInfoADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string CategoryID = string.Empty;

                //判斷並取得 HTTP POST CategoryID 的值
                if (Request.Form["CategoryID"] != null && Request.Form["CategoryID"].ToString() != "")
                {
                    CategoryID = Request.Form["CategoryID"].ToString();

                    switch(CategoryID)
                    {
                        case "C1":
                            dropSubject.SelectedValue = "C1";
                            btnAddSubject.Text = "新增 C1 課程";
                            break;
                        case "C2":
                            dropSubject.SelectedValue = "C2";
                            btnAddSubject.Text = "新增 C2 課程";
                            break;
                        case "C2QT":
                            dropSubject.SelectedValue = "C2QT";
                            btnAddSubject.Text = "新增  QT 研習營課程";
                            break;
                    }

                }
                else
                {
                    CategoryID = "C1";
                    dropSubject.SelectedValue = "C1";
                    btnAddSubject.Text = "新增 C1 課程";
                }

                gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate(CategoryID);
                gvSubject.DataBind();
            }

        }

        protected void gvSubject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //滑鼠移入移出效果
            if (e.Row.RowType == DataControlRowType.DataRow &&
                ((e.Row.RowState & DataControlRowState.Edit) <= 0))
            {
                ((Label)e.Row.FindControl("lblSubCount")).Text = DataBinder.Eval(e.Row.DataItem, "SubCount").ToString().Substring(0, 4) + "年第" +
                    DataBinder.Eval(e.Row.DataItem, "SubCount").ToString().Substring(4, 2) + "次開";

                switch (DataBinder.Eval(e.Row.DataItem, "CategoryID").ToString())
                {
                    case "C112":
                        ((Label)e.Row.FindControl("lblCategoryID")).Text = "C1 一、二課";
                        break;
                    case "C134":
                        ((Label)e.Row.FindControl("lblCategoryID")).Text = "C1 三、四課";
                        break;
                    case "C212":
                        ((Label)e.Row.FindControl("lblCategoryID")).Text = "C2 一、二課";
                        break;
                    case "C234":
                        ((Label)e.Row.FindControl("lblCategoryID")).Text = "C2 三、四課";
                        break;
                    case "C25":
                        ((Label)e.Row.FindControl("lblCategoryID")).Text = "C2 五課";
                        break;
                    case "C2QT":
                        ((Label)e.Row.FindControl("lblCategoryID")).Text = "QT 研習營";
                        break;
                }

                switch (DataBinder.Eval(e.Row.DataItem, "CategoryID").ToString())
                {
                    case "C112":
                    case "C134":
                        ((Button)e.Row.FindControl("btnEdit")).PostBackUrl = "SubjectC1Edit.aspx?id=" + DataBinder.Eval(e.Row.DataItem, "SID").ToString();
                        break;

                    case "C212":
                    case "C234":
                    case "C25":
                        ((Button)e.Row.FindControl("btnEdit")).PostBackUrl = "SubjectC2Edit.aspx?id=" + DataBinder.Eval(e.Row.DataItem, "SID").ToString();
                        break;

                    case "C2QT":
                        ((Button)e.Row.FindControl("btnEdit")).PostBackUrl = "SubjectC2QTEdit.aspx?id=" + DataBinder.Eval(e.Row.DataItem, "SID").ToString();
                        break;
                }

                //報名時間
                ((Label)e.Row.FindControl("lblSubDate")).Text = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "SubStrDate").ToString()).ToString("yyyy/MM/dd") + "~" + 
                    DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "SubEndDate").ToString()).ToString("yyyy/MM/dd");

                if (DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "SubStrDate").ToString()) < DateTime.UtcNow.AddHours(8))
                {
                    ((Label)e.Row.FindControl("lblSubject")).Text = "開始報名中";
                    ((Button)e.Row.FindControl("btnEdit")).Visible = true; //隱藏編輯鈕

                    if (Request.QueryString["delete"] != null && Request.QueryString["delete"].ToString() == "true")
                    {
                        ((Button)e.Row.FindControl("btnDel")).Visible = false; //隱藏刪除鈕
                    }
                    else
                    {
                        ((Button)e.Row.FindControl("btnDel")).Visible = true; //隱藏刪除鈕
                    }

                }
                else if (DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "SDate").ToString()) < DateTime.UtcNow.AddHours(8))
                {
                    ((Label)e.Row.FindControl("lblSubject")).Text = "已結束";
                    ((Button)e.Row.FindControl("btnEdit")).Visible = false; //隱藏編輯鈕
                    ((Button)e.Row.FindControl("btnDel")).Visible = false; //隱藏刪除鈕
                }

            }
        }

        protected void gvSubject_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string SID = ((Button)gvSubject.Rows[e.RowIndex].FindControl("btnDel")).Attributes["DELID"].ToString();
            string CategoryID = ((Label)gvSubject.Rows[e.RowIndex].FindControl("lblCategoryID")).Attributes["CategoryID"].ToString();

            SubjectDate.DelSubjectDate(int.Parse(SID), CategoryID);
            SubjectInfo.sp_Delete_SubjectInfo(int.Parse(SID));

            ViewBygvSubject();
        }

        private void ViewBygvSubject()
        {
            switch (dropSubject.SelectedValue)
            {
                case "C1":
                    gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate("C1");
                    gvSubject.DataBind();
                    break;
                case "C2":
                    gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate("C2");
                    gvSubject.DataBind();
                    break;
                case "C2QT":
                    gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate("C2QT");
                    gvSubject.DataBind();
                    break;
            }
        }

        protected void dropSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewBygvSubject();
        }

        protected void btnAddSubject_Click(object sender, EventArgs e)
        {
            switch (dropSubject.SelectedValue)
            {
                case "C1":
                    Response.Redirect("SubjectC1Add.aspx");
                    break;
                case "C2":
                    Response.Redirect("SubjectC2Add.aspx");
                    break;
                case "C2QT":
                    Response.Redirect("SubjectC2QTAdd.aspx");
                    break;
            }
        }


    }
}