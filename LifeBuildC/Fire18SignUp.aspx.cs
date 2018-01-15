using ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC
{
    public partial class Fire18SignUp : System.Web.UI.Page
    {
        FirePassWADO firePass = new FirePassWADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string PassKey = txtPassKey.Value.Trim().ToUpper();
            if (firePass.CheckPassKey(PassKey))
            {
                Session["PassKey"] = PassKey;
                Response.Redirect("Fire18SignUp02_1.aspx");
            }
            else
            {
                Response.Write("<script>alert('密碼錯誤');</script>");
            }
        }

        protected void btnSend12_Click(object sender, EventArgs e)
        {
            Response.Redirect("Fire18SignUp02_2.aspx");
        }
    }
}