using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebColliersCore.Data;
using WebColliersCore.Models;

namespace WebColliersCore.Controllers
{
    public class UsuariosController : Controller
    {
                // GET: Usuarios
        public ActionResult Index()
        {
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if(rol!="1" )
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }

            DataUsuarios dataUsuarios = new DataUsuarios();
            List<Usuario> listUsuarios = dataUsuarios.recuperaUsuariosActivos();
            return View(listUsuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return Redirect("~/Usuarios");
            }
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "1" )
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }

            Usuario usuario = new Usuario();

            DataUsuarios dataUsuarios = new DataUsuarios();
            usuario = dataUsuarios.recuperaUsuario(id);
            usuario.CancelarCFDIBool = usuario.CancelarCFDI > 0 ? true : false;

            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "1")
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }
            DataSelectListItem dataSelectListItem = new DataSelectListItem();
            ViewBag.Rol = dataSelectListItem.Rol;
            ViewBag.Niveles = dataSelectListItem.Niveles;
            ViewBag.Puesto = dataSelectListItem.ActividadPuesto(); 
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario, IFormCollection collection)
        {
            try
            {
                Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "1")
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }

                DataUsuarios dataUsuarios = new DataUsuarios();

                if (!TryValidateModel(usuario) || dataUsuarios.ValidaUsuario(usuario.Username))
                {
                    DataSelectListItem dataSelectListItem = new DataSelectListItem();
                    ViewBag.Rol = dataSelectListItem.Rol;
                    ViewBag.Niveles = dataSelectListItem.Niveles;
                    ViewBag.Puesto = dataSelectListItem.ActividadPuesto();
                    return View();
                }
                if (usuario.CancelarCFDIBool)
                    usuario.CancelarCFDI = 1;
                else
                    usuario.CancelarCFDI = 0;
                if (usuario.Apellido2 == null)
                    usuario.Apellido2 = "";
               

                dataUsuarios.UsuarioInsert(usuario);

                return Redirect("~/Usuarios");

            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return Redirect("~/Usuarios");
            }
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "1")
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }

            DataSelectListItem dataSelectListItem = new DataSelectListItem();
            ViewBag.Rol = dataSelectListItem.Rol;
            ViewBag.Niveles = dataSelectListItem.Niveles;
            ViewBag.Puesto = dataSelectListItem.ActividadPuesto();
            Usuario usuario = new Usuario();

            DataUsuarios dataUsuarios = new DataUsuarios();
            usuario = dataUsuarios.recuperaUsuario(id);
            usuario.CancelarCFDIBool = usuario.CancelarCFDI > 0 ? true : false;

            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario, int id, IFormCollection collection)
        {
            try
            {
                if (id == 0)
                {
                    return Redirect("~/Usuarios");
                }
                Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
                if (strCookies != null)
                {
                    var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                    string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                    if (rol != "1")
                        return Redirect("~/Home");
                }
                else
                {
                    return Redirect("~/Home");
                }

                if (!TryValidateModel(usuario))
                {
                    DataSelectListItem dataSelectListItem = new DataSelectListItem();
                    ViewBag.Rol = dataSelectListItem.Rol;
                    ViewBag.Niveles = dataSelectListItem.Niveles;
                    ViewBag.Puesto = dataSelectListItem.ActividadPuesto();
                    return View();
                }

                usuario.IdUsuario = id;

                DataUsuarios dataUsuarios = new DataUsuarios();
                if (dataUsuarios.ValidaUsuario(usuario.Username, usuario.IdUsuario))
                {
                    ViewBag.MuestraError = true;
                    DataSelectListItem dataSelectListItem = new DataSelectListItem();
                    ViewBag.Rol = dataSelectListItem.Rol;
                    ViewBag.Niveles = dataSelectListItem.Niveles;
                    ViewBag.Puesto = dataSelectListItem.ActividadPuesto();
                    return View();
                }

                if (usuario.CancelarCFDIBool)
                    usuario.CancelarCFDI = 1;
                else
                    usuario.CancelarCFDI = 0;

                if (usuario.Apellido2 == null)
                    usuario.Apellido2 = "";

                dataUsuarios.UsuarioUpdate(usuario);

                return Redirect("~/Usuarios");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return Redirect("~/Usuarios");
            }
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "1")
                    return Redirect("~/Home");
            }
            else
            { 
            return Redirect("~/Home");
            }

        Usuario usuario = new Usuario();
            DataUsuarios dataUsuarios = new DataUsuarios();
            usuario = dataUsuarios.recuperaUsuario(id);
            usuario.CancelarCFDIBool = usuario.CancelarCFDI > 0 ? true : false;

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Usuario usuario, int id, IFormCollection collection)
        {
            try
            {
                if (id == 0)
                {
                    return Redirect("~/Usuarios");
                }
                Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
                if (strCookies != null)
                {
                    var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                    string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                    if (rol != "1")
                        return Redirect("~/Home");
                }
                else
                { 
                return Redirect("~/Home");
                }

                usuario.IdUsuario = id;
                DataUsuarios dataUsuarios = new DataUsuarios();
                dataUsuarios.UsuarioDelete(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

      

    }
}