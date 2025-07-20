using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using System.Collections.Generic;
using WebColliersCore.Data;
using WebColliersCore.Models;
using WebColliersCore;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using WebLomelinCore.Data;

namespace WebLomelinCore.Controllers
{
    public class B_inmuebles_visitas_organiza_imagenesController : Controller
    {


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

        // GET: InmueblesVisitasReporteController
        public ActionResult Index()
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            EstablecerPermisos(IdUsuario, System.Reflection.MethodBase.GetCurrentMethod(), idCartera);


            return View();
        }



        [HttpPost]
        public ActionResult OrganizaImagenes(int tipo, DateTime fecha, DateTime fecha2)
        {


            if (fecha > fecha2)
            {
                DateTime aux = fecha2;
                fecha2 = fecha;
                fecha = aux;
            }

            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return Redirect("~/Home");
            #endregion

            DataInmueblesContratos dataInmueblesContratos = new DataInmueblesContratos();
            List<B_inmuebles_contrato> b_Inmuebles_Contrato_Correos = new List<B_inmuebles_contrato>();

            if (tipo == 1)
                CreaCarpeta(IdUsuario, fecha, fecha2);


            return Json(new { userNull = true });
        }

        public void CreaCarpeta(int IdUsuario2, DateTime fecha, DateTime fecha2)
        {
            #region Validación de permisos
            var claims = HttpContext.User.Claims;
            Menu menu = new Menu();
            int IdUsuario = 0, idCartera = 0, tipoNivel = 0;//0 , 1-detalle,2-editar y detalle, 3 crear-eliminar, editar y detalle   
            if (!menu.ValidaPermiso(System.Reflection.MethodBase.GetCurrentMethod(), ref IdUsuario, ref idCartera, ref tipoNivel, claims))
                return;
            #endregion

            DataInmueblesVisita dataInmueblesVisita = new DataInmueblesVisita();
            List<B_inmuebles_visitas> b_Inmuebles_Visitas = new List<B_inmuebles_visitas>();
            b_Inmuebles_Visitas = dataInmueblesVisita.Get(idCartera, IdUsuario2, fecha, fecha2);

            if (b_Inmuebles_Visitas.Count > 0)
            {
                foreach (var item in b_Inmuebles_Visitas)
                {
                    try
                    {


                        string filePath = Environment.CurrentDirectory;
                        filePath = Path.Combine(filePath, "Images");
                        filePath = Path.Combine(filePath, item.id_b_inmuebles.ToString("000"));
                        filePath = Path.Combine(filePath, item.fecha_visita.ToString("yyMMdd"));


                        if (Directory.Exists(filePath))
                        {
                            DataInmuebles dataInmuebles = new DataInmuebles();
                            B_inmuebles b_Inmuebles = dataInmuebles.Get(1, IdUsuario2, item.id_b_inmuebles);//IdUsuario2=2131
                            if (b_Inmuebles == null)
                                break;

                            string cr = b_Inmuebles.cr;



                            int tam_cadena = b_Inmuebles.cr.Length;
                            if (tam_cadena >= 4)
                            {
                                cr = b_Inmuebles.cr.Substring((tam_cadena - 4), 4);
                            }
                            string filePathNew = Environment.CurrentDirectory;
                            filePathNew = Path.Combine(filePathNew, "ImagesOrdenadas");
                            String nombreAux = Regex.Replace(b_Inmuebles.nombre, @"[^\w\.@-]", " ").Trim();
                            filePathNew = Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_" + cr + "_" + nombreAux);
                            if (!Directory.Exists(filePathNew))
                            {
                                Directory.CreateDirectory(filePathNew);
                            }

                            int contador = 1;
                            string filePathAux = filePath;

                            //    //Interior
                            //    contador = 1;
                            //    if (item.interior1.Count() > 3)
                            //    {
                            //        filePathAux = filePath;
                            //        filePathAux = Path.Combine(filePathAux, item.interior1);
                            //        try
                            //        {
                            //            string nuevaDireccion = Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_IN_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.interior1));
                            //            System.IO.File.Copy(filePathAux, nuevaDireccion, true);
                            //            contador++;
                            //        }
                            //        catch (Exception) { }
                            //    }
                            //    if (item.interior2.Count() > 3)
                            //    {
                            //        filePathAux = filePath;
                            //        filePathAux = Path.Combine(filePathAux, item.interior2);
                            //        try
                            //        {
                            //            System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_IN_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.interior2)), true);
                            //            contador++;
                            //        }
                            //        catch (Exception) { }
                            //    }
                            //    if (item.espacio1.Count() > 3)
                            //    {
                            //        filePathAux = filePath;
                            //        filePathAux = Path.Combine(filePathAux, item.espacio1);
                            //        try
                            //        {
                            //            System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_IN_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.espacio1)), true);
                            //            contador++;
                            //        }
                            //        catch (Exception) { }
                            //    }
                            //    if (item.espacio2.Count() > 3)
                            //    {
                            //        filePathAux = filePath;
                            //        filePathAux = Path.Combine(filePathAux, item.espacio2);
                            //        try
                            //        {
                            //            System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_IN_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.espacio2)), true);
                            //            contador++;
                            //        }
                            //        catch (Exception) { }
                            //    }


                            //    if (b_Inmuebles.id_b_cg_clasificacion != 3)
                            //    {
                            //        if (item.exterior1.Count() > 3)
                            //        {
                            //            filePathAux = Path.Combine(filePathAux, item.exterior1);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_EX_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.exterior1)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }
                            //        if (item.exterior2.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.exterior2);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_EX_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.exterior2)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }
                            //        if (item.circundante_norte.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.circundante_norte);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_EX_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.circundante_norte)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }
                            //        if (item.circundante_sur.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.circundante_sur);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_EX_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.circundante_sur)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }
                            //        if (item.circundante_oriente.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.circundante_oriente);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_EX_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.circundante_oriente)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }
                            //        if (item.circundante_poniente.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.circundante_poniente);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_EX_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.circundante_poniente)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }

                            //        contador = 1;
                            //        if (item.toma_agua.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.toma_agua);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_SE_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.toma_agua)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }
                            //        if (item.medidor_energia.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.medidor_energia);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_SE_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.medidor_energia)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }

                            //    }

                            //    if (b_Inmuebles.id_b_cg_clasificacion == 3)
                            //    {
                            //        contador = 1;
                            //        if (item.exterior1.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.exterior1);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_ES_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.exterior1)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }
                            //        if (item.exterior2.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.exterior2);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_ES_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.exterior2)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }
                            //        if (item.circundante_norte.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.circundante_norte);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_ES_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.circundante_norte)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }
                            //        if (item.circundante_oriente.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.circundante_oriente);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_ES_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.circundante_oriente)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }
                            //        if (item.circundante_poniente.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.circundante_poniente);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_ES_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.circundante_poniente)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }
                            //        if (item.circundante_sur.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.circundante_sur);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_ES_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.circundante_sur)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }
                            //        if (item.estacionamiento.Count() > 3)
                            //        {
                            //            filePathAux = filePath;
                            //            filePathAux = Path.Combine(filePathAux, item.circundante_sur);
                            //            try
                            //            {
                            //                System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_ES_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.circundante_sur)), true);
                            //                contador++;
                            //            }
                            //            catch (Exception) { }
                            //        }
                            //    }

                            //}



                            if (item.sucursal.Count() > 3)
                            {
                                filePathAux = filePath;
                                filePathAux = Path.Combine(filePathAux, item.sucursal);
                                try
                                {
                                    System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_SUC_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.sucursal)), true);
                                    contador++;
                                }
                                catch (Exception) { }
                            }
                            if (item.exterior1.Count() > 3)
                            {
                                filePathAux = filePath;
                                filePathAux = Path.Combine(filePathAux, item.exterior1);
                                try
                                {
                                    System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_EX_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.exterior1)), true);
                                    contador++;
                                }
                                catch (Exception) { }
                            }
                            if (item.exterior2.Count() > 3)
                            {
                                filePathAux = filePath;
                                filePathAux = Path.Combine(filePathAux, item.exterior2);
                                try
                                {
                                    System.IO.File.Copy(filePathAux, Path.Combine(filePathNew, b_Inmuebles.ue.ToString() + "_EX_" + contador.ToString("000")) + Path.GetExtension(Path.GetExtension(item.exterior2)), true);
                                    contador++;
                                }
                                catch (Exception) { }
                            }




                        }
                    }
                    catch (Exception e)
                    {

                    }



                }
            }

        }


    }
}
