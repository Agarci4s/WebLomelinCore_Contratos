using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using System.Collections.Generic;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebColliersCore;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using WebLomelinCore.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using NPOI.SS.Formula.Functions;
using System.Linq;
using Humanizer.Localisation;
using System.Data;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Components;
using System.Drawing;
using MySqlX.XDevAPI.Common;
using System.Runtime.Intrinsics.X86;

namespace WebLomelinCore.Controllers
{
    public class B_inmuebles_visitas_CedulaResumenMasivo : Controller
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

        // GET: B_inmuebles_visitas_CedulaResumenMasivo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 3;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            DataInmuebles dataInmuebles = new DataInmuebles();
            var result = dataInmuebles.Get(idCartera, IdUsuario).Find(x => x.id_b_inmuebles == id);
            //if (result == null || result.id_b_inmuebles != id)
            //    return Redirect("~/Home");

            //DataInmueblesComparativo dataInmueblesComparativo = new DataInmueblesComparativo();
            //List<B_inmuebles_comparativo> b_Inmuebles_Comparativos = dataInmueblesComparativo.Get(idCartera, IdUsuario, id);
            ViewData["id_b_inmuebles"] = id;
            TempData["id_b_inmuebles"] = id;

            //return View(b_Inmuebles_Comparativos);
            return View("Index2");
        }

        public IActionResult GetReport()
        {
            string filePath1 = Environment.CurrentDirectory;
            DataInmueblesVisita dataInmueblesVisita = new DataInmueblesVisita();
            int id = (int)TempData["id_b_inmuebles"];
            DataTable dataTableCR = new DataTable();

            //Masivo
            DataTable dataTableVisitas = dataInmueblesVisita.GetResumenCedularMasivo();
            foreach (DataRow item in dataTableVisitas.Rows)
            {
                DataTable dataTableCRAux = dataInmueblesVisita.GetResumenCedular(int.Parse(item["id_b_inmuebles"].ToString()));
                if (dataTableCR is null || dataTableCR.Rows.Count == 0)
                    dataTableCR = dataTableCRAux;
                else
                    dataTableCR.ImportRow(dataTableCRAux.Rows[0]);
            }

            StiReport report = new StiReport();
            string filePath = "";
            filePath = Directory.GetCurrentDirectory() + "\\Report\\CedulaResumen\\rptCedulaResumen2.2.mrt";



            report.Load(filePath);

            report.Dictionary.Databases.Clear();
            report.RegData("dtCedulaResumen", dataTableCR);
            report.RegData("dtCR1", dataTableCR);

            report.ExportDocument(StiExportFormat.Pdf, $"{filePath1}//CedulaResumen//all"+DateTime.Now.ToString("yyMMddHHmmss")+".pdf");


            return StiNetCoreViewer.InteractionResult(this, report);
        }

        public IActionResult ViewerEvent()

        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }







        public ActionResult ExportarPDFs()
        {
            string filePath1 = Environment.CurrentDirectory;
            DataInmueblesVisita dataInmueblesVisita = new DataInmueblesVisita();
            //int id = (int)TempData["id_b_inmuebles"];

            DataTable dataTableVisitas = dataInmueblesVisita.GetResumenCedularMasivo();
            //DataTable dataTableCR = dataInmueblesVisita.GetResumenCedular(id);
            DataTable dataTableCR = null;

            StiReport report = new StiReport();
            string filePath = "";
            filePath = Directory.GetCurrentDirectory() + "\\Report\\CedulaResumen\\rptCedulaResumen.mrt";

            report.Load(filePath);

            foreach (DataRow item in dataTableVisitas.Rows)
            {
                dataTableCR = dataInmueblesVisita.GetResumenCedular(int.Parse(item["id_b_inmuebles"].ToString()));

                if (System.IO.File.Exists(filePath1 + dataTableCR.Rows[0]["PathExterior1"].ToString()))
                {
                    StiImage stiImage = report.GetComponents()["Exterior1"] as StiImage;
                    stiImage.Stretch = true;
                    stiImage.AspectRatio = false;
                    Image myImage = Image.FromFile(filePath1 + dataTableCR.Rows[0]["PathExterior1"].ToString());
                    stiImage.Image = myImage;
                }

                if (System.IO.File.Exists(filePath1 + dataTableCR.Rows[0]["PathExterior2"].ToString()))
                {
                    StiImage stiImage2 = report.GetComponents()["Exterior2"] as StiImage;
                    stiImage2.Stretch = true;
                    stiImage2.AspectRatio = false;
                    Image myImage2 = Image.FromFile(filePath1 + dataTableCR.Rows[0]["PathExterior2"].ToString());
                    stiImage2.Image = myImage2;
                }

                if (System.IO.File.Exists(filePath1 + dataTableCR.Rows[0]["PathInterior1"].ToString()))
                {
                    StiImage stiImage3 = report.GetComponents()["Interior1"] as StiImage;
                    stiImage3.Stretch = true;
                    stiImage3.AspectRatio = false;
                    Image myImage3 = Image.FromFile(filePath1 + dataTableCR.Rows[0]["PathInterior1"].ToString());
                    stiImage3.Image = myImage3;
                }

                if (System.IO.File.Exists(filePath1 + dataTableCR.Rows[0]["PathInterior2"].ToString()))
                {
                    StiImage stiImage4 = report.GetComponents()["Interior2"] as StiImage;
                    stiImage4.Stretch = true;
                    stiImage4.AspectRatio = false;
                    Image myImage4 = Image.FromFile(filePath1 + dataTableCR.Rows[0]["PathInterior2"].ToString());
                    stiImage4.Image = myImage4;
                }

                report.Dictionary.Databases.Clear();
                report.RegData("dtCedulaResumen", dataTableCR);
                report.RegData("dtCR", dataTableCR);
                report.RegData("dtCR1", dataTableCR);

                report.Render(false);
                report.ExportDocument(StiExportFormat.Pdf, $"{filePath1}//CedulaResumen//{dataTableCR.Rows[0]["nombrePDF"].ToString()}.pdf");
            }

            return View();
        }

    }
}
