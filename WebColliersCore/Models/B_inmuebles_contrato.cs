using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WebColliersCore.Models
{
    public class B_inmuebles_contrato
    {
        public int id_b_inmuebles_contrato { get; set; }

        public int id_b_inmuebles { get; set; }

        [Display(Name = "Tipo contrato")]
        [Range(1, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int id_b_cg_contrato_tipo { get; set; }

        [Display(Name = "Estatus")]
        [Range(1, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int id_b_cg_contrato_estatu { get; set; }

        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Contrato")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string contrato { get; set; }

        [Display(Name = "Renta")]
        [Range(.01, double.MaxValue, ErrorMessage = "El importe mínimo es de 0.01")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal renta { get; set; }

        [Display(Name = "Fecha inicio")]
        [Remote(action: "VerificaFechaInicio", controller: "B_inmueblesContrato", AdditionalFields = nameof(id_b_inmuebles) + "," + nameof(id_b_inmuebles_contrato))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime fecha_inicio { get; set; }
        public string fechainicio { get; set; }

        [Display(Name = "Fecha término")]
        [Remote(action: "VerificaFechaTermino", controller: "B_inmueblesContrato", AdditionalFields = nameof(fecha_inicio))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime fecha_termino { get; set; }
        public string fechatermino { get; set; }

        [Display(Name = "Días de anticipación para negociación")]
        [Remote(action: "VerificaAnticipacion", controller: "B_inmueblesContrato", AdditionalFields = nameof(fecha_inicio) + "," + nameof(fecha_termino))]
        [Range(30, 365, ErrorMessage = "El rango de días debe estar entre 30 y 365")]
        public int fecha_anticipacion { get; set; }

        [Display(Name = "Fecha obligado")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime fecha_obligado { get; set; }

        [Display(Name = "Litigio y consignación")]
        public bool litigio { get; set; }

        [Display(Name = "Observación")]
        [MaxLength(799, ErrorMessage = "Agregue un valor valido")]
        public string observaciones { get; set; }



        [Display(Name = "Fecha revisión")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Remote(action: "VerificaRevision", controller: "B_inmueblesContrato", AdditionalFields = nameof(fecha_inicio) + "," + nameof(fecha_termino))]
        public DateTime fecha_revision { get; set; }
        public string fecharevision { get; set; }

        [Display(Name = "Plazo revisión")]
        [Range(1, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string plazo { get; set; }

        [Display(Name = "Base revisión")]
        [Range(1, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string base_revision { get; set; }

        [Display(Name = "Redacción de correo")]
        [MaxLength(799, ErrorMessage = "Agregue un valor valido")]
        public string correo_especial { get; set; }

        [Display(Name = "Especial")]
        public bool especial { get; set; }

        [Display(Name = "Fecha de envío")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime fecha_envio { get; set; }

        [Display(Name = "INCP adicional")]
        [Range(.01, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        // [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal incp { get; set; }

        [Display(Name = "Otro")]
        [Range(1, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string base_revision_otro { get; set; }
        
        [Display(Name = "Contrato")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        //[MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string contrato_pdf { get; set; }



        //aux
        [Display(Name = "Contrato PDF")]
        public IFormFile File { get; set; }

        [Display(Name = "Tipo")]
        public string tipo { get; set; }

        [Display(Name = "Estatus")]
        public string estatus { get; set; }


        [Display(Name = "CR")]
        public string cr { get; set; }


        [Display(Name = "UE")]
        public string ue { get; set; }

        /*
          ==================================================================================
          |             Campos Migrados del Catálogo de Localidades                        |
          ==================================================================================  
        */

        [Display(Name = "Cantidad de Locales Agrupados")]
        public int LocalesAgrupados { get; set; }
        [Display(Name = "Propio o Terceros")]
        public string PropTer { get; set; }
        [Display(Name = "Depósito de Renta")]
        public double DepRta { get; set; }
        [Display(Name = "Pagaré")]
        public string Pagare { get; set; }
        [Display(Name = "Día Límite de Pago")]
        public int DiaPago { get; set; }
        [Display(Name = "Categoría")]
        public int categoria { get; set; }
        [Display(Name = "Observaciones Depósito en Garantía")]
        public string ObservacionDepGarantia { get; set; }
        [Display(Name = "Número de Referencia (Renta)")]
        public string NumeroReferenciado { get; set; }
        [Display(Name = "Número de Referencia (Mtto)")]
        public string NumeroReferenciadoMtto { get; set; }
        [Display(Name = "Fecha de Ocupación")]
        public DateTime fechaOcupacion { get; set; }
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
        [Display(Name = "Porcentaje Adicional Revisión")]
        public double revisionPorcentajeAdicional { get; set; }
        [Display(Name = "Comentarios de Revisión")]
        public string ComentariosRevision { get; set; }

        public bool Check { get; set; }

        /*
          ==================================================================================          
        */

        public B_inmuebles b_inmuebles;
        public List<B_inmuebles_contrato_correos> b_Inmuebles_Contrato_Correos;


    }
}
