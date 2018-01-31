using ADO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Tool
{
    public partial class Tool001 : System.Web.UI.Page
    {
        CLC_FinancialSys_ID_Temp_1ADO temp1 = new CLC_FinancialSys_ID_Temp_1ADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = temp1.QueryCLC_FinancialSys_ID_Temp_1();


        }
    }
}