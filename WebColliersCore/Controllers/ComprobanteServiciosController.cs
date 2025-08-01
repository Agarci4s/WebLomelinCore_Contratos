using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebColliersCore;
using WebColliersCore.Data;
using WebLomelinCore.Data;
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
            
            ViewBag.TipoServicios = new DataSelectService().getTipoServicio;
            
            DataLocalidades dataLocalidades = new DataLocalidades();
            ViewBag.Inmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
            ViewBag.Localidades = dataLocalidades.LocalidadesGet(-1);
            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ServicioAgua(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SevicioLuz(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ServicioPredial(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
    }
}
