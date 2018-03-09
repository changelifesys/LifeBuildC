/*
 用途：
   生命建造-課程報名

 流程：
   [View]SubjectSignUp.aspx?id=c1 > 報名 > [API]AddSubSign

 API演算法：
   

 */
using ADO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class AddSubSign : System.Web.UI.Page
    {
        SubjectInfoADO subjectinfo = new SubjectInfoADO();
        ChcMemberSub_TempADO chcmembersubtemp = new ChcMemberSub_TempADO();
        ChcMemberADO chcmember = new ChcMemberADO();

        PageData pgdata = new PageData();
        string strAddSubSign = string.Empty;
        string GroupCName = string.Empty;
        string GroupName = string.Empty;
        string GroupClass = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageStart();
            }

        }

        private void PageStart()
        {
            if (Request.QueryString["test"] != null &&
                Request.QueryString["test"].ToString() == "1")
            {
                pgdata.S_ID = 1;
                pgdata.Category_ID = "C2";
                pgdata.gcroup = "社青";
                pgdata.group = "CA202.信豪牧區-彥伯小組";
                pgdata.Ename = "劉彥伯";
                pgdata.Phone = "0919963322";
                pgdata.Gmail = "dennis866@gmail.com";
                pgdata.Church = "江子翠行道會";
            }
            else
            {
                using (Stream receiveStream = Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        strAddSubSign = readStream.ReadToEnd();
                    }
                }

                pgdata = JsonConvert.DeserializeObject<PageData>(strAddSubSign);
            }

            if (pgdata != null)
            {
                try
                {
                    pgdata.IsApiError = false;

                    //小組
                    string[] arrg = pgdata.group.Split('.');
                    GroupCName = arrg[1].Split('-')[0];
                    GroupName = arrg[1].Split('-')[1];
                    GroupClass = pgdata.gcroup;

                    if (pgdata.Category_ID == "C1")
                    { //C1課程沒有限制報名資格

                        InsChcMemberSub_Temp();
                        pgdata.ApiMsg = "C1 課程報名成功！";

                    }
                    else if (pgdata.Category_ID == "C2")
                    { //C2課程需上完C1才可報名

                        DataTable dtMem = chcmember.GetChcMemberByGroup(GroupCName, GroupName, pgdata.Ename);
                        if (dtMem != null && dtMem.Rows.Count > 0)
                        {
                            bool IsC112 = bool.Parse(dtMem.Rows[0]["IsC112"].ToString());
                            bool IsC134 = bool.Parse(dtMem.Rows[0]["IsC134"].ToString());
                            if (IsC112 && IsC134)
                            {
                                InsChcMemberSub_Temp();
                                pgdata.ApiMsg = "C2 課程報名成功！";
                            }
                            else
                            {
                                pgdata.IsApiError = true;
                                pgdata.ApiMsg = "您沒有符合上C2的資格！請向小組長或區長確認，或是自行查詢是否C1課程尚未完成";
                            }


                        }
                        else
                        {
                            pgdata.IsApiError = true;
                            pgdata.ApiMsg = "您沒有符合上C2的資格！請向小組長或區長確認，或是自行查詢是否C1課程尚未完成";
                        }

                    }


                }
                catch (Exception e)
                {
                    pgdata.IsApiError = true;
                    pgdata.ApiMsg = "網路斷線或填寫的資料內容有誤！請重新填寫報名資料";
                }

            }

            Response.Write(JsonConvert.SerializeObject(pgdata));

        }

        private void InsChcMemberSub_Temp()
        {
            DataTable dtSub = subjectinfo.GetSubjectDateBySubjectInfo(pgdata.S_ID);
            foreach (DataRow drSub in dtSub.Rows)
            {
                chcmembersubtemp.InsChcMemberSub_Temp_2(pgdata.S_ID, drSub["CategoryID2"].ToString(), GroupCName, GroupName, GroupClass,
                     pgdata.Ename, pgdata.Phone, pgdata.Gmail, pgdata.Church, "0", DateTime.Parse(drSub["SDate"].ToString()), "");
            }
        }

        /// <summary>
        /// API參數
        /// </summary>
        public class PageData
        {
            /// <summary>
            /// 課程ID(SubjectInfo.SID)
            /// </summary>
            public int S_ID { get; set; }
            /// <summary>
            /// 課程類別(ChcMemberSub_Temp.CategoryID)
            /// </summary>
            public string Category_ID { get; set; }
            /// <summary>
            /// 組別(ChcMemberSub_Temp.GroupClass)
            /// </summary>
            public string gcroup { get; set; }
            /// <summary>
            /// 小組(ChcMemberSub_Temp.GroupCName+ChcMemberSub_Temp.GroupName)
            /// </summary>
            public string group { get; set; }
            /// <summary>
            /// 姓名(ChcMemberSub_Temp.Ename)
            /// </summary>
            public string Ename { get; set; }
            /// <summary>
            /// 手機(ChcMemberSub_Temp.Phone)
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// Email(ChcMemberSub_Temp.Gmail)
            /// </summary>
            public string Gmail { get; set; }
            /// <summary>
            /// 所屬教會(ChcMemberSub_Temp.Church)
            /// </summary>
            public string Church { get; set; }
            /// <summary>
            /// API 訊息
            /// </summary>
            public string ApiMsg { get; set; }
            /// <summary>
            /// API 有錯(true: 有錯; false: 沒有錯)
            /// </summary>
            public bool IsApiError { get; set; }
        }

    }
}