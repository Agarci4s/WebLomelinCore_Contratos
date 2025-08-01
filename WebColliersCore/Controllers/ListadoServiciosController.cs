using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        // GET: ListadoServiciosController
        public ActionResult Index()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion
           
            ViewBag.Estatus = new DataSelectService().getStatusServicio.OrderBy(x => x.Value); ;
            ViewBag.TipoServicios = new DataSelectService().getTipoServicio.OrderBy(x => x.Value);
            ViewBag.Regiones = new DataSelectService().getRegionesList.OrderBy(x => x.Value);
            ViewBag.Inmuebles = new DataInmuebles().GetInmuebleByRegion(-1, idCartera, IdUsuario);// dataLocalidades.InmueblesGet(IdUsuario, idCartera);
            ViewBag.Localidades = new DataLocalidades().LocalidadesGet(-1).OrderBy(x => x.Value); ;
            ViewBag.Cuentas = new DataSelectService().getCuentas(null, null, null);
            ViewBag.TipoServicioSolcitud = 0;
            return View();
        }

        [HttpPost]
        public ActionResult Busqueda(PagoUnificadoDTO model)
        {
            PagoUnificadoDTO response = PagoUnificadoDTO.getPagoServiciosList(model.IdInmueble ,model.IdLocalidad,model.idCuenta,model.IdTipoServicio,model.IdStatusProceso);
            ViewBag.TipoServicioSolciitud = 1;
            return PartialView("_ListServicios", response);
        }


        [HttpGet]
        public ActionResult EditarCuentaAgua  (int Id)
        {
            pagosagua response = new pagosagua();
            return View(response);
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

            DataInmuebles dataInmuebles = new DataInmuebles();
            return Json(dataInmuebles.GetInmuebleByRegion(IdRegion, idCartera, IdUsuario).OrderBy(x => x.Value));
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

        
    }
}
