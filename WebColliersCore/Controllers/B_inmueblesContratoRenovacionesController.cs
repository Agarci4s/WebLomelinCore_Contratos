using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using WebColliersCore;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebLomelinCore.Data;
using WebLomelinCore.Models;

namespace WebLomelinCore.Controllers
{
    public class B_inmueblesContratoRenovacionesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Adela()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesRenovaciones dataInmueblesRenovaciones = new();
            DataInmuebles dataInmuebles = new();
            NegociacionesAdela renovacionAdela = dataInmueblesRenovaciones.GetByIdAdela(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_Inmuebles_Contrato_Distribucions = null;
            return View(renovacionAdela);
        }

        public ActionResult ConvenioModificatorio()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesRenovaciones dataInmueblesRenovaciones = new();
            DataInmuebles dataInmuebles = new();
            NegociacionesConvenioModificatorio renovacionConvenioModificatorio = dataInmueblesRenovaciones.GetByIdConvenioModificatorio(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_Inmuebles_Contrato_Distribucions = null;
            return View(renovacionConvenioModificatorio);
        }

        public ActionResult Renovacion()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesRenovaciones dataInmueblesRenovaciones = new();
            DataInmuebles dataInmuebles = new();
            NegociacionesRenovacion renovacionAdela = dataInmueblesRenovaciones.GetByIdRenovacion(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_Inmuebles_Contrato_Distribucions = null;
            return View(renovacionAdela);
        }

        public ActionResult RenovacionAdela()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 1;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesRenovaciones dataInmueblesRenovaciones = new();
            DataInmuebles dataInmuebles = new();
            RenovacionAdela renovacionAdela = dataInmueblesRenovaciones.GetById(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_inmuebles = dataInmuebles.Get(idCartera, IdUsuario, 130569);
            //renovacionAdela.b_Inmuebles_Contrato_Distribucions = null;
            return View(renovacionAdela);
        }
    }
}
