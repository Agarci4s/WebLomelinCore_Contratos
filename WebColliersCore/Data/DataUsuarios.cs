using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;

namespace WebColliersCore.Data
{
    public class DataUsuarios
    {
        private Conexion conexion = new Conexion();       

        public bool ValidaUsuario(String username)
        {
              
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("Username", username));

            DataTable dataTable = conexion.RunStoredProcedure("usuariosContratosValida", listSqlParameters);
            if (dataTable.Rows.Count > 0)
                return true;
            return false;
        }

        public bool ValidaUsuario(String username, String password)
        {
              
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("Username", username));
            listSqlParameters.Add(new MySqlParameter("Password", password));

            DataTable dataTable = conexion.RunStoredProcedure("usuariosContratosValida2", listSqlParameters);
            if (dataTable.Rows.Count > 0)
                return true;
            return false;
        }

        public bool ValidaUsuario(String username, int IdUsuario)
        {
              
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("Username", username));
            listSqlParameters.Add(new MySqlParameter("IdUsuario", IdUsuario));

            DataTable dataTable = conexion.RunStoredProcedure("usuariosContratosValida3", listSqlParameters);
            if (dataTable.Rows.Count > 0)
                return true;
            return false;
        }

        public Usuario RecuperaUsuario(String username, String password)
        {
              
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("Username", username));
            listSqlParameters.Add(new MySqlParameter("Password", password));

            DataTable dataTable = conexion.RunStoredProcedure("usuariosContratosValida2", listSqlParameters);
            if (dataTable.Rows.Count == 1)
                return DataToModel(dataTable)[0];
            return new Usuario();
        }
        public Usuario RecuperaUsuario(string strCookies)
        {
            var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
            string username = legacyCookie["userName"];
            string password = LegacyCookieExtensions.Decrypt(legacyCookie["userName2"], SystemComplementos.Key);

              
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("Username", username));
            listSqlParameters.Add(new MySqlParameter("Password", password));

            DataTable dataTable = conexion.RunStoredProcedure("usuariosContratosValida2", listSqlParameters);
            if (dataTable.Rows.Count == 1)
                return DataToModel(dataTable)[0];
            return new Usuario();
        }

        public TpCartera RecuperaCartera(string strCookies)
        {
            try
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string cartera = LegacyCookieExtensions.Decrypt(legacyCookie["cartera"], SystemComplementos.Key);
                DataTpCartera dataTpCartera = new DataTpCartera();
                if (int.Parse(cartera)==0)
                {
                    return new TpCartera { }   ;
                }
                else
                {
                return dataTpCartera.recuperaTpCartera(int.Parse(cartera));
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public int UsuarioRecuperaId(String username, String password)
        {
              
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("Username", username));
            listSqlParameters.Add(new MySqlParameter("Password", password));

            DataTable dataTable = conexion.RunStoredProcedure("usuariosContratosValida2", listSqlParameters);
            if (dataTable.Rows.Count == 1)
                return (int)dataTable.Rows[0]["IdUsuario"];
            return 0;
        }

        public List<Usuario> recuperaUsuariosActivos()
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("usuariosContratosGet", listSqlParameters);
            return DataToModel(dataTable);
        }

        public Usuario recuperaUsuario(int IdUsuario)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdUsuario", IdUsuario));
            DataTable dataTable = conexion.RunStoredProcedure("usuarioContratosGetById", listSqlParameters);
            if (dataTable.Rows.Count > 0)
                return DataToModel(dataTable).First();
            else
                return new Usuario();
        }

        public int UsuarioInsert(Usuario usuario)
        {
            List<MySqlParameter> listSqlParametersIn = new List<MySqlParameter>();
            List<MySqlParameter> listSqlParametersOut = new List<MySqlParameter>();

            listSqlParametersIn.Add(new MySqlParameter("Username", usuario.Username));
            listSqlParametersIn.Add(new MySqlParameter("Nombre", usuario.Nombre));
            listSqlParametersIn.Add(new MySqlParameter("Apellido1", usuario.Apellido1));
            listSqlParametersIn.Add(new MySqlParameter("Apellido2", usuario.Apellido2));
            listSqlParametersIn.Add(new MySqlParameter("Password", usuario.Password));
            listSqlParametersIn.Add(new MySqlParameter("Nivel", usuario.Nivel));
            listSqlParametersIn.Add(new MySqlParameter("Puesto", usuario.Puesto));
            listSqlParametersIn.Add(new MySqlParameter("Telefono", usuario.Telefono));
            listSqlParametersIn.Add(new MySqlParameter("Email", usuario.Email));
            listSqlParametersIn.Add(new MySqlParameter("CancelarCFDI", usuario.CancelarCFDI));
            listSqlParametersIn.Add(new MySqlParameter("Rol", usuario.Rol));
            listSqlParametersIn.Add(new MySqlParameter("clave_ejecutivo", usuario.clave_ejecutivo));
            listSqlParametersOut.Add(new MySqlParameter("IdUsuario", usuario.IdUsuario));


            MySqlParameterCollection mySqlParameterCollection = conexion.RunStoredProcedure("usuariosInsert", listSqlParametersIn, listSqlParametersOut);

            return (int)mySqlParameterCollection["IdUsuario"].Value;
        }

        public bool UsuarioUpdate(Usuario usuario)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            listSqlParameters.Add(new MySqlParameter("IdUsuario", usuario.IdUsuario));
            listSqlParameters.Add(new MySqlParameter("Username", usuario.Username));
            listSqlParameters.Add(new MySqlParameter("Nombre", usuario.Nombre));
            listSqlParameters.Add(new MySqlParameter("Apellido1", usuario.Apellido1));
            listSqlParameters.Add(new MySqlParameter("Apellido2", usuario.Apellido2));
            listSqlParameters.Add(new MySqlParameter("Password", usuario.Password));
            listSqlParameters.Add(new MySqlParameter("Nivel", usuario.Nivel));
            listSqlParameters.Add(new MySqlParameter("Puesto", usuario.Puesto));
            listSqlParameters.Add(new MySqlParameter("Telefono", usuario.Telefono));
            listSqlParameters.Add(new MySqlParameter("Email", usuario.Email));
            listSqlParameters.Add(new MySqlParameter("CancelarCFDI", usuario.CancelarCFDI));
            listSqlParameters.Add(new MySqlParameter("Rol", usuario.Rol));
            listSqlParameters.Add(new MySqlParameter("clave_ejecutivo", usuario.clave_ejecutivo));


            DataTable dataTable = conexion.RunStoredProcedure("usuariosUpdate", listSqlParameters);
            return true;
        }

        public bool UsuarioUpdateStatus(Usuario usuario)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            listSqlParameters.Add(new MySqlParameter("IdUsuario", usuario.IdUsuario));
            listSqlParameters.Add(new MySqlParameter("Status", usuario.Status));
            DataTable dataTable = conexion.RunStoredProcedure("usuariosUpdateStatus_Contratos", listSqlParameters);
            return true;
        }

        public bool UsuarioDelete(Usuario usuario)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            listSqlParameters.Add(new MySqlParameter("IdUsuario", usuario.IdUsuario));
            DataTable dataTable = conexion.RunStoredProcedure("usuariosDeleteLog", listSqlParameters);
            return true;
        }

        private List<Usuario> DataToModel(DataTable dataTable)
        {
            List<Usuario> listUsuarios = new List<Usuario>();
            foreach (DataRow item in dataTable.Rows)
            {
                Usuario usuario = new Usuario();
                usuario.Apellido1 = item["Apellido1"].ToString();
                usuario.Apellido2 = item["Apellido2"].ToString();
                usuario.CancelarCFDI = Int32.Parse(item["CancelarCFDI"].ToString());
                usuario.DateCreate = (DateTime)item["DateCreate"];
                usuario.DateEdit = (DateTime)item["DateEdit"];
                usuario.Email = item["Email"].ToString();
                usuario.IdUsuario = Convert.ToInt32(item["IdUsuario"].ToString());
                usuario.Nivel = Int32.Parse(item["Nivel"].ToString());
                usuario.Nombre = item["Nombre"].ToString();
                usuario.Password = item["Password"].ToString();
                usuario.Puesto = item["Puesto"].ToString();
                usuario.Status = Int32.Parse(item["Status"].ToString());
                usuario.Telefono = item["Telefono"].ToString();
                usuario.Username = item["Username"].ToString();
                usuario.Rol = item["Rol"].ToString();
                usuario.clave_ejecutivo = item["clave_ejecutivo"].ToString();
                usuario.StatusContratos = Int32.Parse(item["StatusContratos"].ToString());

                listUsuarios.Add(usuario);
            }
            return listUsuarios;
        }
    }
}
