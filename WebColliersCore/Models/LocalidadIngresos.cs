using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    public class LocalidadIngresos
    {
        public int IdLocalidad { get; set; }
        public int IdGasto { get; set; }
        public string Concepto { get; set; }
        public double Importe { get; set; }
        public string Unidad { get; set; }
        public string ProdServ { get; set; }
        public string RetencionISR { get; set; }
        public string RetencionIVA { get; set; }

    
    }
}
