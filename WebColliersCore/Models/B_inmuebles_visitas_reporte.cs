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
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;

namespace WebColliersCore.Models
{
    public class B_inmuebles_visitas_reporte
    {
        [Display(Name = "UE")]
        [DisplayName("UE")]
        public string ue { get; set; }


        [Display(Name = "CR")]
        [DisplayName("CR")]
        public string cr { get; set; }

        [Display(Name = "No. de contrato ")]
        [DisplayName("No. de contrato ")]
        public string contrato { get; set; }

        [Display(Name = "Nombre de sucursal")]
        [DisplayName("Nombre de sucursal")]
        public string nombre { get; set; }

        [Display(Name = "Región")]
        [DisplayName("Región")]
        public string region { get; set; }

        [Display(Name = "Estado")]
        [DisplayName("Estado")]
        public string estado { get; set; }

        [Display(Name = "Alcaldía / Municipio")]
        [DisplayName("Alcaldía / Municipio")]
        public string municipio { get; set; }

        [Display(Name = "Regimen")]
        [DisplayName("Regimen")]
        public string regimen { get; set; }

        [Display(Name = "Estatus")]
        [DisplayName("Estatus")]
        public string inmuebles_estatus { get; set; }

        [Display(Name = "Estatus de la visita")]
        [DisplayName("Estatus de la visita")]
        public string inmuebles_visitas_estatus { get; set; }

        [Display(Name = "Fecha de la visita")]
        [DisplayName("Fecha de la visita")]
        public string fecha_visita { get; set; }


        [Display(Name = "Vandalizado")]
        [DisplayName("Vandalizado")]
        public string vandalizado { get; set; }

        [Display(Name = "Invadido")]
        [DisplayName("Invadido")]
        public string invadido { get; set; }

        [Display(Name = "Desocupado")]
        [DisplayName("Desocupado")]
        public string desocupado { get; set; }


        [Display(Name = "Motivo especial")]
        [DisplayName("Motivo especial")]
        public string motivo_especial { get; set; }

        [Display(Name = "Dirección")]
        [DisplayName("Dirección")]
        public string direccion { get; set; }

        [Display(Name = "Latitud")]
        [DisplayName("Latitud")]
        public double latidud { get; set; }

        [Display(Name = "Longitud")]
        [DisplayName("Longitud")]
        public double longitud { get; set; }

        [Display(Name = "Link a Google Maps")]
        [DisplayName("Link a Google Maps")]
        public string link_maps { get; set; }


        [Display(Name = "Asentamiento")]
        [DisplayName("Asentamiento")]
        public string asentamiento { get; set; }

        [Display(Name = "Tipificación")]
        [DisplayName("Tipificación")]
        public string tipificacion { get; set; }

        [Display(Name = "NSE")]
        [DisplayName("NSE")]
        public string NSE_1 { get; set; }


        [Display(Name = "NSE2")]
        [DisplayName("NSE2")]
        public string NSE_2 { get; set; }



        [Display(Name = "M.2 terreno")]
        [DisplayName("M.2 terreno")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal m2_terreno { get; set; }

        [Display(Name = "Área rentable")]
        [DisplayName("Área rentable")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal area_rentable { get; set; }

        [Display(Name = "M.2 construcción")]
        [DisplayName("M.2 construcción")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal m2_construccion { get; set; }

        [Display(Name = "Superficie estacionamiento")]
        [DisplayName("Superficie estacionamiento")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal superficie_estacionamiento { get; set; }

        [Display(Name = "Mampara de seguridad")]
        [DisplayName("Mampara de seguridad")]
        public string mapa_seguridad { get; set; }

        /// <summary>
        [Display(Name = "Anuncio marquesina")]
        [DisplayName("Anuncio marquesina")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int marquesina { get; set; }






        [Display(Name = "Anuncio letras rotuladas / adosadas ")]
        [DisplayName("Anuncio letras rotuladas / adosadas")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int letras_rotuladas { get; set; }

        [Display(Name = "Anuncio bandera")]
        [DisplayName("Anuncio bandera")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int bandera { get; set; }

        [Display(Name = "Anuncio paleta")]
        [DisplayName("Anuncio paleta")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int paleta { get; set; }

        [Display(Name = "Anuncio unipolar")]
        [DisplayName("Unipolar")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int unipolar { get; set; }

        [Display(Name = "Anuncio tótem")]
        [DisplayName("Tótem")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int totem { get; set; }

        [Display(Name = "No. total de anuncios")]
        [DisplayName("No. total de anuncios")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int no_anuncios { get; set; }
        /// </summary>

        [Display(Name = "Inclusión")]
        [DisplayName("Inclusión")]
        public string inclusion { get; set; }

        [Display(Name = "Aire acondicionado")]
        [DisplayName("Aire acondicionado")]
        public string aire_acondicionado { get; set; }

        [Display(Name = "Planta de emergencia")]
        [DisplayName("Planta de emergencia")]
        public string planta_emergencia { get; set; }

        [Display(Name = "Impermeabilización")]
        [DisplayName("Impermeabilización")]
        public string impermeabilizacion { get; set; }

        [Display(Name = "Medidor de energía eléctrica")]
        [DisplayName("Medidor de energía eléctrica")]
        public string medidor { get; set; }

        [Display(Name = "Dirección medidor de energía eléctrica")]
        [DisplayName("Dirección medidor de energía eléctrica")]
        public string medidor_direccion { get; set; }

        [Display(Name = "Toma de agua")]
        [DisplayName("Toma de agua")]
        public string agua_toma { get; set; }

        [Display(Name = "Dirección toma de agua")]
        [DisplayName("Dirección toma de agua")]
        public string agua_toma_direccion { get; set; }

        [Display(Name = "Practicajas totales")]
        [DisplayName("Practicajas totales")]
        public int practicajas { get; set; }

        [Display(Name = "En servicio")]
        [DisplayName("En servicio")]
        public int practicajas_servicio { get; set; }

        [Display(Name = "Fuera de servicio")]
        [DisplayName("Fuera de servicio")]
        public int practicajas_sin_servicio { get; set; }

        [Display(Name = "Ventanillas totales")]
        [DisplayName("Ventanillas totales")]
        public int ventanillas { get; set; }

        [Display(Name = "En servicio")]
        [DisplayName("En servicio 2")]
        public int ventanillas_servicio { get; set; }

        [Display(Name = "Fuera de servicio")]
        [DisplayName("Fuera de servicio 2")]
        public int ventanillas_sin_servicio { get; set; }

        [Display(Name = "Cajeros automáticos totales")]
        [DisplayName("Cajeros automáticos totales")]
        public int cajeros_automaticos { get; set; }

        [Display(Name = "En servicio")]
        [DisplayName("En servicio 3")]
        public int cajeros_automaticos_servicio { get; set; }

        [Display(Name = "Fuera de servicio")]
        [DisplayName("Fuera de servicio 3")]
        public int cajeros_automaticos_sin_servicio { get; set; }

        [Display(Name = "Cajones de estacionamiento")]
        [DisplayName("Cajones de estacionamiento")]
        public int cajones_estacionamiento { get; set; }

        [Display(Name = "Cajones de estacionamiento en servicio")]
        [DisplayName("Cajones de estacionamiento en servicio")]
        public int cajones_estacionamiento_servicio { get; set; }

        [Display(Name = "Fuera de servicio ")]
        [DisplayName("Fuera de servicio 4")]
        public int cajones_estacionamiento_sin_servicio { get; set; }

        [Display(Name = "Cajones ejecutivos")]
        [DisplayName("Cajones ejecutivos")]
        public int cajones_estacionamiento_ejecutivo { get; set; }

        [Display(Name = "Ocupados por ejecutivos")]
        [DisplayName("Ocupados por ejecutivos")]
        public int cajones_estacionamiento_ejecutivo_servicio { get; set; }

        [Display(Name = "Fuera de servicio")]
        [DisplayName("Fuera de servicio 5")]
        public int cajones_estacionamiento_ejecutivo_sin_servicio { get; set; }

        [Display(Name = "Operado por")]
        [DisplayName("Operado por")]
        public string operador_estacionamiento { get; set; }

        [Display(Name = "Valet parking")]
        [DisplayName("Valet parking")]
        public string valet_parking { get; set; }

        [Display(Name = "Personal exterior")]
        [DisplayName("Personal exterior")]
        public string personal_exterior { get; set; }

        [Display(Name = "Demanda de estacionamiento")]
        [DisplayName("Demanda de estacionamiento")]
        public string demanda_estacionamiento { get; set; }

        [Display(Name = "Renta de contrato")]
        [DisplayName("Renta de contrato")]
        public decimal contrato_renta { get; set; }

        [Display(Name = "Precio por M.2")]
        [DisplayName("Precio por M.2")]
        public decimal m2_sucursal_precio { get; set; }

        [Display(Name = "M.2 totales")]
        [DisplayName("M.2 totales")]
        public decimal m2_totales { get; set; }

        [Display(Name = "No. de comparables")]
        [DisplayName("No. de comparables")]
        public int no_comparables { get; set; }

        [Display(Name = "Valores de mercado")]
        [DisplayName("Valores de mercado")]
        public string valor_mercado { get; set; }

        [Display(Name = "Variación de inmueble")]
        [DisplayName("Variación de inmueble")]
        public string variacion_inmueble { get; set; }


        [Display(Name = "Valor comercial por capitalización")]
        [DisplayName("Valor comercial por capitalización")]
        public decimal valor_comercial { get; set; }


        [Display(Name = "Precio por M.2 capitalización rango inferior")]
        [DisplayName("Precio por M.2 capitalización rango inferior")]
        public decimal m2_rango_inf { get; set; }

        [Display(Name = "Precio por M.2 capitalización rango superior")]
        [DisplayName("Precio por M.2 capitalización rango superior")]
        public decimal m2_rango_sup { get; set; }

        [Display(Name = "Promedio por M.2 de capitalización")]
        [DisplayName("Promedio por M.2 de capitalización")]
        public decimal m2_promedio { get; set; }




        //
        [Display(Name = "Precio por M.2 del mercado rango inferior (inmueble)")]
        [DisplayName("Precio por M.2 del mercado rango inferior(inmueble)")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_rango_inf2 { get; set; }

        [Display(Name = "Precio por M.2 del mercado rango superior (inmueble)")]
        [DisplayName("Precio por M.2 del mercado rango superior (inmueble)")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_rango_sup2 { get; set; }

        [Display(Name = "Promedio (inmueble)")]
        [DisplayName("Promedio (inmueble)")]
        public decimal m2_rango_prom2 { get; set; }




        [Display(Name = "Precio por M.2 del mercado rango inferior (estacionamiento)")]
        [DisplayName("Precio por M.2 del mercado rango inferior (estacionamiento)")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_rango_inf3 { get; set; }

        [Display(Name = "Precio por M.2 del mercado rango superior (estacionamiento)")]
        [DisplayName("Precio por M.2 del mercado rango superior (estacionamiento)")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_rango_sup3 { get; set; }

        [Display(Name = "Promedio (estacionamiento)")]
        [DisplayName("Promedio (estacionamiento)")]
        public decimal m2_rango_prom3 { get; set; }

        [Display(Name = "Tendencia")]
        [DisplayName("Tendencia")]
        public string tendencia { get; set; }



        [Display(Name = "Áreas de convivencia")]
        [DisplayName("Áreas de convivencia")]
        public string areas_convivencia_desc { get; set; }
        



        [Display(Name = "Mantenimiento")]
        [DisplayName("Mantenimiento")]
        public string mantenimiento { get; set; }
        
        [Display(Name = "Estado de conservación")]
        [DisplayName("Estado de conservación")]
        public string conservacion { get; set; }

        [Display(Name = "Observaciones")]
        [DisplayName("Observaciones")]
        public string observaciones { get; set; }

        [Display(Name = "Conclusiones")]
        [DisplayName("Conclusiones")]
        public string conclusiones { get; set; }


      

        //[Display(Name = "Precio por M.2 del mercado rango superior promedio")]
        //[DisplayName("Precio por M.2 del mercado rango superior promedio")]
        //public decimal m2_rango_sup_prom { get; set; }

      
        //[Display(Name = "estacionamiento")]
        //[DisplayName("estacionamiento")]
        //public string estacionamiento2 { get; set; }



        //[Display(Name = "Segmento")]
        //[DisplayName("Segmento")]
        //public string segmento { get; set; }
        

    }

}

