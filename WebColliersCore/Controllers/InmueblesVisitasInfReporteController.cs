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
using DocumentFormat.OpenXml.Spreadsheet;

using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using static Stimulsoft.Report.StiOptions.Export;
using System.ComponentModel;
using static Stimulsoft.Report.Func;
using System.Diagnostics;

namespace WebLomelinCore.Controllers
{
    public class InmueblesVisitasInfReporteController : Controller
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

            DataCG dataCG = new DataCG();
            //ViewBag.lstFechas = dataCG.fecha();
            ViewBag.lstEstatusVisita = dataCG.b_cg_estatus_visita_inmueble(1);

            return View();
        }

        // GET: InmueblesVisitasReporteController/Details/5
        public ActionResult ExportarExcel(int id, int tipo, int filtro, DateTime fecha, DateTime fecha2)
        {
            string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string nombre = "na";

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

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            DataInmueblesVisita dataInmueblesVisita = new DataInmueblesVisita();
            DataInmuebles dataInmuebles = new DataInmuebles();
            List<B_inmuebles_visitas_inf_reporte> b_Inmuebles_Visitas = new List<B_inmuebles_visitas_inf_reporte>();
            List<B_inmuebles_reporte> b_Inmuebles = new List<B_inmuebles_reporte>();

            if (tipo == 1)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoInfReporte(idCartera, IdUsuario, filtro, true, false, fecha, fecha2);
                nombre = "Visitados";
            }
            if (tipo == 2)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoInfReporte(idCartera, IdUsuario, 2, false, false, fecha, fecha2);
                nombre = "Autorizados";
            }
            if (tipo == 3)
            {
                b_Inmuebles = dataInmuebles.GetReporte(idCartera, IdUsuario, fecha, fecha2);
                nombre = "SinVisitar";
            }

            if (tipo == 4)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoFueraPoliticaInfReporte(idCartera, IdUsuario, 2, fecha, fecha2);
                nombre = "Fuera_de_politica";
            }
            if (tipo == 5)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoInfReporte(idCartera, IdUsuario, 2, false, true, fecha, fecha2);
                nombre = "Especiales";
            }

            int columnas = 9;
            //crea el excel
            using (var libro = new ExcelPackage())
            {
                var worksheet = libro.Workbook.Worksheets.Add(nombre);
                if (tipo != 3)
                {
                    worksheet.Cells["A1"].LoadFromCollection(b_Inmuebles_Visitas, PrintHeaders: true);
                    for (var col = 1; col < b_Inmuebles_Visitas.Count + 1; col++)
                    {
                        worksheet.Column(col).AutoFit();
                    }
                    //worksheet.Cells["Q1"].Value = "mampara seguridad";
                    columnas = 17;
                }
                else
                {
                    worksheet.Cells["A1"].LoadFromCollection(b_Inmuebles, PrintHeaders: true);
                    for (var col = 1; col < b_Inmuebles.Count + 1; col++)
                    {
                        worksheet.Column(col).AutoFit();
                    }

                }
                // Agregar formato de tabla
                var tabla = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: b_Inmuebles_Visitas.Count + 1, toColumn: columnas), "Productos");
                tabla.ShowHeader = true;
                tabla.TableStyle = OfficeOpenXml.Table.TableStyles.Light6;
                tabla.ShowTotal = true;

                return File(libro.GetAsByteArray(), excelContentType, nombre + DateTime.Now.ToString("yyMMhhmm") + ".xlsx");
            }
        }

        [HttpPost]
        public ActionResult ReporteVisita(int tipo, int filtro, DateTime fecha, DateTime fecha2)
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

            DataInmueblesVisita dataInmueblesVisita = new DataInmueblesVisita();
            DataInmuebles dataInmuebles = new DataInmuebles();
            List<B_inmuebles_reporte> b_Inmuebles = new List<B_inmuebles_reporte>();
            List<B_inmuebles_visitas_inf_reporte> b_Inmuebles_Visitas = new List<B_inmuebles_visitas_inf_reporte>();

            if (tipo == 1)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoInfReporte(idCartera, IdUsuario, filtro, true, false, fecha, fecha2);
                return PartialView("ReporteVisita", b_Inmuebles_Visitas);

            }

            if (tipo == 2)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoInfReporte(idCartera, IdUsuario, 2, false, false, fecha, fecha2);
                return PartialView("ReporteVisita", b_Inmuebles_Visitas);

            }

            if (tipo == 3)
            {
                b_Inmuebles = dataInmuebles.GetReporte(idCartera, IdUsuario, fecha, fecha2);
                return PartialView("ReporteSinVisita", b_Inmuebles);
            }

            if (tipo == 4)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoFueraPoliticaInfReporte(idCartera, IdUsuario, 2, fecha, fecha2);
                return PartialView("ReporteVisita", b_Inmuebles_Visitas);

            }
            if (tipo == 5)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoInfReporte(idCartera, IdUsuario, 2, false, true, fecha, fecha2);
                return PartialView("ReporteVisita", b_Inmuebles_Visitas);
            }

            return PartialView("ReporteSinVisita", b_Inmuebles_Visitas);
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
    }
}
