using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebColliersCore.Models
{
    [Serializable]
    public class Catalogo
    {
        /// <summary>
        /// Obtiene o establece el valor del id del catalogo
        /// </summary>
        public int IdCatalogo { get; set; }

        /// <summary>
        /// Obtiene o establece la descripción
        /// </summary>
        public string Descripcion { get; set; }

    }
}
