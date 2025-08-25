using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    public class Localidad
    {
        #region "Datos Iniciales..."

        [Display(Name = "Inmueble")]
        public int IdInmueble { get; set; }
        [Display(Name = "Tipo de localidad")]
        public int IdUsoInmueble { get; set; }
        [Display(Name = "Estatus")]
        public int Estado { get; set; }
        [Display(Name = "Arrendatario")]
        public int IdInquilino { get; set; }
        [Display(Name = "Nombre comercial")]
        public int IdNombreComercial { get; set; }
        [Display(Name = "Localidad")]
        public string NumeroLocalidad { get; set; }
        [Display(Name = "Fecha Ingreso / Vacío")]
        public DateTime FechaIngreso { get; set; }

        #endregion

        #region "Pestaña Contrato..."

        [Display(Name = "Cantidad de Locales Agrupados")]
        public int LocalesAgrupados { get; set; }
        [Display(Name = "Propio o Tercero")]
        public string PropTer { get; set; }
        [Display(Name = "Depósito de Renta")]
        public double DepRta { get; set; }
        [Display(Name = "Pagaré")]
        public string Pagare { get; set; }
        [Display(Name = "Día Vencimiento")]
        public int DiaVencimiento { get; set; }
        [Display(Name = "Día Límite de Pago")]
        public int DiaLimitePago { get; set; }
        [Display(Name = "Ruta")]
        public string Ruta { get; set; }
        [Display(Name = "División")]
        public string DivisionRuta { get; set; }
        [Display(Name = "Folio Contrato")]
        public string FolioContrato { get; set; }
        [Display(Name = "Categoría")]
        public int Categoria { get; set; }
        [Display(Name = "Observaciones del Depósito en Garantía")]
        public string ObservacionDepositoGarantia { get; set; }
        [Display(Name = "Número de Referencia (Renta)")]
        public string NumeroReferenciado { get; set; }
        [Display(Name = "Número de Referencia (Mtto.)")]
        public string NumeroReferenciadoMtto { get; set; }

        #endregion

        #region "Pestaña Plazos..."

        [Display(Name = "Fecha Inicio de Contrato")]
        public DateTime FechaUltimaRenovacion { get; set; }
        [Display(Name = "Fecha Término de Contrato")]
        public DateTime FechaTermino { get; set; }
        [Display(Name = "Fecha de Revisión")]
        public DateTime FechaRevision { get; set; }
        [Display(Name = "Fecha de Recepción")]
        public DateTime FechaOcupacion { get; set; }
        [Display(Name = "Fecha Inicio 1er. Contrato")]
        public DateTime FechaInicioContratoUno { get; set; }
        [Display(Name = "Fecha Término 1er. Contrato")]
        public DateTime FechaFinContratoUno { get; set; }

        #region "Arrendador Forzoso"
        [Display(Name = "Años")]
        public int PlazoPFAnio { get; set; }
        [Display(Name = "Meses")]
        public int PlazoPFMes { get; set; }
        [Display(Name = "Días")]
        public int PlazoPFDia { get; set; }

        #endregion

        #region "Arrendatario Forzoso"
        [Display(Name = "Años")]
        public int PlazoIFAnio { get; set; }
        [Display(Name = "Meses")]
        public int PlazoIFMes { get; set; }
        [Display(Name = "Días")]
        public int PlazoIFDia { get; set; }
        #endregion

        #region "Arrendador Voluntario"
        [Display(Name = "Años")]
        public int PlazoPVAnio { get; set; }
        [Display(Name = "Meses")]
        public int PlazoPVMes { get; set; }
        [Display(Name = "Días")]
        public int PlazoPVDia { get; set; }
        #endregion

        #region "Arrendatario Voluntario"
        [Display(Name = "Años")]
        public int PlazoIVAnio { get; set; }
        [Display(Name = "Meses")]
        public int PlazoIVMes { get; set; }
        [Display(Name = "Días")]
        public int PlazoIVDia { get; set; }
        #endregion

        [Display(Name = "Porcentaje Adicional Revisión")]
        public double RevisionPorcentajeAdicional { get; set; }
        [Display(Name = "Plazo de Revisión")]
        public string RevisionPeriodo { get; set; }
        [Display(Name = "Base de Revisión")]
        public int IdRevisionBase { get; set; }
        [Display(Name = "Comentarios de revisión")]
        public string ObservacionesContrato { get; set; }

        #endregion

        #region "Pestaña Caracteristicas..."

        public CaracteristicasInmueble caracteristicasInmueble { get; set; }

        #endregion

        #region "Pestaña Ingresos..."
        [Display(Name = "Aplica IVA")]
        public bool IVA { get; set; }
        public double PorCIVA { get; set; }
        public double ImporteIVA { get; set; }
        public dtGastosFijos dtGastosFijos { get; set; }
        [Display(Name = "Hacer factura individual de otros conceptos de cobro (Agua, cuota de Manto.)")]
        public bool FacturarConceptosIndividuales { get; set; }
        [Display(Name = "Factura emitida por")]
        public int IdModoFacturacion { get; set; }
        [Display(Name = "Moneda")]
        public int IdMonedaPago { get; set; }
        [Display(Name = "Tipo de cambio por convenio")]
        public double TipoCambioConvenio { get; set; }
        [Display(Name = "Porcentaje de ventas")]
        public double PorVenta { get; set; }
        [Display(Name = "Renta gratis")]
        public string RentaGratis { get; set; }
        [Display(Name = "Derecho al tanto")]
        public string DerechoTanto { get; set; }
        [Display(Name = "Emitir factura directamente del propietario")]
        public bool AplicaComplementoTerceros { get; set; }
        [Display(Name = "Incobrable")]
        public bool Incobrable { get; set; }
        public LocalidadIngresos LocalidadIngresos { get; set; }

        public List<dtGastosFijos> dtGastosFijosList { get; set; }

        #endregion

        #region "Pestaña Notas..."

        public NotasLocalidad NotasLocalidad { get; set; }
        public List<NotasLocalidad> dtNotasList { get; set; }

        #endregion

        #region "Pestaña de Prorrateos..."

        [Display(Name = "Paga Agua")]
        public bool PagaAgua { get; set; }
        [Display(Name = "Paga Agua Helada")]
        public bool PagaAguaHelada { get; set; }
        [Display(Name = "Paga Energía Eléctrica")]
        public bool PagaEnergiaElectrica { get; set; }
        [Display(Name = "Porcentaje Agua")]
        public double PorcentajeAgua { get; set; }
        [Display(Name = "Porcentaje Agua Helada")]
        public double PorcentajeAguaHelada { get; set; }
        [Display(Name = "Porcentaje Energía Eléctrica")]
        public double PorcentajeEnergiaElectrica { get; set; }

        #endregion

        public int IdLocalidad { get; set; }
        public string NotaPromocion { get; set; }
        public double RtaAnt { get; set; }
        public DateTime FechaInicioAnt { get; set; }
        public DateTime FechaTerminoAnt { get; set; }
        public int Status { get; set; }
        public string DescripcionWeb { get; set; }
        public bool Penalidad { get; set; }
        public double PorCPenalidad { get; set; }
        public string Usuario { get; set; }
        public string Maquina { get; set; }
        public string DireccionIP { get; set; }
        DateTime DateCreate { get; set; }
        DateTime DateEdit { get; set; }

        [Display(Name = "Tipo localidad")]
        public string TipoLocalidadAux { get; set; }
        public string InmuebleAux { get; set; }
        public string EstatusAux { get; set; }
        public string ArrendatarioAux { get; set; }

        [Display(Name = "Localidad")]
        public string NombreComercialAux { get; set; }
        public string ClaveInmuebleAux { get; set; }
        public string EstadoAux { get; set; }

        [Display(Name = "Observaciones")]
        public string ClausulasTerminacion { get; set; }

        [Display(Name = "Tipo")]
        public string TipoInmuebleGenteraAux { get; set; }
    }
}
