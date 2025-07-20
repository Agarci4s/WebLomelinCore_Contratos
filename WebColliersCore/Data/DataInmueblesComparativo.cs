using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;

namespace WebLomelinCore.Data
{
    public class DataInmueblesComparativo
    {
        private Conexion conexion = new Conexion();

        public List<B_inmuebles_comparativo> DATOS_SIN_IMAGENES()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("DATOS_SIN_IMAGENES", listSqlParameters);
            return DataToModel(dataTable);
        }

        public List<B_inmuebles_comparativo> DATOS_SIN_IMAGENES(int id_b_inmuebles_comparativoIn, string fotoIn)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_comparativoIn", id_b_inmuebles_comparativoIn));
            listSqlParameters.Add(new MySqlParameter("fotoIn", fotoIn));
            DataTable dataTable = conexion.RunStoredProcedure("DATOS_SIN_IMAGENES2", listSqlParameters);
            return DataToModel(dataTable);
        }

        public void Insert(B_inmuebles_comparativo b_Inmuebles_Comparativo)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("id_b_inmueblesIN", b_Inmuebles_Comparativo.id_b_inmuebles));
            listSqlParameters.Add(new MySqlParameter("nombreIN", b_Inmuebles_Comparativo.nombre));
            listSqlParameters.Add(new MySqlParameter("direccionIN", b_Inmuebles_Comparativo.direccion));
            listSqlParameters.Add(new MySqlParameter("cpIN", b_Inmuebles_Comparativo.cp));
            listSqlParameters.Add(new MySqlParameter("estadoIN", b_Inmuebles_Comparativo.estado));
            listSqlParameters.Add(new MySqlParameter("municipioIN", b_Inmuebles_Comparativo.municipio));
            listSqlParameters.Add(new MySqlParameter("coloniaIN", b_Inmuebles_Comparativo.colonia));
            listSqlParameters.Add(new MySqlParameter("noextIN", b_Inmuebles_Comparativo.noext));
            listSqlParameters.Add(new MySqlParameter("manzanaIN", b_Inmuebles_Comparativo.manzana));
            listSqlParameters.Add(new MySqlParameter("loteIN", b_Inmuebles_Comparativo.lote));
            listSqlParameters.Add(new MySqlParameter("referencia_calleIN", b_Inmuebles_Comparativo.referencia_calle));
            listSqlParameters.Add(new MySqlParameter("referencia_calle2IN", b_Inmuebles_Comparativo.referencia_calle2));
            listSqlParameters.Add(new MySqlParameter("latidudIN", b_Inmuebles_Comparativo.latidud));
            listSqlParameters.Add(new MySqlParameter("longitudIN", b_Inmuebles_Comparativo.longitud));
            listSqlParameters.Add(new MySqlParameter("link_mapsIN", b_Inmuebles_Comparativo.link_maps));
            listSqlParameters.Add(new MySqlParameter("estacionamientoIN", b_Inmuebles_Comparativo.estacionamiento));
            listSqlParameters.Add(new MySqlParameter("m2_construccionIN", b_Inmuebles_Comparativo.m2_construccion));
            listSqlParameters.Add(new MySqlParameter("m2_precioIN", b_Inmuebles_Comparativo.m2_precio));
            listSqlParameters.Add(new MySqlParameter("rentaIN", b_Inmuebles_Comparativo.renta));
            listSqlParameters.Add(new MySqlParameter("fotoIN", b_Inmuebles_Comparativo.foto));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_regionIN", b_Inmuebles_Comparativo.id_b_cg_region));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_categoriasIN", b_Inmuebles_Comparativo.id_b_cg_categorias));

            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_comparativoInsert", listSqlParameters);

            return;
        }

        public void Edit(B_inmuebles_comparativo b_Inmuebles_Comparativo)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_comparativoIN", b_Inmuebles_Comparativo.id_b_inmuebles_comparativo));
            listSqlParameters.Add(new MySqlParameter("nombreIN", b_Inmuebles_Comparativo.nombre));
            listSqlParameters.Add(new MySqlParameter("direccionIN", b_Inmuebles_Comparativo.direccion));
            listSqlParameters.Add(new MySqlParameter("cpIN", b_Inmuebles_Comparativo.cp));
            listSqlParameters.Add(new MySqlParameter("estadoIN", b_Inmuebles_Comparativo.estado));
            listSqlParameters.Add(new MySqlParameter("municipioIN", b_Inmuebles_Comparativo.municipio));
            listSqlParameters.Add(new MySqlParameter("coloniaIN", b_Inmuebles_Comparativo.colonia));
            listSqlParameters.Add(new MySqlParameter("noextIN", b_Inmuebles_Comparativo.noext));
            listSqlParameters.Add(new MySqlParameter("manzanaIN", b_Inmuebles_Comparativo.manzana));
            listSqlParameters.Add(new MySqlParameter("loteIN", b_Inmuebles_Comparativo.lote));
            listSqlParameters.Add(new MySqlParameter("referencia_calleIN", b_Inmuebles_Comparativo.referencia_calle));
            listSqlParameters.Add(new MySqlParameter("referencia_calle2IN", b_Inmuebles_Comparativo.referencia_calle2));
            listSqlParameters.Add(new MySqlParameter("latidudIN", b_Inmuebles_Comparativo.latidud));
            listSqlParameters.Add(new MySqlParameter("longitudIN", b_Inmuebles_Comparativo.longitud));
            listSqlParameters.Add(new MySqlParameter("link_mapsIN", b_Inmuebles_Comparativo.link_maps));
            listSqlParameters.Add(new MySqlParameter("estacionamientoIN", b_Inmuebles_Comparativo.estacionamiento));
            listSqlParameters.Add(new MySqlParameter("m2_construccionIN", b_Inmuebles_Comparativo.m2_construccion));
            listSqlParameters.Add(new MySqlParameter("m2_precioIN", b_Inmuebles_Comparativo.m2_precio));
            listSqlParameters.Add(new MySqlParameter("rentaIN", b_Inmuebles_Comparativo.renta));
            listSqlParameters.Add(new MySqlParameter("fotoIN", b_Inmuebles_Comparativo.foto));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_regionIN", b_Inmuebles_Comparativo.id_b_cg_region));
            listSqlParameters.Add(new MySqlParameter("id_b_cg_categoriasIN", b_Inmuebles_Comparativo.id_b_cg_categorias));

            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_comparativoEdit", listSqlParameters);

            return;
        }

        public void Delete(B_inmuebles_comparativo b_Inmuebles_Comparativo)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_comparativoIN", b_Inmuebles_Comparativo.id_b_inmuebles_comparativo));
           
            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_comparativoDelete", listSqlParameters);

            return;
        }

        public List<B_inmuebles_comparativo> Get(int IdCartera, int IdUsuario, int idInmueble)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("idInmueble_In", idInmueble));


            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_comparativoGet", listSqlParameters);

            return DataToModel(dataTable);
        }

        public B_inmuebles_comparativo GetById(int IdCartera, int IdUsuario, int id_b_inmuebles_comparativo)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("id_b_inmuebles_comparativo_In", id_b_inmuebles_comparativo));

            DataTable dataTable = conexion.RunStoredProcedure("b_inmuebles_comparativoGetById", listSqlParameters);
            return DataToModel(dataTable).FirstOrDefault();
        }

        private List<B_inmuebles_comparativo> DataToModel(DataTable dataTable)
        {
            List<B_inmuebles_comparativo> b_Inmuebles_Comparativos = new List<B_inmuebles_comparativo>();
            foreach (DataRow item in dataTable.Rows)
            {
                try
                {
                    B_inmuebles_comparativo b_Inmuebles_Comparativo = new B_inmuebles_comparativo();

                    b_Inmuebles_Comparativo.id_b_inmuebles_comparativo = int.Parse(item["id_b_inmuebles_comparativo"].ToString());
                    b_Inmuebles_Comparativo.id_b_inmuebles = int.Parse(item["id_b_inmuebles"].ToString());
                    b_Inmuebles_Comparativo.nombre = item["nombre"].ToString();
                    b_Inmuebles_Comparativo.direccion = item["direccion"].ToString();
                    b_Inmuebles_Comparativo.cp = int.Parse(item["cp"].ToString());
                    b_Inmuebles_Comparativo.estado = item["estado"].ToString();
                    b_Inmuebles_Comparativo.municipio = item["municipio"].ToString();
                    b_Inmuebles_Comparativo.colonia = item["colonia"].ToString();
                    b_Inmuebles_Comparativo.noext = item["noext"].ToString();
                    b_Inmuebles_Comparativo.manzana = item["manzana"].ToString();
                    b_Inmuebles_Comparativo.lote = item["lote"].ToString();
                    b_Inmuebles_Comparativo.referencia_calle = item["referencia_calle"].ToString();
                    b_Inmuebles_Comparativo.referencia_calle2 = item["referencia_calle2"].ToString();
                    b_Inmuebles_Comparativo.latidud = float.Parse(item["latidud"].ToString());
                    b_Inmuebles_Comparativo.longitud = float.Parse(item["longitud"].ToString());
                    b_Inmuebles_Comparativo.link_maps = item["link_maps"].ToString();
                    b_Inmuebles_Comparativo.estacionamiento = Convert.ToBoolean(Convert.ToInt16(item["estacionamiento"].ToString()));
                    b_Inmuebles_Comparativo.m2_construccion = decimal.Parse(item["m2_construccion"].ToString());
                    b_Inmuebles_Comparativo.m2_precio = decimal.Parse(item["m2_precio"].ToString());
                    b_Inmuebles_Comparativo.renta = decimal.Parse(item["renta"].ToString());
                    b_Inmuebles_Comparativo.foto = item["foto"].ToString();
                    b_Inmuebles_Comparativo.id_b_cg_region = int.Parse(item["id_b_cg_region"].ToString());
                    b_Inmuebles_Comparativo.id_b_cg_categorias = int.Parse(item["id_b_cg_categorias"].ToString());

                    try
                    {
                        b_Inmuebles_Comparativo.region = item["region"].ToString();

                    }
                    catch (Exception)
                    {
                    }

                    b_Inmuebles_Comparativos.Add(b_Inmuebles_Comparativo);
                }
                catch (Exception e)
                {
                    throw;
                }



            }
            return b_Inmuebles_Comparativos;
        }
    }
}
