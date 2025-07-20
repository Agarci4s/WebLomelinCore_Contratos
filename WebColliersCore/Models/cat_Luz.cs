using System;
using System.ComponentModel.DataAnnotations;

namespace WebLomelinCore.Models
{
    public class cat_Luz
    {
        public int Id { get; set; }
        [Display(Name = "Inmueble")]
        public int IdInmueble { get; set; }
        [Display(Name = "Localidad")]
        public int IdLocalidad { get; set; }
        [Display(Name = "Periodicidad")]
        public int IdPeriodicidad { get; set; }
        [Display(Name = "Cuenta de Luz")]
        public string CuentaLuz { get; set; }
        [Display(Name = "Número de Servicio")]
        public int NumeroServicio { get; set; }
        [Display(Name = "Número de Medidor")]
        public int NumeroMedidor { get; set; }

        [Display(Name = "Tipo de Tarifa")]
        public int TipoTarifa { get; set; } // 1 = Domestica, 2 = Comercial, 3 = Industrial        
        public string Tarifa => TipoTarifa switch
        {
            1 => "Domestica",
            2 => "Industrial",
            _ => "-"
        };
        [Display(Name = "Fecha de Corte")]
        public DateTime FechaCorte { get; set; }
        [Display(Name = "Fecha Límite de Pago")]
        public DateTime FechaLimitePago { get; set; }
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
