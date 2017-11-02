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
    public partial class ExamList : System.Web.UI.Page
    {
        ExamQuestionsADO eques = new ExamQuestionsADO();
        AnswerItemADO ansitem = new AnswerItemADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CheckStringByRequest("ec") != "")
                {
                    gvExam.DataSource = eques.QueryExamQuestions(Request.QueryString["ec"]);
                    gvExam.DataBind();
                }
                else
                {
                    Response.Redirect("ExamList.aspx");
                }

            }
        }

        private string CheckStringByRequest(string QueryString_Name)
        {
            string _value = string.Empty;
            if (Request.QueryString[QueryString_Name] != null && Request.QueryString[QueryString_Name].ToString().Length > 0)
                _value = Request.QueryString[QueryString_Name].ToString();

            return _value;
        }

        protected void gvExam_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //滑鼠移入移出效果
            if (e.Row.RowType == DataControlRowType.DataRow &&
                ((e.Row.RowState & DataControlRowState.Edit) <= 0))
            {
                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsField1").ToString()))
                {
                    //((Label)e.Row.FindControl("lblField1")).Text = DataBinder.Eval(e.Row.DataItem, "Field1").ToString();
                    ((Label)e.Row.FindControl("lblField1")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Row.FindControl("lblField1")).Font.Underline = true;
                }
                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsField2").ToString()))
                {
                    ((Label)e.Row.FindControl("lblField2")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Row.FindControl("lblField2")).Font.Underline = true;
                }
                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsField3").ToString()))
                {
                    ((Label)e.Row.FindControl("lblField3")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Row.FindControl("lblField3")).Font.Underline = true;
                }
                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsField4").ToString()))
                {
                    ((Label)e.Row.FindControl("lblField4")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Row.FindControl("lblField4")).Font.Underline = true;
                }
                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsField5").ToString()))
                {
                    ((Label)e.Row.FindControl("lblField5")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Row.FindControl("lblField5")).Font.Underline = true;
                }
                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsField6").ToString()))
                {
                    ((Label)e.Row.FindControl("lblField6")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Row.FindControl("lblField6")).Font.Underline = true;
                }
                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsField7").ToString()))
                {
                    ((Label)e.Row.FindControl("lblField7")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Row.FindControl("lblField7")).Font.Underline = true;
                }
                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsField8").ToString()))
                {
                    ((Label)e.Row.FindControl("lblField8")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Row.FindControl("lblField8")).Font.Underline = true;
                }
                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsField9").ToString()))
                {
                    ((Label)e.Row.FindControl("lblField9")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Row.FindControl("lblField9")).Font.Underline = true;
                }
                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsField10").ToString()))
                {
                    ((Label)e.Row.FindControl("lblField10")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Row.FindControl("lblField10")).Font.Underline = true;
                }

                ((LinkButton)e.Row.FindControl("btnEdit")).PostBackUrl = "ExamEdit.aspx?ec=" + DataBinder.Eval(e.Row.DataItem, "ExamCategory").ToString() +
                    "&id=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString();

            }

        }

        protected void gvExam_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = ((Button)gvExam.Rows[e.RowIndex].FindControl("btnDel")).Attributes["DELID"].ToString();

            //從答案項目中刪除
            DataTable dt = eques.QueryExamQuestionsByID(ID);
            for (int i = 1; i <=10; i++)
            {
                if (bool.Parse(dt.Rows[0]["IsField" + i].ToString()))
                {
                    ansitem.DelAnswerItem(Request.QueryString["ec"], dt.Rows[0]["Field" + i].ToString());
                }
            }

            //刪除題目
            eques.DelExamQuestions(ID);

            //刷新資料
            gvExam.DataSource = eques.QueryExamQuestions(Request.QueryString["ec"]);
            gvExam.DataBind();
        }
    }
}