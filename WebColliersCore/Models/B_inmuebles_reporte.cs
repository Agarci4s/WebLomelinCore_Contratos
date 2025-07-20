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

namespace WebColliersCore.Models
{
    public class B_inmuebles_reporte
    {
        [Display(Name = "UE")]
        public string ue { get; set; }


        [Display(Name = "CR")]
        public string cr { get; set; }

        [Display(Name = "Nombre de sucursal")]
        public string nombre { get; set; }

        [Display(Name = "Región")]
        public string region { get; set; }

        [Display(Name = "Estatus")]
        public string inmuebles_estatus { get; set; }

        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Display(Name = "Latitud")]
        public double latidud { get; set; }

        [Display(Name = "Longitud")]
        public double longitud { get; set; }

        [Display(Name = "Link a Google Maps")]
        public string link_maps { get; set; }


    }

}

