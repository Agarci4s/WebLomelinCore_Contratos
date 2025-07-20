using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using WebColliersCore.Data;
using WebColliersCore;
using WebLomelinCore.Data;
using static NPOI.HSSF.Util.HSSFColor;
using WebLomelinCore.Models;
using System.Collections.Generic;

namespace WebLomelinCore.Controllers
{
    public class CatLuzController : Controller
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

        // GET: CatLuzController
        public ActionResult Index()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataCatLuz dataCatLuz = new DataCatLuz();
            return View(dataCatLuz.GetCuentasLuz());           
        }

        // GET: CatLuzController/Details/5
        public ActionResult Details(int id)
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

                DataCatLuz dataCatLuz = new DataCatLuz();
                DataLocalidades dataLocalidades = new DataLocalidades();
                DataSelectListItem dataSelectListItem = new DataSelectListItem();
                EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
                cat_Luz cat_Luz = new cat_Luz();
                cat_Luz = dataCatLuz.GetCuentasLuzById(id);
                ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("L");
                ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
                ViewBag.CuentaLuz = cat_Luz.CuentaLuz;

                return View(cat_Luz);
            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }

        // GET: CatLuzController/Create
        public ActionResult Create()
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

                DataLocalidades dataLocalidades = new DataLocalidades();
                DataSelectListItem dataSelectListItem = new DataSelectListItem();
                EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
                ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("L");
                ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);

                ViewBag.Insert = false;
                ViewBag.Duplicada = false;
                ViewBag.MensajeDuplicada = "";
                return View();
            }
            catch (Exception)
            {
                return View();
                throw;
            }            
        }

        // POST: CatLuzController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, cat_Luz luz)
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
                    luz.IdUsuarioAlta = IdUsuario;
                    DataCatLuz dataCatLuz = new DataCatLuz();

                    if (dataCatLuz.Existe(luz))
                    {
                        ViewBag.Insert = false;
                        ViewBag.Duplicada = true;
                        ViewBag.MensajeDuplicada = $"La cuenta de luz {luz.CuentaLuz} ya se encuentra registrada.";
                        DataLocalidades dataLocalidades = new DataLocalidades();
                        DataSelectListItem dataSelectListItem = new DataSelectListItem();
                        ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                        ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("L");
                        ViewBag.Localidades = dataLocalidades.LocalidadesGet(luz.IdInmueble);
                        return View(luz);
                    }

                    if (dataCatLuz.Insert(luz))
                    {
                        ViewBag.Insert = true;
                    }
                    else
                    {
                        ViewBag.Insert = false;
                    }

                }
                return View(luz);                
            }
            catch
            {
                ViewBag.Error = true;
                return View(luz);
            }
        }

        // GET: CatLuzController/Edit/5
        public ActionResult Edit(int id)
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

                DataCatLuz dataCatLuz = new DataCatLuz();
                DataLocalidades dataLocalidades = new DataLocalidades();
                DataSelectListItem dataSelectListItem = new DataSelectListItem();
                EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
                cat_Luz cat_Luz = new cat_Luz();
                cat_Luz = dataCatLuz.GetCuentasLuzById(id);
                ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("L");
                ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
                ViewBag.CuentaLuz = cat_Luz.CuentaLuz;
                ViewBag.Update = false;
                ViewBag.Error = false;

                return View(cat_Luz);
            }
            catch (Exception)
            {
                return View();
                throw;
            }            
        }

        // POST: CatLuzController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, cat_Luz cat_Luz)
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
                    cat_Luz.IdUsuarioUpdate = IdUsuario;
                    DataCatLuz dataCatLuz = new DataCatLuz();

                    if (dataCatLuz.Update(cat_Luz))
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
                ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("L");
                ViewBag.Localidades = dataLocalidades.LocalidadesGet(cat_Luz.IdInmueble);

                return View(cat_Luz);
            }
            catch
            {
                DataLocalidades dataLocalidades = new DataLocalidades();
                DataSelectListItem dataSelectListItem = new DataSelectListItem();
                ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("L");
                ViewBag.Localidades = dataLocalidades.LocalidadesGet(cat_Luz.IdInmueble);
                ViewBag.Error = true;
                return View(cat_Luz);
            }
        }

        // GET: CatLuzController/Delete/5
        public ActionResult Delete(int id)
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

                DataCatLuz dataCatLuz = new DataCatLuz();
                DataLocalidades dataLocalidades = new DataLocalidades();
                DataSelectListItem dataSelectListItem = new DataSelectListItem();
                EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
                cat_Luz cat_Luz = new cat_Luz();
                cat_Luz = dataCatLuz.GetCuentasLuzById(id);
                ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("L");
                ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
                ViewBag.CuentaLuz = cat_Luz.CuentaLuz;
                ViewBag.Delete = false;
                ViewBag.Error = false;

                return View(cat_Luz);
            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }

        // POST: CatLuzController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection, cat_Luz cat_Luz)
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
                    cat_Luz.IdUsuarioUpdate = IdUsuario;
                    DataCatLuz dataCatLuz = new();

                    if (dataCatLuz.Delete(cat_Luz))
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
                ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("L");
                ViewBag.Localidades = dataLocalidades.LocalidadesGet(cat_Luz.IdInmueble);

                return View(cat_Luz);
            }
            catch
            {
                DataLocalidades dataLocalidades = new DataLocalidades();
                DataSelectListItem dataSelectListItem = new DataSelectListItem();
                ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("L");
                ViewBag.Localidades = dataLocalidades.LocalidadesGet(cat_Luz.IdInmueble);
                ViewBag.Error = true;
                return View(cat_Luz);
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

            DataCatLuz dataCatLuz = new DataCatLuz();

            IEnumerable<String> model = dataCatLuz.getLog();
            return PartialView("_Logs", model);
        }
    }
}
