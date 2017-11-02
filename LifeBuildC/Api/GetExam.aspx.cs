using ADO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// 取得共10格考題
/// </summary>
namespace LifeBuildC.Api
{
    public partial class GetExam : System.Web.UI.Page
    {
        ExamQuestionsADO examq = new ExamQuestionsADO();
        UserScoreADO uscore = new UserScoreADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            PageData PageData = new PageData();

//            string struser = 
//                "{" +
//"\"eclass\": \"C212\"," +
//"\"group\": \"彥伯\"," +
//"\"name\": \"流大丹\"," +
//"\"mobile\": \"0919963322\"}";

            string struser = CheckStringByRequest("user");

            user user = JsonConvert.DeserializeObject<user>(struser);

            #region 檢查是否有重覆考試

            //PageData.IsError = true 表示有重覆考試，失去本次考試資格，並跳訊息提醒。
            //PageData.IsError = false 預設，開放可以考試。

            DataTable dtScore = uscore.QueryUserScore(user.eclass, user.group, user.name, DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd"));
            if (dtScore.Rows.Count > 0)
            { //有重覆考試
                try
                {
                    #region try

                    PageData.IsError = true;

                    if (int.Parse(dtScore.Rows[0]["EScore"].ToString()) >= 70)
                    {
                        PageData.ErrorMessage = "於" + DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd") + " 您已經考過試了，分數為 " + dtScore.Rows[0]["EScore"].ToString() + " 分【及格】";
                    }
                    else
                    {
                        PageData.ErrorMessage = "於" + DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd") + " 您已經考過試了，分數為 " + dtScore.Rows[0]["EScore"].ToString() + " 分【不及格】";
                    }

                    #endregion
                }
                catch
                {
                    PageData.IsError = false;
                    uscore.DelUserScore(user.eclass, user.group, user.name, DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd"));
                }

            }

            #endregion

            #region 檢查是否繼續考下一科

            //若有連續考試，即使 SystemSet.IsEnable = 'False' 也可以考試

            DataTable dtChkExam = uscore.QueryUserScoreNoneExamCategory(user.group, user.name, DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd"));

            #endregion

            #region 檢查 SystemSet.IsEnable

            //SystemSet.IsEnable = 'True' 可以考試
            //SystemSet.IsEnable = 'False' 不可以考試

            DataTable dt = new DataTable();
            if (dtChkExam != null && dtChkExam.Rows.Count > 0)
            { //有連續考試
              //不用檢查 SystemSet.IsEnable
                dt = examq.QueryExamQuestionsByExamToTrue(user.eclass);
            }
            else
            { //初次考試
              //檢查 SystemSet.IsEnable = 'True'
                dt = examq.QueryExamQuestionsByIsEnable(user.eclass);
            }

            #endregion

            #region 隨機取得考試題目

            DataTable dtRandom = dt.Copy();
            dtRandom.Clear();

            List<int> lsRndA = new List<int>(); //將全部考題存入暫存A
            List<int> lsRndB = new List<int>(); //將暫存A隨機排序
            List<int> lsRndExam = new List<int>(); //篩選考題

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lsRndA.Add(i);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Random rnd = new Random();
                int _rvalue = rnd.Next(0, lsRndA.Count);
                lsRndB.Add(lsRndA[_rvalue]);
                lsRndA.Remove(lsRndA[_rvalue]);
            }

            int _FieldCnt = 0; //存放題目格數
            foreach (int i in lsRndB)
            {
                if (_FieldCnt + int.Parse(dt.Rows[i]["FieldCnt"].ToString()) <= 10)
                {
                    lsRndExam.Add(i);
                    _FieldCnt = _FieldCnt + int.Parse(dt.Rows[i]["FieldCnt"].ToString());
                }
                else
                    continue;
            }

            //若題目仍小於10
            //則計算到大於10為止
            if (_FieldCnt < 10)
            {
                foreach (int i in lsRndB)
                {
                    int _cnt = 0;
                    foreach (int j in lsRndExam)
                    {
                        if (i == j)
                            _cnt++;
                    }

                    if (_cnt == 0)
                    {
                        lsRndExam.Add(i);
                        _FieldCnt = _FieldCnt + int.Parse(dt.Rows[i]["FieldCnt"].ToString());

                        if (_FieldCnt > 10)
                            break;

                    }

                }
            }

            if (_FieldCnt != 0)
                PageData.ScoreMessage = "答錯每格扣 " + (100 / _FieldCnt).ToString() + " 分，全錯 0 分";

            int _randcnt = 0;
            foreach (int i in lsRndExam)
            {
                DataRow dr = dtRandom.NewRow();
                dr.ItemArray = dt.Rows[i].ItemArray;
                dtRandom.Rows.InsertAt(dr, _randcnt);
                _randcnt++;
            }

            #endregion

            if (dtRandom != null && dtRandom.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRandom.Rows)
                {
                    #region Add DataInfo

                    DataInfo dinfo = new DataInfo();

                    dinfo.ID = int.Parse(dr["ID"].ToString());

                    if (!bool.Parse(dr["IsField1"].ToString()))
                    {
                        dinfo.IsField1 = dr["Field1"].ToString();
                    }

                    if (!bool.Parse(dr["IsField2"].ToString()))
                    {
                        dinfo.IsField2 = dr["Field2"].ToString();
                    }

                    if (!bool.Parse(dr["IsField3"].ToString()))
                    {
                        dinfo.IsField3 = dr["Field3"].ToString();
                    }

                    if (!bool.Parse(dr["IsField4"].ToString()))
                    {
                        dinfo.IsField4 = dr["Field4"].ToString();
                    }

                    if (!bool.Parse(dr["IsField5"].ToString()))
                    {
                        dinfo.IsField5 = dr["Field5"].ToString();
                    }

                    if (!bool.Parse(dr["IsField6"].ToString()))
                    {
                        dinfo.IsField6 = dr["Field6"].ToString();
                    }

                    if (!bool.Parse(dr["IsField7"].ToString()))
                    {
                        dinfo.IsField7 = dr["Field7"].ToString();
                    }

                    if (!bool.Parse(dr["IsField8"].ToString()))
                    {
                        dinfo.IsField8 = dr["Field8"].ToString();
                    }

                    if (!bool.Parse(dr["IsField9"].ToString()))
                    {
                        dinfo.IsField9 = dr["Field9"].ToString();
                    }

                    if (!bool.Parse(dr["IsField10"].ToString()))
                    {
                        dinfo.IsField10 = dr["Field10"].ToString();
                    }

                    PageData.DataInfo.Add(dinfo);

                    #endregion
                }
                
            }
            else
            {
                PageData.IsError = true;
                PageData.ErrorMessage = "試題內容已經關閉，現在不開放考試";

            }

            Response.Write(JsonConvert.SerializeObject(PageData));

        }

        public class PageData
        {
            public bool IsError = false; //True: 不開放考題 ; False: 開放考題
            public string ErrorMessage; //存放錯誤訊息
            public string ScoreMessage; //每格的分數
            public List<DataInfo> DataInfo = new List<DataInfo>();
        }

        public class DataInfo
        {
            public int ID;
            public string IsField1 = "true";
            public string IsField2 = "true";
            public string IsField3 = "true";
            public string IsField4 = "true";
            public string IsField5 = "true";
            public string IsField6 = "true";
            public string IsField7 = "true";
            public string IsField8 = "true";
            public string IsField9 = "true";
            public string IsField10 = "true";
        }

        public class user
        {
            public string eclass;
            public string group;
            public string name;
            public string mobile;
        }


        /// <summary>
        /// 檢查 Request.Form 傳入的值是否異常，若異常回傳空值。
        /// </summary>
        /// <param name="RequestForm_Name">Request.Form 的名稱</param>
        /// <returns>回傳 Request.Form 為 string 的值</returns>
        private string CheckStringByRequest(string RequestForm_Name)
        {
            string _value = string.Empty;
            if (Request.Form[RequestForm_Name] != null && Request.Form[RequestForm_Name].ToString().Length > 0)
                _value = Request.Form[RequestForm_Name].ToString();

            return _value;
        }

    }
}