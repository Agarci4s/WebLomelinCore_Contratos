using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace WebColliersCore.Models
{
    public class B_inmuebles
    {
        public int id_b_inmuebles { get; set; }

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

        [Display(Name = "Entre calle")]
        [DefaultValue("")]
        [MaxLength(500, ErrorMessage = "Agregue un valor valido")]
        public string referencia_calle { get; set; }

        [Display(Name = "Y calle")]
        [DefaultValue("")]
        [MaxLength(255, ErrorMessage = "Agregue un valor valido")]
        public string referencia_calle2 { get; set; }

        [Display(Name = "Latitud")]
        [Range(-999, 999, ErrorMessage = "Agregue un valor valido")]
        public double latidud { get; set; }

        [Display(Name = "Longitud")]
        [Range(-999, 999, ErrorMessage = "Agregue un valor valido")]
        public double longitud { get; set; }

        [Display(Name = "Link Google Maps")]
        [DefaultValue("")]
        [MaxLength(500, ErrorMessage = "Agregue un valor valido")]

        public string link_maps { get; set; }

        [Display(Name = "Valor comercial por capitalización de rental")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal valor_comercial { get; set; }

        [Display(Name = "Regimen")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(1, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int id_b_cg_regimen { get; set; }

        [Display(Name = "Estatus")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(1, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int id_b_cg_inmuebles_estatus { get; set; }

        [Display(Name = "Clasificación")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(1, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int id_b_cg_clasificacion { get; set; }

        [Display(Name = "Región")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(1, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int id_b_cg_region { get; set; }

        [Display(Name = "Subclasificación")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int id_b_cg_subclasificacion { get; set; }
        

        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(1, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int IDCARTERA { get; set; }

        [Display(Name = "M.2 terreno")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal m2_terreno { get; set; }

        [Display(Name = "Área rentable")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal area_rentable { get; set; }

        [Display(Name = "M.2 construcción")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public decimal m2_construccion { get; set; }

        [Display(Name = "M.2 estacionamiento")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal m2_estacionamiento { get; set; }

        [Display(Name = "Cajones de estacionamiento")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int cajones_estacionamiento { get; set; }

        [Display(Name = "Cajones de estacionamiento ejecutivo")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int cajones_estacionamiento_ejecutivo { get; set; }

        [Display(Name = "Operador del estacionamiento")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        public string operador_estacionamiento { get; set; }


        [Display(Name = "Opera estacionamiento")]
        public bool opera_estacionamiento { get; set; }

        [Display(Name = "Valet parking")]
        public bool valet_parking { get; set; }


        [Display(Name = "Cajeros automáticos")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int cajeros_automaticos { get; set; }

        [Display(Name = "Practicajas")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int practicajas { get; set; }

        [Display(Name = "Ventanilla")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int ventanillas { get; set; }

        [Display(Name = "Precio del contrato")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal contrato_renta { get; set; }

        [Display(Name = "M.2 precio")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_precio { get; set; }

        [Display(Name = "Rango inferior")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_rango_inf { get; set; }

        [Display(Name = "Rango superior")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_rango_sup { get; set; }

        [Display(Name = "Valor del mercado")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [Range(1, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int id_b_cg_valor_mercado { get; set; }

        [Display(Name = "Estado de la impermeabilización")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string estado_impermeabilizacion { get; set; }


        [Display(Name = "Medidor de luz")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string estado_medidor_energia_electrica { get; set; }

        [Display(Name = "Aire acondicionado")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string aire_acondiconado { get; set; }

        [Display(Name = "Letreros")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string letreros { get; set; }

        [Display(Name = "Contrato")]
        //[Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string contrato { get; set; }

        [Display(Name = "Renta")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal renta { get; set; }

        [Display(Name = "Fecha inicio")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        public DateTime fecha_inicio { get; set; }

        [Display(Name = "Fecha término")]
        public DateTime fecha_termino { get; set; }

        [Display(Name = "Fecha obligado")]
        public DateTime fecha_obligado { get; set; }

        [Display(Name = "Año de ingreso")]
        [Range(0, 2100, ErrorMessage = "Agregue un valor valido")]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:D4}", ApplyFormatInEditMode = true)]
        public int ingreso { get; set; }

        [Display(Name = "Propietario")]
        public int IdPropietario { get; set; }





        //auxiliares
        [Display(Name = "Clasificación")]
        public string clasificacion { get; set; }

        [Display(Name = "Regimen")]
        public string regimen { get; set; }



        //Borrar
        [Display(Name = "Derecho de agua")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal agua { get; set; }

        [Display(Name = "Energía eléctrica")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal luz { get; set; }

        [Display(Name = "Impuesto predial")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal predial { get; set; }

        [Display(Name = "M.2 bodega")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_bodega { get; set; }

        [Display(Name = "M.2 totales construcción")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal m2_local { get; set; }

        [Display(Name = "Cajones de estacionamiento pensión")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int cajones_pensiones { get; set; }

        [Display(Name = "Boletos de cortesía")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(0, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int cortesias_boletos { get; set; }



    }
}
