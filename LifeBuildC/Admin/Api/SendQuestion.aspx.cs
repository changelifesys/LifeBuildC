using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin.Api
{
    public partial class SendQuestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Form["GroupClass"] != null &&
                    Request.Form["GroupName"] != null &&
                    Request.Form["CategoryName"] != null &&
                    Request.Form["Email"] != null &&
                    Request.Form["SubLine"] != null &&
                    Request.Form["Question"] != null)
                {
                    PageData PageData = new PageData();

                    string GroupClass = Request.Form["GroupClass"].ToString();
                    string GroupName = Request.Form["GroupName"].ToString();
                    string CategoryName = Request.Form["CategoryName"].ToString();
                    string Email = Request.Form["Email"].ToString();
                    string SubLine = Request.Form["SubLine"].ToString(); //主旨
                    string Question = Request.Form["Question"].ToString(); //問題描述

                    //主旨
                    SubLine = "[" + GroupClass + GroupName + "]" + "關於 " + CategoryName + " 問題：" + SubLine;

                    //問題描述
                    Question += "<p/><label style='color: red'>寄件人Mail：" + Email + "</label>";

                    SendEmail("changelifesys@gmail.com", SubLine, Question);
                    PageData.IsPageError = false;
                    Response.Write(JsonConvert.SerializeObject(PageData));

                }
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
            /// API 訊息
            /// </summary>
            public string ApiMsg { get; set; }
            /// <summary>
            /// 頁面是否有錯(True: 正常; False: 有錯)
            /// </summary>
            public bool IsPageError { get; set; }
        }

    }
}