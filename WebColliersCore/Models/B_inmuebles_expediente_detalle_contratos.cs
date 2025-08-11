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

namespace WebColliersCore.Models
{
    public class B_inmuebles_expediente_detalle_contratos
    {
        public int id_b_inmuebles_expediente_detalle_contratos { get; set; }

        public int id_b_inmuebles { get; set; }

        public int id_b_cg_tipo_expediente_contratos { get; set; }

        public int id_b_cg_periodicidad_contratos { get; set; }

        //[Display(Name = "Circundantes norte")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string ruta { get; set; }

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

