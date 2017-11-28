using ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin.MemSubData
{
    public partial class MemSubDataList : System.Web.UI.Page
    {
        ChcMemberADO member = new ChcMemberADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvChcMember.DataSource = member.QueryAllByChcMember();
                gvChcMember.DataBind();
            }
        }

        protected void gvChcMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //滑鼠移入移出效果
            if (e.Row.RowType == DataControlRowType.DataRow &&
                ((e.Row.RowState & DataControlRowState.Edit) <= 0))
            {
                ((Button)e.Row.FindControl("btnView")).PostBackUrl = "MemSubDataEdit.aspx?id=" + DataBinder.Eval(e.Row.DataItem, "MID").ToString();

                #region 小組

                ((Label)e.Row.FindControl("lblGroupCName")).Text = DataBinder.Eval(e.Row.DataItem, "GroupCName").ToString();
                ((Label)e.Row.FindControl("lblGroupName")).Text = DataBinder.Eval(e.Row.DataItem, "GroupName").ToString();

                #endregion

                #region C1 第一、二課

                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsC112").ToString()))
                {
                    ((Label)e.Row.FindControl("lblIsC112")).Text = "V";
                }
                else
                {
                    ((Label)e.Row.FindControl("lblIsC112")).Text = "";
                }

                #endregion

                #region C1 第三、四課

                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsC134").ToString()))
                {
                    ((Label)e.Row.FindControl("lblIsC134")).Text = "V";
                }
                else
                {
                    ((Label)e.Row.FindControl("lblIsC134")).Text = "";
                }

                #endregion

                #region C2 第一、二課

                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsC212").ToString()))
                {
                    ((Label)e.Row.FindControl("lblIsC212")).Text = "V";
                }
                else
                {
                    ((Label)e.Row.FindControl("lblIsC212")).Text = "";
                }

                #endregion

                #region C2 第三、四課

                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsC234").ToString()))
                {
                    ((Label)e.Row.FindControl("lblIsC234")).Text = "V";
                }
                else
                {
                    ((Label)e.Row.FindControl("lblIsC234")).Text = "";
                }

                #endregion

                #region C2 第五課

                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "IsC25").ToString()))
                {
                    ((Label)e.Row.FindControl("lblIsC25")).Text = "V";
                }
                else
                {
                    ((Label)e.Row.FindControl("lblIsC25")).Text = "";
                }

                #endregion

                #region 交見證

                if (bool.Parse(DataBinder.Eval(e.Row.DataItem, "Iswitness").ToString()))
                {
                    ((Label)e.Row.FindControl("lblIswitness")).Text = "V";
                }
                else
                {
                    ((Label)e.Row.FindControl("lblIswitness")).Text = "";
                }

                #endregion

            }
        }


    }
}