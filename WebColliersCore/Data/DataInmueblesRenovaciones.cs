using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebColliersCore.Data;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;
using WebLomelinCore.Models;

namespace WebLomelinCore.Data
{
    public class DataInmueblesRenovaciones : Conexion
    {
        private Conexion conexion = new Conexion();

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

        public NegociacionesRenovacion Negociacion_contratos_get(int IdCartera, int IdUsuario, int id_b_inmuebles_contrato)
        {

            List<MySqlParameter> listSqlParameters = new()
            {
                new MySqlParameter("IdCartera_In", IdCartera),
                new MySqlParameter("IdUsuario_In", IdUsuario),
                new MySqlParameter("id_b_inmuebles_contrato_In", id_b_inmuebles_contrato)
            };

            DataTable dataTable = RunStoredProcedure("negociacion_contratos_getByContrato", listSqlParameters);

            return DataToModelRenovacion(dataTable).FirstOrDefault(new NegociacionesRenovacion());
        }

        public int Negociacion_contratos_insert(NegociacionesRenovacion renovacion)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_In", renovacion.id_b_inmuebles));
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_contrato_In", renovacion.id_b_inmuebles_contrato));
            listSqlParameters.Add(new MySqlParameter("inpc_In", renovacion.inpc));
            listSqlParameters.Add(new MySqlParameter("porcentaje_negociar_In", renovacion.porcentaje_negociar));
            listSqlParameters.Add(new MySqlParameter("rentaminima_In", renovacion.Rentaminima));
            listSqlParameters.Add(new MySqlParameter("rentamaxima_In", renovacion.Rentamaxima));
            listSqlParameters.Add(new MySqlParameter("vigenteAl_In", renovacion.VigenteAl));
            listSqlParameters.Add(new MySqlParameter("rentaActual_In", renovacion.RentaActual));
            listSqlParameters.Add(new MySqlParameter("PrecioM2RentaActual_In", renovacion.PrecioM2RentaActual));
            listSqlParameters.Add(new MySqlParameter("rentaPropuesta_In", renovacion.RentaPropuesta));
            listSqlParameters.Add(new MySqlParameter("PrecioM2RentaPropuesta_In", renovacion.PrecioM2RentaPropuesta));
            listSqlParameters.Add(new MySqlParameter("topeRenta_In", renovacion.TopeRenta));
            listSqlParameters.Add(new MySqlParameter("PrecioM2TopeRenta_In", renovacion.PrecioM2TopeRenta));
            listSqlParameters.Add(new MySqlParameter("RentaPactada_In", renovacion.RentaPactada));
            listSqlParameters.Add(new MySqlParameter("observaciones_In", renovacion.Observaciones));
            listSqlParameters.Add(new MySqlParameter("EscrituraPublicaActaConst_In", renovacion.EscrituraPublicaActaConst));
            listSqlParameters.Add(new MySqlParameter("NumeroNotarioActaConst_In", renovacion.NumeroNotarioActaConst));
            listSqlParameters.Add(new MySqlParameter("DomicilioEmpresa_In", renovacion.DomicilioEmpresa));
            listSqlParameters.Add(new MySqlParameter("FechaPoderNotarial_In", renovacion.FechaPoderNotarial));
            listSqlParameters.Add(new MySqlParameter("FechaEscrituraActaConst_In", renovacion.FechaEscrituraActaConst));
            listSqlParameters.Add(new MySqlParameter("NumeroFolioMercantilActaConst_In", renovacion.NumeroFolioMercantilActaConst));
            listSqlParameters.Add(new MySqlParameter("RFCEmpresa_In", renovacion.RFCEmpresa));
            listSqlParameters.Add(new MySqlParameter("NumeroNotarioPoderNotaria_In", renovacion.NumeroNotarioPoderNotaria));
            listSqlParameters.Add(new MySqlParameter("NombreNotarioActaConst_In", renovacion.NombreNotarioActaConst));
            listSqlParameters.Add(new MySqlParameter("FechaFolioActaConst_In", renovacion.FechaFolioActaConst));
            listSqlParameters.Add(new MySqlParameter("NombreRepresentanteLegal_In", renovacion.NombreRepresentanteLegal));
            listSqlParameters.Add(new MySqlParameter("NombreNotarioPoderNotarial_In", renovacion.NombreNotarioPoderNotarial));
            listSqlParameters.Add(new MySqlParameter("plazoPFAnio_In", renovacion.plazoPFAnio));
            listSqlParameters.Add(new MySqlParameter("plazoPFMes_In", renovacion.plazoPFMes));
            listSqlParameters.Add(new MySqlParameter("plazoPFdia_In", renovacion.plazoPFdia));
            listSqlParameters.Add(new MySqlParameter("DAPF_In", renovacion.DAPF));
            listSqlParameters.Add(new MySqlParameter("plazoPVAnio_In", renovacion.plazoPVAnio));
            listSqlParameters.Add(new MySqlParameter("plazoPVMes_In", renovacion.plazoPVMes));
            listSqlParameters.Add(new MySqlParameter("plazoPVDia_In", renovacion.plazoPVDia));
            listSqlParameters.Add(new MySqlParameter("DAPV_In", renovacion.DAPV));
            listSqlParameters.Add(new MySqlParameter("plazoIFAnio_In", renovacion.plazoIFAnio));
            listSqlParameters.Add(new MySqlParameter("plazoIFMes_In", renovacion.plazoIFMes));
            listSqlParameters.Add(new MySqlParameter("plazoIFDia_In", renovacion.plazoIFDia));
            listSqlParameters.Add(new MySqlParameter("DAIF_In", renovacion.DAIF));
            listSqlParameters.Add(new MySqlParameter("plazoIVAnio_In", renovacion.plazoIVAnio));
            listSqlParameters.Add(new MySqlParameter("plazoIVMes_In", renovacion.plazoIVMes));
            listSqlParameters.Add(new MySqlParameter("plazoIVDia_In", renovacion.plazoIVDia));
            listSqlParameters.Add(new MySqlParameter("DAIV_In", renovacion.DAIV));


            List<MySqlParameter> listSqlParametersOUT = new List<MySqlParameter>();
            listSqlParametersOUT.Add(new MySqlParameter("id_b_inmuebles_contrato_Out", renovacion.id_b_inmuebles_contrato));

            MySqlParameterCollection mySqlParameterCollection = conexion.RunStoredProcedure("negociacion_contratos_insert", listSqlParameters, listSqlParametersOUT);
            int id = (int)mySqlParameterCollection["id_b_inmuebles_contrato_Out"].Value;

            return id;
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
            List<NegociacionesRenovacion> negociacionesRenovacionList = new List<NegociacionesRenovacion>();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    NegociacionesRenovacion negociacionesRenovacion = new NegociacionesRenovacion();

                    //Id's
                    negociacionesRenovacion.id_negociacion_contratos = int.Parse(item["id_negociacion_contratos"].ToString());
                    negociacionesRenovacion.id_b_inmuebles = int.Parse(item["id_b_inmuebles"].ToString());
                    negociacionesRenovacion.id_b_inmuebles_contrato = int.Parse(item["id_b_inmuebles_contrato"].ToString());


                    // Revisión
                    negociacionesRenovacion.inpc = decimal.Parse(item["inpc"].ToString());
                    negociacionesRenovacion.porcentaje_negociar = decimal.Parse(item["porcentaje_negociar"].ToString());


                    negociacionesRenovacion.Rentaminima = decimal.Parse(item["Rentaminima"].ToString());
                    negociacionesRenovacion.Rentamaxima = decimal.Parse(item["Rentamaxima"].ToString());
                    negociacionesRenovacion.VigenteAl = Convert.ToDateTime(item["VigenteAl"].ToString());

                    negociacionesRenovacion.RentaActual = decimal.Parse(item["RentaActual"].ToString());
                    negociacionesRenovacion.PrecioM2RentaActual = decimal.Parse(item["PrecioM2RentaActual"].ToString());

                    negociacionesRenovacion.RentaPropuesta = decimal.Parse(item["RentaPropuesta"].ToString());
                    negociacionesRenovacion.PrecioM2RentaPropuesta = decimal.Parse(item["PrecioM2RentaPropuesta"].ToString());

                    negociacionesRenovacion.TopeRenta = decimal.Parse(item["TopeRenta"].ToString());
                    negociacionesRenovacion.PrecioM2TopeRenta = decimal.Parse(item["PrecioM2TopeRenta"].ToString());

                    negociacionesRenovacion.RentaPactada = decimal.Parse(item["RentaPactada"].ToString());
                    negociacionesRenovacion.Observaciones = item["Observaciones"].ToString();

                    // Representante
                    negociacionesRenovacion.EscrituraPublicaActaConst = item["EscrituraPublicaActaConst"].ToString();
                    negociacionesRenovacion.NumeroNotarioActaConst = item["NumeroNotarioActaConst"].ToString();
                    negociacionesRenovacion.DomicilioEmpresa = item["DomicilioEmpresa"].ToString();
                    negociacionesRenovacion.FechaPoderNotarial = Convert.ToDateTime(item["FechaPoderNotarial"].ToString());
                    negociacionesRenovacion.FechaEscrituraActaConst = Convert.ToDateTime(item["FechaEscrituraActaConst"].ToString());
                    negociacionesRenovacion.NumeroFolioMercantilActaConst = item["NumeroFolioMercantilActaConst"].ToString();
                    negociacionesRenovacion.RFCEmpresa = item["RFCEmpresa"].ToString();
                    negociacionesRenovacion.NumeroNotarioPoderNotaria = item["NumeroNotarioPoderNotaria"].ToString();
                    negociacionesRenovacion.NombreNotarioActaConst = item["NombreNotarioActaConst"].ToString();
                    negociacionesRenovacion.FechaFolioActaConst = Convert.ToDateTime(item["FechaFolioActaConst"].ToString());
                    negociacionesRenovacion.NombreRepresentanteLegal = item["NombreRepresentanteLegal"].ToString();
                    negociacionesRenovacion.NombreNotarioPoderNotarial = item["NombreNotarioPoderNotarial"].ToString();

                    // Plazos 
                    negociacionesRenovacion.plazoPFAnio = int.Parse(item["plazoPFAnio"].ToString());
                    negociacionesRenovacion.plazoPFMes = int.Parse(item["plazoPFMes"].ToString());
                    negociacionesRenovacion.plazoPFdia = int.Parse(item["plazoPFdia"].ToString());
                    negociacionesRenovacion.DAPF = int.Parse(item["DAPF"].ToString());

                    negociacionesRenovacion.plazoPVAnio = int.Parse(item["plazoPVAnio"].ToString());
                    negociacionesRenovacion.plazoPVMes = int.Parse(item["plazoPVMes"].ToString());
                    negociacionesRenovacion.plazoPVDia = int.Parse(item["plazoPVDia"].ToString());
                    negociacionesRenovacion.DAPV = int.Parse(item["DAPV"].ToString());

                    negociacionesRenovacion.plazoIFAnio = int.Parse(item["plazoIFAnio"].ToString());
                    negociacionesRenovacion.plazoIFMes = int.Parse(item["plazoIFMes"].ToString());
                    negociacionesRenovacion.plazoIFDia = int.Parse(item["plazoIFDia"].ToString());
                    negociacionesRenovacion.DAIF = int.Parse(item["DAIF"].ToString());

                    negociacionesRenovacion.plazoIVAnio = int.Parse(item["plazoIVAnio"].ToString());
                    negociacionesRenovacion.plazoIVMes = int.Parse(item["plazoIVMes"].ToString());
                    negociacionesRenovacion.plazoIVDia = int.Parse(item["plazoIVDia"].ToString());
                    negociacionesRenovacion.DAIV = int.Parse(item["DAIV"].ToString());



                    negociacionesRenovacionList.Add(negociacionesRenovacion);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return negociacionesRenovacionList;
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
