using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeBuildC.PrjMethod
{
    public class PrjDate
    {
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

    }
}