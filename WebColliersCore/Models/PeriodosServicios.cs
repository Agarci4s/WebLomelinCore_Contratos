using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebLomelinCore.Data;

namespace WebLomelinCore.Models
{
    public class PeriodosServicios
    {
        [Required(ErrorMessage = "Seleccione el servicio")]
        [Display(Name = "Servicio")]
        public int IdServicio { get; set; }
        public string Servicio { get; set; }

        [Required(ErrorMessage = "Seleccione periodicidad")]
        [Display(Name = "Periodicidad")]
        public int IdPeriodicidad { get; set; }
        public string Periodicidad { get; set; }

        [Required(ErrorMessage = "Seleccione periodo")]
        [Display(Name = "Periodo")]
        public int IdPeriodoDisponible { get; set; }
        public string PeriodoDisponible { get; set; }

        [Required(ErrorMessage = "Seleccione el segmento temporal")]
        [Display(Name = "Segmento temporal")]
        public int IdBimestre { get; set; }
        public string Bimestre { get; set; }

        public static List<SelectListItem> getBimestres(int? IdPeriodicidad)
        {
                return new DataSelectService()
                            .getBimestre(IdPeriodicidad)
                            .OrderBy(x => int.TryParse(x.Value, out var val)? val :0)
                            .ToList();
        }

        public static List<SelectListItem> getPeriodosSiponibles
        {
            get
            {
                return new DataSelectService().getPeriodosDiponibles.OrderBy(x => x.Value).ToList();
            }
        }


        public static List<SelectListItem> getPeriodicidad(int? IdServicio)
        {
                return new DataSelectService().getPeriodicidad(IdServicio).OrderBy(x => x.Value).ToList();
        }
    }
}
