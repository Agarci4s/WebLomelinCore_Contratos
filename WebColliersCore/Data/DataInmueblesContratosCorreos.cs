using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebColliersCore.DataAccess;
using System.Data;
using MySql.Data.MySqlClient;
using WebColliersCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace WebColliersCore.Data
{
    public class DataInmueblesContratosCorreos
    {
        private Conexion conexion = new Conexion();     

        public List<B_inmuebles_contrato_correos> Get(int IdCartera, int IdUsuario, int id_b_inmuebles_contrato)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_contrato_In", id_b_inmuebles_contrato));

            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_Contratos_correosGet", listSqlParameters);
            return DataToModel(dataTable);
        }

        public B_inmuebles_contrato_correos GetById(int IdCartera, int IdUsuario, int id_b_inmuebles_contrato_correo)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_contrato_correo_In", id_b_inmuebles_contrato_correo));

            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_Contratos_correosGetById", listSqlParameters);
            return DataToModel(dataTable).FirstOrDefault(); ;
        }

        public void Delete(B_inmuebles_contrato_correos b_Inmuebles_Contrato_Correos)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_contrato_correo_In", b_Inmuebles_Contrato_Correos.id_b_inmuebles_contrato_correo));

            conexion.RunStoredProcedure("b_inmuebles_Contratos_correosDelete", listSqlParameters);
        }

        public void Edit(B_inmuebles_contrato_correos b_Inmuebles_Contrato_Correos)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_contrato_correo_In", b_Inmuebles_Contrato_Correos.id_b_inmuebles_contrato_correo));
            listSqlParameters.Add(new MySqlParameter("nombre_In", b_Inmuebles_Contrato_Correos.nombre));
            listSqlParameters.Add(new MySqlParameter("correo_In", b_Inmuebles_Contrato_Correos.correo));
            listSqlParameters.Add(new MySqlParameter("telefono_In", b_Inmuebles_Contrato_Correos.telefono));

            conexion.RunStoredProcedure("b_inmuebles_Contratos_correosUpdate", listSqlParameters);
        }

        public void Insert(B_inmuebles_contrato_correos b_Inmuebles_Contrato_Correos)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_contrato_In", b_Inmuebles_Contrato_Correos.id_b_inmuebles_contrato));
            listSqlParameters.Add(new MySqlParameter("nombre_In", b_Inmuebles_Contrato_Correos.nombre));
            listSqlParameters.Add(new MySqlParameter("correo_In", b_Inmuebles_Contrato_Correos.correo));
            listSqlParameters.Add(new MySqlParameter("telefono_In", b_Inmuebles_Contrato_Correos.telefono));

            conexion.RunStoredProcedure("b_inmuebles_Contratos_correosInsert", listSqlParameters);
        }

        private List<B_inmuebles_contrato_correos> DataToModel(DataTable dataTable)
        {
            List<B_inmuebles_contrato_correos> List_b_Inmuebles_Contrato_Correos = new List<B_inmuebles_contrato_correos>();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    B_inmuebles_contrato_correos b_Inmuebles_Contrato_Correos = new B_inmuebles_contrato_correos();

                    b_Inmuebles_Contrato_Correos.id_b_inmuebles_contrato_correo = int.Parse(item["id_b_inmuebles_contrato_correo"].ToString());
                    b_Inmuebles_Contrato_Correos.id_b_inmuebles_contrato = int.Parse(item["id_b_inmuebles_contrato"].ToString());
                    b_Inmuebles_Contrato_Correos.nombre = item["nombre"].ToString();
                    b_Inmuebles_Contrato_Correos.telefono = item["telefono"].ToString();
                    b_Inmuebles_Contrato_Correos.correo = item["correo"].ToString();

                    List_b_Inmuebles_Contrato_Correos.Add(b_Inmuebles_Contrato_Correos);
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            return List_b_Inmuebles_Contrato_Correos;
        }


    }
}
