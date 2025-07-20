using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using WebColliersCore.Data;
using WebColliersCore.Models;

namespace WebColliersCore.Controllers
{
    public class TipoCarteraController : Controller
    {
        DataTpCartera dataTpCartera = new DataTpCartera();

        public void EstablecerPermisos(int IdUsuario, MethodBase currentMethod, int idCartera)
        {
            try
            {
                DataCatalogos dataCatalogos = new DataCatalogos();
                dataCatalogos.SetPermisos(IdUsuario, currentMethod, idCartera);
                ViewBag.Agregar = dataCatalogos.Agregar;
                ViewBag.Editar = dataCatalogos.Editar;
                ViewBag.Eliminar = dataCatalogos.Eliminar;
                ViewBag.Cartera = dataCatalogos.Cartera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: TipoCartera
        public ActionResult Index()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            return View(dataTpCartera.GetTipoCartera());
        }

        // GET: TipoCartera/Details/5
        public ActionResult Details(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            return View(dataTpCartera.GetTipoCartera(id));
        }

        // GET: TipoCartera/Create
        public ActionResult Create()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 3;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            return View();
        }

        // POST: TipoCartera/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, TpCartera tpCartera)
        {
            try
            {
                #region Validación de permisos
                var claims = HttpContext.User.Claims;
                Menu menu = new Menu();
                int IdUsuario = 0, idCartera = 0, tipoNivel = 3;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
                if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                    return Redirect("~/Home");
                #endregion

                EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
                if (string.IsNullOrEmpty(tpCartera.descripcionCartera))
                {
                    ViewBag.Error = true;
                    ViewBag.Mensaje = "Falta proporcionar el nuevo tipo de cartera";
                    return View(tpCartera);
                }
                if (dataTpCartera.ExisteTipoCartera(tpCartera.descripcionCartera))
                {
                    ViewBag.Error = true;
                    ViewBag.Mensaje = "El tipo de cartera ya existe";
                    return View(tpCartera);
                }
                dataTpCartera.Agregar(tpCartera);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoCartera/Edit/5
        public ActionResult Edit(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            if (!ViewBag.Editar)
            {
                return Redirect("~/Propietarios");
            }
            ViewBag.lstTipoComprobante = dataTpCartera.lstTipoComprobante();
            return View(dataTpCartera.GetTipoCartera(id));
        }

        // POST: TipoCartera/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, TpCartera tpCartera)
        {
            try
            {
                #region Validación de permisos
                var claims = HttpContext.User.Claims;
                Menu menu = new Menu();
                int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
                if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                    return Redirect("~/Home");
                #endregion

                EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
                if (string.IsNullOrEmpty(tpCartera.descripcionCartera))
                {
                    ViewBag.Error = true;
                    ViewBag.Mensaje = "Falta proporcionar el tipo de cartera";
                    return View(tpCartera);
                }
                dataTpCartera.Actualizar(id, tpCartera);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoCartera/Delete/5
        public ActionResult Delete(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 3;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            if (!ViewBag.Eliminar)
            {
                return Redirect("~/Propietarios");
            }
            return View(dataTpCartera.GetTipoCartera(id));
        }

        // POST: TipoCartera/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 3;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            try
            {
                dataTpCartera.Eliminar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult SelectSeries(int idCartera)
        {
            return PartialView("lstSeries", dataTpCartera.SeriesList(idCartera));
        }

        public ActionResult InsertSerie(int id, string serie, string tipo)
        {
            dataTpCartera.InsertSerie(id, serie, tipo);

            return new JsonResult(id) { Value = "Se registro la serie correctamente." };
        }
    }
}