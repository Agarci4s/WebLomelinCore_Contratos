using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    public class DtActividadPuesto
    {
        public int idactividad_puesto { get; set; }

        [Display(Name = "Puesto")]
        [MaxLength(150)]
        public string actividad { get; set; } //150
    }
}
