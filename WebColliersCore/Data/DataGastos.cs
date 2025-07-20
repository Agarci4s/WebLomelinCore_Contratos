using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using WebColliersCore.DataAccess;
using WebLomelinCore.Models;

namespace WebLomelinCore.Data
{
    public class DataGastos : Conexion
    {
        public List<SelectListItem> GetEgresosAll()
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new();
                DataTable tb = RunStoredProcedure("tbegresos_GetAll", listSqlParameters);
                List<SelectListItem> lstEgresos = new();
                foreach (DataRow item in tb.Rows)
                {
                    lstEgresos.Add(new SelectListItem { Text = item["Concepto"].ToString(), Value = item["Id"].ToString() });
                }
                return lstEgresos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool ValidaExisteXML(string folioFiscal)
        {

            try
            {

                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("Folio_In", folioFiscal));
                var dataTable = RunStoredProcedure("fact_xml_ValidaExisteUUID", listSqlParameters);


                if (dataTable.Rows.Count > 0)
                {
                    int countFolio = int.Parse(dataTable.Rows[0]["CountFolio"].ToString());


                    if (countFolio > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return false;
        }

        public int GuardaMovimientoPresupuestal(Factura movimientos, string usuario)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new()
                {
                    new("Concepto_in", movimientos.Concepto == null ? "" : movimientos.Concepto.Length > 250 ? movimientos.Concepto.Substring(0, 250) : movimientos.Concepto),
                    new("Proveedor_in", movimientos.Proveedor ?? ""),
                    new("FechaContable_in", movimientos.FechaContable ?? ""),
                    new("FolioFiscal_in", movimientos.FolioFiscal ?? ""),
                    new("IdInmueble_in", movimientos.IdInmueble),
                    new("Moneda_in", movimientos.Moneda ?? ""),
                    new("TipoCambio_in", movimientos.TipoCambio),
                    new("FormaPago_in", movimientos.FormaPago ?? ""),
                    new("Folio_in", movimientos.Folio ?? ""),
                    new("Serie_in", movimientos.Serie ?? ""),
                    new("Importe_in", movimientos.Importe),
                    new("Descuento_in", movimientos.Descuento),
                    new("Subtotal_in", movimientos.Subtotal),
                    new("IVA_in", movimientos.IVA),
                    new("IEPS_in", movimientos.IEPS),
                    new("IVARet_in", movimientos.IVARet),
                    new("ISR_in", movimientos.ISR),
                    new("Total_in", movimientos.Total),
                    new("Periodo_in", movimientos.Periodo),
                    new("Usuario_in", usuario),
                    new("Path_in", movimientos.Path),
                    new("RFC_in", movimientos.RFC),
                    new("OrigenCaptura_in", "Lectura XML"),
                    new("MetodoPago_In", movimientos.MetodoPago),
                    new("Receptor_In", movimientos.ReceptorNombre),
                    new("RFCReceptor_In", movimientos.ReceptorRFC ?? ""),
                    new("PathXML_In", movimientos.PathLocalXML ?? ""),
                    new("PathPDF_In", movimientos.PathLocalPDF ?? "")
                };
                

                DataTable dataTable = RunStoredProcedure("fact_xml_Insert", listSqlParameters);

                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;                
            }            
        }
    }
}
