using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebColliersCore;
using WebColliersCore.Data;
using WebLomelinCore.Data;
using WebLomelinCore.Models;

namespace WebLomelinCore.Controllers
{
    public class GenerarPeriodosServiciosController : Controller
    {

        private bool InicializaVista(int? IdServicio, int? IdPeriodicidad, int? IdPerido, int? IdBimestre)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            bool response = menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims);
            #endregion

            ViewBag.Servicios = PagosServicios.setItem(PagosServicios.getTipoSerivcios, IdServicio);
            ViewBag.Periodicidad = PagosServicios.setItem(PeriodosServicios.getPeriodicidad, IdPeriodicidad);
            ViewBag.Periodos = PagosServicios.setItem(PeriodosServicios.getPeriodosSiponibles, IdPerido);
            ViewBag.Bimestres = PagosServicios.setItem(PeriodosServicios.getBimestres, IdBimestre);

            return response;
        }

        public IActionResult Index()
        {
            if (!InicializaVista(null,null,null,null))
                return Redirect("~/Home");

            return View();
        }

        [HttpPost]
        public IActionResult Index(PeriodosServicios model)
        {
            if (!InicializaVista(model.IdServicio,model.IdPeriodicidad,model.IdPeriodoDisponible,model.IdBimestre))
                return Redirect("~/Home");


            //  guarda en bd
            return View();
        }
    }
}
