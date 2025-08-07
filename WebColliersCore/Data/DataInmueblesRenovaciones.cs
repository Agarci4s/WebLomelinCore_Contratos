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

            List<MySqlParameter> listSqlParameters = new()
            {
                new MySqlParameter("IdCartera_In", IdCartera),
                new MySqlParameter("IdUsuario_In", IdUsuario),
                new MySqlParameter("id_b_inmuebles_contrato_In", id_b_inmuebles_contrato)
            };

            DataTable dataTable = RunStoredProcedure("b_inmuebles_ContratosByIdGet", listSqlParameters);

            return DataToModel(dataTable).FirstOrDefault();
        }

        public NegociacionesAdela GetByIdAdela(int IdCartera, int IdUsuario, int id_b_inmuebles_contrato)
        {

            List<MySqlParameter> listSqlParameters = new()
            {
                new MySqlParameter("IdCartera_In", IdCartera),
                new MySqlParameter("IdUsuario_In", IdUsuario),
                new MySqlParameter("id_b_inmuebles_contrato_In", id_b_inmuebles_contrato)
            };

            DataTable dataTable = RunStoredProcedure("b_inmuebles_ContratosByIdGet", listSqlParameters);

            return DataToModelAdela(dataTable).FirstOrDefault();
        }

        public NegociacionesRenovacion GetByIdRenovacion(int IdCartera, int IdUsuario, int id_b_inmuebles_contrato)
        {

            List<MySqlParameter> listSqlParameters = new()
            {
                new MySqlParameter("IdCartera_In", IdCartera),
                new MySqlParameter("IdUsuario_In", IdUsuario),
                new MySqlParameter("id_b_inmuebles_contrato_In", id_b_inmuebles_contrato)
            };

            DataTable dataTable = RunStoredProcedure("b_inmuebles_ContratosByIdGet", listSqlParameters);

            return DataToModelRenovacion(dataTable).FirstOrDefault();
        }

        public NegociacionesConvenioModificatorio GetByIdConvenioModificatorio(int IdCartera, int IdUsuario, int id_b_inmuebles_contrato)
        {

            List<MySqlParameter> listSqlParameters = new()
            {
                new MySqlParameter("IdCartera_In", IdCartera),
                new MySqlParameter("IdUsuario_In", IdUsuario),
                new MySqlParameter("id_b_inmuebles_contrato_In", id_b_inmuebles_contrato)
            };

            DataTable dataTable = RunStoredProcedure("b_inmuebles_ContratosByIdGet", listSqlParameters);

            return DataToModelConvenioModificatorio(dataTable).FirstOrDefault();
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

        private List<NegociacionesAdela> DataToModelAdela(DataTable dataTable)
        {
            List<NegociacionesAdela> b_Inmuebles_ContratosList = new();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    NegociacionesAdela renovacionAdela = new()
                    {
                        NumProrrateo = item["LocalesAgrupados"].ToString(),                        
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

        private List<NegociacionesRenovacion> DataToModelRenovacion(DataTable dataTable)
        {
            List<NegociacionesRenovacion> b_Inmuebles_ContratosList = new();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    NegociacionesRenovacion renovacion = new()
                    {
                        NumProrrateo = item["LocalesAgrupados"].ToString(),
                    };

                    b_Inmuebles_ContratosList.Add(renovacion);
                }
                catch (Exception e)
                {
                    throw;
                }



            }
            return b_Inmuebles_ContratosList;
        }

        private List<NegociacionesConvenioModificatorio> DataToModelConvenioModificatorio(DataTable dataTable)
        {
            List<NegociacionesConvenioModificatorio> b_Inmuebles_ContratosList = new();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    NegociacionesConvenioModificatorio renovacionConvenioModificatorio = new()
                    {
                        NumProrrateo = item["LocalesAgrupados"].ToString(),
                    };

                    b_Inmuebles_ContratosList.Add(renovacionConvenioModificatorio);
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
