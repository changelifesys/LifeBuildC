/*
 生命建造成績查詢 - 見證繳交
 */
using ADO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class UpdWitness : System.Web.UI.Page
    {
        ChcMemberADO member = new ChcMemberADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageData PageData = new PageData();
                string strUpdWitness = string.Empty;

                if (Request.QueryString["test"] != null &&
                    Request.QueryString["test"].ToString() == "1")
                { //測試電文

                    PageData.gcroup = "社青";
                    PageData.group = "CA202.信豪牧區-彥伯小組";
                    PageData.Ename = "流大丹";
                    PageData.witness1 = "我在信主前，是什麼樣的人。";
                    PageData.witness2 = "我是在什麼樣的情況下，遇見耶穌的。";
                    PageData.witness3 = "我是如何信耶穌的。";
                    PageData.witness4 = "信主後，我的生命有那些改變。";
                }
                else
                {
                    using (Stream receiveStream = Request.InputStream)
                    {
                        using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                        {
                            strUpdWitness = readStream.ReadToEnd();
                        }
                    }

                    PageData = JsonConvert.DeserializeObject<PageData>(strUpdWitness);
                }

                if (PageData != null)
                {
                    try
                    {
                        //小組
                        string[] arrg = PageData.group.Split('.');
                        string GroupCName = arrg[1].Split('-')[0];
                        string GroupName = arrg[1].Split('-')[1];
                        string witness = PageData.witness1 + "\n" +
                                                      PageData.witness2 + "\n" +
                                                      PageData.witness3 + "\n" +
                                                      PageData.witness4;

                        //member.UpdWitness(GroupCName, GroupName, PageData.Ename, witness);
                        PageData.ApiMsg = "已收到您見證繳交";
                    }
                    catch
                    {
                        PageData.ApiMsg = "網路斷線或填寫的資料內容有誤，請重新填寫繳交您的見證";
                    }


                }

                Response.Write(JsonConvert.SerializeObject(PageData));

            }
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
            /// <summary>
            /// 見證 - 我在信主前，是什麼樣的人。
            /// </summary>
            public string witness1 { get; set; }
            /// <summary>
            /// 見證 - 我是在什麼樣的情況下，遇見耶穌的。
            /// </summary>
            public string witness2 { get; set; }
            /// <summary>
            /// 見證 - 我是如何信耶穌的。
            /// </summary>
            public string witness3 { get; set; }
            /// <summary>
            /// 見證 - 信主後，我的生命有那些改變。
            /// </summary>
            public string witness4 { get; set; }
            /// <summary>
            /// API 訊息
            /// </summary>
            public string ApiMsg { get; set; }
        }

    }
}