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
    public partial class GetMember : System.Web.UI.Page
    {
        //會友資訊
        ChcMemberADO ChcMember = new ChcMemberADO();
        //小組資訊
        ChcGroupADO ChcGroup = new ChcGroupADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 測試電文

            string ReqGetSubSign =
            @"
            {
              CategoryID: ""C1"",
              Phone: ""0919963321"",
              Ename: """"
            }
            ";

            #endregion

            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    ReqGetSubSign = readStream.ReadToEnd();
                }
            }

            PageData PageData = JsonConvert.DeserializeObject<PageData>(ReqGetSubSign);

            DataTable dtMem = new DataTable();

            if (PageData.Phone != "")
            { //表示輸入的是手機
                dtMem = ChcMember.QueryPhoneByChcMember(PageData.Phone);
            }
            else if (PageData.Ename != "")
            { //表示輸入的是姓名
                dtMem = ChcMember.QueryEnameByChcMember(PageData.Ename);
            }

            
            Member Member = new Member();

            if (dtMem.Rows.Count > 0)
            {
                DataTable dtGroup = ChcGroup.QueryGroupClassByChcGroup(dtMem.Rows[0]["GroupName"].ToString());
                Member.gclass = dtGroup.Rows[0]["GroupClass"].ToString();
                Member.group2 = dtGroup.Rows[0]["GroupName"].ToString();

                Member.MID = int.Parse(dtMem.Rows[0]["MID"].ToString());
                Member.Ename = dtMem.Rows[0]["Ename"].ToString();
                Member.Gmail = dtMem.Rows[0]["Gmail"].ToString();
                Member.Church = dtMem.Rows[0]["Church"].ToString();
                //Member.gclass = dtMem.Rows[0]["GroupClass"].ToString();
                //Member.group2 = dtMem.Rows[0]["GroupName"].ToString();

                //string[] _arrg = dtMem.Rows[0]["GroupName"].ToString().Split('.');
                //string[] arrg = _arrg[1].Split('-');
                //Member.groupc = arrg[0];
                //Member.group = arrg[1];

            }

            try
            {

            }
            catch { }
            
            Response.Write(JsonConvert.SerializeObject(Member));

        }

        public class PageData
        {
            public string Phone { get; set; } //手機
            public string Ename { get; set; }
        }

        public class Member
        {
            public int MID { get; set; }
            public string Ename { get; set; } //姓名
            public string Gmail { get; set; } //Gmail
            public string Church { get; set; } //所屬教會
            public string gclass { get; set; } //組別
            //public string groupc { get; set; } //牧區
            //public string group { get; set; } //小組
            public string group2 { get; set; } //AA101.永健牧區-永健小組

        }

    }
}