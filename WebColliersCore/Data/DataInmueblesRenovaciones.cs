using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;
using WebLomelinCore.Models;

namespace WebLomelinCore.Data
{
    public class DataInmueblesRenovaciones : Conexion
    {
        public RenovacionAdela GetById(int IdCartera, int IdUsuario, int id_b_inmuebles_contrato)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_contrato_In", id_b_inmuebles_contrato));

            DataTable dataTable = RunStoredProcedure("b_inmuebles_ContratosByIdGet", listSqlParameters);

            return DataToModel(dataTable).FirstOrDefault();
        }

        private List<RenovacionAdela> DataToModel(DataTable dataTable)
        {
            List<RenovacionAdela> b_Inmuebles_ContratosList = new();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    RenovacionAdela renovacionAdela = new()
                    {                        
                        Rentaminima = int.Parse(item["LocalesAgrupados"].ToString()),
                        Rentamaxima = int.Parse(item["LocalesAgrupados"].ToString()),
                        VigenteAl = Convert.ToDateTime(item["fecha_inicio"].ToString()),                        
                    };
                    
                    b_Inmuebles_ContratosList.Add(renovacionAdela);
                }
                catch (Exception e)
                {
                    throw;
                }



            }
            return b_Inmuebles_ContratosList;
        }
    }
}
