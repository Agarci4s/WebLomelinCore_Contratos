using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    public class RegistroSeries
    {
        public int ClaSerie { get; set; }
        public int ClaveRFC { get; set; } 
        public string Serie { get; set; }
        public int IdCartera { get; set; }
        [Display(Name = "Tipo de Comprobante")]
        public int IdTipoComprobante { get; set; }
        [Display(Name="Tipo de Comprobante")]
        public string TipoComprobante { get; set; }
    }
}
