using System.ComponentModel.DataAnnotations;

namespace WebLomelinCore.Models
{
    public class cat_Agua
    {
        public int Id { get; set; }
        [Display(Name = "Inmueble")]
        public int IdInmueble { get; set; }
        [Display(Name = "Localidad")]
        public int IdLocalidad { get; set; }
        [Display(Name = "Periodicidad")]
        public int IdPeriodicidad { get; set; }
        [Display(Name = "Cuenta de Agua")]
        public string CuentaAgua { get; set; }
        [Display(Name = "Diametro")]
        public float Diametro { get; set; }
        [Display(Name = "Número de Medidor")]
        public int NumeroMedidor { get; set; }

        [Display(Name = "Funciona Medidor")]
        public bool FuncionaMedidor { get; set; } = false;

        [Display(Name = "Tipo de Tarifa")]
        public int TipoTarifa { get; set; } // 1 = Domestica, 2 = Comercial, 3 = Industrial        
        public string Tarifa => TipoTarifa switch
        {
            1 => "Domestica",
            2 => "Comercial",
            3 => "Industrial",
            _ => "-"
        };
        //Auxiliares

        [Display(Name = "Inmueble")]
        public string InmuebleAux { get; set; }
        [Display(Name = "Localidad")]
        public string LocalidadAux { get; set ;}
        [Display(Name = "Periodicidad")]
        public string PeriodicidadAux { get; set; } 
        public int IdUsuarioAlta { get; set; }  
        public string UsuarioAlta { get; set; }
        public int IdUsuarioUpdate { get; set; }
        public string UsuarioUpdate { get; set; }


    }
}
