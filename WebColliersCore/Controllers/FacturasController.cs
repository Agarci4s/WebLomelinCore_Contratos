using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HPSF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebColliersCore;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebLomelinCore.Data;
using WebLomelinCore.Models;

namespace WebLomelinCore.Controllers
{
    public class FacturasController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FacturasController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
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

        
            ViewBag.Conceptos = PagosServicios.setItem(new DataGastos().GetEgresosAll(), null);

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
                       collection.IdConcepto,
                       collection.IdInmueble,
                       null
                );
                return PartialView("_Listado", model);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult DescargaExcel(FacturasPagadas collection)
        {
            var data = new DataGastos().GetFacturasPagadas(collection.IdConcepto, collection.IdInmueble, null);

            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Facturas");

            //Encabezados
            worksheet.Cell(1, 1).Value = "ue";
            worksheet.Cell(1, 2).Value = "cr";
            worksheet.Cell(1, 3).Value = "Nombre Sucursal";
            worksheet.Cell(1, 4).Value = "Concepto";
            worksheet.Cell(1, 5).Value = "Importe";
            worksheet.Cell(1, 6).Value = "Mes de Pago";
            worksheet.Cell(1, 8).Value = "Fecha Pago Realizado";

            //Datos
            for (int i = 0; i < data.Count; i++)
            {
                var item = data[i];
                worksheet.Cell(i + 2, 1).Value = item.Inmueble.ue;
                worksheet.Cell(i + 2, 2).Value = item.Inmueble.cr;
                worksheet.Cell(i + 2, 3).Value = item.Inmueble.nombre;
                worksheet.Cell(i + 2, 4).Value = item.Factura.Concepto;
                worksheet.Cell(i + 2, 5).Value = item.Factura.Importe;
                worksheet.Cell(i + 2, 6).Value = item.MesPago;
                worksheet.Cell(i + 2, 8).Value = item.FechaPagoRealizado.ToShortDateString();
            }
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "FacturasCargadas.xlsx");
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
        public ActionResult Documento(int? Id) 
        {  
            var registro = new DataGastos().GetFacturasPagadas(null,null,Id).FirstOrDefault();
            return View(registro); 
        }

        [HttpPost]
        public async Task<JsonResult> Documento(DocumentsUpload file)
        {
            try
            {
                if (file == null || file.Archivo == null || file.Archivo.Length == 0)
                {
                    ModelState.AddModelError("", "Archivo inválido.");
                    return Json(null);
                }

                var fileName = file.Archivo.FileName.Replace(" ", "_");
                var fileExt = file.Archivo.FileName.Substring(file.Archivo.FileName.LastIndexOf('.'));

                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Archivos\\ComprobantesPagos\\");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    await fileStream.CopyToAsync(fileStream);
                }

                /*
                 * se manda actualizacion a bd 
                 */

                file.RutaArchivo = filePath;
                file.IdRegistro = file.IdRegistro.HasValue ? file.IdRegistro.Value : 0;

                return Json($"El archivo '{fileName}' se cargo correctamente.");
            }
            catch (Exception ex)
            {
                ex.ToString();
                return Json($"Ocurrio un problema al cargar el archivo. Intentelo nuevamente.");
            }
        }

        [HttpPost]
        public async Task<JsonResult> EnvioIcoi([FromBody] List<FacturasPagadas> model)
        {
            List< facturasModel > listToIcoi = new List<facturasModel>();
            foreach (var item in model)
            {
                /*
                 * obtner los datos de la factura por id                 
                 */
                var registro = new DataGastos().GetFacturasPagadas(null, null, item.IdRegistro).FirstOrDefault();
                IFormFile xml =new FormFile(new MemoryStream(), 0, 0, "file", registro.RutaXml);
                IFormFile pdf = new FormFile(new MemoryStream(), 0, 0, "file", registro.RutaPdf);
                
                facturasModel sendcoi = new facturasModel
                {
                    rfcEmisor = registro.Factura.RFCEmisor,
                    rfcReceptor = registro.Factura.ReceptorRFC,
                    folio = registro.Factura.FolioFiscal,
                    serie = registro.Factura.Serie,  /*flta mapear en bd*/
                    total = registro.Factura.Total.ToString("F2"),
                    uuid = registro.Factura.FolioFiscal, // Asumiendo que FolioFiscal es el UUID
                    fecha = DateTime.Now.ToString(),      //validar si es la currendate
                    pdf= getFile(registro.RutaPdf),
                    xml = getFile(registro.RutaXml)
                };
                listToIcoi.Add(sendcoi);
                /*actualiza la tabla de envio icoi*/
            }

            /*
             * manda los datos a icoi service
             */

            Uri uri = new Uri("http://sicoiweb.lomelin.com.mx/LomelinFacturasWebAPI/api/Facturas/EnviofacturasSICOI");
            var response = await  DataApi.CallPostMethod(uri, listToIcoi);
            if (response.IsSuccessStatusCode)
            {
                /*
                * manda los datos a icoi service
                */

                foreach (var item in model)
                {
                    /*
                     * actualiza la tabla de envio icoi
                     */
                    //new DataGastos().UpdateEnvioIcoi(item.IdRegistro);
                }
                return Json("Se envio correctamente a SICOI");
            }
            else
            {
                return Json("Ocurrio un error al enviar a SICOI");
            }
            return Json(null);
        }

        private static IFormFile getFile(string path)
        {
            IFormFile response = null;
            try
            {
                response=new FormFile(new FileStream(path, FileMode.Open), 0, new FileInfo(path).Length, "file", Path.GetFileName(path));
            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }

    }
}
