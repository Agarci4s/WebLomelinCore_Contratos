using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;
using System.Linq;
using WebColliersCore.Data;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using WebLomelinCore.Models;

namespace WebLomelinCore.Data
{
    public class DataInmueblesVisitaMotivoEspecial
    {
        private Conexion conexion = new Conexion();

        public List<B_inmuebles_visitas_motivo_especial> Get(int id_b_inmuebles_visita)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("d_b_inmuebles_visitaIn", id_b_inmuebles_visita));
            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_visitas_motivo_especialGet", listSqlParameters);
            return DataToModel(dataTable);
        }


        public void Insert(int id_b_cg_motivo_especial, int d_b_inmuebles_visita)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("id_b_cg_motivo_especialIn", id_b_cg_motivo_especial));
            listSqlParameters.Add(new MySqlParameter("d_b_inmuebles_visitaIn", d_b_inmuebles_visita));

            conexion.RunStoredProcedure("b_inmuebles_visitas_motivo_especialInsert", listSqlParameters);
            return;
        }

        public void Delete(int id_b_cg_motivo_especial, int d_b_inmuebles_visita)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("id_b_cg_motivo_especialIn", id_b_cg_motivo_especial));
            listSqlParameters.Add(new MySqlParameter("d_b_inmuebles_visitaIn", d_b_inmuebles_visita));

            conexion.RunStoredProcedure("b_inmuebles_visitas_motivo_especialDelete", listSqlParameters);
            return;
        }

        private List<B_inmuebles_visitas_motivo_especial> DataToModel(DataTable dataTable)
        {
            List<B_inmuebles_visitas_motivo_especial> b_inmuebles_visitas_motivo_especialList = new List<B_inmuebles_visitas_motivo_especial>();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    B_inmuebles_visitas_motivo_especial B_inmuebles_visitas_motivo_especial = new B_inmuebles_visitas_motivo_especial();

                    B_inmuebles_visitas_motivo_especial.descripcion = item["descripcion"].ToString();
                    B_inmuebles_visitas_motivo_especial.status = Convert.ToBoolean(Convert.ToInt16(item["status"].ToString()));
                    B_inmuebles_visitas_motivo_especial.id_b_cg_motivo_especial = int.Parse(item["id_b_cg_motivo_especial"].ToString());


                    b_inmuebles_visitas_motivo_especialList.Add(B_inmuebles_visitas_motivo_especial);
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            return b_inmuebles_visitas_motivo_especialList;
        }


    }
}

