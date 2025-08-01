using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Debugger.Contracts.EditAndContinue;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;
using WebLomelinCore.Models;

namespace WebLomelinCore.Data
{
    public class DataSelectService
    {
        private Conexion conexion = new Conexion();
        private List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

        /// <summary>
        /// Obtiene las regiones
        /// </summary>
        public List<SelectListItem> getRegionesList
        {
            get
            {
                DataTable dataTable = conexion.RunStoredProcedure("cg_region_get", listSqlParameters);
                List<SelectListItem> response = (from item in dataTable.Rows.Cast<DataRow>()
                                                 select new SelectListItem
                                                 {
                                                     Value = item.Field<int>("id_b_cg_region").ToString(),
                                                     Text = item.Field<string>("nombre"),
                                                 }).ToList();
                response.Add(new SelectListItem
                {
                    Text = "Seleccione una región",
                    Value = "0"
                });
                return response;
            }
        }


        /// <summary>
        /// Obtiene los estatus
        /// </summary>
        public List<SelectListItem> getStatusServicio
        {
            get
            {
                DataTable dataTable = conexion.RunStoredProcedure("getStatusPagosServicios", listSqlParameters);
                List<SelectListItem> response = (from item in dataTable.Rows.Cast<DataRow>()
                                                 select new SelectListItem
                                                 {
                                                     Value = item.Field<int>("IdStatus").ToString(),
                                                     Text = item.Field<string>("STATUS"),
                                                 }).ToList();
                response.Add(new SelectListItem
                {
                    Text = "Seleccione un status",
                    Value = "0"
                });
                return response;
            }
        }
        /// <summary>
        /// Obtiene los tipos de servicios
        /// </summary>
        public List<SelectListItem> getTipoServicio 
        {
            get
            {
                DataTable dataTable = conexion.RunStoredProcedure("getTiposPagosServicios", listSqlParameters);
                List<SelectListItem> response = (from item in dataTable.Rows.Cast<DataRow>()
                                                 select new SelectListItem
                                                 {
                                                     Value = item.Field<int>("IdTipo").ToString(),
                                                     Text = item.Field<string>("Tipo"),
                                                 }).ToList();
                response.Add(new SelectListItem
                {
                    Text = "Seleccione un servicio",
                    Value = "0"
                });
                return response;
            }

        }

        /// <summary>
        /// Obtiene las cuentas, por idinmueble, localidad, tipo de servicio, estatus (si es null, se trae por todos los estatus)
        /// </summary>
        /// <param name="IdInmueble"></param>
        /// <param name="IdLocalidad"></param>
        /// <param name="IdTipoServicio"></param>
        /// <param name="IdEstatus"></param>
        /// <returns>Listado de cuentas</returns>
        public List<SelectListItem> getCuentas(int? IdInmueble, int? IdLocalidad, int? IdTipoServicio)
        {
            DataTable dataTable;
            conexion=new Conexion();
            List<SelectListItem> response = new List<SelectListItem>();
            
            if (IdInmueble.HasValue && IdLocalidad.HasValue && IdTipoServicio.HasValue)
            {
                listSqlParameters.Add(new MySqlParameter("IdInmueble_in", IdInmueble));
                listSqlParameters.Add(new MySqlParameter("IdLocalidad_in", IdLocalidad));
                if (IdTipoServicio==1) //agua
                {
                    
                    dataTable = conexion.RunStoredProcedure("getCuentaAguaByInmuebleLocalidad", listSqlParameters);
                    response = (from item in dataTable.Rows.Cast<DataRow>()
                                                     select new SelectListItem
                                                     {
                                                         Value = item.Field<int>("Id").ToString(),
                                                         Text = item.Field<string>("Cuenta"),
                                                     }).ToList();
                }
                else if (IdTipoServicio == 2) //luz
                {
                    dataTable = conexion.RunStoredProcedure("getCuentaLuzByInmuebleLocalidad", listSqlParameters);
                     response = (from item in dataTable.Rows.Cast<DataRow>()
                                                     select new SelectListItem
                                                     {
                                                         Value = item.Field<int>("Id").ToString(),
                                                         Text = item.Field<string>("Cuenta"),
                                                     }).ToList();
                }
                else if (IdTipoServicio == 3) //predial
                {
                    dataTable = conexion.RunStoredProcedure("getCuentaPredialByInmuebleLocalidad", listSqlParameters);
                     response = (from item in dataTable.Rows.Cast<DataRow>()
                                                     select new SelectListItem
                                                     {
                                                         Value = item.Field<int>("Id").ToString(),
                                                         Text = item.Field<string>("Cuenta"),
                                                     }).ToList();
                }
            }
             
            response.Add(new SelectListItem
            {
                Text = "Seleccione una cuenta",
                Value = "0"
            });
            return response;
        }

        public static PagoUnificadoDTO getPagoServiciosList(int? IdInmueble, int? IdLocalidad, int? IdCuenta, int? IdTipoServicio, int? Estatus)
        {
            PagoUnificadoDTO response = new PagoUnificadoDTO();
            if (IdTipoServicio == 1)/*agua*/
            {
                response.PagosAgua = new List<pagosagua>
                {
                  new pagosagua
                  {
                      idPagoAgua=1,
                      CuentaAgua="1010101",
                      FechaLectura1=DateTime.Now,
                      Lectura1=10,
                      FechaLectura2 = DateTime.Now,
                      Lectura2=20,
                               ImporteHabitacional=10,
                               ImporteComercial=10,
                               IvaComercial=10,
                               Recargos=10,
                               Actualizacion=10,
                               Multas=10,
                                          GastosEjecucion=10,
                                                              ConceptoPago="Agua",
                                                              FechaVencimiento=DateTime.Now,
                                                                                            UsuarioAutoriza=1,
                                                                                            UsuarioAutorizaDescripcion="Msilver",
                                                                                            StatusProceso=1,
                                                                                            StatusProcesoDescripcion="Registrado"
                  }
                };
            }
            else if (IdTipoServicio == 2)/*luz*/
            {
                response.PagosLuz = new List<pagosluz>
                   {
                       new pagosluz
                       {
                               idDtPagosLuz=1,
                               CuentaLuz="1010101",
                               fechaPago=DateTime.Now,
                                             periodoPago="Julio-Agosto",
                                                                 importe=100,
                                                                  iva=650,
                                                                           conceptoPago="Luz" ,
                                                                           UsuarioAutorizaDescripcion="Msilver",
                                                                           StatusProcesoDescripcion="Registrado"


                       }
                   };
            }
            else if (IdTipoServicio == 3)/*predial*/
            {
                response.PagosPredial = new List<pagospredial>
                {
                    new pagospredial
                    {
                             idDtPagosPredial=1,
                                CuentaPredial="110011",
                                periodoPago="Julio-Agosto",
                                  Recargos=10,
                                  Multas=10,
                                     importe=10,
                                              iva = 10,
                                                  Actualizacion = 10,
                                                                     conceptoPago="predial",
                                                                                              fechaPagolimite=DateTime.Now,
                                                                                              UsuarioAutorizaDescripcion="Msilver",
                                                                           StatusProcesoDescripcion="Registrado"


                    },
                };
            }
            return response;
        }
    }
}
