﻿using ADO;
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
    public partial class GetSUBInfo : System.Web.UI.Page
    {
        SubjectInfoADO SubjectInfo = new SubjectInfoADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            string ReqGetSUBInfo =
            @"
            {
              ""Category_ID"" : ""C1""
            }
            ";

            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    ReqGetSUBInfo = readStream.ReadToEnd();
                }
            }

            PageData PageData = JsonConvert.DeserializeObject<PageData>(ReqGetSUBInfo);

            DataTable dtSubject = SubjectInfo.GetSubjectInfo(DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd"), PageData.Category_ID);
            if (dtSubject.Rows.Count > 0)
            {
                PageData.S_ID = int.Parse(dtSubject.Rows[0]["SID"].ToString());
                PageData.SBName = dtSubject.Rows[0]["SubName"].ToString();
                PageData.SUC = dtSubject.Rows[0]["SUCondition"].ToString();

                //2/12(日)、 2/19(日)下午14:30~17:30
                string _SBDate = "";
                foreach (DataRow dr in dtSubject.Rows)
                {
                    _SBDate += dr["SDate"].ToString().Replace(DateTime.UtcNow.AddHours(8).Year.ToString() + "/", "") + 
                        "(" + GetDayOfWeek(DateTime.Parse(dr["SDate"].ToString())) + ")、";
                }

                PageData.SBDate = _SBDate.Substring(0, _SBDate.Length - 1) + " " + dtSubject.Rows[0]["SubTime"].ToString();
                PageData.SBL = dtSubject.Rows[0]["SubLocation"].ToString();

                //即日起~2/9(四)截止報名，之後請現場報名。
                PageData.SUDate = "即日起~" +
                    dtSubject.Rows[0]["SubEndDate"].ToString().Replace(DateTime.UtcNow.AddHours(8).Year.ToString() + "/", "") + 
                        "(" + GetDayOfWeek(DateTime.Parse(dtSubject.Rows[0]["SubEndDate"].ToString())) + ") " +
                    "截止報名，之後請現場報名。";
            }
            else
            {
                PageData.ErrMsg = "尚未開放" + PageData.Category_ID + "報名時間";
            }

            PageData.Church.Add("江子翠行道會會友");
            PageData.Church.Add("新莊幸福行道會會友");

            PageData.MakeUp.Add("我不需要補課");
            PageData.MakeUp.Add("我要補一、二課");
            PageData.MakeUp.Add("我要補三、四課");

            Response.Write(JsonConvert.SerializeObject(PageData));


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

        public class PageData
        {
            public int S_ID { get; set; } //SID
            public string SBName { get; set; } //SubName
            public string SUC { get; set; } //SUCondition
            public string SBDate { get; set; } //
            public string SBL { get; set; } //SubLocation
            public string SUDate { get; set; } //
            public List<string> Church = new List<string>();
            public List<string> MakeUp = new List<string>();
            public string ErrMsg { get; set; }
            public string Category_ID { get; set; }
        }

    }
}