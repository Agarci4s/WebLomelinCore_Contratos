using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebColliersCore.Data;
using WebColliersCore.Models;
using System.Threading.Tasks;

namespace WebColliersCore.Controllers
{
    public class InicioSesionController : Controller
    {
        // GET: InicioSesion
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> IndexAsync(int id, UsuarioLogin usuarioLogin)
        {
            DataUsuarios dataUsuarios = new DataUsuarios();
            string username = usuarioLogin.Username;
            string password = usuarioLogin.Password;
            Usuario usuario = dataUsuarios.RecuperaUsuario(username, password);
            DateTime dateTime = DateTime.Now;
            int fechaNum = int.Parse(dateTime.ToString("MMddHHmm"));
            dateTime = dateTime.AddMinutes(SystemComplementos.tiempoSesion);

            if (usuario.IdUsuario > 0)
            {
                //claim
                //DEBEMOS CREAR UNA IDENTIDAD (name y role)
                //Y UN PRINCIPAL
                //DICHA IDENTIDAD DEBEMOS COMBINARLA CON LA COOKIE DE 
                //AUTENTIFICACION
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                //TODO USUARIO PUEDE CONTENER UNA SERIE DE CARACTERISTICAS
                //LLAMADA CLAIMS.  DICHAS CARACTERISTICAS PODEMOS ALMACENARLAS
                //DENTRO DE USER PARA UTILIZARLAS A LO LARGO DE LA APP
                Claim claimUserName = new Claim(ClaimTypes.Name, usuario.Nombre);
                Claim claimRole = new Claim(ClaimTypes.Role, "Admin");
                Claim claimIdUsuario = new Claim("IdUsuario", usuario.IdUsuario.ToString());
                Claim claimEmail = new Claim("EmailUsuario", usuario.Email);
                Claim claimCartera = new Claim("Cartera", usuario.idCartera.ToString());

                //HttpContext.Session.SetString("name", usuario.Nombre);
                identity.AddClaim(claimUserName);
                identity.AddClaim(claimRole);
                identity.AddClaim(claimIdUsuario);
                identity.AddClaim(claimEmail);
                identity.AddClaim(claimCartera);

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(45)
                });




                //// Set current principal
                //Thread.CurrentPrincipal = claimsPrincipal;


                bool debugger = false;

                if (System.Diagnostics.Debugger.IsAttached)
                {
                    debugger = true;
                }

                if (debugger || usuario.Status <= fechaNum)
                {
                    usuario.Status = int.Parse(dateTime.ToString("MMddHHmm"));
                    dataUsuarios.UsuarioUpdateStatus(usuario);
                    if (Request.Cookies["CoreInmocontrol"] == null)
                    {
                        password = LegacyCookieExtensions.Encrypt(password, SystemComplementos.Key);
                        var collieresCookie = new Dictionary<string, string>(){
                        { "userName", username },
                        { "userName2",password },
                        { "name",usuario.Nombre },
                        { "apellido",usuario.Nombre },
                        { "nivel",LegacyCookieExtensions.Encrypt(usuario.Nivel.ToString(),  SystemComplementos.Key) },
                        { "rol",LegacyCookieExtensions.Encrypt(usuario.Rol.ToString(),  SystemComplementos.Key) },
                        { "id",LegacyCookieExtensions.Encrypt(usuario.IdUsuario.ToString(),  SystemComplementos.Key)  }
                        };
                        CookieOptions option = new CookieOptions();
                        option.Expires = dateTime;
                        option.IsEssential = true;
                        Response.Cookies.Append("CoreInmocontrol", LegacyCookieExtensions.ToLegacyCookieString(collieresCookie), option);

                        Response.Cookies.Delete("CoreInmocontrolCartera");
                        var collieresCookieCartera = new Dictionary<string, string>(){
                        { "cartera", LegacyCookieExtensions.Encrypt("0", SystemComplementos.Key) },
                        { "carteraN", "Cartera" }
                        };
                        Response.Cookies.Append("CoreInmocontrolCartera", LegacyCookieExtensions.ToLegacyCookieString(collieresCookieCartera));
                    }
                    return Redirect("~/Cartera");
                }
                else
                {
                    ViewBag.MuestraError2 = true;
                    return View();
                }

            }
            else
            {
                ViewBag.MuestraError = true;
                return View();
            }
        }

        public async Task<ActionResult> CloseSesionAsync(int id)
        {
            try
            {
                //Claims
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                // return Redirect("~/Home");



                Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
                if (strCookies != null)
                {
                    DataUsuarios dataUsuarios = new DataUsuarios();
                    var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                    string username = legacyCookie["userName"];
                    string password = LegacyCookieExtensions.Decrypt(legacyCookie["userName2"], SystemComplementos.Key);
                    Usuario usuario = dataUsuarios.RecuperaUsuario(username, password);
                    usuario.Status = 1;
                    dataUsuarios.UsuarioUpdateStatus(usuario);
                    Response.Cookies.Delete("CoreInmocontrol");
                    Response.Cookies.Delete("CoreInmocontrolCartera");
                    return Redirect("~/Home");
                }
            }
            catch (Exception)
            {
            }
            return Redirect("~/Home");
        }

        [HttpPost]
        public ActionResult ActualizaSesion()
        {
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strRequest2);
            if (strRequest2 != null)
            {
                Menu menu2 = new Menu();
                var dateTime2 = DateTime.Now.AddMinutes(SystemComplementos.tiempoSesion);
                CookieOptions option2 = new CookieOptions();
                option2.Expires = dateTime2;
                option2.IsEssential = true;
                Response.Cookies.Append("CoreInmocontrol", strRequest2, option2);
                menu2.AcualizaStatusUsuario(strRequest2);
            }
            return Json(1);
        }


        [HttpPost]
        public ActionResult ActualizaUsuarioStatus()
        {
            string idUsuario = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value;
            int id = 0;
            int.TryParse(idUsuario, out id);

            if (id > 0)
            {
                DataUsuarios dataUsuarios = new DataUsuarios();
                dataUsuarios.UsuarioUpdateStatus(new Usuario { IdUsuario = id, Status = 2 });

                try
                {

                    //Response.Cookies.Delete("CoreInmocontrol");
                    //Response.Cookies.Delete("CoreInmocontrolCartera");

                    Response.Cookies.Delete("CoreInmocontrol", new CookieOptions()
                    {
                        //Secure = true,
                        Expires = DateTime.Now.AddDays(-1),
                    });
                    Response.Cookies.Delete("CoreInmocontrolCartera", new CookieOptions()
                    {
                        //Secure = true,
                        Expires = DateTime.Now.AddDays(-1),

                    });

                    //foreach (var cookie in Request.Cookies.Keys)
                    //{
                    //    Response.Cookies.Delete(cookie);
                    //}

                }
                catch (Exception)
                {
                }
            }


            return Json(1);
        }

       // RecuperaInformacion

        public ActionResult RecuperaNombreCartera()
        {
            string cartera = "";

            Request.Cookies.TryGetValue("CoreInmocontrolCartera", out string strCookies);
            Request.Cookies.TryGetValue("CoreInmocontrolCartera", out string strCookies2);

            if (strCookies2 != null)
            {
                cartera = "Cartera";
            }
            if (strCookies != null)
            {
                var legacyCookie = LegacyCookieExtensions.FromLegacyCookieString(strCookies);
                cartera = legacyCookie["carteraN"];
            }

            return Json(cartera);
        }
    }
}