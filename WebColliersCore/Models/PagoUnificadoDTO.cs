using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebLomelinCore.Data;

namespace WebLomelinCore.Models
{
    public class PagoUnificadoDTO
    {
        public List<pagosagua> PagosAgua { get; set; }
        public List<pagosluz> PagosLuz { get; set; }
        public List<pagospredial> PagosPredial { get; set; }

        [Required(ErrorMessage = "Seleccione el servicio a consultar")]
        [DisplayName("Servicio")]
        public int IdTipoServicio { get; set; }

        [Required(ErrorMessage = "Seleccione la región")]
        [Display(Name = "Región")]
        public int IdRegion { get; set; }

        [DisplayName("Servicio ")]
        public string TipoServicio { get; set; }
                
        [Display(Name = "Inmueble")]
        public int IdInmueble { get; set; }
                
        [Display(Name = "Localidad")]
        public int IdLocalidad { get; set; }
                
        [Display(Name = "Cuenta")]
        public int idCuenta { get; set; }
        
        [Display(Name = "Estatus")]
        public int IdStatusProceso { get; set; }   // 1=Autorizado, 2=Rechazado, etc.
        public int IdPagoServicio { get; set; }
        public int IdCuentaServicio { get; set; }

        public static PagoUnificadoDTO getPagoServiciosList(
            int? IdInmueble, 
            int? IdLocalidad, 
            int? IdCuenta, 
            int? IdTipoServicio, 
            int? Estatus,
            int? IdCuentaServicio)
        {
            var service = new DataSelectService(); // Crear instancia
            return service.getPagoServiciosList(
                IdInmueble, 
                IdLocalidad, 
                IdCuenta, 
                IdTipoServicio, 
                Estatus,
                IdCuentaServicio);
            
        }        
    }
}

