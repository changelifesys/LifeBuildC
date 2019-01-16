using ADO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        /// 狀態C001
        /// </summary>
        public string ClassStatus_C001 = string.Empty;

        #endregion

        #region Get

        //Get

        /// <summary>
        /// 取得小組資料
        /// </summary>
        /// <param name="group">小組(Ex. CA202.信豪牧區-彥伯小組)</param>
        /// <param name="gcroup">組別(家庭組弟兄 || 家庭組姊妹 || 社青 || 學生)</param>
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
        public string GetDayOfWeek(DateTime dtime)
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

        //AA101.永健牧區-永健小組
        public string Get_group(string GroupCName, string GroupName, string GroupClass)
        {
            ChcGroupADO _ChcGroupADO = new ChcGroupADO();
            DataTable dt = _ChcGroupADO.QueryChcGroupByCondition(GroupCName, GroupName, GroupClass);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["GroupID"].ToString() + "." +
                             dt.Rows[0]["GroupCName"].ToString() + "-" +
                             dt.Rows[0]["GroupName"].ToString();
            }
            else
            {
                return "";
            }


        }

        #endregion

        #region Check

        //Check

        public int CheckIntByRequest(string Request_Name, int initial_value)
        {
            string _value = initial_value.ToString();
            if (HttpContext.Current.Request[Request_Name] != null && HttpContext.Current.Request[Request_Name].ToString().Length > 0)
                _value = HttpContext.Current.Request[Request_Name].ToString();

            int _IntValue = initial_value;
            if (CheckInt(_value)) //只能輸入數字
                _IntValue = int.Parse(_value); //是數字

            return _IntValue;
        }

        /// <summary>
        /// Int資料格式檢查
        /// </summary>
        /// <param name="IntData">Int資料</param>
        /// <returns>True:驗證通過;Fals:驗證失敗</returns>
        public bool CheckInt(string IntData)
        {
            int _data = 0;
            int.TryParse(IntData, out _data);

            if (_data > 0)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Class

        //Class



        #endregion

    }
}
