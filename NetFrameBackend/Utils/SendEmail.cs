using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.IO;
using System.Web;

namespace NetFrameBackend.Utils
{
    class Email
    {
        string smtpHost = "smtp.qq.com";
        string Sendmailaddress = "2870375520@qq.com";
        string AuthKey = "lrgfomoliqwtdfia";
        string SendDisplayname = "your friend";
        int Port = 587;

        /// <summary>
        /// 发送邮件功能
        /// </summary>
        /// <param name="mailsubject">邮件标题</param>
        /// <param name="mailbody">邮件主要内容</param>
        /// <param name="isadddocument">是否添加附件</param>
        /// <param name="documentpath">添加附件的文件路径列表</param>
        /// <returns></returns>
        public bool Sendmail(string mailsubject, string mailbody,String receiver)
        {
            bool sendstatus = false;
            try
            {
               
                MailAddress from = new MailAddress(Sendmailaddress, SendDisplayname, Encoding.UTF8);
                MailAddress to = new MailAddress(receiver, "myfriend", Encoding.UTF8);
                MailMessage message = new MailMessage(from,to);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = false;
                message.Subject = mailsubject;
                message.SubjectEncoding = Encoding.UTF8;
                message.Body = mailbody;
                message.BodyEncoding = Encoding.UTF8;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = smtpHost; //邮件服务器SMTP
                smtpClient.Port = Port; //邮件服务器端口
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(Sendmailaddress, AuthKey);
                smtpClient.Send(message);
                sendstatus = true;
            }
            catch { }
            return sendstatus;
        }

        /// <summary>
        /// 添加附件功能
        /// </summary>
        /// <param name="message">Mailmessage对象</param>
        /// <param name="Documentpath">附件路径列表</param>
        private void AddDocument(MailMessage message, List<string> Documentpath)
        {
            foreach (string filepath in Documentpath)
            {
                try
                {
                    if (File.Exists(filepath)) //判断文件是否存在
                    {
                        Attachment attach = new Attachment(filepath);    //构造一个附件对象
                        ContentDisposition disposition = attach.ContentDisposition;   //得到文件的信息
                        disposition.CreationDate = System.IO.File.GetCreationTime(filepath);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(filepath);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(filepath);
                        message.Attachments.Add(attach);   //向邮件添加附件
                    }
                }
                catch { }
            }
        }
    }
}