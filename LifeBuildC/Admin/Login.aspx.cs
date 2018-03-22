using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Login"] = null;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string Acc = txtAcc.Text.Trim();
            string Pass = txtPass.Text.Trim();
            bool IsCheck = false;

            if (Acc == "hsinhao" && Pass == "@1225")
            {
                IsCheck = true;
            }
            else if (Acc == "yujen" && Pass == "@1225")
            {
                IsCheck = true;
            }
            else if (Acc == "clc1225" && Pass == "@1225")
            {
                IsCheck = true;
            }

            if (IsCheck)
            {
                Session["Login"] = "ok";
                Response.Redirect("MemSubData/MemSubDataList.aspx");
            }

        }
    }
}