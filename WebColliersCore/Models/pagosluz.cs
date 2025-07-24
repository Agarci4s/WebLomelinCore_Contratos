using System.Collections.Generic;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace WebLomelinCore.Models
{
    public class pagosluz
    {
        public int idDtPagosLuz { get; set; }//
        public int idCgCuentaLuz { get; set; }//
        public DateTime fechaPago { get; set; }
        public string periodoPago { get; set; }//
        public double importe { get; set; }
        public double iva { get; set; }
        public string autorizacionPago { get; set; }//
        public int status { get; set; }
        public string conceptoPago { get; set; }
        public string traspaso { get; set; }
        public string TipoPago { get; set; }
        public string comentarioNoPagarServicios { get; set; }
        public string traspasoIndividual { get; set; }
        public string Numero { get; set; }
        public string LineaCaptura { get; set; }
        public string LineaCapturaCompleta { get; set; }

        public List<pagosluz> GetPagosluzs()
        {
            List<pagosluz> pagoServicioLuz = new List<pagosluz>
            {
                new pagosluz {idDtPagosLuz = 1, idCgCuentaLuz = 200, periodoPago = "2024-01", importe = 3500.00, status = 1},
                new pagosluz {idDtPagosLuz= 2, idCgCuentaLuz = 2001, periodoPago = "2024-02", importe = 4200.50, status = 2 },
                new pagosluz {idDtPagosLuz = 3, idCgCuentaLuz = 2002, periodoPago = "2024-03", importe = 2800.16, status = 3 }
            };



            var response = new List<pagosluz>();

            foreach (var item in pagoServicioLuz)
            {
                response.Add(new pagosluz
                {
                    idDtPagosLuz = item.idDtPagosLuz,
                    idCgCuentaLuz = item.idCgCuentaLuz,
                    periodoPago = item.periodoPago,
                    importe = item.importe,
                    status = item.status
                });
            }
            return response;
        }
    }
}
