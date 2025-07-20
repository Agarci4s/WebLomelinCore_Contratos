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
    public class B_inmuebles_visitas
    {
        public int id_b_inmuebles_visita { get; set; }

        [Display(Name = "Inmueble")]
        public int id_b_inmuebles { get; set; }

        [Display(Name = "Estatus de la visita")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int estatus_visita { get; set; }

        [Display(Name = "Fecha de la visita")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Remote(action: "VerificaFecha", controller: "B_inmuebles_visitas", AdditionalFields = nameof(id_b_inmuebles) + "," + nameof(id_b_inmuebles_visita))]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime fecha_visita { get; set; }

        [Display(Name = "Mampara de seguridad")]
        public bool mapa_seguridad { get; set; }

        [Display(Name = "Inclusión")]
        public bool inclusion { get; set; }

        [Display(Name = "Total de anuncios")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int no_anuncios { get; set; }

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

        [Display(Name = "Renta de contrato")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal contrato_renta { get; set; }

        [Display(Name = "Precio por M.2")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_sucursal_precio { get; set; }

        [Display(Name = "M.2 totales")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal m2_totales { get; set; }

        [Display(Name = "No. de comparables")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int no_comparables { get; set; }

        [Display(Name = "Valores de mercado")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal valor_mercado { get; set; }

        [Display(Name = "Precio por M.2 capitalización rango inferior")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_rango_inf { get; set; }

        [Display(Name = "Precio por M.2 capitalización rango superior")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_rango_sup { get; set; }

        [Display(Name = "Valores del mercado")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int id_b_cg_valor_mercado { get; set; }

        [Display(Name = "Estado de conservación del inmueble")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int id_b_cg_conservacion { get; set; }

        [Display(Name = "Mantenimiento")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string mantenimiento { get; set; }

        [Display(Name = "Aire acondicionado")]
        public bool aire_acondicionado { get; set; }

        [Display(Name = "Aire acondicionado")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string aire_acondicionado2 { get; set; }

        [Display(Name = "Planta de emergencia")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string planta_emergencia { get; set; }

        [Display(Name = "Impermeabilización")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string impermeabilizacion { get; set; }

        [Display(Name = "Medidor de energía eléctrica")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string medidor { get; set; }

        [Display(Name = "Dirección medidor de energía eléctrica ")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string medidor_direccion { get; set; }



        [Display(Name = "Toma de agua")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string agua_toma { get; set; }

        [Display(Name = "Dirección toma de agua ")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string agua_toma_direccion { get; set; }

        [Display(Name = "Practicajas totales")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int practicajas { get; set; }

        [Display(Name = "En servicio")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int practicajas_servicio { get; set; }

        [Display(Name = "Ventanillas totales")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int ventanillas { get; set; }

        [Display(Name = "En servicio")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int ventanillas_servicio { get; set; }

        [Display(Name = "Cajeros automáticos totales")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int cajeros_automaticos { get; set; }

        [Display(Name = "En servicio")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int cajeros_automaticos_servicio { get; set; }


        [Display(Name = "Cajones de estacionamiento")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int cajones_estacionamiento { get; set; }

        [Display(Name = "Cajones de estacionamiento en servicio")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int cajones_estacionamiento_servicio { get; set; }

        [Display(Name = "Cajones ejecutivos")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int cajones_estacionamiento_ejecutivo { get; set; }

        [Display(Name = "Ocupados por ejecutivos")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int cajones_estacionamiento_ejecutivo_servicio { get; set; }

        [Display(Name = "Operado por")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(255, ErrorMessage = "Agregue un valor valido")]
        public string operador_estacionamiento { get; set; }

        //[Display(Name = "Operador estacionamiento ejecutivo")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        //[MaxLength(255, ErrorMessage = "Agregue un valor valido")]
        //public string operador_estacionamiento_servicio { get; set; }

        [Display(Name = "Libre")]
        public bool libre { get; set; }

        [Display(Name = "Cortesías")]
        public bool cortesias { get; set; }

        [Display(Name = "Concesionaria")]
        public bool concesionaria { get; set; }

        [Display(Name = "Valet parking")]
        public bool valet_parking { get; set; }

        [Display(Name = "Personal exterior")]
        public bool personal_exterior { get; set; }

        [Display(Name = "Demanda de estacionamiento")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string demanda_estacionamiento { get; set; }

        //Fotos
        [Display(Name = "Sucursal")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")] public string sucursal { get; set; }

        [Display(Name = "Geolocalización")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string geolocalizacion { get; set; }

        [Display(Name = "Generales (exterior 1)")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string exterior1 { get; set; }

        [Display(Name = "Generales (exterior 2)")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string exterior2 { get; set; }

        [Display(Name = "Circundantes norte")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string circundante_norte { get; set; }

        [Display(Name = "Circundantes sur")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string circundante_sur { get; set; }

        [Display(Name = "Circundante oriente")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string circundante_oriente { get; set; }

        [Display(Name = "Circundante poniente")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string circundante_poniente { get; set; }

        [Display(Name = "Generales del interior 1")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string interior1 { get; set; }

        [Display(Name = "Generales del interior 2")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string interior2 { get; set; }

        [Display(Name = "Generales de espacios 1")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string espacio1 { get; set; }

        [Display(Name = "Generales de espacios 2")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string espacio2 { get; set; }

        [Display(Name = "Requiere mantenimiento 1")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string mantenimiento1 { get; set; }

        [Display(Name = "Requiere mantenimiento 2")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string mantenimiento2 { get; set; }

        [Display(Name = "Planta de emergencia")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string planta_emergencia2 { get; set; }

        [Display(Name = "Medidor de energía eléctrica")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string medidor_energia { get; set; }

        [Display(Name = "Toma de agua")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string toma_agua { get; set; }

        [Display(Name = "Impermeabilizante")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string impermeabilizante { get; set; }

        [Display(Name = "Arquitectónico")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string arquitectonico { get; set; }


        [Display(Name = "Inclusión")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string inclusion2 { get; set; }

        [Display(Name = "Estacionamiento")]
        [MinLength(5, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(150, ErrorMessage = "Agregue un valor valido")]
        public string estacionamiento { get; set; }




        [Display(Name = "Observaciones")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(800, ErrorMessage = "Agregue un valor valido")]
        public string observaciones { get; set; }

        [Display(Name = "Conclusiones")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(800, ErrorMessage = "Agregue un valor valido")]
        public string conclusiones { get; set; }

        [Display(Name = "Vinculo a mapa")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(250, ErrorMessage = "Agregue un valor valido")]
        public string vinculo_mapa { get; set; }




        [Display(Name = "M.2 terreno")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal m2_terreno { get; set; }

        [Display(Name = "Área rentable")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal area_rentable { get; set; }

        [Display(Name = "M.2 construcción")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal m2_construccion { get; set; }

        [Display(Name = "Superficie estacionamiento")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal superficie_estacionamiento { get; set; }

        [Display(Name = "Valor comercial por capitalización")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public decimal valor_comercial { get; set; }

        [Display(Name = "Variación de inmueble")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int id_b_variacion_inmueble { get; set; }

        [Display(Name = "Precio por M.2 del mercado rango inferior (inmueble)")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_rango_inf2 { get; set; }

        [Display(Name = "Precio por M.2 del mercado rango superior (inmueble)")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_rango_sup2 { get; set; }

        [Display(Name = "Precio por M.2 del mercado rango inferior (estacionamiento)")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_rango_inf3 { get; set; }

        [Display(Name = "Precio por M.2 del mercado rango superior (estacionamiento)")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_rango_sup3 { get; set; }


        [Display(Name = "Revisado")]
        public bool revisado { get; set; }

        [Display(Name = "Visitado")]
        public bool vistado { get; set; }

        [Display(Name = "Especial")]
        public bool especial { get; set; }

        [Display(Name = "Otro")]
        [MaxLength(100, ErrorMessage = "Agregue un valor valido")]
        public string especial_otro { get; set; }

        [Display(Name = "NSE")]
        [DefaultValue("")]
        [MaxLength(3, ErrorMessage = "Agregue un valor valido")]
        public string NSE_1 { get; set; }

        [Display(Name = "NSE+")]
        [DefaultValue("")]
        [MaxLength(3, ErrorMessage = "Agregue un valor valido")]
        public string NSE_2 { get; set; }


        [Display(Name = "Vandalizado")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string vandalizado { get; set; }

        [Display(Name = "Invadido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string invadido { get; set; }

        [Display(Name = "Desocupado")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string desocupado { get; set; }

        [Display(Name = "Estacionamiento")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string estacionamiento2 { get; set; }


        [Display(Name = "Tipificación")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string tipificacion { get; set; }

        [Display(Name = "Asentamiento")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string asentamiento { get; set; }


        [Display(Name = "Tendencia")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string tendencia { get; set; }


        [Display(Name = "Segmento")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string segmento { get; set; }

        [Display(Name = "Áreas de convivencia")]
        [MaxLength(5, ErrorMessage = "Agregue un valor valido")]
        public string areas_convivencia { get; set; }

        [Display(Name = "Áreas descripción")]
        [MaxLength(800, ErrorMessage = "Agregue un valor valido")]
        public string areas_convivencia_desc { get; set; }


        //Auxiliares
        public B_inmuebles b_inmuebles;
        public List<B_inmuebles_comparativo> b_inmuebles_comparativo;
        public List<B_inmuebles_visitas_motivo_especial> b_inmuebles_visitas_motivo_especial;
        

    }

}

