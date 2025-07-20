using Microsoft.AspNetCore.Http;
using NuGet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using WebColliersCore.Data;
using WebColliersCore.Models;

namespace WebColliersCore
{
    public class Menu
    {
        public List<Transf_Opciones> CreaMenu(string strCookies)
        {
            List<Transf_Opciones> listTransf_Opciones = new List<Transf_Opciones>();

            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                DataUsuarios dataUsuarios = new DataUsuarios();
                var user = legacyCookie["userName"];
                var pass = legacyCookie["userName2"];
                int idUsuario = dataUsuarios.UsuarioRecuperaId(user, LegacyCookieExtensions.Decrypt(pass, SystemComplementos.Key));
                DataTransf_Opciones transf_Opciones = new DataTransf_Opciones();
                listTransf_Opciones = transf_Opciones.RecuperaTransf_Opciones(idUsuario);
            }
            return listTransf_Opciones.FindAll(x => x.checkTransf_Opciones == true);
        }

        public string RecuperaNombreUsuario(string strCookies)
        {
            string name = "";

            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                name = legacyCookie["name"];
            }
            return name;
        }

        public string RecuperaRol(string strCookies)
        {
            string rol = "";

            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
            }
            return rol;
        }
        //public string RecuperaNivel(string strCookies, MethodBase currentMethod)
        //{
        //    string nivel = "";

        //    if (strCookies != null)
        //    {
        //        var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
        //        nivel = LegacyCookieExtensions.Decrypt(legacyCookie["nivel"], SystemComplementos.Key);
        //    }
        //    return nivel;



        //    /////
        //    //var legacyCookie2 = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
        //    //string usuarioIdStr = LegacyCookieExtensions.Decrypt(legacyCookie2["id"], SystemComplementos.Key);
        //    //int usuarioId = int.Parse(usuarioIdStr);
        //    //List<Transf_Opciones> listTransf_Opciones = new List<Transf_Opciones>();
        //    //DataTransf_Opciones transf_Opciones = new DataTransf_Opciones();
        //    //listTransf_Opciones = transf_Opciones.RecuperaTransf_Opciones_Controller(usuarioId, currentMethod.DeclaringType.Name.Replace("Controller", ""));
        //    //if (listTransf_Opciones == null || listTransf_Opciones.Count == 0)
        //    //{
        //    //    return "0";
        //    //}
        //    //return listTransf_Opciones[0].Nivel.ToString();
        //}

        public string RecuperaNivel(string strCookies, string currentMethod)
        {
            string nivel = "";
            int usuarioId;

            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string usuarioIdStr = LegacyCookieExtensions.Decrypt(legacyCookie["id"], SystemComplementos.Key);
                usuarioId = int.Parse(usuarioIdStr);
            }
            else
            {
                return nivel;
            }

            List<Transf_Opciones> listTransf_Opciones = new List<Transf_Opciones>();
            DataTransf_Opciones transf_Opciones = new DataTransf_Opciones();
            listTransf_Opciones = transf_Opciones.RecuperaTransf_Opciones_Controller(usuarioId, currentMethod.Replace("Controller", ""));

            if (listTransf_Opciones == null || listTransf_Opciones.Count == 0)
            {
                return nivel;
            }

            if (listTransf_Opciones[0].Nivel == 0)
            {
                return "3";
            }

            return listTransf_Opciones[0].Nivel.ToString();
        }

        public string RecuperaCarteraNombre(string strCookies)
        {
            string cartera = "Cartera";

            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                cartera = legacyCookie["carteraN"];
            }
            return cartera;
        }


        public void AcualizaStatusUsuario(string strCookies)
        {
            try
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string username = legacyCookie["userName"];
                string password = LegacyCookieExtensions.Decrypt(legacyCookie["userName2"], SystemComplementos.Key);
                DataUsuarios dataUsuarios = new DataUsuarios();
                Usuario usuario = dataUsuarios.RecuperaUsuario(username, password);
                usuario.Status = int.Parse(DateTime.Now.AddMinutes(SystemComplementos.tiempoSesion).ToString("yyMMddHHmm"));
                dataUsuarios.UsuarioUpdateStatus(usuario);
            }
            catch
            {
            }

        }

        //public bool ValidaPermiso(MethodBase currentMethod, ref int usuarioId, ref int carteraId, string strCookies, string strCookies2, ref int tipoNivel)
        //{
        //    if (strCookies != null)
        //    {
        //        var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
        //        string usuarioIdStr = LegacyCookieExtensions.Decrypt(legacyCookie["id"], SystemComplementos.Key);
        //        string username = legacyCookie["userName"];


        //        legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies2);
        //        string carteraIdStr = LegacyCookieExtensions.Decrypt(legacyCookie["cartera"], SystemComplementos.Key);
        //        usuarioId = int.Parse(usuarioIdStr);
        //        carteraId = int.Parse(carteraIdStr);
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if (carteraId <= 0)
        //    {
        //        return false;
        //    }



        //    List<Transf_Opciones> listTransf_Opciones = new List<Transf_Opciones>();
        //    DataTransf_Opciones transf_Opciones = new DataTransf_Opciones();
        //    listTransf_Opciones = transf_Opciones.RecuperaTransf_Opciones_Controller(usuarioId, currentMethod.DeclaringType.Name.Replace("Controller", ""));

        //    if (listTransf_Opciones == null || listTransf_Opciones.Count == 0)
        //    {
        //        return false;
        //    }

        //    tipoNivel = listTransf_Opciones[0].Nivel;
        //    if (listTransf_Opciones[0].TipoVista >= 1 && listTransf_Opciones[0].Nivel < tipoNivel)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //public bool ValidaPermiso(MethodBase currentMethod, ref int usuarioId, ref int carteraId, string strCookies, string strCookies2, int tipoNivel)
        //{
        //    if (strCookies != null)
        //    {
        //        var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
        //        string usuarioIdStr = LegacyCookieExtensions.Decrypt(legacyCookie["id"], SystemComplementos.Key);
        //        string username = legacyCookie["userName"];


        //        legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies2);
        //        string carteraIdStr = LegacyCookieExtensions.Decrypt(legacyCookie["cartera"], SystemComplementos.Key);
        //        usuarioId = int.Parse(usuarioIdStr);
        //        carteraId = int.Parse(carteraIdStr);
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if (carteraId <= 0)
        //    {
        //        return false;
        //    }



        //    List<Transf_Opciones> listTransf_Opciones = new List<Transf_Opciones>();
        //    DataTransf_Opciones transf_Opciones = new DataTransf_Opciones();
        //    listTransf_Opciones = transf_Opciones.RecuperaTransf_Opciones_Controller(usuarioId, currentMethod.DeclaringType.Name.Replace("Controller", ""));

        //    if (listTransf_Opciones == null || listTransf_Opciones.Count == 0)
        //    {
        //        return false;
        //    }

        //    tipoNivel = listTransf_Opciones[0].Nivel;
        //    if (listTransf_Opciones[0].TipoVista >= 1 && listTransf_Opciones[0].Nivel < tipoNivel)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        public bool ValidaPermiso(MethodBase currentMethod, ref int usuarioId, ref int carteraId,ref int tipoNivel, IEnumerable<Claim> claims)
        {
            if (claims.FirstOrDefault() != null)
            {
                var IdUsuarioStr = claims.FirstOrDefault(c => c.Type == "IdUsuario").Value;
                var IdCarteraStr = claims.FirstOrDefault(c => c.Type == "Cartera").Value;


                usuarioId = 0;
                int.TryParse(IdUsuarioStr, out usuarioId);
                carteraId = 0;
                int.TryParse(IdCarteraStr, out carteraId);
                //tipoNivel = 0;
                //int.TryParse(IdNiveloStr, out tipoNivel);
            }
            else
            {
                return false;
            }

            if (carteraId <= 0)
            {
                return false;
            }

            List<Transf_Opciones> listTransf_Opciones = new List<Transf_Opciones>();
            DataTransf_Opciones transf_Opciones = new DataTransf_Opciones();
            listTransf_Opciones = transf_Opciones.RecuperaTransf_Opciones_Controller(usuarioId, currentMethod.DeclaringType.Name.Replace("Controller", ""));

            if (listTransf_Opciones == null || listTransf_Opciones.Count == 0)
            {
                return false;
            }

            if (listTransf_Opciones[0].TipoVista >= 1 && listTransf_Opciones[0].Nivel < tipoNivel)
            {
                return false;
            }

            tipoNivel = listTransf_Opciones[0].Nivel;

            return true;
        }

        public bool ValidaPermisoSinlistTransf_Opciones(MethodBase currentMethod, ref int usuarioId, ref int carteraId, ref int tipoNivel, IEnumerable<Claim> claims)
        {
            if (claims.FirstOrDefault() != null)
            {
                var IdUsuarioStr = claims.FirstOrDefault(c => c.Type == "IdUsuario").Value;
                var IdCarteraStr = claims.FirstOrDefault(c => c.Type == "Cartera").Value;


                usuarioId = 0;
                int.TryParse(IdUsuarioStr, out usuarioId);
                carteraId = 0;
                int.TryParse(IdCarteraStr, out carteraId);
                //tipoNivel = 0;
                //int.TryParse(IdNiveloStr, out tipoNivel);
            }
            else
            {
                return false;
            }

            if (carteraId <= 0)
            {
                return false;
            }


            return true;
        }

        //public bool ValidaPermiso(MethodBase currentMethod, ref int usuarioId, ref int carteraId, ref string username, string strCookies, string strCookies2, int tipoNivel)
        //{
        //    if (strCookies != null)
        //    {
        //        var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
        //        string usuarioIdStr = LegacyCookieExtensions.Decrypt(legacyCookie["id"], SystemComplementos.Key);
        //        username = legacyCookie["userName"];


        //        legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies2);
        //        string carteraIdStr = LegacyCookieExtensions.Decrypt(legacyCookie["cartera"], SystemComplementos.Key);
        //        usuarioId = int.Parse(usuarioIdStr);
        //        carteraId = int.Parse(carteraIdStr);
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    List<Transf_Opciones> listTransf_Opciones = new List<Transf_Opciones>();
        //    DataTransf_Opciones transf_Opciones = new DataTransf_Opciones();
        //    listTransf_Opciones = transf_Opciones.RecuperaTransf_Opciones_Controller(usuarioId, currentMethod.DeclaringType.Name.Replace("Controller", ""));

        //    if (listTransf_Opciones == null || listTransf_Opciones.Count == 0)
        //    {
        //        return false;
        //    }

        //    if (listTransf_Opciones[0].TipoVista >= 1 && listTransf_Opciones[0].Nivel < tipoNivel)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //public bool ValidaPermiso(string controllerName, ref int usuarioId, ref int carteraId, string strCookies, string strCookies2, int tipoNivel)
        //{
        //    if (strCookies != null)
        //    {
        //        var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
        //        string usuarioIdStr = LegacyCookieExtensions.Decrypt(legacyCookie["id"], SystemComplementos.Key);
        //        string username = legacyCookie["userName"];


        //        legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies2);
        //        string carteraIdStr = LegacyCookieExtensions.Decrypt(legacyCookie["cartera"], SystemComplementos.Key);
        //        usuarioId = int.Parse(usuarioIdStr);
        //        carteraId = int.Parse(carteraIdStr);
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    List<Transf_Opciones> listTransf_Opciones = new List<Transf_Opciones>();
        //    DataTransf_Opciones transf_Opciones = new DataTransf_Opciones();
        //    listTransf_Opciones = transf_Opciones.RecuperaTransf_Opciones_Controller(usuarioId, controllerName.Replace("Controller", ""));

        //    if (listTransf_Opciones == null || listTransf_Opciones.Count == 0)
        //    {
        //        return false;
        //    }

        //    if (listTransf_Opciones[0].TipoVista >= 1 && listTransf_Opciones[0].Nivel < tipoNivel)
        //    {
        //        return false;
        //    }
        //    return true;
        //}


    }
}
