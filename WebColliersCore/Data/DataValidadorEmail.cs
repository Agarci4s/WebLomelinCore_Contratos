using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebColliersCore.Data
{
    [Serializable]
    /// <summary>
    ///   Permite llevar a cabo validaciones con las direcciones de correo electrónico.
    /// </summary>
    public class DataValidadorEmail : IDisposable
    {
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
                    InvalidEmails.Clear();
                    ValidEmails.Clear();
                    InvalidEmails = null;
                    ValidEmails = null;
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
        ~DataValidadorEmail()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion



        /// <summary>
        ///   Inicializa una nueva instancia de la clase <see cref="EmailValidator" />.
        /// </summary>
        public DataValidadorEmail()
        {
            InvalidEmails = new List<string>();
            ValidEmails = new List<string>();

        }

        /// <summary>
        ///   Devuelve una lista de correos electrónicos inválidos.
        /// </summary>
        public List<string> InvalidEmails { get; private set; }

        /// <summary>
        ///   Devuelve una lista de correos electrónicos válidos.
        /// </summary>
        public List<string> ValidEmails { get; private set; }


        /// <summary>
        ///   Construye un objeto <see cref="string" /> para representar la lista de correos que son inválidos.
        /// </summary>
        /// <param name="invalidEmails"> Lista de correos inválidos. </param>
        /// <returns> Devuelve un objeto <see cref="string" /> que representa la lista de correos electrónicos que son inválidos. </returns>
        public static string InvalidEmailRawFormat(IEnumerable<string> invalidEmails)
        {
            var invalidEmailsBuilder = new StringBuilder();
            foreach (var emailItem in invalidEmails)
            {
                invalidEmailsBuilder.Append(string.Format("{0}, ", emailItem));
            }

            var invalidmails = invalidEmailsBuilder.ToString();
            return invalidmails.Length > 2 ? invalidmails.Substring(0, invalidmails.Length - 2) : invalidmails;
        }

        /// <summary>
        ///   Construye un objeto <see cref="List{T}" /> con los correos electrónicos.
        /// </summary>
        /// <param name="emails"> Objeto <see cref="string" /> con las direcciones de correo electrónico, separadas por el caractér coma. </param>
        /// <returns> Objeto <see cref="List{T}" /> con las direcciones de correo electrónico. </returns>
        public static List<string> BuildEmailList(string emails)
        {
            var emailList = new List<string>();

            if (string.IsNullOrWhiteSpace(emails)) return emailList;

            emailList.AddRange(emails.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

            return emailList;
        }


    }
}
