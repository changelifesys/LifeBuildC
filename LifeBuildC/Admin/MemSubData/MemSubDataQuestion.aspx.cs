using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin.MemSubData
{
    public partial class MemSubDataQuestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Form["hidGroupClass"] != null)
                {
                    lblGroupClass.Text = Request.Form["hidGroupClass"].ToString();
                }

                if (Request.Form["hidGroupName"] != null)
                {
                    lblGroupName.Text = Request.Form["hidGroupName"].ToString();
                }

                if (Request.Form["hidEname"] != null)
                {
                    lblEname.Text = Request.Form["hidEname"].ToString();
                }

            }
        }
    }
}