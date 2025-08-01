using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebColliersCore.Models;

namespace WebLomelinCore.Models
{
    public class pagosluz
    {
        public int idDtPagosLuz { get; set; }//
        public int idCgCuentaLuz { get; set; }//
        public string CuentaLuz { get; set; }

        [Required(ErrorMessage = "Ingrese la fecha de pago")]
        [Display(Name = "fecha de Pago")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime fechaPago { get; set; }

        [Display(Name = "Periodo de pago")]
        [Required(ErrorMessage ="Ingrese el periodo de pago")]
        public string periodoPago { get; set; }//

        [Display(Name = "Importe")]
        [Required(ErrorMessage ="Ingrese el importe")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "Ingrese un importe válido")]
        public double importe { get; set; }

        [Display(Name ="Iva")]
        [Required(ErrorMessage ="Ingrese el iva")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage ="Ingrese el iva válido")]
        public double iva { get; set; }

        [Display(Name ="Concepto pago")]
        public string conceptoPago { get; set; }

        [Required(ErrorMessage = "Ingrese línea de captura")]
        [Display(Name = "Línea de captura")]
        public string LineaCaptura { get; set; }
        
        public int UsuarioAutoriza { get; set; }
        public string UsuarioAutorizaDescripcion { get; set; }
        public int StatusProceso { get; set; }
        public string StatusProcesoDescripcion { get; set; }

        /*DATOS DEL FORMULARIO*/


        public string autorizacionPago { get; set; }//
        public string traspaso { get; set; }
        public string TipoPago { get; set; }
        public string comentarioNoPagarServicios { get; set; }
        public string traspasoIndividual { get; set; }
        public string Numero { get; set; }
        public string LineaCapturaCompleta { get; set; }
        
        //public List<pagosluz> GetPagosluzs(int? idCuenta)
        //{
        //    List<pagosluz> response = new List<pagosluz>
        //    {
        //        new pagosluz {idDtPagosLuz = 1, idCgCuentaLuz = 200, periodoPago = "2024-01", importe = 3500.00, status = 1},
        //        new pagosluz {idDtPagosLuz= 2, idCgCuentaLuz = 2001, periodoPago = "2024-02", importe = 4200.50, status = 2 },
        //        new pagosluz {idDtPagosLuz = 3, idCgCuentaLuz = 2002, periodoPago = "2024-03", importe = 2800.16, status = 3 }
        //    };

        //    if (idCuenta.HasValue)
        //    {
        //        return response
        //            .Where(x => x.idCgCuentaLuz == idCuenta).ToList();
        //    }
        //    else
        //    {
        //        return response;
        //    }
        //}
    }
}
