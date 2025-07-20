using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using NPOI.SS.Formula.Functions;
using System.Collections.Generic;
using System.Data;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;

namespace WebColliersCore.Data
{
    public class DataCG
    {
        private Conexion conexion = new Conexion();
        public List<SelectListItem> lst_b_cg_operador_estacionamiento()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_operador_estacionamientoGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["descripcion"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> lst_areas_convivencia()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "No", Value = "No" });
            listSelectListItems.Add(new SelectListItem { Text = "Si", Value = "Si" });
            return listSelectListItems;
        }

        public List<SelectListItem> lst_segmento()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            listSelectListItems.Add(new SelectListItem { Text = "A/B alto", Value = "A/B alto" });
            listSelectListItems.Add(new SelectListItem { Text = "C+ medio", Value = "C+ medio" });
            listSelectListItems.Add(new SelectListItem { Text = "C medio típico", Value = "C medio típico" });
            listSelectListItems.Add(new SelectListItem { Text = "C- Medio emergente", Value = "C- Medio emergente" });
            listSelectListItems.Add(new SelectListItem { Text = "D+ bajo típico", Value = "D+ bajo típico" });
            listSelectListItems.Add(new SelectListItem { Text = "D/E bajo extremo muy extremo", Value = "D/E bajo extremo muy extremo" });
            return listSelectListItems;
        }

        public List<SelectListItem> lst_tendencia()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            listSelectListItems.Add(new SelectListItem { Text = "Estable", Value = "Estable" });
            listSelectListItems.Add(new SelectListItem { Text = "A la alza", Value = "A la alza" });
            listSelectListItems.Add(new SelectListItem { Text = "A la baja", Value = "A la baja" });
            return listSelectListItems;
        }

        public List<SelectListItem> lst_asentamiento()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            listSelectListItems.Add(new SelectListItem { Text = "Megaciudad ", Value = "Megaciudad " });
            listSelectListItems.Add(new SelectListItem { Text = "Ciudad", Value = "Ciudad" });
            listSelectListItems.Add(new SelectListItem { Text = "Poblado", Value = "Poblado" });
            listSelectListItems.Add(new SelectListItem { Text = "Zona rural", Value = "Zona rural" });

            return listSelectListItems;
        }

        public List<SelectListItem> lst_tipificacion()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            listSelectListItems.Add(new SelectListItem { Text = "En plaza comercial", Value = "En plaza comercial" });
            listSelectListItems.Add(new SelectListItem { Text = "En calle", Value = "En calle" });
            listSelectListItems.Add(new SelectListItem { Text = "En edificio", Value = "En edificio" });
            listSelectListItems.Add(new SelectListItem { Text = "En strip mall", Value = "En strip mall" });
            return listSelectListItems;
        }

        public List<SelectListItem> lst_estacionamiento2()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            listSelectListItems.Add(new SelectListItem { Text = "Promedio", Value = "Promedio" });
            listSelectListItems.Add(new SelectListItem { Text = "Máximo", Value = "Máximo" });
            listSelectListItems.Add(new SelectListItem { Text = "Mínimo", Value = "Mínimo" });
            return listSelectListItems;
        }

        public List<SelectListItem> b_cg_baseGet()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_baseGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["id_b_cg_base"].ToString() });
            }
            return listSelectListItems;
        }
        public List<SelectListItem> b_cg_plazoGet()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_plazoGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["id_b_cg_plazo"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> b_cg_contrato_tiposGet()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_contrato_tiposGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["id_b_cg_contrato_tipo"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> b_cg_contrato_estatusGet()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_contrato_estatusGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["id_b_cg_contrato_estatu"].ToString() });
            }
            return listSelectListItems;
        }

        //public List<SelectListItem> fecha()
        //{
        //    List<SelectListItem> listSelectListItems = new List<SelectListItem>();
        //    listSelectListItems.Add(new SelectListItem { Text = "2023", Value = "2023" });
        //    return listSelectListItems;
        //}

        public List<SelectListItem> lst_mantenimiento()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Sin determinar", Value = "Sin determinar" });
            listSelectListItems.Add(new SelectListItem { Text = "Preventivo", Value = "Preventivo" });
            listSelectListItems.Add(new SelectListItem { Text = "Correctivo", Value = "Correctivo" });
            return listSelectListItems;
        }

        public List<SelectListItem> lst_aire_acondicionado2()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Funciona", Value = "Funciona" });
            listSelectListItems.Add(new SelectListItem { Text = "No Funciona", Value = "No Funciona" });
            return listSelectListItems;
        }

        public List<SelectListItem> lst_si_no_selecciona()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            listSelectListItems.Add(new SelectListItem { Text = "No", Value = "No" });
            listSelectListItems.Add(new SelectListItem { Text = "Si", Value = "Si" });
            return listSelectListItems;
        }

        public List<SelectListItem> lst_si_no()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "No", Value = "No" });
            listSelectListItems.Add(new SelectListItem { Text = "Si", Value = "Si" });
            return listSelectListItems;
        }

        public List<SelectListItem> lst_agua_toma()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            listSelectListItems.Add(new SelectListItem { Text = "No", Value = "No" });
            listSelectListItems.Add(new SelectListItem { Text = "Sin acceso", Value = "Sin acceso" });
            listSelectListItems.Add(new SelectListItem { Text = "Malo", Value = "Malo" });
            listSelectListItems.Add(new SelectListItem { Text = "Regular", Value = "Regular" });
            listSelectListItems.Add(new SelectListItem { Text = "Bueno", Value = "Bueno" });
            //listSelectListItems.Add(new SelectListItem { Text = "En sitio", Value = "En sitio" });
            listSelectListItems.Add(new SelectListItem { Text = "No se encontró", Value = "No se encontró" });

            return listSelectListItems;
        }
        public List<SelectListItem> lst_medidor()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            listSelectListItems.Add(new SelectListItem { Text = "No", Value = "No" });
            listSelectListItems.Add(new SelectListItem { Text = "Sin acceso", Value = "Sin acceso" });
            listSelectListItems.Add(new SelectListItem { Text = "Malo", Value = "Malo" });
            listSelectListItems.Add(new SelectListItem { Text = "Regular", Value = "Regular" });
            listSelectListItems.Add(new SelectListItem { Text = "Bueno", Value = "Bueno" });
            //listSelectListItems.Add(new SelectListItem { Text = "En sitio", Value = "En sitio" });
            listSelectListItems.Add(new SelectListItem { Text = "No se encontró", Value = "No se encontró" });


            return listSelectListItems;
        }

        public List<SelectListItem> lst_impermeabilizacion()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            //listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });

            listSelectListItems.Add(new SelectListItem { Text = "No", Value = "No" });
            listSelectListItems.Add(new SelectListItem { Text = "Sin acceso", Value = "Sin acceso" });
            listSelectListItems.Add(new SelectListItem { Text = "Malo", Value = "Malo" });
            listSelectListItems.Add(new SelectListItem { Text = "Regular", Value = "Regular" });
            listSelectListItems.Add(new SelectListItem { Text = "Bueno", Value = "Bueno" });
            listSelectListItems.Add(new SelectListItem { Text = "N/A", Value = "N/A" });


            return listSelectListItems;
        }

        public List<SelectListItem> lst_planta_emergencia()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            //listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });

            listSelectListItems.Add(new SelectListItem { Text = "No", Value = "No" });
            listSelectListItems.Add(new SelectListItem { Text = "Sin acceso", Value = "Sin acceso" });
            listSelectListItems.Add(new SelectListItem { Text = "Malo", Value = "Malo" });
            listSelectListItems.Add(new SelectListItem { Text = "Regular", Value = "Regular" });
            listSelectListItems.Add(new SelectListItem { Text = "Bueno", Value = "Bueno" });
            listSelectListItems.Add(new SelectListItem { Text = "N/A", Value = "N/A" });


            return listSelectListItems;
        }

        public List<SelectListItem> noBuenoRegularMalo2()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            //listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });

            listSelectListItems.Add(new SelectListItem { Text = "No", Value = "No" });
            listSelectListItems.Add(new SelectListItem { Text = "Sin acceso", Value = "Sin acceso" });
            listSelectListItems.Add(new SelectListItem { Text = "Malo", Value = "Malo" });
            listSelectListItems.Add(new SelectListItem { Text = "Regular", Value = "Regular" });
            listSelectListItems.Add(new SelectListItem { Text = "Bueno", Value = "Bueno" });
            listSelectListItems.Add(new SelectListItem { Text = "N/A", Value = "N/A" });


            return listSelectListItems;
        }


        public List<SelectListItem> b_cg_variacionInmuebleGet()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_variacionInmuebleGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["id_b_variacion_inmueble"].ToString() });
            }
            return listSelectListItems;

        }

        public List<SelectListItem> b_cg_estatus_visita_inmueble(int rol)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_visitas_estatusGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataTable.Rows)
            {
                if (int.Parse(item["id_b_inmuebles_visitas_estatus"].ToString()) != 2 || rol == 1 || rol == 52)
                    listSelectListItems.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["id_b_inmuebles_visitas_estatus"].ToString() });
            }
            return listSelectListItems;

        }

        public List<SelectListItem> b_cg_categoriasGet()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_categoriasGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["id_b_cg_categorias"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> b_cg_conservacionGet()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_conservacionGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["id_b_cg_conservacion"].ToString() });
            }
            return listSelectListItems;
        }



        public List<SelectListItem> b_cg_regimenGet()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_regimenGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["regimen"].ToString(), Value = item["id_b_cg_regimen"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> b_cg_inmuebles_estatusGet()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_inmuebles_estatusGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["descripcion_estatus"].ToString(), Value = item["id_b_cg_inmuebles_estatus"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> b_cg_subclasificacionGet()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_subclasificacionGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una subclasificación", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["id_b_cg_subclasificacion"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> b_cg_clasificacionGet()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_clasificacionGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una clasificación", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["id_b_cg_clasificacion"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> b_cg_regionGet()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_regionGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["nombre"].ToString(), Value = item["id_b_cg_region"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> b_cg_valor_mercado()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("b_cg_valor_mercado", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["id_b_cg_valor_mercado"].ToString() });
            }
            return listSelectListItems;
        }


    }
}
