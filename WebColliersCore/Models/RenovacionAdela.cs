using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebColliersCore.Models;

namespace WebLomelinCore.Models
{
    public class RenovacionAdela
    {
        public int Id { get; set; }
        [Display(Name = "Inmueble")]
        public int IdInmueble { get; set; }
        [Display(Name = "Contrato")]
        public int IdContrato { get; set; }

        #region "Distribución..."
        [Display(Name = "Uso")]
        public string Uso { get; set; }
        [Display(Name = "Superficie")]
        public double Superficie { get; set; }
        [Display(Name = "Precio x M2")]
        public double PrecioM2 { get; set; }
        [Display(Name = "Precio x Superficie")]
        public double PrecioSuperficie { get; set; }
        public List<B_inmuebles_contrato_distribucion> b_Inmuebles_Contrato_Distribucions { get; set; }

        #endregion

        #region "Revisión..."

        [Display(Name = "Mínima")]
        public double Rentaminima { get; set; }
        [Display(Name = "Máxima")]
        public double Rentamaxima { get; set; }
        [Display(Name = "Vigente al")]
        public DateTime VigenteAl { get; set; }
        [Display(Name = "Renta")]
        public double RentaActual { get; set; }
        [Display(Name = "Precio por M2")]
        public double PrecioM2RentaActual { get; set; }
        [Display(Name = "Renta")]
        public double RentaPropuesta { get; set; }
        [Display(Name = "Precio por M2")]
        public double PrecioM2RentaPropuesta { get; set; }
        [Display(Name = "Renta")]
        public double TopeRenta { get; set; }
        [Display(Name = "Precio por M2")]
        public double PrecioM2TopeRenta { get; set; }
        [Display(Name = "Nueva Renta")]
        public double RentaPactada { get; set; }
        [Display(Name = "Precio por M2")]
        public double PrecioM2RentaPactada { get; set; }

        #endregion

        #region "Adela..."

        [Display(Name = "Adela")]
        public string Adela { get; set; }
        [Display(Name = "Número de Prorrateo")]
        public string NumProrrateo { get; set; }
        [Display(Name = "% de renta para el CEPRO")]
        public double PorcRtaCepro { get; set; }
        [Display(Name = "¿Pierde diferencias?")]
        public string PierdeDif { get; set; }
        [Display(Name = "Meses anticipados")]
        public int MesesAnticipados { get; set; }
        [Display(Name = "% de descuento")]
        public double PorcDescuento { get; set; }
        [Display(Name = "Importe anticipado")]
        public double ImporteAnticipado { get; set; }
        [Display(Name = "Importe descontado")]
        public double ImporteDescontado { get; set; }
        [Display(Name = "DECIL RANK")]
        public string DecilRankAdela { get; set; }
        [Display(Name = "Importe Total")]
        public double ImporteTotal { get; set; }

        [Display(Name = "Fecha revisión")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        //[Remote(action: "VerificaRevision", controller: "B_inmueblesContrato", AdditionalFields = nameof(fecha_inicio) + "," + nameof(fecha_termino))]
        public DateTime fecha_revision { get; set; }

        [Display(Name = "Fecha inicio")]
        //[Remote(action: "VerificaFechaInicio", controller: "B_inmueblesContrato", AdditionalFields = nameof(id_b_inmuebles) + "," + nameof(id_b_inmuebles_contrato))]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime fecha_inicio { get; set; }

        [Display(Name = "Fecha término")]
        //[Remote(action: "VerificaFechaTermino", controller: "B_inmueblesContrato", AdditionalFields = nameof(fecha_inicio))]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime fecha_termino { get; set; }

        [Display(Name = "INPC Vigente")]
        [Range(.01, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        // [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal inpc { get; set; }

        [Display(Name = "% Sugerido para Negociar")]
        [Range(.01, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        // [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal PorcentajeNegociar { get; set; }

        #endregion

        public B_inmuebles b_inmuebles;

        [Display(Name = "UE")]
        [DefaultValue("000")]
        public int ue { get; set; }

        [Display(Name = "CR")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [RegularExpression("MX[0-9]{8}$", ErrorMessage = "Agregue un valor valido")]
        public string cr { get; set; }

        [Display(Name = "Nombre de sucursal")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        public string nombre { get; set; }

        [Display(Name = "Calle / AV")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        public string direccion { get; set; }

        [Display(Name = "C.P.")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [RegularExpression("^[0-9]{5}$", ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:D5}", ApplyFormatInEditMode = true)]
        public int cp { get; set; }

        [Display(Name = "Ciudad / Estado")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [DefaultValue("")]
        public string estado { get; set; }

        [Display(Name = "Alcaldía / Municipio")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [DefaultValue("")]
        public string municipio { get; set; }

        [Display(Name = "Colonia")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(100, ErrorMessage = "Agregue un valor valido")]
        [DefaultValue("")]
        public string colonia { get; set; }

        [Display(Name = "Número")]
        [DefaultValue("")]
        [MaxLength(70, ErrorMessage = "Agregue un valor valido")]
        public string noext { get; set; }

        [Display(Name = "Número interior")]
        [DefaultValue("")]
        [MaxLength(500, ErrorMessage = "Agregue un valor valido")]
        public string noint { get; set; }

        [Display(Name = "Manzana")]
        [DefaultValue("")]
        [MaxLength(45, ErrorMessage = "Agregue un valor valido")]
        public string manzana { get; set; }

        [Display(Name = "Lote")]
        [DefaultValue("")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string lote { get; set; }

    }
}