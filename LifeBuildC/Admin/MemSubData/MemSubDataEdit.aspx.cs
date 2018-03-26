using ADO;
using LifeBuildC.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin.MemSubData
{
    public partial class MemSubDataEdit : System.Web.UI.Page
    {
        ChcMemberADO member = new ChcMemberADO();
        ChcGroupADO group = new ChcGroupADO();
        ClassStatusADO cstatus = new ClassStatusADO();
        ChcGroupClass groupclass = new ChcGroupClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CheckIntByRequest("id", 0) > 0)
                {
                    hfChcGroup.Value = groupclass.GetJsonByChcGroup();
                    PageDataLoad();
                }
                else
                {
                    Response.Redirect("MemSubDataList.aspx");
                }

            }

        }

        private void PageDataLoad()
        {
            DataTable dt = member.QueryMIDByChcMember(int.Parse(Request.QueryString["id"].ToString()));

            //組別
            hfGroupClass.Value = dt.Rows[0]["GroupClass"].ToString();

            //小組
            hfGroupName.Value = dt.Rows[0]["group2"].ToString();

            //姓名
            txtEname.Text = dt.Rows[0]["Ename"].ToString();

            //教會
            dropChurch.SelectedValue = dt.Rows[0]["Church"].ToString();

            //C1 第一、二課
            if (bool.Parse(dt.Rows[0]["IsC112"].ToString()))
            {
                chkIsC112.Checked = true;
            }

            //C1 第三、四課
            if (bool.Parse(dt.Rows[0]["IsC134"].ToString()))
            {
                chkIsC134.Checked = true;
            }

            //C2 第一、二課
            if (bool.Parse(dt.Rows[0]["IsC212"].ToString()))
            {
                chkIsC212.Checked = true;
            }

            //C2 第三、四課
            if (bool.Parse(dt.Rows[0]["IsC234"].ToString()))
            {
                chkIsC234.Checked = true;
            }

            //C2 第五課
            if (bool.Parse(dt.Rows[0]["IsC25"].ToString()))
            {
                chkIsC25.Checked = true;
            }

            //C1 考試
            txtC1_Score.Text = dt.Rows[0]["C1_Score"].ToString();

            //C2 第一、二課考試
            txtC212_Score.Text = dt.Rows[0]["C212_Score"].ToString();

            //C2 第三、四課考試
            txtC234_Score.Text = dt.Rows[0]["C234_Score"].ToString();

            //交見證
            if (bool.Parse(dt.Rows[0]["Iswitness"].ToString()))
            {
                chkIswitness.Checked = true;
            }

            txtwitness.Text = dt.Rows[0]["witness"].ToString();

            //C1 通過判定
            lblC1_Status.Text = dt.Rows[0]["C1_Status"].ToString();

            //C2 通過判定
            lblC2_Status.Text = dt.Rows[0]["C2_Status"].ToString();

            //是否離開教會
            chkIsLeave.Checked = false;
            if (bool.Parse(dt.Rows[0]["IsLeave"].ToString()))
            {
                chkIsLeave.Checked = true;
            }

        }

        /// <summary>
        /// 儲存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int MID = int.Parse(Request.QueryString["id"].ToString());

            DataTable dtStatus = cstatus.QueryByClassStatus();

            //組別
            string GroupClass = hfGroupClassValue.Value;

            //小組
            string GroupCName = string.Empty;
            string GroupName = string.Empty;
            try
            {
                string _group = hfGroupNameValue.Value;
                string[] arrg = _group.Split('.');
                GroupCName = arrg[1].Split('-')[0];
                GroupName = arrg[1].Split('-')[1];
            }
            catch
            {
                GroupCName = "";
                GroupName = "";
            }


            //姓名
            string Ename = txtEname.Text.Trim();

            //教會
            string Church = dropChurch.SelectedItem.Text;

            //C1 第一、二課
            bool IsC112 = chkIsC112.Checked;

            //C1 第三、四課
            bool IsC134 = chkIsC134.Checked;

            //C2 第一、二課
            bool IsC212 = chkIsC212.Checked;

            //C2 第三、四課
            bool IsC234 = chkIsC234.Checked;

            //C2 第五課
            bool IsC25 = chkIsC25.Checked;

            //C1 考試
            int C1_Score = int.Parse(txtC1_Score.Text.Trim());

            //C2 第一、二課考試
            int C212_Score = int.Parse(txtC212_Score.Text.Trim());

            //C2 第三、四課考試
            int C234_Score = int.Parse(txtC234_Score.Text.Trim());

            //交見證
            bool Iswitness = chkIswitness.Checked;
            string witness = txtwitness.Text.Trim();

            //C1 通過判定
            string C1_Status = dtStatus.Select("StatusID='C002'")[0]["ClassStatus"].ToString();
            if (IsC112 && IsC134)
            {
                C1_Status = dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString();
            }


            //C2 通過判定
            string C2_Status = dtStatus.Select("StatusID='C002'")[0]["ClassStatus"].ToString();
            if (IsC212 && IsC234 && IsC25 && Iswitness && C1_Score > 70 && C212_Score > 70 && C234_Score > 70)
            {
                C2_Status = dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString();
            }

            //是否離開教會
            bool IsLeave = chkIsLeave.Checked;

            member.UpdChcMember2(MID, GroupCName, GroupName, GroupClass,
                                                             Ename, Church, C1_Status, C2_Status,
                                                             IsC112, IsC134, IsC212, IsC234, IsC25, C1_Score, C212_Score, C234_Score,
                                                             witness, Iswitness, IsLeave);

            btnSave.PostBackUrl = "~/Admin/MemSubData/MemSubDataList.aspx";
            //Response.Write("<script>alert('儲存成功');</script>");
            ScriptManager.RegisterStartupScript(Page, GetType(), "clicktest", "<script>clickSave()</script>", false);
        }

        private int CheckIntByRequest(string Request_Name, int initial_value)
        {
            string _value = initial_value.ToString();
            if (Request[Request_Name] != null && Request[Request_Name].ToString().Length > 0)
                _value = Request[Request_Name].ToString();

            int _IntValue = initial_value;
            if (CheckInt(_value)) //只能輸入數字
                _IntValue = int.Parse(_value); //是數字

            return _IntValue;
        }

        /// <summary>
        /// Int資料格式檢查
        /// </summary>
        /// <param name="IntData">Int資料</param>
        /// <returns>True:驗證通過;Fals:驗證失敗</returns>
        public bool CheckInt(string IntData)
        {
            int _data = 0;
            int.TryParse(IntData, out _data);

            if (_data > 0)
            {
                return true;
            }

            return false;
        }

        protected void btnCel_Click(object sender, EventArgs e)
        {
        }
    }
}