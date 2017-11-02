using ADO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static LifeBuildC.Api.GetAnsItem;

namespace LifeBuildC.Admin
{
    public partial class ExamScore : System.Web.UI.Page
    {
        UserScoreADO score = new UserScoreADO();

        public string tbscore;

        protected void Page_Load(object sender, EventArgs e)
        {

            tbscore =
"{" +
"\"DataInfo\": [" +
  "{" +
     "\"ExamCategory\": \"C1\"," +
     "\"Egroup\": \"AAA\"," +
     "\"Ename\": \"dennis\"," +
     "\"Emobile\": \"0919xxxx\"," +
     "\"EScore\": \"100\"," +
     "\"CreateDate\": \"2017/03/17 00:00:00\"" +
  "}," +
  "{" +
      "\"ExamCategory\": \"C212\"," +
      "\"Egroup\": \"BBB\"," +
      "\"Ename\": \"mary\"," +
      "\"Emobile\": \"0928xxxx\"," +
      "\"EScore\": \"90\"," +
      "\"CreateDate\": \"2017/03/16 00:00:00\"" +
    "}" +
  "]" +
"}";
            PageData PageData = new PageData();
            DataTable dt = score.QueryAllUserScore();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ScoreData ScoreData = new ScoreData();
                    ScoreData.USID = dr["USID"].ToString();
                    ScoreData.ExamCategory = dr["ExamCategory"].ToString();
                    ScoreData.Egroup = dr["Egroup"].ToString();
                    ScoreData.Ename = dr["Ename"].ToString();
                    ScoreData.Emobile = dr["Emobile"].ToString();
                    ScoreData.EScore = dr["EScore"].ToString();
                    ScoreData.CreateDate = dr["CreateDate"].ToString();
                    PageData.DataInfo.Add(ScoreData);

                }
            }


            #region 取得考試日期

            DataTable dtexamdate = score.Getexamdate();
            if (dtexamdate != null && dtexamdate.Rows.Count > 0)
            {
                foreach (DataRow dr in dtexamdate.Rows)
                {
                    ExamDate ExamDate = new ExamDate();
                    ExamDate.Edate = dr["CreateDate"].ToString();
                    PageData.ExamDate.Add(ExamDate);
                }


            }

            #endregion

            tbscore = JsonConvert.SerializeObject(PageData);
            //Response.Write(JsonConvert.SerializeObject(PageData));

        }

        public class PageData
        {
            public List<ExamDate> ExamDate = new List<ExamDate>();
            public List<ScoreData> DataInfo = new List<ScoreData>();
        }

        public class ScoreData
        {
            public string USID;
            public string ExamCategory;
            public string Egroup;
            public string Ename;
            public string Emobile;
            public string EScore;
            public string CreateDate;
        }

        public class ExamDate
        {
            public string Edate;
        }

    }
}