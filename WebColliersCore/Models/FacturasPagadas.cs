using DocumentFormat.OpenXml.Presentation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebColliersCore.Models;

namespace WebLomelinCore.Models
{
    public class FacturasPagadas
    {

        public FacturasPagadas()
        {
            Inmueble = new B_inmuebles();
            Factura = new Factura();
        }

        [Required(ErrorMessage = "Seleccione el inmueble")]
        [DisplayName("Inmueble")]
        public int IdInmueble { get; set; }

        [Required(ErrorMessage = "Seleccione la región")]
        [Display(Name = "Región")]
        public int IdRegion { get; set; }

        [Required(ErrorMessage = "Seleccione un concepto")]
        [Display(Name = "Concepto")]
        public int IdConcepto { get; set; }

        [Display(Name = "Estatus")]
        public int IdStatusProceso { get; set; }   // 1=Autorizado, 2=Rechazado, etc.

        public B_inmuebles Inmueble { get; set; }
        public Factura Factura { get; set; }
        public DateTime FechaLimitePago { get; set; }
        public DateTime FechaPagoRealizado { get; set; }
        public string MesPago { get; set; }

        public bool EnvioIcoi { get; set; } = false;
        public bool ComprobantePago { get; set; } = false;
        public int IdRegistro { get; set; } = 0;
        public string? RutaXml { get; set; }
        public string? RutaPdf { get; set; }



    }
}
