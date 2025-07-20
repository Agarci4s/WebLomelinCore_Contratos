using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebColliersCore;
using WebLomelinCore.Models;
using WebLomelinCore.Data;
using static NPOI.HSSF.Util.HSSFColor;
using System.Collections.Generic;
using System.Net;

namespace WebLomelinCore.Controllers
{
    public class CatAguaController : Controller
    {

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

        // GET: CatAguaController
        public ActionResult Index()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataCatAgua dataCatAgua = new DataCatAgua();
            return View(dataCatAgua.GetCuentasAgua());
        }

        // GET: CatAguaController/Details/5
        public ActionResult Details(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataCatAgua dataCatAgua = new DataCatAgua();
            DataLocalidades dataLocalidades = new DataLocalidades();
            DataSelectListItem dataSelectListItem = new DataSelectListItem();
            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            cat_Agua cat_Agua = new cat_Agua();
            cat_Agua = dataCatAgua.GetCuentasAguaById(id);
            ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
            ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("A");
            ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
            ViewBag.CuentaAgua = cat_Agua.CuentaAgua;

            return View(cat_Agua);
        }

        // GET: CatAguaController/Create
        public ActionResult Create()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataLocalidades dataLocalidades = new DataLocalidades();
            DataSelectListItem dataSelectListItem = new DataSelectListItem();
            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
            ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("A");
            ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);

            ViewBag.Insert = false;
            ViewBag.Duplicada = false;
            ViewBag.MensajeDuplicada = "";
            return View();
        }

        // POST: CatAguaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, cat_Agua agua)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion
            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);

            try
            {
                if (ModelState.IsValid)
                {
                    agua.IdUsuarioAlta = IdUsuario;
                    DataCatAgua dataCatAgua = new DataCatAgua();

                    if (dataCatAgua.Existe(agua))
                    {
                        ViewBag.Insert = false;
                        ViewBag.Duplicada = true;
                        ViewBag.MensajeDuplicada = $"La cuenta de agua {agua.CuentaAgua} ya se encuentra registrada.";
                        DataLocalidades dataLocalidades = new DataLocalidades();
                        DataSelectListItem dataSelectListItem = new DataSelectListItem();                        
                        ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                        ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("A");
                        ViewBag.Localidades = dataLocalidades.LocalidadesGet(agua.IdInmueble);
                        return View(agua);
                    }

                    if (dataCatAgua.Insert(agua))
                    {
                        ViewBag.Insert = true;
                    }
                    else
                    {                        
                        ViewBag.Insert = false;
                    }
                    
                }
                return View(agua);
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Error = true;
                return View(agua);
            }
        }

        // GET: CatAguaController/Edit/5
        public ActionResult Edit(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataCatAgua dataCatAgua = new DataCatAgua();
            DataLocalidades dataLocalidades = new DataLocalidades();
            DataSelectListItem dataSelectListItem = new DataSelectListItem();
            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            cat_Agua cat_Agua = new cat_Agua();
            cat_Agua = dataCatAgua.GetCuentasAguaById(id);
            ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
            ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("A");
            ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
            ViewBag.CuentaAgua = cat_Agua.CuentaAgua;
            ViewBag.Update = false;
            ViewBag.Error = false;

            return View(cat_Agua);
        }

        // POST: CatAguaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, cat_Agua cat_Agua)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            try
            {
                if (ModelState.IsValid)
                {
                    cat_Agua.IdUsuarioUpdate = IdUsuario;
                    DataCatAgua dataCatAgua = new DataCatAgua();

                    if (dataCatAgua.Update(cat_Agua))
                    {
                        ViewBag.Update = true;
                    }
                    else
                    {
                        ViewBag.Update = false;
                    }
                }
                DataLocalidades dataLocalidades = new DataLocalidades();
                DataSelectListItem dataSelectListItem = new DataSelectListItem();
                ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("A");
                ViewBag.Localidades = dataLocalidades.LocalidadesGet(cat_Agua.IdInmueble);

                return View(cat_Agua);                
            }
            catch
            {
                DataLocalidades dataLocalidades = new DataLocalidades();
                DataSelectListItem dataSelectListItem = new DataSelectListItem();
                ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("A");
                ViewBag.Localidades = dataLocalidades.LocalidadesGet(cat_Agua.IdInmueble);
                ViewBag.Error = true;
                return View(cat_Agua);
            }
        }

        // GET: CatAguaController/Delete/5
        public ActionResult Delete(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataCatAgua dataCatAgua = new DataCatAgua();
            DataLocalidades dataLocalidades = new DataLocalidades();
            DataSelectListItem dataSelectListItem = new DataSelectListItem();
            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            cat_Agua cat_Agua = new cat_Agua();
            cat_Agua = dataCatAgua.GetCuentasAguaById(id);
            ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
            ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("A");
            ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
            ViewBag.CuentaAgua = cat_Agua.CuentaAgua;
            ViewBag.Delete = false;
            ViewBag.Error = false;

            return View(cat_Agua);
        }

        // POST: CatAguaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection, cat_Agua cat_Agua)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion
            
            try
            {
                if (ModelState.IsValid)
                {
                    cat_Agua.IdUsuarioUpdate = IdUsuario;
                    DataCatAgua dataCatAgua = new DataCatAgua();

                    if (dataCatAgua.Delete(cat_Agua))
                    {
                        ViewBag.Delete = true;
                    }
                    else
                    {
                        ViewBag.Delete = false;
                    }
                }
                DataLocalidades dataLocalidades = new DataLocalidades();
                DataSelectListItem dataSelectListItem = new DataSelectListItem();
                ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("A");
                ViewBag.Localidades = dataLocalidades.LocalidadesGet(cat_Agua.IdInmueble);

                return View(cat_Agua);
            }
            catch
            {
                DataLocalidades dataLocalidades = new DataLocalidades();
                DataSelectListItem dataSelectListItem = new DataSelectListItem();
                ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("A");
                ViewBag.Localidades = dataLocalidades.LocalidadesGet(cat_Agua.IdInmueble);
                ViewBag.Error = true;
                return View(cat_Agua);
            }
        }

        [HttpGet]
        public JsonResult getLocalidades(int IdInmueble)
        {
            DataLocalidades dataLocalidades = new DataLocalidades();
            return Json(dataLocalidades.LocalidadesGet(IdInmueble));
        }

        [HttpGet]
        public ActionResult Logs()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataCatAgua dataCatAgua = new DataCatAgua();

            IEnumerable<String> model = dataCatAgua.getLog();
            return PartialView("_Logs", model);
        }
    }
}
