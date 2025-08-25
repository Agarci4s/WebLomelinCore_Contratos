using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using WebColliersCore;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebLomelinCore.Data;
using WebLomelinCore.Models;

namespace WebLomelinCore.Controllers
{
    public class B_inmueblesContratoRenovacionesController : Controller
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

        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Adela()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesRenovaciones dataInmueblesRenovaciones = new();
            DataInmuebles dataInmuebles = new();
            NegociacionesAdela renovacionAdela = dataInmueblesRenovaciones.GetByIdAdela(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_Inmuebles_Contrato_Distribucions = null;
            return View(renovacionAdela);
        }

        public ActionResult ConvenioModificatorio()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesRenovaciones dataInmueblesRenovaciones = new();
            DataInmuebles dataInmuebles = new();
            NegociacionesConvenioModificatorio renovacionConvenioModificatorio = dataInmueblesRenovaciones.GetByIdConvenioModificatorio(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_Inmuebles_Contrato_Distribucions = null;
            return View(renovacionConvenioModificatorio);
        }

        public ActionResult Renovacion()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            DataInmuebles dataInmuebles = new DataInmuebles();

            List<B_inmuebles> b_inmuebles = dataInmuebles.GetWithContrato(idCartera, IdUsuario);

            return View(b_inmuebles);
        }

        public ActionResult RenovarInmueble(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            //#region Validación de página anterior
            //// Obtiene el encabezado Referer
            //var referer = HttpContext.Request.Headers["Referer"].ToString();
            //// Comprueba si el Referer es nulo, está vacío o no es un dominio conocido
            //if (string.IsNullOrEmpty(referer))
            //    // La solicitud no es de una página anterior válida
            //    return Redirect("~/");
            //#endregion



            DataInmueblesRenovaciones dataInmueblesRenovaciones = new DataInmueblesRenovaciones();
            DataInmuebles dataInmuebles = new DataInmuebles();
            NegociacionesRenovacion renovacion = dataInmueblesRenovaciones.Negociacion_contratos_get(idCartera, IdUsuario, id);
            renovacion.inmueble = dataInmuebles.Get(idCartera, IdUsuario, id);

            //renovacionAdela.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_Inmuebles_Contrato_Distribucions = null;
            return View(renovacion);
        }

        [HttpPost]
        public ActionResult RenovarInmueble(int id, NegociacionesRenovacion renovacion)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion


            DataInmueblesRenovaciones dataInmueblesRenovaciones = new DataInmueblesRenovaciones();
            DataInmuebles dataInmuebles = new DataInmuebles();

            renovacion.id_b_inmuebles = id;
            renovacion.id_b_inmuebles_contrato = id;


            renovacion.id_negociacion_contratos = dataInmueblesRenovaciones.Negociacion_contratos_insert(renovacion);
            //renovacionAdela.b_Inmuebles_Contrato_Distribucions = null;
            return RedirectToAction(nameof(renovacion));
        }

        public ActionResult RenovacionAdela()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesRenovaciones dataInmueblesRenovaciones = new();
            DataInmuebles dataInmuebles = new();
            RenovacionAdela renovacionAdela = dataInmueblesRenovaciones.GetById(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_Inmuebles_Contrato_Distribucions = null;
            return View(renovacionAdela);
        }
    }
}
