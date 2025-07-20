using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System;
using WebColliersCore.DataAccess;
using WebLomelinCore.Models;

namespace WebLomelinCore.Data
{
    public class DataCatLuz
    {
        private Conexion conexion = new Conexion();

        public bool Insert(cat_Luz luz)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("IdInmueble_In", luz.IdInmueble),
                    new("IdLocalidad_In", luz.IdLocalidad),
                    new("IdPeriodicidad_In", luz.IdPeriodicidad),
                    new("CuentaLuz_In", luz.CuentaLuz),
                    new("NumeroServicio_In", luz.NumeroServicio),
                    new("NumeroMedidor_In", luz.NumeroMedidor),
                    new("TipoTarifa_In", luz.TipoTarifa),
                    new("FechaCorte_In", luz.FechaCorte),
                    new("FechaLimitePago_In", luz.FechaLimitePago),
                    new("UsuarioAlta_In", luz.IdUsuarioAlta)
                };


                conexion.RunStoredProcedure("b_cg_luz_Insert", mySqlParameters);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Update(cat_Luz luz)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("IdInmueble_In", luz.IdInmueble),
                    new("IdLocalidad_In", luz.IdLocalidad),
                    new("IdPeriodicidad_In", luz.IdPeriodicidad),
                    new("CuentaLuz_In", luz.CuentaLuz),
                    new("NumeroServicio_In", luz.NumeroServicio),
                    new("NumeroMedidor_In", luz.NumeroMedidor),
                    new("UsuarioUpdate_In", luz.IdUsuarioUpdate),
                    new("Id_In", luz.Id)
                };


                conexion.RunStoredProcedure("b_cg_luz_Update", mySqlParameters);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Delete(cat_Luz luz)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("UsuarioUpdate_In", luz.IdUsuarioUpdate),
                    new("Id_In", luz.Id)
                };


                conexion.RunStoredProcedure("b_cg_luz_Delete", mySqlParameters);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Existe(cat_Luz luz)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("CuentaLuz_In", luz.CuentaLuz)
                };

                DataTable dataTable = conexion.RunStoredProcedure("b_cg_luz_Duplicada", mySqlParameters);

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

        public List<cat_Luz> GetCuentasLuz()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new();

                DataTable dataTable = conexion.RunStoredProcedure("b_cg_luz_Get", mySqlParameters);

                return ConvertToListModel(dataTable);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public cat_Luz GetCuentasLuzById(int id)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("Id_In",id)
                };

                DataTable dataTable = conexion.RunStoredProcedure("b_cg_luz_GetById", mySqlParameters);

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

                var dataTable = conexion.RunStoredProcedure("b_cg_luz_log_Get", listSqlParameters);

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

        public List<cat_Luz> ConvertToListModel(DataTable dataTable)
        {
            try
            {
                List<cat_Luz> list = new List<cat_Luz>();
                foreach (DataRow item in dataTable.Rows)
                {
                    list.Add(new cat_Luz()
                    {
                        Id = int.Parse(item["Id"].ToString()),
                        InmuebleAux = item["Inmueble"].ToString(),
                        LocalidadAux = item["Localidad"].ToString(),
                        PeriodicidadAux = item["Periodicidad"].ToString(),
                        CuentaLuz = item["CuentaLuz"].ToString(),
                        IdInmueble = int.Parse(item["IdInmueble"].ToString()),
                        IdLocalidad = int.Parse(item["IdLocalidad"].ToString()),
                        IdPeriodicidad = int.Parse(item["IdPeriodicidad"].ToString()),
                        NumeroServicio = int.Parse(item["NumeroServicio"].ToString()),
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

        public cat_Luz ConvertToModel(DataTable dataTable)
        {
            try
            {
                List<cat_Luz> list = new List<cat_Luz>();
                cat_Luz cat_Luz = new cat_Luz();

                foreach (DataRow item in dataTable.Rows)
                {
                    cat_Luz.Id = int.Parse(item["Id"].ToString());
                    cat_Luz.InmuebleAux = item["Inmueble"].ToString();
                    cat_Luz.LocalidadAux = item["Localidad"].ToString();
                    cat_Luz.PeriodicidadAux = item["Periodicidad"].ToString();
                    cat_Luz.CuentaLuz = item["CuentaLuz"].ToString();
                    cat_Luz.IdInmueble = int.Parse(item["IdInmueble"].ToString());
                    cat_Luz.IdLocalidad = int.Parse(item["IdLocalidad"].ToString());
                    cat_Luz.IdPeriodicidad = int.Parse(item["IdPeriodicidad"].ToString());
                    cat_Luz.NumeroServicio = int.Parse(item["NumeroServicio"].ToString());
                    cat_Luz.NumeroMedidor = int.Parse(item["NumeroMedidor"].ToString());
                }

                return cat_Luz;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
    }
}
