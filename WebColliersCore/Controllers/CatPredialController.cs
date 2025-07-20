using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using WebColliersCore;
using WebColliersCore.Data;
using WebLomelinCore.Models;
using System.Collections.Generic;
using WebLomelinCore.Data;
using static NPOI.HSSF.Util.HSSFColor;
using WebColliersCore.Models;

namespace WebLomelinCore.Controllers
{
    public class CatPredialController : Controller
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

        // GET: CatPredialController
        public ActionResult Index()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataCatPredial dataCatPredial = new DataCatPredial();
            return View(dataCatPredial.GetCuentasPredial());
        }

        // GET: CatPredialController/Details/5
        public ActionResult Details(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataCatPredial dataCatPredial = new DataCatPredial();
            cat_Predial cat_Predial = new cat_Predial();
            cat_Predial = dataCatPredial.GetCuentasPredialById(id);

            cat_Predial.TipoUsos = dataCatPredial.GetCuentasTUPredialById(id);

            DataLocalidades dataLocalidades = new DataLocalidades();
            DataSelectListItem dataSelectListItem = new DataSelectListItem();
            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
            ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("P");
            ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
            ViewBag.TiposUso = dataSelectListItem.TiposUsoPredial();

            ViewBag.Update = false;

            return View(cat_Predial);
        }

        // GET: CatPredialController/Create
        public ActionResult Create()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            cat_Predial cat_Predial = new cat_Predial();
            List<cat_Predial_TipoUsos> cat_Predial_TipoUsos = new List<cat_Predial_TipoUsos>();

            cat_Predial.TipoUsos = cat_Predial_TipoUsos;

            DataLocalidades dataLocalidades = new DataLocalidades();
            DataSelectListItem dataSelectListItem = new DataSelectListItem();
            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
            ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("P");
            ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
            ViewBag.TiposUso = dataSelectListItem.TiposUsoPredial();

            ViewBag.Insert = false;
            ViewBag.Duplicada = false;
            ViewBag.MensajeDuplicada = "";
            return View(cat_Predial);
        }

        // POST: CatPredialController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion
            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            cat_Predial cat_Predial = new cat_Predial();
            DataCatPredial dataCatPredial = new DataCatPredial();
            try
            {                
                cat_Predial.IdInmueble = int.Parse(collection["IdInmueble"]);
                cat_Predial.IdLocalidad = int.Parse(collection["IdLocalidad"]);
                cat_Predial.IdPeriodicidad = int.Parse(collection["IdPeriodicidad"]);
                cat_Predial.CuentaPredial = collection["CuentaPredial"];
                cat_Predial.M2Terreno = int.Parse(collection["M2Terreno"]);
                cat_Predial.ValorAvaluoCatastral = int.Parse(collection["ValorAvaluoCatastral"]);
                cat_Predial.ClaveCorredor = collection["ClaveCorredor"];
                cat_Predial.IdUsuarioAlta = IdUsuario;

                var TiposUsoList = collection["TipoUsoList"];
                var NivelesList = collection["NivelList"];
                var ClaseList = collection["ClaseList"];
                var M2ConstruccionList = collection["M2ContruccionList"];
                var AntiguedadList = collection["AntiguedadList"];

                List<cat_Predial_TipoUsos> cat_Predial_Tipos = new List<cat_Predial_TipoUsos>();
                for (int i = 0; i < TiposUsoList.Count; i++)
                {
                    cat_Predial_Tipos.Add(new cat_Predial_TipoUsos()
                    {
                        TipoUso = TiposUsoList[i],
                        Nivel = NivelesList[i],
                        Clase = ClaseList[i],
                        M2Construccion = double.Parse(M2ConstruccionList[i]),
                        Antiguedad = int.Parse(AntiguedadList[i])
                    });
                }

                if (dataCatPredial.Existe(cat_Predial)) {                                        
                    cat_Predial.TipoUsos = cat_Predial_Tipos;

                    DataLocalidades dataLocalidades = new DataLocalidades();
                    DataSelectListItem dataSelectListItem = new DataSelectListItem();                    
                    ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                    ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("P");
                    ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
                    ViewBag.TiposUso = dataSelectListItem.TiposUsoPredial();

                    ViewBag.Insert = false;
                    ViewBag.Duplicada = true;
                    ViewBag.MensajeDuplicada = "";

                    return View(cat_Predial);
                }
                

                cat_Predial.TipoUsos = cat_Predial_Tipos;
                
                if (dataCatPredial.Insert(cat_Predial))
                {
                    ViewBag.Insert = true;
                }
                else
                {
                    ViewBag.Insert = false;
                }

                return View(cat_Predial);
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Error = true;
                return View(cat_Predial);
            }
        }

        // GET: CatPredialController/Edit/5
        public ActionResult Edit(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataCatPredial dataCatPredial = new DataCatPredial();   
            cat_Predial cat_Predial = new cat_Predial();
            cat_Predial = dataCatPredial.GetCuentasPredialById(id);

            cat_Predial.TipoUsos = dataCatPredial.GetCuentasTUPredialById(id);

            DataLocalidades dataLocalidades = new DataLocalidades();
            DataSelectListItem dataSelectListItem = new DataSelectListItem();
            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
            ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("P");
            ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
            ViewBag.TiposUso = dataSelectListItem.TiposUsoPredial();
            
            ViewBag.Update = false;
            
            return View(cat_Predial);
        }

        // POST: CatPredialController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion
            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            cat_Predial cat_Predial = new cat_Predial();
            DataCatPredial dataCatPredial = new DataCatPredial();
            try
            {
                cat_Predial.Id = id;
                cat_Predial.IdInmueble = int.Parse(collection["IdInmueble"]);
                cat_Predial.IdLocalidad = int.Parse(collection["IdLocalidad"]);
                cat_Predial.IdPeriodicidad = int.Parse(collection["IdPeriodicidad"]);
                cat_Predial.CuentaPredial = collection["CuentaPredial"];
                cat_Predial.M2Terreno = int.Parse(collection["M2Terreno"]);
                cat_Predial.ValorAvaluoCatastral = int.Parse(collection["ValorAvaluoCatastral"]);
                cat_Predial.ClaveCorredor = collection["ClaveCorredor"];
                cat_Predial.IdUsuarioUpdate = IdUsuario;

                var TiposUsoList = collection["TipoUsoList"];
                var NivelesList = collection["NivelList"];
                var ClaseList = collection["ClaseList"];
                var M2ConstruccionList = collection["M2ContruccionList"];
                var AntiguedadList = collection["AntiguedadList"];

                List<cat_Predial_TipoUsos> cat_Predial_Tipos = new List<cat_Predial_TipoUsos>();
                for (int i = 0; i < TiposUsoList.Count; i++)
                {
                    cat_Predial_Tipos.Add(new cat_Predial_TipoUsos()
                    {
                        TipoUso = TiposUsoList[i],
                        Nivel = NivelesList[i],
                        Clase = ClaseList[i],
                        M2Construccion = double.Parse(M2ConstruccionList[i]),
                        Antiguedad = int.Parse(AntiguedadList[i])
                    });
                }                

                cat_Predial.TipoUsos = cat_Predial_Tipos;

                if (dataCatPredial.Update(cat_Predial))
                {
                    ViewBag.Update = true;
                }
                else
                {
                    DataLocalidades dataLocalidades = new DataLocalidades();
                    DataSelectListItem dataSelectListItem = new DataSelectListItem();
                    ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                    ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("P");
                    ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
                    ViewBag.TiposUso = dataSelectListItem.TiposUsoPredial();
                    ViewBag.Update = false;
                }

                return View(cat_Predial);
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Error = true;
                return View(cat_Predial);
            }
        }

        // GET: CatPredialController/Delete/5
        public ActionResult Delete(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataCatPredial dataCatPredial = new DataCatPredial();
            cat_Predial cat_Predial = new cat_Predial();
            cat_Predial = dataCatPredial.GetCuentasPredialById(id);

            cat_Predial.TipoUsos = dataCatPredial.GetCuentasTUPredialById(id);

            DataLocalidades dataLocalidades = new DataLocalidades();
            DataSelectListItem dataSelectListItem = new DataSelectListItem();
            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
            ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("P");
            ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
            ViewBag.TiposUso = dataSelectListItem.TiposUsoPredial();

            ViewBag.Update = false;

            return View(cat_Predial);
        }

        // POST: CatPredialController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion
            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            cat_Predial cat_Predial = new cat_Predial();
            DataCatPredial dataCatPredial = new DataCatPredial();            
            
            try
            {
                cat_Predial.Id = id;                
                cat_Predial.IdUsuarioUpdate = IdUsuario;
                if (dataCatPredial.Delete(cat_Predial))
                {
                    ViewBag.Delete = true;
                }
                else
                {
                    DataLocalidades dataLocalidades = new DataLocalidades();
                    DataSelectListItem dataSelectListItem = new DataSelectListItem();
                    ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                    ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("P");
                    ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
                    ViewBag.TiposUso = dataSelectListItem.TiposUsoPredial();
                    ViewBag.Delete = false;
                }
                cat_Predial = dataCatPredial.GetCuentasPredialById(id);
                cat_Predial.TipoUsos = dataCatPredial.GetCuentasTUPredialById(id);
                return View(cat_Predial);
            }
            catch
            {
                DataLocalidades dataLocalidades = new DataLocalidades();
                DataSelectListItem dataSelectListItem = new DataSelectListItem();
                ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
                ViewBag.Periodicidades = dataSelectListItem.PeriodicidadServicios("P");
                ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
                ViewBag.TiposUso = dataSelectListItem.TiposUsoPredial();
                return View(cat_Predial);
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

            DataCatPredial dataCatPredial = new DataCatPredial();

            IEnumerable<String> model = dataCatPredial.getLog();
            return PartialView("_Logs", model);
        }
    }
}
