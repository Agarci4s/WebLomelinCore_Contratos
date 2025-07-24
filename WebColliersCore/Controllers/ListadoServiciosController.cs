using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebColliersCore;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebLomelinCore.Data;


namespace WebLomelinCore.Controllers
{
    public class ListadoServiciosController : Controller
    {
        // GET: ListadoServiciosController
        public ActionResult Index()
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
            DataSelectService dataSelectListItem = new DataSelectService();
            ViewBag.Status = dataSelectListItem.getCamposStatusServicio;

            DataSelectService dataSelectListItemServicio = new DataSelectService();
            ViewBag.Servicio = dataSelectListItemServicio.getCamposTipoServicio;

            List<B_inmuebles_contrato> item = new List<B_inmuebles_contrato>
            {
                new B_inmuebles_contrato
                {
                    cr="010101010101",
                    ue="010101010101",
                    fecha_inicio= new System.DateTime(2025,01,01),
                    fecha_termino = new System.DateTime(2025,01,31),
                    estatus="aprobado"
                },
                new B_inmuebles_contrato
            {
                    cr = "020202020202",
                    ue = "020202020202",
                    fecha_inicio = new System.DateTime(2025, 02, 01),
                    fecha_termino = new System.DateTime(2025, 02, 28),
                    estatus = "rechazado"
                },
            new B_inmuebles_contrato
            {
                cr = "030303030303",
                ue = "030303030303",
                fecha_inicio = new System.DateTime(2025, 03, 01),
                fecha_termino = new System.DateTime(2025, 03, 31),
                estatus = "pagado"
                },
            new B_inmuebles_contrato
            {
                cr = "040404040404",
                ue = "040404040404",
                fecha_inicio = new System.DateTime(2025, 04, 01),
                fecha_termino = new System.DateTime(2025, 04, 30),
                estatus = "cargado"
            }
            };
            ViewBag.TestList = item;

            return View();
        }

        // GET: ListadoServiciosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListadoServiciosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListadoServiciosController/Create
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

        // GET: ListadoServiciosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListadoServiciosController/Edit/5
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

        // GET: ListadoServiciosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListadoServiciosController/Delete/5
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
