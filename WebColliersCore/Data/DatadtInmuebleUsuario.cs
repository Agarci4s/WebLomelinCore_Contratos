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
    public class DatadtInmuebleUsuario
    {
        private Conexion conexion = new Conexion();

        public List<DtInmuebleUsuario> Get(int IdUsuario)
        {
              
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            DataTable dataTable = conexion.RunStoredProcedure("dtInmuebleUsuarioGet", listSqlParameters);
            return DataToModel(dataTable);
        }

        public List<DtInmuebleUsuario> GetByCartera(int IdUsuario, int idCartera)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("idCartera_In", idCartera));

            DataTable dataTable = conexion.RunStoredProcedure("dtInmuebleUsuarioCarteraGet", listSqlParameters);
            return DataToModel(dataTable);
        }

        public bool Update(List<DtInmuebleUsuario> dtInmuebleUsuarioOld, List<DtInmuebleUsuario> dtInmuebleUsuarioNew)
        {
            for (int i = dtInmuebleUsuarioOld.Count - 1; i >= 0; i--)
            {
                foreach (var item in dtInmuebleUsuarioNew)
                {
                    if (item.idInmueble == dtInmuebleUsuarioOld[i].idInmueble && item.IdUsuario == dtInmuebleUsuarioOld[i].IdUsuario)
                    {
                        dtInmuebleUsuarioNew.Remove(item);
                        dtInmuebleUsuarioOld.RemoveAt(i);
                        break;
                    }
                }

            }

            foreach (var item in dtInmuebleUsuarioNew)
            {
                  
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("idInmueble_In", item.idInmueble));
                listSqlParameters.Add(new MySqlParameter("IdUsuario_In", item.IdUsuario));
                DataTable dataTable = conexion.RunStoredProcedure("DtInmuebleUsuarioInsert", listSqlParameters);
            }

            foreach (var item in dtInmuebleUsuarioOld)
            {
                  
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("idInmueble_In", item.idInmueble));
                listSqlParameters.Add(new MySqlParameter("IdUsuario_In", item.IdUsuario));
                DataTable dataTable = conexion.RunStoredProcedure("DtInmuebleUsuarioDeleted", listSqlParameters);
            }
            return true;
        }

        private List<DtInmuebleUsuario> DataToModel(DataTable dataTable)
        {
            List<DtInmuebleUsuario> dtInmuebleUsuarioList = new List<DtInmuebleUsuario>();
            foreach (DataRow item in dataTable.Rows)
            {
                DtInmuebleUsuario dtInmuebleUsuario = new DtInmuebleUsuario();

                dtInmuebleUsuario.idInmueble = Int64.Parse(item["idInmueble"].ToString());
                dtInmuebleUsuario.Administrativo = Int64.Parse(item["Administrativo"].ToString());
                dtInmuebleUsuario.NombreInmueble = item["NombreInmueble"].ToString();
                dtInmuebleUsuario.Calle = item["Calle"].ToString();
                dtInmuebleUsuario.Colonia = item["Colonia"].ToString();
                dtInmuebleUsuario.CP = item["CP"].ToString();
                dtInmuebleUsuario.IdCartera = Int64.Parse(item["IdCartera"].ToString());
                Int64 IdInmuebleUsuario = 0;
                Int64.TryParse(item["IdInmuebleUsuario"].ToString(), out IdInmuebleUsuario);

                dtInmuebleUsuario.IdInmuebleUsuario = IdInmuebleUsuario;
                dtInmuebleUsuario.Propietario = item["Propietario"].ToString();
                if (dtInmuebleUsuario.IdInmuebleUsuario > 0)
                    dtInmuebleUsuario.checkAux = true;
                else
                    dtInmuebleUsuario.checkAux = false;
                int IdUsuario = 0;
                int.TryParse(item["IdUsuario"].ToString(), out IdUsuario);
                dtInmuebleUsuario.IdUsuario = IdUsuario;
                dtInmuebleUsuarioList.Add(dtInmuebleUsuario);
            }
            return dtInmuebleUsuarioList;
        }
    }
}
