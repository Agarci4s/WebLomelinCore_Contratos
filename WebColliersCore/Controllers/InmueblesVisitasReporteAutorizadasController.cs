using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using System.Collections.Generic;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebColliersCore;
using WebLomelinCore.Data;
using OfficeOpenXml;

namespace WebLomelinCore.Controllers
{
    public class InmueblesVisitasReporteAutorizadasController : Controller
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
        public ActionResult ExportarExcel(int id, int tipo, int filtro, DateTime fecha, DateTime fecha2, int regimen)
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
            List<B_inmuebles_visitas_reporte> b_Inmuebles_Visitas = new List<B_inmuebles_visitas_reporte>();
            List<B_inmuebles_reporte> b_Inmuebles = new List<B_inmuebles_reporte>();

            //Sólo autorizadas
            tipo = 2;

            if (tipo == 1)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoReporte(idCartera, IdUsuario, filtro, true, false, fecha, fecha2, regimen);
                nombre = "Visitados";
            }
            if (tipo == 2)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoReporte(idCartera, IdUsuario, 2, false, false, fecha, fecha2, regimen);
                nombre = "Autorizados";
            }
            if (tipo == 3)
            {
                b_Inmuebles = dataInmuebles.GetReporte(idCartera, IdUsuario, fecha, fecha2);
                nombre = "SinVisitar";
            }

            if (tipo == 4)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoFueraPoliticaReporte(idCartera, IdUsuario, 2, fecha, fecha2);
                nombre = "Fuera_de_politica";
            }
            if (tipo == 5)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoReporte(idCartera, IdUsuario, 2, false, true, fecha, fecha2, regimen);
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
                    if (b_Inmuebles_Visitas.Count > 0)
                    {
                        worksheet.Cells["N2:N" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells["O2:O" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells["P2:P" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells["Q2:Q" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells["R2:R" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "#,##0.00";

                        worksheet.Cells["BA2:BA" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "$#,##0.00";
                        worksheet.Cells["BB2:BB" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "$#,##0.00";
                        worksheet.Cells["BC2:BC" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells["BD2:BD" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells["BE2:BE2" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "#,##0.00";

                        worksheet.Cells["BF2:BF" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "#,##0";

                        worksheet.Cells["BI2:BI" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "$#,##0.00";
                        worksheet.Cells["BJ2:BJ" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "$#,##0.00";
                        worksheet.Cells["BK2:BK" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "$#,##0.00";
                        worksheet.Cells["BL2:BL" + (b_Inmuebles_Visitas.Count + 1)].Style.Numberformat.Format = "$#,##0.00";

                    }
                    columnas = 67;

                    if (tipo != 5)
                    {
                        worksheet.DeleteColumn(11);
                        columnas = columnas - 1;
                    }
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
        public ActionResult ReporteVisita(int tipo, int filtro, DateTime fecha, DateTime fecha2, int regimen)
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
            List<B_inmuebles_visitas_reporte> b_Inmuebles_Visitas = new List<B_inmuebles_visitas_reporte>();

            //Sólo autorizadas
            tipo = 2;

            if (tipo == 1)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoReporte(idCartera, IdUsuario, filtro, true, false, fecha, fecha2, regimen);
                return PartialView("ReporteVisita", b_Inmuebles_Visitas);

            }

            if (tipo == 2)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoReporte(idCartera, IdUsuario, 2, false, false, fecha, fecha2, regimen);
                return PartialView("ReporteVisita", b_Inmuebles_Visitas);

            }

            if (tipo == 3)
            {
                b_Inmuebles = dataInmuebles.GetReporte(idCartera, IdUsuario, fecha, fecha2);
                return PartialView("ReporteSinVisita", b_Inmuebles);
            }

            if (tipo == 4)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoFueraPoliticaReporte(idCartera, IdUsuario, 2, fecha, fecha2);
                return PartialView("ReporteVisita", b_Inmuebles_Visitas);

            }
            if (tipo == 5)
            {
                b_Inmuebles_Visitas = dataInmueblesVisita.GetFormatoReporte(idCartera, IdUsuario, 2, false, true, fecha, fecha2, regimen);
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
