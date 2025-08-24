using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Data
{
    [Serializable]
    /// <summary>
    ///   Obtiene o establece las propiedades  que se utilizan para el envío de un correo electrónico.
    /// </summary
    public class DataEmail
    {
        ///// <summary>
        ///// Clave o nombre de la cuenta que enviará el correo electrónico.
        ///// </summary>
        //public string Sender { get; set; }

        /// <summary>
        ///   Dirección de correo electrónico de quien lo envía.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        ///   Devuelve o establece una o varias direcciones de correo electrónico a las que ha de llegar el mensaje.
        /// </summary>
        public string Receivers { get; set; }

        /// <summary>
        ///   Devuelve o establece una o varias direcciones de correo electrónico a las que ha de llegar el mensaje como copia. 
        /// </summary>
        /// <remarks>Quienes estén en esta lista recibirán también el mensaje, pero verán que no va dirigido a ellos, 
        /// sino a quien esté designado en el campo Para (<c>Receivers</c>). Este campo lo ven todos los que reciben 
        /// el mensaje, tanto el destinatario principal como los de éste campo pueden ver la lista completa.</remarks>
        public string Cc { get; set; }

        /// <summary>
        ///   Devuelve o establece una o varias direcciones de correo electrónico a las que ha de llegar el mensaje como copia. 
        ///   Los destinatarios que reciban el mensaje no aparecerán en ninguna lista. 
        /// </summary>
        /// <remarks>Los destinatarios recibirán el mensaje sin aparecer en ninguna lista.</remarks>
        public string Bcc { get; set; }

        /// <summary>
        ///   Descripción corta que verá la persona que lo reciba antes de abrir el correo
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        ///   Texto del mensaje a enviar, puede ser sólo texto, o incluir formato.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///   Colección de archivos o documentos adjuntos.
        /// </summary>
        /// <remarks>Si no hay archivos o documentos adjuntos, esta propiedad es <c>null</c> o vacía.</remarks>
        public List<DataAttachmentEmail> Attachments { get; set; }

        /// <summary>
        ///   Especifica si el cuerpo del mensaje está en formato HTML. 
        /// </summary>
        /// <remarks>Sí el cuerpo del mensaje está en formato HTML el valor es <c>true</c>.</remarks>
        public bool IsHtmlFormat { get; set; }

    }
}
