using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    public class CaracteristicasInmueble
    {
        public int IdLocalidad { get; set; }
        [Display(Name="Característica")]
        public int IdConceptoUsoInmueble { get; set; }
        public string Caracteristica { get; set; }
        public int IdUsoInmueble { get; set; }
        public int Tipo { get; set; }
        public int Status { get; set; }
        [Display(Name = "Cantidad")]
        public double Cantidad { get; set; }
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
    }
}
