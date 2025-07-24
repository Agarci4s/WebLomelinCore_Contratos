using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebLomelinCore.Models;

namespace WebLomelinCore.Controllers
{
    public class ComprobanteServiciosController : Controller
    {
        // GET: ComprobanteServiciosController
        public ActionResult Index()
        {
            ViewBag.TipoServicios = new PagosServicios().getTipoSerivcios;
            return View();
        }

        // GET: ComprobanteServiciosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ComprobanteServiciosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComprobanteServiciosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ComprobanteServiciosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ComprobanteServiciosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ComprobanteServiciosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ComprobanteServiciosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
