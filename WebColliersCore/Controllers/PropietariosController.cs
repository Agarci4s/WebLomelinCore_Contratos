using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebColliersCore.Data;
using WebColliersCore.Models;

namespace WebColliersCore.Controllers
{
    public class PropietariosController : Controller
    {
        private DataPropietarios dataProp;

        public List<SelectListItem> fis_mor { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="F", Text = "Física" ,},
            new SelectListItem { Value = "M", Text = "Moral"  },
        };

        public List<SelectListItem> aTTCamb { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="S", Text = "Si" ,},
            new SelectListItem { Value = "N", Text = "No"  },
        };

        public List<SelectListItem> emiEti { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="A", Text = "Atención" ,},
            new SelectListItem { Value = "C", Text = "Cambio de nombre de etiqueta"  },
        };

        public List<SelectListItem> tipoIdentificacion { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="-1", Text = "--SELECCIONE UNA OPCIÓN--" },
            new SelectListItem {Value="0", Text = "INE" },
            new SelectListItem { Value = "1", Text = "CARTILLA SERVICIO MILITAR"  },
            new SelectListItem { Value = "2", Text = "CEDULA PROFESIONAL"  },
            new SelectListItem { Value = "3", Text = "PASAPORTE"  },
            new SelectListItem { Value = "4", Text = "F1 (EXTRANJEROS)"  },
        };

        [HttpGet]
        public JsonResult GetRegimenFiscal(string id)
        {
            dataProp = new DataPropietarios();
            return Json(dataProp.GetRegimenFiscal(id));
        }

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

        // GET: Propietarios
        //public ActionResult Index(CgPropietarios propietarioIn)
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
            dataProp = new DataPropietarios();
            //dataProp.GetListaPropietarios("", 0, usuario.idCartera, usuario.IdUsuario);
            //return View(dataProp.GetListaPropietarios("", propietarioIn.IdPropietario,  usuario.idCartera, usuario.IdUsuario));
            return View(dataProp.GetListaPropietarios("", 0, idCartera, IdUsuario));
        }

        // GET: Propietarios/Details/5
        public ActionResult Details(long id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            dataProp = new DataPropietarios();
            return View(dataProp.GetPropietarioById(id, idCartera, IdUsuario));
        }

        public void InicializarCombos(string tipo, string fismor)
        {
            dataProp = new DataPropietarios();
            ViewBag.lstNacionalidad = dataProp.GetPaises();
            ViewBag.lstBancos = dataProp.GetBancos();
            ViewBag.fis_mor = fis_mor;
            ViewBag.aTTCamb = aTTCamb;
            ViewBag.emiEti = emiEti;
            ViewBag.tipoIdentificacion = tipoIdentificacion;
            ViewBag.lstPaises = new DataInmuebles().GetListaPaises();
            ViewBag.lstEstados = new DataInmuebles().GetListaEstados();
            if (tipo == "E") {
                ViewBag.lstRegimenFiscal = dataProp.GetRegimenFiscal(fismor);
            }
            else{
                ViewBag.lstRegimenFiscal = dataProp.GetRegimenFiscal("F");
            }
            
        }

        // GET: Propietarios/Create
        public ActionResult Create()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            if (!ViewBag.Agregar)
            {
                return Redirect("~/Propietarios");
            }
            InicializarCombos("C","");
            return View();
        }

        // POST: Propietarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, CgPropietarios cgPropietarios)
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

                dataProp = new DataPropietarios();
                if (dataProp.Crear(cgPropietarios))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (dataProp.TipoError == 1)
                        ViewBag.ErrorRFC = true;
                    if (dataProp.TipoError == 2)
                        ViewBag.ErrorFisicas = true;
                    if (dataProp.TipoError == 3)
                        ViewBag.ErrorRFCRep = true;
                    if (dataProp.TipoError == 4)
                        ViewBag.ErrorRFC = true;
                    if (dataProp.TipoError == 5)
                        ViewBag.ErrorMorales = true;
                    if (dataProp.TipoError == 6)
                        ViewBag.ErrorRegimenFiscal = true;

                    InicializarCombos("C","");
                    return View(cgPropietarios);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                InicializarCombos("C","");
                return View();
            }
        }

        // GET: Propietarios/Edit/5
        public ActionResult Edit(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            if (!ViewBag.Editar)
            {
                return Redirect("~/Propietarios");
            }
            dataProp = new DataPropietarios();
            CgPropietarios cgPropietarios = new CgPropietarios();
            cgPropietarios = dataProp.GetPropietarioById(id, idCartera, IdUsuario);
            InicializarCombos("E", cgPropietarios.Fis_Mor);
            return View(cgPropietarios);
        }

        // POST: Propietarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, CgPropietarios cgPropietarios)
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

                dataProp = new DataPropietarios();
                if (dataProp.Actualizar(id, cgPropietarios))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (dataProp.TipoError == 1)
                        ViewBag.ErrorRFC = true;
                    if (dataProp.TipoError == 2)
                        ViewBag.ErrorFisicas = true;
                    if (dataProp.TipoError == 3)
                        ViewBag.ErrorRFCRep = true;
                    if (dataProp.TipoError == 4)
                        ViewBag.ErrorRFC = true;
                    if (dataProp.TipoError == 5)
                        ViewBag.ErrorMorales = true;

                    InicializarCombos("E", cgPropietarios.Fis_Mor);
                    return View(cgPropietarios);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                return View();
            }
        }

        // GET: Propietarios/Delete/5
        public ActionResult Delete(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            if (!ViewBag.Eliminar)
            {
                return Redirect("~/Propietarios");
            }
            dataProp = new DataPropietarios();
            return View(dataProp.GetPropietarioById(id, idCartera, IdUsuario));
        }

        // POST: Propietarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection, CgPropietarios cgPropietarios)
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

                dataProp = new DataPropietarios();
                dataProp.Eliminar(id);

                return RedirectToAction(nameof(Index));
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult SelectPropietarios(string Clapro)
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

                EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
                dataProp = new DataPropietarios();

                return PartialView("lstPropietarios", dataProp.GetListaPropietarios((Clapro == null ? "" : Clapro), 0, idCartera, IdUsuario));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private Usuario validaPermiso()
        {
            Usuario usuario = null;
            Request.Cookies.TryGetValue("CoreColliers", out string strCookies);
            Request.Cookies.TryGetValue("CoreColliersCartera", out string strCookies2);
            if (strCookies == null || strCookies2 == null)
            {
                return usuario;
            }
            var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies2);
            string cartera = LegacyCookieExtensions.Decrypt(legacyCookie["cartera"], SystemComplementos.Key);
            int.TryParse(cartera, out int idCartera);
            legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
            DataUsuarios dataUsuarios = new DataUsuarios();
            string username = legacyCookie["userName"];
            string password = LegacyCookieExtensions.Decrypt(legacyCookie["userName2"], SystemComplementos.Key);
            usuario = dataUsuarios.RecuperaUsuario(username, password);
            usuario.idCartera = idCartera;
            return usuario;
        }
    }
}