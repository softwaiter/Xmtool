using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CodeM.Common.Tools
{
    public class MailTool
    {
        private static ConcurrentDictionary<string, MailTool> sClients = new ConcurrentDictionary<string, MailTool>();
        private static object sClientLock = new object();

        public static MailTool New(string host, int port, bool enableSsl, string account, string password)
        {
            MailTool result;
            string key = string.Concat(host.Trim(), port, enableSsl, account.Trim(), password.Trim()).ToLower();
            if (!sClients.TryGetValue(key, out result))
            {
                lock (sClientLock)
                {
                    if (!sClients.TryGetValue(key, out result))
                    {
                        result = new MailTool(host, port, enableSsl, account, password);
                        sClients.TryAdd(key, result);
                    }
                }
            }
            return result;
        }

        private MailTool(string host, int port, bool enableSsl, string account, string password)
        {
            this.Host = host;
            this.Port = port;
            this.EnableSsl = enableSsl;
            this.Account = account;
            this.Password = password;
        }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        private SmtpClient GetClient()
        {
            SmtpClient client = new SmtpClient(this.Host, this.Port);
            client.EnableSsl = this.EnableSsl;
            client.Credentials = new NetworkCredential(this.Account, this.Password);
            return client;
        }

        private MailMessage BuildMailMessage(string subject, string body, string bodyEncoding, bool isHtmlBody,
            string from, string fromName, string to, string replyTo, string cc, string bcc,
            params string[] attachments)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new Exception("邮件标题不能为空。");
            }

            if (string.IsNullOrWhiteSpace(body))
            {
                throw new Exception("邮件内容不能为空。");
            }

            if (string.IsNullOrWhiteSpace(from))
            {
                throw new Exception("发件地址不能为空。");
            }

            if (string.IsNullOrWhiteSpace(to))
            {
                throw new Exception("收件地址不能为空。");
            }

            if (string.IsNullOrWhiteSpace(fromName))
            {
                fromName = from;
            }

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress(from, fromName);  // 发送地址

            string[] tos = to.Split(";");
            foreach (string toAddr in tos)
            {
                if (!string.IsNullOrWhiteSpace(toAddr))
                {
                    mm.To.Add(to);  // 接收地址
                }
            }

            if (!string.IsNullOrWhiteSpace(replyTo))
            {
                string[] replyTos = replyTo.Split(";");
                foreach (string replyToAddr in replyTos)
                {
                    if (!string.IsNullOrWhiteSpace(replyToAddr))
                    {
                        mm.ReplyToList.Add(replyToAddr);    //回复地址
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(cc))
            {
                string[] ccs = cc.Split(";");
                foreach (string ccAddr in ccs)
                {
                    if (!string.IsNullOrWhiteSpace(ccAddr))
                    {
                        mm.CC.Add(ccAddr);  //抄送地址
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(bcc))
            {
                string[] bccs = bcc.Split(";");
                foreach (string bccAddr in bccs)
                {
                    if (!string.IsNullOrWhiteSpace(bccAddr))
                    {
                        mm.Bcc.Add(bccAddr);    //秘密抄送地址
                    }
                }
            }

            Encoding encoding = Encoding.UTF8;
            if (!string.IsNullOrWhiteSpace(bodyEncoding))
            {
                encoding = Encoding.GetEncoding(bodyEncoding);
            }

            mm.Subject = subject;           // 标题
            mm.Body = body;                 // 邮件内容
            mm.BodyEncoding = encoding;     // 邮件内容编码格式
            mm.IsBodyHtml = isHtmlBody;     // 内容是否网页格式

            if (attachments.Length > 0)
            {
                foreach (string attachment in attachments)
                {
                    mm.Attachments.Add(new Attachment(attachment));
                }
            }

            return mm;
        }

        private void Send(string subject, string body, string bodyEncoding, bool isHtmlBody,
            string from, string fromName, string to, string replyTo, string cc, string bcc,
            params string[] attachments)
        {
            MailMessage mm = BuildMailMessage(subject, body, bodyEncoding, isHtmlBody,
                from, fromName, to, replyTo, cc, bcc, attachments);
            GetClient().Send(mm);
        }

        public void Send(string subject, string body, string bodyEncoding,
            string from, string fromName, string to, string replyTo, string cc, string bcc,
            params string[] attachments)
        {
            Send(subject, body, bodyEncoding, false, from, fromName, to, replyTo, cc, bcc, attachments);
        }

        public void Send(string subject, string body, string from, string to, params string[] attachments)
        {
            Send(subject, body, null, from, null, to, null, null, null, attachments);
        }

        public void SendHtml(string subject, string body, string bodyEncoding,
            string from, string fromName, string to, string replyTo, string cc, string bcc,
            params string[] attachments)
        {
            Send(subject, body, bodyEncoding, true, from, fromName, to, replyTo, cc, bcc, attachments);
        }

        public void SendHtml(string subject, string body, string from, string to, params string[] attachments)
        {
            SendHtml(subject, body, null, from, null, to, null, null, null, attachments);
        }

        private async Task SendAsync(string subject, string body, string bodyEncoding, bool isHtmlBody,
            string from, string fromName, string to, string replyTo, string cc, string bcc,
            params string[] attachments)
        {
            MailMessage mm = BuildMailMessage(subject, body, bodyEncoding, isHtmlBody,
                from, fromName, to, replyTo, cc, bcc, attachments);
            await GetClient().SendMailAsync(mm);
        }

        public async Task SendAsync(string subject, string body, string bodyEncoding,
            string from, string fromName, string to, string replyTo, string cc, string bcc,
            params string[] attachments)
        {
            await SendAsync(subject, body, bodyEncoding, false, from, fromName, to, replyTo, cc, bcc, attachments);
        }

        public async Task SendAsync(string subject, string body, string from, string to, params string[] attachments)
        {
            await SendAsync(subject, body, null, from, null, to, null, null, null, attachments);
        }

        public async Task SendHtmlAsync(string subject, string body, string bodyEncoding,
            string from, string fromName, string to, string replyTo, string cc, string bcc,
            params string[] attachments)
        {
            await SendAsync(subject, body, bodyEncoding, true, from, fromName, to, replyTo, cc, bcc, attachments);
        }

        public async Task SendHtmlAsync(string subject, string body, string from, string to, params string[] attachments)
        {
            await SendHtmlAsync(subject, body, null, from, null, to, null, null, null, attachments);
        }
    }
}
