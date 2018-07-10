using ADO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeBuildC.Class
{
    public class ChcGroupClass
    {
        ChcGroupADO chcgroup = new ChcGroupADO();

        public string GetJsonByChcGroup()
        {
            PageData PageData = new PageData();
            DataTable dt = chcgroup.Query_ChcGroup_GroupClass();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataInfo dinfo = new DataInfo();
                    dinfo.group = dr["GroupClass"].ToString();

                    DataTable dtlist = chcgroup.Query_ChcGroup_GSort_GroupClass(dr["GroupClass"].ToString());
                    foreach (DataRow drlist in dtlist.Rows)
                    {
                        string _GroupID = drlist["GroupID"].ToString();
                        string _GroupCName = drlist["GroupCName"].ToString();
                        string _GroupName = drlist["GroupName"].ToString();

                        //出輸格式
                        //AA101.永健牧區-永健小組
                        dinfo.list.Add(_GroupID + "." + _GroupCName + "-" + _GroupName);
                    }

                    PageData.DataInfo.Add(dinfo);
                }


            }

            return JsonConvert.SerializeObject(PageData);

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