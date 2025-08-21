using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;
using System.Net;
using System.Configuration;
using WebColliersCore.Models;
using MySql.Data.MySqlClient;
using System.Data;
using WebColliersCore.DataAccess;

namespace WebColliersCore.Data
{
    public class DataEnvioEmail : Conexion
    {

        public DataResultEmail EmailSend(DataEmail emailData)
        {
            DataResultEmail result = new DataResultEmail();

            result = SendEmail(emailData.From, DataValidadorEmail.BuildEmailList(emailData.Receivers), DataValidadorEmail.BuildEmailList(emailData.Cc), DataValidadorEmail.BuildEmailList(emailData.Bcc), emailData.Subject, emailData.Body, emailData.IsHtmlFormat, emailData.Attachments);


            return result;
        }

        public DataResultEmail EmailSendOC(DataEmail emailData)
        {
            DataResultEmail result = new DataResultEmail();

            result = SendEmailOC(emailData.From, DataValidadorEmail.BuildEmailList(emailData.Receivers), DataValidadorEmail.BuildEmailList(emailData.Cc), DataValidadorEmail.BuildEmailList(emailData.Bcc), emailData.Subject, emailData.Body, emailData.IsHtmlFormat, emailData.Attachments);


            return result;
        }

        private DataResultEmail SendEmail(string fromc, List<string> receivers, List<string> cc, List<string> bcc, string subject, string body,
              bool isHtmlFormat, List<DataAttachmentEmail> attachmentList = null)
        {
            List<EmailDatosServidorModel> DatosServidor = ObtineDatosServidorEmail(4);

            fromc = DatosServidor[0].UserName;

            var results = new DataResultEmail();
            //Se crea el mensaje 
            var mailMessage = new MailMessage
            {
              
                From = new MailAddress(fromc),  // new MailAddress(ConfigurationManager.AppSettings["User"].ToString()),
                Body = body,
                BodyEncoding = Encoding.Default,
                Subject = subject,
                SubjectEncoding = Encoding.Default,
                IsBodyHtml = isHtmlFormat,
                Priority = MailPriority.Normal,              

            };

            receivers.ToList().ForEach(toItem => mailMessage.To.Add(toItem));
            cc.ToList().ForEach(ccItem => mailMessage.CC.Add(ccItem));
            bcc.ToList().ForEach(bccItem => mailMessage.Bcc.Add(bccItem));
            
            if (attachmentList != null && attachmentList.Count > 0)
            {
                ProcessAttachments(attachmentList).ToList().ForEach(attachmentItem => mailMessage.Attachments.Add(attachmentItem));
            }

            try
            {
                

                /*Gmail*/
                //string Host = "smtp.gmail.com";//ConfigurationManager.AppSettings["Host"].ToString();
                //string User = "";//ConfigurationManager.AppSettings["User"].ToString();
                //string Password = ""; //ConfigurationManager.AppSettings["Password"].ToString();
                //int Port = 587;     //int.Parse(ConfigurationManager.AppSettings["Port"].ToString());
                //bool EnableSsl = true;//bool.Parse(ConfigurationManager.AppSettings["EnableSsl"].ToString());

                /*Colliers*/
                string Host = DatosServidor[0].Host;
                string User = DatosServidor[0].UserName;
                string Password = DatosServidor[0].Password; 
                int Port = DatosServidor[0].Port;     
                bool EnableSsl = true;


                using (var client = new SmtpClient
                {
                    Host = Host,
                    Port = Port,
                    EnableSsl = EnableSsl,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(User, Password)       
                })
                {
                    client.Send(mailMessage);
                    results = new DataResultEmail
                    {

                        AttachmentsSent = attachmentList != null ? attachmentList.Count : 0,
                        AttachmentsUnSent = 0,
                        InvalidEmails = new List<string>(),
                        Observaciones = "1 - Correo enviado satisfactoriamente"
                    };

                }
            }
            catch (Exception ex)
            {
                // throw new EmailException(ex);

                results = new DataResultEmail
                {

                    AttachmentsSent = attachmentList != null ? attachmentList.Count : 0,
                    AttachmentsUnSent = 0,
                    InvalidEmails = new List<string>(),
                    Observaciones = "Error: " + ex.Message
                };


            }
            finally
            {
                try
                {
                    mailMessage.Dispose();
                }
                catch { }
            }




            return results;
        }

        private DataResultEmail SendEmailOC(string fromc, List<string> receivers, List<string> cc, List<string> bcc, string subject, string body,
              bool isHtmlFormat, List<DataAttachmentEmail> attachmentList = null)
        {
            List<EmailDatosServidorModel> DatosServidor = ObtineDatosServidorEmail(1);

            fromc = DatosServidor[0].UserName;

            var results = new DataResultEmail();
            //Se crea el mensaje 
            var mailMessage = new MailMessage
            {

                From = new MailAddress(fromc, "Notificaciones Lomelin"),  // new MailAddress(ConfigurationManager.AppSettings["User"].ToString()),
                Body = body,
                BodyEncoding = Encoding.Default,
                Subject = subject,
                SubjectEncoding = Encoding.Default,
                IsBodyHtml = isHtmlFormat,
                Priority = MailPriority.High,

            };

            receivers.ToList().ForEach(toItem => mailMessage.To.Add(toItem));
            cc.ToList().ForEach(ccItem => mailMessage.CC.Add(ccItem));
            bcc.ToList().ForEach(bccItem => mailMessage.Bcc.Add(bccItem));

            if (attachmentList != null && attachmentList.Count > 0)
            {
                ProcessAttachments(attachmentList).ToList().ForEach(attachmentItem => mailMessage.Attachments.Add(attachmentItem));
            }

            try
            {


                /*Gmail*/
                //string Host = "smtp.gmail.com";//ConfigurationManager.AppSettings["Host"].ToString();
                //string User = "";//ConfigurationManager.AppSettings["User"].ToString();
                //string Password = ""; //ConfigurationManager.AppSettings["Password"].ToString();
                //int Port = 587;     //int.Parse(ConfigurationManager.AppSettings["Port"].ToString());
                //bool EnableSsl = true;//bool.Parse(ConfigurationManager.AppSettings["EnableSsl"].ToString());

                /*Colliers*/
                string Host = DatosServidor[0].Host;
                string User = DatosServidor[0].UserName;
                string Password = DatosServidor[0].Password;
                int Port = DatosServidor[0].Port;
                bool EnableSsl = true;


                using (var client = new SmtpClient
                {
                    Host = Host,
                    Port = Port,
                    EnableSsl = EnableSsl,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(User, Password)
                })
                {
                    client.Send(mailMessage);
                    results = new DataResultEmail
                    {

                        AttachmentsSent = attachmentList != null ? attachmentList.Count : 0,
                        AttachmentsUnSent = 0,
                        InvalidEmails = new List<string>(),
                        Observaciones = "1 - Correo enviado satisfactoriamente"
                    };

                }
            }
            catch (Exception ex)
            {
                // throw new EmailException(ex);

                results = new DataResultEmail
                {

                    AttachmentsSent = attachmentList != null ? attachmentList.Count : 0,
                    AttachmentsUnSent = 0,
                    InvalidEmails = new List<string>(),
                    Observaciones = "Error: " + ex.Message
                };


            }
            finally
            {
                try
                {
                    mailMessage.Dispose();
                }
                catch { }
            }

            return results;
        }

        private IEnumerable<Attachment> ProcessAttachments(List<DataAttachmentEmail> attachmentList)
        {
            var attachments = new List<Attachment>();
            using (var attachmentValidator = new DataAttachmentValidatorEmail())
            {
                attachmentValidator.ValidateAttachmentList(attachmentList);

                if (attachmentValidator.ValidAttachments.Count != attachmentList.Count)
                {
                    //throw new InvalidAttachmentException("AttachmentNotSend");
                }

                var resultSizeValidation = DataAttachmentValidatorEmail.ValidateTotalSize(attachmentList, 10);
                if (resultSizeValidation != null)
                {
                    if (!resultSizeValidation.Value)
                    {

                        //throw new InvalidAttachmentException("MaximumSizeExceeded");
                    }
                }

                attachments.AddRange(attachmentValidator.ValidAttachments.Select(attachmentItem =>
                    new Attachment(new MemoryStream(attachmentItem.Content), new ContentType(attachmentItem.FileMime))
                    {
                        Name = attachmentItem.FileName,
                        NameEncoding = Encoding.Default
                    }));

            }

            return attachments;
        }

        public List<EmailDatosServidorModel> ObtineDatosServidorEmail(int idEmailServidor)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("IdEmailServidor_In", MySqlDbType.Int32) { Value = idEmailServidor });
            DataTable dataTable = RunStoredProcedure("ObtieneDatosServidorEMail", mySqlParameters);

            try
            {
                List<EmailDatosServidorModel> lst = new List<EmailDatosServidorModel>();
                foreach (DataRow item in dataTable.Rows)
                {
                    EmailDatosServidorModel listOptions = new EmailDatosServidorModel();
                    listOptions.IdEmailServidor = Convert.ToInt32(item["idEmailServidor"]);
                    listOptions.Host = item["HostName"].ToString();
                    listOptions.Port = Convert.ToInt32(item["PortNumber"]);
                    listOptions.UserName = item["userEmailServer"].ToString();
                    listOptions.Password = item["passwordEmailServer"].ToString();

                    lst.Add(listOptions);
                }
                return lst;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
