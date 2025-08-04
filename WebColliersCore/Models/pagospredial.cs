using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebColliersCore.Models;
using System;


namespace WebLomelinCore.Models
{
    public class pagospredial
    {
       /*DATOS DEL FORMULARIO*/
        public int idPagoPredial { get; set; }

        public int idDtPagosPredial { get; set; }//

        [Required(ErrorMessage = "Seleccione el inmueble")]
        [Display(Name = "Inmueble")]
        public int IdInmueble { get; set; }

        [Required(ErrorMessage = "Seleccione la localidad")]
        [Display(Name = "Localidad")]
        public int IdLocalidad { get; set; }

        [Required(ErrorMessage = "Seleccione la cuenta luz correspondiente")]
        [Display(Name = "Cuenta predial")]
        public int idCuentaPredial { get; set; }

        public int idCgCuentaPredial { get; set; }//
        public string CuentaPredial { get; set; }

        [Required(ErrorMessage = "Ingrese el periodo de pago")]
        [Display(Name = "Periodo de pago")]
        public string periodoPago { get; set; }

        [Display(Name = "Recargos")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "El campo Recargos es incorrecto")]
        public double Recargos { get; set; }

        [Display(Name = "Multas")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "El campo Multa es incorrecto")]
        public double Multas { get; set; }

        [Required(ErrorMessage = "Ingrese el importe")]
        [Display(Name = "Importe")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "Ingrese un importe correcto")]
        public double importe { get; set; }//

        [Required(ErrorMessage = "Ingrese el IVA")]
        [Display(Name = "IVA")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "Ingrese un iva correcto")]
        public double iva { get; set; }

        [Display(Name = "Actualización")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency, ErrorMessage = "El campo Actualización es incorrecto")]
        public double Actualizacion { get; set; }

        [Display(Name = "Concepto de pago")]
        public string conceptoPago { get; set; }

        [Required(ErrorMessage = "Ingrese línea de captura")]
        [Display(Name = "Línea de captura")]
        public string LineaCaptura { get; set; }

        // Fecha límite para realizar el pago
        [Required(ErrorMessage = "Ingrese la fecha límite de pago")]
        [Display(Name = "Fecha límite de pago")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime fechaPagolimite { get; set; }

        [Display(Name = "Línea de captura de pago")]
        public string LineaCapturaPago { get; set; }

        public int UsuarioAutoriza { get; set; }
        [Display(Name = "Usuario")]
        public string UsuarioAutorizaDescripcion { get; set; }
        public int StatusProceso { get; set; }
        [Display(Name = "Estatus")]
        public string StatusProcesoDescripcion { get; set; }
        
        public DateTime FechaAltaRegistro { get; set; }
        public DateTime FechaUpdateRegistro { get; set; }
        public int id_usuario { get; set; }


        [Display(Name = "Importe Total")]
        public double ImporteTotal { get; set; }

        [Display(Name = "Seleccionar")]
        public bool EsSeleccionado { get; set; }

        /*DATOS DEL FORMULARIO*/

        public DateTime fechaPago { get; set; }//
        public string traspasoGastos { get; set; }
        public string autorizacion { get; set; }
        public string Tipo { get; set; }
        public string BoletaPredial { get; set; }
        public string AComprobante { get; set; }
        public string ArchivoFacPDF { get; set; }
        public string ArchivoFacXML { get; set; }
        public string comentarioNoPagarServicios { get; set; }
        public DateTime FechaNoPagarServicos { get; set; }
        public string traspasoIndividual { get; set; }
        public string tipoPago { get; set; }
        public string Ticket { get; set; }
        
        public string NumeroAnt { get; set; }
        public double m2Terreno { get; set; }
        public double ValorUTerreno { get; set; }
        public double ValorTerreno { get; set; }
        public double m2Construccion { get; set; }
        public double ValorUConstruccion { get; set; }
        public double ValorConstruccion { get; set; }
        public double ValorCatastral { get; set; }
        public string ClaveUso { get; set; }
        public string Rango { get; set; }
        public string Clase { get; set; }
        public double ValorMetros { get; set; }
        public int IntalacionEsp { get; set; }
        public int Niveles { get; set; }
        public int Antiguedad { get; set; }
        public string ClaveCorredor { get; set; }
        public string Observaciones { get; set; }
        public int Nivel { get; set; } // '0: PENDIENTE\r\n2: EN REVISIÓN DE EJECUTIVA\r\n6: NO AUTORIZADO\r\n7: EN ESPERA DE COMPROBANTE',
        public DateTime FechaEnvioEjecutivo { get; set; }
        public DateTime FechaEnvioDireccion { get; set; }
        public DateTime FechaEnvioTesoreria { get; set; }
        public DateTime FechaNoAutoriza { get; set; }
        public string UsrNoAutoriza { get; set; }
        public string MotivoNoAuto { get; set; }
        

        public List<pagospredial> GetPagoPredials(int?idCuenta)
        {
            List<pagospredial> response  = new List<pagospredial>
            {
                new pagospredial { idDtPagosPredial = 1, idCgCuentaPredial = 100, periodoPago = "2024-1", importe = 5500, Nivel = 0 },
                new pagospredial { idDtPagosPredial = 2, idCgCuentaPredial = 101, periodoPago = "2024-2", importe = 7800, Nivel = 2 },
                new pagospredial { idDtPagosPredial = 3, idCgCuentaPredial = 102, periodoPago = "2024-3", importe = 6800, Nivel = 7 }
            };
            if (idCuenta.HasValue)
            {
                return response
                    .Where(x => x.idCgCuentaPredial == idCuenta).ToList();
            }
            else
            {
                return response;
            }
        }
    }
}
