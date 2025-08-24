using DocumentFormat.OpenXml.VariantTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebColliersCore.Models;


namespace WebLomelinCore.Models
{
    public class pagosluz
    {
        /*DATOS DEL FORMULARIO*/
        public int idPagoLuz { get; set; }
        public int idDtPagosLuz { get; set; }//

        [Required(ErrorMessage = "Seleccione el inmueble")]
        [Display(Name = "Inmueble")]
        public int IdInmueble { get; set; }

        [Required(ErrorMessage = "Seleccione la localidad")]
        [Display(Name = "Localidad")]
        public int IdLocalidad { get; set; }

        [Required(ErrorMessage = "Seleccione la cuenta luz correspondiente")]
        [Display(Name = "Cuenta Luz")]
        public int idCuentaLuz { get; set; }

        public int idCgCuentaLuz { get; set; }
        public string CuentaLuz { get; set; }

        [Required(ErrorMessage = "Ingrese la fecha de pago")]
        [Display(Name = "Fecha de Pago")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime fechaPago { get; set; }

        [Display(Name = "Periodo de Pago")]
        [Required(ErrorMessage ="Ingrese el periodo de pago")]
        public string periodoPago { get; set; }//

        [Display(Name = "Importe")]
        [Required(ErrorMessage ="Ingrese el importe")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "Ingrese un importe válido")]
        public double importe { get; set; }

        [Display(Name ="I.V.A.")]
        [Required(ErrorMessage ="Ingrese el iva")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage ="Ingrese el iva válido")]
        public double iva { get; set; }

        [Display(Name ="Concepto de Pago")]
        public string conceptoPago { get; set; }

        [Required(ErrorMessage = "Ingrese línea de captura")]
        [Display(Name = "Línea de Captura")]
        public string LineaCaptura { get; set; }
        
        public int UsuarioAutoriza { get; set; }
        [Display(Name = "Usuario")]
        public string UsuarioAutorizaDescripcion { get; set; }
        public int StatusProceso { get; set; }

        [Display(Name = "Estatus")]
        public string StatusProcesoDescripcion { get; set; }

        
        public DateTime FechaAltaRegistro { get; set; }
        public DateTime FechaUpdateRegistro { get; set; }


        [Display(Name = "Importe Total")]
        public double ImporteTotal { get; set; }

        [Display(Name = "Seleccionar")]
        public bool EsSeleccionado {  get; set; }

        [Required(ErrorMessage = "Ingrese Fecha Limite de Pago")]
        [Display(Name = "Fecha Limite de Pago")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FechaLimitePago { get; set; }

        [Required(ErrorMessage = "Ingrese Fecha de Corte")]
        [Display(Name = "Fecha de Corte")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FechaCorte { get; set; }

        [Display(Name = "Lectura Anterior")]
        [Required(ErrorMessage = "Ingrese Lectura Anterior")]
        public double LecturaAnterior { get; set; }

        [Display(Name = "Lectura Actual")]
        [Required(ErrorMessage = "Ingrese Lectura Actual")]
        public double LecturaActual { get; set; }

        public B_inmuebles InmuebleData {  get; set; }
        public pagosluz ConsumoAnterior { get; set; }
        public decimal? PorcentajeConsumo { get; set; }
        public string ClassPorcentaje { get; set; }

        /*DATOS DEL FORMULARIO*/


        public string autorizacionPago { get; set; }//
        public string traspaso { get; set; }
        public string TipoPago { get; set; }
        public string comentarioNoPagarServicios { get; set; }
        public string traspasoIndividual { get; set; }
        
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
