
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebColliersCore.Models;

namespace WebLomelinCore.Models
{
    public class pagosagua
    {
        /* DATOS FORMULARIO */
        public int idPagoAgua { get; set; }

        [Required(ErrorMessage = "Seleccione el Inmueble")]
        [Display(Name = "Inmueble")]
        public int IdInmueble { get; set; }

        [Required(ErrorMessage = "Seleccione la Localidad")]
        [Display(Name = "Localidad")]
        public int IdLocalidad { get; set; }

        [Required(ErrorMessage = "Seleccione la cuenta agua correspondiente")]
        [Display(Name = "Cuenta Agua")]
        public int idCuentaAgua { get; set; }
        [Display(Name = "Cuenta Agua")]
        public string CuentaAgua { get; set; }

        [Required(ErrorMessage = "Ingrese el periodo de pago")]
        [Display(Name = "Periodo Pago")]
        public string periodoPago { get; set; }

        [Required(ErrorMessage ="Ingrese la fecha de lectura 1")]
        [Display(Name = "Fecha Lectura 1")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date, ErrorMessage ="Ingrese una fecha valida")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        //[DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime FechaLectura1 { get; set; }

        [Display(Name = "Lectura 1")]
        [Required(ErrorMessage = "Ingrese Lectura 1")]
        public float Lectura1 { get; set; }

        [Required(ErrorMessage = "Ingrese la fecha de lectura 2")]
        [Display(Name = "Fecha Lectura 2")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FechaLectura2 { get; set; }
        
        [Display(Name = "Lectura 2")]
        [Required(ErrorMessage = "Ingrese Lectura 2")]
        public float Lectura2 { get; set; }

        [Display(Name = "Importe Habitacional")]
        [Required(ErrorMessage = "Ingrese el importe habitacional")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "Ingrese un importe habitacional válidao")]
        public double ImporteHabitacional { get; set; }

        [Display(Name = "Importe Comercial")]
        [Required(ErrorMessage = "Ingrese el importe Cormecial")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "Ingrese un importe comercial válidao")]        
        public double ImporteComercial { get; set; }

        [Display(Name = "I.V.A. Comercial")]
        [Required(ErrorMessage = "Ingrese el I.V.A. Comercial")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "Ingrese el iva comercial válidao")]
        public double IvaComercial { get; set; }

        [Display(Name = "Recargos")]
        //[Required(ErrorMessage = "Ingrese el iva habitacional")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "El campo Recargos es incorrecto")]
        public double Recargos { get; set; }

        [Display(Name = "Actualización")]
        //[Required(ErrorMessage = "Ingrese el iva habitacional")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "El campo Actualización es incorrecto")]
        public double Actualizacion { get; set; }

        [Display(Name = "Multas")]
        //[Required(ErrorMessage = "Ingrese el iva habitacional")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "El campo Multas es incorrecto")]
        public double Multas { get; set; }

        [Display(Name = "Gastos Ejecución")]
        //[Required(ErrorMessage = "Ingrese el iva habitacional")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "El campo Gastos ejecución es incorrecto")]
        public double GastosEjecucion { get; set; }

        [Display(Name = "Concepto Pago")]
        public string ConceptoPago { get; set; } /*conceptopago*/

        [Required(ErrorMessage = "Ingrese Fecha Limite de Pago")]
        [Display(Name = "Fecha Limite de Pago")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage = "Ingrese línea de captura")]
        [Display(Name = "Línea de Captura")]
        public string LineaCaptura { get; set; }

        [Required(ErrorMessage = "Ingrese consumo bimestral")]
        [Display(Name = "Consumo Bimestral")]
        public double ConsumoBimestral { get; set; }

        public int UsuarioAutoriza { get; set; }
        [Display(Name = "Usuario")]
        public string UsuarioAutorizaDescripcion { get; set; }
        
        public int StatusProceso { get; set; }
        [Display(Name = "Estatus")]
        public string StatusProcesoDescripcion { get; set; }

        [Display(Name = "Importe Total")]
        public double ImporteTotal { get; set; }

        [Display(Name = "Seleccionar")]
        public bool EsSeleccionado { get; set; }

        /// <summary>
        /// Datos del inmueble
        /// </summary>
        public B_inmuebles InmuebleData { get; set; }

        /// <summary>
        /// Datos del penultimo consumo 
        /// </summary>
        public pagosagua ConsumoAnterior { get; set; }


        /// <summary>
        /// Escribe un motivo por el cuel reactiva este item al estar en estatus rechazdo
        /// </summary>
        public string MotivoReactiva { get; set; }

        public int id_usuario { get; set; }
        public string status { get; set; }

        public DateTime FechaAltaRegistro { get; set; }
        public DateTime FechaUpdateRegistro { get; set; }

        /* DATOS FORMULARIO */

  
        public DateTime FechaPago { get; set; }        
        public string funsionaMedidor { get; set; }        
        public string boletaDeclaracion { get; set; }
        public long localidadesComerciales { get; set; }
        public long localidadesHabitacionales { get; set; }
        public string autorizacionPago { get; set; }
        public string traspaso { get; set; }
        public string comentarioNoPagarServicios { get; set; }
        public DateTime fechaNoPagarServicios { get; set; }
        public string traspasoIndividual { get; set; }
        public string comentarioLectura { get; set; }
        public string Ticket { get; set; }
        public string NumeroAnt { get; set; }
        public int SinLecturas { get; set; }

        public DateTime FechaEnvioEjecutivo { get; set; }
        public DateTime FechaEnvioDireccion { get; set; }
        public DateTime FechaEnvioTesoreria { get; set; }
        public DateTime FechaNoAutoriza { get; set; }
        public DateTime FechaReactiva { get; set; }
        public string MotivoNoAutoriza { get; set; }
        public string NoAutoriza { get; set; }
        public int FolioGestor { get; set; }
        public int FolioDireccion { get; set; }
        public DateTime FechaTraspaso { get; set; }
        public int FormaPago { get; set; }
        public string Reactiva { get; set; }
        public string ArchivoComprovante { get; set; }
        public string ArchivoFacPDF { get; set; }
        public string ArchivoFacXML { get; set; }
        public string ALinea { get; set; }
        public string AComprobante { get; set; }
        public string ABoleta { get; set; }
        public string ObsGestorRecopila { get; set; }
        public string ObsEjecutivoRecopila { get; set; }
        public string TipoAutorizacion { get; set; }
        public string NotaAutorizacion { get; set; }
        public string UsrAutoriza { get; set; }
        public int AplicaBeneficio { get; set; }
        


        public List<pagosagua> GetPagosaguas(int? idCuenta)
        {
            List<pagosagua>  response = new List<pagosagua>
            {
                new pagosagua { idPagoAgua = 1, idCuentaAgua = 100, CuentaAgua = "CUENTA-001", StatusProceso = 1 },
                new pagosagua { idPagoAgua = 2, idCuentaAgua = 101, CuentaAgua = "CUENTA-002", StatusProceso = 2 }
            };

            if (idCuenta.HasValue)
            {
                return response
                    .Where(x => x.idCuentaAgua == idCuenta).ToList();
            }
            else
            {
                return response;
            }
        }

    }
}
