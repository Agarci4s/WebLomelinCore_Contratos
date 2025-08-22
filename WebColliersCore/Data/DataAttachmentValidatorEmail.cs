using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Data
{
    [Serializable]
    /// <summary>
    ///   Realiza validaciones con la lista de datos adjuntos.
    /// </summary>
    public class DataAttachmentValidatorEmail : IDisposable
    {

        ///// <summary>
        ///// Especifíca en bytes el tamaño máximo de los datos adjuntos.
        ///// </summary>
        //public const int MaxSizeForAttachments = 85000; //8500000;

        #region ## IDisposable #

        /// <summary>
        ///   Track whether Dispose has been called.
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///   Realiza tareas definidas por la aplicación asociadas a la liberación o al restablecimiento de recursos no administrados.
        ///   Este método no debe ser virtual y no debería ser sobre-escrito.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue 
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Dispose managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing"> If disposing equals true, the method has been called directly or indirectly by a user's code. Managed and unmanaged resources can be disposed. If disposing equals false, the method has been called by the runtime from inside the finalizer and you should not reference other objects. Only unmanaged resources can be disposed. </param>
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!_disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    ValidAttachments.Clear();
                    ValidAttachments = null;
                }

                // Call the appropriate methods to clean up 
                // unmanaged resources here.
                // If disposing is false, 
                // only the following code is executed.
            }
            _disposed = true;
        }

        /// <summary>
        ///   Use C# destructor syntax for finalization code. This destructor will run only if 
        ///   the Dispose method does not get called. It gives your base class the opportunity 
        ///   to finalize. Do not provide destructors in types derived from this class.
        /// </summary>
        ~DataAttachmentValidatorEmail()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion

        /// <summary>
        ///   Inicializa una nueva instancia de la clase <see cref="DataAttachmentValidatorEmail" />.
        /// </summary>
        public DataAttachmentValidatorEmail()
        {
            ValidAttachments = new List<DataAttachmentEmail>();
        }
        
        /// <summary>
        ///   Devuelve una lista de datos adjuntos válidos.
        /// </summary>
        public List<DataAttachmentEmail> ValidAttachments { get; private set; }

        /// <summary>
        /// Verifica que la suma de los tamaños de los datos adjuntos sea menor al
        /// valor establecido.
        /// </summary>
        /// <param name="attachmentList"> Lista de datos adjuntos. </param>
        /// <param name="maxAttachmentBytesSize">Tamaño en bytes de los datos adjuntos. </param>
        /// <returns>Devuelve <c>true</c> si el tamaño de los datos adjuntos 
        /// es menor a  <c>maxAttachmentBytesSize</c>.</returns>
        public static bool? ValidateTotalSize(List<DataAttachmentEmail> attachmentList, int maxAttachmentBytesSize)
        {
            bool flat = true;
            if (attachmentList == null || attachmentList.Count <= 0) return flat = false;

            //var sizeRule = new ValidateRange().
            //    SetValueRange(attachmentList.ToList().Sum(x => x.FileSize),
            //    2, maxAttachmentBytesSize, ValidationDataType.Integer);

            //var businessValue = new BusinessValue();

            //businessValue.AddRule(sizeRule);

            //return businessValue.Validate();
            return flat;
        }

        /// <summary>
        ///   Lleva a cabo una validación de los datos adjuntos.
        /// </summary>
        /// <param name="attachmentList"> Lista de datos adjuntos. </param>
        public void ValidateAttachmentList(List<DataAttachmentEmail> attachmentList)
        {
            ValidAttachments.Clear();

            if (attachmentList == null || attachmentList.Count <= 0) return;

            //var mimeRequiredRule =
            //    new ValidateRequired().SetPropertyRequired(
            //        ReflectionHelper.GetPropertyNameOf<EmailAttachment>(x => x.FileMime));

            //var nameRequiredRule = new ValidateRequired().SetPropertyRequired(
            //    ReflectionHelper.GetPropertyNameOf<EmailAttachment>(x => x.FileName));

            foreach (var attachmentItem in attachmentList)
            {
                //var businessObject = new BusinessObject<EmailAttachment>(attachmentItem);
                //businessObject.AddRules(nameRequiredRule, mimeRequiredRule);
                //if (businessObject.Validate())
                //{
                ValidAttachments.Add(attachmentItem);
                //}
            }
        }
    }
}
