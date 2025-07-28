using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace WebLomelinCore.Models
{
    public class PagoUnificadoDTO
    {
        public List<pagosagua> PagosAgua { get; set; }
        public List<pagosluz> PagosLuz { get; set; }
        public List<pagospredial> PagosPredial { get; set; }

        public int Id { get; set; }            // ID del pago
        public int IdCuenta { get; set; }      // ID de la cuenta
        public double Importe { get; set; }    // Monto pagado
        public string TipoServicio { get; set; } // "1"=Agua, "2"=Luz, "3"=Predial
        public int StatusProceso { get; set; }   // 1=Autorizado, 2=Rechazado, etc.
        
        public List<SelectListItem> getTipoSerivcios 
        { 
            get
                {
                   return  new PagosServicios().getTipoSerivcios;
                }
        }

        public PagoUnificadoDTO getServicios(int? idCuenta, int idServicio)
        {
            PagoUnificadoDTO response = new PagoUnificadoDTO();
            if (idServicio == 1)
            {
                response.PagosAgua = new pagosagua().GetPagosaguas(idCuenta);
            }
            if (idServicio == 2)
            {
                response.PagosLuz = new pagosluz().GetPagosluzs();
            }
            return response;
        }
    }
}
