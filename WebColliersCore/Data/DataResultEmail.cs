using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebColliersCore.Data
{
    [Serializable]
    /// <summary>
    /// Proporciona un resumen de la información que fue enviada por correo electrónico.
    /// </summary>
    public class DataResultEmail
    {
        /// <summary>
        /// Número de correos electrónicos principales a los que se envió el correo electrónico.
        /// </summary>
        public int Sent { get; set; }

        /// <summary>
        /// Número de correos electrónicos con copia oculta a los que se envió el correo electrónico.
        /// </summary>
        public int SentBcc { get; set; }

        /// <summary>
        /// Número de correos electrónicos con copia a los que se envió el correo electrónico.
        /// </summary>
        public int SentCc { get; set; }

        /// <summary>
        /// Número de correos electrónicos a los que no se envió el correo electrónico.
        /// </summary>
        public int UnSent { get; set; }

        /// <summary>
        /// Número de correos electrónicos con copia oculta a los que no se envió el correo electrónico.
        /// </summary>
        public int UnSentBcc { get; set; }

        /// <summary>
        /// Número de correos electrónicos con copia a los que no se envió el correo electrónico.
        /// </summary>
        public int UnSentCc { get; set; }

        /// <summary>
        /// Número de datos adjuntos enviados.
        /// </summary>
        public int AttachmentsSent { get; set; }

        /// <summary>
        /// Número de datos adjuntos que no pudieron ser enviados.
        /// </summary>
        public int AttachmentsUnSent { get; set; }

        /// <summary>
        /// Devuelve o establece una lista de correos electrónicos inválidos.
        /// </summary>
        public List<string> InvalidEmails { get; set; }

        /// <summary>
        /// Devuelve o establece alguna observacion.
        /// </summary>
        public string Observaciones { get; set; }


    }
}
