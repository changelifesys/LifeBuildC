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
    public partial class SystemSet : System.Web.UI.Page
    {

        SystemSetADO syset = new SystemSetADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageDataLoad();
            }
        }

        private void PageDataLoad()
        {
            DataTable dt = syset.QuerySystemSet();
            foreach (DataRow dr in dt.Rows)
            {
                switch (dr["ExamCategory"].ToString())
                {
                    case "C212":
                        txtC212.Value = dr["ScoreGoogleLink"].ToString();
                        ckbIsEnable_C212.Checked = bool.Parse(dr["IsEnable"].ToString());
                        break;
                    case "C234":
                        txtC234.Value = dr["ScoreGoogleLink"].ToString();
                        ckbIsEnable_C234.Checked = bool.Parse(dr["IsEnable"].ToString());
                        break;
                    case "C1":
                        txtC1.Value = dr["ScoreGoogleLink"].ToString();
                        ckbIsEnable_C1.Checked = bool.Parse(dr["IsEnable"].ToString());
                        break;
                }

            }
        }

        /// <summary>
        /// 儲存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            syset.UpdUserScore(txtC1.Value.Trim(), "", "C1", ckbIsEnable_C1.Checked.ToString());
            syset.UpdUserScore(txtC212.Value.Trim(), "", "C212", ckbIsEnable_C212.Checked.ToString());
            syset.UpdUserScore(txtC234.Value.Trim(), "", "C234", ckbIsEnable_C234.Checked.ToString());
            Response.Write("<script>alert('儲存成功');</script>");


        }
    }
}