
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WebLomelinCore.Models
{
    public class pagosagua
    {
   
        public int idPagoAgua { get; set; }
        public int idCuentaAgua { get; set; }
        public string CuentaAgua { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime fechaLectura1 { get; set; }
        public bool Lectura1 { get; set; }
        public DateTime fechaLectura2 { get; set; }
        public bool Lectura2 { get; set; }
        public string funsionaMedidor { get; set; }
        public double importeHabitacional { get; set; }
        public double importeComercial { get; set; }
        public double ivaComercial { get; set; }
        public double Recargos { get; set; }
        public double Actualizacion { get; set; }
        public double Multas { get; set; }
        public double GastosEjecucion { get; set; }
        public string tipoPago { get; set; }
        public string boletaDeclaracion { get; set; }
        public long localidadesComerciales { get; set; }
        public long localidadesHabitacionales { get; set; }
        public string autorizacionPago { get; set; }
        public int status { get; set; }
        public string traspaso { get; set; }
        public string conceptoPago { get; set; }
        public string comentarioNoPagarServicios { get; set; }
        public DateTime fechaNoPagarServicios { get; set; }
        public string traspasoIndividual { get; set; }
        public string comentarioLectura { get; set; }
        public string Ticket { get; set; }
        public string Numero { get; set; }
        public string NumeroAnt { get; set; }
        public int SinLecturas { get; set; }
        public int statusProceso { get; set; }
        public DateTime FechaEnvioEjecutivo { get; set; }
        public DateTime FechaEnvioDireccion { get; set; }
        public DateTime FechaEnvioTesoreria { get; set; }
        public DateTime FechaNoAutoriza { get; set; }
        public DateTime FechaReactiva { get; set; }
        public string MotivoNoAutoriza { get; set; }
        public string NoAutoriza { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int FolioGestor { get; set; }
        public int FolioDireccion { get; set; }
        public DateTime FechaTraspaso { get; set; }
        public int FormaPago { get; set; }
        public string MotivoReactiva { get; set; }
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
        public string LineaCaptura { get; set; }
        public string NotaAutorizacion { get; set; }
        public string UsrAutoriza { get; set; }
        public double ConsumoBimestral { get; set; }
        public int AplicaBeneficio { get; set; }
        public DateTime FechaAltaRegistro { get; set; }
        public DateTime FechaUpdateRegistro { get; set; }


        public List<pagosagua> GetPagosaguas(int? idCuenta)
        {
            List<pagosagua>  response = new List<pagosagua>
            {
                new pagosagua { idPagoAgua = 1, idCuentaAgua = 100, CuentaAgua = "CUENTA-001", statusProceso = 1 },
                new pagosagua { idPagoAgua = 2, idCuentaAgua = 101, CuentaAgua = "CUENTA-002", statusProceso = 2 }
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
