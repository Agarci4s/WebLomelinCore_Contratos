using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;

namespace WebColliersCore.Data
{
    public class DataCatalogos
    {
        public bool Agregar { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
        public int Cartera { get; set; }

        //public void SetPermisos(int id)
        //{
        //    try
        //    {
        //        List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
        //        listSqlParameters.Add(new MySqlParameter("IdIn", id));
        //        DataTable dataTable = new Conexion().RunStoredProcedure("usuariosGetNivelAndCartera", listSqlParameters);
        //        if (dataTable.Rows.Count > 0)
        //        {
        //            switch (int.Parse(dataTable.Rows[0]["Nivel"].ToString()))
        //            {
        //                case 1:
        //                    Agregar = false;
        //                    Editar = false;
        //                    Eliminar = false;
        //                    break;
        //                case 2:
        //                    Agregar = false;
        //                    Editar = true;
        //                    Eliminar = false;
        //                    break;
        //                case 3:
        //                    Agregar = true;
        //                    Editar = true;
        //                    Eliminar = true;
        //                    break;
        //            }

        //            //Cartera = int.Parse(dataTable.Rows[0]["IdCartera"].ToString());
        //        }
        //        else
        //        {
        //            Agregar = false;
        //            Editar = false;
        //            Eliminar = false;
        //            Cartera = -1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void SetPermisos(int IdUsuario, MethodBase currentMethod, int idCartera)
        {
            try
            {
                Agregar = false;
                Editar = false;
                Eliminar = false;
                Cartera = idCartera;

                List<Transf_Opciones> listTransf_Opciones = new List<Transf_Opciones>();
                DataTransf_Opciones transf_Opciones = new DataTransf_Opciones();
                listTransf_Opciones = transf_Opciones.RecuperaTransf_Opciones_Controller(IdUsuario, currentMethod.DeclaringType.Name.Replace("Controller", ""));

                if (listTransf_Opciones == null || listTransf_Opciones.Count == 0)
                {
                    return;
                }


                switch (listTransf_Opciones[0].Nivel)
                {
                    case 0:
                        Agregar = true;
                        Editar = true;
                        Eliminar = true;
                        break;
                    case 1:
                        Agregar = false;
                        Editar = false;
                        Eliminar = false;
                        break;
                    case 2:
                        Agregar = false;
                        Editar = true;
                        Eliminar = false;
                        break;
                    case 3:
                        Agregar = true;
                        Editar = true;
                        Eliminar = true;
                        break;
                }
            }
            catch
            {

            }
        }
    }
}
