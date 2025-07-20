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
    public class DataInmuebles
    {
        private Conexion conexion = new Conexion();

        //borrar
        public List<B_inmuebles> GetAll(int IdCartera, int IdUsuario)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            DataTable dataTable = conexion.RunStoredProcedure("b_inmueblesGetAll", listSqlParameters);

            return DataToModel(dataTable);
        }

        public List<B_inmuebles> Get(int IdCartera, int IdUsuario)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            DataTable dataTable = conexion.RunStoredProcedure("b_inmueblesGet", listSqlParameters);

            return DataToModel(dataTable);
        }

        public B_inmuebles Get(int IdCartera, int IdUsuario, int id_b_inmuebles)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("id_b_inmueblesIn", id_b_inmuebles));
            DataTable dataTable = conexion.RunStoredProcedure("b_inmueblesGetbyId", listSqlParameters);

            return DataToModel(dataTable).FirstOrDefault();
        }

        public List<B_inmuebles_reporte> GetReporte(int IdCartera, int IdUsuario, DateTime fecha, DateTime fecha2)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("fechaIn", fecha));
            listSqlParameters.Add(new MySqlParameter("fecha2In", fecha2));

            DataTable dataTable = conexion.RunStoredProcedure("b_inmueblesGetSinVisitar", listSqlParameters);

            return DataToModelFormatoReporte(dataTable);
        }

        public void Delete(int IdCartera, int id_b_inmuebles)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCarteraIn", IdCartera));
            listSqlParameters.Add(new MySqlParameter("id_b_inmueblesIn", id_b_inmuebles));
            DataTable dataTable = conexion.RunStoredProcedure("b_inmueblesDelete", listSqlParameters);

            return;
        }

        public void Edit(B_inmuebles b_Inmuebles)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("id_b_inmueblesIN", b_Inmuebles.id_b_inmuebles));
            listSqlParameters.Add(new MySqlParameter("crIN", b_Inmuebles.cr));
            listSqlParameters.Add(new MySqlParameter("ueIN", b_Inmuebles.ue));
            listSqlParameters.Add(new MySqlParameter("nombreIN", b_Inmuebles.nombre));
            listSqlParameters.Add(new MySqlParameter("direccionIN", b_Inmuebles.direccion));
            listSqlParameters.Add(new MySqlParameter("cpIN", b_Inmuebles.cp));
            listSqlParameters.Add(new MySqlParameter("municipioIN", b_Inmuebles.municipio));
            listSqlParameters.Add(new MySqlParameter("estadoIN", b_Inmuebles.estado));
            listSqlParameters.Add(new MySqlParameter("coloniaIN", b_Inmuebles.colonia));
            listSqlParameters.Add(new MySqlParameter("noextIN", b_Inmuebles.noext));
            listSqlParameters.Add(new MySqlParameter("manzanaIN", b_Inmuebles.manzana));
            listSqlParameters.Add(new MySqlParameter("loteIN", b_Inmuebles.lote));
            listSqlParameters.Add(new MySqlParameter("referencia_calleIN", b_Inmuebles.referencia_calle));
            listSqlParameters.Add(new MySqlParameter("referencia_calle2IN", b_Inmuebles.referencia_calle2));
            listSqlParameters.Add(new MySqlParameter("latidudIN", b_Inmuebles.latidud));
            listSqlParameters.Add(new MySqlParameter("longitudIN", b_Inmuebles.longitud));
            listSqlParameters.Add(new MySqlParameter("link_mapsIN", b_Inmuebles.link_maps));
            listSqlParameters.Add(new MySqlParameter("valor_comercialIN", b_Inmuebles.valor_comercial));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_regimenIN", b_Inmuebles.id_b_cg_regimen));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_inmuebles_estatusIN", b_Inmuebles.id_b_cg_inmuebles_estatus));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_clasificacionIN", b_Inmuebles.id_b_cg_clasificacion));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_regionIN", b_Inmuebles.id_b_cg_region));
            listSqlParameters.Add(new MySqlParameter("IDCARTERAIN", b_Inmuebles.IDCARTERA));
            listSqlParameters.Add(new MySqlParameter("m2_terrenoIN", b_Inmuebles.m2_terreno));
            listSqlParameters.Add(new MySqlParameter("area_rentableIN", b_Inmuebles.area_rentable));
            listSqlParameters.Add(new MySqlParameter("m2_construccionIN", b_Inmuebles.m2_construccion));
            listSqlParameters.Add(new MySqlParameter("m2_estacionamientoIN", b_Inmuebles.m2_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("m2_bodegaIN", b_Inmuebles.m2_bodega));
            listSqlParameters.Add(new MySqlParameter("m2_localIN", b_Inmuebles.m2_local));
            listSqlParameters.Add(new MySqlParameter("cajones_estacionamientoIN", b_Inmuebles.cajones_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("cajones_estacionamiento_ejecutivoIN", b_Inmuebles.cajones_estacionamiento_ejecutivo));
            listSqlParameters.Add(new MySqlParameter("cajones_pensionesIN", b_Inmuebles.cajones_pensiones));
            listSqlParameters.Add(new MySqlParameter("cortesias_boletosIN", b_Inmuebles.cortesias_boletos));
            listSqlParameters.Add(new MySqlParameter("operador_estacionamientoIN", b_Inmuebles.operador_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("cajeros_automaticosIN", b_Inmuebles.cajeros_automaticos));
            listSqlParameters.Add(new MySqlParameter("practicajasIN", b_Inmuebles.practicajas));
            listSqlParameters.Add(new MySqlParameter("ventanillasIN", b_Inmuebles.ventanillas));
            listSqlParameters.Add(new MySqlParameter("contrato_rentaIN", b_Inmuebles.contrato_renta));
            listSqlParameters.Add(new MySqlParameter("m2_precioIN", b_Inmuebles.m2_precio));
            listSqlParameters.Add(new MySqlParameter("m2_rango_infIN", b_Inmuebles.m2_rango_inf));
            listSqlParameters.Add(new MySqlParameter("m2_rango_supIN", b_Inmuebles.m2_rango_sup));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_valor_mercadoIN", b_Inmuebles.id_b_cg_valor_mercado));
            listSqlParameters.Add(new MySqlParameter("aguaIN", b_Inmuebles.agua));
            listSqlParameters.Add(new MySqlParameter("luzIN", b_Inmuebles.luz));
            listSqlParameters.Add(new MySqlParameter("predialIN", b_Inmuebles.predial));
            listSqlParameters.Add(new MySqlParameter("contratoIN", b_Inmuebles.contrato));
            listSqlParameters.Add(new MySqlParameter("rentaIN", b_Inmuebles.renta));
            listSqlParameters.Add(new MySqlParameter("fecha_inicioIN", b_Inmuebles.fecha_inicio));
            listSqlParameters.Add(new MySqlParameter("fecha_terminoIN", b_Inmuebles.fecha_termino));
            listSqlParameters.Add(new MySqlParameter("fecha_obligadoIN", b_Inmuebles.fecha_obligado));
            listSqlParameters.Add(new MySqlParameter("ingresoIN", b_Inmuebles.ingreso));

            listSqlParameters.Add(new MySqlParameter("nointIN", b_Inmuebles.noint));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_subclasificacionIN", b_Inmuebles.id_b_cg_subclasificacion));
            listSqlParameters.Add(new MySqlParameter("opera_estacionamientoIN", b_Inmuebles.opera_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("valet_parkingIN", b_Inmuebles.valet_parking));
            
            listSqlParameters.Add(new MySqlParameter("IdPropietario_In", b_Inmuebles.IdPropietario));

            conexion.RunStoredProcedure("b_inmueblesEdit", listSqlParameters);

        }

        public void Insert(B_inmuebles b_Inmuebles, int IdUsuario)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            listSqlParameters.Add(new MySqlParameter("crIN", b_Inmuebles.cr));
            listSqlParameters.Add(new MySqlParameter("ueIN", b_Inmuebles.ue));
            listSqlParameters.Add(new MySqlParameter("nombreIN", b_Inmuebles.nombre));
            listSqlParameters.Add(new MySqlParameter("direccionIN", b_Inmuebles.direccion));
            listSqlParameters.Add(new MySqlParameter("cpIN", b_Inmuebles.cp));
            listSqlParameters.Add(new MySqlParameter("municipioIN", b_Inmuebles.municipio));
            listSqlParameters.Add(new MySqlParameter("coloniaIN", b_Inmuebles.colonia));
            listSqlParameters.Add(new MySqlParameter("noextIN", b_Inmuebles.noext));
            listSqlParameters.Add(new MySqlParameter("manzanaIN", b_Inmuebles.manzana));
            listSqlParameters.Add(new MySqlParameter("loteIN", b_Inmuebles.lote));
            listSqlParameters.Add(new MySqlParameter("referencia_calleIN", b_Inmuebles.referencia_calle));
            listSqlParameters.Add(new MySqlParameter("referencia_calle2IN", b_Inmuebles.referencia_calle2));
            listSqlParameters.Add(new MySqlParameter("estadoIN", b_Inmuebles.estado));
            listSqlParameters.Add(new MySqlParameter("latidudIN", b_Inmuebles.latidud));
            listSqlParameters.Add(new MySqlParameter("longitudIN", b_Inmuebles.longitud));
            listSqlParameters.Add(new MySqlParameter("link_mapsIN", b_Inmuebles.link_maps));
            listSqlParameters.Add(new MySqlParameter("valor_comercialIN", b_Inmuebles.valor_comercial));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_regimenIN", b_Inmuebles.id_b_cg_regimen));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_inmuebles_estatusIN", b_Inmuebles.id_b_cg_inmuebles_estatus));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_clasificacionIN", b_Inmuebles.id_b_cg_clasificacion));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_regionIN", b_Inmuebles.id_b_cg_region));
            listSqlParameters.Add(new MySqlParameter("IDCARTERAIN", b_Inmuebles.IDCARTERA));
            listSqlParameters.Add(new MySqlParameter("m2_terrenoIN", b_Inmuebles.m2_terreno));
            listSqlParameters.Add(new MySqlParameter("area_rentableIN", b_Inmuebles.area_rentable));
            listSqlParameters.Add(new MySqlParameter("m2_construccionIN", b_Inmuebles.m2_construccion));
            listSqlParameters.Add(new MySqlParameter("m2_estacionamientoIN", b_Inmuebles.m2_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("m2_bodegaIN", b_Inmuebles.m2_bodega));
            listSqlParameters.Add(new MySqlParameter("m2_localIN", b_Inmuebles.m2_local));
            listSqlParameters.Add(new MySqlParameter("cajones_estacionamientoIN", b_Inmuebles.cajones_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("cajones_estacionamiento_ejecutivoIN", b_Inmuebles.cajones_estacionamiento_ejecutivo));
            listSqlParameters.Add(new MySqlParameter("cajones_pensionesIN", b_Inmuebles.cajones_pensiones));
            listSqlParameters.Add(new MySqlParameter("cortesias_boletosIN", b_Inmuebles.cortesias_boletos));
            listSqlParameters.Add(new MySqlParameter("operador_estacionamientoIN", b_Inmuebles.operador_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("cajeros_automaticosIN", b_Inmuebles.cajeros_automaticos));
            listSqlParameters.Add(new MySqlParameter("practicajasIN", b_Inmuebles.practicajas));
            listSqlParameters.Add(new MySqlParameter("ventanillasIN", b_Inmuebles.ventanillas));
            listSqlParameters.Add(new MySqlParameter("contrato_rentaIN", b_Inmuebles.contrato_renta));
            listSqlParameters.Add(new MySqlParameter("m2_precioIN", b_Inmuebles.m2_precio));
            listSqlParameters.Add(new MySqlParameter("m2_rango_infIN", b_Inmuebles.m2_rango_inf));
            listSqlParameters.Add(new MySqlParameter("m2_rango_supIN", b_Inmuebles.m2_rango_sup));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_valor_mercadoIN", b_Inmuebles.id_b_cg_valor_mercado));
            listSqlParameters.Add(new MySqlParameter("aguaIN", b_Inmuebles.agua));
            listSqlParameters.Add(new MySqlParameter("luzIN", b_Inmuebles.luz));
            listSqlParameters.Add(new MySqlParameter("predialIN", b_Inmuebles.predial));
            listSqlParameters.Add(new MySqlParameter("contratoIN", b_Inmuebles.contrato));
            listSqlParameters.Add(new MySqlParameter("rentaIN", b_Inmuebles.renta));
            listSqlParameters.Add(new MySqlParameter("fecha_inicioIN", b_Inmuebles.fecha_inicio));
            listSqlParameters.Add(new MySqlParameter("fecha_terminoIN", b_Inmuebles.fecha_termino));
            listSqlParameters.Add(new MySqlParameter("fecha_obligadoIN", b_Inmuebles.fecha_obligado));
            listSqlParameters.Add(new MySqlParameter("ingresoIN", b_Inmuebles.ingreso));

            listSqlParameters.Add(new MySqlParameter("nointIN", b_Inmuebles.noint));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_subclasificacionIN", b_Inmuebles.id_b_cg_subclasificacion));
            listSqlParameters.Add(new MySqlParameter("opera_estacionamientoIN", b_Inmuebles.opera_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("valet_parkingIN", b_Inmuebles.valet_parking));
            
            listSqlParameters.Add(new MySqlParameter("IdPropietario_In", b_Inmuebles.IdPropietario));

            List<MySqlParameter> listSqlParametersOUT = new List<MySqlParameter>();
            listSqlParametersOUT.Add(new MySqlParameter("idOUT", b_Inmuebles.id_b_inmuebles));

            MySqlParameterCollection mySqlParameterCollection = conexion.RunStoredProcedure("b_inmueblesInsert", listSqlParameters, listSqlParametersOUT);
            int id = (int)mySqlParameterCollection["idOUT"].Value;

            List<MySqlParameter> listSqlParameters2 = new List<MySqlParameter>();
            listSqlParameters2.Add(new MySqlParameter("idInmueble_In", id));
            listSqlParameters2.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            DataTable dataTable = conexion.RunStoredProcedure("DtInmuebleUsuarioInsert", listSqlParameters2);

        }

        private List<B_inmuebles> DataToModel(DataTable dataTable)
        {
            List<B_inmuebles> dataB_InmueblesList = new List<B_inmuebles>();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    B_inmuebles dataB_Inmuebles = new B_inmuebles();

                    dataB_Inmuebles.id_b_inmuebles = int.Parse(item["id_b_inmuebles"].ToString());
                    dataB_Inmuebles.ue = int.Parse(item["ue"].ToString());
                    dataB_Inmuebles.cr = item["cr"].ToString();
                    dataB_Inmuebles.nombre = item["nombre"].ToString();
                    dataB_Inmuebles.direccion = item["direccion"].ToString();
                    dataB_Inmuebles.cp = int.Parse(item["cp"].ToString());
                    dataB_Inmuebles.estado = item["estado"].ToString();
                    dataB_Inmuebles.municipio = item["municipio"].ToString();
                    dataB_Inmuebles.colonia = item["colonia"].ToString();
                    dataB_Inmuebles.noext = item["noext"].ToString();
                    dataB_Inmuebles.manzana = item["manzana"].ToString();
                    dataB_Inmuebles.lote = item["lote"].ToString();
                    dataB_Inmuebles.referencia_calle = item["referencia_calle"].ToString();
                    dataB_Inmuebles.referencia_calle2 = item["referencia_calle2"].ToString();
                    dataB_Inmuebles.latidud = double.Parse(item["latidud"].ToString());
                    dataB_Inmuebles.longitud = double.Parse(item["longitud"].ToString());
                    dataB_Inmuebles.link_maps = item["link_maps"].ToString();
                    dataB_Inmuebles.valor_comercial = decimal.Parse(item["valor_comercial"].ToString());
                    dataB_Inmuebles.id_b_cg_regimen = int.Parse(item["id_b_cg_regimen"].ToString());
                    dataB_Inmuebles.id_b_cg_inmuebles_estatus = int.Parse(item["id_b_cg_inmuebles_estatus"].ToString());
                    dataB_Inmuebles.id_b_cg_clasificacion = int.Parse(item["id_b_cg_clasificacion"].ToString());
                    dataB_Inmuebles.id_b_cg_region = int.Parse(item["id_b_cg_region"].ToString());
                    dataB_Inmuebles.IDCARTERA = int.Parse(item["IDCARTERA"].ToString());
                    dataB_Inmuebles.m2_terreno = decimal.Parse(item["m2_terreno"].ToString());
                    dataB_Inmuebles.area_rentable = decimal.Parse(item["area_rentable"].ToString());
                    dataB_Inmuebles.m2_construccion = decimal.Parse(item["m2_construccion"].ToString());
                    dataB_Inmuebles.m2_estacionamiento = decimal.Parse(item["m2_estacionamiento"].ToString());
                    dataB_Inmuebles.m2_bodega = decimal.Parse(item["m2_bodega"].ToString());
                    dataB_Inmuebles.m2_local = decimal.Parse(item["m2_local"].ToString());
                    dataB_Inmuebles.cajones_estacionamiento = int.Parse(item["cajones_estacionamiento"].ToString());
                    dataB_Inmuebles.cajones_estacionamiento_ejecutivo = int.Parse(item["cajones_estacionamiento_ejecutivo"].ToString());
                    dataB_Inmuebles.cajones_pensiones = int.Parse(item["cajones_pensiones"].ToString());
                    dataB_Inmuebles.cortesias_boletos = int.Parse(item["cortesias_boletos"].ToString());
                    dataB_Inmuebles.operador_estacionamiento = item["operador_estacionamiento"].ToString();
                    dataB_Inmuebles.cajeros_automaticos = int.Parse(item["cajeros_automaticos"].ToString());
                    dataB_Inmuebles.practicajas = int.Parse(item["practicajas"].ToString());
                    dataB_Inmuebles.ventanillas = int.Parse(item["ventanillas"].ToString());
                    dataB_Inmuebles.contrato_renta = decimal.Parse(item["contrato_renta"].ToString());
                    dataB_Inmuebles.m2_precio = decimal.Parse(item["m2_precio"].ToString());
                    dataB_Inmuebles.m2_rango_inf = decimal.Parse(item["m2_rango_inf"].ToString());
                    dataB_Inmuebles.m2_rango_sup = decimal.Parse(item["m2_rango_sup"].ToString());
                    dataB_Inmuebles.id_b_cg_valor_mercado = int.Parse(item["id_b_cg_valor_mercado"].ToString());
                    dataB_Inmuebles.agua = decimal.Parse(item["agua"].ToString());
                    dataB_Inmuebles.luz = decimal.Parse(item["luz"].ToString());
                    dataB_Inmuebles.predial = decimal.Parse(item["predial"].ToString());

                    dataB_Inmuebles.contrato = item["contrato"].ToString();
                    dataB_Inmuebles.renta = decimal.Parse(item["renta"].ToString());
                    dataB_Inmuebles.fecha_inicio = Convert.ToDateTime(item["fecha_inicio"].ToString());
                    dataB_Inmuebles.fecha_termino = Convert.ToDateTime(item["fecha_termino"].ToString());
                    dataB_Inmuebles.fecha_obligado = Convert.ToDateTime(item["fecha_obligado"].ToString());
                    dataB_Inmuebles.ingreso = int.Parse(item["ingreso"].ToString());

                    dataB_Inmuebles.noint = item["noint"].ToString();
                    dataB_Inmuebles.id_b_cg_subclasificacion = int.Parse(item["id_b_cg_subclasificacion"].ToString());
                    dataB_Inmuebles.opera_estacionamiento = Convert.ToBoolean(Convert.ToInt16(item["opera_estacionamiento"].ToString()));
                    dataB_Inmuebles.valet_parking = Convert.ToBoolean(Convert.ToInt16(item["valet_parking"].ToString()));
                    dataB_Inmuebles.IdPropietario = int.Parse(item["IdPropietario"].ToString());


                    //Aux
                    dataB_Inmuebles.clasificacion = item["clasificacion"].ToString();
                    dataB_Inmuebles.regimen = item["regimen"].ToString();

                    dataB_InmueblesList.Add(dataB_Inmuebles);
                }
                catch (Exception e)
                {
                    throw;
                }



            }
            return dataB_InmueblesList;
        }

        private List<B_inmuebles_reporte> DataToModelFormatoReporte(DataTable dataTable)
        {
            List<B_inmuebles_reporte> dataB_InmueblesList = new List<B_inmuebles_reporte>();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    B_inmuebles_reporte dataB_Inmuebles = new B_inmuebles_reporte();

                    dataB_Inmuebles.ue = "" + item["ue"].ToString();
                    dataB_Inmuebles.cr = "" + item["cr"].ToString();
                    dataB_Inmuebles.nombre = "" + item["nombre"].ToString();
                    dataB_Inmuebles.region = "" + item["region"].ToString();
                    dataB_Inmuebles.inmuebles_estatus = "" + item["inmuebles_estatus"].ToString();
                    dataB_Inmuebles.direccion = "" + item["direccion"].ToString();
                    dataB_Inmuebles.latidud = double.Parse(item["latidud"].ToString());
                    dataB_Inmuebles.longitud = double.Parse(item["longitud"].ToString());
                    dataB_Inmuebles.link_maps = "" + item["link_maps"].ToString();

                    dataB_InmueblesList.Add(dataB_Inmuebles);
                }
                catch (Exception e)
                {
                    throw;
                }



            }
            return dataB_InmueblesList;
        }

        public List<SelectListItem> GetListaPaises()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable dataTable = conexion.RunStoredProcedure("countryGet", mySqlParameters);
                List<SelectListItem> lstPaises = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstPaises.Add(new SelectListItem { Text = item["Name"].ToString(), Value = item["Code"].ToString() });
                }

                return lstPaises;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> GetListaEstados()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable dataTable = conexion.RunStoredProcedure("cityGet", mySqlParameters);
                List<SelectListItem> lstEstados = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstEstados.Add(new SelectListItem { Text = item["District"].ToString(), Value = item["District"].ToString() });
                }

                return lstEstados;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> GetCmbInmuebles(int IdCartera, int IdUsuario)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new()
                {
                    new MySqlParameter("IdCartera_In", IdCartera),
                    new MySqlParameter("IdUsuario_In", IdUsuario)
                };                
                DataTable tb = conexion.RunStoredProcedure("b_inmueblesGetCmb", listSqlParameters);
                List<SelectListItem> lstPropietarios = new();
                foreach (DataRow item in tb.Rows)
                {
                    lstPropietarios.Add(new SelectListItem { Text = item["Inmueble"].ToString(), Value = item["IdInmueble"].ToString() });
                }
                return lstPropietarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<SelectListItem> GetListaPropietarios()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                MySqlParameter mySqlParameter1 = new MySqlParameter("V_OPCION", MySqlDbType.Int32) { Value = 16 };
                MySqlParameter mySqlParameter2 = new MySqlParameter("V_TIPO_PROPIEDAD", MySqlDbType.VarChar) { Value = "R" };
                MySqlParameter mySqlParameter3 = new MySqlParameter("V_IDPROPIETARIO", MySqlDbType.Int32) { Value = 0 };
                MySqlParameter mySqlParameter4 = new MySqlParameter("V_CLAPRO", MySqlDbType.Int32) { Value = 0 };
                MySqlParameter mySqlParameter5 = new MySqlParameter("V_NOMBRE", MySqlDbType.VarChar) { Value = "" };
                MySqlParameter mySqlParameter6 = new MySqlParameter("V_CARTERA", MySqlDbType.Int32) { Value = 1 };
                MySqlParameter mySqlParameter7 = new MySqlParameter("V_IDUSUARIO", MySqlDbType.Int32) { Value = 0 };
                mySqlParameters.Add(mySqlParameter1);
                mySqlParameters.Add(mySqlParameter2);
                mySqlParameters.Add(mySqlParameter3);
                mySqlParameters.Add(mySqlParameter4);
                mySqlParameters.Add(mySqlParameter5);
                mySqlParameters.Add(mySqlParameter6);
                mySqlParameters.Add(mySqlParameter7);
                DataTable tb = conexion.RunStoredProcedure("propietariosGet", mySqlParameters);
                List<SelectListItem> lstPropietarios = new List<SelectListItem>();
                foreach (DataRow item in tb.Rows)
                {
                    lstPropietarios.Add(new SelectListItem { Text = item["Propietario"].ToString(), Value = item["IdPropietario"].ToString() });
                }
                return lstPropietarios;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
