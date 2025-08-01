using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebColliersCore;
using WebColliersCore.Data;
using WebLomelinCore.Models;

namespace WebLomelinCore.Controllers
{
    public class ComprobanteServiciosController : Controller
    {
        // GET: ComprobanteServiciosController
        public ActionResult Index()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataLocalidades dataLocalidades = new DataLocalidades();
            ViewBag.TipoServicios = new PagosServicios().getTipoSerivcios;

            ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
            ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
                        return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ServicioAgua(PagosServicios model)
        
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

               // if (TryValidateModel(model.PagosAgua))
               // {
                    pagosagua pagoAgua = model.PagosAgua;
                    pagoAgua.UsuarioAutoriza = IdUsuario;
                    pagoAgua.FechaAltaRegistro = System.DateTime.Now;
                    pagoAgua.StatusProceso = 1;
                    ViewBag.Message = PagosServicios.InsertaPagosAgua(pagoAgua) ? true : false;
                    return RedirectToAction("Index");
                // }
            }
            catch (Exception ex)
            {

                ViewBag.isOk = false;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SevicioLuz(PagosServicios model)
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

                // if (TryValidateModel(model.PagosLuz))
                // {
                pagosluz pagoLuz = model.PagosLuz;
                pagoLuz.UsuarioAutoriza = IdUsuario;
                pagoLuz.FechaAltaRegistro = System.DateTime.Now;
                pagoLuz.statusproceso = 1;
                ViewBag.Message = PagosServicios.InsertaPagosLuz(pagoLuz) ? true : false;
                return RedirectToAction("Index");
                // }
            }
            catch
            {
                ViewBag.isOk = false;
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ServicioPredial(PagosServicios model)
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

                pagospredial pagoPredial = model.PagosPredial;
                pagoPredial.UsuarioAutoriza = IdUsuario;
                pagoPredial.FechaAltaRegistro = System.DateTime.Now;
                pagoPredial.statusproceso = 1;
                ViewBag.isOk = PagosServicios.InsertaPagosPredial(pagoPredial);
                return View();

            }
            catch
            {
                ViewBag.isOk = false;
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ImportarXML(List<IFormFile> archivos, List<Factura> facturas, string periodo, List<IFormFile> archivoFactura2)
        {
            return PartialView("_FacturasCargadas", null);
        }

        [HttpGet]
        public JsonResult getLocalidades(int IdInmueble)
        {
            DataLocalidades dataLocalidades = new DataLocalidades();
            return Json(dataLocalidades.LocalidadesGet(IdInmueble));
        }

        [HttpGet]
        public JsonResult getCuentasAgua(int IdLocalidad)
        {
            DataLocalidades dataLocalidades = new DataLocalidades();
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text="101010101",
                    Value="1"
                }
            };
            return Json(list);
        }

        [HttpGet]
        public JsonResult getCuentasLuz(int IdLocalidad)
        {
            DataLocalidades dataLocalidades = new DataLocalidades();
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text="101010101",
                    Value="1"
                }
            };
            return Json(list);
        }

        [HttpGet]
        public JsonResult getCuentasPredial(int IdLocalidad)
        {
            DataLocalidades dataLocalidades = new DataLocalidades();
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text="101010101",
                    Value="1"
                }
            };
            return Json(list);
        }

    }
}
