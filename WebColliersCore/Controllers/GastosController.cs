using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using WebColliersCore;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebLomelinCore.Data;
using WebLomelinCore.Models;

namespace WebLomelinCore.Controllers
{
    public class GastosController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        [TempData]
        public string MensajeLoad { get; set; }

        [TempData]
        public string MovimientosExitosos { get; set; }

        [TempData]
        public string MovimientosFallidos { get; set; }

        public GastosController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        private List<SelectListItem> FormasPagos = new List<SelectListItem> {
                new SelectListItem { Value = "Transferencia bancaria", Text = "Transferencia bancaria" },
                new SelectListItem { Value = "Caja chica (Efectivo)", Text = "Caja chica (Efectivo)" },
                new SelectListItem { Value = "TC", Text = "TC" },
                new SelectListItem { Value = "Cheque", Text = "Cheque" },
                new SelectListItem { Value = "Transferencias a terceros", Text = "Transferencias a terceros" },
                new SelectListItem { Value = "Otro", Text = "Otro" } };

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetInmuebles()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmuebles dataInmuebles = new();
            return Json(dataInmuebles.GetCmbInmuebles(idCartera, IdUsuario));
        }

        [HttpGet]
        public ActionResult LoadXML()
        {

            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion                       

            //ViewBag.ListaPeriodo = new DataPartidaPresupuestal().GetPeriodos();
            DataSelectListItem dataSelectListItem = new DataSelectListItem();
            //ViewBag.ListaInmuebles = dataSelectListItem.InmueblesByUsuario(usuario.idCartera, usuario.IdUsuario);
            //ViewBag.ListaInmuebles = ObtieneListaInmuebles(usuario.IdUsuario, usuario.idCartera);


            //List<SelectListItem> categorias = new DataPartidaPresupuestal().GetCategoriaEgresos();
            //ViewBag.Categoria = new SelectList(categorias, "Value", "Text");


            var model = new MovimientosGastos();
            model.ListaFacturas = new List<Factura>();
            //model.ListaMovimientosPresupuestalCargaDetalleFacturas = new List<MovimientosPresupuestalCargaDetalleFacturas>();
            //model.MovimientosPartidasCargadas = new MovimientosPartidasCargadas();

            //ViewBag.IdCartera = usuario.idCartera;


            return View(model);
        }

        [HttpGet]
        public IActionResult LoadPage()
        {
            TempData["partidas"] = string.Empty;
            return Json(new JsonResult("success"));
        }

        [HttpPost]
        public ActionResult ObtieneRutaCarpeta(List<IFormFile> archivoFactura)
        {
            foreach (var formFile in archivoFactura)
            {
                string ruta = formFile.FileName.ToString();
                if (ruta != "" && ruta != null)
                {
                    string[] Carpeta = ruta.Split('/');

                    return Json(new { success = true, ruta = Carpeta[0] });
                }
            }
            return Json(new { success = true, ruta = "" });
        }

        [HttpPost]
        public async Task<ActionResult> ImportarXML(List<IFormFile> archivoFactura, List<Factura> facturas, string periodo, List<IFormFile> archivoFactura2)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermisoSinlistTransf_Opciones(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion
            periodo = "2025";                      

            var model = await ObtieneListaFacturasAsync(archivoFactura2);

            List<Factura> resultT = new();
            List<Factura> resultT2 = new();
            List<Factura> resultT3 = new();

            foreach (var row in model)
            {
                resultT.Add(new Factura()
                {
                    Concepto = row.Concepto,
                    Proveedor = row.Proveedor,
                    FechaContable = row.FechaContable,
                    FolioFiscal = row.FolioFiscal,
                    ConceptoComplementario = row.ConceptoComplementario,
                    FechaSolicitud = row.FechaSolicitud,
                    ClaveSolicitud = row.ClaveSolicitud,
                    Moneda = row.Moneda,
                    TipoCambio = row.TipoCambio,
                    FormaPago = row.FormaPago,
                    FechaReembolso = row.FechaReembolso,
                    Folio = row.Folio,
                    Serie = row.Serie,
                    ImporteUSD = row.ImporteUSD,
                    Importe = row.Importe,
                    Descuento = row.Descuento,
                    Subtotal = row.Subtotal,
                    IVA = row.IVA,
                    IEPS = row.IEPS,
                    IVARet = row.IVARet,
                    ISR = row.ISR,
                    Total = row.Total,
                    FolioGastoNoDeducible = row.FolioGastoNoDeducible,
                    Path = row.Path,
                    RFC = row.RFC,
                    IdProveedor = row.IdProveedor,
                    RowIndex = row.RowIndex,
                    DescuentoDolares = row.DescuentoDolares,
                    IVADolares = row.IVADolares,
                    IEPSDolares = row.IEPSDolares,
                    IVARetDolares = row.IVARetDolares,
                    ISRRetDolares = row.ISRRetDolares,
                    TotalDolares = row.TotalDolares,
                    ReceptorNombre = row.ReceptorNombre,
                    MessageError = row.MessageError,
                    FileNameXml = row.FileNameXml,
                    VersionCFDI = row.VersionCFDI,
                    TipoComprobante = row.TipoComprobante,
                    MetodoPago = row.MetodoPago,
                    ReceptorRFC = row.ReceptorRFC,
                });
            }

            int countError = 0;

            for (int i = 0; i < resultT.Count(); i++)
            {
                if (resultT[i].MessageError != null && resultT[i].MessageError != "")
                    countError++;
            }



            if (countError > 0 && facturas.Count() > 0)
            {
                if (facturas.Count() > 0)
                    facturas.ForEach(c => resultT3.Add(c));

                foreach (var capa in resultT)
                {
                    resultT3.RemoveAll(c => c.FolioFiscal == capa.FolioFiscal);

                }

                if (resultT3.Count() > 0)
                    resultT3.ForEach(c => resultT.Add(c));                

                ViewBag.FormasPagoItem = FormasPagos;
                ViewBag.GastosItem = new DataGastos().GetEgresosAll();
                ViewBag.InmueblesItem = new DataInmuebles().GetCmbInmuebles(idCartera, IdUsuario);
                ViewBag.FacutasNoCargadas = 0;
                //Aqui va la busqueda de proveedor
                return PartialView("_FacturasCargadas", resultT);
            }

            if (countError > 0 && facturas.Count() == 0)
            {
                ViewBag.FormasPagoItem = FormasPagos;
                ViewBag.GastosItem = new DataGastos().GetEgresosAll();
                ViewBag.InmueblesItem = new DataInmuebles().GetCmbInmuebles(idCartera, IdUsuario);
                ViewBag.FacutasNoCargadas = 0;

                //Aqui va la busqueda de proveedor
                return PartialView("_FacturasCargadas", resultT);

            }



            /*Obtiene las facturas que no corresponden al periodo de la fecha contable*/
            var periodos = new string[] { "2024", "2025" };

            var listFacturaNoCargadas = from facturasPeriodo in model
                                        where 
                                            facturasPeriodo.FechaContable.Substring(0, 4) != periodo
                                        select facturasPeriodo;


            if (facturas.Count() > 0)
                facturas.ForEach(c => resultT3.Add(c));

            foreach (var capa in resultT)
            {
                resultT3.RemoveAll(c => c.FolioFiscal == capa.FolioFiscal);

            }

            if (resultT3.Count() > 0)
                resultT3.ForEach(c => resultT.Add(c));

            var listFacturaPorPeriodo = from facturasPeriodo in resultT
                                        where 
                                            facturasPeriodo.FechaContable.Substring(0, 4) == periodo
                                        select facturasPeriodo;

            resultT2 = listFacturaPorPeriodo.ToList();


            ViewBag.FormasPagoItem = FormasPagos;
            ViewBag.GastosItem = new DataGastos().GetEgresosAll();
            ViewBag.InmueblesItem = new DataInmuebles().GetCmbInmuebles(idCartera, IdUsuario);
            ViewBag.FacutasNoCargadas = listFacturaNoCargadas.Count();
            //Aqui va la busqueda de proveedor
            return PartialView("_FacturasCargadas", resultT2);

        }

        [HttpPost]
        public JsonResult GuardaMovimientos(List<Factura> movimientos)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            menu.ValidaPermisoSinlistTransf_Opciones(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims);
                
            #endregion                                         

            string foliosGuardados = "", foliosNoGuardados = "", foliosDuplicados = "";
            int foliosGuardadosCount = 0, foliosNoGuardadosCount = 0, foliosDuplicadosCount = 0;
            DataGastos dataMovimientos = new();
            foreach (var item in movimientos)
            {

                //item.FechaSolicitudPago = (string.IsNullOrEmpty(item.FechaSolicitudPago) ? DateTime.Now.ToString("yyyy-MM") : item.FechaSolicitudPago);
                //item.FechaReembolso = (string.IsNullOrEmpty(item.FechaReembolso) ? "1900-01-01" : item.FechaReembolso);
                //item.FolioGastoNoDeducible = string.IsNullOrEmpty(item.FolioGastoNoDeducible) ? "" : item.FolioGastoNoDeducible;
                //item.FechaContable = (string.IsNullOrEmpty(item.FechaSolicitudPago) ? DateTime.Now.ToString("yyyy-MM") : item.FechaSolicitudPago);
                //item.FechaContable = (string.IsNullOrEmpty(item.FechaContable) ? DateTime.Now.ToString("yyyy-MM") : item.FechaContable);
                item.Periodo = $"{(Convert.ToDateTime(item.FechaContable).ToString("yyyyMM"))}";
                //item.Periodo = Convert.ToDateTime(item.FechaReembolso).ToString("yyyyMM");

                bool existFolio = new DataGastos().ValidaExisteXML(item.FolioFiscal);
                var retorno = 0;

                if (!existFolio)
                {
                    retorno = dataMovimientos.GuardaMovimientoPresupuestal(item, IdUsuario.ToString());

                    foliosGuardadosCount++;

                    if (foliosNoGuardadosCount == 1)
                    {

                        foliosGuardados = item.FolioFiscal;
                    }
                    else
                    {
                        foliosGuardados = item.FolioFiscal + ", " + foliosGuardados;
                    }


                    //new DataMovimientosPartidaPresupuestal().InsertaLog(item.IdInmueble, item.Periodo, usuario.IdUsuario, "Importación de XML guardada");
                }
                else
                {
                    foliosDuplicadosCount++;

                    if (foliosDuplicadosCount == 1)
                    {

                        foliosDuplicados = item.FolioFiscal;
                    }
                    else
                    {
                        foliosDuplicados = foliosDuplicados + ", " + item.FolioFiscal ;
                    }
                }

                if (retorno == 0)
                {
                    //message = "Error al Guardar Movimiento, " + fechaLog + " Usuario: " + usuario.Username + " | Periodo: " + item.Periodo;
                    //dataEliminaMov.InsertaLog(0, item.IdInmueble, item.Periodo.ToString(), usuario.IdUsuario, usuario.idCartera, "Carga Movimientos a Presupuestos", message, message);

                }
                else
                {
                    //string message = "Movimiento Guardado Correctamente, " + fechaLog + " Usuario: " + usuario.Username + " | Periodo: " + item.Periodo;
                    //dataEliminaMov.InsertaLog(retorno, item.IdInmueble, item.Periodo.ToString(), usuario.IdUsuario, usuario.idCartera, "Carga Movimientos a Presupuestos", message, message);


                }
            }


            //MensajeLoad = $"Los siguientes folios se guardaron: {foliosGuardados}.\n Los siguientes folios no se guardaron: {foliosNoGuardados}";
            //MensajeLoad = "true";
            //MovimientosExitosos = foliosGuardados;
            //MovimientosFallidos = foliosNoGuardados;

            string[] objResult = { foliosGuardados, foliosNoGuardados, "/Gastos/LoadXML", foliosDuplicados };

            return Json(objResult);
        }

        private async Task<List<Factura>> ObtieneListaFacturasAsync(List<IFormFile> archivoFactura)
        {
            var lista = new List<Factura>();
            try
            {                
                string version = string.Empty;
                int contFilesXML = 0;
                int rowIndex = 0;
                int idProveedor = 0;
                foreach (var formFile in archivoFactura)
                {
                    if (formFile.ContentType.ToString().Equals("text/xml"))
                    {
                        contFilesXML++;
                        if (formFile.Length > 0)
                        {
                            string fileNameXml = formFile.FileName.ToString();
                            //Stream fileStream = formFile.OpenReadStream();
                            //fileStream.Flush();
                            XmlDocument document = new XmlDocument();


                            document.Load(formFile.OpenReadStream());


                            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Archivos/CFDIs");

                            //string fileName = $"Factura_{DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()}.xml";
                            string fileName = fileNameXml[(fileNameXml.LastIndexOf("/") + 1)..];

                            var fileNameWithPath = string.Concat(filePath, "//", fileName);

                            string l_version = "3.3";

                            XmlNodeList nodeList = document.GetElementsByTagName("cfdi:Comprobante");

                            l_version = document.GetElementsByTagName("cfdi:Comprobante").Item(0).Attributes.GetNamedItem("Version").InnerText;

                            //var itemMovimientoPartida = ObtieneItemFactura33(document, fileNameWithPath);
                            var itemMovimientoPartida = ObtieneItemFactura40(document, fileNameWithPath);

                            //if (l_version == "4.0")
                            //{
                            //    itemMovimientoPartida = ObtieneItemFactura40(document, fileNameWithPath);
                            //}


                            if (itemMovimientoPartida.MessageError != "")
                            {
                                itemMovimientoPartida.FileNameXml = fileNameXml;
                                //return new List<MovimientoPartidaPresupuestalFacturas>() { new MovimientoPartidaPresupuestalFacturas()
                                //{
                                //    MessageError = itemMovimientoPartida.MessageError
                                //} };
                            }

                            itemMovimientoPartida.PathLocalXML = fileNameWithPath;
                            itemMovimientoPartida.PathLocalPDF = fileNameWithPath.Replace("xml","pdf");

                            if (System.IO.File.Exists(itemMovimientoPartida.PathLocalXML))
                            {
                                System.IO.File.Delete(itemMovimientoPartida.PathLocalXML);
                            }

                            //if (System.IO.File.Exists(itemMovimientoPartida.PathLocalPDF))
                            //{
                            //    System.IO.File.Delete(itemMovimientoPartida.PathLocalPDF);
                            //}

                            using (var localFile = System.IO.File.OpenWrite(fileNameWithPath))
                            using (var uploadedFile = formFile.OpenReadStream())
                            {
                                await uploadedFile.CopyToAsync(localFile);
                            }

                            using (var stream = System.IO.File.Create(fileNameWithPath))
                            {

                                await formFile.CopyToAsync(stream);
                                await stream.FlushAsync();

                            }
                            //try
                            //{
                            //    IFormFile formFile1;
                            //    using (var stream = System.IO.File.OpenRead(formFile.FileName.ToString().Replace("xml", ".pdf")))
                            //    {

                            //        formFile1 = new FormFile(stream, 0, stream.Length, "", fileName.Replace("xml", ".pdf"));

                            //    }

                            //    using (var stream = System.IO.File.Create(itemMovimientoPartida.PathLocalPDF))
                            //    {

                            //        await formFile1.CopyToAsync(stream);
                            //        await stream.FlushAsync();

                            //    }
                            //}
                            //catch (Exception)
                            //{

                            //}


                            itemMovimientoPartida.RowIndex = rowIndex;
                            rowIndex++;

                            //idProveedor = new DataMovimientosPartidaPresupuestal().ObtieneProveerPorRfc(itemMovimientoPartida.RFC).IdProveedor;

                            itemMovimientoPartida.IdProveedor = 0;

                            string moneda = "MXN";
                            if (itemMovimientoPartida.Moneda.ToLower() == moneda.ToLower())
                                itemMovimientoPartida.TipoCambio = "1";
                            else
                                itemMovimientoPartida.TipoCambio = "";                            

                            ////INICIA ALGORITMO IMPLEMENTADO PARA SUBTOTAL E IMPORTE CORRECTO
                            /*Caso normal sin descuento*/
                            if (Math.Abs((itemMovimientoPartida.Importe - itemMovimientoPartida.IVA) - itemMovimientoPartida.Subtotal) < 1 && itemMovimientoPartida.Descuento == 0)
                            {
                                itemMovimientoPartida.Subtotal = Math.Round(itemMovimientoPartida.Importe - itemMovimientoPartida.IVA, 2);
                            }
                            else if (Math.Abs(itemMovimientoPartida.Importe - itemMovimientoPartida.Subtotal) < 1 && itemMovimientoPartida.Descuento > 0)
                            {//Caso normal con descuento
                                itemMovimientoPartida.Subtotal = Math.Round(itemMovimientoPartida.Importe - itemMovimientoPartida.Descuento, 2);
                            }
                            else if (itemMovimientoPartida.Importe != itemMovimientoPartida.Subtotal && Math.Abs((itemMovimientoPartida.Importe - itemMovimientoPartida.IVA) - itemMovimientoPartida.Subtotal) < 1)
                            {//importe ya contiene iva     
                                itemMovimientoPartida.Subtotal = Math.Round((itemMovimientoPartida.Importe - itemMovimientoPartida.IVA), 2);
                            }
                            else if (Math.Abs((itemMovimientoPartida.Importe - itemMovimientoPartida.Descuento) - itemMovimientoPartida.Subtotal) < 1)
                            {//Importe ya contiene el descuento, se tendria que tomar el subtotal
                                itemMovimientoPartida.Subtotal = Math.Round(itemMovimientoPartida.Importe - itemMovimientoPartida.Descuento, 2);
                            }
                            else if (Math.Abs((itemMovimientoPartida.Importe + itemMovimientoPartida.IEPS) - itemMovimientoPartida.Subtotal) < 1)
                            {//Subtotal contiene IEPS, se tiene que tomar el importe
                                itemMovimientoPartida.Subtotal = Math.Round(itemMovimientoPartida.Importe - itemMovimientoPartida.Descuento, 2);
                            }

                            if (Math.Abs((itemMovimientoPartida.Subtotal + itemMovimientoPartida.IVA) - itemMovimientoPartida.Importe) < 1 && itemMovimientoPartida.IVA > 0)
                            {
                                itemMovimientoPartida.Importe = itemMovimientoPartida.Importe - itemMovimientoPartida.IVA;
                            }
                            //////FIN DEL ALGORITMO
                            ///

                            if (itemMovimientoPartida.RFCEmisor == "CSS160330CP7" && itemMovimientoPartida.DescuentoCFE > 0)
                            {
                                itemMovimientoPartida.Total = (itemMovimientoPartida.IVA) - (itemMovimientoPartida.DescuentoCFE - itemMovimientoPartida.Importe);
                                itemMovimientoPartida.Subtotal = (itemMovimientoPartida.Importe - itemMovimientoPartida.DescuentoCFE);
                                itemMovimientoPartida.Importe = (itemMovimientoPartida.Importe - itemMovimientoPartida.DescuentoCFE);

                            }


                            itemMovimientoPartida.DescuentoDolares = itemMovimientoPartida.Descuento;
                            itemMovimientoPartida.IVADolares = itemMovimientoPartida.IVA;
                            itemMovimientoPartida.IEPSDolares = itemMovimientoPartida.IEPS;
                            itemMovimientoPartida.IVARetDolares = itemMovimientoPartida.IVARet;
                            itemMovimientoPartida.ISRRetDolares = itemMovimientoPartida.ISR;
                            itemMovimientoPartida.TotalDolares = itemMovimientoPartida.Total;



                            lista.Add(itemMovimientoPartida);
                        }
                    }
                    else
                    {
                        if (formFile.ContentType.ToString().Equals("application/pdf"))
                        {
                            string fileNamePDF = formFile.FileName.ToString();
                            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Archivos/CFDIs");
                            string fileName = fileNamePDF[(fileNamePDF.LastIndexOf("/") + 1)..];
                            var fileNameWithPath = string.Concat(filePath, "//", fileName);

                            if (System.IO.File.Exists(fileNameWithPath))
                            {
                                System.IO.File.Delete(fileNameWithPath);
                            }

                            using (var stream = System.IO.File.Create(fileNameWithPath))
                            {

                                await formFile.CopyToAsync(stream);
                                await stream.FlushAsync();

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return lista;
        }

        private Factura ObtieneItemFactura40(XmlDocument document, string fileName)
        {
            var factura = new Factura();

            factura.MessageError = "";
            if (ObtieneEmisorNombre(document) == "")
            {

                factura.MessageError = "El Emisor de la factura no cuenta con nombre";
                //return facutra;
            }

            if (ObtieneReceptorNombre(document) == "")
            {

                    factura.MessageError = "El Receptor de la factura no cuenta con nombre";
                // return facutra;
            }

            factura.VersionCFDI = ObtieneVersion(document);
            factura.TipoComprobante = ObtieneTipoComprobante(document);
            factura.Concepto = ObtieneConcepto(document);
            factura.Path = fileName;
            factura.FechaContable = ObtieneFechaContable(document);
            factura.Moneda = ObtieneMoneda(document);
            factura.TipoCambio = ObtieneTipoCambio(document);
            factura.Folio = ObtieneFolio(document);
            factura.Serie = ObtieneSerie(document);
            factura.MetodoPago = ObtieneMetodoPago(document);
            factura.FormaPago = ObtieneFormaPago(document);
            factura.ReceptorNombre = ObtieneReceptorNombre(document);
            factura.ReceptorRFC = ObtieneReceptorRFC(document);

            if (factura.Moneda.ToUpper() == "USD")
            {
                factura.ImporteUSD = ObtieneImporte(document);
            }
            else
            {
                factura.Importe = ObtieneImporte(document);
            }


            factura.Descuento = ObtieneDescuento(document);
            factura.Total = ObtieneTotal(document);
            factura.Proveedor = ObtieneEmisorNombre(document);
            factura.Subtotal = ObtieneSubTotal(document);
            factura.AcreedorFactura = ObtieneAcreedorFactura(document);
            factura.RFC = ObtieneRFC(document);
            factura.RFCEmisor = ObtieneRFCEmisor(document);

            XmlNodeList listComplementos = ObtieneComplemento(document);

            if (listComplementos != null && listComplementos.Count > 0)
            {

                foreach (XmlNode item in listComplementos)
                {
                    factura.FolioFiscal = ObtieneUUID(item.ChildNodes);
                    factura.Subtotal += ObtieneTotalTrasladados(item.ChildNodes);
                    if (factura.Moneda.ToUpper() == "USD")
                    {
                        factura.ImporteUSD += ObtieneTotalTrasladados(item.ChildNodes);
                    }
                    else
                    {
                        factura.Importe += ObtieneTotalTrasladados(item.ChildNodes);
                    }
                    //facutra.Importe += ObtieneTotalTrasladados(item.ChildNodes);
                    // facutra.ISR += ObtieneTotalRetenciones(item.ChildNodes);
                }
            }

            XmlNodeList listRet = ObtieneRetenciones(document);

            if (listRet != null && listRet.Count > 0)
            {
                int count = 0;
                foreach (XmlNode item in listRet)
                {
                    if (count > 0)
                    {
                        factura.ISR += ObtieneTotalRetencionesISR(item.ChildNodes);
                        factura.IVARet += ObtieneTotalRetencionesIvaRet(item.ChildNodes);

                    }

                    count++;
                }
            }


            XmlNodeList listImpuestos = ObtieneImpuestos(document);
            double importe = 0;
            string xmlTotal = "";

            string nodoConcepto = string.Empty;
            string nodoImporte = string.Empty;
            double nodoDescuento = 0;

            XmlNodeList listCredito = ObtieneCreditoAplicado(document);
            foreach (XmlElement nodo in listCredito)
            {

                foreach (XmlElement elemento in nodo)
                {
                    if (elemento.LocalName.Equals("Conceptos"))
                    {
                        foreach (XmlNode hijo in elemento.ChildNodes)
                        {
                            if (hijo.InnerText.Trim().Equals("Credito Aplic. Fac."))
                            {
                                nodoConcepto = hijo.Name;
                                nodoImporte = nodoConcepto.Replace("Concepto", "Importe");
                                break;
                            }

                        }
                    }


                    if (nodoImporte.Trim().Length > 0)
                    {
                        if (elemento.LocalName.Equals("Importes"))
                        {
                            foreach (XmlNode hijoImporte in elemento.ChildNodes)
                            {
                                if (hijoImporte.Name.Equals(nodoImporte))
                                {
                                    nodoDescuento = Math.Abs(Convert.ToDouble(hijoImporte.InnerText));
                                    break;
                                }
                            }
                        }
                    }

                    if (elemento.LocalName.Equals("IMPTOTALXML"))
                    {
                        factura.TotalCFE = Convert.ToDouble(elemento.InnerText);
                    }


                }
            }

            factura.DescuentoCFE = nodoDescuento;


            foreach (XmlNode item in listImpuestos)
            {
                xmlTotal = ObtieneTotalImpuestosTrasladados(item);

                foreach (XmlNode itmImpuesto in item.ChildNodes)
                {
                    if (itmImpuesto.LocalName.Equals("Traslados"))
                    {
                        foreach (XmlNode itmTraslado in itmImpuesto.ChildNodes)
                        {
                            string tipoImpuesto = itmTraslado.Attributes.GetNamedItem("Impuesto").InnerText;
                            if (itmTraslado.Attributes.GetNamedItem("TipoFactor").InnerText != "Tasa")
                                importe = 0;
                            else
                                importe = Convert.ToDouble(itmTraslado.Attributes.GetNamedItem("Importe").InnerText);

                            if (string.IsNullOrEmpty(xmlTotal))
                            {
                                //001 = ISR
                                if (tipoImpuesto.Equals("002")) //IVA
                                {
                                    factura.IVA += importe;
                                }

                            }


                            if (tipoImpuesto.Equals("003")) //IEPS
                            {
                                factura.IEPS += importe;
                            }

                        }

                    }

                }
            }

            //Aqui va la parte de Impuestos retenidos

            //XmlNodeList listIvaRetenido = ObtieneImpuestos(document);

            //if (listIvaRetenido != null && listIvaRetenido.Count > 0)
            //{

            //    foreach (XmlNode item in listIvaRetenido)
            //    {

            //        facutra.IVARet += Convert.ToDouble(ObtieneTotalImpuestosRetenidos(item));
            //    }
            //}
            // facutra.IVARet = 0;

            //facutra.DescuentoDolares = facutra.Descuento;
            //facutra.IVADolares = facutra.IVA;
            //facutra.IEPSDolares = facutra.IEPS;
            //facutra.IVARetDolares = facutra.IVARet;
            //facutra.ISRRetDolares = facutra.ISR;
            //facutra.TotalDolares = facutra.Total;

            if (factura.TipoComprobante == "E")
            {
                factura.Importe = factura.Importe * -1;
                factura.Subtotal = factura.Subtotal * -1;
                factura.IVA = factura.IVA * -1;
                factura.IVARet = factura.IVARet * -1;
                factura.Total = factura.Total * -1;
            }

            return factura;
        }

        private string ObtieneRFCEmisor(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("cfdi:Emisor").Item(0).Attributes.GetNamedItem("Rfc").InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        private string ObtieneEmisorNombre(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("cfdi:Emisor").Item(0).Attributes.GetNamedItem("Nombre").InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        private string ObtieneRFC(XmlDocument document)
        {
            try
            {                
                return document.GetElementsByTagName("cfdi:Emisor").Item(0).Attributes.GetNamedItem("Rfc").InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        private string ObtieneReceptorNombre(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("cfdi:Receptor").Item(0).Attributes.GetNamedItem("Nombre").InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        private string ObtieneReceptorRFC(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("cfdi:Receptor").Item(0).Attributes.GetNamedItem("Rfc").InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        private string ObtieneTotalImpuestosTrasladados(XmlNode item)
        {
            string totalImpuesto = "";
            try
            {
                totalImpuesto = item.Attributes.GetNamedItem("TotalImpuestosTrasladados")?.InnerText;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                totalImpuesto = "";
            }

            return totalImpuesto;
        }

        private double ObtieneTotalImpuestosRetenidos(XmlNode item)
        {
            double totalImpuesto = 0;
            try
            {
                totalImpuesto = Convert.ToDouble(item.Attributes.GetNamedItem("TotalImpuestosRetenidos").InnerText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                totalImpuesto = 0;
            }

            return totalImpuesto;
        }

        private string ObtieneConcepto(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("cfdi:Conceptos").Item(0).ChildNodes.Item(0).Attributes.GetNamedItem("Descripcion").InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }

        }

        private XmlNodeList ObtieneImpuestos(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("cfdi:Impuestos");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        private double ObtieneTotalTrasladados(XmlNodeList nodeList)
        {
            double totalDeTraslados = 0;
            try
            {
                foreach (XmlNode nodeItem in nodeList)
                {
                    if (nodeItem.Name.Equals("implocal:ImpuestosLocales"))
                    {
                        totalDeTraslados += Convert.ToDouble(nodeItem.Attributes.GetNamedItem("TotaldeTraslados").InnerText);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }

            return totalDeTraslados;
        }

        private double ObtieneTotalRetenciones(XmlNodeList nodeList)
        {
            double totalRetenciones = 0;

            try
            {
                foreach (XmlNode nodeItem in nodeList)
                {
                    if (nodeItem.Name.Equals("implocal:ImpuestosLocales"))
                    {
                        totalRetenciones += Convert.ToDouble(nodeItem.Attributes.GetNamedItem("TotaldeRetenciones").InnerText);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }

            return totalRetenciones;
        }

        private double ObtieneTotalRetencionesIvaRet(XmlNodeList nodeList)
        {
            double totalRetenciones = 0;

            try
            {
                foreach (XmlNode nodeItem in nodeList)
                {
                    if (nodeItem.Name.Equals("cfdi:Retencion"))
                    {
                        string tipoImpuesto = nodeItem.Attributes.GetNamedItem("Impuesto").InnerText;

                        //002 = IVA Ret.
                        if (tipoImpuesto.Equals("002"))
                        {
                            totalRetenciones = Convert.ToDouble(nodeItem.Attributes.GetNamedItem("Importe").InnerText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }

            return totalRetenciones;
        }

        private double ObtieneTotalRetencionesISR(XmlNodeList nodeList)
        {
            double totalRetenciones = 0;

            try
            {
                foreach (XmlNode nodeItem in nodeList)
                {
                    if (nodeItem.Name.Equals("cfdi:Retencion"))
                    {
                        string tipoImpuesto = nodeItem.Attributes.GetNamedItem("Impuesto").InnerText;
                        //002 = ISR.
                        if (tipoImpuesto.Equals("001"))
                        {
                            totalRetenciones = Convert.ToDouble(nodeItem.Attributes.GetNamedItem("Importe").InnerText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }

            return totalRetenciones;
        }

        private string ObtieneUUID(XmlNodeList nodeList)
        {
            string uuid = "";
            try
            {
                foreach (XmlNode nodeItem in nodeList)
                {
                    if (nodeItem.Name.Equals("tfd:TimbreFiscalDigital"))
                    {
                        uuid = nodeItem.Attributes.GetNamedItem("UUID").InnerText;
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }

            return uuid;
        }

        private XmlNodeList ObtieneComplemento(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("cfdi:Complemento");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        private XmlNodeList ObtieneRetenciones(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("cfdi:Retenciones");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        private XmlNodeList ObtieneCreditoAplicado(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("clsRegArchFact");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        private string ObtieneAcreedorFactura(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("cfdi:Emisor").Item(0).Attributes.GetNamedItem("Nombre").InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        private double ObtieneSubTotal(XmlDocument document)
        {
            try
            {
                return Convert.ToDouble(document.GetElementsByTagName("cfdi:Comprobante").Item(0).Attributes.GetNamedItem("SubTotal").InnerText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        private double ObtieneTotal(XmlDocument document)
        {
            try
            {

                return Convert.ToDouble(document.GetElementsByTagName("cfdi:Comprobante").Item(0).Attributes.GetNamedItem("Total").InnerText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        private double ObtieneDescuento(XmlDocument document)
        {
            try
            {
                return Convert.ToDouble(document.GetElementsByTagName("cfdi:Comprobante")?.Item(0).Attributes?.GetNamedItem("Descuento")?.InnerText);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        private double ObtieneImporte(XmlDocument document)
        {
            try
            {
                return Convert.ToDouble(document.GetElementsByTagName("cfdi:Comprobante").Item(0).Attributes.GetNamedItem("SubTotal").InnerText); //Revisar que importe tomar
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        private string ObtieneSerie(XmlDocument document)
        {
            try
            {
               
                return document.GetElementsByTagName("cfdi:Comprobante")?.Item(0)?.Attributes?.GetNamedItem("Serie")?.InnerText;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        private string ObtieneFolio(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("cfdi:Comprobante").Item(0).Attributes.GetNamedItem("Folio").InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        private string ObtieneMetodoPago(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("cfdi:Comprobante").Item(0).Attributes.GetNamedItem("MetodoPago").InnerText;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        private string ObtieneFormaPago(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("cfdi:Comprobante").Item(0).Attributes.GetNamedItem("FormaPago").InnerText;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        private string ObtieneTipoComprobante(XmlDocument document)
        {
            try
            {                
                return document.GetElementsByTagName("cfdi:Comprobante").Item(0).Attributes.GetNamedItem("TipoDeComprobante").InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "I";
            }
        }

        private string ObtieneVersion(XmlDocument document)
        {
            try
            {                
                return document.GetElementsByTagName("cfdi:Comprobante").Item(0).Attributes.GetNamedItem("Version").InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "3.3";
            }
        }

        private string ObtieneTipoCambio(XmlDocument document)
        {
            try
            {
                try
                {
                    return document.GetElementsByTagName("cfdi:Comprobante")?.Item(0)?.Attributes?.GetNamedItem("TipoCambio")?.InnerText;
                }
                catch (Exception)
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        private string ObtieneMoneda(XmlDocument document)
        {
            try
            {
                return document.GetElementsByTagName("cfdi:Comprobante").Item(0).Attributes.GetNamedItem("Moneda").InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        private string ObtieneFechaContable(XmlDocument document)
        {
            try
            {
                return Convert.ToDateTime(document.GetElementsByTagName("cfdi:Comprobante").Item(0).Attributes.GetNamedItem("Fecha").InnerText).ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }
    }
}
