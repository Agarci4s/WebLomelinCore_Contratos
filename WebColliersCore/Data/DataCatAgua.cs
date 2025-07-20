using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;
using WebLomelinCore.Models;
using System.Data;
using NPOI.SS.Formula.Functions;
using static NPOI.HSSF.Util.HSSFColor;

namespace WebLomelinCore.Data
{
    public class DataCatAgua
    {
        private Conexion conexion = new Conexion();

        public bool Insert(cat_Agua agua)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("IdInmueble_In", agua.IdInmueble),
                    new("IdLocalidad_In", agua.IdLocalidad),
                    new("IdPeriodicidad_In", agua.IdPeriodicidad),
                    new("CuentaAgua_In", agua.CuentaAgua),
                    new("Diametro_In", agua.Diametro),
                    new("NumeroMedidor_In", agua.NumeroMedidor),
                    new("FuncionaMedidor_In", agua.FuncionaMedidor),
                    new("FuncionaMedidor_In", agua.TipoTarifa),
                    new("UsuarioAlta_In", agua.IdUsuarioAlta)
                };


                conexion.RunStoredProcedure("b_cg_agua_Insert", mySqlParameters);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Update(cat_Agua agua)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("IdInmueble_In", agua.IdInmueble),
                    new("IdLocalidad_In", agua.IdLocalidad),
                    new("IdPeriodicidad_In", agua.IdPeriodicidad),
                    new("CuentaAgua_In", agua.CuentaAgua),
                    new("Diametro_In", agua.Diametro),
                    new("NumeroMedidor_In", agua.NumeroMedidor),
                    new("UsuarioUpdate_In", agua.IdUsuarioUpdate),
                    new("Id_In", agua.Id)
                };


                conexion.RunStoredProcedure("b_cg_agua_Update", mySqlParameters);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Delete(cat_Agua agua)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {                    
                    new("UsuarioUpdate_In", agua.IdUsuarioUpdate),
                    new("Id_In", agua.Id)
                };


                conexion.RunStoredProcedure("b_cg_agua_Delete", mySqlParameters);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Existe(cat_Agua agua)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("CuentaAgua_In", agua.CuentaAgua)
                };

                DataTable dataTable = conexion.RunStoredProcedure("b_cg_agua_Duplicada", mySqlParameters);

                if (int.Parse(dataTable.Rows[0]["Existe"].ToString()) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public List<cat_Agua> GetCuentasAgua()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new();

                DataTable dataTable = conexion.RunStoredProcedure("b_cg_agua_Get", mySqlParameters);

                return ConvertToListModel(dataTable);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public cat_Agua GetCuentasAguaById(int id)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("Id_In",id)
                };

                DataTable dataTable = conexion.RunStoredProcedure("b_cg_agua_GetById", mySqlParameters);

                return ConvertToModel(dataTable);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<string> getLog()
        {
            List<string> lstSelectListItem = new List<string>();
            try
            {

                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();               

                var dataTable = conexion.RunStoredProcedure("b_cg_agua_log_Get", listSqlParameters);

                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow item in dataTable.Rows)
                    {
                        lstSelectListItem.Add(Convert.ToString(item["Log"]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstSelectListItem;
        }

        public List<cat_Agua> ConvertToListModel(DataTable dataTable)
        {
            try
            {
                List<cat_Agua> list = new List<cat_Agua>();
                foreach (DataRow item in dataTable.Rows)
                {
                    list.Add(new cat_Agua()
                    {
                        Id = int.Parse(item["Id"].ToString()),
                        InmuebleAux = item["Inmueble"].ToString(),
                        LocalidadAux = item["Localidad"].ToString(),
                        PeriodicidadAux = item["Periodicidad"].ToString(),
                        CuentaAgua = item["CuentaAgua"].ToString(),
                        IdInmueble = int.Parse(item["IdInmueble"].ToString()),
                        IdLocalidad = int.Parse(item["IdLocalidad"].ToString()),
                        IdPeriodicidad = int.Parse(item["IdPeriodicidad"].ToString()),
                        Diametro = float.Parse(item["Diametro"].ToString()),
                        NumeroMedidor = int.Parse(item["NumeroMedidor"].ToString()),
                    });
                }

                return list;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public cat_Agua ConvertToModel(DataTable dataTable)
        {
            try
            {
                List<cat_Agua> list = new List<cat_Agua>();
                cat_Agua cat_Agua = new cat_Agua();

                foreach (DataRow item in dataTable.Rows)
                {
                    cat_Agua.Id = int.Parse(item["Id"].ToString());
                    cat_Agua.InmuebleAux = item["Inmueble"].ToString();
                    cat_Agua.LocalidadAux = item["Localidad"].ToString();
                    cat_Agua.PeriodicidadAux = item["Periodicidad"].ToString();
                    cat_Agua.CuentaAgua = item["CuentaAgua"].ToString();
                    cat_Agua.IdInmueble = int.Parse(item["IdInmueble"].ToString());
                    cat_Agua.IdLocalidad = int.Parse(item["IdLocalidad"].ToString());
                    cat_Agua.IdPeriodicidad = int.Parse(item["IdPeriodicidad"].ToString());
                    cat_Agua.Diametro = float.Parse(item["Diametro"].ToString());
                    cat_Agua.NumeroMedidor = int.Parse(item["NumeroMedidor"].ToString());
                }

                return cat_Agua;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
    }
}
