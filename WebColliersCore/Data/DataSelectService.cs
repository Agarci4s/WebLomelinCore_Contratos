using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public List<SelectListItem> getBimestre
        {
            get {
                return new List<SelectListItem>
                {
                      new SelectListItem
                  {
                      Value="0",
                      Text="Seleccione una opción"
                  },
                  new SelectListItem
                  {
                      Value="1",
                      Text="Enero - Febrero"
                  },
                  new SelectListItem
                  {
                      Value="1",
                      Text="Marzo - Abril"
                  },
                  new SelectListItem
                  {
                      Value="1",
                      Text="Mayo - Junio"
                  },
                  new SelectListItem
                  {
                      Value="1",
                      Text="Julio - Agosto"
                  },
                  new SelectListItem
                  {
                      Value="1",
                      Text="Septiembre - Octubre"
                  },
                  new SelectListItem
                  {
                      Value="1",
                      Text="Noviembre - Diciembre"
                  },
                };
            }
        }

        public List<SelectListItem> getPeriodosDiponibles
        {
            get {
                return new List<SelectListItem>
                {
                     new SelectListItem
                  {
                      Value="0",
                      Text="Seleccione una opción"
                  },
                  new SelectListItem
                  {
                      Value="1",
                      Text="2025"
                  },
                  new SelectListItem
                  {
                      Value="1",
                      Text="2026"
                  },
                  new SelectListItem
                  {
                      Value="1",
                      Text="2027"
                  },
                };
            }
        }

        public List<SelectListItem> getPeriodicidad
        {
            get
            {
                return new List<SelectListItem>
                {
                  new SelectListItem
                  {
                      Value="0",
                      Text="Seleccione una opción"
                  },
                  new SelectListItem
                  {
                      Value="1",
                      Text="Bimestral"
                  },
                  new SelectListItem
                  {
                      Value="2",
                      Text="Anual"
                  }

                };
            }
        }

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
                response.Insert(0, new SelectListItem
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

        public PagoUnificadoDTO getPagoServiciosList(
            int? IdInmueble, 
            int? IdLocalidad, 
            int? IdCuenta, 
            int? IdTipoServicio, 
            int? Estatus
            /*int? IdPagoServicio,
            int? IdCuentaServicio */)
            //agregar idpagoservicio, idcuentaservicio = int?
        {
            PagoUnificadoDTO response = new PagoUnificadoDTO();
            if (IdTipoServicio == 1)/*agua*/
            {
                /*
                 * parametros
                 */
                var listSqlParameters = new List<MySqlParameter>
                {
                    new MySqlParameter("IdInmueble", IdInmueble ?? (object)DBNull.Value),
                    new MySqlParameter("IdLocalidad", IdLocalidad ?? (object)DBNull.Value),
                    new MySqlParameter("IdCuenta", IdCuenta ?? (object)DBNull.Value),
                    new MySqlParameter("IdTipoServicio", IdTipoServicio ?? (object)DBNull.Value),
                    new MySqlParameter("Estatus", Estatus ?? (object)DBNull.Value),
                    /*new MySqlParameter("IdPagoServicio", IdPagoServicio ?? (object)DBNull.Value),
                    new MySqlParameter("IdCuentaServicio", IdCuentaServicio ?? (object)DBNull.Value)*/
                };

                DataTable dataTable = conexion.RunStoredProcedure("Cuentas_pagos_agua_Get", listSqlParameters);
                
                List<pagosagua> agualist = (from item in dataTable.Rows.Cast<DataRow>()
                                                 select new pagosagua
                                                 {
                                                     idPagoAgua = item.Field<int>("idDtPagosAgua"),
                                                     idCuentaAgua = item.Field<int>("IdCuentaAgua"),
                                                     ConsumoBimestral = item.Field<double>("ConsumoBimestral"),
                                                     FechaLectura1 = item.Field<DateTime>("fechaLectura1"),
                                                     Lectura1 = item.Field<float>("Lectura1"),
                                                     FechaLectura2= item.Field<DateTime>("fechaLectura2"),
                                                     Lectura2 = item.Field<float>("Lectura2"),
                                                     ImporteHabitacional = item.Field<double>("importeHabitacional"),
                                                     ImporteComercial = item.Field<double>("importeComercial"),
                                                     IvaComercial = item.Field<double>("ivaComercial"),
                                                     Recargos = item.Field<double>("Recargos"),
                                                     Actualizacion = item.Field<double>("Actualizacion"),
                                                     Multas = item.Field<double>("Multas"),
                                                     GastosEjecucion = item.Field<double>("GastosEjecucion"),
                                                     FechaVencimiento=item.Field<DateTime>("FechaVencimiento"),
                                                     StatusProceso = item.Field<int>("statusProceso"),
                                                     status = item.Field<string>("STATUS"),
                                                     InmuebleData=new B_inmuebles
                                                     {
                                                         ue = item.Field<int>("ue"),
                                                         cr = item.Field<string>("cr"),
                                                         nombre = item.Field<string>("nombre")
                                                     }
                                                 }).ToList();
                response.PagosAgua = new List<pagosagua>();

                response.PagosAgua = agualist.Any() ? agualist : new List<pagosagua>();

                /*response.PagosAgua.Add(

                  new pagosagua
                  {
                      idPagoAgua = 1,
                      CuentaAgua = "1010101",
                      ConsumoBimestral = 100,

                      FechaLectura1 = DateTime.Now,
                      Lectura1 = 200,

                      FechaLectura2 = DateTime.Now,
                      Lectura2 = 100,

                      ImporteHabitacional = 10,
                      ImporteComercial = 10,
                      IvaComercial = 10,
                      Recargos = 10,
                      Actualizacion = 10,
                      Multas = 10,
                      GastosEjecucion = 10,

                      ConceptoPago = "Agua",
                      FechaVencimiento = DateTime.Now,

                      UsuarioAutoriza = 1,
                      UsuarioAutorizaDescripcion = "Msilver",
                      StatusProceso = 1,
                      StatusProcesoDescripcion = "Registrado",

                      InmuebleData = new B_inmuebles
                      {
                          ue = 1010101,
                          cr = "10101010",
                          nombre = "Sucursal"
                      },

                      ConsumoAnterior = new pagosagua
                      {
                          idPagoAgua = 1,
                          CuentaAgua = "",
                          ConsumoBimestral = 50,

                          FechaLectura1 = DateTime.Now,
                          Lectura1 = 50,

                          FechaLectura2 = DateTime.Now,
                          Lectura2 = 100,
                      },
                  }
                );*/
                // hacer un metodo que convierta lo que traiga el dataTable 

                response.PagosAgua.ForEach(x =>
                {
                    //x.idCuentaAgua
                    DataTable dataTable = conexion.RunStoredProcedure("Cuentas_pagos_agua_Get", listSqlParameters);

                    List<pagosagua> agualist = (from item in dataTable.Rows.Cast<DataRow>()
                                                select new pagosagua
                                                {
                                                    idPagoAgua = item.Field<int>("idDtPagosAgua"),
                                                    idCuentaAgua = item.Field<int>("IdCuentaAgua"),
                                                    ConsumoBimestral = item.Field<double>("ConsumoBimestral"),
                                                    FechaLectura1 = item.Field<DateTime>("fechaLectura1"),
                                                    Lectura1 = item.Field<float>("Lectura1"),
                                                    FechaLectura2 = item.Field<DateTime>("fechaLectura2"),
                                                    Lectura2 = item.Field<float>("Lectura2"),
                                                    ImporteHabitacional = item.Field<double>("importeHabitacional"),
                                                    ImporteComercial = item.Field<double>("importeComercial"),
                                                    IvaComercial = item.Field<double>("ivaComercial"),
                                                    Recargos = item.Field<double>("Recargos"),
                                                    Actualizacion = item.Field<double>("Actualizacion"),
                                                    Multas = item.Field<double>("Multas"),
                                                    GastosEjecucion = item.Field<double>("GastosEjecucion"),
                                                    FechaVencimiento = item.Field<DateTime>("FechaVencimiento"),
                                                    StatusProceso = item.Field<int>("statusProceso"),
                                                    status = item.Field<string>("STATUS"),
                                                    ue = item.Field<int>("ue"),
                                                    cr = item.Field<string>("cr"),
                                                    nombre = item.Field<string>("nombre"),
                                                    FechaAltaRegistro = item.Field<DateTime>("FechaAltaRegistro")
                                                }).OrderByDescending(x=>x.FechaAltaRegistro).ToList();


                    x.ImporteTotal = (x.ImporteHabitacional + x.ImporteComercial + x.IvaComercial + x.Recargos + x.Actualizacion + x.Multas + x.GastosEjecucion);
                    x.ConsumoAnterior = agualist.ElementAt(1);
                    x.ConsumoAnterior.ImporteTotal = (x.ConsumoAnterior.ImporteHabitacional + x.ConsumoAnterior.ImporteComercial + x.ConsumoAnterior.IvaComercial + x.ConsumoAnterior.Recargos + x.ConsumoAnterior.Actualizacion + x.ConsumoAnterior.Multas + x.ConsumoAnterior.GastosEjecucion);
                });
            }
            else if (IdTipoServicio == 2)/*luz*/
            {

                var listSqlParameters = new List<MySqlParameter>
                {
                    new MySqlParameter("IdInmueble", IdInmueble ?? (object)DBNull.Value),
                    new MySqlParameter("IdLocalidad", IdLocalidad ?? (object)DBNull.Value),
                    new MySqlParameter("IdCuenta", IdCuenta ?? (object)DBNull.Value),
                    new MySqlParameter("IdTipoServicio", IdTipoServicio ?? (object)DBNull.Value),
                    new MySqlParameter("Estatus", Estatus ?? (object)DBNull.Value),
                    /*new MySqlParameter("IdPagoServicio", IdPagoServicio ?? (object)DBNull.Value),
                    new MySqlParameter("IdCuentaServicio", IdCuentaServicio ?? (object)DBNull.Value)*/
                };
                DataTable dataTable = conexion.RunStoredProcedure("Cuentas_pagos_luz_Get", listSqlParameters);

                List<pagosluz> agualist = (from item in dataTable.Rows.Cast<DataRow>()
                                            select new pagosluz
                                            { 

                                            }).ToList();



                /* response.PagosLuz = new List<pagosluz>
                    {
                        new pagosluz
                        {
                                idDtPagosLuz=1,
                                 idPagoLuz=1,
                                CuentaLuz="1010101",
                                fechaPago=DateTime.Now,
                                periodoPago="Julio-Agosto",

                                importe=2000,
                                iva=120,
                                conceptoPago="Luz" ,
                                UsuarioAutorizaDescripcion="Msilver",
                                StatusProcesoDescripcion="Registrado",

                                InmuebleData=new B_inmuebles
                                 {
                                     ue=1010101,
                                     cr="10101010",
                                     nombre="Sucursal"
                                 },

                                ConsumoAnterior=new pagosluz
                                {
                                     idDtPagosLuz=1,
                                     CuentaLuz="1010101",
                                     fechaPago=DateTime.Now,
                                     periodoPago="Julio-Agosto",   
                                     importe=1000,
                                     iva=65,
                                     conceptoPago="Luz" ,
                                     UsuarioAutorizaDescripcion="Msilver",
                                     StatusProcesoDescripcion="Registrado",
                                }
                        }
                 };*/
                response.PagosLuz.ForEach(x => {
                    x.ImporteTotal = (x.importe + x.iva);
                    x.ConsumoAnterior.ImporteTotal = (x.ConsumoAnterior.importe + x.ConsumoAnterior.iva);
                });
            }
            else if (IdTipoServicio == 3)/*predial*/
            {
                response.PagosPredial = new List<pagospredial>
                {
                    new pagospredial
                    {
                        idPagoPredial=1,
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
                        StatusProcesoDescripcion="Registrado",
                          
                        InmuebleData=new B_inmuebles
                        {
                            ue=1010101,
                            cr="10101010",
                            nombre="Sucursal"
                        },
                        ConsumoAnterior=new pagospredial
                        {
                            idDtPagosPredial=1,
                            CuentaPredial="110011",
                            periodoPago="Julio-Agosto",
                            Recargos = 10,
                            Multas = 10,
                            importe = 10,
                            iva = 10,
                            Actualizacion = 10,
                        }
                    },
                };

                response.PagosPredial.ForEach(x => {
                    x.ImporteTotal = (x.Recargos + x.Multas + x.importe + x.iva + x.Actualizacion);
                    x.ConsumoAnterior.ImporteTotal = (x.ConsumoAnterior.Recargos + x.ConsumoAnterior.Multas + x.ConsumoAnterior.importe + x.iva + x.ConsumoAnterior.Actualizacion);
                });
            }
            return response;
        }
        
        /// <summary>
        /// Registra en bd la información de pago de una cuent de agua
        /// </summary>
        /// <param name="pagosagua"></param>
        /// <returns></returns>
        public bool  InsertaPagosAgua(pagosagua pagosagua)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCuentaAgua", pagosagua.idCuentaAgua));
            listSqlParameters.Add(new MySqlParameter("FechaLectura1", pagosagua.FechaLectura1));
            listSqlParameters.Add(new MySqlParameter("Lectura1", pagosagua.Lectura1));
            listSqlParameters.Add(new MySqlParameter("FechaLectura2", pagosagua.FechaLectura2));
            listSqlParameters.Add(new MySqlParameter("Lectura2", pagosagua.Lectura2));
            listSqlParameters.Add(new MySqlParameter("ImporteHabitacional", pagosagua.ImporteHabitacional));
            listSqlParameters.Add(new MySqlParameter("ImporteComercial", pagosagua.ImporteComercial));
            listSqlParameters.Add(new MySqlParameter("IvaComercial", pagosagua.IvaComercial));
            listSqlParameters.Add(new MySqlParameter("Recargos", pagosagua.Recargos));
            listSqlParameters.Add(new MySqlParameter("Actualizacion", pagosagua.Actualizacion));
            listSqlParameters.Add(new MySqlParameter("Multas", pagosagua.Multas));
            listSqlParameters.Add(new MySqlParameter("GastosEjecucion", pagosagua.GastosEjecucion));
            listSqlParameters.Add(new MySqlParameter("ConceptoPago", pagosagua.ConceptoPago));
            listSqlParameters.Add(new MySqlParameter("FechaVencimiento", pagosagua.FechaVencimiento));
            listSqlParameters.Add(new MySqlParameter("LineaCaptura", pagosagua.LineaCaptura));
            listSqlParameters.Add(new MySqlParameter("ConsumoBimestral", pagosagua.ConsumoBimestral));
            listSqlParameters.Add(new MySqlParameter("FechaAltaRegistro", pagosagua.FechaAltaRegistro));
            listSqlParameters.Add(new MySqlParameter("IdUsuario", pagosagua.UsuarioAutoriza));
            listSqlParameters.Add(new MySqlParameter("Estatus", pagosagua.StatusProceso));
            listSqlParameters.Add(new MySqlParameter("PeriodoPago", pagosagua.periodoPago));
            conexion.RunStoredProcedure("InsertaPagosAgua", listSqlParameters);
            return true;
        }

        /// <summary>
        /// Registra en bd la información de pago de una cuent de luz
        /// </summary>
        /// <param name="pagosluz"></param>
        /// <returns></returns>
        public bool InsertaPagosLuz(pagosluz pagosluz)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCuentaLuz", pagosluz.idCgCuentaLuz));
            listSqlParameters.Add(new MySqlParameter("FechaPago", pagosluz.fechaPago));
            listSqlParameters.Add(new MySqlParameter("PeriodoPago", pagosluz.periodoPago));
            listSqlParameters.Add(new MySqlParameter("ImporteLuz", pagosluz.importe));
            listSqlParameters.Add(new MySqlParameter("IvaLuz", pagosluz.iva));
            listSqlParameters.Add(new MySqlParameter("ConceptoPago", pagosluz.conceptoPago));
            listSqlParameters.Add(new MySqlParameter("LineaCaptura", pagosluz.LineaCaptura));
            listSqlParameters.Add(new MySqlParameter("IdUsuario", pagosluz.UsuarioAutoriza));
            listSqlParameters.Add(new MySqlParameter("Estatus", pagosluz.StatusProceso));
            listSqlParameters.Add(new MySqlParameter("FechaAltaRegistro", pagosluz.FechaAltaRegistro));
            listSqlParameters.Add(new MySqlParameter("fechaLimitePago", pagosluz.FechaLimitePago));
            listSqlParameters.Add(new MySqlParameter("fechaCorte", pagosluz.FechaCorte));
            listSqlParameters.Add(new MySqlParameter("lecturaActual", pagosluz.LecturaActual));
            listSqlParameters.Add(new MySqlParameter("lecturaAnterior", pagosluz.LecturaAnterior));
            conexion.RunStoredProcedure("InsertaPagosLuz", listSqlParameters);
            return true;
        }

        /// <summary>
        /// Registra en bd la información de pago de una cuent de predial
        /// </summary>
        /// <param name="pagospredial"></param>
        /// <returns></returns>
        public bool InsertaPagosPredial(pagospredial pagospredial)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCuentaPredial", pagospredial.idCgCuentaPredial));
            listSqlParameters.Add(new MySqlParameter("PeriodoPago", pagospredial.periodoPago));
            listSqlParameters.Add(new MySqlParameter("ImportePredial", pagospredial.importe));
            listSqlParameters.Add(new MySqlParameter("Recargos", pagospredial.Recargos));
            listSqlParameters.Add(new MySqlParameter("Multas", pagospredial.Multas));
            listSqlParameters.Add(new MySqlParameter("Actualizacion", pagospredial.Actualizacion));
            listSqlParameters.Add(new MySqlParameter("ConceptoPago", pagospredial.conceptoPago));
            listSqlParameters.Add(new MySqlParameter("LineaCaptura", pagospredial.LineaCaptura));
            listSqlParameters.Add(new MySqlParameter("FechaPagoLimite", pagospredial.fechaPagolimite));
            listSqlParameters.Add(new MySqlParameter("FechaAltaRegistro", pagospredial.FechaAltaRegistro));
            listSqlParameters.Add(new MySqlParameter("IdUsuario", pagospredial.UsuarioAutoriza));
            listSqlParameters.Add(new MySqlParameter("Estatus", pagospredial.StatusProceso));
            listSqlParameters.Add(new MySqlParameter("Diferencia", pagospredial.Diferencia));
            listSqlParameters.Add(new MySqlParameter("Honorarios", pagospredial.Honorarios));
            listSqlParameters.Add(new MySqlParameter("Notificacion", pagospredial.Notificacion));
            listSqlParameters.Add(new MySqlParameter("GastoEjecucion", pagospredial.GastoEjecucion));
            listSqlParameters.Add(new MySqlParameter("Descuento", pagospredial.Descuento));
            conexion.RunStoredProcedure("InsertaPagosPredial", listSqlParameters);
            return true;
        }

        public bool ActualizaPagosAgua(pagosagua pagosagua)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

                //identificador para saber que registro se va actualizar
                listSqlParameters.Add(new MySqlParameter("IdPagoAgua", pagosagua.idPagoAgua));
               
                //Campos que se actualizan
                listSqlParameters.Add(new MySqlParameter("IdCuentaAgua", pagosagua.idCuentaAgua));
                listSqlParameters.Add(new MySqlParameter("FechaLectura1", pagosagua.FechaLectura1));
                listSqlParameters.Add(new MySqlParameter("Lectura1", pagosagua.Lectura1));
                listSqlParameters.Add(new MySqlParameter("FechaLectura2", pagosagua.FechaLectura2));
                listSqlParameters.Add(new MySqlParameter("Lectura2", pagosagua.Lectura2));
                listSqlParameters.Add(new MySqlParameter("ImporteHabitacional", pagosagua.ImporteHabitacional));
                listSqlParameters.Add(new MySqlParameter("ImporteComercial", pagosagua.ImporteComercial));
                listSqlParameters.Add(new MySqlParameter("IvaComercial", pagosagua.IvaComercial));
                listSqlParameters.Add(new MySqlParameter("Recargos", pagosagua.Recargos));
                listSqlParameters.Add(new MySqlParameter("Actualizacion", pagosagua.Actualizacion));
                listSqlParameters.Add(new MySqlParameter("Multas", pagosagua.Multas));
                listSqlParameters.Add(new MySqlParameter("GastosEjecucion", pagosagua.GastosEjecucion));
                listSqlParameters.Add(new MySqlParameter("ConceptoPago", pagosagua.ConceptoPago));
                listSqlParameters.Add(new MySqlParameter("FechaVencimiento", pagosagua.FechaVencimiento));
                listSqlParameters.Add(new MySqlParameter("LineaCaptura", pagosagua.LineaCaptura));
                listSqlParameters.Add(new MySqlParameter("ConsumoBimestral", pagosagua.ConsumoBimestral));

                conexion.RunStoredProcedure("ActualizaPagosAgua", listSqlParameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar: " + ex.Message);
                return false;
            }   
        }

        public bool ActualizaPagosLuz(pagosluz pagosluz)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

                //identificador para saber que registro se va actualizar
                listSqlParameters.Add(new MySqlParameter("IdPagoLuz", pagosluz.idPagoLuz));
                //campos a actualizar
                listSqlParameters.Add(new MySqlParameter("IdCuentaLuz", pagosluz.idCuentaLuz));
                listSqlParameters.Add(new MySqlParameter("FechaPago", pagosluz.fechaPago));
                listSqlParameters.Add(new MySqlParameter("PeriodoPago", pagosluz.periodoPago));
                listSqlParameters.Add(new MySqlParameter("ImporteLuz", pagosluz.importe));
                listSqlParameters.Add(new MySqlParameter("IvaLuz", pagosluz.iva));
                listSqlParameters.Add(new MySqlParameter("ConceptoPago", pagosluz.conceptoPago));
                listSqlParameters.Add(new MySqlParameter("FechaPago", pagosluz.fechaPago));
                listSqlParameters.Add(new MySqlParameter("LineaCaptura", pagosluz.LineaCaptura));

                conexion.RunStoredProcedure("ActualizaPagosLuz", listSqlParameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar: " + ex.Message);
                return false;
            }
        }

        public bool ActualizaPagosPredial(pagospredial pagospredial)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

                //identificador para saber que registro se va actualizar
                listSqlParameters.Add(new MySqlParameter("IdPagoPredial", pagospredial.idDtPagosPredial));

                //Campos a actualizar
                listSqlParameters.Add(new MySqlParameter("IdCuentaPredial", pagospredial.idCgCuentaPredial));
                listSqlParameters.Add(new MySqlParameter("PeriodoPago", pagospredial.periodoPago));
                listSqlParameters.Add(new MySqlParameter("ImportePredial", pagospredial.importe));
                listSqlParameters.Add(new MySqlParameter("IvaPredial", pagospredial.iva));
                listSqlParameters.Add(new MySqlParameter("Recargos", pagospredial.Recargos));
                listSqlParameters.Add(new MySqlParameter("Multas", pagospredial.Multas));
                listSqlParameters.Add(new MySqlParameter("Actualizacion", pagospredial.Actualizacion));
                listSqlParameters.Add(new MySqlParameter("ConceptoPago", pagospredial.conceptoPago));
                listSqlParameters.Add(new MySqlParameter("LineaCaptura", pagospredial.LineaCaptura));
                listSqlParameters.Add(new MySqlParameter("FechaPagoLimite", pagospredial.fechaPagolimite));

                conexion.RunStoredProcedure("ActualizaPagosPredial", listSqlParameters);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al actualizar: " + ex.Message);
                return false;
            }
        }

    }
                      
}
