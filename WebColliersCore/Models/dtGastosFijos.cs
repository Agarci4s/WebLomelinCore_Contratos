using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    public class dtGastosFijos
    {
        public int IdLocalidad { get; set; }
        [Display(Name="Concepto")]
        public int IdGasto { get; set; }
        [Display(Name ="Importe")]
        public double Importe { get; set; }
        public string Usuario { get; set; }
        public string Maquina { get; set; }
        public string DireccionIP { get; set; }
        [Display(Name = "Clave de Unidad")]
        public string clvUnidad { get; set; }
        [Display(Name = "Clave de Producto y/o Servicio")]
        public string clvProdServ { get; set; }
        [Display(Name = "Retención ISR")]
        public string RetencionISR { get; set; }
        public double PorCRetencionISR { get; set; }
        [Display(Name = "Retención IVA")]
        public string RetencionIVA { get; set; }
        public double PorCRetencionIVA { get; set; }
        [Display(Name = "Concepto")]
        public string Concepto { get; set; }
        [Display(Name = "Impuesto Cedular")]
        public string ImpuestoCedular { get; set; }   
        [Display(Name = "(%) Impuesto Cedular")]
        [Range(0,100)]
        public double PorCImpuestoCedular { get; set; }

        //Auxiliar
        public Boolean IVA { get; set; }
    }
}
