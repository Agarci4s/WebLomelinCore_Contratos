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
    public class DataInmueblesVisita
    {
        private Conexion conexion = new Conexion();

        public List<B_inmuebles_visitas> Get(int idCartera, int idUsuario, DateTime fecha, DateTime fecha2)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idCarteraIn", idCartera));
            listSqlParameters.Add(new MySqlParameter("idUsuarioIn", idUsuario));
            listSqlParameters.Add(new MySqlParameter("fechaIn", fecha));
            listSqlParameters.Add(new MySqlParameter("fecha2In", fecha2));
            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_visitasGet", listSqlParameters);
            return DataToModel(dataTable);
        }

        public List<B_inmuebles_visitas_inf_reporte> GetFormatoInfReporte(int idCartera, int idUsuario, int estatus_visita, bool vistado, bool especial, DateTime fecha, DateTime fecha2)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idCarteraIn", idCartera));
            listSqlParameters.Add(new MySqlParameter("idUsuarioIn", idUsuario));
            listSqlParameters.Add(new MySqlParameter("estatus_visitaIN", estatus_visita));
            listSqlParameters.Add(new MySqlParameter("vistadoIn", vistado));
            listSqlParameters.Add(new MySqlParameter("especialIn", especial));
            listSqlParameters.Add(new MySqlParameter("fechaIn", fecha));
            listSqlParameters.Add(new MySqlParameter("fecha2In", fecha2));



            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_visitasReporteGet", listSqlParameters);
            return DataToModelFormatoInfReporte(dataTable);
        }

        public List<B_inmuebles_visitas_inf_reporte> GetFormatoFueraPoliticaInfReporte(int idCartera, int idUsuario, int estatus_visita, DateTime fecha, DateTime fecha2)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idCarteraIn", idCartera));
            listSqlParameters.Add(new MySqlParameter("idUsuarioIn", idUsuario));
            listSqlParameters.Add(new MySqlParameter("estatus_visitaIN", estatus_visita));
            listSqlParameters.Add(new MySqlParameter("fechaIn", fecha));
            listSqlParameters.Add(new MySqlParameter("fecha2In", fecha2));


            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_visitasFueraPoliticaGet", listSqlParameters);
            return DataToModelFormatoInfReporte(dataTable);
        }

        public List<B_inmuebles_visitas_reporte> GetFormatoReporte(int idCartera, int idUsuario, int estatus_visita, bool vistado, bool especial, DateTime fecha, DateTime fecha2, int regimen)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idCarteraIn", idCartera));
            listSqlParameters.Add(new MySqlParameter("idUsuarioIn", idUsuario));
            listSqlParameters.Add(new MySqlParameter("estatus_visitaIN", estatus_visita));
            listSqlParameters.Add(new MySqlParameter("vistadoIn", vistado));
            listSqlParameters.Add(new MySqlParameter("especialIn", especial));
            listSqlParameters.Add(new MySqlParameter("fechaIn", fecha));
            listSqlParameters.Add(new MySqlParameter("fecha2In", fecha2));
            listSqlParameters.Add(new MySqlParameter("regimenIn", regimen));


            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_visitasReporteGet", listSqlParameters);
            return DataToModelFormatoReporte(dataTable);
        }

        public List<B_inmuebles_visitas_reporte> GetFormatoFueraPoliticaReporte(int idCartera, int idUsuario, int estatus_visita, DateTime fecha, DateTime fecha2)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idCarteraIn", idCartera));
            listSqlParameters.Add(new MySqlParameter("idUsuarioIn", idUsuario));
            listSqlParameters.Add(new MySqlParameter("estatus_visitaIN", estatus_visita));
            listSqlParameters.Add(new MySqlParameter("fechaIn", fecha));
            listSqlParameters.Add(new MySqlParameter("fecha2In", fecha2));


            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_visitasFueraPoliticaGet", listSqlParameters);
            return DataToModelFormatoReporte(dataTable);
        }

        public List<B_inmuebles_visitas> Get(int idUsuario, int id_b_inmuebles, int estatus_visita)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idUsuarioIn", idUsuario));
            listSqlParameters.Add(new MySqlParameter("id_b_inmueblesIn", id_b_inmuebles));
            listSqlParameters.Add(new MySqlParameter("estatus_visitaIN", estatus_visita));

            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_visitasGetByStatus", listSqlParameters);

            return DataToModel(dataTable);
        }

        public List<B_inmuebles_visitas> GetById(int idUsuario, int id_b_inmuebles_visita)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idUsuarioIn", idUsuario));
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_visitaIn", id_b_inmuebles_visita));

            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_visitasGetById", listSqlParameters);

            return DataToModel(dataTable);
        }

        public DataTable GetResumenCedular(int id_b_inmuebles)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("id_b_inmueblesIn", id_b_inmuebles));
            //listSqlParameters.Add(new MySqlParameter("estatus_visitaIN", estatus_visita));

            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_visitas_GetCedulaResumen", listSqlParameters);

            return dataTable;
            //return DataToModelCedulaResumen(dataTable);
        }

        public void Edit(B_inmuebles_visitas b_inmuebles_visitas)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_visitaIn", b_inmuebles_visitas.id_b_inmuebles_visita));
            listSqlParameters.Add(new MySqlParameter("id_b_inmueblesIn", b_inmuebles_visitas.id_b_inmuebles));
            listSqlParameters.Add(new MySqlParameter("estatus_visitaIn", b_inmuebles_visitas.estatus_visita));
            listSqlParameters.Add(new MySqlParameter("fecha_visitaIn", b_inmuebles_visitas.fecha_visita));
            listSqlParameters.Add(new MySqlParameter("mapa_seguridadIn", b_inmuebles_visitas.mapa_seguridad));
            listSqlParameters.Add(new MySqlParameter("inclusionIn", b_inmuebles_visitas.inclusion));
            listSqlParameters.Add(new MySqlParameter("no_anunciosIn", b_inmuebles_visitas.no_anuncios));
            listSqlParameters.Add(new MySqlParameter("marquesinaIn", b_inmuebles_visitas.marquesina));
            listSqlParameters.Add(new MySqlParameter("letras_rotuladasIn", b_inmuebles_visitas.letras_rotuladas));
            listSqlParameters.Add(new MySqlParameter("banderaIn", b_inmuebles_visitas.bandera));
            listSqlParameters.Add(new MySqlParameter("paletaIn", b_inmuebles_visitas.paleta));
            listSqlParameters.Add(new MySqlParameter("unipolarIn", b_inmuebles_visitas.unipolar));
            listSqlParameters.Add(new MySqlParameter("totemIn", b_inmuebles_visitas.totem));
            listSqlParameters.Add(new MySqlParameter("contrato_rentaIn", b_inmuebles_visitas.contrato_renta));
            listSqlParameters.Add(new MySqlParameter("m2_sucursal_precioIn", b_inmuebles_visitas.m2_sucursal_precio));
            listSqlParameters.Add(new MySqlParameter("m2_totalesIn", b_inmuebles_visitas.m2_totales));
            listSqlParameters.Add(new MySqlParameter("no_comparablesIn", b_inmuebles_visitas.no_comparables));
            listSqlParameters.Add(new MySqlParameter("valor_mercadoIn", b_inmuebles_visitas.valor_mercado));
            listSqlParameters.Add(new MySqlParameter("m2_rango_infIn", b_inmuebles_visitas.m2_rango_inf));
            listSqlParameters.Add(new MySqlParameter("m2_rango_supIn", b_inmuebles_visitas.m2_rango_sup));
            listSqlParameters.Add(new MySqlParameter("m2_rango_inf2In", b_inmuebles_visitas.m2_rango_inf2));
            listSqlParameters.Add(new MySqlParameter("m2_rango_sup2In", b_inmuebles_visitas.m2_rango_sup2));
            listSqlParameters.Add(new MySqlParameter("m2_rango_inf3In", b_inmuebles_visitas.m2_rango_inf3));
            listSqlParameters.Add(new MySqlParameter("m2_rango_sup3In", b_inmuebles_visitas.m2_rango_sup3));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_valor_mercadoIn", b_inmuebles_visitas.id_b_cg_valor_mercado));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_conservacionIn", b_inmuebles_visitas.id_b_cg_conservacion));
            listSqlParameters.Add(new MySqlParameter("aire_acondicionadoIn", b_inmuebles_visitas.aire_acondicionado));
            listSqlParameters.Add(new MySqlParameter("planta_emergenciaIn", b_inmuebles_visitas.planta_emergencia));
            listSqlParameters.Add(new MySqlParameter("impermeabilizacionIn", b_inmuebles_visitas.impermeabilizacion));
            listSqlParameters.Add(new MySqlParameter("medidorIn", b_inmuebles_visitas.medidor));
            listSqlParameters.Add(new MySqlParameter("medidor_direccionIn", b_inmuebles_visitas.medidor_direccion));
            listSqlParameters.Add(new MySqlParameter("agua_tomaIn", b_inmuebles_visitas.agua_toma));
            listSqlParameters.Add(new MySqlParameter("agua_toma_direccionIn", b_inmuebles_visitas.agua_toma_direccion));
            listSqlParameters.Add(new MySqlParameter("practicajasIn", b_inmuebles_visitas.practicajas));
            listSqlParameters.Add(new MySqlParameter("practicajas_servicioIn", b_inmuebles_visitas.practicajas_servicio));
            listSqlParameters.Add(new MySqlParameter("ventanillasIn", b_inmuebles_visitas.ventanillas));
            listSqlParameters.Add(new MySqlParameter("ventanillas_servicioIn", b_inmuebles_visitas.ventanillas_servicio));
            listSqlParameters.Add(new MySqlParameter("cajeros_automaticosIn", b_inmuebles_visitas.cajeros_automaticos));
            listSqlParameters.Add(new MySqlParameter("cajeros_automaticos_servicioIn", b_inmuebles_visitas.cajeros_automaticos_servicio));
            listSqlParameters.Add(new MySqlParameter("cajones_estacionamientoIn", b_inmuebles_visitas.cajones_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("cajones_estacionamiento_servicioIn", b_inmuebles_visitas.cajones_estacionamiento_servicio));
            listSqlParameters.Add(new MySqlParameter("cajones_estacionamiento_ejecutivoIn", b_inmuebles_visitas.cajones_estacionamiento_ejecutivo));
            listSqlParameters.Add(new MySqlParameter("cajones_estacionamiento_ejecutivo_servicioIn", b_inmuebles_visitas.cajones_estacionamiento_ejecutivo_servicio));
            listSqlParameters.Add(new MySqlParameter("operador_estacionamientoIn", b_inmuebles_visitas.operador_estacionamiento ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("libreIn", b_inmuebles_visitas.libre));
            listSqlParameters.Add(new MySqlParameter("cortesiasIn", b_inmuebles_visitas.cortesias));
            listSqlParameters.Add(new MySqlParameter("concesionariaIn", b_inmuebles_visitas.concesionaria));
            listSqlParameters.Add(new MySqlParameter("valet_parkingIn", b_inmuebles_visitas.valet_parking));
            listSqlParameters.Add(new MySqlParameter("personal_exteriorIn", b_inmuebles_visitas.personal_exterior));
            listSqlParameters.Add(new MySqlParameter("demanda_estacionamientoIn", b_inmuebles_visitas.demanda_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("sucursalIn", b_inmuebles_visitas.sucursal));
            listSqlParameters.Add(new MySqlParameter("geolocalizacionIn", b_inmuebles_visitas.geolocalizacion));
            listSqlParameters.Add(new MySqlParameter("exterior1In", b_inmuebles_visitas.exterior1));
            listSqlParameters.Add(new MySqlParameter("exterior2In", b_inmuebles_visitas.exterior2));
            listSqlParameters.Add(new MySqlParameter("circundante_norteIn", b_inmuebles_visitas.circundante_norte));
            listSqlParameters.Add(new MySqlParameter("circundante_surIn", b_inmuebles_visitas.circundante_sur));
            listSqlParameters.Add(new MySqlParameter("circundante_orienteIn", b_inmuebles_visitas.circundante_oriente));
            listSqlParameters.Add(new MySqlParameter("circundante_ponienteIn", b_inmuebles_visitas.circundante_poniente));
            listSqlParameters.Add(new MySqlParameter("interior1In", b_inmuebles_visitas.interior1));
            listSqlParameters.Add(new MySqlParameter("interior2In", b_inmuebles_visitas.interior2));
            listSqlParameters.Add(new MySqlParameter("espacio1In", b_inmuebles_visitas.espacio1));
            listSqlParameters.Add(new MySqlParameter("espacio2In", b_inmuebles_visitas.espacio2));
            listSqlParameters.Add(new MySqlParameter("mantenimiento1In", b_inmuebles_visitas.mantenimiento1));
            listSqlParameters.Add(new MySqlParameter("mantenimiento2In", b_inmuebles_visitas.mantenimiento2));
            listSqlParameters.Add(new MySqlParameter("planta_emergencia2In", b_inmuebles_visitas.planta_emergencia2));
            listSqlParameters.Add(new MySqlParameter("medidor_energiaIn", b_inmuebles_visitas.medidor_energia));
            listSqlParameters.Add(new MySqlParameter("toma_aguaIn", b_inmuebles_visitas.toma_agua));
            listSqlParameters.Add(new MySqlParameter("impermeabilizanteIn", b_inmuebles_visitas.impermeabilizante));
            listSqlParameters.Add(new MySqlParameter("arquitectonicoIn", b_inmuebles_visitas.arquitectonico));
            listSqlParameters.Add(new MySqlParameter("inclusion2In", b_inmuebles_visitas.inclusion2));
            listSqlParameters.Add(new MySqlParameter("estacionamientoIn", b_inmuebles_visitas.estacionamiento));
            listSqlParameters.Add(new MySqlParameter("observacionesIn", b_inmuebles_visitas.observaciones ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("conclusionesIn", b_inmuebles_visitas.conclusiones ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("vinculo_mapaIn", b_inmuebles_visitas.vinculo_mapa));
            listSqlParameters.Add(new MySqlParameter("m2_terrenoIn", b_inmuebles_visitas.m2_terreno));
            listSqlParameters.Add(new MySqlParameter("area_rentableIn", b_inmuebles_visitas.area_rentable));
            listSqlParameters.Add(new MySqlParameter("m2_construccionIn", b_inmuebles_visitas.m2_construccion));
            listSqlParameters.Add(new MySqlParameter("superficie_estacionamientoIn", b_inmuebles_visitas.superficie_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("valor_comercialIn", b_inmuebles_visitas.valor_comercial));
            listSqlParameters.Add(new MySqlParameter("id_b_variacion_inmuebleIn", b_inmuebles_visitas.id_b_variacion_inmueble));
            listSqlParameters.Add(new MySqlParameter("revisadoIn", b_inmuebles_visitas.revisado));
            listSqlParameters.Add(new MySqlParameter("vistadoIn", b_inmuebles_visitas.vistado));
            listSqlParameters.Add(new MySqlParameter("especialIn", b_inmuebles_visitas.especial));
            listSqlParameters.Add(new MySqlParameter("especial_otroIn", b_inmuebles_visitas.especial_otro));
            listSqlParameters.Add(new MySqlParameter("mantenimientoIn", b_inmuebles_visitas.mantenimiento));
            listSqlParameters.Add(new MySqlParameter("aire_acondicionado2In", b_inmuebles_visitas.aire_acondicionado2));
            listSqlParameters.Add(new MySqlParameter("NSE_1In", b_inmuebles_visitas.NSE_1?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("NSE_2In", b_inmuebles_visitas.NSE_2?.ToString() ?? string.Empty));

            listSqlParameters.Add(new MySqlParameter("vandalizadoIn", b_inmuebles_visitas.vandalizado?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("invadido", b_inmuebles_visitas.invadido?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("desocupadoIn", b_inmuebles_visitas.desocupado?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("estacionamiento2In", b_inmuebles_visitas.estacionamiento2?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("tipificacionIn", b_inmuebles_visitas.tipificacion?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("asentamientoIn", b_inmuebles_visitas.asentamiento?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("tendenciaIn", b_inmuebles_visitas.tendencia?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("segmentoIn", b_inmuebles_visitas.segmento?.ToString() ?? string.Empty));

            listSqlParameters.Add(new MySqlParameter("areas_convivenciaIn", b_inmuebles_visitas.areas_convivencia?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("areas_convivencia_descIn", b_inmuebles_visitas.areas_convivencia_desc?.ToString() ?? string.Empty));

            conexion.RunStoredProcedure("b_inmuebles_visitasEdit", listSqlParameters);
        }

        public int Insert(B_inmuebles_visitas b_inmuebles_visitas)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("id_b_inmueblesIn", b_inmuebles_visitas.id_b_inmuebles));
            listSqlParameters.Add(new MySqlParameter("estatus_visitaIn", b_inmuebles_visitas.estatus_visita));
            listSqlParameters.Add(new MySqlParameter("fecha_visitaIn", b_inmuebles_visitas.fecha_visita));
            listSqlParameters.Add(new MySqlParameter("mapa_seguridadIn", b_inmuebles_visitas.mapa_seguridad));
            listSqlParameters.Add(new MySqlParameter("inclusionIn", b_inmuebles_visitas.inclusion));
            listSqlParameters.Add(new MySqlParameter("no_anunciosIn", b_inmuebles_visitas.no_anuncios));
            listSqlParameters.Add(new MySqlParameter("marquesinaIn", b_inmuebles_visitas.marquesina));
            listSqlParameters.Add(new MySqlParameter("letras_rotuladasIn", b_inmuebles_visitas.letras_rotuladas));
            listSqlParameters.Add(new MySqlParameter("banderaIn", b_inmuebles_visitas.bandera));
            listSqlParameters.Add(new MySqlParameter("paletaIn", b_inmuebles_visitas.paleta));
            listSqlParameters.Add(new MySqlParameter("unipolarIn", b_inmuebles_visitas.unipolar));
            listSqlParameters.Add(new MySqlParameter("totemIn", b_inmuebles_visitas.totem));
            listSqlParameters.Add(new MySqlParameter("contrato_rentaIn", b_inmuebles_visitas.contrato_renta));
            listSqlParameters.Add(new MySqlParameter("m2_sucursal_precioIn", b_inmuebles_visitas.m2_sucursal_precio));
            listSqlParameters.Add(new MySqlParameter("m2_totalesIn", b_inmuebles_visitas.m2_totales));
            listSqlParameters.Add(new MySqlParameter("no_comparablesIn", b_inmuebles_visitas.no_comparables));
            listSqlParameters.Add(new MySqlParameter("valor_mercadoIn", b_inmuebles_visitas.valor_mercado));
            listSqlParameters.Add(new MySqlParameter("m2_rango_infIn", b_inmuebles_visitas.m2_rango_inf));
            listSqlParameters.Add(new MySqlParameter("m2_rango_supIn", b_inmuebles_visitas.m2_rango_sup));
            listSqlParameters.Add(new MySqlParameter("m2_rango_inf2In", b_inmuebles_visitas.m2_rango_inf2));
            listSqlParameters.Add(new MySqlParameter("m2_rango_sup2In", b_inmuebles_visitas.m2_rango_sup2));

            listSqlParameters.Add(new MySqlParameter("m2_rango_inf3In", b_inmuebles_visitas.m2_rango_inf3));
            listSqlParameters.Add(new MySqlParameter("m2_rango_sup3In", b_inmuebles_visitas.m2_rango_sup3));

            listSqlParameters.Add(new MySqlParameter("id_b_cg_valor_mercadoIn", b_inmuebles_visitas.id_b_cg_valor_mercado));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_conservacionIn", b_inmuebles_visitas.id_b_cg_conservacion));
            listSqlParameters.Add(new MySqlParameter("aire_acondicionadoIn", b_inmuebles_visitas.aire_acondicionado));
            listSqlParameters.Add(new MySqlParameter("planta_emergenciaIn", b_inmuebles_visitas.planta_emergencia));
            listSqlParameters.Add(new MySqlParameter("impermeabilizacionIn", b_inmuebles_visitas.impermeabilizacion));
            listSqlParameters.Add(new MySqlParameter("medidorIn", b_inmuebles_visitas.medidor));
            listSqlParameters.Add(new MySqlParameter("medidor_direccionIn", b_inmuebles_visitas.medidor_direccion));
            listSqlParameters.Add(new MySqlParameter("agua_tomaIn", b_inmuebles_visitas.agua_toma));
            listSqlParameters.Add(new MySqlParameter("agua_toma_direccionIn", b_inmuebles_visitas.agua_toma_direccion));
            listSqlParameters.Add(new MySqlParameter("practicajasIn", b_inmuebles_visitas.practicajas));
            listSqlParameters.Add(new MySqlParameter("practicajas_servicioIn", b_inmuebles_visitas.practicajas_servicio));
            listSqlParameters.Add(new MySqlParameter("ventanillasIn", b_inmuebles_visitas.ventanillas));
            listSqlParameters.Add(new MySqlParameter("ventanillas_servicioIn", b_inmuebles_visitas.ventanillas_servicio));
            listSqlParameters.Add(new MySqlParameter("cajeros_automaticosIn", b_inmuebles_visitas.cajeros_automaticos));
            listSqlParameters.Add(new MySqlParameter("cajeros_automaticos_servicioIn", b_inmuebles_visitas.cajeros_automaticos_servicio));
            listSqlParameters.Add(new MySqlParameter("cajones_estacionamientoIn", b_inmuebles_visitas.cajones_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("cajones_estacionamiento_servicioIn", b_inmuebles_visitas.cajones_estacionamiento_servicio));
            listSqlParameters.Add(new MySqlParameter("cajones_estacionamiento_ejecutivoIn", b_inmuebles_visitas.cajones_estacionamiento_ejecutivo));
            listSqlParameters.Add(new MySqlParameter("cajones_estacionamiento_ejecutivo_servicioIn", b_inmuebles_visitas.cajones_estacionamiento_ejecutivo_servicio));
            listSqlParameters.Add(new MySqlParameter("operador_estacionamientoIn", b_inmuebles_visitas.operador_estacionamiento ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("libreIn", b_inmuebles_visitas.libre));
            listSqlParameters.Add(new MySqlParameter("cortesiasIn", b_inmuebles_visitas.cortesias));
            listSqlParameters.Add(new MySqlParameter("concesionariaIn", b_inmuebles_visitas.concesionaria));
            listSqlParameters.Add(new MySqlParameter("valet_parkingIn", b_inmuebles_visitas.valet_parking));
            listSqlParameters.Add(new MySqlParameter("personal_exteriorIn", b_inmuebles_visitas.personal_exterior));
            listSqlParameters.Add(new MySqlParameter("demanda_estacionamientoIn", b_inmuebles_visitas.demanda_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("sucursalIn", b_inmuebles_visitas.sucursal));
            listSqlParameters.Add(new MySqlParameter("geolocalizacionIn", b_inmuebles_visitas.geolocalizacion));
            listSqlParameters.Add(new MySqlParameter("exterior1In", b_inmuebles_visitas.exterior1));
            listSqlParameters.Add(new MySqlParameter("exterior2In", b_inmuebles_visitas.exterior2));
            listSqlParameters.Add(new MySqlParameter("circundante_norteIn", b_inmuebles_visitas.circundante_norte));
            listSqlParameters.Add(new MySqlParameter("circundante_surIn", b_inmuebles_visitas.circundante_sur));
            listSqlParameters.Add(new MySqlParameter("circundante_orienteIn", b_inmuebles_visitas.circundante_oriente));
            listSqlParameters.Add(new MySqlParameter("circundante_ponienteIn", b_inmuebles_visitas.circundante_poniente));
            listSqlParameters.Add(new MySqlParameter("interior1In", b_inmuebles_visitas.interior1));
            listSqlParameters.Add(new MySqlParameter("interior2In", b_inmuebles_visitas.interior2));
            listSqlParameters.Add(new MySqlParameter("espacio1In", b_inmuebles_visitas.espacio1));
            listSqlParameters.Add(new MySqlParameter("espacio2In", b_inmuebles_visitas.espacio2));
            listSqlParameters.Add(new MySqlParameter("mantenimiento1In", b_inmuebles_visitas.mantenimiento1));
            listSqlParameters.Add(new MySqlParameter("mantenimiento2In", b_inmuebles_visitas.mantenimiento2));
            listSqlParameters.Add(new MySqlParameter("planta_emergencia2In", b_inmuebles_visitas.planta_emergencia2));
            listSqlParameters.Add(new MySqlParameter("medidor_energiaIn", b_inmuebles_visitas.medidor_energia));
            listSqlParameters.Add(new MySqlParameter("toma_aguaIn", b_inmuebles_visitas.toma_agua));
            listSqlParameters.Add(new MySqlParameter("impermeabilizanteIn", b_inmuebles_visitas.impermeabilizante));
            listSqlParameters.Add(new MySqlParameter("arquitectonicoIn", b_inmuebles_visitas.arquitectonico));
            listSqlParameters.Add(new MySqlParameter("inclusion2In", b_inmuebles_visitas.inclusion2));
            listSqlParameters.Add(new MySqlParameter("estacionamientoIn", b_inmuebles_visitas.estacionamiento));
            listSqlParameters.Add(new MySqlParameter("observacionesIn", b_inmuebles_visitas.observaciones ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("conclusionesIn", b_inmuebles_visitas.conclusiones ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("vinculo_mapaIn", b_inmuebles_visitas.vinculo_mapa));
            listSqlParameters.Add(new MySqlParameter("m2_terrenoIn", b_inmuebles_visitas.m2_terreno));
            listSqlParameters.Add(new MySqlParameter("area_rentableIn", b_inmuebles_visitas.area_rentable));
            listSqlParameters.Add(new MySqlParameter("m2_construccionIn", b_inmuebles_visitas.m2_construccion));
            listSqlParameters.Add(new MySqlParameter("superficie_estacionamientoIn", b_inmuebles_visitas.superficie_estacionamiento));
            listSqlParameters.Add(new MySqlParameter("valor_comercialIn", b_inmuebles_visitas.valor_comercial));
            listSqlParameters.Add(new MySqlParameter("id_b_variacion_inmuebleIn", b_inmuebles_visitas.id_b_variacion_inmueble));
            listSqlParameters.Add(new MySqlParameter("revisadoIn", b_inmuebles_visitas.revisado));
            listSqlParameters.Add(new MySqlParameter("vistadoIn", b_inmuebles_visitas.vistado));
            listSqlParameters.Add(new MySqlParameter("especialIn", b_inmuebles_visitas.especial));
            listSqlParameters.Add(new MySqlParameter("especial_otroIn", b_inmuebles_visitas.especial_otro));
            listSqlParameters.Add(new MySqlParameter("mantenimientoIn", b_inmuebles_visitas.mantenimiento));
            listSqlParameters.Add(new MySqlParameter("aire_acondicionado2In", b_inmuebles_visitas.aire_acondicionado2));
            listSqlParameters.Add(new MySqlParameter("NSE_1In", b_inmuebles_visitas.NSE_1?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("NSE_2In", b_inmuebles_visitas.NSE_2?.ToString() ?? string.Empty));

            listSqlParameters.Add(new MySqlParameter("vandalizadoIn", b_inmuebles_visitas.vandalizado?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("invadido", b_inmuebles_visitas.invadido?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("desocupadoIn", b_inmuebles_visitas.desocupado?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("estacionamiento2In", b_inmuebles_visitas.estacionamiento2?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("tipificacionIn", b_inmuebles_visitas.tipificacion?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("asentamientoIn", b_inmuebles_visitas.asentamiento?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("tendenciaIn", b_inmuebles_visitas.tendencia?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("segmentoIn", b_inmuebles_visitas.segmento?.ToString() ?? string.Empty));

            listSqlParameters.Add(new MySqlParameter("areas_convivenciaIn", b_inmuebles_visitas.areas_convivencia?.ToString() ?? string.Empty));
            listSqlParameters.Add(new MySqlParameter("areas_convivencia_descIn", b_inmuebles_visitas.areas_convivencia_desc?.ToString() ?? string.Empty));



            List<MySqlParameter> listSqlParametersOUT = new List<MySqlParameter>();
            listSqlParametersOUT.Add(new MySqlParameter("idOUT", b_inmuebles_visitas.id_b_inmuebles_visita));

            MySqlParameterCollection mySqlParameterCollection = conexion.RunStoredProcedure("b_inmuebles_visitasInsert", listSqlParameters, listSqlParametersOUT);
            int id = (int)mySqlParameterCollection["idOUT"].Value;

            return id;

        }

        private List<B_inmuebles_visitas> DataToModel(DataTable dataTable)
        {
            List<B_inmuebles_visitas> b_Inmuebles_VisitasList = new List<B_inmuebles_visitas>();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    B_inmuebles_visitas b_Inmuebles_Visita = new B_inmuebles_visitas();

                    b_Inmuebles_Visita.id_b_inmuebles_visita = int.Parse(item["id_b_inmuebles_visita"].ToString());
                    b_Inmuebles_Visita.id_b_inmuebles = int.Parse(item["id_b_inmuebles"].ToString());
                    b_Inmuebles_Visita.estatus_visita = int.Parse(item["estatus_visita"].ToString());
                    b_Inmuebles_Visita.fecha_visita = Convert.ToDateTime(item["fecha_visita"].ToString());
                    b_Inmuebles_Visita.mapa_seguridad = Convert.ToBoolean(Convert.ToInt16(item["mapa_seguridad"].ToString()));
                    b_Inmuebles_Visita.inclusion = Convert.ToBoolean(Convert.ToInt16(item["inclusion"].ToString()));
                    b_Inmuebles_Visita.no_anuncios = int.Parse(item["no_anuncios"].ToString());
                    b_Inmuebles_Visita.marquesina = int.Parse(item["marquesina"].ToString());
                    b_Inmuebles_Visita.letras_rotuladas = int.Parse(item["letras_rotuladas"].ToString());
                    b_Inmuebles_Visita.bandera = int.Parse(item["bandera"].ToString());
                    b_Inmuebles_Visita.paleta = int.Parse(item["paleta"].ToString());
                    b_Inmuebles_Visita.unipolar = int.Parse(item["unipolar"].ToString());
                    b_Inmuebles_Visita.totem = int.Parse(item["totem"].ToString());
                    b_Inmuebles_Visita.contrato_renta = decimal.Parse(item["contrato_renta"].ToString());
                    b_Inmuebles_Visita.m2_sucursal_precio = decimal.Parse(item["m2_sucursal_precio"].ToString());
                    b_Inmuebles_Visita.m2_totales = decimal.Parse(item["m2_totales"].ToString());
                    b_Inmuebles_Visita.no_comparables = int.Parse(item["no_comparables"].ToString());
                    b_Inmuebles_Visita.valor_mercado = decimal.Parse(item["valor_mercado"].ToString());
                    b_Inmuebles_Visita.m2_rango_inf = decimal.Parse(item["m2_rango_inf"].ToString());
                    b_Inmuebles_Visita.m2_rango_sup = decimal.Parse(item["m2_rango_sup"].ToString());
                    b_Inmuebles_Visita.m2_rango_inf2 = decimal.Parse(item["m2_rango_inf2"].ToString());
                    b_Inmuebles_Visita.m2_rango_sup2 = decimal.Parse(item["m2_rango_sup2"].ToString());
                    b_Inmuebles_Visita.m2_rango_inf3 = decimal.Parse(item["m2_rango_inf3"].ToString());
                    b_Inmuebles_Visita.m2_rango_sup3 = decimal.Parse(item["m2_rango_sup3"].ToString());
                    b_Inmuebles_Visita.id_b_cg_valor_mercado = int.Parse(item["id_b_cg_valor_mercado"].ToString());
                    b_Inmuebles_Visita.id_b_cg_conservacion = int.Parse(item["id_b_cg_conservacion"].ToString());
                    b_Inmuebles_Visita.aire_acondicionado = Convert.ToBoolean(Convert.ToInt16(item["aire_acondicionado"].ToString()));
                    b_Inmuebles_Visita.planta_emergencia = item["planta_emergencia"].ToString();
                    b_Inmuebles_Visita.impermeabilizacion = item["impermeabilizacion"].ToString();
                    b_Inmuebles_Visita.medidor = item["medidor"].ToString();
                    b_Inmuebles_Visita.medidor_direccion = item["medidor_direccion"].ToString();
                    b_Inmuebles_Visita.agua_toma = item["agua_toma"].ToString();
                    b_Inmuebles_Visita.agua_toma_direccion = item["agua_toma_direccion"].ToString();
                    b_Inmuebles_Visita.practicajas = int.Parse(item["practicajas"].ToString());
                    b_Inmuebles_Visita.practicajas_servicio = int.Parse(item["practicajas_servicio"].ToString());
                    b_Inmuebles_Visita.ventanillas = int.Parse(item["ventanillas"].ToString());
                    b_Inmuebles_Visita.ventanillas_servicio = int.Parse(item["ventanillas_servicio"].ToString());
                    b_Inmuebles_Visita.cajeros_automaticos = int.Parse(item["cajeros_automaticos"].ToString());
                    b_Inmuebles_Visita.cajeros_automaticos_servicio = int.Parse(item["cajeros_automaticos_servicio"].ToString());
                    b_Inmuebles_Visita.cajones_estacionamiento = int.Parse(item["cajones_estacionamiento"].ToString());
                    b_Inmuebles_Visita.cajones_estacionamiento_servicio = int.Parse(item["cajones_estacionamiento_servicio"].ToString());
                    b_Inmuebles_Visita.cajones_estacionamiento_ejecutivo = int.Parse(item["cajones_estacionamiento_ejecutivo"].ToString());
                    b_Inmuebles_Visita.cajones_estacionamiento_ejecutivo_servicio = int.Parse(item["cajones_estacionamiento_ejecutivo_servicio"].ToString());
                    b_Inmuebles_Visita.operador_estacionamiento = item["operador_estacionamiento"].ToString();
                    b_Inmuebles_Visita.libre = Convert.ToBoolean(Convert.ToInt16(item["libre"].ToString()));
                    b_Inmuebles_Visita.cortesias = Convert.ToBoolean(Convert.ToInt16(item["cortesias"].ToString()));
                    b_Inmuebles_Visita.concesionaria = Convert.ToBoolean(Convert.ToInt16(item["concesionaria"].ToString()));
                    b_Inmuebles_Visita.valet_parking = Convert.ToBoolean(Convert.ToInt16(item["valet_parking"].ToString()));
                    b_Inmuebles_Visita.personal_exterior = Convert.ToBoolean(Convert.ToInt16(item["personal_exterior"].ToString()));
                    b_Inmuebles_Visita.demanda_estacionamiento = item["demanda_estacionamiento"].ToString();
                    b_Inmuebles_Visita.sucursal = item["sucursal"].ToString();
                    b_Inmuebles_Visita.geolocalizacion = item["geolocalizacion"].ToString();
                    b_Inmuebles_Visita.exterior1 = item["exterior1"].ToString();
                    b_Inmuebles_Visita.exterior2 = item["exterior2"].ToString();
                    b_Inmuebles_Visita.circundante_norte = item["circundante_norte"].ToString();
                    b_Inmuebles_Visita.circundante_sur = item["circundante_sur"].ToString();
                    b_Inmuebles_Visita.circundante_oriente = item["circundante_oriente"].ToString();
                    b_Inmuebles_Visita.circundante_poniente = item["circundante_poniente"].ToString();
                    b_Inmuebles_Visita.interior1 = item["interior1"].ToString();
                    b_Inmuebles_Visita.interior2 = item["interior2"].ToString();
                    b_Inmuebles_Visita.espacio1 = item["espacio1"].ToString();
                    b_Inmuebles_Visita.espacio2 = item["espacio2"].ToString();
                    b_Inmuebles_Visita.mantenimiento1 = item["mantenimiento1"].ToString();
                    b_Inmuebles_Visita.mantenimiento2 = item["mantenimiento2"].ToString();
                    b_Inmuebles_Visita.planta_emergencia2 = item["planta_emergencia2"].ToString();
                    b_Inmuebles_Visita.medidor_energia = item["medidor_energia"].ToString();
                    b_Inmuebles_Visita.toma_agua = item["toma_agua"].ToString();
                    b_Inmuebles_Visita.impermeabilizante = item["impermeabilizante"].ToString();
                    b_Inmuebles_Visita.arquitectonico = item["arquitectonico"].ToString();
                    b_Inmuebles_Visita.inclusion2 = item["inclusion2"].ToString();
                    b_Inmuebles_Visita.estacionamiento = item["estacionamiento"].ToString();
                    b_Inmuebles_Visita.observaciones = item["observaciones"].ToString();
                    b_Inmuebles_Visita.conclusiones = item["conclusiones"].ToString();
                    b_Inmuebles_Visita.vinculo_mapa = item["vinculo_mapa"].ToString();
                    b_Inmuebles_Visita.m2_terreno = decimal.Parse(item["m2_terreno"].ToString());
                    b_Inmuebles_Visita.area_rentable = decimal.Parse(item["area_rentable"].ToString());
                    b_Inmuebles_Visita.m2_construccion = decimal.Parse(item["m2_construccion"].ToString());
                    b_Inmuebles_Visita.superficie_estacionamiento = decimal.Parse(item["superficie_estacionamiento"].ToString());
                    b_Inmuebles_Visita.valor_comercial = decimal.Parse(item["valor_comercial"].ToString());
                    b_Inmuebles_Visita.id_b_variacion_inmueble = int.Parse(item["id_b_variacion_inmueble"].ToString());
                    b_Inmuebles_Visita.revisado = Convert.ToBoolean(Convert.ToInt16(item["revisado"].ToString()));
                    b_Inmuebles_Visita.vistado = Convert.ToBoolean(Convert.ToInt16(item["vistado"].ToString()));
                    b_Inmuebles_Visita.especial = Convert.ToBoolean(Convert.ToInt16(item["especial"].ToString()));
                    b_Inmuebles_Visita.especial_otro = item["especial_otro"].ToString();
                    b_Inmuebles_Visita.mantenimiento = item["mantenimiento"].ToString();
                    b_Inmuebles_Visita.aire_acondicionado2 = item["aire_acondicionado2"].ToString();
                    b_Inmuebles_Visita.NSE_1 = item["NSE_1"].ToString();
                    b_Inmuebles_Visita.NSE_2 = item["NSE_2"].ToString();

                    b_Inmuebles_Visita.vandalizado = item["vandalizado"].ToString();
                    b_Inmuebles_Visita.invadido = item["invadido"].ToString();
                    b_Inmuebles_Visita.desocupado = item["desocupado"].ToString();
                    b_Inmuebles_Visita.estacionamiento2 = item["estacionamiento2"].ToString();
                    b_Inmuebles_Visita.tipificacion = item["tipificacion"].ToString();
                    b_Inmuebles_Visita.asentamiento = item["asentamiento"].ToString();
                    b_Inmuebles_Visita.tendencia = item["tendencia"].ToString();
                    b_Inmuebles_Visita.segmento = item["segmento"].ToString();

                    b_Inmuebles_Visita.areas_convivencia = item["areas_convivencia"].ToString();
                    b_Inmuebles_Visita.areas_convivencia_desc = item["areas_convivencia_desc"].ToString();

                    b_Inmuebles_VisitasList.Add(b_Inmuebles_Visita);
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            return b_Inmuebles_VisitasList;
        }

        private List<B_inmuebles_visitas_reporte> DataToModelFormatoReporte(DataTable dataTable)
        {
            List<B_inmuebles_visitas_reporte> b_Inmuebles_VisitasList = new List<B_inmuebles_visitas_reporte>();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    B_inmuebles_visitas_reporte b_Inmuebles_Visita = new B_inmuebles_visitas_reporte();
                    try
                    {
                        b_Inmuebles_Visita.ue = item["ue"].ToString();
                        b_Inmuebles_Visita.cr = item["cr"].ToString();
                        b_Inmuebles_Visita.contrato = item["contrato"].ToString();
                        b_Inmuebles_Visita.nombre = item["nombre"].ToString();
                        b_Inmuebles_Visita.regimen = item["regimen"].ToString();
                        b_Inmuebles_Visita.region = item["region"].ToString();
                        b_Inmuebles_Visita.estado = item["estado"].ToString();
                        b_Inmuebles_Visita.municipio = item["municipio"].ToString();
                        b_Inmuebles_Visita.inmuebles_estatus = item["inmuebles_estatus"].ToString();
                        b_Inmuebles_Visita.inmuebles_visitas_estatus = item["inmuebles_visitas_estatus"].ToString();
                        b_Inmuebles_Visita.fecha_visita = Convert.ToDateTime(item["fecha_visita"].ToString()).ToString("dd/MM/yyyy");
                        b_Inmuebles_Visita.motivo_especial = item["motivo_especial"].ToString();
                        b_Inmuebles_Visita.direccion = item["direccion"].ToString();
                        b_Inmuebles_Visita.latidud = double.Parse(item["latidud"].ToString());
                        b_Inmuebles_Visita.longitud = double.Parse(item["longitud"].ToString());
                        b_Inmuebles_Visita.link_maps = item["link_maps"].ToString();
                        b_Inmuebles_Visita.m2_terreno = decimal.Parse(item["m2_terreno"].ToString());
                        b_Inmuebles_Visita.area_rentable = decimal.Parse(item["area_rentable"].ToString());
                        b_Inmuebles_Visita.m2_construccion = decimal.Parse(item["m2_construccion"].ToString());
                        b_Inmuebles_Visita.superficie_estacionamiento = decimal.Parse(item["superficie_estacionamiento"].ToString());
                        b_Inmuebles_Visita.mapa_seguridad = Convert.ToBoolean(Convert.ToInt16(item["mapa_seguridad"].ToString())) ? "Si" : "No";
                        b_Inmuebles_Visita.marquesina = int.Parse(item["marquesina"].ToString());
                        b_Inmuebles_Visita.letras_rotuladas = int.Parse(item["letras_rotuladas"].ToString());
                        b_Inmuebles_Visita.bandera = int.Parse(item["bandera"].ToString());
                        b_Inmuebles_Visita.paleta = int.Parse(item["paleta"].ToString());
                        b_Inmuebles_Visita.unipolar = int.Parse(item["unipolar"].ToString());
                        b_Inmuebles_Visita.totem = int.Parse(item["totem"].ToString());
                        b_Inmuebles_Visita.no_anuncios = int.Parse(item["no_anuncios"].ToString());
                        b_Inmuebles_Visita.inclusion = Convert.ToBoolean(Convert.ToInt16(item["inclusion"].ToString())) ? "Si" : "No";
                        b_Inmuebles_Visita.aire_acondicionado = Convert.ToBoolean(Convert.ToInt16(item["aire_acondicionado"].ToString())) ? "Si" : "No";
                        b_Inmuebles_Visita.planta_emergencia = item["planta_emergencia"].ToString();
                        b_Inmuebles_Visita.impermeabilizacion = item["impermeabilizacion"].ToString();
                        b_Inmuebles_Visita.medidor = item["medidor"].ToString();
                        b_Inmuebles_Visita.medidor_direccion = item["medidor_direccion"].ToString();
                        b_Inmuebles_Visita.agua_toma = item["agua_toma"].ToString();
                        b_Inmuebles_Visita.agua_toma_direccion = item["agua_toma_direccion"].ToString();
                        b_Inmuebles_Visita.practicajas = int.Parse(item["practicajas"].ToString());
                        b_Inmuebles_Visita.practicajas_servicio = int.Parse(item["practicajas_servicio"].ToString());
                        b_Inmuebles_Visita.practicajas_sin_servicio = b_Inmuebles_Visita.practicajas - b_Inmuebles_Visita.practicajas_servicio;
                        b_Inmuebles_Visita.ventanillas = int.Parse(item["ventanillas"].ToString());
                        b_Inmuebles_Visita.ventanillas_servicio = int.Parse(item["ventanillas_servicio"].ToString());
                        b_Inmuebles_Visita.ventanillas_sin_servicio = b_Inmuebles_Visita.ventanillas - b_Inmuebles_Visita.ventanillas_servicio;
                        b_Inmuebles_Visita.cajeros_automaticos = int.Parse(item["cajeros_automaticos"].ToString());
                        b_Inmuebles_Visita.cajeros_automaticos_servicio = int.Parse(item["cajeros_automaticos_servicio"].ToString());
                        b_Inmuebles_Visita.cajeros_automaticos_sin_servicio = b_Inmuebles_Visita.cajeros_automaticos - b_Inmuebles_Visita.cajeros_automaticos_servicio;
                        b_Inmuebles_Visita.cajones_estacionamiento = int.Parse(item["cajones_estacionamiento"].ToString());
                        b_Inmuebles_Visita.cajones_estacionamiento_servicio = int.Parse(item["cajones_estacionamiento_servicio"].ToString());
                        b_Inmuebles_Visita.cajones_estacionamiento_sin_servicio = b_Inmuebles_Visita.cajones_estacionamiento - b_Inmuebles_Visita.cajones_estacionamiento_servicio;
                        b_Inmuebles_Visita.cajones_estacionamiento_ejecutivo = int.Parse(item["cajones_estacionamiento_ejecutivo"].ToString());
                        b_Inmuebles_Visita.cajones_estacionamiento_ejecutivo_servicio = int.Parse(item["cajones_estacionamiento_ejecutivo_servicio"].ToString());
                        b_Inmuebles_Visita.cajones_estacionamiento_ejecutivo_sin_servicio = b_Inmuebles_Visita.cajones_estacionamiento_ejecutivo - b_Inmuebles_Visita.cajones_estacionamiento_ejecutivo_servicio;
                        b_Inmuebles_Visita.operador_estacionamiento = item["operador_estacionamiento"].ToString();
                        b_Inmuebles_Visita.valet_parking = Convert.ToBoolean(Convert.ToInt16(item["valet_parking"].ToString())) ? "Si" : "No";
                        b_Inmuebles_Visita.personal_exterior = Convert.ToBoolean(Convert.ToInt16(item["personal_exterior"].ToString())) ? "Si" : "No";
                        b_Inmuebles_Visita.demanda_estacionamiento = item["demanda_estacionamiento"].ToString();
                        b_Inmuebles_Visita.contrato_renta = decimal.Parse(item["contrato_renta"].ToString());
                        b_Inmuebles_Visita.m2_sucursal_precio = decimal.Parse(item["m2_sucursal_precio"].ToString());
                        b_Inmuebles_Visita.m2_totales = decimal.Parse(item["m2_totales"].ToString());
                        b_Inmuebles_Visita.no_comparables = int.Parse(item["no_comparables"].ToString());
                        b_Inmuebles_Visita.valor_mercado = item["valor_mercado"].ToString();
                        b_Inmuebles_Visita.variacion_inmueble = item["variacion_inmueble"].ToString();
                        b_Inmuebles_Visita.valor_comercial = decimal.Parse(item["valor_comercial"].ToString());
                        b_Inmuebles_Visita.m2_rango_inf = decimal.Parse(item["m2_rango_inf"].ToString());
                        b_Inmuebles_Visita.m2_rango_sup = decimal.Parse(item["m2_rango_sup"].ToString());
                        b_Inmuebles_Visita.m2_promedio = (b_Inmuebles_Visita.m2_rango_inf + b_Inmuebles_Visita.m2_rango_sup) / 2;
                        b_Inmuebles_Visita.m2_rango_inf2 = decimal.Parse(item["m2_rango_inf2"].ToString());
                        b_Inmuebles_Visita.m2_rango_sup2 = decimal.Parse(item["m2_rango_sup2"].ToString());
                        b_Inmuebles_Visita.m2_rango_prom2 = (b_Inmuebles_Visita.m2_rango_inf2 + b_Inmuebles_Visita.m2_rango_sup2) / 2;
                        b_Inmuebles_Visita.conservacion = item["conservacion"].ToString();
                        b_Inmuebles_Visita.observaciones = item["observaciones"].ToString();
                        b_Inmuebles_Visita.conclusiones = item["conclusiones"].ToString();
                        b_Inmuebles_Visita.NSE_1 = item["NSE_1"].ToString();
                        b_Inmuebles_Visita.NSE_2 = item["NSE_2"].ToString();
                        b_Inmuebles_Visita.m2_rango_inf3 = decimal.Parse(item["m2_rango_inf3"].ToString());
                        b_Inmuebles_Visita.m2_rango_sup3 = decimal.Parse(item["m2_rango_sup3"].ToString());
                        b_Inmuebles_Visita.m2_rango_prom3 = (b_Inmuebles_Visita.m2_rango_inf3 + b_Inmuebles_Visita.m2_rango_sup3) / 2;
                        b_Inmuebles_Visita.vandalizado = item["vandalizado"].ToString();
                        b_Inmuebles_Visita.invadido = item["invadido"].ToString();
                        b_Inmuebles_Visita.desocupado = item["desocupado"].ToString();
                        b_Inmuebles_Visita.tipificacion = item["tipificacion"].ToString();
                        b_Inmuebles_Visita.asentamiento = item["asentamiento"].ToString();
                        b_Inmuebles_Visita.tendencia = item["tendencia"].ToString();
                        b_Inmuebles_Visita.mantenimiento = item["mantenimiento"].ToString();
                        b_Inmuebles_Visita.areas_convivencia_desc = item["areas_convivencia_desc"].ToString();

                    }
                    catch (Exception)
                    {

                    }

                    b_Inmuebles_VisitasList.Add(b_Inmuebles_Visita);
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            return b_Inmuebles_VisitasList;
        }

        private List<B_inmuebles_visitas_inf_reporte> DataToModelFormatoInfReporte(DataTable dataTable)
        {
            List<B_inmuebles_visitas_inf_reporte> b_Inmuebles_VisitasList = new List<B_inmuebles_visitas_inf_reporte>();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    B_inmuebles_visitas_inf_reporte b_Inmuebles_Visita = new B_inmuebles_visitas_inf_reporte();
                    try
                    {
                        b_Inmuebles_Visita.ue = item["ue"].ToString();
                        b_Inmuebles_Visita.cr = item["cr"].ToString();
                        b_Inmuebles_Visita.nombre = item["nombre"].ToString();
                        b_Inmuebles_Visita.regimen = item["regimen"].ToString();
                        b_Inmuebles_Visita.region = item["region"].ToString();
                        b_Inmuebles_Visita.inmuebles_estatus = item["inmuebles_estatus"].ToString();
                        b_Inmuebles_Visita.inmuebles_visitas_estatus = item["inmuebles_visitas_estatus"].ToString();
                        b_Inmuebles_Visita.fecha_visita = Convert.ToDateTime(item["fecha_visita"].ToString()).ToString("dd/MM/yyyy");
                        b_Inmuebles_Visita.direccion = item["direccion"].ToString();
                        b_Inmuebles_Visita.latidud = double.Parse(item["latidud"].ToString());
                        b_Inmuebles_Visita.longitud = double.Parse(item["longitud"].ToString());

                        b_Inmuebles_Visita.marquesina = int.Parse(item["marquesina"].ToString());
                        b_Inmuebles_Visita.letras_rotuladas = int.Parse(item["letras_rotuladas"].ToString());
                        b_Inmuebles_Visita.bandera = int.Parse(item["bandera"].ToString());
                        b_Inmuebles_Visita.paleta = int.Parse(item["paleta"].ToString());
                        b_Inmuebles_Visita.unipolar = int.Parse(item["unipolar"].ToString());
                        b_Inmuebles_Visita.totem = int.Parse(item["totem"].ToString());
                        b_Inmuebles_Visita.no_anuncios = int.Parse(item["no_anuncios"].ToString());

                    }
                    catch (Exception)
                    {

                    }

                    b_Inmuebles_VisitasList.Add(b_Inmuebles_Visita);
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            return b_Inmuebles_VisitasList;
        }

        private decimal ConvertToDecimal(string cadena)
        {
            decimal valor;
            decimal.TryParse(cadena.Replace(",", "").Replace("$", ""), out valor);
            return valor;
        }

        public DataTable GetResumenCedularMasivo()
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_visitas_GetCedulaResumenMasivo", listSqlParameters);

            return dataTable;
        }
    }
}

