using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using WebColliersCore.DataAccess;
using WebLomelinCore.Models;
using System.Data;

namespace WebLomelinCore.Data
{
    public class DataCatPredial
    {
        private Conexion conexion = new Conexion();

        public bool Insert(cat_Predial predial)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("IdInmueble_In", predial.IdInmueble),
                    new("IdLocalidad_In", predial.IdLocalidad),
                    new("IdPeriodicidad_In", predial.IdPeriodicidad),
                    new("CuentaPredial_In", predial.CuentaPredial),
                    new("M2Terrero_In", predial.M2Terreno),
                    new("ValorAvaluoCatastral_In", predial.ValorAvaluoCatastral),
                    new("ClaveCorredor_In", predial.ClaveCorredor),
                    new("UsuarioAlta_In", predial.IdUsuarioAlta)
                };

                var rtnPredial = conexion.RunStoredProcedure("b_cg_predial_Insert", mySqlParameters);
                DataRow row = rtnPredial.Rows[0];
                int IdPredial = int.Parse(row["IdPredial_Out"].ToString());

                foreach (cat_Predial_TipoUsos item in predial.TipoUsos)
                {
                    try
                    {
                        List<MySqlParameter> mySqlParametersTiposUso = new()
                        {
                            new("IdPredial_In", IdPredial),
                            new("TipoUso_In", item.TipoUso),
                            new("Niveles_In", item.Nivel),
                            new("Clase_In", item.Clase),
                            new("M2Construccion_In", item.M2Construccion),
                            new("Antiguedad_In", item.Antiguedad),
                            new("UsuarioAlta_In", predial.IdUsuarioAlta)
                        };

                        conexion.RunStoredProcedure("b_cg_predial_tipouso_Insert", mySqlParametersTiposUso);
                    }
                    catch (Exception)
                    {                        
                    }                    
                }

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Update(cat_Predial predial)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("IdInmueble_In", predial.IdInmueble),
                    new("IdLocalidad_In", predial.IdLocalidad),
                    new("IdPeriodicidad_In", predial.IdPeriodicidad),
                    new("CuentaPredial_In", predial.CuentaPredial),
                    new("M2Terrero_In", predial.M2Terreno),
                    new("ValorAvaluoCatastral_In", predial.ValorAvaluoCatastral),
                    new("ClaveCorredor_In", predial.ClaveCorredor),
                    new("UsuarioUpdate_In", predial.IdUsuarioUpdate),
                    new("Id_In", predial.Id)
                };

                conexion.RunStoredProcedure("b_cg_predial_Update", mySqlParameters);

                List<MySqlParameter> mySqlParametersD = new()
                {                    
                    new("Id_In", predial.Id)
                };

                conexion.RunStoredProcedure("b_cg_predial_tipouso_DeleteByIdPredial", mySqlParametersD);

                foreach (cat_Predial_TipoUsos item in predial.TipoUsos)
                {
                    try
                    {
                        List<MySqlParameter> mySqlParametersTiposUso = new()
                        {
                            new("IdPredial_In", predial.Id),
                            new("TipoUso_In", item.TipoUso),
                            new("Niveles_In", item.Nivel),
                            new("Clase_In", item.Clase),
                            new("M2Construccion_In", item.M2Construccion),
                            new("Antiguedad_In", item.Antiguedad),
                            new("UsuarioAlta_In", predial.IdUsuarioUpdate)
                        };

                        conexion.RunStoredProcedure("b_cg_predial_tipouso_Insert", mySqlParametersTiposUso);
                    }
                    catch (Exception)
                    {
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Delete(cat_Predial predial)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("UsuarioUpdate_In", predial.IdUsuarioUpdate),
                    new("Id_In", predial.Id)
                };


                conexion.RunStoredProcedure("b_cg_predial_Delete", mySqlParameters);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Existe(cat_Predial predial)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("CuentaPredial_In", predial.CuentaPredial)
                };

                DataTable dataTable = conexion.RunStoredProcedure("b_cg_predial_Duplicada", mySqlParameters);

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

        public List<cat_Predial> GetCuentasPredial()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new();

                DataTable dataTable = conexion.RunStoredProcedure("b_cg_predial_Get", mySqlParameters);

                return ConvertToListModel(dataTable);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public cat_Predial GetCuentasPredialById(int id)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("Id_In",id)
                };

                DataTable dataTable = conexion.RunStoredProcedure("b_cg_predial_GetById", mySqlParameters);

                return ConvertToModel(dataTable);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<cat_Predial_TipoUsos> GetCuentasTUPredialById(int id)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new()
                {
                    new("Id_In",id)
                };

                DataTable dataTable = conexion.RunStoredProcedure("b_cg_predial_tiposuso_GetByIdPredial", mySqlParameters);

                return ConvertToListModelTU(dataTable);
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

                var dataTable = conexion.RunStoredProcedure("b_cg_predial_log_Get", listSqlParameters);

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

        public List<cat_Predial> ConvertToListModel(DataTable dataTable)
        {
            try
            {
                List<cat_Predial> list = new List<cat_Predial>();
                foreach (DataRow item in dataTable.Rows)
                {
                    list.Add(new cat_Predial()
                    {
                        Id = int.Parse(item["Id"].ToString()),
                        InmuebleAux = item["Inmueble"].ToString(),
                        LocalidadAux = item["Localidad"].ToString(),
                        PeriodicidadAux = item["Periodicidad"].ToString(),
                        CuentaPredial = item["CuentaPredial"].ToString(),
                        IdInmueble = int.Parse(item["IdInmueble"].ToString()),
                        IdLocalidad = int.Parse(item["IdLocalidad"].ToString()),
                        IdPeriodicidad = int.Parse(item["IdPeriodicidad"].ToString()),
                        M2Terreno = float.Parse(item["M2Terrero"].ToString()),
                        ValorAvaluoCatastral = int.Parse(item["ValorAvaluoCatastral"].ToString()),
                        ClaveCorredor = item["ClaveCorredor"].ToString(),
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

        public cat_Predial ConvertToModel(DataTable dataTable)
        {
            try
            {
                List<cat_Predial> list = new List<cat_Predial>();
                cat_Predial cat_Predial = new cat_Predial();

                foreach (DataRow item in dataTable.Rows)
                {
                    cat_Predial.Id = int.Parse(item["Id"].ToString());
                    cat_Predial.InmuebleAux = item["Inmueble"].ToString();
                    cat_Predial.LocalidadAux = item["Localidad"].ToString();
                    cat_Predial.PeriodicidadAux = item["Periodicidad"].ToString();
                    cat_Predial.CuentaPredial = item["CuentaPredial"].ToString();
                    cat_Predial.IdInmueble = int.Parse(item["IdInmueble"].ToString());
                    cat_Predial.IdLocalidad = int.Parse(item["IdLocalidad"].ToString());
                    cat_Predial.IdPeriodicidad = int.Parse(item["IdPeriodicidad"].ToString());
                    cat_Predial.M2Terreno = int.Parse(item["M2Terrero"].ToString());
                    cat_Predial.ValorAvaluoCatastral = int.Parse(item["ValorAvaluoCatastral"].ToString());
                    cat_Predial.ClaveCorredor = item["ClaveCorredor"].ToString();
                }

                return cat_Predial;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<cat_Predial_TipoUsos> ConvertToListModelTU(DataTable dataTable)
        {
            try
            {
                List<cat_Predial_TipoUsos> list = new List<cat_Predial_TipoUsos>();
                foreach (DataRow item in dataTable.Rows)
                {
                    list.Add(new cat_Predial_TipoUsos()
                    {
                        Id = int.Parse(item["Id"].ToString()),
                        TipoUso = item["TipoUso"].ToString(),
                        Nivel = item["Niveles"].ToString(),
                        Clase = item["Clase"].ToString(),
                        M2Construccion = double.Parse(item["M2Construccion"].ToString()),
                        Antiguedad = int.Parse(item["Antiguedad"].ToString()),                        
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
    }
}
