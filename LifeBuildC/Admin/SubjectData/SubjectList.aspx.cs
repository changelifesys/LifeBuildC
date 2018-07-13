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
    public partial class SubjectList : System.Web.UI.Page
    {
        SubjectDateADO SubjectDate = new SubjectDateADO();
        SubSignInfoADO SubSignInfo = new SubSignInfoADO();
        SubjectInfoADO SubjectInfo = new SubjectInfoADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string CategoryID = "C1";
                rdoC1List.Checked = true;

                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                {
                    CategoryID = Request.QueryString["id"].ToString();

                    if (CategoryID == "C2")
                    {
                        rdoC2List.Checked = true;
                    }

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
                }

                switch (DataBinder.Eval(e.Row.DataItem, "CategoryID").ToString())
                {
                    case "C112":
                    case "C134":
                        ((Button)e.Row.FindControl("btnEdit")).PostBackUrl = "SubjectC1Edit.aspx?id=" +
                        DataBinder.Eval(e.Row.DataItem, "SID").ToString();
                        break;

                    case "C212":
                    case "C234":
                    case "C25":
                        ((Button)e.Row.FindControl("btnEdit")).PostBackUrl = "SubjectC2Edit.aspx?id=" +
                        DataBinder.Eval(e.Row.DataItem, "SID").ToString();
                        break;
                }

                //報名時間
                ((Label)e.Row.FindControl("lblSubDate")).Text = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "SubStrDate").ToString()).ToString("yyyy/MM/dd") + "~" + 
                    DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "SubEndDate").ToString()).ToString("yyyy/MM/dd");

                if (DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "SDate").ToString()) < DateTime.UtcNow.AddHours(8))
                {
                    ((Label)e.Row.FindControl("lblSubject")).Text = "課程已結束";
                    ((Button)e.Row.FindControl("btnEdit")).Visible = false; //隱藏編輯鈕
                    ((Button)e.Row.FindControl("btnDel")).Visible = false; //隱藏刪除鈕
                }
                


            }
        }

        protected void rdoC1List_CheckedChanged(object sender, EventArgs e)
        {
            gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate("C1");
            gvSubject.DataBind();
        }

        protected void rdoC2List_CheckedChanged(object sender, EventArgs e)
        {
            gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate("C2");
            gvSubject.DataBind();
        }

        protected void btnAddC1_Click(object sender, EventArgs e)
        {
            //HttpCookie mycookie = new HttpCookie("CategoryID");
            //mycookie.Value = "C1";
            //mycookie.Expires = DateTime.Now.AddHours(1);
            //Response.Cookies.Add(mycookie);
            Response.Redirect("SubjectC1Add.aspx");
        }

        protected void btnAddC2_Click(object sender, EventArgs e)
        {
            //HttpCookie mycookie = new HttpCookie("CategoryID");
            //mycookie.Value = "C2";
            //mycookie.Expires = DateTime.Now.AddHours(1);
            //Response.Cookies.Add(mycookie);
            Response.Redirect("SubjectC2Add.aspx");
        }

        protected void gvSubject_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string SID = ((Button)gvSubject.Rows[e.RowIndex].FindControl("btnDel")).Attributes["DELID"].ToString();
            string CategoryID = ((Label)gvSubject.Rows[e.RowIndex].FindControl("lblCategoryID")).Attributes["CategoryID"].ToString();

            SubjectDate.DelSubjectDate(int.Parse(SID), CategoryID);
            SubjectInfo.sp_Delete_SubjectInfo(int.Parse(SID));

            if (rdoC1List.Checked)
            {
                gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate("C1");
            }
            else
            {
                gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate("C2");
            }

            gvSubject.DataBind();
        }

    }
}