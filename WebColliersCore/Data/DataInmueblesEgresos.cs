using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System;
using WebColliersCore.DataAccess;
using WebLomelinCore.Models;

namespace WebLomelinCore.Data
{
    public class DataInmueblesEgresos: Conexion
    {        

        public List<SelectListItem> GetImpuestos(string Tipo)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new()
                {
                    new MySqlParameter("Tipo_In", Tipo)                    
                };
                DataTable tb = RunStoredProcedure("tbimpuestos_Get", listSqlParameters);
                List<SelectListItem> lstImpuestos = new();
                foreach (DataRow item in tb.Rows)
                {
                    lstImpuestos.Add(new SelectListItem { Text = item["Text"].ToString(), Value = item["Value"].ToString() });
                }
                return lstImpuestos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<SelectListItem> GetImpuestosRetIVA(int IVA)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new()
                {
                    new MySqlParameter("IVA_In", IVA)
                };
                DataTable tb = RunStoredProcedure("tbimpuestos_GetRetencionIVA", listSqlParameters);
                List<SelectListItem> lstImpuestos = new();
                foreach (DataRow item in tb.Rows)
                {
                    lstImpuestos.Add(new SelectListItem { Text = item["Text"].ToString(), Value = item["Value"].ToString() });
                }
                return lstImpuestos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<SelectListItem> GetContratos(int IdInmueble)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new()
                {
                    new MySqlParameter("IdInmueble_In", IdInmueble)
                };
                DataTable tb = RunStoredProcedure("b_inmuebles_contratos_GetCmb", listSqlParameters);
                List<SelectListItem> lstContratos = new();
                foreach (DataRow item in tb.Rows)
                {
                    lstContratos.Add(new SelectListItem { Text = item["Contrato"].ToString(), Value = item["Id"].ToString() });
                }
                return lstContratos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<SelectListItem> GetEgresos()
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new();
                DataTable tb = RunStoredProcedure("tbegresos_Get", listSqlParameters);
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

        public List<SelectListItem> GetMonedas()
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new();
                DataTable tb = RunStoredProcedure("tbmoneda_Get", listSqlParameters);
                List<SelectListItem> lstEgresos = new();
                foreach (DataRow item in tb.Rows)
                {
                    lstEgresos.Add(new SelectListItem { Text = item["Moneda"].ToString(), Value = item["Id"].ToString() });
                }
                return lstEgresos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool InsertEgreso(B_inmuebles_egresos b_Inmuebles_Egresos, int IdUsuario)
        {
            try
            {
                ExecuteNonQuerySP("rel_inmuebles_egresos_Insert",
                    new MySqlParameter("IdInmueble_In", b_Inmuebles_Egresos.IdInmueble),
                    new MySqlParameter("IdEgreso_In", b_Inmuebles_Egresos.IdEgreso),
                    new MySqlParameter("Importe_In", b_Inmuebles_Egresos.Importe),
                    new MySqlParameter("PorcIVA_In", b_Inmuebles_Egresos.PorcIVA),
                    new MySqlParameter("IVA_In", b_Inmuebles_Egresos.IVA),
                    new MySqlParameter("PorcRetISR_In", b_Inmuebles_Egresos.PorcRetISR),
                    new MySqlParameter("RetISR_In", b_Inmuebles_Egresos.RetISR),
                    new MySqlParameter("PorcRetIVA_In", b_Inmuebles_Egresos.PorcRetIVA),
                    new MySqlParameter("RetIVA_In", b_Inmuebles_Egresos.RetIVA),
                    new MySqlParameter("IdMoneda_In", b_Inmuebles_Egresos.IdMoneda),
                    new MySqlParameter("Usuario_In", IdUsuario),
                    new MySqlParameter("IdContrato", b_Inmuebles_Egresos.IdContrato)
                    );

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool DeleteEgreso(int Id)
        {
            try
            {
                ExecuteNonQuerySP("rel_inmuebles_egresos_Delete",                    
                    new MySqlParameter("Id_In", Id)
                    );

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<B_inmuebles_egresos> GetEgresosByIdInmueble(int IdInmueble, int IdContrato)
        {
            try
            {
                DataTable dataTable = ExecuteSP("rel_inmuebles_egresos_OtrosGetByIdInmueble",
                    new MySqlParameter("IdInmueble_In", IdInmueble),
                    new MySqlParameter("IdContrato_In", IdContrato)
                    );
                return ConvertToListModel(dataTable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public List<B_inmuebles_egresos> GetEgresosByContrato(int IdContrato)
        {
            try
            {
                DataTable dataTable = ExecuteSP("rel_inmuebles_egresos_GetByIdContrato",
                    new MySqlParameter("IdContrato_In", IdContrato)
                    );
                return ConvertToListModel(dataTable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public List<B_inmuebles_egresos> GetEgresosRentasByIdInmueble(int IdInmueble, int IdContrato)
        {
            try
            {
                DataTable dataTable = ExecuteSP("rel_inmuebles_egresos_RentasGetByIdInmueble",
                    new MySqlParameter("IdInmueble_In", IdInmueble),
                    new MySqlParameter("IdContrato_In", IdContrato)
                    );
                return ConvertToListModel(dataTable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private List<B_inmuebles_egresos> ConvertToListModel(DataTable dataTable)
        {
            try
            {
                List<B_inmuebles_egresos> listEgresos = new();

                foreach (DataRow row in dataTable.Rows)
                {
                    listEgresos.Add(new B_inmuebles_egresos
                    {
                        Id = int.Parse(row["Id"].ToString()),
                        Egreso = row["Egreso"].ToString(),
                        Importe = double.Parse(row["Importe"].ToString()),
                        PorcIVA = double.Parse(row["PorcIVA"].ToString()),
                        IVA = double.Parse(row["IVA"].ToString()),
                        PorcRetISR = double.Parse(row["PorcRetISR"].ToString()),
                        RetISR = double.Parse(row["RetISR"].ToString()),
                        PorcRetIVA = double.Parse(row["PorcRetIVA"].ToString()),
                        RetIVA = double.Parse(row["RetIVA"].ToString()),
                        Moneda = row["Moneda"].ToString()                        
                    });
                }

                return listEgresos;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
