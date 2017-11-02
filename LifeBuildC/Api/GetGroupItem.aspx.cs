﻿using ADO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class GetGroupItem : System.Web.UI.Page
    {
        //取得區牧小組群組

        ChcGroupADO chcgroup = new ChcGroupADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageStart();
        }

        private void PageStart()
        {
            PageData PageData = new PageData();
            DataTable dt = chcgroup.QueryGroupByChcGroup();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataInfo dinfo = new DataInfo();
                    dinfo.group = dr["GroupClass"].ToString();

                    DataTable dtlist = chcgroup.QueryGroupNameByChcGroup(dr["GroupClass"].ToString());
                    foreach (DataRow drlist in dtlist.Rows)
                    {
                        dinfo.list.Add(drlist["GroupName"].ToString());
                    }

                    PageData.DataInfo.Add(dinfo);
                }

                Response.Write(JsonConvert.SerializeObject(PageData));
            }

        }

        public class PageData
        {
            public List<DataInfo> DataInfo = new List<DataInfo>();
        }

        public class DataInfo
        {
            public string group;
            public List<string> list = new List<string>();
        }

    }
}