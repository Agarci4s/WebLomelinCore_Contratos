using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Google.Protobuf.WellKnownTypes;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.Table;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebColliersCore;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebLomelinCore.Data;
using static Stimulsoft.Report.Func;
using static Stimulsoft.Report.Import.OleUnit;
using static Stimulsoft.Report.StiOptions.Export;

namespace WebLomelinCore.Controllers
{
    public class B_inmueblesContratoReporteController : Controller
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

        // GET: InmueblesVisitasReporteController
        public ActionResult Index()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);

            //envìo de correos
            DataEmail dataEmail = new DataEmail();
            //dataEmail.EnviaCorreo(idCartera,IdUsuario);
            //DataCG dataCG = new DataCG();
            //ViewBag.lstFechas = dataCG.fecha();
            //ViewBag.lstEstatusVisita = dataCG.b_cg_estatus_visita_inmueble(1);

            return View();
        }

        // GET: InmueblesVisitasReporteController
        public ActionResult ProximosVencer()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion            

            //envìo de correos
            //DataEmail dataEmail = new DataEmail();
            //dataEmail.EnviaCorreo(idCartera, IdUsuario);
            DataSelectListItem dataSelectListItem = new DataSelectListItem();
            ViewBag.Campos = dataSelectListItem.getCamposParametrizadosListContratos;

            return View();
        }



        [HttpPost]
        public ActionResult getListadoContratosParametrizado(int Campo, int Meses)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion            

            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            List<B_inmuebles_contrato> reporte = dataInmueblesContratos.GetReporte(idCartera, IdUsuario, Campo, Meses);

            ViewBag.Campo = Campo; // == 1 ? "Fecha de Termino" : "Fecha de Revisión";

            return PartialView("_ListContratosPV", reporte);
        }

        [HttpPost]
        public ActionResult ReporteVisita(int tipo, DateTime fecha, DateTime fecha2)
        {


            if (fecha > fecha2)
            {
                DateTime aux = fecha2;
                fecha2 = fecha;
                fecha = aux;
            }

            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            List<B_inmuebles_contrato> b_Inmuebles_Contrato_Correos = new List<B_inmuebles_contrato>();

            bool correo_enviado;
            if (tipo == 1)
                correo_enviado = true;
            else
                correo_enviado = false;

            b_Inmuebles_Contrato_Correos = dataInmueblesContratos.GetReporte(idCartera, IdUsuario, correo_enviado, fecha, fecha2);

            return PartialView("List", b_Inmuebles_Contrato_Correos);
        }

        // GET: InmueblesVisitasReporteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InmueblesVisitasReporteController/Create
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

        // GET: InmueblesVisitasReporteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InmueblesVisitasReporteController/Edit/5
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

        // GET: InmueblesVisitasReporteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InmueblesVisitasReporteController/Delete/5
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
        public ActionResult getListadoContratosParametrizadoReporte(int Campo, int Meses)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion            

            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();

            TempData["idCartera"] = idCartera;
            TempData["IdUsuario"] = IdUsuario;
            TempData["Campo"] = Campo;
            TempData["Meses"] = Meses;


            //List<B_inmuebles_contrato> reporte = dataInmueblesContratos.GetReporte(idCartera, IdUsuario, Campo, Meses);

            //ViewBag.Campo = Campo; // == 1 ? "Fecha de Termino" : "Fecha de Revisión";

            return PartialView("_ListContratosPVReport");
        }


       
        //[HttpPost]
        public IActionResult GetReport()
        {
            //bool correo_enviado = (bool)TempData["correo_enviado"];
            int idCartera = (int)TempData["idCartera"];
            int IdUsuario = (int)TempData["IdUsuario"];
            int Campo = (int)TempData["Campo"];
            int Meses = (int)TempData["Meses"];
            //DateTime fecha = (DateTime)TempData["fecha"];
            //DateTime fecha2 = (DateTime)TempData["fecha2"];

            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            List<B_inmuebles_contrato> reporte = dataInmueblesContratos.GetReporte(idCartera, IdUsuario, Campo, Meses);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(reporte);
            DataTable pDt = JsonConvert.DeserializeObject<DataTable>(json);

            //DataTable dataTableCR = dataInmueblesVisita.GetResumenCedular(id);


          







            StiReport report = new StiReport();
            string filePath = "";
            filePath = Directory.GetCurrentDirectory() + "\\Report\\ContratoVencerListado.mrt";


            report.Load(filePath);
            report.Dictionary.Databases.Clear();
            report.RegData("Demo", pDt);

            return StiNetCoreViewer.InteractionResult(this, report);


        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }

        [HttpPost]
        public ActionResult Enviar(string movAut, int meses, int campo)
        {
            List<B_inmuebles_contrato> lista = new List<B_inmuebles_contrato>();

            string TmpDataStr = movAut.Replace("&quot;", "\"");
            TmpDataStr = TmpDataStr.Replace("12:00:00 a. m.", "");
            lista = JsonConvert.DeserializeObject<List<B_inmuebles_contrato>>(TmpDataStr);

            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            List<B_inmuebles_contrato> listaMovimientos = new List<B_inmuebles_contrato>();
            string mensaje = "";
            ModelState.Clear();

            B_inmuebles_contrato Movimiento = new B_inmuebles_contrato();
            foreach (var item in lista)
            {
                if (item.Check && !string.IsNullOrEmpty(item.email))
                {
                    Movimiento = item;
                    string lblFecha = "";
                    string fldFecha;
                    DataEnvioEmail dataMail = new DataEnvioEmail();
                    StringBuilder html = new StringBuilder();
                    string _subject = "Contrato próximo a vencer";
                    if (campo == 1)
                    {
                        html.AppendLine($"Estimad@, le informamos que el (los) siguiente (s) Contrato(s) de Arrendamiento vencerá pronto:");
                        lblFecha = "Fecha de Termino";
                        fldFecha = item.fechatermino;
                        _subject = "Contrato próximo a vencer";
                    }
                    else
                    {
                        html.AppendLine($"Estimad@, le informamos que al (los) siguiente (s) Contrato(s) de Arrendamiento le corresponde el incremento anual el presente mes:");
                        lblFecha = "Fecha de Revisión";
                        fldFecha = item.fecharevision;
                        _subject = "Contrato próximo a incremento anual";
                    }

                    DataEmail modelMail = new DataEmail()
                    {
                        Receivers = Movimiento.email,
                        //Bcc = dataOrdenCompra.getCorreosAutorizacion(Movimiento.Id, usuario.IdUsuario),
                        Subject = _subject,
                        //Attachments = dataAttachments,
                        IsHtmlFormat = true
                    };



                    html.AppendLine("<header> ");
                    html.AppendLine("    <style> ");

                    html.AppendLine("       table tr th {");
                    html.AppendLine("       cursor: pointer;");
                    html.AppendLine("           -webkit - user - select: none;");
                    html.AppendLine("           -moz - user - select: none;");
                    html.AppendLine("           -ms - user - select: none;");
                    html.AppendLine("           user - select: none;");
                    html.AppendLine("       }");

                    html.AppendLine(" ");
                    html.AppendLine("        .tableFixHead { ");
                    html.AppendLine("        overflow-y: auto; ");
                    html.AppendLine("        height: 480px; ");
                    html.AppendLine("        } ");
                    html.AppendLine(" ");
                    html.AppendLine("        .tableFixHead table { ");
                    html.AppendLine("        border-collapse: collapse; ");
                    html.AppendLine("        width: 100%; ");
                    html.AppendLine("        } ");
                    html.AppendLine(" ");
                    html.AppendLine("        .tableFixHead th, ");
                    html.AppendLine("        .tableFixHead td { ");
                    html.AppendLine("        padding: 8px 16px; ");
                    html.AppendLine("        } ");
                    html.AppendLine(" ");
                    html.AppendLine("        .tableFixHead td { ");
                    html.AppendLine("        text-align: center; ");
                    html.AppendLine("        } ");
                    html.AppendLine(" ");
                    html.AppendLine("        .tableFixHead th { ");
                    html.AppendLine("        position: sticky; ");
                    html.AppendLine("        top: 0; ");
                    html.AppendLine("        /* background-color: white; ");
                    html.AppendLine("        border: 0px solid white; */ ");
                    html.AppendLine("        text-align: center; ");
                    html.AppendLine("        background: rgb(37, 64, 143); ");
                    html.AppendLine("        color: white; ");
                    html.AppendLine("        } ");
                    html.AppendLine(" ");
                    html.AppendLine("        .form-group-custom { ");
                    html.AppendLine("        margin-bottom: 6px; ");
                    html.AppendLine("        } ");
                    html.AppendLine(" ");
                    html.AppendLine("        .font-size-row { ");
                    html.AppendLine("        font-size: 12px !important; ");
                    html.AppendLine("        } ");
                    html.AppendLine(" ");
                    html.AppendLine("        .font-size-head { ");
                    html.AppendLine("        font-size: 12px !important; ");
                    html.AppendLine("        } ");
                    html.AppendLine(" ");
                    html.AppendLine("    </style> ");
                    html.AppendLine("</header> ");

                    html.AppendLine("<br/><br/>");
                    html.AppendLine("<div class=\"tableFixHead\">");
                    html.AppendLine("<table class=\"table table-responsive-sm table-sm small\" id=\"tab\">");
                    html.AppendLine("<thead class=\"font-size-head\">");
                    html.AppendLine("<tr>");
                    html.AppendLine("<th>Nombre</th>");
                    html.AppendLine("<th>Contrato</th>");
                    html.AppendLine("<th>Fecha Inicio</th>");
                    html.AppendLine($"<th>{lblFecha}</th>");
                    html.AppendLine("</tr>");
                    html.AppendLine("</thead>");
                    html.AppendLine("<tbody>");
                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{Movimiento.nombre}</td>");
                    html.AppendLine($"<td>{Movimiento.contrato}</td>");
                    html.AppendLine($"<td>{String.Format("{0:dd-MM-yyyy}", Movimiento.fechainicio)}</td>");
                    html.AppendLine($"<td>{String.Format("{0:dd-MM-yyyy}", fldFecha)}</td>");
                    html.AppendLine("</tr>");
                    html.AppendLine("</tbody>");
                    html.AppendLine("</table>");
                    html.AppendLine("</div>");

                    modelMail.Body = html.ToString();

                    dataMail.EmailSendOC(modelMail);
                }
                //}
                //}

                //return PartialView("_ReviewOrden", listaMovimientos);

            }

            //listaMovimientos = ObtieneListaMovimientosAutorizar(idInmueble, periodo, "P", usuario.IdUsuario);
            Response.StatusCode = (int)HttpStatusCode.OK;
            return PartialView("_ListContratosPV");
        }
    }
}
