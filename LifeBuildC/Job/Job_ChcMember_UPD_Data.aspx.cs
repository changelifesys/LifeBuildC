using ADO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Job
{
    public partial class Job_ChcMember_UPD_Data : System.Web.UI.Page
    {
        ChcMemberSub_TempADO memsub = new ChcMemberSub_TempADO();
        ChcMemberADO chcmem = new ChcMemberADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtMemSub = memsub.QueryuptynByChcMemberSub_Temp();
                DataTable dtChcMem = chcmem.QueryAllByChcMember();

                foreach (DataRow dr in dtMemSub.Rows)
                {
                    DataRow[] drChcMem = dtChcMem.Select("GroupName='" + dr["GroupName"].ToString() + "' AND Ename='" + dr["Ename"].ToString() + "'");
                    if (drChcMem.Count() > 0)
                    {
                        //小組&姓名若填寫正確
                        //更新其他欄位資料

                    }
                    else
                    {
                        drChcMem = null;
                        drChcMem = dtChcMem.Select("Ename='" + dr["Ename"].ToString() + "' AND Phone='" + dr["Phone"].ToString() + "'");

                        if (drChcMem.Count() > 0)
                        {
                            //姓名 & 手機若填寫正確
                            //更新其他欄位資料

                        }
                        else
                        { 
                            //新增資料

                        }


                    }


                }

            }
        }

    }
}