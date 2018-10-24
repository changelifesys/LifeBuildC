using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC
{
    public partial class SubjectCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                switch (Request.QueryString["id"].ToString().ToUpper())
                {
                    case "C1":
                        Response.Redirect("http://form.changelifesys.org/signin/c1");
                        break;
                    case "C2":
                        Response.Redirect("http://form.changelifesys.org/signin/c2");
                        break;
                    default:
                        Response.Redirect("http://form.changelifesys.org/");
                        break;
                }


            }
        }
    }
}