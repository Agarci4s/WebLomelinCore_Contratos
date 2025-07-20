using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NPOI.SS.Formula.Functions;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebLomelinCore.Data;

namespace WebColliersCore.Controllers
{
    public class CarteraController : Controller
    {
        // GET: Cartera
        public ActionResult Index()
        {
            Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
            if (strCookies != null)
            {
                DataUsuarios dataUsuarios = new DataUsuarios();
                Usuario usuario = dataUsuarios.RecuperaUsuario(strCookies);

                DataTpCartera dataTpCartera = new DataTpCartera();
                List<TpCartera> tpCarterasList = dataTpCartera.GetByUser(usuario.IdUsuario);

                List<SelectListItem> SelectListItemCarteras = new List<SelectListItem>();
                SelectListItemCarteras.Add(new SelectListItem { Value = "0", Text = "Seleccione una cartera" }); ;
                foreach (var item in tpCarterasList)
                {
                    SelectListItemCarteras.Add(new SelectListItem { Value = item.idCartera.ToString(), Text = item.descripcionCartera, }); ;
                }
                ViewBag.Carteras = SelectListItemCarteras;

                Request.Cookies.TryGetValue("CoreInmocontrolCartera", out string strCookiesCartera);
                TpCartera tpCartera = new TpCartera(); ;
                if (strCookiesCartera != null)
                {
                    tpCartera = dataUsuarios.RecuperaCartera(strCookiesCartera);
                    tpCartera.idCartera = 0;
                }

                return View(tpCartera);
            }
            else
                return Redirect("~/Home");
        }

        // Post: Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IndexAsync(IFormCollection collection, TpCartera tpCartera)
        {
            try
            {
                Request.Cookies.TryGetValue("CoreInmocontrol", out string strCookies);
                if (tpCartera.idCartera > 0)
                {
                    DataUsuarios dataUsuarios = new DataUsuarios();
                    Usuario usuario = dataUsuarios.RecuperaUsuario(strCookies);
                    DataTpCartera dataTpCartera = new DataTpCartera();
                    TpCartera tpCarterasList = dataTpCartera.GetByUser(usuario.IdUsuario).Where(x=>x.idCartera == tpCartera.idCartera).FirstOrDefault();


                    Response.Cookies.Delete("CoreInmocontrolCartera", new CookieOptions()
                    {
                        //Secure = true,
                        Expires = DateTime.Now.AddDays(-1),

                    });

                    var collieresCookie = new Dictionary<string, string>(){
                        { "cartera", LegacyCookieExtensions.Encrypt(tpCartera.idCartera.ToString(), SystemComplementos.Key) },
                        { "carteraN", tpCarterasList.descripcionCartera}
                    };
                    Response.Cookies.Append("CoreInmocontrolCartera", LegacyCookieExtensions.ToLegacyCookieString(collieresCookie));



                    //Actualiza el claim
                    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                    Claim claimUserName = new Claim(ClaimTypes.Name, usuario.Nombre);
                    Claim claimRole = new Claim(ClaimTypes.Role, "Admin1234");
                    Claim claimIdUsuario = new Claim("IdUsuario", usuario.IdUsuario.ToString());
                    Claim claimEmail = new Claim("EmailUsuario", usuario.Email);
                    Claim claimNameCartera = new Claim("NameCartera", tpCarterasList.descripcionCartera);
                    Claim claimCartera = new Claim("Cartera", tpCarterasList.idCartera.ToString());

                    identity.AddClaim(claimUserName);
                    identity.AddClaim(claimRole);
                    identity.AddClaim(claimIdUsuario);
                    identity.AddClaim(claimEmail);
                    identity.AddClaim(claimNameCartera);
                    identity.AddClaim(claimCartera);

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.Now.AddMinutes(45)
                    });




                    return Redirect("~/Home");
                }
                else
                {
                    DataUsuarios dataUsuarios = new DataUsuarios();
                    Usuario usuario = dataUsuarios.RecuperaUsuario(strCookies);
                    DataTpCartera dataTpCartera = new DataTpCartera();
                    List<TpCartera> tpCarterasList = dataTpCartera.GetByUser(usuario.IdUsuario);
                    List<SelectListItem> SelectListItemCarteras = new List<SelectListItem>();
                    SelectListItemCarteras.Add(new SelectListItem { Value = "0", Text = "Seleccione una cartera" }); ;
                    foreach (var item in tpCarterasList)
                    {
                        SelectListItemCarteras.Add(new SelectListItem { Value = item.idCartera.ToString(), Text = item.descripcionCartera, }); ;
                    }
                    ViewBag.Carteras = SelectListItemCarteras;
                    return View();

                }
            }
            catch
            {
                return Redirect("~/Home");
            }


        }

       
    }

}