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
            dataEmail.EnviaCorreo(idCartera,IdUsuario);
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
        public ActionResult getListadoContratosParametrizado(DateTime FechaInicio, DateTime FechaFin, int Campo)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion            

            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            List<B_inmuebles_contrato> reporte = dataInmueblesContratos.GetReporte(idCartera, IdUsuario, FechaInicio, FechaFin, Campo);

            //ViewBag.Campo = campo == 1 ? "Fecha de Termino" : "Fecha de Revisión";

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

            b_Inmuebles_Contrato_Correos = dataInmueblesContratos.GetReporte(idCartera,  IdUsuario, correo_enviado, fecha, fecha2);
             
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
    }
}
