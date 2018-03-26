/*
 用途：
   生命建造-線上查詢 (個人查詢)

 流程：
   [View]MemSubQuery > 頁面資料載入 > [API]GetGroupByEname

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
    public partial class GetGroupByEname : System.Web.UI.Page
    {
        ChcGroupADO chcGroup = new ChcGroupADO();
        ChcMemberADO chcMember = new ChcMemberADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageStart();
        }

        private void PageStart()
        {
            PageData PageData = new PageData();
            string strGetGroupByEname = string.Empty;

            if (Request.QueryString["test"] != null &&
                Request.QueryString["test"].ToString() == "1")
            {
                PageData.Ename = "劉彥伯";
                PageData.ApiMsg = "";
                PageData.IsApiError = false;
            }
            else
            {
                using (Stream receiveStream = Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        strGetGroupByEname = readStream.ReadToEnd();
                    }
                }

                PageData = JsonConvert.DeserializeObject<PageData>(strGetGroupByEname);

            }

            if (PageData != null)
            {
                PageData.IsApiError = false;

                try
                {
                    DataTable dt = chcMember.QueryEnameByChcMember_1(PageData.Ename);
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dt.Rows)
                        {

                            DataTable dtGroup = chcGroup.QueryChcGroupByCondition(dr["GroupCName"].ToString(),
                                dr["GroupName"].ToString(),
                                dr["GroupClass"].ToString());

                            if (dtGroup != null && dtGroup.Rows.Count > 0)
                            {
                                ChcGroupData ChcGroupData = new ChcGroupData();

                                //出輸格式
                                //AA101.永健牧區-永健小組
                                ChcGroupData.group.Add(dtGroup.Rows[0]["GroupID"].ToString() + "." +
                                    dtGroup.Rows[0]["GroupCName"].ToString() + "-" +
                                    dtGroup.Rows[0]["GroupName"].ToString());

                                PageData.group.Add(ChcGroupData);

                            }
                            else
                            {
                                PageData.IsApiError = true;
                                PageData.ApiMsg = "查無小組資料，請向您的小組長確認小組資料是否正確";
                                break;
                            }

                        }

                    }
                    else
                    {
                        PageData.IsApiError = true;
                        PageData.ApiMsg = "查無小組資料，請向您的小組長確認小組資料是否正確";
                    }

                }
                catch (Exception e)
                {
                    PageData.IsApiError = true;
                    PageData.ApiMsg = "網路斷線或填寫的資料內容有誤，請重新填寫查詢姓名";
                }

            }

            Response.Write(JsonConvert.SerializeObject(PageData));

        }

        public class PageData
        {
            public List<ChcGroupData> group = new List<ChcGroupData>();
            /// <summary>
            /// 姓名
            /// </summary>
            public string Ename { get; set; }
            /// <summary>
            /// API 訊息
            /// </summary>
            public string ApiMsg { get; set; }
            /// <summary>
            /// API 有錯(true: 有錯; false: 沒有錯)
            /// </summary>
            public bool IsApiError { get; set; }

        }

        public class ChcGroupData
        {
            /// <summary>
            /// 小組
            /// </summary>
            public List<string> group = new List<string>();
        }

    }
}