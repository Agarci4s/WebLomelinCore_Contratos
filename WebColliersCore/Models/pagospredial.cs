using System.Collections.Generic;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace WebLomelinCore.Models
{
    public class pagospredial
    {
        public int idDtPagosPredial { get; set; }//
        public int idCgCuentaPredial { get; set; }//
        public DateTime fechaPago { get; set; }//
        public string periodoPago { get; set; }//
        public double Recargos { get; set; }
        public double Multas { get; set; }
        public double importe { get; set; }//
        public double iva { get; set; }
        public double Actualizacion { get; set; }
        public string conceptoPago { get; set; }
        public string traspasoGastos { get; set; }
        public string autorizacion { get; set; }
        public string Tipo { get; set; }
        public int status { get; set; }
        public string LineaCaptura { get; set; }
        public string BoletaPredial { get; set; }
        public string AComprobante { get; set; }
        public string ArchivoFacPDF { get; set; }
        public string ArchivoFacXML { get; set; }
        public string comentarioNoPagarServicios { get; set; }
        public DateTime FechaNoPagarServicos { get; set; }
        public string traspasoIndividual { get; set; }
        public string tipoPago { get; set; }
        public string Ticket { get; set; }
        public string Numero { get; set; }
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
        public DateTime fechaPagolimite { get; set; }
        public string Observaciones { get; set; }
        public int Nivel { get; set; } // '0: PENDIENTE\r\n2: EN REVISIÓN DE EJECUTIVA\r\n6: NO AUTORIZADO\r\n7: EN ESPERA DE COMPROBANTE',
        public DateTime FechaEnvioEjecutivo { get; set; }
        public DateTime FechaEnvioDireccion { get; set; }
        public DateTime FechaEnvioTesoreria { get; set; }
        public DateTime FechaNoAutoriza { get; set; }
        public string UsrNoAutoriza { get; set; }
        public string MotivoNoAuto { get; set; }
        public string LineaCapturaPago { get; set; }
        public DateTime FechaAltaRegistro { get; set; }
        public DateTime FechaUpdateRegistro { get; set; }

        public List<pagospredial> GetPagoPredials()
        {
            List<pagospredial> pagoServicioPredial = new List<pagospredial>
            {
                new pagospredial { idDtPagosPredial = 1, idCgCuentaPredial = 100, periodoPago = "2024-1", importe = 5500, Nivel = 0 },
                new pagospredial { idDtPagosPredial = 2, idCgCuentaPredial = 101, periodoPago = "2024-2", importe = 7800, Nivel = 2 },
                new pagospredial { idDtPagosPredial = 3, idCgCuentaPredial = 102, periodoPago = "2024-3", importe = 6800, Nivel = 7 }
            };

            var response = new List<pagospredial>();

            foreach (var item in pagoServicioPredial)
            {
                response.Add(new pagospredial
                {
                    idDtPagosPredial = item.idDtPagosPredial,
                    idCgCuentaPredial = item.idCgCuentaPredial,
                    periodoPago = item.periodoPago,
                    importe = item.importe,
                    Nivel = item.Nivel
                });
            }
            return response;
        }
    }
}
