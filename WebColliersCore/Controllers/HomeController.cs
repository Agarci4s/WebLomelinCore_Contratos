using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebColliersCore.Data;
using WebColliersCore.Models;

namespace WebColliersCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ////Crea el munú de la ventana
            //Menu menu = new Menu();
            //ViewBag.listTransf_Opciones = menu.CreaMenu(Request.Cookies["CoreInmocontrol"]);
            return View();
        }

        public IActionResult Privacy()
        {
            ////Crea el munú de la ventana
            //Menu menu = new Menu();
            //ViewBag.listTransf_Opciones = menu.CreaMenu(Request.Cookies["CoreInmocontrol"]);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
