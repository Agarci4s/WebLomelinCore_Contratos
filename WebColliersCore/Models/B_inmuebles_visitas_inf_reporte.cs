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
    public class B_inmuebles_visitas_inf_reporte
    {
        [Display(Name = "UE")]
        public string ue { get; set; }


        [Display(Name = "CR")]
        public string cr { get; set; }

        [Display(Name = "Nombre de sucursal")]
        public string nombre { get; set; }

        [Display(Name = "Región")]
        public string region { get; set; }

        [Display(Name = "Regimen")]
        public string regimen { get; set; }

        [Display(Name = "Estatus")]
        public string inmuebles_estatus { get; set; }

        [Display(Name = "Estatus de la visita")]
        public string inmuebles_visitas_estatus { get; set; }

        [Display(Name = "Fecha de la visita")]
        public string fecha_visita { get; set; }

        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Display(Name = "Latitud")]
        public double latidud { get; set; }

        [Display(Name = "Longitud")]
        public double longitud { get; set; }



        [Display(Name = "Marquesina")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int marquesina { get; set; }

        [Display(Name = "Letras rotuladas / adosadas")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int letras_rotuladas { get; set; }

        [Display(Name = "Bandera")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int bandera { get; set; }

        [Display(Name = "Paleta")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int paleta { get; set; }

        [Display(Name = "Unipolar")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int unipolar { get; set; }

        [Display(Name = "Tótem")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int totem { get; set; }

        [Display(Name = "Total de anuncios")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int no_anuncios { get; set; }


    }

}

