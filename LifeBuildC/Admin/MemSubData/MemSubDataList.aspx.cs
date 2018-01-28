using ADO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin.MemSubData
{
    public partial class MemSubDataList : System.Web.UI.Page
    {
        ChcMemberADO member = new ChcMemberADO();
        ChcGroupADO group = new ChcGroupADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //gvChcMember.DataSource = member.QueryTop100ByChcMember();
                //gvChcMember.DataBind();

                int _cnt = 0;

                #region 組別

                DataTable dtGClass = group.QueryGroupClassByMemSubData();
                foreach (DataRow dr in dtGClass.Rows)
                {
                    ListItem li = new ListItem();
                    li.Text = dr["GroupClass"].ToString();
                    li.Value = _cnt.ToString();
                    dropGroupClass.Items.Add(li);

                    _cnt++;
                }

                #endregion

                if (Request.QueryString["gid"] == null || Request.QueryString["gid"].ToString() == "1")
                { //中央同工&小組長
                    gid2_1.Visible = false;
                    gid2_2.Visible = false;
                }
                else if (Request.QueryString["gid"].ToString() == "2")
                { //區長
                    gid1_1.Visible = false;
                    gid1_2.Visible = false;
                }


            }
        }

        protected void gvChcMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //滑鼠移入移出效果
            if (e.Row.RowType == DataControlRowType.DataRow &&
                ((e.Row.RowState & DataControlRowState.Edit) <= 0))
            {
                ((Button)e.Row.FindControl("btnView")).OnClientClick = "window.open('MemSubDataEdit.aspx?id=" + DataBinder.Eval(e.Row.DataItem, "MID").ToString() + "')";
                

                if (Request.QueryString["gid"] != null)
                {
                    ((Button)e.Row.FindControl("btnView")).Visible = false;
                }

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

        //組別
        protected void dropGroupClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _cnt = 0;

            if (Request.QueryString["gid"] == null || Request.QueryString["gid"].ToString() == "1")
            {
                #region 中央同工&小組長

                DataTable dtGroup = group.QueryGroupNameByChcGroup_1(dropGroupClass.SelectedItem.Text);

                dropGroupName.Items.Clear();
                foreach (DataRow dr in dtGroup.Rows)
                {
                    ListItem li = new ListItem();
                    //出輸格式
                    //AA101.永健牧區-永健小組
                    li.Text = dr["GroupID"].ToString() + "." + dr["GroupCName"].ToString() + "-" + dr["GroupName"].ToString();
                    li.Value = dr["GSort"].ToString();
                    dropGroupName.Items.Add(li);
                }

                dropGroupName.SelectedIndex = 0;
                DataTable dtGName = member.QueryGroupCNameByChcMember(dropGroupName.SelectedItem.Text);
                gvChcMember.DataSource = dtGName;
                gvChcMember.DataBind();

                lblDataCnt.Text = "查詢共 " + dtGName.Rows.Count + " 筆資料";

                #endregion
            }
            else if (Request.QueryString["gid"].ToString() == "2")
            { 
                #region 區長

                DataTable dtGroup = group.QueryGroupCNameByChcGroup(dropGroupClass.SelectedItem.Text);

                dropGroupCName.Items.Clear();
                foreach (DataRow dr in dtGroup.Rows)
                {
                    ListItem li = new ListItem();
                    li.Text = dr["GroupCName"].ToString();
                    li.Value = _cnt.ToString();
                    dropGroupCName.Items.Add(li);
                    _cnt++;
                }

                dropGroupCName.SelectedIndex = 0;
                DataTable dtGCName = member.QueryGroupCNameByChcMember(dropGroupCName.SelectedItem.Text);
                gvChcMember.DataSource = dtGCName;
                gvChcMember.DataBind();

                lblDataCnt.Text = "查詢共 " + dtGCName.Rows.Count + " 筆資料";

                #endregion
            }

        }

        //小組
        protected void dropGroupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //小組
            string[] arrg = dropGroupName.SelectedItem.Text.Split('.');
            string GroupCName = arrg[1].Split('-')[0];
            string GroupName = arrg[1].Split('-')[1];

            DataTable dt = member.QueryGroupNameByChcMember(GroupName);
            gvChcMember.DataSource = dt;
            gvChcMember.DataBind();

            lblDataCnt.Text = "查詢共 " + dt.Rows.Count + " 筆資料";
        }

        //區長
        protected void dropGroupCName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = member.QueryGroupCNameByChcMember(dropGroupCName.SelectedItem.Text);
            gvChcMember.DataSource = dt;
            gvChcMember.DataBind();

            lblDataCnt.Text = "查詢共 " + dt.Rows.Count + " 筆資料";
        }
    }
}