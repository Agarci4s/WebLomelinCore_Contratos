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
    public class DataPuesto
    {
        private Conexion conexion = new Conexion();

        public List<DtActividadPuesto> Get(int idactividad_puesto)
        {
              
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idactividad_puesto_In", idactividad_puesto));
            DataTable dataTable = conexion.RunStoredProcedure("dtactividad_puestoGet", listSqlParameters);
            return DataToModel(dataTable);
        }

        public bool Insert(DtActividadPuesto dtActividadPuesto)
        {
              
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("actividad_In", dtActividadPuesto.actividad));
            conexion.RunStoredProcedure("dtactividad_puestoInsert", listSqlParameters);
            return true;
        }

        public bool Delete(int idactividad_puesto)
        {
              
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idactividad_puesto_In", idactividad_puesto));
          conexion.RunStoredProcedure("dtactividad_puestoDelete", listSqlParameters);
            return true;
        }

        public bool Update(DtActividadPuesto dtActividadPuesto)
        {
              
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idactividad_puesto_In", dtActividadPuesto.idactividad_puesto));
            listSqlParameters.Add(new MySqlParameter("actividad_In", dtActividadPuesto.actividad));
            conexion.RunStoredProcedure("dtactividad_puestoUpdate", listSqlParameters);
            return true;
        }

        private List<DtActividadPuesto> DataToModel(DataTable dataTable)
        {
            List<DtActividadPuesto> dtActividadPuestoList = new List<DtActividadPuesto>();
            foreach (DataRow item in dataTable.Rows)
            {
                DtActividadPuesto dtActividadPuesto = new DtActividadPuesto();

                dtActividadPuesto.idactividad_puesto = int.Parse(item["idactividad_puesto"].ToString());
                dtActividadPuesto.actividad = item["actividad"].ToString();
                dtActividadPuestoList.Add(dtActividadPuesto);
            }
            return dtActividadPuestoList;
        }


    }
}
