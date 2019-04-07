using ADO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
                if (Request.QueryString["gc"] != null && Request.QueryString["gc"].ToString() != "")
                {

                    string GroupClass = Request.QueryString["gc"].ToString();
                    switch (GroupClass)
                    {
                        case "L": //成人報名
                            btnSend.Visible = true;
                            btnStudent.Visible = false;
                            btnChild.Visible = false;
                            break;
                        case "S": //學青報名
                            btnSend.Visible = false;
                            btnStudent.Visible = true;
                            btnChild.Visible = false;
                            break;
                        case "C": //12歲以下報名(含12歲)
                            btnSend.Visible = false;
                            btnStudent.Visible = false;
                            btnChild.Visible = true;
                            break;
                    }

                }
                else
                {
                    Response.Write("<script>alert('請重新掃QRCode報名');</script>");
                }
            }
        }

    }
}