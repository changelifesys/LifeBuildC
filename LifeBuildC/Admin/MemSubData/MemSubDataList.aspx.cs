using ADO;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

                if (Request.QueryString["gid"] != null && Request.QueryString["gid"].ToString() == "1")
                { //小組長
                    Label10.Visible = false;
                    dropGroupCName.Visible = false;
                }
                else if (Request.QueryString["gid"] != null && Request.QueryString["gid"].ToString() == "2")
                { //區長
                    Label6.Visible = false;
                    dropGroupName.Visible = false;
                }
                else if (Request.QueryString["gid"] == null && Session["Login"] != null && Session["Login"].ToString() == "ok")
                { //中央同工
                    Label10.Visible = false;
                    dropGroupCName.Visible = false;
                }
                else
                {
                    Response.Redirect("MemSubDataList.aspx?gid=1");
                }

                //組別
                if (Request.Form["hfGroupClassValue"] != null)
                {
                    for (int i = 0; i < dropGroupClass.Items.Count; i++)
                    {
                        if (dropGroupClass.Items[i].Text == Request.Form["hfGroupClassValue"].ToString())
                        {
                            dropGroupClass.SelectedIndex = i;
                            dropGroupClass_SelectedIndexChanged(null, null);
                            break;
                        }
                    }
                }

                //小組
                if (Request.Form["hfGroupNameValue"] != null)
                {
                    for (int i = 0; i < dropGroupName.Items.Count; i++)
                    {
                        if (dropGroupName.Items[i].Text == Request.Form["hfGroupNameValue"].ToString())
                        {
                            dropGroupName.SelectedIndex = i;
                            dropGroupName_SelectedIndexChanged(null, null);
                            break;
                        }
                    }
                }

            }
        }

        protected void gvChcMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //滑鼠移入移出效果
            if (e.Row.RowType == DataControlRowType.DataRow &&
                ((e.Row.RowState & DataControlRowState.Edit) <= 0))
            {
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='rgb(187,255,255)'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");

                ((Button)e.Row.FindControl("btnView")).PostBackUrl = "MemSubDataEdit.aspx?id=" + DataBinder.Eval(e.Row.DataItem, "MID").ToString();
                

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
            txtEname.Text = "";

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

                //小組
                string GroupCName = "";
                string GroupName = "";
                if (dropGroupName.SelectedItem != null)
                {
                    string[] arrg = dropGroupName.SelectedItem.Text.Split('.');
                    GroupCName = arrg[1].Split('-')[0];
                    GroupName = arrg[1].Split('-')[1];
                }


                DataTable dtGName = member.QueryGroupNameByChcMember(GroupName);
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
            txtEname.Text = "";

            //小組
            string GroupCName = "";
            string GroupName = "";
            string[] arrg = dropGroupName.SelectedItem.Text.Split('.');
            if (dropGroupName.SelectedItem != null)
            {
                GroupCName = arrg[1].Split('-')[0];
                GroupName = arrg[1].Split('-')[1];
            }

            DataTable dt = member.QueryGroupNameByChcMember(GroupName);
            gvChcMember.DataSource = dt;
            gvChcMember.DataBind();

            lblDataCnt.Text = "查詢共 " + dt.Rows.Count + " 筆資料";
        }

        //區長
        protected void dropGroupCName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEname.Text = "";

            DataTable dt = member.QueryGroupCNameByChcMember(dropGroupCName.SelectedItem.Text);
            gvChcMember.DataSource = dt;
            gvChcMember.DataBind();

            lblDataCnt.Text = "查詢共 " + dt.Rows.Count + " 筆資料";
        }

        protected void txtEname_TextChanged(object sender, EventArgs e)
        {
            if (txtEname.Text.Trim() != "")
            {
                gvChcMember.DataSource = member.QueryLikeEnameByChcMember(txtEname.Text.Trim());
                gvChcMember.DataBind();
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
            //if (((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
            //{
            //    labelText = "Browser is a mobile device.";
            //}
            //else
            //{
            //    labelText = "Browser is not a mobile device.";
            //}

            if ((dropGroupName.Items.Count > 1 ||
                dropGroupCName.Items.Count > 1) && !((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
            {
                //小組
                string GroupCName = string.Empty;
                string GroupName = string.Empty;

                if (Request.QueryString["gid"] == null || Request.QueryString["gid"].ToString() == "1")
                { //中央同工&小組長

                    string[] arrg = dropGroupName.SelectedItem.Text.Split('.');
                    if (dropGroupName.SelectedItem != null)
                    {
                        GroupCName = arrg[1].Split('-')[0];
                        GroupName = arrg[1].Split('-')[1];
                    }
                }
                else if (Request.QueryString["gid"].ToString() == "2")
                { //區長

                    GroupName = dropGroupCName.SelectedItem.Text;
                }
                

                HSSFWorkbook wb = new HSSFWorkbook();
                MemoryStream ms = new MemoryStream();
                HSSFSheet ns = (HSSFSheet)wb.CreateSheet("mySheet");

                ns.CreateRow(0).CreateCell(0).SetCellValue("組   別");
                ns.GetRow(0).CreateCell(1).SetCellValue("小   組");
                ns.GetRow(0).CreateCell(2).SetCellValue("姓   名");
                ns.GetRow(0).CreateCell(3).SetCellValue("教   會");
                ns.GetRow(0).CreateCell(4).SetCellValue("C1第一、二課");
                ns.GetRow(0).CreateCell(5).SetCellValue("C1第三、四課");
                ns.GetRow(0).CreateCell(6).SetCellValue("C2第一、二課");
                ns.GetRow(0).CreateCell(7).SetCellValue("C2第三、四課");
                ns.GetRow(0).CreateCell(8).SetCellValue("C2第五課");
                ns.GetRow(0).CreateCell(9).SetCellValue("C1 考試");
                ns.GetRow(0).CreateCell(10).SetCellValue("C2 第一、二課考試");
                ns.GetRow(0).CreateCell(11).SetCellValue("C2 第三、四課考試");
                ns.GetRow(0).CreateCell(12).SetCellValue("交見證");
                ns.GetRow(0).CreateCell(13).SetCellValue("C1 通過判定");
                ns.GetRow(0).CreateCell(14).SetCellValue("C2 通過判定");

                for (int i = 0; i < gvChcMember.Rows.Count; i++)
                {
                    ns.CreateRow(i + 1).CreateCell(0).SetCellValue(gvChcMember.Rows[i].Cells[0].Text);
                    for (int j = 1; j < gvChcMember.Columns.Count; j++)
                    {
                        switch (j)
                        {
                            case 2: //小組
                                ns.GetRow(i + 1).CreateCell(j - 1).SetCellValue(((Label)gvChcMember.Rows[i].FindControl("lblGroupCName")).Text + "-" +
                                    ((Label)gvChcMember.Rows[i].FindControl("lblGroupName")).Text);
                                break;

                            case 5: //C1 第一、二課
                                ns.GetRow(i + 1).CreateCell(j - 1).SetCellValue(((Label)gvChcMember.Rows[i].FindControl("lblIsC112")).Text);
                                break;
                            case 6:
                                ns.GetRow(i + 1).CreateCell(j - 1).SetCellValue(((Label)gvChcMember.Rows[i].FindControl("lblIsC134")).Text);
                                break;
                            case 7:
                                ns.GetRow(i + 1).CreateCell(j - 1).SetCellValue(((Label)gvChcMember.Rows[i].FindControl("lblIsC212")).Text);
                                break;
                            case 8:
                                ns.GetRow(i + 1).CreateCell(j - 1).SetCellValue(((Label)gvChcMember.Rows[i].FindControl("lblIsC234")).Text);
                                break;
                            case 9:
                                ns.GetRow(i + 1).CreateCell(j - 1).SetCellValue(((Label)gvChcMember.Rows[i].FindControl("lblIsC25")).Text);
                                break;

                            case 13:
                                ns.GetRow(i + 1).CreateCell(j - 1).SetCellValue(((Label)gvChcMember.Rows[i].FindControl("lblIswitness")).Text);
                                break;

                            default:
                                ns.GetRow(i + 1).CreateCell(j - 1).SetCellValue(gvChcMember.Rows[i].Cells[j].Text.Replace("&nbsp;", ""));
                                break;
                        }

                    }
                }

                wb.Write(ms);
                string fileName = GroupName + DateTime.Now.ToString("yyyyMMddHHMMss") + ".xls";
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename=" + fileName));
                Response.BinaryWrite(ms.ToArray());
                wb = null;
                ms.Close();
                ms.Dispose();
            }
            else if (((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
            {
                Response.Write("<script>alert('必需使用電腦才能使用Excel匯出的功能');</script>");
            }
            else
            {
                Response.Write("<script>alert('請先選擇組別&小組查詢資料');</script>");
            }

            

        }



    }
}