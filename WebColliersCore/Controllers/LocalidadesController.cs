using System;
using System.Collections.Generic;
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
    public class LocalidadesController : Controller
    {
        private DataLocalidades dataLocalidades = new DataLocalidades();

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


        //GET: Localidades
        public ActionResult Index()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            ViewBag.lstInmuebles = dataLocalidades.InmueblesGet(IdUsuario, idCartera);
            return View(dataLocalidades.GetListaLocalidades(0, "", "", idCartera, IdUsuario));
        }

        [HttpGet]
        public JsonResult GetArrendatariosList(int id)
        {
            return Json(dataLocalidades.arrendatariosGet(id));
        }

        [HttpGet]
        public JsonResult GetNombresComercialesList(int id)
        {
            return Json(dataLocalidades.nombreComercialGet(id));
        }

        [HttpGet]
        public JsonResult GetLocalidadesByInmuebleList(int id)
        {
            return Json(dataLocalidades.GetListaLocalidades(0, "", "", 0, 0));
        }

        [HttpGet]
        public JsonResult GetCaracteristicasList(int id)
        {
            return Json(dataLocalidades.CaracteristicasGet(id));
        }

        [HttpGet]
        public JsonResult GetLocalidadesOcupadas(int idinquilino, int idinmueble)
        {
            return Json(dataLocalidades.LocalidadesOcupadasByInquilino(idinquilino, idinmueble));
        }

        [HttpGet]
        public JsonResult GetCaracteristicasByLocalidad(int IdLocalidad, int IdUsoInmueble)
        {
            return Json(dataLocalidades.CaracteristicasByLocalidad(IdLocalidad, IdUsoInmueble));
        }

        // GET: Localidades/Details/5
        public ActionResult Details(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            dataLocalidades = new DataLocalidades();
            return View(dataLocalidades.GetLocalidadById(id));
        }

        // GET: Localidades/Create
        public ActionResult Create()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            InicializarCombos(IdUsuario, idCartera);
            //ViewBag.lstCaracteristicas = dataLocalidades.CaracteristicasGet(2);
            return View();
        }

        // POST: Localidades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Localidad localidad)
        {
            try
            {
                #region Validación de permisos
                var claims = HttpContext.User.Claims;
                Menu menu = new Menu();
                int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
                if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                    return Redirect("~/Home");
                #endregion

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    if (localidad.IdInmueble == -1)
                    {
                        ViewBag.ErrorLocalidad = true;
                        ViewBag.Mensaje = "Falta seleccionar un inmueble.";
                        InicializarCombos(IdUsuario, idCartera);
                        //ViewBag.lstCaracteristicas = dataLocalidades.CaracteristicasGet(2);
                        return View(localidad);
                    }
                    if (localidad.IdUsoInmueble == -1)
                    {
                        ViewBag.ErrorLocalidad = true;
                        ViewBag.Mensaje = "Falta seleccionar el tipo de localidad.";
                        InicializarCombos(IdUsuario, idCartera);
                        //ViewBag.lstCaracteristicas = dataLocalidades.CaracteristicasGet(2);
                        return View(localidad);
                    }
                    if (localidad.NumeroLocalidad == null)
                    {
                        ViewBag.ErrorLocalidad = true;
                        ViewBag.Mensaje = "Falta proporcionar el número de la localidad.";
                        InicializarCombos(IdUsuario, idCartera);
                        //ViewBag.lstCaracteristicas = dataLocalidades.CaracteristicasGet(2);
                        return View(localidad);
                    }
                    if (localidad.IdInquilino == -1)
                    {
                        ViewBag.ErrorLocalidad = true;
                        ViewBag.Mensaje = "Falta seleccionar el nombre del arrendatario.";
                        InicializarCombos(IdUsuario, idCartera);
                        //ViewBag.lstCaracteristicas = dataLocalidades.CaracteristicasGet(2);
                        return View(localidad);
                    }
                    dataLocalidades.Crear(localidad);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                return View();
            }
        }

        // GET: Localidades/Edit/5
        public ActionResult Edit(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            InicializarCombos(IdUsuario, idCartera);
            List<dtGastosFijos> dtGastosFijosList = new List<dtGastosFijos>();
            Localidad localidad = new Localidad();
            dtGastosFijosList.Add(new dtGastosFijos { IdGasto = 0 });
            localidad.dtGastosFijosList = dtGastosFijosList;

            //List<NotasLocalidad> dtNotasList = new List<NotasLocalidad>();
            //dtNotasList.Add(new NotasLocalidad { Nota = "PRUEBA" });
            //localidad.dtNotasList = dtNotasList;
            localidad = dataLocalidades.GetLocalidadById(id);
            ViewBag.lstNombreComercial = dataLocalidades.nombreComercialGet(localidad.IdInquilino);
            //ViewBag.lstCaracteristicas = dataLocalidades.CaracteristicasGet(id);


            return View(localidad);
        }

        public void InicializarCombos(int IdUsuario, int IdCartera)
        {
            ViewBag.lstInmuebles = dataLocalidades.InmueblesGet(IdUsuario, IdCartera);
            ViewBag.lstUso = dataLocalidades.usoLocalidadGet();
            ViewBag.lstEstado = dataLocalidades.estadoLocalidadGet();
            ViewBag.lstArrendatarios = dataLocalidades.arrendatariosGet(0);
            ViewBag.lstNombreComercial = dataLocalidades.nombreComercialGet(208912);
            ViewBag.lstPropioTercero = dataLocalidades.propioTerceroGet();
            ViewBag.lstCategorias = dataLocalidades.categoriasGet();
            ViewBag.lstRevision = dataLocalidades.baseRevisionGet();
            ViewBag.lstRevisionPlazo = dataLocalidades.plazoRevisionGet();
            ViewBag.lstGastosFijos = dataLocalidades.GastosFijosGet();
            ViewBag.lstClaveUnidad = dataLocalidades.ClaveUnidadGet();
            ViewBag.lstClaveProdServ = dataLocalidades.ClaveProdServGet();
            ViewBag.lstEmiteFactura = dataLocalidades.EmiteFacturacionGet();
            ViewBag.lstMonedaPago = dataLocalidades.MonedaPagoGet();
            //ViewBag.lstTipoNota = dataLocalidades.TipoNotaGet();
            //ViewBag.lstCaracteristicas = dataLocalidades.CaracteristicasGet(2);
            ViewBag.FechaActual = String.Format("{0:yyyy-MM-dd}", DateTime.Now);
        }

        // POST: Localidades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, Localidad localidad)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            try
            {
                //localidad.NotasLocalidad.Fecha = DateTime.Now;
                //localidad.NotasLocalidad.Tipo = "T";
                //localidad.NotasLocalidad.Nota = "";
                //localidad.NotasLocalidad.Status = 0;
                //localidad.NotasLocalidad.IdNota = 0;
                //localidad.NotasLocalidad.IdLocalidad = id;

                if (ModelState.IsValid)
                {
                    if (localidad.IdInmueble == -1)
                    {
                        ViewBag.ErrorLocalidad = true;
                        ViewBag.Mensaje = "Falta seleccionar un inmueble.";
                        InicializarCombos(IdUsuario, idCartera);
                        //ViewBag.lstCaracteristicas = dataLocalidades.CaracteristicasGet(id);
                        return View(localidad);
                    }
                    if (localidad.IdUsoInmueble == -1)
                    {
                        ViewBag.ErrorLocalidad = true;
                        ViewBag.Mensaje = "Falta seleccionar el tipo de localidad.";
                        InicializarCombos(IdUsuario, idCartera);
                        //ViewBag.lstCaracteristicas = dataLocalidades.CaracteristicasGet(id);
                        return View(localidad);
                    }
                    if (localidad.NumeroLocalidad == null)
                    {
                        ViewBag.ErrorLocalidad = true;
                        ViewBag.Mensaje = "Falta proporcionar el número de la localidad.";
                        InicializarCombos(IdUsuario, idCartera);
                        //ViewBag.lstCaracteristicas = dataLocalidades.CaracteristicasGet(id);
                        return View(localidad);
                    }
                    if (localidad.IdInquilino == -1)
                    {
                        ViewBag.ErrorLocalidad = true;
                        ViewBag.Mensaje = "Falta seleccionar el nombre del arrendatario.";
                        InicializarCombos(IdUsuario, idCartera);
                        //ViewBag.lstCaracteristicas = dataLocalidades.CaracteristicasGet(id);
                        return View(localidad);
                    }
                    dataLocalidades.Actualizar(localidad, id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    InicializarCombos(IdUsuario, idCartera);
                    //ViewBag.lstCaracteristicas = dataLocalidades.CaracteristicasGet(id);
                    return View(localidad);
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                InicializarCombos(IdUsuario, idCartera);
                //ViewBag.lstCaracteristicas = dataLocalidades.CaracteristicasGet(id);
                return View(localidad);
            }
        }

        // GET: Localidades/Delete/5
        public ActionResult Delete(int id)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            dataLocalidades = new DataLocalidades();
            return View(dataLocalidades.GetLocalidadById(id));
        }

        // POST: Localidades/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                #region Validación de permisos
                var claims = HttpContext.User.Claims;
                Menu menu = new Menu();
                int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
                if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                    return Redirect("~/Home");
                #endregion

                dataLocalidades = new DataLocalidades();
                dataLocalidades.Eliminar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public List<SelectListItem> plazoRevisionGet()
        {
            try
            {
                List<SelectListItem> lstPlazosRevision = new List<SelectListItem>();
                lstPlazosRevision.Add(new SelectListItem { Value = "ANUAL", Text = "ANUAL", });
                lstPlazosRevision.Add(new SelectListItem { Value = "SEMESTRAL", Text = "SEMESTRAL" });
                lstPlazosRevision.Add(new SelectListItem { Value = "9 MESES", Text = "9 MESES" });
                lstPlazosRevision.Add(new SelectListItem { Value = "11 MESES", Text = "11 MESES" });

                return lstPlazosRevision;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult InsertGastoFijo([FromBody] dtGastosFijos gf)
        {
            if (gf != null)
            {
                dataLocalidades.InsertGastosFijos(gf);
            }
            return new JsonResult(gf) { Value = "Se registro el concepto correctamente." };
        }

        [HttpPost]
        public ActionResult DeleteGastos(int id, int idlocalidad)
        {
            dataLocalidades.EliminarGastos(id, idlocalidad);
            return new JsonResult(id) { Value = "Se elimino el gasto correctamente." };
        }

        [HttpPost]
        public ActionResult SelectGastoFijo(int idLocalidad)
        {
            return PartialView("lstGastoFijo", dataLocalidades.dtGastosFijosList(idLocalidad));
        }

        [HttpPost]
        public ActionResult InsertNotas([FromBody] NotasLocalidad notas)
        {
            if (notas != null)
            {
                dataLocalidades.InsertNota(notas);
            }
            return new JsonResult(notas) { Value = "Se registro la nota correctamente." };
        }

        [HttpPost]
        public ActionResult DeleteNotas(int id)
        {
            dataLocalidades.EliminarNota(id);
            return new JsonResult(id) { Value = "Se elimino la nota correctamente." };
        }

        //[HttpPost]
        //public ActionResult SelectNotas(int idLocalidad)
        //{
        //    return PartialView("lstNotas", dataLocalidades.NotasList(idLocalidad));
        //}

        [HttpPost]
        public ActionResult InsertCaracteristicas([FromBody] CaracteristicasInmueble c)
        {
            if (c != null)
            {
                dataLocalidades.InsertCaracteristicas(c);
            }
            return new JsonResult(c) { Value = "Se registro el concepto correctamente." };
        }

        [HttpPost]
        public ActionResult DeleteCaracteristicas(int id, int idusoinmueble)
        {
            dataLocalidades.EliminarCaracteristicas(id, idusoinmueble);
            return new JsonResult(id) { Value = "Se elimino el gasto correctamente." };
        }

        //[HttpPost]
        //public ActionResult SelectCaracteristicas(int idLocalidad, int IdUsoInmueble)
        //{
        //    return PartialView("lstCaracteristicas", dataLocalidades.CaracteristicasByLocalidad(idLocalidad, IdUsoInmueble));
        //}

        [HttpPost]
        public ActionResult SelectLocalidades(int clapro, string numloc, string nomcom)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 2;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);
            ViewBag.VB_idCartera = idCartera;
            return PartialView("lstLocalidades", dataLocalidades.GetListaLocalidades(clapro.ToString() == null ? 0 : clapro, numloc == null ? "" : numloc, nomcom == null ? "" : nomcom, idCartera, IdUsuario));
        }


    }
}