using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebColliersCore.Models;

namespace WebLomelinCore.Models
{
    public class NegociacionesConvenioModificatorio
    {
        public int Id { get; set; }
        [Display(Name = "Inmueble")]
        public int IdInmueble { get; set; }
        [Display(Name = "Contrato")]
        public int IdContrato { get; set; }

        #region Plantilla ...

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

        #endregion

        #region Convenio Modificatorio ...

        [Display(Name = "Número de Prorrateo")]
        public string NumProrrateo { get; set; }        

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
        
        [Display(Name = "Renta Actual")]
        public double RentaActual { get; set; }

        [Display(Name = "Metros de construcción")]
        public double MetrosConstruccion { get; set; }

        [Display(Name = "Superficie de terreno")]
        public double SuperficieTerreno { get; set; }

        [Display(Name = "Fecha de firma")]        
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime FechaFirma { get; set; }

        [Display(Name = "Clausula")]
        public string Clausula {  get; set; }

        public List<NegociacionesClausulas> negociacionesClausulas { get; set; }

        #endregion
    }
}
