using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;
namespace WebLomelinCore.Data
{
    public class DataInmueblesExpedienteContratos
    {
        private Conexion conexion = new Conexion();

        public List<B_cg_tipo_expediente_contratos> Get()
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            DataTable dataTable = conexion.RunStoredProcedure("b_cg_tipo_expediente_contratos_get", listSqlParameters);
            return DataToModelCG(dataTable);
        }
        public int Insert(B_inmuebles_expediente_detalle_contratos b_inmuebles_expediente_detalle_contratos)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles", b_inmuebles_expediente_detalle_contratos.id_b_inmuebles));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_tipo_expediente_contratos", b_inmuebles_expediente_detalle_contratos.id_b_cg_tipo_expediente_contratos));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_periodicidad_contratos", b_inmuebles_expediente_detalle_contratos.id_b_cg_periodicidad_contratos));
            listSqlParameters.Add(new MySqlParameter("ruta", b_inmuebles_expediente_detalle_contratos.ruta));
            listSqlParameters.Add(new MySqlParameter("periodo", b_inmuebles_expediente_detalle_contratos.periodo));
            listSqlParameters.Add(new MySqlParameter("fecha_periodo_inicio", b_inmuebles_expediente_detalle_contratos.fecha_periodo_inicio));
            listSqlParameters.Add(new MySqlParameter("fecha_periodo_fin", b_inmuebles_expediente_detalle_contratos.fecha_periodo_fin));

            List<MySqlParameter> listSqlParametersOUT = new List<MySqlParameter>();
            listSqlParametersOUT.Add(new MySqlParameter("idOUT", b_inmuebles_expediente_detalle_contratos.id_b_inmuebles_expediente_detalle_contratos));
            MySqlParameterCollection mySqlParameterCollection = conexion.RunStoredProcedure("b_inmuebles_expediente_detalle_contratos_insert", listSqlParameters, listSqlParametersOUT);
            int id = (int)mySqlParameterCollection["idOUT"].Value;

            return id;

        }

        public List<B_inmuebles_expediente_detalle_contratos> GetById(int id_b_inmuebles)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_in", id_b_inmuebles));
            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_expediente_detalle_contratos_get", listSqlParameters);

            return DataToModel(dataTable);

        }

        private List<B_inmuebles_expediente_detalle_contratos> DataToModel(DataTable dataTable)
        {
            List<B_inmuebles_expediente_detalle_contratos> b_inmuebles_expediente_detalle_contratosList = new List<B_inmuebles_expediente_detalle_contratos>();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    B_inmuebles_expediente_detalle_contratos b_inmuebles_expediente_detalle_contratos = new B_inmuebles_expediente_detalle_contratos();

                    b_inmuebles_expediente_detalle_contratos.id_b_inmuebles_expediente_detalle_contratos = int.Parse(item["id_b_inmuebles_expediente_detalle_contratos"].ToString());
                    b_inmuebles_expediente_detalle_contratos.id_b_inmuebles = int.Parse(item["id_b_inmuebles"].ToString());
                    b_inmuebles_expediente_detalle_contratos.id_b_cg_tipo_expediente_contratos = int.Parse(item["id_b_cg_tipo_expediente_contratos"].ToString());
                    b_inmuebles_expediente_detalle_contratos.id_b_cg_periodicidad_contratos = int.Parse(item["id_b_cg_periodicidad_contratos"].ToString());
                    b_inmuebles_expediente_detalle_contratos.ruta = item["ruta"].ToString();
                    b_inmuebles_expediente_detalle_contratos.periodo = int.Parse(item["periodo"].ToString());
                    b_inmuebles_expediente_detalle_contratos.fecha_periodo_inicio = Convert.ToDateTime(item["fecha_periodo_inicio"].ToString());
                    b_inmuebles_expediente_detalle_contratos.fecha_periodo_fin = Convert.ToDateTime(item["fecha_periodo_fin"].ToString());
                   
                    b_inmuebles_expediente_detalle_contratosList.Add(b_inmuebles_expediente_detalle_contratos);
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            return b_inmuebles_expediente_detalle_contratosList;
        }

        private List<B_cg_tipo_expediente_contratos> DataToModelCG(DataTable dataTable)
        {
            List<B_cg_tipo_expediente_contratos> b_cg_tipo_expediente_contratosList = new List<B_cg_tipo_expediente_contratos>();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    B_cg_tipo_expediente_contratos b_cg_tipo_expediente_contratos = new B_cg_tipo_expediente_contratos();

                    b_cg_tipo_expediente_contratos.id_b_cg_tipo_expediente_contratos = int.Parse(item["id_b_cg_tipo_expediente_contratos"].ToString());
                    b_cg_tipo_expediente_contratos.categoria = item["categoria"].ToString();
                    b_cg_tipo_expediente_contratos.sub_categoria = item["sub_categoria"].ToString();
                    b_cg_tipo_expediente_contratos.periodo = Convert.ToBoolean(Convert.ToInt16(item["periodo"].ToString()));
                    //b_cg_tipo_expediente_contratos.status = Convert.ToBoolean(Convert.ToInt16(item["cortesias"].ToString()));

                    b_cg_tipo_expediente_contratosList.Add(b_cg_tipo_expediente_contratos);
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            return b_cg_tipo_expediente_contratosList;
        }


    }
}

