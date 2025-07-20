using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebColliersCore;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebLomelinCore.Data;

namespace WebLomelinCore.Controllers
{
    public class B_inmueblesContratoController : Controller
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

        // GET: B_inmueblesContratoController
        public ActionResult Index()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            DataInmuebles dataInmuebles = new DataInmuebles();

            DataCG dataCG = new DataCG();
            ViewBag.lstClasificacion = dataCG.b_cg_clasificacionGet();

            DataUsuarios dataUsuarios = new DataUsuarios();
            List<B_inmuebles> listB_inmuebles = dataInmuebles.Get(idCartera, IdUsuario);
            return View(listB_inmuebles);
        }

        public ActionResult ListContratos(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            List<B_inmuebles_contrato> b_Inmuebles_Contratos = dataInmueblesContratos.Get(idCartera, IdUsuario, id);
            B_inmuebles_contrato b_Inmuebles_Contrato = new B_inmuebles_contrato();
            DataInmuebles dataInmuebles = new DataInmuebles();
            b_Inmuebles_Contrato.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, id);

            DataLocalidades dataLocalidades = new DataLocalidades();
            ViewBag.lstCategorias = dataLocalidades.categoriasGet();

            ViewData["id_b_inmuebles"] = id;
            ViewData["CR"] = b_Inmuebles_Contrato.b_inmuebles.cr;

            //ViewBag.lstClasificacion = dataCG.b_cg_clasificacionGet();

            return View(b_Inmuebles_Contratos);
        }

        public ActionResult Create(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            B_inmuebles_contrato b_Inmuebles_Contrato = new B_inmuebles_contrato();
            DataCG dataCG = new DataCG();
            DataInmuebles dataInmuebles = new DataInmuebles();
            DataInmueblesContratosCorreos dataInmueblesContratosCorreos = new DataInmueblesContratosCorreos();

            b_Inmuebles_Contrato.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, id);

            ViewData["id_b_inmuebles"] = id;
            b_Inmuebles_Contrato.id_b_inmuebles = id;
            b_Inmuebles_Contrato.fecha_inicio = DateTime.Now;
            b_Inmuebles_Contrato.fecha_termino = DateTime.Now;
            b_Inmuebles_Contrato.fecha_obligado = DateTime.Now;
            b_Inmuebles_Contrato.fecha_anticipacion = 90;
            b_Inmuebles_Contrato.fecha_revision = DateTime.Now;

            DataLocalidades dataLocalidades = new DataLocalidades();
            ViewBag.lstCategorias = dataLocalidades.categoriasGet();

            ViewBag.lstEstatus = dataCG.b_cg_contrato_estatusGet();
            ViewBag.lstTipo = dataCG.b_cg_contrato_tiposGet();
            ViewBag.lstPlazo = dataCG.b_cg_plazoGet();
            ViewBag.lstBase = dataCG.b_cg_baseGet();

            return View(b_Inmuebles_Contrato);
        }

        // POST: B_inmueblesContratoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(B_inmuebles_contrato b_Inmuebles_Contrato)
        {
            try
            {
                //string filePath = Environment.CurrentDirectory;
                string filePath = Path.Combine("Archivos", b_Inmuebles_Contrato.id_b_inmuebles.ToString("000"));
                filePath = Path.Combine(filePath, b_Inmuebles_Contrato.id_b_inmuebles_contrato.ToString("000"));

                if (!Directory.Exists(filePath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(filePath);
                }

                IFormFile file = b_Inmuebles_Contrato.File;
                if (file != null && file.Length != 0)
                {
                    filePath = Path.Combine(filePath, "contrato" + Path.GetExtension(Path.GetExtension(file.FileName)));
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyToAsync(fileStream);

                    }
                }
                


                DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
                dataInmueblesContratos.Insert(b_Inmuebles_Contrato);

                return RedirectToAction("ListContratos", new { id = b_Inmuebles_Contrato.id_b_inmuebles });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataCG dataCG = new DataCG();
            DataInmuebles dataInmuebles = new DataInmuebles();
            DataInmueblesContratosCorreos dataInmueblesContratosCorreos = new DataInmueblesContratosCorreos();
            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            B_inmuebles_contrato b_Inmuebles_Contrato = dataInmueblesContratos.GetById(idCartera, IdUsuario, id);

            b_Inmuebles_Contrato.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, b_Inmuebles_Contrato.id_b_inmuebles);

            ViewData["id_b_inmuebles"] = b_Inmuebles_Contrato.id_b_inmuebles;

            DataLocalidades dataLocalidades = new DataLocalidades();
            ViewBag.lstCategorias = dataLocalidades.categoriasGet();

            ViewBag.lstEstatus = dataCG.b_cg_contrato_estatusGet();
            ViewBag.lstTipo = dataCG.b_cg_contrato_tiposGet();
            ViewBag.lstPlazo = dataCG.b_cg_plazoGet();
            ViewBag.lstBase = dataCG.b_cg_baseGet();

            return View(b_Inmuebles_Contrato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(B_inmuebles_contrato b_Inmuebles_Contrato, int id)
        {
            try
            {
                b_Inmuebles_Contrato.id_b_inmuebles_contrato = id;
                DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
                DataLocalidades dataLocalidades = new DataLocalidades();
                ViewBag.lstCategorias = dataLocalidades.categoriasGet();

                dataInmueblesContratos.Edit(b_Inmuebles_Contrato);

                return RedirectToAction("ListContratos", new { id = b_Inmuebles_Contrato.id_b_inmuebles });

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataCG dataCG = new DataCG();
            DataInmuebles dataInmuebles = new DataInmuebles();
            DataInmueblesContratosCorreos dataInmueblesContratosCorreos = new DataInmueblesContratosCorreos();
            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            B_inmuebles_contrato b_Inmuebles_Contrato = dataInmueblesContratos.GetById(idCartera, IdUsuario, id);

            b_Inmuebles_Contrato.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, b_Inmuebles_Contrato.id_b_inmuebles);

            ViewData["id_b_inmuebles"] = b_Inmuebles_Contrato.id_b_inmuebles;

            DataLocalidades dataLocalidades = new DataLocalidades();
            ViewBag.lstCategorias = dataLocalidades.categoriasGet();

            ViewBag.lstEstatus = dataCG.b_cg_contrato_estatusGet();
            ViewBag.lstTipo = dataCG.b_cg_contrato_tiposGet();
            ViewBag.lstPlazo = dataCG.b_cg_plazoGet();
            ViewBag.lstBase = dataCG.b_cg_baseGet();

            return View(b_Inmuebles_Contrato);
        }

        public ActionResult Delete(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataCG dataCG = new DataCG();
            DataInmuebles dataInmuebles = new DataInmuebles();
            DataInmueblesContratosCorreos dataInmueblesContratosCorreos = new DataInmueblesContratosCorreos();
            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            B_inmuebles_contrato b_Inmuebles_Contrato = dataInmueblesContratos.GetById(idCartera, IdUsuario, id);

            b_Inmuebles_Contrato.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, b_Inmuebles_Contrato.id_b_inmuebles);

            ViewData["id_b_inmuebles"] = b_Inmuebles_Contrato.id_b_inmuebles;

            ViewBag.lstEstatus = dataCG.b_cg_contrato_estatusGet();
            ViewBag.lstTipo = dataCG.b_cg_contrato_tiposGet();

            return View(b_Inmuebles_Contrato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(B_inmuebles_contrato b_Inmuebles_Contrato, int id)
        {
            try
            {
                #region Validación de permisos
                var claims = HttpContext.User.Claims;
                Menu menu = new Menu();
                int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
                if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                    return Redirect("~/Home");
                #endregion

                DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
                DataInmuebles dataInmuebles = new DataInmuebles();
                b_Inmuebles_Contrato.id_b_inmuebles_contrato = id;
                dataInmueblesContratos.Delete(b_Inmuebles_Contrato);

                b_Inmuebles_Contrato = dataInmueblesContratos.GetById(idCartera, IdUsuario, id);
                b_Inmuebles_Contrato.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, b_Inmuebles_Contrato.id_b_inmuebles);

                return RedirectToAction("ListContratos", new { id = b_Inmuebles_Contrato.id_b_inmuebles });

            }
            catch
            {
                return View();
            }
        }







        //Apartado de correos
        public ActionResult Correos(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesContratosCorreos dataInmueblesContratosCorreos = new DataInmueblesContratosCorreos();
            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            List<B_inmuebles_contrato_correos> b_Inmuebles_Contrato_Correos = new List<B_inmuebles_contrato_correos>();
            b_Inmuebles_Contrato_Correos = dataInmueblesContratosCorreos.Get(idCartera, IdUsuario, id);

            B_inmuebles_contrato b_Inmuebles_Contrato = dataInmueblesContratos.GetById(idCartera, IdUsuario, id);


            ViewData["id_b_inmuebles_contrato"] = id;
            ViewData["id_b_inmuebles"] = b_Inmuebles_Contrato.id_b_inmuebles;

            //ViewBag.lstClasificacion = dataCG.b_cg_clasificacionGet();

            return View(b_Inmuebles_Contrato_Correos);
        }

        public ActionResult CreateCorreos(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion


            //B_inmuebles b_Inmuebles = new B_inmuebles();    
            //DataInmuebles dataInmuebles = new DataInmuebles();
            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            B_inmuebles_contrato b_Inmuebles_Contrato = dataInmueblesContratos.GetById(idCartera, IdUsuario, id);
            //b_Inmuebles = dataInmuebles.Get(idCartera, IdUsuario, b_Inmuebles_Contrato.id_b_inmuebles);

            ViewData["id_b_inmuebles_contrato"] = b_Inmuebles_Contrato.id_b_inmuebles_contrato;

            return View(new B_inmuebles_contrato_correos());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCorreos(B_inmuebles_contrato_correos inmuebles_Contrato_Correos, int id)
        {
            try
            {
                DataInmueblesContratosCorreos dataInmueblesContratosCorreos = new DataInmueblesContratosCorreos();
                inmuebles_Contrato_Correos.id_b_inmuebles_contrato = id;
                dataInmueblesContratosCorreos.Insert(inmuebles_Contrato_Correos);

                return RedirectToAction("Correos", new { id = id });

            }
            catch
            {
                return View();
            }
        }


        public ActionResult EditCorreos(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion


            //B_inmuebles b_Inmuebles = new B_inmuebles();    
            //DataInmuebles dataInmuebles = new DataInmuebles();
            //DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            DataInmueblesContratosCorreos dataInmueblesContratosCorreos = new DataInmueblesContratosCorreos();
            B_inmuebles_contrato_correos inmuebles_Contrato_Correos = dataInmueblesContratosCorreos.GetById(idCartera, IdUsuario, id);

            ViewData["id_b_inmuebles_contrato"] = inmuebles_Contrato_Correos.id_b_inmuebles_contrato;

            return View(inmuebles_Contrato_Correos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCorreos(B_inmuebles_contrato_correos inmuebles_Contrato_Correos, int id)
        {
            try
            {
                #region Validación de permisos
                var claims = HttpContext.User.Claims;
                Menu menu = new Menu();
                int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
                if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                    return Redirect("~/Home");
                #endregion

                DataInmueblesContratosCorreos dataInmueblesContratosCorreos = new DataInmueblesContratosCorreos();
                inmuebles_Contrato_Correos.id_b_inmuebles_contrato_correo = id;
                dataInmueblesContratosCorreos.Edit(inmuebles_Contrato_Correos);

                inmuebles_Contrato_Correos = dataInmueblesContratosCorreos.GetById(idCartera, IdUsuario, inmuebles_Contrato_Correos.id_b_inmuebles_contrato_correo);
                return RedirectToAction("Correos", new { id = inmuebles_Contrato_Correos.id_b_inmuebles_contrato });

            }
            catch
            {
                return View();
            }
        }

        public ActionResult DetailsCorreos(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion


            //B_inmuebles b_Inmuebles = new B_inmuebles();    
            //DataInmuebles dataInmuebles = new DataInmuebles();
            //DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            DataInmueblesContratosCorreos dataInmueblesContratosCorreos = new DataInmueblesContratosCorreos();
            B_inmuebles_contrato_correos inmuebles_Contrato_Correos = dataInmueblesContratosCorreos.GetById(idCartera, IdUsuario, id);

            ViewData["id_b_inmuebles_contrato"] = inmuebles_Contrato_Correos.id_b_inmuebles_contrato;

            return View(inmuebles_Contrato_Correos);
        }

        public ActionResult DeleteCorreos(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion


            //DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            DataInmueblesContratosCorreos dataInmueblesContratosCorreos = new DataInmueblesContratosCorreos();
            B_inmuebles_contrato_correos inmuebles_Contrato_Correos = dataInmueblesContratosCorreos.GetById(idCartera, IdUsuario, id);

            ViewData["id_b_inmuebles_contrato"] = inmuebles_Contrato_Correos.id_b_inmuebles_contrato;

            return View(inmuebles_Contrato_Correos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCorreos(B_inmuebles_contrato_correos inmuebles_Contrato_Correos, int id)
        {
            try
            {
                #region Validación de permisos
                var claims = HttpContext.User.Claims;
                Menu menu = new Menu();
                int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
                if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                    return Redirect("~/Home");
                #endregion

                DataInmueblesContratosCorreos dataInmueblesContratosCorreos = new DataInmueblesContratosCorreos();
                inmuebles_Contrato_Correos.id_b_inmuebles_contrato_correo = id;
                dataInmueblesContratosCorreos.Delete(inmuebles_Contrato_Correos);

                inmuebles_Contrato_Correos = dataInmueblesContratosCorreos.GetById(idCartera, IdUsuario, inmuebles_Contrato_Correos.id_b_inmuebles_contrato_correo);
                return RedirectToAction("Correos", new { id = inmuebles_Contrato_Correos.id_b_inmuebles_contrato });

            }
            catch
            {
                return View();
            }
        }

        /// Validación de fechas

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificaFechaInicio(DateTime fecha_inicio, int id_b_inmuebles, int id_b_inmuebles_contrato)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            List<B_inmuebles_contrato> b_Inmuebles_Contratos = dataInmueblesContratos.Get(idCartera, IdUsuario, id_b_inmuebles);

            bool exist = b_Inmuebles_Contratos.Any(d => d.fecha_termino >= fecha_inicio && d.id_b_inmuebles_contrato != id_b_inmuebles_contrato);

            if (exist)
                return Json($"Existe un contrato con la misma fecha");
            else
                return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificaFechaTermino(DateTime fecha_termino, DateTime fecha_inicio)
        {
            if (fecha_termino <= fecha_inicio)
                return Json($"La fecha de término debe ser mayor que la de inicio");
            else
                return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificaAnticipacion(int fecha_anticipacion, DateTime fecha_inicio, DateTime fecha_termino)
        {
            if (fecha_inicio.AddDays(fecha_anticipacion) >= fecha_termino)
                return Json($"Los días de de anticipación no son validos");

            return Json(true);
        }

        public IActionResult VerificaRevision(DateTime fecha_revision, DateTime fecha_inicio, DateTime fecha_termino)
        {
            if (fecha_revision > fecha_inicio && fecha_revision < fecha_termino)
                return Json(true);

            return Json($"La fecha de revisión debe ser mayor a la fecha de inicio y menor a la fecha de termino");
        }

    }
}
