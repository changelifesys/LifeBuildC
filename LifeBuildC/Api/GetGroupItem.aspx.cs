/*
小組的下拉選單
 */
using ADO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class GetGroupItem : System.Web.UI.Page
    {
        ApiData api = new ApiData();
        ChcGroupADO chcgroup = new ChcGroupADO();

        DataTable dtGroupClass = new DataTable();
        DataTable dtGroup = new DataTable();

        /// <summary>
        /// Receive 字串
        /// </summary>
        string ReceiveStr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["test"] != null && Request.QueryString["test"].ToString() == "true")
            {
                api.CategoryID = "C1";
            }
            else
            {
                using (Stream receiveStream = Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        ReceiveStr = readStream.ReadToEnd();
                    }
                }

                try
                { //有傳參數
                    api = JsonConvert.DeserializeObject<ApiData>(ReceiveStr);
                }
                catch
                { //沒有傳參數
                    api = null;
                }
            }

            if (api == null)
            { //沒有傳參數
                api = new ApiData();
                dtGroup = chcgroup.Query_ChcGroup_GSort();
                dtGroupClass = chcgroup.Query_ChcGroup_GroupClass();
            }
            else
            { //有傳參數
                dtGroup = chcgroup.Query_ChcGroup_CategoryID(api.CategoryID);
                dtGroupClass = chcgroup.Query_ChcGroup_CategoryID_GSort(api.CategoryID);
            }

            if (dtGroupClass != null && dtGroupClass.Rows.Count > 0)
            {
                foreach (DataRow dr in dtGroupClass.Rows)
                {
                    DataInfo dinfo = new DataInfo();
                    dinfo.group = dr["GroupClass"].ToString();

                    //DataTable dtlist = chcgroup.Query_ChcGroup_GSort_GroupClass(dr["GroupClass"].ToString());
                    DataRow[] drGroup = dtGroup.Select("GroupClass='" + dr["GroupClass"].ToString() + "'");
                    foreach (DataRow drlist in drGroup)
                    {
                        string _GroupID = drlist["GroupID"].ToString();
                        string _GroupCName = drlist["GroupCName"].ToString();
                        string _GroupName = drlist["GroupName"].ToString();

                        //出輸格式
                        //AA101.永健牧區-永健小組
                        dinfo.list.Add(_GroupID + "." + _GroupCName + "-" + _GroupName);
                    }

                    api.DataInfo.Add(dinfo);
                }

                Response.Write(JsonConvert.SerializeObject(api));
            }
        }

        public class ApiData
        {
            /// <summary>
            ///  課程類別
            /// </summary>
            public string CategoryID { get; set; }
            /// <summary>
            /// 小組資料
            /// </summary>
            public List<DataInfo> DataInfo = new List<DataInfo>();
        }

        public class DataInfo
        {
            /// <summary>
            /// 小組
            /// </summary>
            public string group;
            /// <summary>
            /// 小組 List
            /// </summary>
            public List<string> list = new List<string>();
        }

    }
}