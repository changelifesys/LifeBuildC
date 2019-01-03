using libLifeBuildC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin.SubjectData
{
    public partial class SubjectC2MEdit : System.Web.UI.Page
    {
        ApiInfo Api_Info = new ApiInfo();
        AdoInfo Ado_Info = new AdoInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Length > 0)
                {
                    PageDataLoad();
                }
            }
        }

        private void PageDataLoad()
        {
            DataTable dt = Ado_Info.SubjectInfo_ADO.GetSubjectDateBySubjectInfo(int.Parse(Request.QueryString["id"].ToString()));

            if (dt.Rows.Count > 0)
            {
                DataRow dr;

                //第幾次上課
                txtSubCount1.Text = dt.Rows[0]["SubCount"].ToString().Substring(0, 4);
                txtSubCount2.Text = dt.Rows[0]["SubCount"].ToString().Substring(4, 2);

                //報名條件
                txtSUCondition.Text = dt.Rows[0]["SUCondition"].ToString();

                //上課時間
                //C2 榮耀男人
                dr = null;
                dr = dt.Select("CategoryID2='C2M'")[0];
                txtSDate.Text = DateTime.Parse(dr["SDate"].ToString()).ToString("yyyy/MM/dd");
                string[] arrDate = dr["SubTime"].ToString().Split(' ');
                dropSubTime.SelectedValue = arrDate[0];
                txtSubTime.Text = arrDate[1];

                //地點
                txtSubLocation.Text = dt.Rows[0]["SubLocation"].ToString();

                //報名日期
                txtSubStrDate.Text = DateTime.Parse(dt.Rows[0]["SubStrDate"].ToString()).ToString("yyyy/MM/dd");

                //報名截止
                txtSubEndDate.Text = DateTime.Parse(dt.Rows[0]["SubEndDate"].ToString()).ToString("yyyy/MM/dd");

            }
        }

        /// <summary>
        /// 儲存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

    }
}