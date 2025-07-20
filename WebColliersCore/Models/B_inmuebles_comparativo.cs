using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore.Query;

namespace WebColliersCore.Models
{
    public class B_inmuebles_comparativo
    {
        public int id_b_inmuebles_comparativo { get; set; }
        public int id_b_inmuebles { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        public string nombre { get; set; }

        [Display(Name = "Calle / AV")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        public string direccion { get; set; }

        [Display(Name = "CP")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [RegularExpression("^[0-9]{5}$", ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:D5}", ApplyFormatInEditMode = true)]
        public int cp { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        public string estado { get; set; }

        [Display(Name = "Municipio")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        public string municipio { get; set; }
           
        [Display(Name = "Colonia")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(45, ErrorMessage = "Agregue un valor valido")]
        public string colonia { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string noext { get; set; }

        [Display(Name = "Manzana")]
        [MaxLength(45, ErrorMessage = "Agregue un valor valido")]
        public string manzana { get; set; }

        [Display(Name = "Lote")]
        [MaxLength(25, ErrorMessage = "Agregue un valor valido")]
        public string lote { get; set; }

        [Display(Name = "Entre calle")]
        [MaxLength(255, ErrorMessage = "Agregue un valor valido")]
        public string referencia_calle { get; set; }

        [Display(Name = "Y calle")]
        [MaxLength(255, ErrorMessage = "Agregue un valor valido")]
        public string referencia_calle2 { get; set; }

        [Display(Name = "Latitud")]
        [Range(-999, 999, ErrorMessage = "Agregue un valor valido")]
        public double latidud { get; set; }

        [Display(Name = "Longitud")]
        [Range(-999, 999, ErrorMessage = "Agregue un valor valido")]

        public double longitud { get; set; }

        [Display(Name = "Link Google Maps")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(500, ErrorMessage = "Agregue un valor valido")]
        public string link_maps { get; set; }

        [Display(Name = "Estacionamiento")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        public bool estacionamiento { get; set; }

        [Display(Name = "M.2 construcción")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal m2_construccion { get; set; }

        [Display(Name = "M.2 precio")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal m2_precio { get; set; }

        [Display(Name = "Renta")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        //[Range(0, double.MaxValue, ErrorMessage = "Agregue un valor valido")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal renta { get; set; }

        [Display(Name = "Foto")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(4, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(255, ErrorMessage = "Agregue un valor valido")]
        public string foto { get; set; }

        [Display(Name = "Foto")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MinLength(4, ErrorMessage = "Agregue un valor valido")]
        [MaxLength(255, ErrorMessage = "Agregue un valor valido")]
        public string foto2 { get; set; }

        [Display(Name = "Región")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(1, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int id_b_cg_region { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [Range(1, int.MaxValue, ErrorMessage = "Agregue un valor valido")]
        public int id_b_cg_categorias { get; set; }


        //Auxiliares
        [Display(Name = "Región")]
        public string region { get; set; }

    }
}
