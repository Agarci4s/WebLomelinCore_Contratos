using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;

namespace WebColliersCore.Data
{
    public class DataTpCartera
    {
        private Conexion conexion = new Conexion();

        public List<TpCartera> recuperaTpCartera()
        {
              
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idCartera_In", 0));
            DataTable dataTable = conexion.RunStoredProcedure("tpCarteraGet", listSqlParameters);
            return DataToModel(dataTable);
        }

        public List<TpCartera> recuperaTpCarteraList(int idUsuario)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idUsuario", idUsuario));
            listSqlParameters.Add(new MySqlParameter("activos", true));
            List<TpCartera> listTpcarteraActivos = DataToModel(conexion.RunStoredProcedure("tpcarteraGetByUsuario", listSqlParameters), true);

            listSqlParameters.RemoveAt(1);
            listSqlParameters.Add(new MySqlParameter("activos", false));
            List<TpCartera> listTpcarteraNoActivos = DataToModel(conexion.RunStoredProcedure("tpcarteraGetByUsuario", listSqlParameters), false);

            List<TpCartera> listTpcarteras = listTpcarteraActivos.Concat(listTpcarteraNoActivos).ToList().OrderBy(l => l.idCartera).ToList();

            return listTpcarteras;
        }

        public TpCartera recuperaTpCartera(int idCartera)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idCartera_In", idCartera));
            TpCartera tpcarteraActivos = DataToModelCartera(conexion.RunStoredProcedure("tpcarteraGet", listSqlParameters));
            return tpcarteraActivos;
        }


        public List<TpCartera> GetByUser(int idUsuario)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idUsuario", idUsuario));
            listSqlParameters.Add(new MySqlParameter("activos", true));
            List<TpCartera> listTpcarteraActivos = DataToModel(conexion.RunStoredProcedure("tpcarteraGetByUsuario", listSqlParameters), true);

            return listTpcarteraActivos;
        }

        private TpCartera DataToModelCartera(DataTable dataTable)
        {
            TpCartera tpCartera = new TpCartera();
            foreach (DataRow item in dataTable.Rows)
            {
                tpCartera.complemento_terceros = (item["complemento_terceros"].ToString() == "T" ? true : false); /*Convert.ToChar(item["complemento_terceros"]);*/
                tpCartera.descripcionCartera = item["descripcionCartera"].ToString();
                tpCartera.idCartera = Int32.Parse(item["idCartera"].ToString());
                try
                {
                    tpCartera.letra = item["letra"].ToString(); /*Convert.ToChar(item["letra"]);*/
                }
                catch (Exception)
                {
                    tpCartera.letra = ""; /*Char.MinValue;*/
                }
                tpCartera.status = Int32.Parse(item["status"].ToString());
                break;
            }
            return tpCartera;
        }

        private List<TpCartera> DataToModel(DataTable dataTable)
        {
            List<TpCartera> listTpCarteras = new List<TpCartera>();
            foreach (DataRow item in dataTable.Rows)
            {
                TpCartera tpCartera = new TpCartera();
                tpCartera.complemento_terceros = bool.Parse(item["complemento_terceros"].ToString());
                tpCartera.descripcionCartera = item["descripcionCartera"].ToString();
                tpCartera.idCartera = Int32.Parse(item["idCartera"].ToString());
                try
                {
                    tpCartera.letra = item["letra"].ToString();
                }
                catch (Exception)
                {
                    tpCartera.letra = "";
                }
                tpCartera.status = Int32.Parse(item["status"].ToString());
                listTpCarteras.Add(tpCartera);
            }
            return listTpCarteras;
        }

        private List<TpCartera> DataToModel(DataTable dataTable, bool estatus)
        {
            List<TpCartera> listTpCarteras = new List<TpCartera>();
            foreach (DataRow item in dataTable.Rows)
            {
                TpCartera tpCartera = new TpCartera();
                tpCartera.carteraBool = estatus;
                tpCartera.complemento_terceros = (item["complemento_terceros"].ToString() == "T" ? true : false);
                tpCartera.descripcionCartera = item["descripcionCartera"].ToString();
                tpCartera.idCartera = Int32.Parse(item["idCartera"].ToString());
                try
                {
                    tpCartera.letra = item["letra"].ToString();
                }
                catch (Exception)
                {
                    tpCartera.letra = "";
                }
                tpCartera.status = Int32.Parse(item["status"].ToString());
                listTpCarteras.Add(tpCartera);
            }
            return listTpCarteras;
        }

        public bool Agregar(TpCartera tpCartera)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("DescripcionCarteraIn", MySqlDbType.VarChar, 60) { Value = tpCartera.descripcionCartera });
            mySqlParameters.Add(new MySqlParameter("LetraIn", MySqlDbType.VarChar, 1) { Value = tpCartera.letra });
            mySqlParameters.Add(new MySqlParameter("ComplementoTercerosIn", MySqlDbType.VarChar, 1) { Value = (tpCartera.complemento_terceros ? "T" : "F") });
            conexion.RunStoredProcedure("tpcarteraInsert", mySqlParameters);

            return true;
        }

        public bool Actualizar(int id, TpCartera tpCartera)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("IdCarteraIn", MySqlDbType.Int32) { Value = id });
            mySqlParameters.Add(new MySqlParameter("DescripcionCarteraIn", MySqlDbType.VarChar, 60) { Value = tpCartera.descripcionCartera });
            mySqlParameters.Add(new MySqlParameter("LetraIn", MySqlDbType.VarChar, 1) { Value = tpCartera.letra });
            mySqlParameters.Add(new MySqlParameter("ComplementoTercerosIn", MySqlDbType.VarChar, 1) { Value = (tpCartera.complemento_terceros ? "T" : "F") });
            conexion.RunStoredProcedure("tpcarteraUpdate", mySqlParameters);

            return true;
        }

        public bool Eliminar(int id)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("IdCarteraIn", MySqlDbType.Int32) { Value = id });
            conexion.RunStoredProcedure("tpcarteraDelete", mySqlParameters);

            return true;
        }

        public List<TpCartera> GetTipoCartera()
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("idCartera_In", MySqlDbType.Int32) { Value = 0 });
            DataTable dataTable = conexion.RunStoredProcedure("tpCarteraGet", mySqlParameters);
            List<TpCartera> lstTpCartera = new List<TpCartera>();

            foreach (DataRow item in dataTable.Rows)
            {
                lstTpCartera.Add(new TpCartera()
                {
                    idCartera = int.Parse(item["IDCARTERA"].ToString()),
                    descripcionCartera = item["DESCRIPCIONCARTERA"].ToString()
                });
            }
            return lstTpCartera;
        }

        public TpCartera GetTipoCartera(int id)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("IdCarteraIn", MySqlDbType.Int32) { Value = id });
            DataTable dataTable = conexion.RunStoredProcedure("tpcarteraGetById", mySqlParameters);

            return (new TpCartera()
            {
                idCartera = int.Parse(dataTable.Rows[0]["IDCARTERA"].ToString()),
                descripcionCartera = dataTable.Rows[0]["DESCRIPCIONCARTERA"].ToString(),
                letra = dataTable.Rows[0]["LETRA"].ToString(),
                complemento_terceros = (dataTable.Rows[0]["COMPLEMENTO_TERCEROS"].ToString() == "T" ? true : false)
            }); 
        }

        public bool ExisteTipoCartera(string Descripcion)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("DescripcionIn", MySqlDbType.VarChar, 60) { Value = Descripcion });
            DataTable dataTable = conexion.RunStoredProcedure("tpcarteraCountByDescripcion", mySqlParameters);
            int Total = int.Parse(dataTable.Rows[0]["Total"].ToString());

            if (Total > 0)
                return true;
            else
                return false;
        }

        public List<RegistroSeries> SeriesList(int idCartera)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("IdCarteraIn", MySqlDbType.Int32) { Value = idCartera });
            var dataTable = conexion.RunStoredProcedure("tpcarteraGetSeriesByCartera", mySqlParameters);

            try
            {
                List<RegistroSeries> lstSeries = new List<RegistroSeries>();
                foreach (DataRow item in dataTable.Rows)
                {
                    RegistroSeries series = new RegistroSeries();
                    series.TipoComprobante = item["TipoComprobante"].ToString();
                    series.IdCartera = int.Parse(item["IdCartera"].ToString());
                    series.Serie = item["Serie"].ToString();                    

                    lstSeries.Add(series);
                }
                return lstSeries;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> lstTipoComprobante()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable dataTable = conexion.RunStoredProcedure("tipodecomprobanteGet", mySqlParameters);
                List<SelectListItem> lstComprobantes = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstComprobantes.Add(new SelectListItem { Text = item["Descripcion"].ToString(), Value = item["IdTipoComprobante"].ToString() });
                }

                return lstComprobantes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertSerie(int id, string serie, string tipo)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();

                mySqlParameters.Add(new MySqlParameter("SerieIn", MySqlDbType.VarChar, 1) { Value = serie });
                mySqlParameters.Add(new MySqlParameter("IdCarteraIn", MySqlDbType.Int32) { Value = id });
                mySqlParameters.Add(new MySqlParameter("IdTipoComprobanteIn", MySqlDbType.VarChar, 2) { Value = tipo });

                conexion.RunStoredProcedure("registroseriesInsert", mySqlParameters);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EliminarSerie(int id)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("IdNotaIn", MySqlDbType.Int64, 20) { Value = id });
                conexion.RunStoredProcedure("notaslocalidadDelete", mySqlParameters);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

