using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebColliersCore.Data;
using WebColliersCore.Models;

namespace WebColliersCore.Controllers
{
    public class PuestoController : Controller
    {
        // GET: Puesto
        public ActionResult Index()
        {
             Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol= LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "2" && rol != "1")
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }

            DataPuesto dataPuesto = new DataPuesto();
            List<DtActividadPuesto> dtActividadPuesto= dataPuesto.Get(0);
            return View(dtActividadPuesto);
        }

        // GET: Puesto/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return Redirect("~/Permisos");
            }
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "2" && rol != "1")
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }

            DataPuesto dataPuesto = new DataPuesto();
            DtActividadPuesto dtActividadPuesto = dataPuesto.Get(id).FirstOrDefault();
            return View(dtActividadPuesto);
        }

        // GET: Puesto/Create
        public ActionResult Create()
        {
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "2" && rol != "1")
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }

            return View();
        }

        // POST: Puesto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, DtActividadPuesto dtActividadPuesto)
        {
            try
            {
                Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
                if (strCookies != null)
                {
                    var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                    string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                    if (rol != "2" && rol != "1")
                        return Redirect("~/Home");
                }
                else
                {
                    return Redirect("~/Home");
                }

                DataPuesto dataPuesto = new DataPuesto();
                dataPuesto.Insert(dtActividadPuesto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Puesto/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return Redirect("~/Permisos");
            }
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "2" && rol != "1")
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }

            DataPuesto dataPuesto = new DataPuesto();
            DtActividadPuesto dtActividadPuesto=dataPuesto.Get(id).FirstOrDefault();
            return View(dtActividadPuesto);
        }

        // POST: Puesto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DtActividadPuesto dtActividadPuesto)
        {
            try
            {
                if (id == 0)
                {
                    return Redirect("~/Permisos");
                }
                Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
                if (strCookies != null)
                {
                    var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                    string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                    if (rol != "2" && rol != "1")
                        return Redirect("~/Home");
                }
                else
                {
                    return Redirect("~/Home");
                }

                DataPuesto dataPuesto = new DataPuesto();
                dtActividadPuesto.idactividad_puesto=id;
                dataPuesto.Update(dtActividadPuesto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Puesto/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return Redirect("~/Permisos");
            }
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "2" && rol != "1")
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }

            DataPuesto dataPuesto = new DataPuesto();
            DtActividadPuesto dtActividadPuesto = dataPuesto.Get(id).FirstOrDefault();
            return View(dtActividadPuesto);
        }

        // POST: Puesto/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DtActividadPuesto dtActividadPuesto)
        {
            try
            {
                if (id == 0)
                {
                    return Redirect("~/Permisos");
                }
                Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
                if (strCookies != null)
                {
                    var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                    string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                    if (rol != "2" && rol != "1")
                        return Redirect("~/Home");
                }
                else
                {
                    return Redirect("~/Home");
                }

                DataPuesto dataPuesto = new DataPuesto();
                dataPuesto.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
             
    }
}