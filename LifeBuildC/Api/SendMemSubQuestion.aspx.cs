/*
 生命建造成績查詢 - 問題反應
 - 寄發Mail
 */
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class SendMemSubQuestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageData PageData = new PageData();
                string strSendMemSubQuestion = string.Empty;

                if (Request.QueryString["test"] != null &&
                    Request.QueryString["test"].ToString() == "1")
                { //測試電文

                    PageData.gcroup = "社青";
                    PageData.group = "CA202.信豪牧區-彥伯小組";
                    PageData.Ename = "流大丹";
                    PageData.QuestionText = HttpContext.Current.Server.HtmlEncode("<font style='color: red;'>問題反應</font><br/>問題反應<br/>問題反應<br/>問題反應<br/>");
                    PageData.CategoryName = "C1,C2";
                    PageData.SubjectLine = "主旨";
                    PageData.Gmail = "dennis866@gmail.com";
                }
                else
                {
                    using (Stream receiveStream = Request.InputStream)
                    {
                        using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                        {
                            strSendMemSubQuestion = readStream.ReadToEnd();
                        }
                    }

                    PageData = JsonConvert.DeserializeObject<PageData>(strSendMemSubQuestion);
                }

                if (PageData != null)
                {
                    try
                    {
                        //主旨
                        PageData.SubjectLine = "[" + PageData.gcroup + PageData.group + "]" + "關於 " + PageData.CategoryName + " 問題：" + PageData.SubjectLine;

                        //問題描述
                        PageData.QuestionText = HttpContext.Current.Server.HtmlDecode(PageData.QuestionText);
                        PageData.QuestionText += "<p/><label style='color: red'>寄件人Mail：" + PageData.Gmail + "</label>";

                        SendEmail("changelifesys@gmail.com", PageData.SubjectLine, PageData.QuestionText);

                        PageData.QuestionText = HttpContext.Current.Server.HtmlEncode(PageData.QuestionText);
                        PageData.ApiMsg = "您的問題已經反應給中央同工，待問題處理後會回覆至您的Mail，謝謝";
                    }
                    catch
                    {
                        PageData.ApiMsg = "網路斷線或填寫的資料內容有誤，請重新填寫資料反應您的問題";
                    }

                }


                Response.Write(JsonConvert.SerializeObject(PageData));

            }
        }

        /// <summary>
        /// 寄發Mail
        /// </summary>
        /// <param name="emailTo">收信方email</param>
        /// <param name="subject">主旨</param>
        /// <param name="body">HTML內容</param>
        public void SendEmail(string emailTo, string subject, string body)
        {
            //設定smtp主機
            string smtpAddress = "smtp.gmail.com";
            //設定Port
            int portNumber = 587;
            bool enableSSL = true;
            //填入寄送方email和密碼
            string emailFrom = "changelifesys@gmail.com";
            string password = "@Chc1225";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                // 若你的內容是HTML格式，則為True
                mail.IsBodyHtml = true;

                //夾帶檔案
                //mail.Attachments.Add(new Attachment(System.Web.HttpContext.Current.Server.MapPath("/img/fire02.jpg")));
                //mail.Attachments.Add(new Attachment(System.Web.HttpContext.Current.Server.MapPath("/img/fire01.png")));

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
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
            /// 問題反應
            /// </summary>
            public string QuestionText { get; set; }
            /// <summary>
            /// 主旨類別
            /// </summary>
            public string CategoryName { get; set; }
            /// <summary>
            /// 主旨
            /// </summary>
            public string SubjectLine { get; set; }
            /// <summary>
            /// API 訊息
            /// </summary>
            public string ApiMsg { get; set; }
            /// <summary>
            /// Mail
            /// </summary>
            public string Gmail { get; set; }
        }

    }
}