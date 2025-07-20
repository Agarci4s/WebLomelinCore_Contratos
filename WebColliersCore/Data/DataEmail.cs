using DocumentFormat.OpenXml.Wordprocessing;
using System.Net.Mail;
using System.Net;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;
using System;
using System.Threading.Tasks;
using MySqlX.XDevAPI;
using WebColliersCore.Data;
using Humanizer;
using Stimulsoft.Report.Export;

namespace WebLomelinCore.Data
{
    public class DataEmail
    {
        private MailAddress fromAddress = new MailAddress("pruebas@horebsystem.com", "Lomelín");
        private const string fromPassword = "Pruebas1@";
        private const string subject = "Contrato por finalizar";
        private const string body = "Prueba de envío de correos.";



        public bool EnviaCorreo(int idCartera, int idUsuario)
        {
            SmtpClient smtp = new SmtpClient()
            {
                Host = "smtp.hostinger.com",
                Port = 587,/*587, 465*/
                EnableSsl = true,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            DataInmueblesContratosCorreos dataInmueblesContratosCorreos = new DataInmueblesContratosCorreos();
            DateTime inicio = new DateTime(1900, 1, 1);
            DateTime fin = DateTime.Now;
            List<B_inmuebles_contrato> b_Inmuebles_Contratos = dataInmueblesContratos.GetReporteAll(idCartera, idUsuario, false, inicio, fin);

            foreach (var item in b_Inmuebles_Contratos)
            {
                try
                {
                    if (item.fecha_termino.AddDays(-item.fecha_anticipacion) <= DateTime.Now)
                    {
                        List<B_inmuebles_contrato_correos> b_Inmuebles_Contrato_Correos = dataInmueblesContratosCorreos.Get(idCartera, idUsuario, item.id_b_inmuebles_contrato);
                        MailMessage message = new MailMessage()
                        {
                            Subject = subject,
                            Body = body
                        };
                        message.From = fromAddress;
                        foreach (var item2 in b_Inmuebles_Contrato_Correos)
                        {
                            MailAddress toAddress = new MailAddress(item2.correo, item2.nombre);
                            message.To.Add(toAddress);
                        }
                        if (b_Inmuebles_Contrato_Correos.Count > 0)
                        {
                            smtp.Send(message);
                            //Actualiza correo enviado
                            dataInmueblesContratos.CorreoEnviado(item.id_b_inmuebles_contrato);
                        }

                    }
                }
                catch (Exception) { }
            }
            return true;
        }

    }
}
