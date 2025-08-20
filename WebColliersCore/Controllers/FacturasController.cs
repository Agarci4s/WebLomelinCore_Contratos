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
    public class FacturasController : Controller
    {
        private bool InicializaVista(int? IdRegion, int? IdInmueble)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            bool response = menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims);
            #endregion

            ViewBag.Estatus = new DataSelectService().getStatusServicio.OrderBy(x => x.Value);
            ViewBag.Regiones = PagosServicios.setItem(new DataSelectService().getRegionesList, IdRegion);

            IdRegion = IdRegion.HasValue ? IdRegion.Value : -1;
            ViewBag.Inmuebles = PagosServicios.setItem(new DataInmuebles().GetInmuebleByRegion(IdRegion.Value, idCartera, IdUsuario), IdInmueble);

            //IdInmueble = IdInmueble.HasValue ? IdInmueble : -1;
            //ViewBag.Localidades = PagosServicios.setItem(new DataLocalidades().LocalidadesGet(IdInmueble.Value), IdLocalidad);

            return response;
        }


        // GET: FacturasController
        public ActionResult Index()
        {
            if (!InicializaVista(null, null))
            {
                return Redirect("~/Home");
            }
            FacturasPagadas model = new FacturasPagadas
            {
                
            };
            return View(model);
        }

      

        // POST: FacturasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Busqueda(FacturasPagadas collection)
        {
            try
            {
                //IEnumerable<FacturasPagadas> model = new List<FacturasPagadas>();
                DataGastos data = new DataGastos();
                var model = data.GetFacturasPagadas(
                       collection.IdRegion,
                       collection.IdInmueble
                );
                return PartialView("_Listado", model);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index");
            }
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

    }
}
