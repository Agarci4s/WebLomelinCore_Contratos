using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using NPOI.SS.Formula.Functions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using WebLomelinCore.Models;
using OfficeOpenXml.FormulaParsing.ExpressionGraph;

namespace WebColliersCore.Models
{
    public class B_inmuebles_expediente_detalle_contratos
    {
        public int id_b_inmuebles_expediente_detalle_contratos { get; set; }

        [Display(Name = "ID de inmueble")]
        public int id_b_inmuebles { get; set; }

        [Display(Name = "Tipo de expediente")]
        public int id_b_cg_tipo_expediente_contratos { get; set; }

        [Display(Name = "Tipo de periodo")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, 50, ErrorMessage = "Agregue un valor valido")]
        public int id_b_cg_periodicidad_contratos { get; set; }

        //[Display(Name = "Circundantes norte")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string ruta { get; set; }

        [Display(Name = "Periodo")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, 50, ErrorMessage = "Agregue un valor valido")]
        public int periodo { get; set; }

        [Display(Name = "Año")]
        [Range(2000, 2050, ErrorMessage = "El año debe estar entre 2000 y 2050")]
        public int anio { get; set; }

        [Display(Name = "Periodo inicial")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Remote(action: "VerificaFecha", controller: "B_inmuebles_visitas", AdditionalFields = nameof(id_b_inmuebles) + "," + nameof(id_b_inmuebles_visita))]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime fecha_periodo_inicio { get; set; }

        [Display(Name = "Periodo final")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Remote(action: "VerificaFecha", controller: "B_inmuebles_visitas", AdditionalFields = nameof(id_b_inmuebles) + "," + nameof(id_b_inmuebles_visita))]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime fecha_periodo_fin { get; set; }

        //Auxiliares
    }

}

