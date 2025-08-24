using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;

namespace WebColliersCore.Data
{
    public class DataInmueblesContratos
    {
        private Conexion conexion = new Conexion();

        public List<B_inmuebles_contrato> GetReporte(int idCartera, int idUsuario, bool correo_enviado, DateTime fecha, DateTime fecha2)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idCarteraIn", idCartera));
            listSqlParameters.Add(new MySqlParameter("idUsuarioIn", idUsuario));
            listSqlParameters.Add(new MySqlParameter("correo_enviadoIN", correo_enviado));
            listSqlParameters.Add(new MySqlParameter("fechaIn", fecha));
            listSqlParameters.Add(new MySqlParameter("fecha2In", fecha2));

            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_contratoReporteGet", listSqlParameters);
            return DataToModel(dataTable);
        }

        public List<B_inmuebles_contrato> GetReporte(int idCartera, int idUsuario, int campo, int meses)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idCarteraIn", idCartera));
            listSqlParameters.Add(new MySqlParameter("idUsuarioIn", idUsuario));            
            //listSqlParameters.Add(new MySqlParameter("fechaIn", fecha));
            //listSqlParameters.Add(new MySqlParameter("fecha2In", fecha2));
            listSqlParameters.Add(new MySqlParameter("CampoIn", campo));
            listSqlParameters.Add(new MySqlParameter("pMeses", meses));

            //DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_contratoGetListado", listSqlParameters);
            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_contratosPVGetList", listSqlParameters);
            return DataToModel(dataTable);
        }

        public List<B_inmuebles_contrato> GetReporteAll(int idCartera, int idUsuario, bool correo_enviado, DateTime fecha, DateTime fecha2)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idCarteraIn", idCartera));
            listSqlParameters.Add(new MySqlParameter("idUsuarioIn", idUsuario));
            listSqlParameters.Add(new MySqlParameter("correo_enviadoIN", correo_enviado));
            listSqlParameters.Add(new MySqlParameter("fechaIn", fecha));
            listSqlParameters.Add(new MySqlParameter("fecha2In", fecha2));

            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_contratoReporteGetAll", listSqlParameters);
            return DataToModel(dataTable);
        }

        public List<B_inmuebles_contrato> Get(int IdCartera, int IdUsuario, int id_b_inmuebles_In)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_In", id_b_inmuebles_In));

            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_ContratosGet", listSqlParameters);

            return DataToModel(dataTable);
        }


        public B_inmuebles_contrato GetById(int IdCartera, int IdUsuario, int id_b_inmuebles_contrato)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_contrato_In", id_b_inmuebles_contrato));

            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_ContratosByIdGet", listSqlParameters);

            return DataToModel(dataTable).FirstOrDefault();
        }

        public void Delete(B_inmuebles_contrato b_Inmuebles_Contrato)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_contrato_In", b_Inmuebles_Contrato.id_b_inmuebles_contrato));

            conexion.RunStoredProcedure("b_inmuebles_ContratosDelete", listSqlParameters);
        }

        public void CorreoEnviado(int id_b_inmuebles_contrato)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_contrato_In", id_b_inmuebles_contrato));

            conexion.RunStoredProcedure("b_inmuebles_ContratosUpdateCorreo", listSqlParameters);

        }

        public void Edit(B_inmuebles_contrato b_Inmuebles_Contrato)
        {

            List<MySqlParameter> listSqlParameters = new()
            {
                new MySqlParameter("id_b_inmuebles_contrato_In", b_Inmuebles_Contrato.id_b_inmuebles_contrato),
                new MySqlParameter("id_b_inmuebles_In", b_Inmuebles_Contrato.id_b_inmuebles),
                new MySqlParameter("id_b_cg_contrato_tipo_In", b_Inmuebles_Contrato.id_b_cg_contrato_tipo),
                new MySqlParameter("id_b_cg_contrato_estatu_In", b_Inmuebles_Contrato.id_b_cg_contrato_estatu),
                new MySqlParameter("contrato_In", b_Inmuebles_Contrato.contrato),
                new MySqlParameter("renta_In", b_Inmuebles_Contrato.renta),
                new MySqlParameter("fecha_inicio_In", b_Inmuebles_Contrato.fecha_inicio),
                new MySqlParameter("fecha_termino_In", b_Inmuebles_Contrato.fecha_termino),
                new MySqlParameter("fecha_anticipacion_In", b_Inmuebles_Contrato.fecha_anticipacion),
                new MySqlParameter("fecha_obligado_In", b_Inmuebles_Contrato.fecha_obligado),
                new MySqlParameter("litigio_In", b_Inmuebles_Contrato.litigio),
                new MySqlParameter("fecha_revision_In", b_Inmuebles_Contrato.fecha_revision),
                new MySqlParameter("plazo_In", b_Inmuebles_Contrato.plazo),
                new MySqlParameter("base_revision_In", b_Inmuebles_Contrato.base_revision),
                new MySqlParameter("especial_In", b_Inmuebles_Contrato.especial),
                new MySqlParameter("correo_especial_In", b_Inmuebles_Contrato.correo_especial ?? ""),
                new MySqlParameter("observaciones_In", b_Inmuebles_Contrato.observaciones ?? ""),
                new MySqlParameter("incp_In", b_Inmuebles_Contrato.incp),
                new MySqlParameter("base_revision_otro_In", b_Inmuebles_Contrato.base_revision_otro ?? ""),

                new MySqlParameter("LocalesAgrupados_In", b_Inmuebles_Contrato.LocalesAgrupados),
                new MySqlParameter("PropTer_In", b_Inmuebles_Contrato.PropTer ?? "P"),
                new MySqlParameter("DepRta_In", b_Inmuebles_Contrato.DepRta),
                new MySqlParameter("Pagare_In", b_Inmuebles_Contrato.Pagare ?? ""),
                new MySqlParameter("DiaPago_In", b_Inmuebles_Contrato.DiaPago),
                new MySqlParameter("categoria_In", b_Inmuebles_Contrato.categoria),
                new MySqlParameter("ObservacionDepGarantia_In", b_Inmuebles_Contrato.ObservacionDepGarantia ?? ""),
                new MySqlParameter("NumeroReferenciado_In", b_Inmuebles_Contrato.NumeroReferenciado ?? ""),
                new MySqlParameter("NumeroReferenciadoMtto_In", b_Inmuebles_Contrato.NumeroReferenciadoMtto ?? ""),
                new MySqlParameter("fechaOcupacion_In", b_Inmuebles_Contrato.fechaOcupacion),
                new MySqlParameter("plazoPFAnio_In", b_Inmuebles_Contrato.plazoPFAnio),
                new MySqlParameter("plazoPFMes_In", b_Inmuebles_Contrato.plazoPFMes),
                new MySqlParameter("plazoPFdia_In", b_Inmuebles_Contrato.plazoPFdia),
                new MySqlParameter("plazoIFAnio_In", b_Inmuebles_Contrato.plazoIFAnio),
                new MySqlParameter("plazoIFMes_In", b_Inmuebles_Contrato.plazoIFMes),
                new MySqlParameter("plazoIFDia_In", b_Inmuebles_Contrato.plazoIFDia),
                new MySqlParameter("plazoPVAnio_In", b_Inmuebles_Contrato.plazoPVAnio),
                new MySqlParameter("plazoPVMes_In", b_Inmuebles_Contrato.plazoPVMes),
                new MySqlParameter("plazoPVDia_In", b_Inmuebles_Contrato.plazoPVDia),
                new MySqlParameter("plazoIVAnio_In", b_Inmuebles_Contrato.plazoIVAnio),
                new MySqlParameter("plazoIVMes_In", b_Inmuebles_Contrato.plazoIVMes),
                new MySqlParameter("plazoIVDia_In", b_Inmuebles_Contrato.plazoIVDia),
                new MySqlParameter("revisionPorcentajeAdicional_In", b_Inmuebles_Contrato.revisionPorcentajeAdicional),
                new MySqlParameter("ComentariosRevision_In", b_Inmuebles_Contrato.ComentariosRevision ?? "")
            };

            conexion.RunStoredProcedure("b_inmuebles_ContratosUpdate", listSqlParameters);

        }

        public void Insert(B_inmuebles_contrato b_Inmuebles_Contrato)
        {

            List<MySqlParameter> listSqlParameters = new()
            {
                new MySqlParameter("id_b_inmuebles_In", b_Inmuebles_Contrato.id_b_inmuebles),
                new MySqlParameter("id_b_cg_contrato_tipo_In", b_Inmuebles_Contrato.id_b_cg_contrato_tipo),
                new MySqlParameter("id_b_cg_contrato_estatu_In", b_Inmuebles_Contrato.id_b_cg_contrato_estatu),
                new MySqlParameter("contrato_In", b_Inmuebles_Contrato.contrato ?? ""),
                new MySqlParameter("renta_In", b_Inmuebles_Contrato.renta),
                new MySqlParameter("fecha_inicio_In", b_Inmuebles_Contrato.fecha_inicio),
                new MySqlParameter("fecha_termino_In", b_Inmuebles_Contrato.fecha_termino),
                new MySqlParameter("fecha_anticipacion_In", b_Inmuebles_Contrato.fecha_anticipacion),
                new MySqlParameter("fecha_obligado_In", b_Inmuebles_Contrato.fecha_obligado),
                new MySqlParameter("litigio_In", b_Inmuebles_Contrato.litigio),
                new MySqlParameter("fecha_revision_In", b_Inmuebles_Contrato.fecha_revision),
                new MySqlParameter("plazo_In", b_Inmuebles_Contrato.plazo ?? ""),
                new MySqlParameter("base_revision_In", b_Inmuebles_Contrato.base_revision),
                new MySqlParameter("especial_In", b_Inmuebles_Contrato.especial),
                new MySqlParameter("correo_especial_In", b_Inmuebles_Contrato.correo_especial ?? ""),
                new MySqlParameter("observaciones_In", b_Inmuebles_Contrato.observaciones ?? ""),
                new MySqlParameter("incp_In", b_Inmuebles_Contrato.incp),
                new MySqlParameter("base_revision_otro_In", b_Inmuebles_Contrato.base_revision_otro ?? ""),

                new MySqlParameter("LocalesAgrupados_In", b_Inmuebles_Contrato.LocalesAgrupados),
                new MySqlParameter("PropTer_In", b_Inmuebles_Contrato.PropTer ?? "P"),
                new MySqlParameter("DepRta_In", b_Inmuebles_Contrato.DepRta),
                new MySqlParameter("Pagare_In", b_Inmuebles_Contrato.Pagare ?? ""),
                new MySqlParameter("DiaPago_In", b_Inmuebles_Contrato.DiaPago),
                new MySqlParameter("categoria_In", b_Inmuebles_Contrato.categoria),
                new MySqlParameter("ObservacionDepGarantia_In", b_Inmuebles_Contrato.ObservacionDepGarantia ?? ""),
                new MySqlParameter("NumeroReferenciado_In", b_Inmuebles_Contrato.NumeroReferenciado ?? ""),
                new MySqlParameter("NumeroReferenciadoMtto_In", b_Inmuebles_Contrato.NumeroReferenciadoMtto ?? ""),
                new MySqlParameter("fechaOcupacion_In", b_Inmuebles_Contrato.fechaOcupacion),
                new MySqlParameter("plazoPFAnio_In", b_Inmuebles_Contrato.plazoPFAnio),
                new MySqlParameter("plazoPFMes_In", b_Inmuebles_Contrato.plazoPFMes),
                new MySqlParameter("plazoPFdia_In", b_Inmuebles_Contrato.plazoPFdia),
                new MySqlParameter("plazoIFAnio_In", b_Inmuebles_Contrato.plazoIFAnio),
                new MySqlParameter("plazoIFMes_In", b_Inmuebles_Contrato.plazoIFMes),
                new MySqlParameter("plazoIFDia_In", b_Inmuebles_Contrato.plazoIFDia),
                new MySqlParameter("plazoPVAnio_In", b_Inmuebles_Contrato.plazoPVAnio),
                new MySqlParameter("plazoPVMes_In", b_Inmuebles_Contrato.plazoPVMes),
                new MySqlParameter("plazoPVDia_In", b_Inmuebles_Contrato.plazoPVDia),
                new MySqlParameter("plazoIVAnio_In", b_Inmuebles_Contrato.plazoIVAnio),
                new MySqlParameter("plazoIVMes_In", b_Inmuebles_Contrato.plazoIVMes),
                new MySqlParameter("plazoIVDia_In", b_Inmuebles_Contrato.plazoIVDia),
                new MySqlParameter("revisionPorcentajeAdicional_In", b_Inmuebles_Contrato.revisionPorcentajeAdicional),
                new MySqlParameter("ComentariosRevision_In", b_Inmuebles_Contrato.ComentariosRevision ?? "")
            };


            conexion.RunStoredProcedure("b_inmuebles_ContratosInsert", listSqlParameters);

        }

        private List<B_inmuebles_contrato> DataToModel(DataTable dataTable)
        {
            List<B_inmuebles_contrato> b_Inmuebles_ContratosList = new List<B_inmuebles_contrato>();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    B_inmuebles_contrato b_Inmuebles_Contrato = new B_inmuebles_contrato();

                    b_Inmuebles_Contrato.id_b_inmuebles_contrato = int.Parse(item["id_b_inmuebles_contrato"].ToString());
                    b_Inmuebles_Contrato.id_b_inmuebles = int.Parse(item["id_b_inmuebles"].ToString());
                    b_Inmuebles_Contrato.id_b_cg_contrato_tipo = int.Parse(item["id_b_cg_contrato_tipo"].ToString());
                    b_Inmuebles_Contrato.id_b_cg_contrato_estatu = int.Parse(item["id_b_cg_contrato_estatu"].ToString());
                    b_Inmuebles_Contrato.contrato = item["contrato"].ToString();
                    b_Inmuebles_Contrato.nombre = item["nombre"].ToString();
                    try
                    {
                        b_Inmuebles_Contrato.email = item["email"].ToString();
                    }
                    catch (Exception)
                    {
                        b_Inmuebles_Contrato.email = "";
                    }                    
                    b_Inmuebles_Contrato.renta = decimal.Parse(item["renta"].ToString());
                    b_Inmuebles_Contrato.fecha_inicio = Convert.ToDateTime(item["fecha_inicio"].ToString());
                    b_Inmuebles_Contrato.fecha_termino = Convert.ToDateTime(item["fecha_termino"].ToString());
                    b_Inmuebles_Contrato.fecha_anticipacion = int.Parse(item["fecha_anticipacion"].ToString());
                    b_Inmuebles_Contrato.fecha_obligado = Convert.ToDateTime(item["fecha_obligado"].ToString());
                    b_Inmuebles_Contrato.litigio = Convert.ToBoolean(Convert.ToInt16(item["litigio"].ToString()));
                    b_Inmuebles_Contrato.fecha_revision = Convert.ToDateTime(item["fecha_revision"].ToString());
                    b_Inmuebles_Contrato.plazo = item["plazo"].ToString();
                    b_Inmuebles_Contrato.base_revision = item["base_revision"].ToString();
                    b_Inmuebles_Contrato.especial = Convert.ToBoolean(Convert.ToInt16(item["especial"].ToString()));
                    b_Inmuebles_Contrato.correo_especial = item["correo_especial"].ToString();
                    b_Inmuebles_Contrato.observaciones = item["observaciones"].ToString();
                    b_Inmuebles_Contrato.fecha_envio = Convert.ToDateTime(item["fecha_envio"].ToString());
                    b_Inmuebles_Contrato.incp = decimal.Parse(item["incp"].ToString());
                    b_Inmuebles_Contrato.base_revision_otro = item["base_revision_otro"].ToString();

                    b_Inmuebles_Contrato.LocalesAgrupados = int.Parse(item["LocalesAgrupados"].ToString());
                    b_Inmuebles_Contrato.PropTer = item["PropTer"].ToString();
                    b_Inmuebles_Contrato.DepRta = double.Parse(item["DepRta"].ToString());
                    b_Inmuebles_Contrato.Pagare = item["Pagare"] != DBNull.Value ? item["Pagare"].ToString() : "";
                    b_Inmuebles_Contrato.DiaPago = item["DiaPago"].ToString() != string.Empty ? int.Parse(item["DiaPago"].ToString()) : 0;
                    b_Inmuebles_Contrato.categoria = int.Parse(item["categoria"].ToString());
                    b_Inmuebles_Contrato.ObservacionDepGarantia = item["ObservacionDepGarantia"].ToString();
                    b_Inmuebles_Contrato.NumeroReferenciado = item["NumeroReferenciado"].ToString();
                    b_Inmuebles_Contrato.NumeroReferenciadoMtto = item["NumeroReferenciadoMtto"].ToString();
                    b_Inmuebles_Contrato.fechaOcupacion =  item["fechaOcupacion"] != DBNull.Value ? Convert.ToDateTime(item["fechaOcupacion"].ToString()) : Convert.ToDateTime("1900-01-01");
                    b_Inmuebles_Contrato.plazoPFAnio = int.Parse(item["plazoPFAnio"].ToString());
                    b_Inmuebles_Contrato.plazoPFMes = int.Parse(item["plazoPFMes"].ToString());
                    b_Inmuebles_Contrato.plazoPFdia = int.Parse(item["plazoPFdia"].ToString());
                    b_Inmuebles_Contrato.plazoIFAnio = int.Parse(item["plazoIFAnio"].ToString());
                    b_Inmuebles_Contrato.plazoIFMes = int.Parse(item["plazoIFMes"].ToString());
                    b_Inmuebles_Contrato.plazoIFDia = int.Parse(item["plazoIFDia"].ToString());
                    b_Inmuebles_Contrato.plazoPVAnio = int.Parse(item["plazoPVAnio"].ToString());
                    b_Inmuebles_Contrato.plazoPVMes = int.Parse(item["plazoPVMes"].ToString());
                    b_Inmuebles_Contrato.plazoPVDia = int.Parse(item["plazoPVDia"].ToString());
                    b_Inmuebles_Contrato.plazoIVAnio = int.Parse(item["plazoIVAnio"].ToString());
                    b_Inmuebles_Contrato.plazoIVMes = int.Parse(item["plazoIVMes"].ToString());
                    b_Inmuebles_Contrato.plazoIVDia = int.Parse(item["plazoIVDia"].ToString());
                    b_Inmuebles_Contrato.revisionPorcentajeAdicional = double.Parse(item["revisionPorcentajeAdicional"].ToString());
                    b_Inmuebles_Contrato.ComentariosRevision = item["ComentariosRevision"].ToString();

                    try
                    {
                        b_Inmuebles_Contrato.tipo = item["tipo"].ToString();
                        b_Inmuebles_Contrato.estatus = item["estatus"].ToString();                        
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {                        
                        b_Inmuebles_Contrato.cr = item["cr"].ToString();
                        b_Inmuebles_Contrato.ue = item["ue"].ToString();
                    }
                    catch (Exception)
                    {

                    }
                    b_Inmuebles_ContratosList.Add(b_Inmuebles_Contrato);
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
