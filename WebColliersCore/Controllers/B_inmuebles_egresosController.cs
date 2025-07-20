using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebColliersCore;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebLomelinCore.Data;
using WebLomelinCore.Models;

namespace WebLomelinCore.Controllers
{
    public class B_inmuebles_egresosController : Controller
    {
        // GET: B_inmuebles_egresosController
        public ActionResult Index()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmuebles dataInmuebles = new();
            DataInmueblesEgresos dataInmueblesEgresos = new();
            ViewBag.Inmuebles = dataInmuebles.GetCmbInmuebles(idCartera, IdUsuario);
            ViewBag.PorcIVA = dataInmueblesEgresos.GetImpuestos("IVA");
            ViewBag.PorcRetISR = dataInmueblesEgresos.GetImpuestos("RET ISR");
            ViewBag.PorcRetIVA = dataInmueblesEgresos.GetImpuestosRetIVA(0);
            ViewBag.Egresos = dataInmueblesEgresos.GetEgresos();
            ViewBag.Monedas = dataInmueblesEgresos.GetMonedas();
            ViewBag.Contratos = dataInmueblesEgresos.GetContratos(-1);

            return View();
        }

        public ActionResult RentaEgresos()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmuebles dataInmuebles = new();
            DataInmueblesEgresos dataInmueblesEgresos = new();
            ViewBag.Inmuebles = dataInmuebles.GetCmbInmuebles(idCartera, IdUsuario);
            ViewBag.PorcIVA = dataInmueblesEgresos.GetImpuestos("IVA");
            ViewBag.PorcRetISR = dataInmueblesEgresos.GetImpuestos("RET ISR");
            ViewBag.PorcRetIVA = dataInmueblesEgresos.GetImpuestosRetIVA(0);
            ViewBag.Egresos = dataInmueblesEgresos.GetEgresos();
            ViewBag.Monedas = dataInmueblesEgresos.GetMonedas();
            ViewBag.Contratos = dataInmueblesEgresos.GetContratos(-1);

            return View();
        }

        // GET: B_inmuebles_egresosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: B_inmuebles_egresosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: B_inmuebles_egresosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: B_inmuebles_egresosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: B_inmuebles_egresosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: B_inmuebles_egresosController/Delete/5
        public ActionResult Delete(int id, string egreso)
        {
            B_inmuebles_egresos b_Inmuebles_Egresos = new();
            b_Inmuebles_Egresos.Id = id;
            ViewBag.Egreso = egreso;
            return View(b_Inmuebles_Egresos);
        }

        // POST: B_inmuebles_egresosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                DataInmueblesEgresos dataInmueblesEgresos = new();
                dataInmueblesEgresos.DeleteEgreso(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetInmuebles()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmuebles dataInmuebles = new();                                   
            return Json(dataInmuebles.GetCmbInmuebles(idCartera, IdUsuario));
        }

        [HttpGet]
        public JsonResult GetPorcRetIVA(int porcentaje)
        {
            DataInmueblesEgresos dataInmueblesEgresos = new();
            return Json(dataInmueblesEgresos.GetImpuestosRetIVA(porcentaje));
        }

        [HttpGet]
        public JsonResult GetContratos(int IdInmueble)
        {
            DataInmueblesEgresos dataInmueblesEgresos = new();
            return Json(dataInmueblesEgresos.GetContratos(IdInmueble));
        }

        [HttpPost]
        public ActionResult SaveEgreso(B_inmuebles_egresos b_Inmuebles_Egresos)
        {
            try
            {
                #region Validación de permisos
                var claims = HttpContext.User.Claims;
                Menu menu = new Menu();
                int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
                if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                    return Redirect("~/Home");
                #endregion

                DataInmueblesEgresos dataInmueblesEgresos = new();
                if (dataInmueblesEgresos.InsertEgreso(b_Inmuebles_Egresos, IdUsuario))
                    return Json(new { success = true, responseText = "OK" });
                else
                    return Json(new { success = false, responseText = "No se pudo registrar el egreso, favor de verificar sus datos e intentarlo de nuevo" });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        [HttpPost]
        public ActionResult GetEgresosByInmueble(int Id, int IdContrato)
        {
            DataInmueblesEgresos dataInmueblesEgresos = new();
            List<B_inmuebles_egresos> egresos = dataInmueblesEgresos.GetEgresosByIdInmueble(Id, IdContrato);

            return PartialView("_ListEgresos", egresos);
        }

        [HttpPost]
        public ActionResult GetEgresosRentasByInmueble(int Id, int IdContrato)
        {
            DataInmueblesEgresos dataInmueblesEgresos = new();
            List<B_inmuebles_egresos> egresos = dataInmueblesEgresos.GetEgresosRentasByIdInmueble(Id, IdContrato);

            return PartialView("_ListEgresos", egresos);
        }

        [HttpPost]
        public ActionResult DeleteEgreso(int id)
        {
            DataInmueblesEgresos dataInmueblesEgresos = new();
            dataInmueblesEgresos.DeleteEgreso(id);
            return Json(new { success = true, responseText = "OK" });
        }
    }
}
