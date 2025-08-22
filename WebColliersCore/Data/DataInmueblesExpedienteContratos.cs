using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;
using System.Linq;
using WebColliersCore.Data;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace WebLomelinCore.Data
{
    public class DataInmueblesExpedienteContratos
    {
        private Conexion conexion = new Conexion();

        public List<B_cg_tipo_expediente_contratos> Get()
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            DataTable dataTable = conexion.RunStoredProcedure("b_cg_tipo_expediente_contratos_get", listSqlParameters);
            return DataToModel(dataTable);
        }


        //public void Edit(B_inmuebles_visitas b_inmuebles_visitas)
        //{

        //    List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
        //    listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_visitaIn", b_inmuebles_visitas.id_b_inmuebles_visita));
        //    listSqlParameters.Add(new MySqlParameter("id_b_inmueblesIn", b_inmuebles_visitas.id_b_inmuebles));
        //    listSqlParameters.Add(new MySqlParameter("estatus_visitaIn", b_inmuebles_visitas.estatus_visita));
        //    listSqlParameters.Add(new MySqlParameter("fecha_visitaIn", b_inmuebles_visitas.fecha_visita));
        //    listSqlParameters.Add(new MySqlParameter("mapa_seguridadIn", b_inmuebles_visitas.mapa_seguridad));
        //    listSqlParameters.Add(new MySqlParameter("inclusionIn", b_inmuebles_visitas.inclusion));
        //    listSqlParameters.Add(new MySqlParameter("no_anunciosIn", b_inmuebles_visitas.no_anuncios));
        //    listSqlParameters.Add(new MySqlParameter("marquesinaIn", b_inmuebles_visitas.marquesina));
        //    listSqlParameters.Add(new MySqlParameter("letras_rotuladasIn", b_inmuebles_visitas.letras_rotuladas));
        //    listSqlParameters.Add(new MySqlParameter("banderaIn", b_inmuebles_visitas.bandera));
        //    listSqlParameters.Add(new MySqlParameter("paletaIn", b_inmuebles_visitas.paleta));
        //    listSqlParameters.Add(new MySqlParameter("unipolarIn", b_inmuebles_visitas.unipolar));
        //    listSqlParameters.Add(new MySqlParameter("totemIn", b_inmuebles_visitas.totem));
        //    listSqlParameters.Add(new MySqlParameter("contrato_rentaIn", b_inmuebles_visitas.contrato_renta));
        //    listSqlParameters.Add(new MySqlParameter("m2_sucursal_precioIn", b_inmuebles_visitas.m2_sucursal_precio));
        //    listSqlParameters.Add(new MySqlParameter("m2_totalesIn", b_inmuebles_visitas.m2_totales));
        //    listSqlParameters.Add(new MySqlParameter("no_comparablesIn", b_inmuebles_visitas.no_comparables));
        //    listSqlParameters.Add(new MySqlParameter("valor_mercadoIn", b_inmuebles_visitas.valor_mercado));
        //    listSqlParameters.Add(new MySqlParameter("m2_rango_infIn", b_inmuebles_visitas.m2_rango_inf));
        //    listSqlParameters.Add(new MySqlParameter("m2_rango_supIn", b_inmuebles_visitas.m2_rango_sup));
        //    listSqlParameters.Add(new MySqlParameter("m2_rango_inf2In", b_inmuebles_visitas.m2_rango_inf2));
        //    listSqlParameters.Add(new MySqlParameter("m2_rango_sup2In", b_inmuebles_visitas.m2_rango_sup2));
        //    listSqlParameters.Add(new MySqlParameter("m2_rango_inf3In", b_inmuebles_visitas.m2_rango_inf3));
        //    listSqlParameters.Add(new MySqlParameter("m2_rango_sup3In", b_inmuebles_visitas.m2_rango_sup3));
        //    listSqlParameters.Add(new MySqlParameter("id_b_cg_valor_mercadoIn", b_inmuebles_visitas.id_b_cg_valor_mercado));
        //    listSqlParameters.Add(new MySqlParameter("id_b_cg_conservacionIn", b_inmuebles_visitas.id_b_cg_conservacion));
        //    listSqlParameters.Add(new MySqlParameter("aire_acondicionadoIn", b_inmuebles_visitas.aire_acondicionado));
        //    listSqlParameters.Add(new MySqlParameter("planta_emergenciaIn", b_inmuebles_visitas.planta_emergencia));
        //    listSqlParameters.Add(new MySqlParameter("impermeabilizacionIn", b_inmuebles_visitas.impermeabilizacion));
        //    listSqlParameters.Add(new MySqlParameter("medidorIn", b_inmuebles_visitas.medidor));
        //    listSqlParameters.Add(new MySqlParameter("medidor_direccionIn", b_inmuebles_visitas.medidor_direccion));
        //    listSqlParameters.Add(new MySqlParameter("agua_tomaIn", b_inmuebles_visitas.agua_toma));
        //    listSqlParameters.Add(new MySqlParameter("agua_toma_direccionIn", b_inmuebles_visitas.agua_toma_direccion));
        //    listSqlParameters.Add(new MySqlParameter("practicajasIn", b_inmuebles_visitas.practicajas));
        //    listSqlParameters.Add(new MySqlParameter("practicajas_servicioIn", b_inmuebles_visitas.practicajas_servicio));
        //    listSqlParameters.Add(new MySqlParameter("ventanillasIn", b_inmuebles_visitas.ventanillas));
        //    listSqlParameters.Add(new MySqlParameter("ventanillas_servicioIn", b_inmuebles_visitas.ventanillas_servicio));
        //    listSqlParameters.Add(new MySqlParameter("cajeros_automaticosIn", b_inmuebles_visitas.cajeros_automaticos));
        //    listSqlParameters.Add(new MySqlParameter("cajeros_automaticos_servicioIn", b_inmuebles_visitas.cajeros_automaticos_servicio));
        //    listSqlParameters.Add(new MySqlParameter("cajones_estacionamientoIn", b_inmuebles_visitas.cajones_estacionamiento));
        //    listSqlParameters.Add(new MySqlParameter("cajones_estacionamiento_servicioIn", b_inmuebles_visitas.cajones_estacionamiento_servicio));
        //    listSqlParameters.Add(new MySqlParameter("cajones_estacionamiento_ejecutivoIn", b_inmuebles_visitas.cajones_estacionamiento_ejecutivo));
        //    listSqlParameters.Add(new MySqlParameter("cajones_estacionamiento_ejecutivo_servicioIn", b_inmuebles_visitas.cajones_estacionamiento_ejecutivo_servicio));
        //    listSqlParameters.Add(new MySqlParameter("operador_estacionamientoIn", b_inmuebles_visitas.operador_estacionamiento ?? string.Empty));
        //    listSqlParameters.Add(new MySqlParameter("libreIn", b_inmuebles_visitas.libre));
        //    listSqlParameters.Add(new MySqlParameter("cortesiasIn", b_inmuebles_visitas.cortesias));
        //    listSqlParameters.Add(new MySqlParameter("concesionariaIn", b_inmuebles_visitas.concesionaria));
        //    listSqlParameters.Add(new MySqlParameter("valet_parkingIn", b_inmuebles_visitas.valet_parking));
        //    listSqlParameters.Add(new MySqlParameter("personal_exteriorIn", b_inmuebles_visitas.personal_exterior));
        //    listSqlParameters.Add(new MySqlParameter("demanda_estacionamientoIn", b_inmuebles_visitas.demanda_estacionamiento));
        //    listSqlParameters.Add(new MySqlParameter("sucursalIn", b_inmuebles_visitas.sucursal));
        //    listSqlParameters.Add(new MySqlParameter("geolocalizacionIn", b_inmuebles_visitas.geolocalizacion));
        //    listSqlParameters.Add(new MySqlParameter("exterior1In", b_inmuebles_visitas.exterior1));
        //    listSqlParameters.Add(new MySqlParameter("exterior2In", b_inmuebles_visitas.exterior2));
        //    listSqlParameters.Add(new MySqlParameter("circundante_norteIn", b_inmuebles_visitas.circundante_norte));
        //    listSqlParameters.Add(new MySqlParameter("circundante_surIn", b_inmuebles_visitas.circundante_sur));
        //    listSqlParameters.Add(new MySqlParameter("circundante_orienteIn", b_inmuebles_visitas.circundante_oriente));
        //    listSqlParameters.Add(new MySqlParameter("circundante_ponienteIn", b_inmuebles_visitas.circundante_poniente));
        //    listSqlParameters.Add(new MySqlParameter("interior1In", b_inmuebles_visitas.interior1));
        //    listSqlParameters.Add(new MySqlParameter("interior2In", b_inmuebles_visitas.interior2));
        //    listSqlParameters.Add(new MySqlParameter("espacio1In", b_inmuebles_visitas.espacio1));
        //    listSqlParameters.Add(new MySqlParameter("espacio2In", b_inmuebles_visitas.espacio2));
        //    listSqlParameters.Add(new MySqlParameter("mantenimiento1In", b_inmuebles_visitas.mantenimiento1));
        //    listSqlParameters.Add(new MySqlParameter("mantenimiento2In", b_inmuebles_visitas.mantenimiento2));
        //    listSqlParameters.Add(new MySqlParameter("planta_emergencia2In", b_inmuebles_visitas.planta_emergencia2));
        //    listSqlParameters.Add(new MySqlParameter("medidor_energiaIn", b_inmuebles_visitas.medidor_energia));
        //    listSqlParameters.Add(new MySqlParameter("toma_aguaIn", b_inmuebles_visitas.toma_agua));
        //    listSqlParameters.Add(new MySqlParameter("impermeabilizanteIn", b_inmuebles_visitas.impermeabilizante));
        //    listSqlParameters.Add(new MySqlParameter("arquitectonicoIn", b_inmuebles_visitas.arquitectonico));
        //    listSqlParameters.Add(new MySqlParameter("inclusion2In", b_inmuebles_visitas.inclusion2));
        //    listSqlParameters.Add(new MySqlParameter("estacionamientoIn", b_inmuebles_visitas.estacionamiento));
        //    listSqlParameters.Add(new MySqlParameter("observacionesIn", b_inmuebles_visitas.observaciones ?? string.Empty));
        //    listSqlParameters.Add(new MySqlParameter("conclusionesIn", b_inmuebles_visitas.conclusiones ?? string.Empty));
        //    listSqlParameters.Add(new MySqlParameter("vinculo_mapaIn", b_inmuebles_visitas.vinculo_mapa));
        //    listSqlParameters.Add(new MySqlParameter("m2_terrenoIn", b_inmuebles_visitas.m2_terreno));
        //    listSqlParameters.Add(new MySqlParameter("area_rentableIn", b_inmuebles_visitas.area_rentable));
        //    listSqlParameters.Add(new MySqlParameter("m2_construccionIn", b_inmuebles_visitas.m2_construccion));
        //    listSqlParameters.Add(new MySqlParameter("superficie_estacionamientoIn", b_inmuebles_visitas.superficie_estacionamiento));
        //    listSqlParameters.Add(new MySqlParameter("valor_comercialIn", b_inmuebles_visitas.valor_comercial));
        //    listSqlParameters.Add(new MySqlParameter("id_b_variacion_inmuebleIn", b_inmuebles_visitas.id_b_variacion_inmueble));
        //    listSqlParameters.Add(new MySqlParameter("revisadoIn", b_inmuebles_visitas.revisado));
        //    listSqlParameters.Add(new MySqlParameter("vistadoIn", b_inmuebles_visitas.vistado));
        //    listSqlParameters.Add(new MySqlParameter("especialIn", b_inmuebles_visitas.especial));
        //    listSqlParameters.Add(new MySqlParameter("especial_otroIn", b_inmuebles_visitas.especial_otro));
        //    listSqlParameters.Add(new MySqlParameter("mantenimientoIn", b_inmuebles_visitas.mantenimiento));
        //    listSqlParameters.Add(new MySqlParameter("aire_acondicionado2In", b_inmuebles_visitas.aire_acondicionado2));
        //    listSqlParameters.Add(new MySqlParameter("NSE_1In", b_inmuebles_visitas.NSE_1?.ToString() ?? string.Empty));
        //    listSqlParameters.Add(new MySqlParameter("NSE_2In", b_inmuebles_visitas.NSE_2?.ToString() ?? string.Empty));

        //    listSqlParameters.Add(new MySqlParameter("vandalizadoIn", b_inmuebles_visitas.vandalizado?.ToString() ?? string.Empty));
        //    listSqlParameters.Add(new MySqlParameter("invadido", b_inmuebles_visitas.invadido?.ToString() ?? string.Empty));
        //    listSqlParameters.Add(new MySqlParameter("desocupadoIn", b_inmuebles_visitas.desocupado?.ToString() ?? string.Empty));
        //    listSqlParameters.Add(new MySqlParameter("estacionamiento2In", b_inmuebles_visitas.estacionamiento2?.ToString() ?? string.Empty));
        //    listSqlParameters.Add(new MySqlParameter("tipificacionIn", b_inmuebles_visitas.tipificacion?.ToString() ?? string.Empty));
        //    listSqlParameters.Add(new MySqlParameter("asentamientoIn", b_inmuebles_visitas.asentamiento?.ToString() ?? string.Empty));
        //    listSqlParameters.Add(new MySqlParameter("tendenciaIn", b_inmuebles_visitas.tendencia?.ToString() ?? string.Empty));
        //    listSqlParameters.Add(new MySqlParameter("segmentoIn", b_inmuebles_visitas.segmento?.ToString() ?? string.Empty));

        //    listSqlParameters.Add(new MySqlParameter("areas_convivenciaIn", b_inmuebles_visitas.areas_convivencia?.ToString() ?? string.Empty));
        //    listSqlParameters.Add(new MySqlParameter("areas_convivencia_descIn", b_inmuebles_visitas.areas_convivencia_desc?.ToString() ?? string.Empty));

        //    conexion.RunStoredProcedure("b_inmuebles_visitasEdit", listSqlParameters);
        //}

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


        private List<B_cg_tipo_expediente_contratos> DataToModel(DataTable dataTable)
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

