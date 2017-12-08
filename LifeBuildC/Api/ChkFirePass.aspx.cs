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
    public partial class ChkFirePass : System.Web.UI.Page
    {
        FireMemberADO fireMem = new FireMemberADO();
        FirePassWADO firePass = new FirePassWADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Test 電文

                string strChkFirePass =
                @" 
                {
                   FirePass: ""0cHzHN""
                }
                ";

                #endregion

                using (Stream receiveStream = Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        strChkFirePass = readStream.ReadToEnd();
                    }
                }

                PageData PageData = JsonConvert.DeserializeObject<PageData>(strChkFirePass);

                if (firePass.CheckPassKey(PageData.FirePass))
                {
                    #region 密碼輸入正確

                    PageData.IsPass = true;
                    PageData.ChkMsg = "";

                    DataTable dt = fireMem.GetFireMemberWherePassKey(PageData.FirePass);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //修改表單
                        PageData.IsMemData = true;
                        PageData.gcroup = dt.Rows[0]["GroupClass"].ToString(); //社青
                        PageData.group = dt.Rows[0]["group2"].ToString(); //CA202.信豪牧區-彥伯小組
                        PageData.Ename = dt.Rows[0]["Ename"].ToString(); //流大丹
                        PageData.Phone = dt.Rows[0]["Phone"].ToString(); //0919963322
                        PageData.Gmail = dt.Rows[0]["Gmail"].ToString(); //dennis866@gmail.com
                        PageData.gender = dt.Rows[0]["gender2"].ToString(); //男生
                        PageData.ClothesSize = dt.Rows[0]["ClothesSize"].ToString(); //S
                        PageData.Course = dt.Rows[0]["Course2"].ToString(); //生命突破
                        PageData.Birthday = DateTime.Parse(dt.Rows[0]["Birthday"].ToString()).ToString("yyyy/MM/dd"); //生日


                    }
                    else
                    { 
                        //新表單
                        PageData.IsMemData = false;
                        PageData.gcroup = "";
                        PageData.group = "";
                        PageData.Ename = "";
                        PageData.Phone = "";
                        PageData.Gmail = "";
                        PageData.gender = "";
                        PageData.ClothesSize = "";
                        PageData.Course = "";
                        PageData.Birthday = "";

                    }

                    #endregion
                }
                else
                {
                    PageData.IsPass = false;
                    PageData.ChkMsg = "請輸入正確的密碼!";
                }

                Response.Write(JsonConvert.SerializeObject(PageData));
            }
        }

        public class PageData
        {
            //傳入的密碼
            public string FirePass { get; set; }
            //True: 通過驗證
            //False: 沒有通過驗證
            public bool IsPass { get; set; }
            //回傳的訊息
            public string ChkMsg { get; set; }

            //True: 有會友資料
            //False: 沒有會友資料
            public bool IsMemData { get; set; }

            //社青
            public string gcroup { get; set; }
            //CA202.信豪牧區-彥伯小組
            public string group { get; set; }
            //流大丹
            public string Ename { get; set; }
            //0919963322
            public string Phone { get; set; }
            //dennis866@gmail.com
            public string Gmail { get; set; }
            //男生
            public string gender { get; set; }
            //S
            public string ClothesSize { get; set; }
            //生命突破
            public string Course { get; set; }
            //生日
            public string Birthday { get; set; }

        }

    }
}