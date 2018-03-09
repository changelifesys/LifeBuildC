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
    public partial class GetMemSub : System.Web.UI.Page
    {
        ChcMemberADO member = new ChcMemberADO();
        ClassStatusADO cstatus = new ClassStatusADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageStart();
        }

        private void PageStart()
        {
            #region Json Test 電文

            string strGetMemSub =
            @" 
                {
                  group: ""CA202.信豪牧區-彥伯小組"",
                  Ename: ""流大丹""
                }
                ";

            #endregion

            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    strGetMemSub = readStream.ReadToEnd();
                }
            }

            PageData PageData = JsonConvert.DeserializeObject<PageData>(strGetMemSub);

            //AA101.永健牧區-永健小組
            string[] arrg = PageData.group.Split('.');
            string GroupCName = arrg[1].Split('-')[0];
            string GroupName = arrg[1].Split('-')[1];

            DataTable dt = member.GetChcMemberByGroup(GroupCName, GroupName, PageData.Ename);
            DataTable dtStatus = cstatus.QueryByClassStatus();

            #region 初始值

            PageData.PageTitle = "查詢不到上課資料，請確認輸入的小組、姓名是否正確！";
            PageData.C112 = dtStatus.Select("StatusID='C003'")[0]["ClassStatus"].ToString();
            PageData.C134 = dtStatus.Select("StatusID='C003'")[0]["ClassStatus"].ToString();
            PageData.C212 = dtStatus.Select("StatusID='C003'")[0]["ClassStatus"].ToString();
            PageData.C234 = dtStatus.Select("StatusID='C003'")[0]["ClassStatus"].ToString();
            PageData.C25 = dtStatus.Select("StatusID='C003'")[0]["ClassStatus"].ToString();
            PageData.C1_Score = 0;
            PageData.C212_Score = 0;
            PageData.C234_Score = 0;
            PageData.C1_Status = dtStatus.Select("StatusID='C002'")[0]["ClassStatus"].ToString();
            PageData.C2_Status = dtStatus.Select("StatusID='C002'")[0]["ClassStatus"].ToString();
            PageData.witness = dtStatus.Select("StatusID='C003'")[0]["ClassStatus"].ToString();
            PageData.witnessText = "";

            #endregion

            if (dt != null && dt.Rows.Count > 0)
            {
                PageData.PageTitle = dt.Rows[0]["GroupCName"].ToString() + "-" + dt.Rows[0]["GroupName"].ToString();

                #region C1

                bool chkC1 = true;
                if (bool.Parse(dt.Rows[0]["IsC112"].ToString()))
                {
                    PageData.C112 = dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString();
                }
                else
                {
                    chkC1 = false;
                }

                if (bool.Parse(dt.Rows[0]["IsC134"].ToString()))
                {
                    PageData.C134 = dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString();
                }
                else
                {
                    chkC1 = false;
                }

                PageData.C1_Score = int.Parse(dt.Rows[0]["C1_Score"].ToString());
                if (PageData.C1_Score < 70)
                {
                    chkC1 = false;
                }

                if (chkC1)
                {
                    PageData.C1_Status = dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString();
                }
                else
                {
                    PageData.C1_Status = dtStatus.Select("StatusID='C002'")[0]["ClassStatus"].ToString();
                }

                #endregion

                #region C2

                bool chkC2 = true;

                if (bool.Parse(dt.Rows[0]["IsC212"].ToString()))
                {
                    PageData.C212 = dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString();
                }
                else
                {
                    chkC2 = false;
                }

                if (bool.Parse(dt.Rows[0]["IsC234"].ToString()))
                {
                    PageData.C234 = dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString();
                }
                else
                {
                    chkC2 = false;
                }

                if (bool.Parse(dt.Rows[0]["IsC25"].ToString()))
                {
                    PageData.C25 = dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString();
                }
                else
                {
                    chkC2 = false;
                }

                PageData.C212_Score = int.Parse(dt.Rows[0]["C212_Score"].ToString());
                PageData.C234_Score = int.Parse(dt.Rows[0]["C234_Score"].ToString());

                if (PageData.C212_Score < 70)
                {
                    chkC2 = false;
                }

                if (PageData.C234_Score < 70)
                {
                    chkC2 = false;
                }

                if (bool.Parse(dt.Rows[0]["witness"].ToString()))
                {
                    PageData.witness = dtStatus.Select("StatusID='C004'")[0]["ClassStatus"].ToString();
                }
                else
                {
                    chkC2 = false;
                }

                PageData.witnessText = dt.Rows[0]["witness"].ToString();

                if (chkC2)
                {
                    PageData.C2_Status = dtStatus.Select("StatusID='C001'")[0]["ClassStatus"].ToString();
                }
                else
                {
                    PageData.C2_Status = dtStatus.Select("StatusID='C002'")[0]["ClassStatus"].ToString();
                }

                #endregion
            }

            Response.Write(JsonConvert.SerializeObject(PageData));

        }

        public class PageData
        {
            public string group { get; set; }
            public string Ename { get; set; }
            public string PageTitle { get; set; }
            public string C112 { get; set; }
            public string C134 { get; set; }
            public string C212 { get; set; }
            public string C234 { get; set; }
            public string C25 { get; set; }
            public int C1_Score { get; set; }
            public int C212_Score { get; set; }
            public int C234_Score { get; set; }
            public string C1_Status { get; set; }
            public string C2_Status { get; set; }
            public string witness { get; set; }
            public string witnessText { get; set; }
    }

    }
}