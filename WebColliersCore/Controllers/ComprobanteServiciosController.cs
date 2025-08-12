using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
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
                pagoLuz.StatusProceso = 1;
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
                pagoPredial.StatusProceso = 1;
                ViewBag.isOk = PagosServicios.InsertaPagosPredial(pagoPredial) ? true : false;
                return RedirectToAction("Index");

            }
            catch
            {
                ViewBag.isOk = false;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> ImportarXML(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Archivo inválido.");
                return Json(null);
            }

            pagosluz response = await ProcesarXml(file);
            return Json(response);
        }

        private async Task<pagosluz> ProcesarXml(IFormFile file)
        {
            XDocument xmlDoc;
            using (var stream = file.OpenReadStream())
            {
                xmlDoc = XDocument.Load(stream);
            }

            XNamespace cfdi = "http://www.sat.gob.mx/cfd/4";
            XNamespace cfe = "http://www.itcomplements.com/cfd/cfe/v1";

            var comprobante = xmlDoc.Element(cfdi + "Comprobante");
            var emisor = comprobante?.Element(cfdi + "Emisor");
            var receptor = comprobante?.Element(cfdi + "Receptor");

            var cfeNodo = comprobante?
                .Element(cfdi + "Addenda")?
                .Element(cfe + "CFE")?
                .Element(cfe + "ComisionFederalElectricidad");

            var clsNodo = comprobante?
                .Element(cfdi + "Addenda")?
                .Elements().FirstOrDefault(e => e.Name.LocalName == "clsRegArchFact");

            var importes = clsNodo.Elements().FirstOrDefault(e => e.Name.LocalName == "Importes");

            pagosluz modelLuz = new pagosluz();


            modelLuz.fechaPago = ExtractDate(clsNodo?.Elements().FirstOrDefault(e => e.Name.LocalName == "FECORTE")?.Value);
            modelLuz.FechaLimitePago = ExtractDate(clsNodo?.Elements().FirstOrDefault(e => e.Name.LocalName == "FECLIMITE")?.Value);
            modelLuz.FechaCorte = ExtractDate(clsNodo?.Elements().FirstOrDefault(e => e.Name.LocalName == "FECORTE")?.Value);
            modelLuz.LecturaActual = ExtractDouble(clsNodo?.Elements().FirstOrDefault(e => e.Name.LocalName == "LECACT1")?.Value);
            modelLuz.LecturaAnterior = ExtractDouble(clsNodo?.Elements().FirstOrDefault(e => e.Name.LocalName == "LECANT1")?.Value);
            
            modelLuz.periodoPago = clsNodo?.Elements().FirstOrDefault(e => e.Name.LocalName == "FECDESDE")?.Value + " " + clsNodo?.Elements().FirstOrDefault(e => e.Name.LocalName == "FECHASTA")?.Value;
            modelLuz.conceptoPago = "Energía";
            modelLuz.LineaCaptura = clsNodo?.Elements().FirstOrDefault(e => e.Name.LocalName == "LineaDeReferencia")?.Value;
            modelLuz.importe = ExtractDouble(importes?.Elements().FirstOrDefault(e => e.Name.LocalName == "Importe1")?.Value);
            modelLuz.iva = ExtractDouble(importes?.Elements().FirstOrDefault(e => e.Name.LocalName == "Importe2")?.Value);
            modelLuz.ImporteTotal = ExtractDouble(importes?.Elements().FirstOrDefault(e => e.Name.LocalName == "Importe3")?.Value);
           
            return modelLuz;
        }

        private double ExtractDouble(string data)
        {
            double response = 0;
            double.TryParse(data, out response);
            return response;

        }
        private DateTime ExtractDate(string data)
        {
            int day = 0;
            int year= 0;
            int mont = 0;
            

            int.TryParse(data.Substring(0, 2), out day);            
            mont = getMonth(data.Substring(3, 3));           
            int.TryParse(data.Substring(7, 4), out year);
                                  
            DateTime response = new DateTime(year,mont,day);
            //DateTime.TryParse(data , out response);
            return response;
        }

        private int getMonth(string mes)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            map.Add("ENE", 1);
            map.Add("FEB", 2);
            map.Add("MAR", 3);
            map.Add("ABR", 4);
            map.Add("MAY", 5);
            map.Add("JUN", 6);
            map.Add("JUL", 7);
            map.Add("AGO", 8);
            map.Add("SEP", 9);
            map.Add("OCT", 10);
            map.Add("NOV", 11);
            map.Add("DIC", 12);

            int response = 0;
            if (map.ContainsKey(mes.ToUpper()))
            {
                response = map[mes.ToUpper()];
            }
            return response;
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

