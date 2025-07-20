using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using WebColliersCore.Data;
using WebColliersCore.Models;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Linq;
using System.Security.Claims;

namespace WebColliersCore.Controllers
{
    public class B_inmuebles_2Controller : Controller
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

        public ActionResult Index()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            if (idCartera == 3)
            {
                return Redirect("~/B_inmuebles");
            }

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            DataInmuebles dataInmuebles = new DataInmuebles();

            DataCG dataCG = new DataCG();
            ViewBag.lstClasificacion = dataCG.b_cg_clasificacionGet();

            DataUsuarios dataUsuarios = new DataUsuarios();
            List<B_inmuebles> listB_inmuebles = dataInmuebles.Get(idCartera, IdUsuario);
            return View(listB_inmuebles);
        }

        public ActionResult Details(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);

            DataCG dataCG = new DataCG();
            ViewBag.lstRegimen = dataCG.b_cg_regimenGet();
            ViewBag.lstEstatus = dataCG.b_cg_inmuebles_estatusGet();
            ViewBag.lstClasificacion = dataCG.b_cg_clasificacionGet();
            ViewBag.lstRegion = dataCG.b_cg_regionGet();
            ViewBag.lstValorMercado = dataCG.b_cg_valor_mercado();

            DataInmuebles dataInmuebles = new DataInmuebles();
            B_inmuebles b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, id);

            return View(b_inmuebles);
        }

        public ActionResult Create()
        {

            DataCG dataCG = new DataCG();
            ViewBag.lstRegimen = dataCG.b_cg_regimenGet();
            ViewBag.lstEstatus = dataCG.b_cg_inmuebles_estatusGet();
            ViewBag.lstClasificacion = dataCG.b_cg_clasificacionGet();
            ViewBag.lstRegion = dataCG.b_cg_regionGet();
            ViewBag.lstValorMercado = dataCG.b_cg_valor_mercado();
            ViewBag.lstSubClasificacion = dataCG.b_cg_subclasificacionGet();

            B_inmuebles b_inmuebles = new B_inmuebles();
            b_inmuebles.ingreso = DateTime.Now.Year;

            return View(b_inmuebles);
        }

        // POST: b_inmueblesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(B_inmuebles b_Inmuebles)
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

                DataInmuebles dataInmuebles = new DataInmuebles();
                b_Inmuebles.IDCARTERA = idCartera;
                dataInmuebles.Insert(b_Inmuebles, IdUsuario);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                DataCG dataCG = new DataCG();
                ViewBag.lstRegimen = dataCG.b_cg_regimenGet();
                ViewBag.lstEstatus = dataCG.b_cg_inmuebles_estatusGet();
                ViewBag.lstClasificacion = dataCG.b_cg_clasificacionGet();
                ViewBag.lstRegion = dataCG.b_cg_regionGet();
                ViewBag.lstValorMercado = dataCG.b_cg_valor_mercado();
                ViewBag.lstSubClasificacion = dataCG.b_cg_subclasificacionGet();

                return View();
            }
        }

        // GET: b_inmueblesController/Edit/5
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
            DataInmuebles dataInmuebles = new DataInmuebles();

            DataCG dataCG = new DataCG();
            ViewBag.lstRegimen = dataCG.b_cg_regimenGet();
            ViewBag.lstEstatus = dataCG.b_cg_inmuebles_estatusGet();
            ViewBag.lstClasificacion = dataCG.b_cg_clasificacionGet();
            ViewBag.lstRegion = dataCG.b_cg_regionGet();
            ViewBag.lstValorMercado = dataCG.b_cg_valor_mercado();
            ViewBag.lstSubClasificacion = dataCG.b_cg_subclasificacionGet();

            B_inmuebles b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, id);
            return View(b_inmuebles);
        }

        // POST: b_inmueblesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, B_inmuebles b_Inmuebles)
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
                DataInmuebles dataInmuebles = new DataInmuebles();
                b_Inmuebles.id_b_inmuebles = id;
                dataInmuebles.Edit(b_Inmuebles);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                DataCG dataCG = new DataCG();
                ViewBag.lstRegimen = dataCG.b_cg_regimenGet();
                ViewBag.lstEstatus = dataCG.b_cg_inmuebles_estatusGet();
                ViewBag.lstClasificacion = dataCG.b_cg_clasificacionGet();
                ViewBag.lstRegion = dataCG.b_cg_regionGet();
                ViewBag.lstValorMercado = dataCG.b_cg_valor_mercado();
                ViewBag.lstSubClasificacion = dataCG.b_cg_subclasificacionGet();

                return View();
            }
        }

        // GET: b_inmueblesController/Delete/5
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
            DataInmuebles dataInmuebles = new DataInmuebles();

            B_inmuebles b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, id);
            return View(b_inmuebles);
        }

        // POST: b_inmueblesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, B_inmuebles b_Inmueble)
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

                DataInmuebles dataInmuebles = new DataInmuebles();
                dataInmuebles.Delete(idCartera, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
