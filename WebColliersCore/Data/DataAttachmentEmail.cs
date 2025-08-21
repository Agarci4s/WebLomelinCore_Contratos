using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Data
{
    [Serializable]
    /// <summary>
    ///   Obtiene o establece las propiedades del archivo adjunto.
    /// </summary>
    public class DataAttachmentEmail
    {
        /// <summary>
        ///   Obtiene el tamaño del archivo en Bytes.
        /// </summary>
        public decimal FileSize
        {
            get { return Content.Length + 1; }
        }

        /// <summary>
        ///   Representa un archivo o información adjunta al correo electrónico.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///   Reprenta el contenido como un flujo de bytes.
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        ///   Representa la aplicación o formato en que esta creado el archivo.
        /// </summary>
        public string FileMime { get; set; }
    }
}
