/*
 Page中使用的API：
 GetSUBInfo.aspx -> 取得課程資訊
 GetGroupItem.aspx -> 小組選項
 ChkMember.aspx -> 更新會友基本資料
 AddSubSign.aspx -> 報名
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC
{
    public partial class SubjectSignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["id"] != null)
            {
                switch (Request.QueryString["id"].ToString().ToUpper())
                {
                    case "C1":
                        Response.Redirect("http://form.changelifesys.org/#/registration/c1");
                        break;
                    case "C2":
                        Response.Redirect("http://form.changelifesys.org/#/registration/c2");
                        break;
                    default:
                        Response.Redirect("http://form.changelifesys.org/#/registration/");
                        break;
                }


            }


        }
    }
}