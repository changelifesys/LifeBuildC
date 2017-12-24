using ADO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC
{
    public partial class Fire18SignUp03 : System.Web.UI.Page
    {
        FirePassWADO firePass = new FirePassWADO();
        FireMemberADO fireMem = new FireMemberADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassKey"] != null && firePass.CheckPassKey(Session["PassKey"].ToString()))
                {
                    DataTable dt = fireMem.GetFireMemberWherePassKey(Session["PassKey"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lblGroupName.Text = dt.Rows[0]["group2"].ToString();
                        lblEname.Text = dt.Rows[0]["Ename"].ToString();
                        lblPhone.Text = dt.Rows[0]["Phone"].ToString();
                        lblGmail.Text = dt.Rows[0]["Gmail"].ToString();
                        lblGmail2.Text = dt.Rows[0]["Gmail"].ToString();

                        if (bool.Parse(dt.Rows[0]["gender"].ToString()))
                        {
                            lblgender.Text = "男生";
                        }
                        else
                        {
                            lblgender.Text = "女生";
                        }


                        lblBirthday.Text = DateTime.Parse(dt.Rows[0]["Birthday"].ToString()).ToString("yyyy/MM/dd");
                        lblClothesSize.Text = dt.Rows[0]["ClothesSize"].ToString();

                        if (bool.Parse(dt.Rows[0]["Course"].ToString()))
                        {
                            lblCourse.Text = "教會突破";
                        }
                        else
                        {
                            lblCourse.Text = "生命突破";
                        }

                        
                    }

                }
                else
                {
                    Response.Redirect("Fire18SignUp.aspx");
                }
            }
        }
    }
}