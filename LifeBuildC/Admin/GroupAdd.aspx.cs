using ADO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin
{
    public partial class GroupAdd : System.Web.UI.Page
    {
        ChcGroupADO chcgroup = new ChcGroupADO();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 儲存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //組別設定
            string _GroupClass = hdenGroupClass.Value.Trim();

            //編號設定(AA101)
            string _GroupID = txtaGroupID.Value.Trim();

            //選項排序設定(要填寫數字)
            DataTable dtGN12 = chcgroup.QueryGroupName12ByChcGroup(_GroupID.Substring(0, 2));
            int GSort = 0;
            if (dtGN12.Rows.Count > 0)
            {
                GSort = int.Parse(dtGN12.Rows[0][0].ToString()) + 1;
            }

            DataTable dtGNum = chcgroup.QueryGSortByChcGroup(GSort);
            foreach (DataRow dr in dtGNum.Rows)
            {

            }

            //牧區設定
            string _Group = txtaGroup.Value.Trim();

            //小組設定
            string GroupName = txtaGroupName.Value.Trim();




        }
    }
}