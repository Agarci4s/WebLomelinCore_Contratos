using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace WebLomelinCore.Models
{
    public class cat_Predial
    {
        public int Id { get; set; }
        [Display(Name = "Inmueble")]
        public int IdInmueble { get; set; }
        [Display(Name = "Localidad")]
        public int IdLocalidad { get; set; }
        [Display(Name = "Periodicidad")]
        public int IdPeriodicidad { get; set; }
        [Display(Name = "Cuenta Predial")]
        public string CuentaPredial { get; set; }
        [Display(Name = "M2 de Terreno")]
        public double M2Terreno { get; set; }
        [Display(Name = "Valor Catastral del Inmueble")]
        public double ValorAvaluoCatastral { get; set; }
        [Display(Name = "Clave Corredor")]
        public string ClaveCorredor { get; set; }
        public string TipoUso { get; set; }
        [Display(Name = "Niveles")]
        public string Nivel { get; set; }
        [Display(Name = "Clase")]
        public string Clase { get; set; }
        [Display(Name = "M2 de Contrucción")]
        public double M2Construccion { get; set; }
        
        [Display(Name = "Antiguedad")]        
        public int Antiguedad { get; set; }
        public List<cat_Predial_TipoUsos> TipoUsos { get; set; }

        //Auxiliares

        [Display(Name = "Inmueble")]
        public string InmuebleAux { get; set; }
        [Display(Name = "Localidad")]
        public string LocalidadAux { get; set; }
        [Display(Name = "Periodicidad")]
        public string PeriodicidadAux { get; set; }
        public int IdUsuarioAlta { get; set; }
        public string UsuarioAlta { get; set; }
        public int IdUsuarioUpdate { get; set; }
        public string UsuarioUpdate { get; set; }
    }
}
