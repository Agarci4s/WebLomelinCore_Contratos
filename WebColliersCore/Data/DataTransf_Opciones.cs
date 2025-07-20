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
    public class DataTransf_Opciones
    {
        private Conexion conexion = new Conexion();

        public List<Transf_Opciones> recuperaUsuariosActivos()
        {
            // MySqlParameter conn = new MySqlParameter(getConnectionString());
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            //new MySqlParameter("p_journalId", SqlDbType.VarChar) { Value = "test" }
            DataTable dataTable = conexion.RunStoredProcedure("usuariosGet", listSqlParameters);
            return DataToModel(dataTable);
        }

        public List<Transf_Opciones> RecuperaTransf_Opciones()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("transf_opciones_contratosGet", listSqlParameters);
            return DataToModel(dataTable);
        }

        public List<Transf_Opciones> RecuperaTransf_Opciones(int idUsuario)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idUsuario", idUsuario));
            listSqlParameters.Add(new MySqlParameter("activos", true));
            List<Transf_Opciones> listTpcarteraActivos = DataToModel(conexion.RunStoredProcedure("transf_opciones_contratosGetByUsuario", listSqlParameters), true);

            listSqlParameters.RemoveAt(1);
            listSqlParameters.Add(new MySqlParameter("activos", false));
            List<Transf_Opciones> listTpcarteraNoActivos = DataToModel(conexion.RunStoredProcedure("transf_opciones_contratosGetByUsuario", listSqlParameters), false);

            List<Transf_Opciones> listTpcarteras = listTpcarteraActivos.Concat(listTpcarteraNoActivos).ToList().OrderBy(l => l.opcion).ThenBy(s => s.sub).ThenBy(ss => ss.sub_sub).ToList();

            return listTpcarteras;

        }

        public List<Transf_Opciones> RecuperaTransf_Opciones_Controller(int idUsuario, string Controller)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idUsuario_In", idUsuario));
            listSqlParameters.Add(new MySqlParameter("Controller_In", Controller));
            List<Transf_Opciones> list = DataToModel(conexion.RunStoredProcedure("transf_opciones_contratosGetByController", listSqlParameters), true);

            return list;

        }

        public List<Transf_Opciones> RecuperaTransf_Opciones(int idUsuario, string Controller)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idUsuario", idUsuario));
            listSqlParameters.Add(new MySqlParameter("idUsuario", idUsuario));
            listSqlParameters.Add(new MySqlParameter("activos", true));
            List<Transf_Opciones> listTpcarteraActivos = DataToModel(conexion.RunStoredProcedure("transf_opciones_contratosGetByUsuario", listSqlParameters), true);

            listSqlParameters.RemoveAt(1);
            listSqlParameters.Add(new MySqlParameter("activos", false));
            List<Transf_Opciones> listTpcarteraNoActivos = DataToModel(conexion.RunStoredProcedure("transf_opciones_contratosGetByUsuario", listSqlParameters), false);

            List<Transf_Opciones> listTpcarteras = listTpcarteraActivos.Concat(listTpcarteraNoActivos).ToList().OrderBy(l => l.opcion).ThenBy(s => s.sub).ToList();

            return listTpcarteras;

        }

        private List<Transf_Opciones> DataToModel(DataTable dataTable)
        {
            List<Transf_Opciones> listTransf_Opciones = new List<Transf_Opciones>();
            foreach (DataRow item in dataTable.Rows)
            {
                Transf_Opciones transf_Opciones = new Transf_Opciones();
                transf_Opciones.descripcion = item["descripcion"].ToString();
                transf_Opciones.idTransfOpciones = Convert.ToInt32(item["idTransfOpciones"].ToString());
                transf_Opciones.opcion = Convert.ToInt32(item["opcion"].ToString());
                transf_Opciones.sub = Convert.ToInt32(item["sub"].ToString());
                transf_Opciones.NameOpcion = item["NameOpcion"].ToString();
                transf_Opciones.Controller = item["Controller"].ToString();
                transf_Opciones.Action = item["Action"].ToString();
                listTransf_Opciones.Add(transf_Opciones);
            }
            return listTransf_Opciones;
        }

        private List<Transf_Opciones> DataToModel(DataTable dataTable, bool checkTransf_Opciones)
        {
            List<Transf_Opciones> listTransf_Opciones = new List<Transf_Opciones>();
            foreach (DataRow item in dataTable.Rows)
            {
                Transf_Opciones transf_Opciones = new Transf_Opciones();
                transf_Opciones.checkTransf_Opciones = checkTransf_Opciones;
                transf_Opciones.descripcion = item["descripcion"].ToString();
                transf_Opciones.idTransfOpciones = Convert.ToInt32(item["idTransfOpciones"].ToString());
                transf_Opciones.opcion = Convert.ToInt32(item["opcion"].ToString());
                transf_Opciones.sub = Convert.ToInt32(item["sub"].ToString());
                transf_Opciones.sub_sub = Convert.ToInt32(item["sub_sub"].ToString());
                transf_Opciones.NameOpcion = item["NameOpcion"].ToString();
                transf_Opciones.NameSub = item["NameSub"].ToString();
                transf_Opciones.Controller = item["Controller"].ToString();
                transf_Opciones.Action = item["Action"].ToString();
                transf_Opciones.TipoVista = Convert.ToInt32(item["TipoVista"].ToString());
                try
                {
                    transf_Opciones.Nivel = Convert.ToInt32(item["Nivel"].ToString());
                    DataSelectListItem dataSelectListItem = new DataSelectListItem();
                    transf_Opciones.NivelStr = dataSelectListItem.Niveles.FirstOrDefault(x => x.Value == transf_Opciones.Nivel.ToString()).Text;

                }
                catch
                {
                    transf_Opciones.Nivel = 0;
                }

                listTransf_Opciones.Add(transf_Opciones);
            }
            return listTransf_Opciones;
        }
    }
}
