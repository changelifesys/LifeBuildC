﻿/*
 用途：
   生命建造-課程報名&簽到

 API 流程：
   [View]SubjectSignUp.aspx?id=c1 > 頁面資料載入 > [API]GetSUBInfo

   [View]SubjectCheck.aspx?id=c1 > 頁面資料載入 > [API]GetSUBInfo

 */
using ADO;
using libLifeBuildC;
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
    /// <summary>
    /// 課程資訊
    /// </summary>
    public partial class GetSUBInfo : System.Web.UI.Page
    {
        ApiData Api_Data = new ApiData();
        AdoInfo Ado_Info = new AdoInfo();

        public class ApiData
        {
            /// <summary>
            /// 課程類別
            /// </summary>
            public string CategoryID { get; set; }
            /// <summary>
            /// 課程ID
            /// </summary>
            public int SID { get; set; }
            /// <summary>
            /// 課程名稱
            /// </summary>
            public string SubName { get; set; }
            /// <summary>
            /// 報名條件
            /// </summary>
            //public string SUCondition { get; set; }
            /// <summary>
            /// 上課日期
            /// </summary>
            //public string SubDate { get; set; }
            /// <summary>
            /// 上課地點
            /// </summary>
            //public string SubLocation { get; set; }
            /// <summary>
            /// 課程截止報名時間
            /// </summary>
            //public string SubEndDate { get; set; }
            /// <summary>
            /// API 訊息
            /// </summary>
            public string ApiMsg = string.Empty;
            /// <summary>
            /// API 有錯(true: 有錯; false: 沒有錯)
            /// </summary>
            public bool IsApiError = false;
            /// <summary>
            /// HTML 上課資訊
            /// </summary>
            public string HtmlSubDesc { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageStart();
        }

        private void PageStart()
        {
            string strGetSUBInfo = string.Empty;

            if (Request.QueryString["test"] != null &&
                Request.QueryString["test"].ToString() == "true")
            {
                Api_Data.CategoryID = "C1";
            }
            else
            {
                using (Stream receiveStream = Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        strGetSUBInfo = readStream.ReadToEnd();
                    }
                }

                Api_Data = JsonConvert.DeserializeObject<ApiData>(strGetSUBInfo);

            }

            if (Api_Data != null)
            {
                DataTable dtSubject = Ado_Info.SubjectInfo_ADO.GetSubjectInfo(DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd"), Api_Data.CategoryID);
                if (dtSubject != null && dtSubject.Rows.Count > 0)
                {
                    Api_Data.SID = int.Parse(dtSubject.Rows[0]["SID"].ToString());
                    Api_Data.SubName = dtSubject.Rows[0]["SubName"].ToString();
                    //PageData.SUCondition = dtSubject.Rows[0]["SUCondition"].ToString();

                    /*
                    //2/12(日)、 2/19(日)下午14:30~17:30
                    string _SBDate = "";
                    foreach (DataRow dr in dtSubject.Rows)
                    {
                        _SBDate += DateTime.Parse(dr["SDate"].ToString()).ToString("yyyy/MM/dd").Replace(DateTime.UtcNow.AddHours(8).Year.ToString() + "/", "") +
                            "(" + GetDayOfWeek(DateTime.Parse(dr["SDate"].ToString())) + ")、";
                    }

                    PageData.SubDate = _SBDate.Substring(0, _SBDate.Length - 1) + " " + dtSubject.Rows[0]["SubTime"].ToString();
                    PageData.SubLocation = dtSubject.Rows[0]["SubLocation"].ToString();

                    //即日起~2/9(四)截止報名，之後請現場報名。
                    PageData.SubEndDate = "即日起~" +
                        DateTime.Parse(dtSubject.Rows[0]["SubEndDate"].ToString()).ToString("yyyy/MM/dd").Replace(DateTime.UtcNow.AddHours(8).Year.ToString() + "/", "") +
                            "(" + GetDayOfWeek(DateTime.Parse(dtSubject.Rows[0]["SubEndDate"].ToString())) + ") " +
                        "截止報名，之後請現場報名。";
                    */

                    //PageData.HtmlSubDesc = HttpUtility.UrlEncode(dtSubject.Rows[0]["HtmlSubDesc"].ToString());
                    Api_Data.HtmlSubDesc = dtSubject.Rows[0]["HtmlSubDesc"].ToString().Replace("\"", "'");
                }
                else
                {
                    Api_Data.IsApiError = true;
                    Api_Data.ApiMsg = "尚未開放" + Api_Data.CategoryID + "報名時間";
                }

            }

            Response.Write(JsonConvert.SerializeObject(Api_Data));

        }

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



    }
}