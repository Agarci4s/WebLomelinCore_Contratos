using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MySql.Data.MySqlClient;
using Stimulsoft.Base.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public List<SelectListItem> getBimestre(int? IdPeriodicidad)
        {
            var lista = new List<SelectListItem>();

            if (!IdPeriodicidad.HasValue)
            {
                lista.Add(new SelectListItem { Value = "0", Text = "Seleccione una opción" });
                return lista;
            }

            switch (IdPeriodicidad.Value)
            {

                case 1: // Anual
                    lista.Add(new SelectListItem { Value = "2023", Text = "2023" });
                    lista.Add(new SelectListItem { Value = "2024", Text = "2024" });
                    lista.Add(new SelectListItem { Value = "2025", Text = "2025" });
                    break;

                case 2: // Bimestral
                    lista.Add(new SelectListItem { Value = "1", Text = "Enero - Febrero" });
                    lista.Add(new SelectListItem { Value = "2", Text = "Marzo - Abril" });
                    lista.Add(new SelectListItem { Value = "3", Text = "Mayo - Junio" });
                    lista.Add(new SelectListItem { Value = "4", Text = "Julio - Agosto" });
                    lista.Add(new SelectListItem { Value = "5", Text = "Septiembre - Octubre" });
                    lista.Add(new SelectListItem { Value = "6", Text = "Noviembre - Diciembre" });
                    break;

                case 3: //Mensual
                    lista.Add(new SelectListItem { Value = "1", Text = "Enero" });
                    lista.Add(new SelectListItem { Value = "2", Text = "Febrero" });
                    lista.Add(new SelectListItem { Value = "3", Text = "Marzo" });
                    lista.Add(new SelectListItem { Value = "4", Text = "Abril" });
                    lista.Add(new SelectListItem { Value = "5", Text = "Mayo" });
                    lista.Add(new SelectListItem { Value = "6", Text = "Junio" });
                    lista.Add(new SelectListItem { Value = "7", Text = "Julio" });
                    lista.Add(new SelectListItem { Value = "8", Text = "Agosto" });
                    lista.Add(new SelectListItem { Value = "9", Text = "Septiembre" });
                    lista.Add(new SelectListItem { Value = "10", Text = "Octubre" });
                    lista.Add(new SelectListItem { Value = "11", Text = "Noviembre" });
                    lista.Add(new SelectListItem { Value = "12", Text = "Diciembre" });
                    break;

                case 5: // Semestral 
                    lista.Add(new SelectListItem { Value = "1", Text = "Enero - Junio" });
                    lista.Add(new SelectListItem { Value = "2", Text = "Julio - Diciembre" });
                    break;

                default:
                    lista.Add(new SelectListItem { Value = "0", Text = "Seleccione una opción" });
                    break;
            }

            lista.Insert(0, new SelectListItem { Value = "0", Text = "Seleccione una opción" });
            return lista;

        }

        public List<SelectListItem> getPeriodosDiponibles
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

        public List<SelectListItem> getPeriodicidad(int? IdServicio)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>
                {
                  new MySqlParameter("@Id_TipoServicio", IdServicio)
                };

            DataTable dataTable = conexion.RunStoredProcedure("sp_GetPeriodicidadServicio", listSqlParameters);

            List<SelectListItem> response = (from item in dataTable.Rows.Cast<DataRow>()
                                             select new SelectListItem
                                             {
                                                 Value = item.Field<int>("IdPeriodicidad").ToString(),
                                                 Text = item.Field<string>("Periodicidad"),
                                             }).ToList();

            response.Insert(0, new SelectListItem
            {
                Text = "Seleccione una opción",
                Value = "0"
            });

            return response;

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
            conexion = new Conexion();
            List<SelectListItem> response = new List<SelectListItem>();

            if (IdInmueble.HasValue && IdLocalidad.HasValue && IdTipoServicio.HasValue)
            {
                listSqlParameters.Add(new MySqlParameter("IdInmueble_in", IdInmueble));
                listSqlParameters.Add(new MySqlParameter("IdLocalidad_in", IdLocalidad));
                if (IdTipoServicio == 1) //agua
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdInmueble">Busca por IdInmueble</param>
        /// <param name="IdLocalidad">IdLocalidad</param>
        /// <param name="Estatus">pro el estatus</param>
        /// <param name="IdPagoServicio">dtpagosagua</param>
        /// <param name="IdCuentaServicio">b_cg_agua</param>
        /// <returns></returns>
        private List<pagosagua> GetPagosAgua(int? IdInmueble, int? IdLocalidad, int? Estatus, int? IdPagoServicio, int? IdCuentaServicio)
        {
            var listSqlParameters = new List<MySqlParameter>
                {
                    new MySqlParameter("idinmueble_IN", IdInmueble ?? (object)DBNull.Value),
                    new MySqlParameter("idlocalidad_IN", IdLocalidad ?? (object)DBNull.Value),
                    new MySqlParameter("idpagosagua_IN", IdPagoServicio ?? (object)DBNull.Value),
                    new MySqlParameter("idcuentaagua_IN", IdCuentaServicio ?? (object)DBNull.Value),
                    new MySqlParameter("statusProceso_IN", Estatus ?? (object)DBNull.Value)                    
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
                                            InmuebleData = new B_inmuebles
                                            {
                                                ue = item.Field<int>("ue"),
                                                cr = item.Field<string>("cr"),
                                                nombre = item.Field<string>("nombre")
                                            },
                                            FechaAltaRegistro = item.Field<DateTime>("FechaAltaRegistro")
                                        }).ToList();
            return agualist;

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdInmueble"></param>
        /// <param name="IdLocalidad"></param>
        /// <param name="Estatus"></param>
        /// <param name="IdPagoServicio">dtpagosluz</param>
        /// <param name="IdCuentaServicio">b_cg_luz</param>
        /// <returns></returns>
        private List<pagosluz> GetPagosLuz(int? IdInmueble, int? IdLocalidad, int? Estatus, int? IdPagoServicio, int? IdCuentaServicio)
        {
            var listSqlParameters = new List<MySqlParameter>
                {
                new MySqlParameter("idpagosLuz_in", IdPagoServicio ?? (object)DBNull.Value),
                new MySqlParameter("idcuentaLuz_in", IdCuentaServicio ?? (object)DBNull.Value) ,
                new MySqlParameter("idinmueble_in", IdInmueble ?? (object)DBNull.Value),
                new MySqlParameter("idlocalidad_in", IdLocalidad ?? (object)DBNull.Value),
                new MySqlParameter("statusProceso_in", Estatus ?? (object)DBNull.Value),
                };
            DataTable dataTable = conexion.RunStoredProcedure("Cuentas_pagos_luz_Get", listSqlParameters);

            List<pagosluz> luzList = (from item in dataTable.Rows.Cast<DataRow>()
                                      select new pagosluz
                                      {
                                          idPagoLuz = item.Field<int>("idDtPagosLuz"),
                                          idCuentaLuz = item.Field<int>("idCgCuentaLuz"),
                                          fechaPago = item.Field<DateTime>("fechaPago"),
                                          periodoPago = item.Field<string>("periodoPago"),
                                          importe = item.Field<double>("importe"),
                                          iva = item.Field<double>("iva"),
                                          StatusProceso = item.Field<int>("statusProceso"),
                                          StatusProcesoDescripcion = item.Field<string>("STATUS"),
                                          InmuebleData = new B_inmuebles
                                          {
                                              ue = item.Field<int>("ue"),
                                              cr = item.Field<string>("cr"),
                                              nombre = item.Field<string>("nombre")
                                          },
                                          FechaAltaRegistro = item.Field<DateTime>("FechaAltaRegistro"),
                                          FechaUpdateRegistro = item.Field<DateTime>("FechaUpdateRegistro"),
                                          FechaLimitePago = item.Field<DateTime>("FechaLimitePago")
                                      }).OrderByDescending(x => x.FechaAltaRegistro).ToList();

            return luzList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdInmueble"></param>
        /// <param name="IdLocalidad"></param>
        /// <param name="Estatus"></param>
        /// <param name="IdPagoServicio">dtpagospredial</param>
        /// <param name="IdCuentaServicio">b_cg_predial</param>
        /// <returns></returns>
        private List<pagospredial> GetPagosPredial(int? IdInmueble, int? IdLocalidad, int? Estatus, int? IdPagoServicio, int? IdCuentaServicio)
        {
            var listSqlParameters = new List<MySqlParameter>
                {
                new MySqlParameter("idpagospredial_in", IdPagoServicio ?? (object)DBNull.Value),
                      new MySqlParameter("idcuentapredial_in", IdCuentaServicio ?? (object)DBNull.Value),
                    new MySqlParameter("idinmueble_in", IdInmueble ?? (object)DBNull.Value),
                    new MySqlParameter("idlocalidad_in", IdLocalidad ?? (object)DBNull.Value),
                           new MySqlParameter("statusProceso_in", Estatus ?? (object)DBNull.Value),
                 };

            DataTable dataTable = conexion.RunStoredProcedure("Cuentas_pagos_predial_Get", listSqlParameters);

            List<pagospredial> predialList = (from item in dataTable.Rows.Cast<DataRow>()
                                              select new pagospredial
                                              {
                                                  idPagoPredial = item.Field<int>("idDtPagosPredial"),
                                                  idCuentaPredial = item.Field<int>("idCgCuentaPredial"),
                                                  periodoPago = item.Field<string>("periodoPago"),
                                                  importe = item.Field<double>("importe"),
                                                  Recargos = item.Field<double>("Recargos"),
                                                  Multas = item.Field<double>("Multas"),
                                                  Actualizacion = item.Field<double>("Actualizacion"),
                                                  Diferencia = item.Field<double>("Diferencia"),
                                                  Honorarios = item.Field<double>("Honorarios"),
                                                  Notificacion = item.Field<double>("Notificacion"),
                                                  GastoEjecucion = item.Field<double>("GastoEjecucion"),
                                                  Descuento = item.Field<double>("Descuento"),
                                                  fechaPagolimite = item.Field<DateTime>("fechaPagolimite"),
                                                  StatusProceso = item.Field<int>("estatusproceso"),
                                                  StatusProcesoDescripcion = item.Field<string>("STATUS"),
                                                  InmuebleData = new B_inmuebles
                                                  {
                                                      ue = item.Field<int>("ue"),
                                                      cr = item.Field<string>("cr"),
                                                      nombre = item.Field<string>("nombre")
                                                  },
                                                  FechaAltaRegistro = item.Field<DateTime>("FechaAltaRegistro"),
                                                  FechaUpdateRegistro = item.Field<DateTime>("FechaUpdateRegistro")
                                              }).ToList();
            return predialList;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdInmueble"></param>
        /// <param name="IdLocalidad"></param>
        /// <param name="IdTipoServicio"></param>
        /// <param name="Estatus"></param>
        /// <param name="IdPagoServicio">Id de la tabla donde se gudaran los pagos por servicio</param>
        /// <param name="IdCuentaServicio">Id de la tabla donde se guardan las cuentas por servicio</param>
        /// <returns></returns>
        public PagoUnificadoDTO getPagoServiciosList(
            int? IdInmueble,
            int? IdLocalidad,
            int? IdTipoServicio,
            int? Estatus,
            int? IdPagoServicio,
            int? IdCuentaServicio)
        //agregar idpagoservicio, idcuentaservicio = int?
        {
            PagoUnificadoDTO response = new PagoUnificadoDTO();
            decimal? porcentajeServicio = ObtienePorcentajePago(IdTipoServicio.Value);

            if (IdTipoServicio == 1)/*agua*/
            {
                response.PagosAgua = GetPagosAgua(IdInmueble, IdLocalidad, Estatus, IdPagoServicio, IdCuentaServicio);

                response.PagosAgua?.ForEach(x =>
                {
                    x.ImporteTotal = (x.ImporteHabitacional + x.ImporteComercial + x.IvaComercial + x.Recargos + x.Actualizacion + x.Multas + x.GastosEjecucion);
                    x.PorcentajeConsumo = ObtienePorcentajePago(porcentajeServicio, x.ImporteTotal);

                    List<pagosagua> agualist = GetPagosAgua(null, null, null, null, x.idCuentaAgua);

                    if (agualist.Any())
                    {                        
                        x.ConsumoAnterior = agualist.ElementAt(1);
                        x.ConsumoAnterior.ImporteTotal = (x.ConsumoAnterior.ImporteHabitacional + x.ConsumoAnterior.ImporteComercial + x.ConsumoAnterior.IvaComercial + x.ConsumoAnterior.Recargos + x.ConsumoAnterior.Actualizacion + x.ConsumoAnterior.Multas + x.ConsumoAnterior.GastosEjecucion);
                        x.ConsumoAnterior.PorcentajeConsumo = ObtienePorcentajePago(porcentajeServicio, x.ConsumoAnterior.ImporteTotal);
                    }
                    else
                    {
                        x.ConsumoAnterior = new pagosagua()
                        {
                            ImporteTotal = 0
                        };
                    }
                    x.ClassPorcentaje = DefineEstiloSemaforo(x.ConsumoAnterior.PorcentajeConsumo, x.PorcentajeConsumo);
                });
            }
            else if (IdTipoServicio == 2)/*luz*/
            {
                response.PagosLuz = GetPagosLuz(IdInmueble, IdLocalidad, Estatus, IdPagoServicio, IdCuentaServicio);

                response.PagosLuz.ForEach(x =>
                {
                    x.ImporteTotal = (x.importe + x.iva);
                    x.PorcentajeConsumo = ObtienePorcentajePago(porcentajeServicio, x.ImporteTotal);

                    List<pagosluz> luzList = GetPagosLuz(null, null, null, null, x.idCuentaLuz);
                    
                    if (luzList.Any())
                    {
                        x.ConsumoAnterior = luzList.ElementAt(1);
                        x.ConsumoAnterior.ImporteTotal = (x.ConsumoAnterior.importe + x.ConsumoAnterior.iva);
                        x.ConsumoAnterior.PorcentajeConsumo = ObtienePorcentajePago(porcentajeServicio, x.ConsumoAnterior.ImporteTotal);
                    }
                    else
                    {
                        x.ConsumoAnterior = new pagosluz()
                        {
                            ImporteTotal = 0
                        };
                    }
                    x.ClassPorcentaje = DefineEstiloSemaforo(x.ConsumoAnterior.PorcentajeConsumo, x.PorcentajeConsumo);
                });

            }
            else if (IdTipoServicio == 3)/*predial*/
            {

                response.PagosPredial = GetPagosPredial(IdInmueble, IdLocalidad, Estatus, IdPagoServicio, IdCuentaServicio);

                response.PagosPredial.ForEach(x =>
                {
                    x.ImporteTotal = (x.importe + x.Recargos + x.Multas + x.Actualizacion + x.Diferencia + x.Honorarios + x.Notificacion + x.GastoEjecucion - x.Descuento);
                    x.PorcentajeConsumo = ObtienePorcentajePago(porcentajeServicio, x.ImporteTotal);

                    List<pagospredial> predialList = GetPagosPredial(null, null, null, null, x.idCuentaPredial);

                    if (predialList.Any())
                    {
                        x.ConsumoAnterior = predialList.ElementAt(1);
                        x.ConsumoAnterior.ImporteTotal = (
                       +x.ConsumoAnterior.importe
                       + x.ConsumoAnterior.Recargos
                       + x.ConsumoAnterior.Multas
                       + x.ConsumoAnterior.Actualizacion
                       + x.ConsumoAnterior.Diferencia
                       + x.ConsumoAnterior.Honorarios
                       + x.ConsumoAnterior.Notificacion
                       + x.ConsumoAnterior.GastoEjecucion
                       - x.ConsumoAnterior.Descuento);
                        x.ConsumoAnterior.PorcentajeConsumo = ObtienePorcentajePago(porcentajeServicio, x.ConsumoAnterior.ImporteTotal);
                    }
                    else
                    {
                        x.ConsumoAnterior = new pagospredial()
                        {
                            ImporteTotal = 0
                        };
                    }
                    x.ClassPorcentaje = DefineEstiloSemaforo(x.ConsumoAnterior.PorcentajeConsumo, x.PorcentajeConsumo);
                });
            }
            return response;
        }

        /// <summary>
        /// Registra en bd la información de pago de una cuent de agua
        /// </summary>
        /// <param name="pagosagua"></param>
        /// <returns></returns>
        public bool InsertaPagosAgua(pagosagua pagosagua)
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
            return conexion.ExecuteNonQrySP("InsertaPagosAgua", listSqlParameters) == 1 ? true : false;

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
            listSqlParameters.Add(new MySqlParameter("IdUsuario", pagosluz.UsuarioAutoriza));
            listSqlParameters.Add(new MySqlParameter("LineaCaptura", pagosluz.LineaCaptura));
            listSqlParameters.Add(new MySqlParameter("Estatus", pagosluz.StatusProceso));
            listSqlParameters.Add(new MySqlParameter("FechaAltaRegistro", pagosluz.FechaAltaRegistro));
            listSqlParameters.Add(new MySqlParameter("fechaLimitePago", pagosluz.FechaLimitePago));
            listSqlParameters.Add(new MySqlParameter("fechaCorte", pagosluz.FechaCorte));
            listSqlParameters.Add(new MySqlParameter("lecturaActual", pagosluz.LecturaActual));
            listSqlParameters.Add(new MySqlParameter("lecturaAnterior", pagosluz.LecturaAnterior));
            return conexion.ExecuteNonQrySP("InsertaPagosLuz", listSqlParameters) == 1 ? true : false;

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
            return conexion.ExecuteNonQrySP("InsertaPagosPredial", listSqlParameters) == 1 ? true : false;

        }

        public bool ActualizaPagosAgua(pagosagua pagosagua, int IdStatusAnterior)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

                //identificador para saber que registro se va actualizar
                listSqlParameters.Add(new MySqlParameter("IdPagoAgua", pagosagua.idPagoAgua));

                //Campos que se actualizan
                listSqlParameters.Add(new MySqlParameter("IdStatusAnterior", IdStatusAnterior));
                listSqlParameters.Add(new MySqlParameter("IdStatusNuevo", pagosagua.StatusProceso));
                listSqlParameters.Add(new MySqlParameter("Fecha", DateTime.Now));
                listSqlParameters.Add(new MySqlParameter("IdUsuario", pagosagua.UsuarioAutoriza));
                return conexion.ExecuteNonQrySP("ActualizaPagosAgua", listSqlParameters) == 1 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar: " + ex.Message);
                return false;
            }
        }

        public bool ActualizaPagosLuz(pagosluz pagosluz, int IdStatusAnterior)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

                //identificador para saber que registro se va actualizar
                listSqlParameters.Add(new MySqlParameter("IdPagoLuz", pagosluz.idPagoLuz));
                //campos a actualizar
                listSqlParameters.Add(new MySqlParameter("IdStatusAnterior", IdStatusAnterior));
                listSqlParameters.Add(new MySqlParameter("IdStatusNuevo", pagosluz.StatusProceso));
                listSqlParameters.Add(new MySqlParameter("Fecha", DateTime.Now));
                listSqlParameters.Add(new MySqlParameter("IdUsuario", pagosluz.UsuarioAutoriza));
                return conexion.ExecuteNonQrySP("ActualizaPagosLuz", listSqlParameters) == 1 ? true : false;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar: " + ex.Message);
                return false;
            }
        }

        public bool ActualizaPagosPredial(pagospredial pagospredial, int IdStatusAnterior)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

                //identificador para saber que registro se va actualizar
                listSqlParameters.Add(new MySqlParameter("IdPagoPredial", pagospredial.idDtPagosPredial));

                //Campos a actualizar
                listSqlParameters.Add(new MySqlParameter("IdStatusAnterior", IdStatusAnterior));
                listSqlParameters.Add(new MySqlParameter("IdStatusNuevo", pagospredial.StatusProceso));
                listSqlParameters.Add(new MySqlParameter("Fecha", DateTime.Now));
                listSqlParameters.Add(new MySqlParameter("IdUsuario", pagospredial.UsuarioAutoriza));
                return conexion.ExecuteNonQrySP("ActualizaPagosPredial", listSqlParameters) == 1 ? true : false;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar: " + ex.Message);
                return false;
            }
        }

        public decimal? ObtienePorcentajePago(int idServicio)
        {
            decimal? response = null;
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>
            {
                new("p_IdTipoServicio", idServicio)
            };

            var data = conexion.RunStoredProcedure("sp_GetPorcentajeServicio", listSqlParameters);

            if (data != null & data.Rows.Count > 0)
            {
                foreach (var item in data.Rows.Cast<DataRow>())
                {
                    response = item.Field<decimal>("Porcentaje");
                }

            }
            return response;
        }
        public decimal? ObtienePorcentajePago(decimal? porcentajeServicio, double? importe)
        {
            decimal? response = null;
            if (porcentajeServicio.HasValue && importe.HasValue)
            {
                try
                {
                    decimal import = 0;
                    decimal.TryParse(importe.ToString(), out import);
                    response = (import * porcentajeServicio.Value) / 100;
                }
                catch (Exception ex)
                {

                    throw;
                }                
            }
            return response;
        }
        public string DefineEstiloSemaforo(decimal? porcentajeAnterior, decimal? porcentajeActual)
        {
            string response = "RowPositivo";
            if (porcentajeAnterior.HasValue && porcentajeActual.HasValue)
            {
                if (porcentajeAnterior>0 & porcentajeActual>0)
                {
                    if (porcentajeActual > porcentajeAnterior)
                    {
                        response = "RowNegativo";
                    }
                }
            }
            return response;
        }
    }                      
}