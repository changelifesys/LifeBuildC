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

        #region Member

        /// <summary>
        /// Get HTTP POST JSON DATA
        /// </summary>
        public string strJsonData = string.Empty;
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
        /// Google表單名稱
        /// </summary>
        public string sheetName = string.Empty;
        /// <summary>
        /// Google表單ID
        /// </summary>
        public string spreadsheetId = string.Empty;


        #endregion

        #region Get

        //Get

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

        /// <summary>
        /// 取得星期
        /// </summary>
        /// <param name="dtime">傳入日期 ex. 2018/08/12</param>
        private string GetDayOfWeek(DateTime dtime)
        {
            switch (dtime.DayOfWeek.ToString("d"))
            {
                case "0":
                    return "日";
                case "1":
                    return "一";
                case "2":
                    return "二";
                case "3":
                    return "三";
                case "4":
                    return "四";
                case "5":
                    return "五";
                case "6":
                    return "六";
                default:
                    return "";
            }
        }

        #endregion



    }
}
