/*
 用途：
   生命建造-線上查詢 (個人查詢)

 流程：
   [View]MemSubQuery > 繼續 > [API]GetGroupByEname > 選擇小組 > [API]GetClassByGroup 帶入組別

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
    public partial class GetClassByGroup : System.Web.UI.Page
    {
        ChcMemberADO chcmember = new ChcMemberADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageStart();
        }

        private void PageStart()
        {
            PageData PageData = new PageData();
            string strGetClassByGroup = string.Empty;

            if (Request.QueryString["test"] != null &&
                Request.QueryString["test"].ToString() == "1")
            {
                PageData.group = "CA202.信豪牧區-彥伯小組";
                PageData.ApiMsg = "";
                PageData.IsApiError = false;
            }
            else
            {
                using (Stream receiveStream = Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        strGetClassByGroup = readStream.ReadToEnd();
                    }
                }

                PageData = JsonConvert.DeserializeObject<PageData>(strGetClassByGroup);
            }

            if (PageData != null)
            {
                PageData.IsApiError = false;

                try
                {
                    //小組
                    string GroupCName = string.Empty;
                    string GroupName = string.Empty;
                    try
                    {
                        string[] arrg = PageData.group.Split('.');
                        GroupCName = arrg[1].Split('-')[0];
                        GroupName = arrg[1].Split('-')[1];
                    }
                    catch
                    {
                        GroupCName = "";
                        GroupName = "";
                    }

                    DataTable dtGroup = chcmember.QueryChcMemberByGroupName(GroupName);
                    if (dtGroup != null && dtGroup.Rows.Count > 0)
                    {
                        PageData.gcroup = dtGroup.Rows[0]["GroupClass"].ToString();
                    }
                }
                catch
                {
                    PageData.IsApiError = true;
                    PageData.ApiMsg = "網路斷線或資料內容有誤，請拍照錯誤訊息Mail中央同工反應您的問題";
                }

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