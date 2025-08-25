using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebColliersCore.Data;
using WebColliersCore.Models;

namespace WebLomelinCore.Models
{
    public class NegociacionesRenovacion
    {
        //[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]


        public int id_negociacion_contratos { get; set; }

        [Display(Name = "Inmueble")]
        public int id_b_inmuebles { get; set; }

        [Display(Name = "Contrato")]
        public int id_b_inmuebles_contrato { get; set; }




        #region "Distribución..."
        [Display(Name = "Uso")]
        public string Uso { get; set; }
        [Display(Name = "Superficie")]
        public decimal Superficie { get; set; }
        [Display(Name = "Precio x M2")]
        public decimal PrecioM2 { get; set; }
        [Display(Name = "Precio x Superficie")]
        public decimal PrecioSuperficie { get; set; }
        public List<B_inmuebles_contrato_distribucion> b_Inmuebles_Contrato_Distribucions { get; set; }

        #endregion

        #region "Revisión..."

        [Display(Name = "Mínima")]
        public decimal Rentaminima { get; set; }
        [Display(Name = "Máxima")]
        public decimal Rentamaxima { get; set; }
        [Display(Name = "Vigente al")]
        public DateTime VigenteAl { get; set; }
        [Display(Name = "Renta")]
        public decimal RentaActual { get; set; }
        [Display(Name = "Precio por M2")]
        public decimal PrecioM2RentaActual { get; set; }
        [Display(Name = "Renta")]
        public decimal RentaPropuesta { get; set; }
        [Display(Name = "Precio por M2")]
        public decimal PrecioM2RentaPropuesta { get; set; }
        [Display(Name = "Renta")]
        public decimal TopeRenta { get; set; }
        [Display(Name = "Precio por M2")]
        public decimal PrecioM2TopeRenta { get; set; }
        [Display(Name = "Nueva Renta")]
        public decimal RentaPactada { get; set; }
        [Display(Name = "Precio por M2")]
        public decimal PrecioM2RentaPactada { get; set; }

        #endregion

        #region "Adela..."

        [Display(Name = "Adela")]
        public string Adela { get; set; }
        [Display(Name = "Número de Prorrateo")]
        public string NumProrrateo { get; set; }
        [Display(Name = "% de renta para el CEPRO")]
        public decimal PorcRtaCepro { get; set; }
        [Display(Name = "¿Pierde diferencias?")]
        public string PierdeDif { get; set; }
        [Display(Name = "Meses anticipados")]
        public int MesesAnticipados { get; set; }
        [Display(Name = "% de descuento")]
        public decimal PorcDescuento { get; set; }
        [Display(Name = "Importe anticipado")]
        public decimal ImporteAnticipado { get; set; }
        [Display(Name = "Importe descontado")]
        public decimal ImporteDescontado { get; set; }
        [Display(Name = "DECIL RANK")]
        public string DecilRankAdela { get; set; }
        [Display(Name = "Importe Total")]
        public decimal ImporteTotal { get; set; }

        [Display(Name = "Fecha revisión")]
        [DisplayFormat(DataFormatString = "{0:d}")]        
        public DateTime fecha_revision { get; set; }

        [Display(Name = "Fecha inicio")]        
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime fecha_inicio { get; set; }

        [Display(Name = "Fecha término")]       
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime fecha_termino { get; set; }

        [Display(Name = "INPC Vigente")]
        [Range(.01, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public decimal inpc { get; set; }

        [Display(Name = "% Sugerido para Negociar")]
        [Range(.01, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public decimal porcentaje_negociar { get; set; }

        #endregion

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
        [DefaultValue("")]
        public string estado { get; set; }

        [Display(Name = "Alcaldía / Municipio")]
        [DefaultValue("")]
        public string municipio { get; set; }

        [Display(Name = "Colonia")]
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

        public string Observaciones { get; set; }

        #region "Representante ..."

        [Display(Name = "Escritura Publica Acta Const.")]
        public string EscrituraPublicaActaConst { get; set; }
        [Display(Name = "Número del Notario Acta Const.")]
        public string NumeroNotarioActaConst { get; set; }
        [Display(Name = "Domicilio de la Empresa")]
        public string DomicilioEmpresa { get; set; }
        [Display(Name = "Fecha Poder Notarial")]
        public DateTime FechaPoderNotarial { get; set; }
        [Display(Name = "Fecha Escritura Acta Const.")]
        public DateTime FechaEscrituraActaConst { get; set; }
        [Display(Name = "Número Folio Mercantil Acta Const.")]
        public string NumeroFolioMercantilActaConst { get; set; }
        [Display(Name = "RFC Empresa")]
        public string RFCEmpresa { get; set; }
        [Display(Name = "Número Notario del Poder Notaria")]
        public string NumeroNotarioPoderNotaria { get; set; }
        [Display(Name = "Nombre Notario Acta Const.")]
        public string NombreNotarioActaConst { get; set; }
        [Display(Name = "Fecha Folio Acta Const.")]
        public DateTime FechaFolioActaConst { get; set; }
        [Display(Name = "Nombre Representante Legal")]
        public string NombreRepresentanteLegal { get; set; }
        [Display(Name = "Nombre Notario Poder Notarial")]
        public string NombreNotarioPoderNotarial { get; set; }

        #endregion

        #region "Plazos ..."

        [Display(Name = "Años")]
        public int plazoPFAnio { get; set; }
        [Display(Name = "Meses")]
        public int plazoPFMes { get; set; }
        [Display(Name = "Días")]
        public int plazoPFdia { get; set; }
        [Display(Name = "Años")]
        public int plazoIFAnio { get; set; }
        [Display(Name = "Meses")]
        public int plazoIFMes { get; set; }
        [Display(Name = "Días")]
        public int plazoIFDia { get; set; }
        [Display(Name = "Años")]
        public int plazoPVAnio { get; set; }
        [Display(Name = "Meses")]
        public int plazoPVMes { get; set; }
        [Display(Name = "Días")]
        public int plazoPVDia { get; set; }
        [Display(Name = "Años")]
        public int plazoIVAnio { get; set; }
        [Display(Name = "Meses")]
        public int plazoIVMes { get; set; }
        [Display(Name = "Días")]
        public int plazoIVDia { get; set; }


        [Display(Name = "Días de aveniencia")]
        public int DAPF { get; set; }
        [Display(Name = "Días de aveniencia")]
        public int DAPV { get; set; }
        [Display(Name = "Días de aveniencia")]
        public int DAIF { get; set; }
        [Display(Name = "Días de aveniencia")]
        public int DAIV { get; set; }

        #endregion

        public B_inmuebles inmueble { get; set; }
    }
}
