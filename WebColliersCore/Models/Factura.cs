using System.ComponentModel.DataAnnotations;

namespace WebLomelinCore.Models
{
    public class Factura
    {
        public string MessageError { get; set; }
        public int RowIndex { get; set; }
        public bool Check { get; set; }

        [Display(Name = "Concepto")]
        public string Concepto { get; set; }

        [Display(Name = "Concepto")]
        public string Proveedor { get; set; }

        [Display(Name = "Fecha Contable")]
        public string FechaContable { get; set; }

        [Display(Name = "Folio")]
        public string FolioFiscal { get; set; }

        [Display(Name = "Acreedor")]
        public string AcreedorFactura { get; set; }

        [Display(Name = "Concepto Complementario")]
        public string ConceptoComplementario { get; set; }

        [Display(Name = "Fecha Solicitud de Pago")]
        public string FechaSolicitud { get; set; }

        [Display(Name = "Clave de Solicitud de Pago")]
        public string ClaveSolicitud { get; set; }

        [Display(Name = "Moneda")]
        public string Moneda { get; set; }
        [Display(Name = "Método de Pago")]
        public string MetodoPago { get; set; }

        [Display(Name = "Forma de Pago")]
        public string FormaPago { get; set; }

        [Display(Name = "Fecha de Reembolso")]
        public string FechaReembolso { get; set; }

        public string Folio { get; set; }

        public string FolioGastoNoDeducible { get; set; }

        [Display(Name = "Serie")]
        public string Serie { get; set; }

        public double ImporteUSD { get; set; }

        [Display(Name = "Importe")]
        public double Importe { get; set; }

        [Display(Name = "Descuento")]
        public double Descuento { get; set; }

        [Display(Name = "Subtotal")]
        public double Subtotal { get; set; }

        [Display(Name = "IVA")]
        public double IVA { get; set; }

        [Display(Name = "IEPS")]
        public double IEPS { get; set; }

        [Display(Name = "IVA Ret.")]
        public double IVARet { get; set; }

        [Display(Name = "ISR Ret.")]
        public double ISR { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }

        [Display(Name = "Path")]
        public string Path { get; set; }

        [Display(Name = "RFC")]
        public string RFC { get; set; }

        public int IdProveedor { get; set; }


        public string TipoCambio { get; set; }

        public string RFCEmisor { get; set; }

        public double DescuentoCFE { get; set; }
        public double TotalCFE { get; set; }

        public double DescuentoDolares { get; set; }
        public double IVADolares { get; set; }
        public double IEPSDolares { get; set; }
        public double IVARetDolares { get; set; }
        public double ISRRetDolares { get; set; }
        public double TotalDolares { get; set; }
        public string ReceptorNombre { get; set; }
        public string FileNameXml { get; set; }

        public string TipoComprobante { get; set; }

        public string VersionCFDI { get; set; }

        public int IdGasto {  get; set; }
        public string Gasto {  get; set; }
        public int IdInmueble { get; set; }
        public string Periodo { get; set; }
        public string PathLocalXML { get; set; }
        public string PathLocalPDF { get; set; }
        public string FileLocal { get; set; }
        public string ReceptorRFC { get; set; }
    }
}
