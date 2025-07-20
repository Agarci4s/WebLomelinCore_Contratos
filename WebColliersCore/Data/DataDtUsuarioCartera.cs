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
    public class DataDtUsuarioCartera
    {
        private Conexion conexion = new Conexion();

        //public List<DtUsuarioCartera> recuperaDtUsuarioCartera()
        //{
        //      
        //    List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
        //    listSqlParameters.Add(mySqlParameter);
        //    DataTable dataTable = conexion.RunStoredProcedure("tpCarteraGet", listSqlParameters);
        //    return DataToModel(dataTable);
        //}

        public bool guardaDtUsuarioCartera(List<DtUsuarioCartera> listTpCarteras)
        {
            foreach (var item in listTpCarteras)
            {
                  
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("IdCartera", item.idCartera));
                listSqlParameters.Add(new MySqlParameter("IdUsuario", item.idUsuario));
                DataTable dataTable = conexion.RunStoredProcedure("dtusuariocarteraInsert", listSqlParameters);
            }
            return true;
        }

        public bool DeleteDtUsuarioCartera(List<DtUsuarioCartera> dtUsuarioCarteras)
        {
            foreach (var item in dtUsuarioCarteras)
            {
                  
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("IdCartera", item.idCartera));
                listSqlParameters.Add(new MySqlParameter("IdUsuario", item.idUsuario));
                DataTable dataTable = conexion.RunStoredProcedure("dtusuariocarteraDelete", listSqlParameters);
            }
            return true;
        }

        public bool UpdateDtUsuarioCartera(List<DtUsuarioCartera> dtUsuarioCarterasOld, List<DtUsuarioCartera> dtUsuarioCarterasNew)
        {
            for (int i = dtUsuarioCarterasOld.Count - 1; i >= 0; i--)
            {
                foreach (var item in dtUsuarioCarterasNew)
                {
                    if (item.idCartera == dtUsuarioCarterasOld[i].idCartera && item.idUsuario == dtUsuarioCarterasOld[i].idUsuario)
                    {
                        dtUsuarioCarterasNew.Remove(item);
                        dtUsuarioCarterasOld.RemoveAt(i);
                        break;
                    }
                }

            }

            foreach (var item in dtUsuarioCarterasNew)
            {
                  
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("IdCartera", item.idCartera));
                listSqlParameters.Add(new MySqlParameter("IdUsuario", item.idUsuario));
                DataTable dataTable = conexion.RunStoredProcedure("dtusuariocarteraInsert", listSqlParameters);
            }

            foreach (var item in dtUsuarioCarterasOld)
            {
                  
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("IdCartera", item.idCartera));
                listSqlParameters.Add(new MySqlParameter("IdUsuario", item.idUsuario));
                DataTable dataTable = conexion.RunStoredProcedure("dtusuariocarteraDelete", listSqlParameters);
            }
            return true;
        }

        private List<DtUsuarioCartera> DataToModel(DataTable dataTable)
        {
            List<DtUsuarioCartera> listDtUsuarioCartera = new List<DtUsuarioCartera>();
            foreach (DataRow item in dataTable.Rows)
            {
                DtUsuarioCartera dtUsuarioCartera = new DtUsuarioCartera();

                dtUsuarioCartera.idCartera = Int32.Parse(item["idCartera"].ToString());
                dtUsuarioCartera.idUsuario |= Int32.Parse(item["idUsuario"].ToString());
                dtUsuarioCartera.idUsuarioCartera = Int32.Parse(item["idUsuarioCartera"].ToString());
                listDtUsuarioCartera.Add(dtUsuarioCartera);
            }
            return listDtUsuarioCartera;
        }
    }
}
