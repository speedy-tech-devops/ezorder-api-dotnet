using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices
{
    public class GEmailExtension
    {
        private SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587) { EnableSsl = true, UseDefaultCredentials = false };
        private readonly string _senderEmail;
        private readonly string _senderPassword;

        public GEmailExtension(string senderEmail, string senderPassword)
        {
            this._senderEmail = senderEmail;
            this._senderPassword = senderPassword;
        }
        public async Task<bool> SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage(_senderEmail, recipientEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                smtpClient.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
