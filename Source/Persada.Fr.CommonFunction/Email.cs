using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Persada.Fr.ModelView;
using System.Net;

namespace Persada.Fr.CommonFunction
{
    public class Email
    {
        public ResultStatus SendEmail(MailMessage mail, string attachment)
        {
            ResultStatus rs = new ResultStatus();
            SmtpClient client = new SmtpClient();

            try
            {
                if (attachment != null && attachment != "")
                {
                    FileStream fs = new FileStream(attachment, FileMode.Open, FileAccess.Read);
                    string fileName = Common.GetFileName(attachment);
                    Attachment a = new Attachment(fs, fileName, MediaTypeNames.Application.Octet);

                    mail.Attachments.Add(a);
                }

                client.Send(mail);
                rs.SetSuccessStatus("Email has been sent");
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public MailMessage MappingEmail(CUSTOM_MAIL customMail)
        {
            MailMessage mail = new MailMessage();
            foreach (var from in customMail.FROM)
            {
                mail.From = new MailAddress(from, "CSProfessionalisme");
            }

            foreach (var to in customMail.TO)
            {
                mail.To.Add(new MailAddress(to));
            }

            foreach (var cc in customMail.CC)
            {
                mail.CC.Add(new MailAddress(cc));
            }

            mail.Subject = customMail.SUBJECT;
            mail.IsBodyHtml = customMail.ISBODYHTML;
            mail.Body = customMail.BODY;

            return mail;
        }

        public byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }
    }
}