using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libLifeBuildC
{
    public class ApiInfo
    {
        /// <summary>
        /// StreamReader電文
        /// </summary>
        public string strStreamReader = string.Empty;
        /// <summary>
        /// 組別
        /// </summary>
        public string GroupClass = string.Empty;
        /// <summary>
        /// 牧區
        /// </summary>
        public string GroupCName = string.Empty;
        /// <summary>
        /// 小組
        /// </summary>
        public string GroupName = string.Empty;

        /// <summary>
        /// 取得小組資料
        /// </summary>
        public void GetGroupData(string group, string gcroup)
        {
            string[] arrg = group.Split('.');
            GroupCName = arrg[1].Split('-')[0];
            GroupName = arrg[1].Split('-')[1];
            GroupClass = gcroup;
        }


    }
}
