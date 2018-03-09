/*
 用途：
   生命建造-線上查詢 

 流程：
   [View]MemSubQuery > 查詢 > [API]GetMemSubData

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
    public partial class GetMemSubData : System.Web.UI.Page
    {
        ChcMemberADO chcmember = new ChcMemberADO();
        ClassStatusADO cstatus = new ClassStatusADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageStart();
        }

        private void PageStart()
        {
            #region Test 電文

            string strGetMemSubData =
            @" 
                {
                   ""gcroup"": ""社青"",
                   ""group"": ""CA202.信豪牧區-彥伯小組"",
                   ""Ename"": ""流大丹"",
                   ""IsC112"": ""true"",
                   ""TxtC112"": ""已上課"",
                   ""IsC134"": ""true"",
                   ""TxtC134"": ""已上課"",
                   ""IsC1_Score"": ""true"",
                   ""TxtC1_Score"": ""100分"",
                   ""IsC212"": ""true"",
                   ""TxtIsC212"": ""已上課"",
                   ""IsC234"": ""true"",
                   ""TxtIsC234"": ""已上課"",
                   ""IsC25"": ""false"",
                   ""TxtIsC25"": ""未上課"",
                   ""Iswitness"": ""false"",
                   ""Txtwitness"": ""未交"",
                   ""IsC212_Score"": ""true"",
                   ""TxtC212_Score"": ""100分"",
                   ""IsC234_Score"": ""false"",
                   ""TxtC234_Score"": ""60分"",
                   ""IsC1_Status"": ""true"",
                   ""TxtC1_Status"": ""C1 判定通過"",
                   ""IsC2_Status"": ""false"",
                   ""TxtC2_Status"": ""C2 判定不通過""
                }
                ";

            #endregion

            strGetMemSubData = "";

            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    strGetMemSubData = readStream.ReadToEnd();
                }
            }

            
            PageData PageData = JsonConvert.DeserializeObject<PageData>(strGetMemSubData);

            string[] arrg = PageData.group.Split('.');
            string GroupCName = arrg[1].Split('-')[0];
            string GroupName = arrg[1].Split('-')[1];

            DataTable dt = chcmember.GetChcMemberByGroup(GroupCName, GroupName, PageData.Ename);
            DataTable dtStatus = cstatus.QueryByClassStatus();
            if (dt != null && dt.Rows.Count > 0)
            {
                #region C1 第一、二課

                //C1 第一、二課
                PageData.IsC112 = bool.Parse(dt.Rows[0]["IsC112"].ToString());

                if (PageData.IsC112)
                {
                    PageData.TxtC112 = "已上課";
                }
                else
                {
                    PageData.TxtC112 = "未上課";
                }

                #endregion

                #region C1 第三、四課

                PageData.IsC134 = bool.Parse(dt.Rows[0]["IsC134"].ToString());

                if (PageData.IsC134)
                {
                    PageData.TxtC134 = "已上課";
                }
                else
                {
                    PageData.TxtC134 = "未上課";
                }

                #endregion

                #region C1 成績

                PageData.TxtC1_Score = dt.Rows[0]["C1_Score"].ToString() + "分";
                if (int.Parse(dt.Rows[0]["C1_Score"].ToString()) >= 70)
                {
                    PageData.IsC1_Score = true;
                }
                else
                {
                    PageData.IsC1_Score = false;
                }

                #endregion

                #region C2 第一、二課

                PageData.IsC212 = bool.Parse(dt.Rows[0]["IsC212"].ToString());
                if (PageData.IsC212)
                {
                    PageData.TxtIsC212 = "已上課";
                }
                else
                {
                    PageData.TxtIsC212 = "未上課";
                }

                #endregion

                #region C2 第三、四課

                PageData.IsC234 = bool.Parse(dt.Rows[0]["IsC234"].ToString());
                if (PageData.IsC234)
                {
                    PageData.TxtIsC234 = "已上課";
                }
                else
                {
                    PageData.TxtIsC234 = "未上課";
                }

                #endregion

                #region C2 第五課

                PageData.IsC25 = bool.Parse(dt.Rows[0]["IsC25"].ToString());
                if (PageData.IsC25)
                {
                    PageData.TxtIsC25 = "已上課";
                }
                else
                {
                    PageData.TxtIsC25 = "未上課";
                }

                #endregion

                #region 見證

                PageData.Iswitness = bool.Parse(dt.Rows[0]["Iswitness"].ToString());
                if (PageData.Iswitness)
                {
                    PageData.Txtwitness = "已交";
                }
                else
                {
                    PageData.Txtwitness = "未交";
                }

                #endregion

                #region C2 一、二課成績

                PageData.TxtC212_Score = dt.Rows[0]["C212_Score"].ToString() + "分";
                if (int.Parse(dt.Rows[0]["C212_Score"].ToString()) >= 70)
                {
                    PageData.IsC212_Score = true;
                }
                else
                {
                    PageData.IsC212_Score = false;
                }

                #endregion

                #region C2 三、四課成績

                PageData.TxtC234_Score = dt.Rows[0]["C234_Score"].ToString() + "分";
                if (int.Parse(dt.Rows[0]["C234_Score"].ToString()) >= 70)
                {
                    PageData.IsC234_Score = true;
                }
                else
                {
                    PageData.IsC234_Score = false;
                }

                #endregion

                #region C1 判定

                PageData.TxtC1_Status = "C1 判定" + dt.Rows[0]["C1_Status"].ToString();

                if (dt.Rows[0]["C1_Status"].ToString() == dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString())
                {
                    PageData.IsC1_Status = true;
                }
                else
                {
                    PageData.IsC1_Status = false;
                }

                #endregion

                #region C2 判定

                PageData.TxtC2_Status = "C2 判定" + dt.Rows[0]["C2_Status"].ToString();

                if (dt.Rows[0]["C2_Status"].ToString() == dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString())
                {
                    PageData.IsC2_Status = true;
                }
                else
                {
                    PageData.IsC2_Status = false;
                }

                #endregion


            }


            Response.Write(JsonConvert.SerializeObject(PageData));
        }

        public class PageData
        {
            /// <summary>
            /// 組別
            /// </summary>
            public string gcroup { get; set; }
            /// <summary>
            /// 小組
            /// </summary>
            public string group { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string Ename { get; set; }
            public bool IsC112 { get; set; }
            public string TxtC112 { get; set; }
            public bool IsC134 { get; set; }
            public string TxtC134 { get; set; }
            public bool IsC1_Score { get; set; }
            public string TxtC1_Score { get; set; }
            public bool IsC212 { get; set; }
            public string TxtIsC212 { get; set; }
            public bool IsC234 { get; set; }
            public string TxtIsC234 { get; set; }
            public bool IsC25 { get; set; }
            public string TxtIsC25 { get; set; }
            public bool Iswitness { get; set; }
            public string Txtwitness { get; set; }
            public bool IsC212_Score { get; set; }
            public string TxtC212_Score { get; set; }
            public bool IsC234_Score { get; set; }
            public string TxtC234_Score { get; set; }
            public bool IsC1_Status { get; set; }
            public string TxtC1_Status { get; set; }
            public bool IsC2_Status { get; set; }
            public string TxtC2_Status { get; set; }
            /// <summary>
            /// API 訊息
            /// </summary>
            public string ApiMsg { get; set; }
        }

    }
}