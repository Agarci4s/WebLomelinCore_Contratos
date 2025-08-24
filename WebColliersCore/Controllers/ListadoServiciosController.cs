using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using WebColliersCore;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebLomelinCore.Data;
using WebLomelinCore.Models;


namespace WebLomelinCore.Controllers
{
    public class ListadoServiciosController : Controller
    {
        private bool InicializaVista(int? IdServicio, int? IdRegion, int? IdInmueble, int? IdLocalidad, int? IdCuenta)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            bool response = menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims);
            #endregion

            ViewBag.Estatus = new DataSelectService().getStatusServicio.OrderBy(x => x.Value);
            ViewBag.TipoServicios = PagosServicios.setItem(new DataSelectService().getTipoServicio, IdServicio);
            ViewBag.Regiones = PagosServicios.setItem(new  DataSelectService().getRegionesList, IdRegion);
            
            IdRegion = IdRegion.HasValue ? IdRegion.Value : -1;
            
            ViewBag.Inmuebles = PagosServicios.setItem(new DataInmuebles().GetInmuebleByRegion(IdRegion.Value, idCartera, IdUsuario), IdInmueble);
            
            IdInmueble = IdInmueble.HasValue ? IdInmueble : -1;
            ViewBag.Localidades = PagosServicios.setItem(new DataLocalidades().LocalidadesGet(IdInmueble.Value), IdInmueble);

            IdLocalidad=IdLocalidad.HasValue?IdLocalidad: -1;
            ViewBag.Cuentas = PagosServicios.setItem(new DataSelectService().getCuentas(IdInmueble, IdLocalidad, IdServicio), IdCuenta);

            IdServicio = IdServicio.HasValue ? IdServicio.Value : 0;

            ViewBag.TipoServicioSolcitud = IdServicio;

            return response;
        }

        

        // GET: ListadoServiciosController
        public ActionResult Index()
        {
            if (!InicializaVista(null,null,null,null,null))
            {
                return Redirect("~/Home");
            }
            PagoUnificadoDTO model = new PagoUnificadoDTO
            {
                PagosAgua = new List<pagosagua>()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Busqueda(PagoUnificadoDTO model)
        {
            PagoUnificadoDTO response = PagoUnificadoDTO.getPagoServiciosList(
                model.IdInmueble,
                model.IdLocalidad,
                model.IdTipoServicio,
                model.IdStatusProceso,
                model.IdPagoServicio,
                model.IdCuentaServicio);

            InicializaVista(model.IdTipoServicio,model.IdRegion, model.IdInmueble, model.IdLocalidad, model.IdCuentaServicio);
            if (model.IdTipoServicio == 1)/*agua*/
            {
                ViewBag.TipoServicioSolcitud = 1;
                return PartialView("ListadoPagosAgua", response.PagosAgua);
            }
            if (model.IdTipoServicio == 2)/*Luz*/
            {
                return PartialView("ListadoPagosLuz", response.PagosLuz);
            }
            if (model.IdTipoServicio == 3)/*Predial*/
            {
                return PartialView("ListadoPagosPredial", response.PagosPredial);
            }

            ViewBag.TipoServicioSolcitud = model.IdTipoServicio;
            return Json(response.PagosAgua);
        }

        [HttpPost]
        public JsonResult AccionesAgua([FromBody]  List<pagosagua> model)
        {
            //InicializaVista(model.IdTipoServicio, model.IdRegion, model.IdInmueble, model.IdLocalidad, model.idCuenta);
            ViewBag.Estatus = new DataSelectService().getStatusServicio.OrderBy(x => x.Value);

            foreach (var item in model)
            {
                var seleccionado = item.EsSeleccionado;
                var estatus = item.StatusProceso;
                var id = item.idPagoAgua;

                /*
                 * generar proceso para enviar los cambios de estatus a bd
                 */
            }            

            return Json(model);
        }

        [HttpPost]
        public JsonResult AccionesLuz([FromBody] List<pagosluz> model)
        {
            ViewBag.Estatus = new DataSelectService().getStatusServicio.OrderBy(x => x.Value);

            foreach (var item in model)
            {
                var seleccionado = item.EsSeleccionado;
                var estatus = item.StatusProceso;
                var id = item.idPagoLuz;

                /*
                 * generar proceso para enviar los cambios de estatus a bd
                 */
            }
            return Json(model);
        }

        [HttpPost]
        public JsonResult AccionesPredial([FromBody]  List<pagospredial> model)
        {
            ViewBag.Estatus = new DataSelectService().getStatusServicio.OrderBy(x => x.Value);

            foreach (var item in model)
            {
                var seleccionado = item.EsSeleccionado;
                var estatus = item.StatusProceso;
                var id = item.idPagoPredial;

                /*
                 * generar proceso para enviar los cambios de estatus a bd
                 */
            }
            return Json(model);
        }


        [HttpGet]
        public ActionResult EditarCuentaLuz(int Id)
        {
            pagosluz response = new pagosluz();
            return View(response);
        }
        [HttpGet]
        public ActionResult EditarCuentaPredial(int Id)
        {
            pagosluz response = new pagosluz();
            return View(response);
        }

        [HttpGet]
        public JsonResult getInmuebles(int IdRegion)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims);
            #endregion

            
           var list = PagosServicios.setItem(new DataInmuebles().GetInmuebleByRegion(IdRegion, idCartera, IdUsuario), null);
            return Json(list);
        }

        [HttpGet]
        public JsonResult getLocalidades(int IdInmueble)
        {
            DataLocalidades dataLocalidades = new DataLocalidades();
            return Json(dataLocalidades.LocalidadesGet(IdInmueble));
        }

        [HttpGet]
        public JsonResult getCuentas(int? IdInmueble, int? IdLocalidad, int? IdServicio)
        {
            return Json(new DataSelectService().getCuentas(IdInmueble, IdLocalidad, IdServicio));
        }

        [HttpGet]
        public JsonResult getPeriodicidad(int? IdServicio)
        {
            return Json(PeriodosServicios.getPeriodicidad(IdServicio));
        }

        [HttpGet]
        public JsonResult getBimestres(int? IdPeriodicidad)
        {
            return Json(PeriodosServicios.getBimestres(IdPeriodicidad));
        }


    }
}
