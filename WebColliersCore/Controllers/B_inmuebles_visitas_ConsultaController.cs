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
    public class B_inmuebles_visitas_ConsultaController : Controller
    {
        private string filePath = Environment.CurrentDirectory;
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

        public IActionResult GetReport()
        {
            string filePath1 = Environment.CurrentDirectory;
            DataInmueblesVisita dataInmueblesVisita = new DataInmueblesVisita();
            int id = (int)TempData["id_b_inmuebles"];

            DataTable dataTableCR = dataInmueblesVisita.GetResumenCedular(id);


            StiReport report = new StiReport();
            string filePath = "";
            filePath = Directory.GetCurrentDirectory() + "\\Report\\CedulaResumen\\rptCedulaResumen.mrt";

            report.Load(filePath);

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

            return StiNetCoreViewer.InteractionResult(this, report);
        }

        public IActionResult ViewerEvent()

        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }

        public ActionResult lstCedulaResumen(int id)
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
            if (result == null || result.id_b_inmuebles != id)
                return Redirect("~/Home");

            DataInmueblesComparativo dataInmueblesComparativo = new DataInmueblesComparativo();
            List<B_inmuebles_comparativo> b_Inmuebles_Comparativos = dataInmueblesComparativo.Get(idCartera, IdUsuario, id);
            ViewData["id_b_inmuebles"] = id;
            TempData["id_b_inmuebles"] = id;

            //return View(b_Inmuebles_Comparativos);
            return View("lstCedulaResumen");
        }

        // GET: B_inmuebles_visitas
        public ActionResult Index()
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

            List<B_inmuebles> b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario);
            return View(b_inmuebles);
        }

        public ActionResult ListVisita(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 3;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);

            DataUsuarios dataUsuarios = new DataUsuarios();
            int rol = int.Parse(dataUsuarios.recuperaUsuario(IdUsuario).Puesto);
            DataCG dataCG = new DataCG();
            ViewBag.rol = rol;


            DataInmueblesVisita dataInmueblesVisita = new DataInmueblesVisita();
            List<B_inmuebles_visitas> b_Inmuebles_Visitas;
            b_Inmuebles_Visitas = dataInmueblesVisita.Get(IdUsuario, id, 2);//Visitas terminadas


            return View(b_Inmuebles_Visitas);
        }

        public ActionResult DetailsVisita(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            DataUsuarios dataUsuarios = new DataUsuarios();
            int rol = int.Parse(dataUsuarios.recuperaUsuario(IdUsuario).Puesto);

            DataCG dataCG = new DataCG();
            ViewBag.lstRegimen = dataCG.b_cg_regimenGet();
            ViewBag.lstEstatus = dataCG.b_cg_inmuebles_estatusGet();
            ViewBag.lstClasificacion = dataCG.b_cg_clasificacionGet();
            ViewBag.lstValorMercado = dataCG.b_cg_valor_mercado();
            ViewBag.lstEstatusVisita = dataCG.b_cg_estatus_visita_inmueble(rol);
            ViewBag.lst_planta_emergencia = dataCG.lst_planta_emergencia();
            ViewBag.lst_impermeabilizacion = dataCG.lst_impermeabilizacion();
            ViewBag.lst_medidor = dataCG.lst_medidor();
            ViewBag.lst_agua_toma = dataCG.lst_agua_toma();
            ViewBag.lstConservacion = dataCG.b_cg_conservacionGet();
            ViewBag.lstVariacionInmueble = dataCG.b_cg_variacionInmuebleGet();
            ViewBag.lst_si_no = dataCG.lst_si_no();
            ViewBag.lst_mantenimiento = dataCG.lst_mantenimiento();
            ViewBag.lst_aire_acondicionado2 = dataCG.lst_aire_acondicionado2();
            ViewBag.rol = rol;

            DataInmueblesVisita dataInmueblesVisita = new DataInmueblesVisita();
            DataInmuebles dataInmuebles = new DataInmuebles();
            DataInmueblesComparativo dataInmueblesComparativo = new DataInmueblesComparativo();
            B_inmuebles_visitas b_Inmuebles_Visitas;

            b_Inmuebles_Visitas = dataInmueblesVisita.GetById(IdUsuario, id).FirstOrDefault();
            b_Inmuebles_Visitas.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, b_Inmuebles_Visitas.id_b_inmuebles);
            b_Inmuebles_Visitas.b_inmuebles_comparativo = dataInmueblesComparativo.Get(idCartera, IdUsuario, b_Inmuebles_Visitas.id_b_inmuebles);

            if (b_Inmuebles_Visitas.b_inmuebles_comparativo != null)
                b_Inmuebles_Visitas.no_comparables = b_Inmuebles_Visitas.b_inmuebles_comparativo.Count();

            //copia las imagenes
            filePath = Path.Combine("wwwroot", "Images");
            filePath = Path.Combine(filePath, "precarga");
            filePath = Path.Combine(filePath, b_Inmuebles_Visitas.id_b_inmuebles.ToString("000"));
            filePath = Path.Combine(filePath, b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd"));
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string filePathAux = Path.Combine("Images", b_Inmuebles_Visitas.id_b_inmuebles.ToString("000"));
            filePathAux = Path.Combine(filePathAux, b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd"));
            if (Directory.Exists(filePathAux))
            {
                foreach (var file in Directory.GetFiles(filePathAux))//directorio de donde se moveran las fotos
                {
                    var nombre = Path.GetFileNameWithoutExtension(file);
                    var nombreAll = Path.GetFileName(file);
                    if (GetFotos().Contains(nombre.Split(" ")[0]))
                    {
                        if (GetFotos(b_Inmuebles_Visitas).Contains(nombreAll))
                            System.IO.File.Copy(file, Path.Combine(filePath, Path.GetFileName(file)), true);
                    }
                }
            }
            if (b_Inmuebles_Visitas.sucursal != null && b_Inmuebles_Visitas.sucursal.Length > 4)
                b_Inmuebles_Visitas.sucursal = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.sucursal;
            if (b_Inmuebles_Visitas.geolocalizacion != null && b_Inmuebles_Visitas.geolocalizacion.Length > 4)
                b_Inmuebles_Visitas.geolocalizacion = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.geolocalizacion;
            if (b_Inmuebles_Visitas.exterior1 != null && b_Inmuebles_Visitas.exterior1.Length > 4)
                b_Inmuebles_Visitas.exterior1 = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.exterior1;
            if (b_Inmuebles_Visitas.exterior2 != null && b_Inmuebles_Visitas.exterior2.Length > 4)
                b_Inmuebles_Visitas.exterior2 = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.exterior2;
            if (b_Inmuebles_Visitas.circundante_norte != null && b_Inmuebles_Visitas.circundante_norte.Length > 4)
                b_Inmuebles_Visitas.circundante_norte = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.circundante_norte;
            if (b_Inmuebles_Visitas.circundante_sur != null && b_Inmuebles_Visitas.circundante_sur.Length > 4)
                b_Inmuebles_Visitas.circundante_sur = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.circundante_sur;
            if (b_Inmuebles_Visitas.circundante_oriente != null && b_Inmuebles_Visitas.circundante_oriente.Length > 4)
                b_Inmuebles_Visitas.circundante_oriente = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.circundante_oriente;
            if (b_Inmuebles_Visitas.circundante_poniente != null && b_Inmuebles_Visitas.circundante_poniente.Length > 4)
                b_Inmuebles_Visitas.circundante_poniente = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.circundante_poniente;
            if (b_Inmuebles_Visitas.interior1 != null && b_Inmuebles_Visitas.interior1.Length > 4)
                b_Inmuebles_Visitas.interior1 = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.interior1;
            if (b_Inmuebles_Visitas.interior2 != null && b_Inmuebles_Visitas.interior2.Length > 4)
                b_Inmuebles_Visitas.interior2 = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.interior2;
            if (b_Inmuebles_Visitas.espacio1 != null && b_Inmuebles_Visitas.espacio1.Length > 4)
                b_Inmuebles_Visitas.espacio1 = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.espacio1;
            if (b_Inmuebles_Visitas.espacio2 != null && b_Inmuebles_Visitas.espacio2.Length > 4)
                b_Inmuebles_Visitas.espacio2 = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.espacio2;
            if (b_Inmuebles_Visitas.mantenimiento1 != null && b_Inmuebles_Visitas.mantenimiento1.Length > 4)
                b_Inmuebles_Visitas.mantenimiento1 = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.mantenimiento1;
            if (b_Inmuebles_Visitas.mantenimiento2 != null && b_Inmuebles_Visitas.mantenimiento2.Length > 4)
                b_Inmuebles_Visitas.mantenimiento2 = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.mantenimiento2;
            if (b_Inmuebles_Visitas.planta_emergencia2 != null && b_Inmuebles_Visitas.planta_emergencia2.Length > 4)
                b_Inmuebles_Visitas.planta_emergencia2 = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.planta_emergencia2;
            if (b_Inmuebles_Visitas.medidor_energia != null && b_Inmuebles_Visitas.medidor_energia.Length > 4)
                b_Inmuebles_Visitas.medidor_energia = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.medidor_energia;
            if (b_Inmuebles_Visitas.toma_agua != null && b_Inmuebles_Visitas.toma_agua.Length > 4)
                b_Inmuebles_Visitas.toma_agua = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.toma_agua;
            if (b_Inmuebles_Visitas.impermeabilizante != null && b_Inmuebles_Visitas.impermeabilizante.Length > 4)
                b_Inmuebles_Visitas.impermeabilizante = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.impermeabilizante;
            if (b_Inmuebles_Visitas.arquitectonico != null && b_Inmuebles_Visitas.arquitectonico.Length > 4)
                b_Inmuebles_Visitas.arquitectonico = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.arquitectonico;
            if (b_Inmuebles_Visitas.inclusion2 != null && b_Inmuebles_Visitas.inclusion2.Length > 4)
                b_Inmuebles_Visitas.inclusion2 = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.inclusion2;
            if (b_Inmuebles_Visitas.estacionamiento != null && b_Inmuebles_Visitas.estacionamiento.Length > 4)
                b_Inmuebles_Visitas.estacionamiento = b_Inmuebles_Visitas.fecha_visita.ToString("yyMMdd") + "/" + b_Inmuebles_Visitas.estacionamiento;

            return View(b_Inmuebles_Visitas);

        }
    
        public ActionResult ListComparativo(int id)
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
            if (result == null || result.id_b_inmuebles != id)
                return Redirect("~/Home");

            DataInmueblesComparativo dataInmueblesComparativo = new DataInmueblesComparativo();
            List<B_inmuebles_comparativo> b_Inmuebles_Comparativos = dataInmueblesComparativo.Get(idCartera, IdUsuario, id);
            ViewData["id_b_inmuebles"] = id;

            return View(b_Inmuebles_Comparativos);
        }

        public ActionResult DetailsComparativo(int id)
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
            DataInmuebles dataInmuebles = new DataInmuebles();

            DataInmueblesComparativo dataInmueblesComparativo = new DataInmueblesComparativo();
            B_inmuebles_comparativo b_Inmuebles_Comparativo = dataInmueblesComparativo.GetById(idCartera, IdUsuario, id);

            var result = dataInmuebles.Get(idCartera, IdUsuario).Find(x => x.id_b_inmuebles == b_Inmuebles_Comparativo.id_b_inmuebles);

            ViewBag.lstRegion = dataCG.b_cg_regionGet();
            ViewBag.lstCategorias = dataCG.b_cg_categoriasGet();
            ViewBag.regimen = result.id_b_cg_regimen;


            return View(b_Inmuebles_Comparativo);

        }

               // GET: Comparativo/Edit/5
        public ActionResult Comparativo()
        {
            List<B_inmuebles_comparativo> b_inmuebles_comparativo = new List<B_inmuebles_comparativo>();
            return View(b_inmuebles_comparativo);
        }

        [HttpPost]
        public async Task<ActionResult> UploadFilesAsync(IList<IFormFile> files, int idInmueble, string nameArchivo)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            filePath = Path.Combine("wwwroot", "Images");
            filePath = Path.Combine(filePath, "precarga");
            filePath = Path.Combine(filePath, idInmueble.ToString("000"));

            if (!Directory.Exists(filePath))
            {
                DirectoryInfo di = Directory.CreateDirectory(filePath);
            }

            IFormFile file = files[0];
            if (file != null && file.Length != 0)
            {
                filePath = Path.Combine(filePath, nameArchivo + Path.GetExtension(Path.GetExtension(file.FileName)));
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            B_inmuebles_visitas_imagenes b_Inmuebles_Visitas_Imagenes = new B_inmuebles_visitas_imagenes { ruta = "/Images/precarga/" + idInmueble.ToString("000") + "/" + nameArchivo + Path.GetExtension(System.IO.Path.GetExtension(file.FileName)) };
            return PartialView("CargaImagen", b_Inmuebles_Visitas_Imagenes);
        }

        [HttpPost]
        public async Task<ActionResult> UploadFilesEditAsync(IList<IFormFile> files, int idInmueble, string nameArchivo, int id_b_inmuebles_visita)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesVisita dataInmueblesVisita = new DataInmueblesVisita();
            B_inmuebles_visitas b_Inmuebles_visitas = dataInmueblesVisita.GetById(IdUsuario, id_b_inmuebles_visita).FirstOrDefault();

            filePath = Path.Combine("wwwroot", "Images");
            filePath = Path.Combine(filePath, "precarga");
            filePath = Path.Combine(filePath, idInmueble.ToString("000"));
            filePath = Path.Combine(filePath, b_Inmuebles_visitas.fecha_visita.ToString("yyMMdd"));

            if (!Directory.Exists(filePath))
            {
                DirectoryInfo di = Directory.CreateDirectory(filePath);
            }

            IFormFile file = files[0];
            if (file != null && file.Length != 0)
            {
                filePath = Path.Combine(filePath, nameArchivo + Path.GetExtension(Path.GetExtension(file.FileName)));
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            B_inmuebles_visitas_imagenes b_Inmuebles_Visitas_Imagenes = new B_inmuebles_visitas_imagenes { ruta = "/Images/precarga/" + idInmueble.ToString("000") + "/" + b_Inmuebles_visitas.fecha_visita.ToString("yyMMdd") + "/" + nameArchivo + Path.GetExtension(System.IO.Path.GetExtension(file.FileName)) };
            return PartialView("CargaImagen", b_Inmuebles_Visitas_Imagenes);
        }

        [HttpPost]
        public async Task<ActionResult> UploadFiles2Async(IList<IFormFile> files, int idInmueble, string nameArchivo)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            filePath = Path.Combine("wwwroot", "Images");
            filePath = Path.Combine(filePath, "precarga");
            filePath = Path.Combine(filePath, idInmueble.ToString("000"));
            if (!Directory.Exists(filePath))
            {
                DirectoryInfo di = Directory.CreateDirectory(filePath);
            }

            string filePath2 = Path.Combine("Images", idInmueble.ToString("000"));
            if (!Directory.Exists(filePath2))
            {
                DirectoryInfo di = Directory.CreateDirectory(filePath2);
            }

            IFormFile file = files[0];
            if (file != null && file.Length != 0)
            {
                filePath = Path.Combine(filePath, nameArchivo + Path.GetExtension(System.IO.Path.GetExtension(file.FileName)));
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

            }

            B_inmuebles_visitas_imagenes b_Inmuebles_Visitas_Imagenes = new B_inmuebles_visitas_imagenes { ruta = "/Images/precarga/" + idInmueble.ToString("000") + "/" + nameArchivo + Path.GetExtension(Path.GetExtension(file.FileName)) };

            return PartialView("CargaImagen", b_Inmuebles_Visitas_Imagenes);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificaFecha(int id, DateTime fecha_visita, int id_b_inmuebles, int id_b_inmuebles_visita)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesVisita dataInmueblesVisita = new DataInmueblesVisita();
            bool exist = dataInmueblesVisita.Get(IdUsuario, id_b_inmuebles, 2).Any(d => d.fecha_visita.Year == fecha_visita.Year && d.fecha_visita.Month == fecha_visita.Month && d.fecha_visita.Day == fecha_visita.Day && d.id_b_inmuebles_visita != id_b_inmuebles_visita);


            if (exist)
                return Json($"Existe una visita con la misma fecha");
            else
                return Json(true);
        }

        public ActionResult View2()
        {
            B_inmuebles_visitas b_Inmuebles_visitas = new B_inmuebles_visitas();
            return View();
        }

        private List<string> GetFotos()
        {
            var data = new List<string>()
            {
                "sucursal",
                "geolocalizacion",
                "exterior1",
                "exterior2",
                "circundante_norte",
                "circundante_sur",
                "circundante_oriente",
                "circundante_poniente",
                "interior1",
                "interior2",
                "espacio1",
                "espacio2",
                "mantenimiento1",
                "mantenimiento2",
                "planta_emergencia2",
                "medidor_energia",
                "toma_agua",
                "impermeabilizante",
                "arquitectonico",
                "inclusion2",
                "estacionamiento"
            };
            return data;
        }

        private List<string> GetFotos(B_inmuebles_visitas b_Inmuebles_visitas)
        {
            var imagenes = new List<string>()
                        {
                            b_Inmuebles_visitas.sucursal,
                            b_Inmuebles_visitas.geolocalizacion,
                            b_Inmuebles_visitas.exterior1,
                            b_Inmuebles_visitas.exterior2,
                            b_Inmuebles_visitas.circundante_norte,
                            b_Inmuebles_visitas.circundante_sur,
                            b_Inmuebles_visitas.circundante_oriente,
                            b_Inmuebles_visitas.circundante_poniente,
                            b_Inmuebles_visitas.interior1,
                            b_Inmuebles_visitas.interior2,
                            b_Inmuebles_visitas.espacio1,
                            b_Inmuebles_visitas.espacio2,
                            b_Inmuebles_visitas.mantenimiento1,
                            b_Inmuebles_visitas.mantenimiento2,
                            b_Inmuebles_visitas.planta_emergencia2,
                            b_Inmuebles_visitas.medidor_energia,
                            b_Inmuebles_visitas.toma_agua,
                            b_Inmuebles_visitas.impermeabilizante,
                            b_Inmuebles_visitas.arquitectonico,
                            b_Inmuebles_visitas.inclusion2,
                            b_Inmuebles_visitas.estacionamiento
                    };
            return imagenes;

        }

    }
}
