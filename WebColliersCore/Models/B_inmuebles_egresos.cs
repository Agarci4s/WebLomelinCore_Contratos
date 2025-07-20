using DocumentFormat.OpenXml.Office.CoverPageProps;
using System.ComponentModel.DataAnnotations;

namespace WebLomelinCore.Models
{
    public class B_inmuebles_egresos
    {
        public int Id { get; set; }
        [Display(Name = "Inmueble")]
        public int IdInmueble {  get; set; }
        [Display(Name = "Contrato")]
        public int IdContrato { get; set; }
        [Display(Name = "Egreso")]
        public int IdEgreso { get; set; }
        [Display(Name = "Egreso")]
        public string Egreso {  get; set; }
        [Display(Name = "Importe")]
        public double Importe { get; set; }
        [Display(Name = "% de IVA")]
        public double PorcIVA { get; set; }
        [Display(Name = "IVA")]
        public double IVA { get; set; }
        [Display(Name = "% de Retención ISR")]
        public double PorcRetISR { get; set; }
        [Display(Name = "Retención ISR")]
        public double RetISR { get; set; }
        [Display(Name = "% de Retención IVA")]
        public double PorcRetIVA { get; set; }
        [Display(Name = "Retención IVA")]
        public double RetIVA { get; set; }
        [Display(Name = "Moneda")]
        public int IdMoneda { get; set; }
        [Display(Name = "Moneda")]
        public string Moneda { get; set; }
    }
}
