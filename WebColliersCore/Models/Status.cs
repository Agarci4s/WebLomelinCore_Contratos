using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    /// <summary>
    /// Obtiene o estable el estatus del resultado de la operación.
    /// </summary>

    [Serializable]
    public class Status
    {
        /// <summary>
        /// Obtiene o estable el resultado de la operacion  true  o false.
        /// </summary>
        public bool StatusOperacion { get; set; }

        /// <summary>
        /// Obtiene o estable el mensaje del resultado de la operacion.
        /// </summary>
        public string Mensaje { get; set; }
    }
}
