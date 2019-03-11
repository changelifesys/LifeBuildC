using libLifeBuildC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Tool
{
    public partial class UpdUserScore : System.Web.UI.Page
    {
        AdoInfo Ado_Info = new AdoInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dtUserScore = Ado_Info.UserScore_ADO.QueryUserScore();
            DataTable dtMem = Ado_Info.ChcMember_ADO.QueryAllByChcMember();
            foreach (DataRow dr in dtUserScore.Rows)
            {
                string GroupCName = "";
                string GroupName = "";

                DataRow[] drChcMem = null;
                drChcMem = dtMem.Select("GroupCName='" + GroupCName + "' AND GroupName='" + GroupName + "' AND Ename='" + dr["Ename"].ToString() + "'");
            }

        }
    }
}