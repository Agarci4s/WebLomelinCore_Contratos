using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebColliersCore.Data;
using WebColliersCore.Models;

namespace WebColliersCore.Controllers
{
    [RequestFormLimits(ValueCountLimit = 5000)]
    public class PermisosController : Controller
    {
        // GET: Permiso
        public ActionResult Index()
        {
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "2" && rol != "1")
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }

            DataUsuarios dataUsuarios = new DataUsuarios();
            List<Usuario> listUsuarios = dataUsuarios.recuperaUsuariosActivos();
            return View(listUsuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return Redirect("~/Permisos");
            }
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "2" && rol != "1")
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }

            Usuario usuario = new Usuario();

            DataUsuarios dataUsuarios = new DataUsuarios();
            usuario = dataUsuarios.recuperaUsuario(id);
            //usuario.CancelarCFDIBool = usuario.CancelarCFDI > 0 ? false : true;
            //usuario.NivelStr = usuario.Nivel.ToString();

            DataTpCartera dataTpCartera = new DataTpCartera();
            usuario.listTpCarteras = dataTpCartera.recuperaTpCarteraList(id);

            DataTransf_Opciones transf_Opciones = new DataTransf_Opciones();
            usuario.listTransf_Opciones = transf_Opciones.RecuperaTransf_Opciones(id);

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {

            #region Validación de permisos
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            Request.Cookies.TryGetValue("CoreInmocontrolCartera", out string strCookies2);
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermisoSinlistTransf_Opciones(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                if (id == 0)
                    return Redirect("~/Permisos");

            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "2" && rol != "1")
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }
            #endregion

            Usuario usuario = new Usuario();
            DataUsuarios dataUsuarios = new DataUsuarios();
            usuario = dataUsuarios.recuperaUsuario(id);
            usuario.CancelarCFDIBool = usuario.CancelarCFDI > 0 ? false : true;

            DataTpCartera dataTpCartera = new DataTpCartera();
            usuario.listTpCarteras = dataTpCartera.recuperaTpCarteraList(id);

            DatadtInmuebleUsuario datadtInmuebleUsuario = new DatadtInmuebleUsuario();
            List<DtInmuebleUsuario> datadtInmuebleUsuariosList = datadtInmuebleUsuario.GetByCartera(id, idCartera);

            foreach (var item in usuario.listTpCarteras)
            {
                item.dtInmuebleUsuarioList = datadtInmuebleUsuariosList.Where(x => x.IdCartera == item.idCartera).ToList();
            }

            DataTransf_Opciones transf_Opciones = new DataTransf_Opciones();
            usuario.listTransf_Opciones = new List<Transf_Opciones>();
            usuario.listTransf_Opciones = transf_Opciones.RecuperaTransf_Opciones(id);

            DataSelectListItem dataSelectListItem = new DataSelectListItem();
            ViewBag.Niveles = dataSelectListItem.Niveles;

            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestFormLimits(ValueCountLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
        [DisableRequestSizeLimit]
        public ActionResult Edit(Usuario usuario, int id, IFormCollection collection)
        {
            try
            {
                #region Validación de permisos
                Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
                Request.Cookies.TryGetValue("CoreInmocontrolCartera", out string strCookies2);

                var claims = HttpContext.User.Claims;
                Menu menu = new Menu();
                int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
                if (!menu.ValidaPermisoSinlistTransf_Opciones(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                    return Redirect("~/Home");
                if (id == 0)
                    return Redirect("~/Permisos");

                if (strCookies != null)
                {
                    var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                    string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                    if (rol != "2" && rol != "1")
                        return Redirect("~/Home");
                }
                else
                {
                    return Redirect("~/Home");
                }
                #endregion

                usuario.IdUsuario = id;

                DataDtUsuarioCartera dataDtUsuarioCartera = new DataDtUsuarioCartera();
                var checkTpCarteras = collection["TpCarteras"];
                List<DtUsuarioCartera> dtUsuarioCarterasNew = new List<DtUsuarioCartera>();
                foreach (var item in checkTpCarteras)
                {
                    dtUsuarioCarterasNew.Add(new DtUsuarioCartera { idCartera = int.Parse(item), idUsuario = usuario.IdUsuario });
                }
                DataTpCartera dataTpCartera = new DataTpCartera();
                List<TpCartera> listTpCarteras = dataTpCartera.recuperaTpCarteraList(id);
                List<DtUsuarioCartera> dtUsuarioCarterasOld = new List<DtUsuarioCartera>();
                foreach (var item in listTpCarteras)
                {
                    if (item.carteraBool)
                        dtUsuarioCarterasOld.Add(new DtUsuarioCartera { idCartera = item.idCartera, idUsuario = usuario.IdUsuario });
                }
                dataDtUsuarioCartera.UpdateDtUsuarioCartera(dtUsuarioCarterasOld, dtUsuarioCarterasNew);


                var checkInmuebleUsuario = collection["InmuebleUsuario"];
                List<DtInmuebleUsuario> dtInmuebleUsuarioListNew = new List<DtInmuebleUsuario>();
                foreach (var item in checkInmuebleUsuario)
                {
                    dtInmuebleUsuarioListNew.Add(new DtInmuebleUsuario { idInmueble = int.Parse(item), IdUsuario = usuario.IdUsuario });
                }
                DatadtInmuebleUsuario datadtInmuebleUsuario = new DatadtInmuebleUsuario();
                List<DtInmuebleUsuario> dtInmuebleUsuarioList = datadtInmuebleUsuario.GetByCartera(id, idCartera); //datadtInmuebleUsuario.Get(id);
                List<DtInmuebleUsuario> dtInmuebleUsuarioListOld = new List<DtInmuebleUsuario>();
                foreach (var item in dtInmuebleUsuarioList)
                {
                    if (item.checkAux)
                        dtInmuebleUsuarioListOld.Add(new DtInmuebleUsuario { idInmueble = item.idInmueble, IdUsuario = usuario.IdUsuario });
                }
                datadtInmuebleUsuario.Update(dtInmuebleUsuarioListOld, dtInmuebleUsuarioListNew);



                DataTransf_Opciones_Usuarios dataTransf_Opciones_Usuarios = new DataTransf_Opciones_Usuarios();
                var checkTransf_Opciones = collection["Transf_Opciones"];
                List<Transf_Opciones_Usuarios> transf_Opciones_UsuariosNew = new List<Transf_Opciones_Usuarios>();
                foreach (var item in checkTransf_Opciones)
                {
                    string inputNiveles = "0";
                    try
                    {
                        inputNiveles = collection["Nivel-" + item];
                    }
                    catch
                    {
                    }
                    int.TryParse(inputNiveles, out int nivel);
                    transf_Opciones_UsuariosNew.Add(new Transf_Opciones_Usuarios { idTransfOpciones = int.Parse(item), IdUsuario = usuario.IdUsuario, Nivel = nivel });
                }

                DataTransf_Opciones DataTransf_Opciones = new DataTransf_Opciones();
                List<Transf_Opciones> transf_Opciones = DataTransf_Opciones.RecuperaTransf_Opciones(id);
                List<Transf_Opciones_Usuarios> transf_Opciones_UsuariosOld = new List<Transf_Opciones_Usuarios>();
                foreach (var item in transf_Opciones)
                {
                    if (item.checkTransf_Opciones)
                        transf_Opciones_UsuariosOld.Add(new Transf_Opciones_Usuarios { idTransfOpciones = item.idTransfOpciones, IdUsuario = usuario.IdUsuario, Nivel = item.Nivel });
                }
                dataTransf_Opciones_Usuarios.UpdateTransf_Opciones_Usuarios(transf_Opciones_UsuariosOld, transf_Opciones_UsuariosNew);

                return Redirect("~/Permisos");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return Redirect("~/Permisos");
            }
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                if (rol != "2" && rol != "1")
                    return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Home");
            }

            Usuario usuario = new Usuario();
            DataUsuarios dataUsuarios = new DataUsuarios();
            usuario = dataUsuarios.recuperaUsuario(id);
            usuario.CancelarCFDIBool = usuario.CancelarCFDI > 0 ? false : true;

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Usuario usuario, int id, IFormCollection collection)
        {
            try
            {
                if (id == 0)
                {
                    return Redirect("~/Permisos");
                }
                Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
                if (strCookies != null)
                {
                    var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                    string rol = LegacyCookieExtensions.Decrypt(legacyCookie["rol"], SystemComplementos.Key);
                    if (rol != "2" && rol != "1")
                        return Redirect("~/Home");
                }
                else
                {
                    return Redirect("~/Home");
                }

                DataDtUsuarioCartera dataDtUsuarioCartera = new DataDtUsuarioCartera();
                DataTpCartera dataTpCartera = new DataTpCartera();
                List<TpCartera> listTpCarteras = dataTpCartera.recuperaTpCarteraList(id);
                List<DtUsuarioCartera> dtUsuarioCarteras = new List<DtUsuarioCartera>();
                foreach (var item in listTpCarteras)
                {
                    if (item.carteraBool)
                        dtUsuarioCarteras.Add(new DtUsuarioCartera { idCartera = item.idCartera, idUsuario = usuario.IdUsuario });
                }
                dataDtUsuarioCartera.DeleteDtUsuarioCartera(dtUsuarioCarteras);


                DataTransf_Opciones_Usuarios dataTransf_Opciones_Usuarios = new DataTransf_Opciones_Usuarios();
                DataTransf_Opciones DataTransf_Opciones = new DataTransf_Opciones();
                List<Transf_Opciones> transf_Opciones = DataTransf_Opciones.RecuperaTransf_Opciones(id);
                List<Transf_Opciones_Usuarios> transf_Opciones_Usuariod = new List<Transf_Opciones_Usuarios>();
                foreach (var item in transf_Opciones)
                {
                    if (item.checkTransf_Opciones)
                        transf_Opciones_Usuariod.Add(new Transf_Opciones_Usuarios { idTransfOpciones = item.idTransfOpciones, IdUsuario = usuario.IdUsuario });
                }
                dataTransf_Opciones_Usuarios.DeleteTransf_Opciones_Usuarios(transf_Opciones_Usuariod);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}