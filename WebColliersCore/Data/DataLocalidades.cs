using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;

namespace WebColliersCore.Data
{
    public class DataLocalidades
    {
        private Conexion conexion = new Conexion();

        public List<SelectListItem> LocalidadesOcupadasByInquilino(int idinquilino, int idinmueble)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("IdInquilinoIn", MySqlDbType.Int32) { Value = idinquilino });
                mySqlParameters.Add(new MySqlParameter("IdInmuebleIn", MySqlDbType.Int32) { Value = idinmueble });
                DataTable dataTable = conexion.RunStoredProcedure("localidadesOcupadasByInquilino", mySqlParameters);
                List<SelectListItem> lstLocalidades = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstLocalidades.Add(new SelectListItem { Text = item["Localidad"].ToString(), Value = item["IdLocalidad"].ToString() });
                }

                return lstLocalidades;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<SelectListItem> InmueblesGet(int IdUsuario, int IdCartera)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("opcionIn", MySqlDbType.Int32) { Value = 0 });
                mySqlParameters.Add(new MySqlParameter("IdUsuarioIn", MySqlDbType.Int32) { Value = IdUsuario });
                mySqlParameters.Add(new MySqlParameter("IdCarteraIn", MySqlDbType.Int32) { Value = IdCartera });
                DataTable dataTable = conexion.RunStoredProcedure("inmueblesGet", mySqlParameters);
                List<SelectListItem> lstInmuebles = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstInmuebles.Add(new SelectListItem { Text = item["Inmueble"].ToString(), Value = item["IdInmueble"].ToString() });
                }

                return lstInmuebles;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public List<SelectListItem> LocalidadesGet(int IdInmueble)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>
                {
                    new("IdInmueble_In", IdInmueble)
                };
                
                DataTable dataTable = conexion.RunStoredProcedure("dtlocalidades_GetByIdInmueble", mySqlParameters);
                List<SelectListItem> lstInmuebles = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstInmuebles.Add(new SelectListItem { Text = item["Localidad"].ToString(), Value = item["Id"].ToString() });
                }

                return lstInmuebles;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public List<SelectListItem> usoLocalidadGet()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("opcionIn", MySqlDbType.Int32) { Value = 0 });
                DataTable dataTable = conexion.RunStoredProcedure("tpusoinmuebleGet", mySqlParameters);
                List<SelectListItem> lstUsoLocalidad = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstUsoLocalidad.Add(new SelectListItem { Text = item["Uso"].ToString(), Value = item["Id"].ToString() });
                }

                return lstUsoLocalidad;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> estadoLocalidadGet()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("opcionIn", MySqlDbType.Int32) { Value = 0 });
                DataTable dataTable = conexion.RunStoredProcedure("tpestadolocalidadGet", mySqlParameters);
                List<SelectListItem> lstEstadoLocalidad = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstEstadoLocalidad.Add(new SelectListItem { Text = item["Estado"].ToString(), Value = item["Id"].ToString() });
                }

                return lstEstadoLocalidad;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> arrendatariosGet(int estado)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("estadoIn", MySqlDbType.Int32) { Value = estado });
                DataTable dataTable = conexion.RunStoredProcedure("arrendatariosGetByEstado", mySqlParameters);
                List<SelectListItem> lstArrendatarios = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstArrendatarios.Add(new SelectListItem { Text = item["Nombre"].ToString(), Value = item["IdInquilino"].ToString() });
                }

                return lstArrendatarios;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> nombreComercialGet(int estado)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("arrendatarioIn", MySqlDbType.Int32) { Value = estado });
                DataTable dataTable = conexion.RunStoredProcedure("cgnombrecomercialinquilinoGetByArrendatario", mySqlParameters);
                List<SelectListItem> lstArrendatarios = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstArrendatarios.Add(new SelectListItem { Text = item["NombreComercial"].ToString(), Value = item["Id"].ToString() });
                }

                return lstArrendatarios;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> propioTerceroGet()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                MySqlParameter mySqlParameter1 = new MySqlParameter("varPro", MySqlDbType.VarChar, 50) { Value = "PoT" };
                mySqlParameters.Add(mySqlParameter1);
                DataTable dataTable = conexion.RunStoredProcedure("ConsultaVerOptC_SP", mySqlParameters);
                List<SelectListItem> lstValoresMercado = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstValoresMercado.Add(new SelectListItem { Text = item["Mostrar"].ToString(), Value = item["ValorC"].ToString() });
                }

                return lstValoresMercado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> categoriasGet()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable dataTable = conexion.RunStoredProcedure("categoriasGet", mySqlParameters);
                List<SelectListItem> lstCategorias = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstCategorias.Add(new SelectListItem { Text = item["Categoria"].ToString(), Value = item["Id"].ToString() });
                }

                return lstCategorias;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> baseRevisionGet()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable dataTable = conexion.RunStoredProcedure("tpbaserevisioncontratosGet", mySqlParameters);
                List<SelectListItem> lstCategorias = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstCategorias.Add(new SelectListItem { Text = item["Revision"].ToString(), Value = item["Id"].ToString() });
                }

                return lstCategorias;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> plazoRevisionGet()
        {
            try
            {
                List<SelectListItem> lstPlazosRevision = new List<SelectListItem>();
                lstPlazosRevision.Add(new SelectListItem { Value = "ANUAL", Text = "ANUAL", });
                lstPlazosRevision.Add(new SelectListItem { Value = "SEMESTRAL", Text = "SEMESTRAL" });
                lstPlazosRevision.Add(new SelectListItem { Value = "9 MESES", Text = "9 MESES" });
                lstPlazosRevision.Add(new SelectListItem { Value = "11 MESES", Text = "11 MESES" });

                return lstPlazosRevision;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> GastosFijosGet()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable dataTable = conexion.RunStoredProcedure("tpgastosGetAll", mySqlParameters);
                List<SelectListItem> lstGastosFijos = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstGastosFijos.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["IdGasto"].ToString() });
                }

                return lstGastosFijos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> ClaveUnidadGet()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable dataTable = conexion.RunStoredProcedure("cgclaveunidadGet", mySqlParameters);
                List<SelectListItem> lstClaveUnidad = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstClaveUnidad.Add(new SelectListItem { Text = item["Nombre"].ToString(), Value = item["Clave"].ToString() });
                }

                return lstClaveUnidad;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> ClaveProdServGet()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable dataTable = conexion.RunStoredProcedure("cgclaveprodservGet", mySqlParameters);
                List<SelectListItem> lstClaveProdServ = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstClaveProdServ.Add(new SelectListItem { Text = item["Descripcion"].ToString(), Value = item["Clave"].ToString() });
                }

                return lstClaveProdServ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> EmiteFacturacionGet()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable dataTable = conexion.RunStoredProcedure("tpModalidadFacturacionGet", mySqlParameters);
                List<SelectListItem> lstEmiteFact = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstEmiteFact.Add(new SelectListItem { Text = item["Modo"].ToString(), Value = item["Id"].ToString() });
                }

                return lstEmiteFact;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> MonedaPagoGet()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable dataTable = conexion.RunStoredProcedure("tpMonedaPagoGet", mySqlParameters);
                List<SelectListItem> lstMoneda = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstMoneda.Add(new SelectListItem { Text = item["Moneda"].ToString(), Value = item["IdMoneda"].ToString() });
                }

                return lstMoneda;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> TipoNotaGet()
        {
            try
            {
                List<SelectListItem> lstTipoNota = new List<SelectListItem>();
                lstTipoNota.Add(new SelectListItem { Value = "T", Text = "TEMPORAL", });
                lstTipoNota.Add(new SelectListItem { Value = "F", Text = "FIJA" });

                return lstTipoNota;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<LocalidadIngresos> GetListaIngresos(int id)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("IdLocalidadIn", MySqlDbType.Int32) { Value = id });
            DataTable dataTable = conexion.RunStoredProcedure("dtGastosFijosGet", mySqlParameters);
            try
            {
                List<LocalidadIngresos> lstlocalidadIngresos = new List<LocalidadIngresos>();
                foreach (DataRow item in dataTable.Rows)
                {
                    LocalidadIngresos localidadIngresos = new LocalidadIngresos();
                    localidadIngresos.IdLocalidad = int.Parse(item["IdLocalidad"].ToString());
                    localidadIngresos.IdGasto = int.Parse(item["IDGASTO"].ToString());
                    localidadIngresos.Concepto = item["Concepto"].ToString();
                    localidadIngresos.Importe = double.Parse(item["Importe"].ToString());
                    localidadIngresos.Unidad = item["clvUnidad"].ToString();
                    localidadIngresos.ProdServ = item["clvProdServ"].ToString();
                    localidadIngresos.RetencionISR = item["RetencionISR"].ToString();
                    localidadIngresos.RetencionIVA = item["RetencionIVA"].ToString();
                    lstlocalidadIngresos.Add(localidadIngresos);
                }
                return lstlocalidadIngresos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> CaracteristicasGet(int id)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("IdUsoInmuebleIn", MySqlDbType.Int32) { Value = id });
                DataTable dataTable = conexion.RunStoredProcedure("tpconceptousoinmuebleGet", mySqlParameters);
                List<SelectListItem> lstCaracteristicas = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstCaracteristicas.Add(new SelectListItem { Text = item["Concepto"].ToString(), Value = item["Id"].ToString() });
                }

                return lstCaracteristicas;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Localidad GetLocalidadById(int id)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("IdLocalidadIn", MySqlDbType.Int32) { Value = id });
            DataTable item = conexion.RunStoredProcedure("localidadesGetById", mySqlParameters);
            try
            {
                Localidad localidad = new Localidad();
                localidad.IdLocalidad = int.Parse(item.Rows[0]["IdLocalidad"].ToString());
                localidad.IdInquilino = int.Parse(item.Rows[0]["IdInquilino"].ToString());
                localidad.IdInmueble = int.Parse(item.Rows[0]["IdInmueble"].ToString());
                localidad.IdUsoInmueble = int.Parse(item.Rows[0]["IdUsoInmueble"].ToString());
                localidad.NumeroLocalidad = item.Rows[0]["NumeroLocalidad"].ToString();
                localidad.Estado = int.Parse(item.Rows[0]["Estado"].ToString());
                localidad.Ruta = item.Rows[0]["Ruta"].ToString();
                localidad.DivisionRuta = item.Rows[0]["DivisionRuta"].ToString();
                localidad.PagaAgua = (int.Parse(item.Rows[0]["PagaAgua2"].ToString()) == 1) ? true : false;
                localidad.DepRta = double.Parse(item.Rows[0]["DepRta"].ToString());
                localidad.ObservacionDepositoGarantia = item.Rows[0]["Observacion"].ToString();
                localidad.FechaIngreso = Convert.ToDateTime(item.Rows[0]["FechaIngreso"].ToString());
                localidad.FechaUltimaRenovacion = Convert.ToDateTime(item.Rows[0]["FechaUltimaRenovacion"].ToString());
                localidad.FechaTermino = Convert.ToDateTime(item.Rows[0]["FechaTermino"].ToString());
                localidad.DiaVencimiento = int.Parse(item.Rows[0]["DiaVencimiento"].ToString());
                localidad.NotaPromocion = item.Rows[0]["NotaPromocion"].ToString();
                localidad.FolioContrato = item.Rows[0]["FolioContrato"].ToString();
                localidad.RtaAnt = double.Parse(item.Rows[0]["RtaAnt"].ToString());
                localidad.FechaInicioAnt = Convert.ToDateTime(item.Rows[0]["FechaInicioAnt"].ToString());
                localidad.FechaTerminoAnt = Convert.ToDateTime(item.Rows[0]["FechaTerminoAnt"].ToString());
                localidad.DiaLimitePago = int.Parse(item.Rows[0]["DiaPago"].ToString());
                localidad.DescripcionWeb = item.Rows[0]["DescripcionWeb"].ToString();
                localidad.Penalidad = (item.Rows[0]["Penalidad"].ToString() == "T") ? true : false;
                localidad.PorCPenalidad = double.Parse(item.Rows[0]["PorCPenalidad"].ToString());
                localidad.IVA = (item.Rows[0]["IVA"].ToString() == "T") ? true : false;
                localidad.PorCIVA = double.Parse(item.Rows[0]["PorCIVA"].ToString());
                localidad.ImporteIVA = double.Parse(item.Rows[0]["ImporteIVA"].ToString());
                localidad.FacturarConceptosIndividuales = (item.Rows[0]["CobrarConceptosIndividuales"].ToString() == "T") ? true : false;
                localidad.IdModoFacturacion = int.Parse(item.Rows[0]["IdModoFacturacion"].ToString());
                localidad.IdMonedaPago = int.Parse(item.Rows[0]["IdMonedaPago"].ToString());
                localidad.Usuario = item.Rows[0]["Usuario"].ToString();
                localidad.Maquina = item.Rows[0]["Maquina"].ToString();
                localidad.DireccionIP = item.Rows[0]["DireccionIP"].ToString();
                localidad.PlazoPFAnio = int.Parse(item.Rows[0]["plazoPFAnio"].ToString());
                localidad.PlazoPFMes = int.Parse(item.Rows[0]["plazoPFMes"].ToString());
                localidad.PlazoPFDia = int.Parse(item.Rows[0]["plazoPFdia"].ToString());
                localidad.PlazoIFAnio = int.Parse(item.Rows[0]["plazoIFAnio"].ToString());
                localidad.PlazoIFMes = int.Parse(item.Rows[0]["plazoIFMes"].ToString());
                localidad.PlazoIFDia = int.Parse(item.Rows[0]["plazoIFDia"].ToString());
                localidad.PlazoPVAnio = int.Parse(item.Rows[0]["plazoPVAnio"].ToString());
                localidad.PlazoPVMes = int.Parse(item.Rows[0]["plazoPVMes"].ToString());
                localidad.PlazoPVDia = int.Parse(item.Rows[0]["plazoPVDia"].ToString());
                localidad.PlazoIVAnio = int.Parse(item.Rows[0]["plazoIVAnio"].ToString());
                localidad.PlazoIVMes = int.Parse(item.Rows[0]["plazoIVMes"].ToString());
                localidad.PlazoIVDia = int.Parse(item.Rows[0]["plazoIVDia"].ToString());
                localidad.IdRevisionBase = int.Parse(item.Rows[0]["idRevisionBase"].ToString());
                localidad.RevisionPeriodo = item.Rows[0]["revisionPeriodo"].ToString();
                localidad.RevisionPorcentajeAdicional = double.Parse(item.Rows[0]["revisionPorcentajeAdicional"].ToString());
                localidad.FechaRevision = Convert.ToDateTime(item.Rows[0]["fechaRevision"].ToString());
                localidad.FechaOcupacion = Convert.ToDateTime(item.Rows[0]["fechaOcupacion"].ToString());
                localidad.FechaInicioContratoUno = Convert.ToDateTime(item.Rows[0]["fechaInicioContratoUno"].ToString());
                localidad.FechaFinContratoUno = Convert.ToDateTime(item.Rows[0]["fechaFinContratoUno"].ToString());
                localidad.TipoCambioConvenio = double.Parse(item.Rows[0]["tipoCambioConvenio"].ToString());
                localidad.PorVenta = double.Parse(item.Rows[0]["PorVenta"].ToString());
                localidad.RentaGratis = item.Rows[0]["RentaGratis"].ToString();
                localidad.DerechoTanto = item.Rows[0]["DerechoTanto"].ToString();
                localidad.LocalesAgrupados = int.Parse(item.Rows[0]["LocalesAgrupados"].ToString());
                localidad.IdNombreComercial = int.Parse(item.Rows[0]["idNombreComercial"].ToString());
                localidad.PropTer = item.Rows[0]["PropTer"].ToString();
                localidad.Categoria = int.Parse(item.Rows[0]["categoria"].ToString());
                localidad.NumeroReferenciado = item.Rows[0]["NumeroReferenciado"].ToString();
                localidad.NumeroReferenciadoMtto = item.Rows[0]["NumeroReferenciadoMtto"].ToString();
                localidad.AplicaComplementoTerceros = (item.Rows[0]["AplicaComplementoTerceros"].ToString() == "1") ? true : false;
                localidad.Incobrable = (item.Rows[0]["Incobrable"].ToString() == "1") ? true : false;
                localidad.Pagare = item.Rows[0]["Pagare"].ToString();
                localidad.ObservacionesContrato = item.Rows[0]["ComentariosContrato"].ToString();
                localidad.TipoLocalidadAux = item.Rows[0]["TipoLocalidadAux"].ToString();
                localidad.InmuebleAux = item.Rows[0]["InmuebleAux"].ToString();
                localidad.EstatusAux = item.Rows[0]["EstadoAux"].ToString();
                localidad.ArrendatarioAux = item.Rows[0]["ArrendatarioAux"].ToString();
                localidad.NombreComercialAux = item.Rows[0]["NombreComercialAux"].ToString();

                localidad.PagaAguaHelada = (int.Parse(item.Rows[0]["PagaAguaHelada2"].ToString()) == 1) ? true : false;
                localidad.PagaEnergiaElectrica = (int.Parse(item.Rows[0]["PagaEnergiaElectrica2"].ToString()) == 1) ? true : false;
                localidad.PorcentajeAgua = double.Parse(item.Rows[0]["PorcentajeAgua"].ToString());
                localidad.PorcentajeAguaHelada = double.Parse(item.Rows[0]["PorcentajeAguaHelada"].ToString());
                localidad.PorcentajeEnergiaElectrica = double.Parse(item.Rows[0]["PorcentajeEnergiaElectrica"].ToString());

                localidad.ClausulasTerminacion = item.Rows[0]["clausulasTermino"].ToString();


                return localidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dtGastosFijos> dtGastosFijosList(int idlocalidad)
        {
            List<dtGastosFijos> dtGastosFijos = new List<dtGastosFijos>();
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            MySqlDataReader reader = null;

            try
            {
                mySqlParameters.Add(new MySqlParameter("IdLocalidadIn", idlocalidad));

                reader = conexion.RunStoredProcedureDR("dtGastosFijosGet", mySqlParameters);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dtGastosFijos.Add(new dtGastosFijos
                        {
                            IdLocalidad = int.Parse(reader["IdLocalidad"].ToString()),
                            Concepto = reader["Concepto"].ToString(),
                            IdGasto = int.Parse(reader["IDGASTO"].ToString()),
                            Importe = double.Parse(reader["Importe"].ToString()),
                            clvUnidad = reader["Unidad"].ToString(),
                            clvProdServ = reader["ProdServ"].ToString(),
                            RetencionISR = (reader["RetencionISR"].ToString() == "T" ? "SI" : "NO"),
                            RetencionIVA = (reader["RetencionIVA"].ToString() == "T" ? "SI" : "NO"),
                            ImpuestoCedular = (reader["ImpuestoCedular"].ToString() == "T" ? "SI" : "NO")
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.CloseConnection();
                if (reader != null)
                    reader.Close();
            }
            return dtGastosFijos;
        }

        public List<dtGastosFijos> dtGastosFijosListByIdGasto(Int64 idlocalidad, int IdGasto)
        {
            List<dtGastosFijos> dtGastosFijos = new List<dtGastosFijos>();
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            MySqlDataReader reader = null;

            try
            {
                mySqlParameters.Add(new MySqlParameter("IdLocalidad_In", idlocalidad));
                mySqlParameters.Add(new MySqlParameter("IdGasto_In", IdGasto));


                //reader = conexion.RunStoredProcedureDR("dtGastosFijosGetByIdGasto", mySqlParameters);
                DataTable dtreader = conexion.RunStoredProcedure("dtGastosFijosGetByIdGasto", mySqlParameters);
                foreach (DataRow item in dtreader.Rows)
                {
                    dtGastosFijos.Add(new dtGastosFijos
                    {
                        IdLocalidad = int.Parse(item["IdLocalidad"].ToString()),
                        IdGasto = int.Parse(item["IDGASTO"].ToString()),
                        Importe = double.Parse(item["Importe"].ToString()),
                        clvUnidad = item["clvUnidad"].ToString(),
                        clvProdServ = item["clvProdServ"].ToString(),
                        RetencionISR = (item["RetencionISR"].ToString() == "T" ? "SI" : "NO"),
                        PorCRetencionISR = double.Parse(item["PorCRetencionISR"].ToString()),
                        RetencionIVA = (item["RetencionIVA"].ToString() == "T" ? "SI" : "NO"),
                        PorCRetencionIVA = double.Parse(item["PorCRetencionIVA"].ToString()),
                        ImpuestoCedular = (item["ImpuestoCedular"].ToString() == "T" ? "SI" : "NO"),
                        PorCImpuestoCedular = double.Parse(item["PorCImpuestoCedular"].ToString())
                    });
                }
               
                //if (reader.HasRows)
                //{
                //    while (reader.Read())
                //    {
                //        dtGastosFijos.Add(new dtGastosFijos
                //        {
                //            IdLocalidad = int.Parse(reader["IdLocalidad"].ToString()),
                //            IdGasto = int.Parse(reader["IDGASTO"].ToString()),
                //            Importe = double.Parse(reader["Importe"].ToString()),
                //            clvUnidad = reader["clvUnidad"].ToString(),
                //            clvProdServ = reader["clvProdServ"].ToString(),
                //            RetencionISR = (reader["RetencionISR"].ToString() == "T" ? "SI" : "NO"),
                //            PorCRetencionISR = double.Parse(reader["PorCRetencionISR"].ToString()),
                //            RetencionIVA = (reader["RetencionIVA"].ToString() == "T" ? "SI" : "NO"),
                //            PorCRetencionIVA = double.Parse(reader["PorCRetencionIVA"].ToString())
                //        });
                //    }
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return dtGastosFijos;
        }

        public List<CaracteristicasInmueble> CaracteristicasByLocalidad(int IdLocalidad, int IdUsoInmueble)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("IdLocalidadIn", MySqlDbType.Int32) { Value = IdLocalidad });
            mySqlParameters.Add(new MySqlParameter("IdUsoInmuebleIn", MySqlDbType.Int32) { Value = IdUsoInmueble });
            DataTable dataTable = conexion.RunStoredProcedure("tpConceptoUsoInmuebleByLocalidad", mySqlParameters);

            try
            {

                List<CaracteristicasInmueble> lstCaracteristicas = new List<CaracteristicasInmueble>();
                foreach (DataRow item in dataTable.Rows)
                {
                    CaracteristicasInmueble caracteristicas = new CaracteristicasInmueble();
                    caracteristicas.IdConceptoUsoInmueble = int.Parse(item["Id"].ToString());
                    caracteristicas.Caracteristica = item["concepto"].ToString();
                    caracteristicas.Cantidad = double.Parse(item["cantidad"].ToString());
                    caracteristicas.Descripcion = item["descripcion"].ToString();

                    lstCaracteristicas.Add(caracteristicas);
                }
                return lstCaracteristicas;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<NotasLocalidad> NotasList(int idlocalidad)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("IdLocalidadIn", MySqlDbType.Int32) { Value = idlocalidad });
            var dataTable = conexion.RunStoredProcedure("notaslocalidadGetById", mySqlParameters);

            try
            {
                List<NotasLocalidad> lstnotas = new List<NotasLocalidad>();
                foreach (DataRow item in dataTable.Rows)
                {
                    NotasLocalidad notas = new NotasLocalidad();
                    notas.IdNota = int.Parse(item["IdNotaLocalidad"].ToString());
                    notas.IdLocalidad = int.Parse(item["IdLocalidad"].ToString());
                    notas.Nota = item["Nota"].ToString();
                    notas.Tipo = item["Tipo"].ToString();
                    notas.Fecha = Convert.ToDateTime(item["Fecha"].ToString());


                    lstnotas.Add(notas);
                }
                return lstnotas;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Localidad> GetListaLocalidades(int clapro, string numloc, string nomcom, int cartera, int usuario)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("idInmuebleIn", MySqlDbType.Int32) { Value = 0 });
            mySqlParameters.Add(new MySqlParameter("ClaproIn", MySqlDbType.Int32) { Value = clapro });
            mySqlParameters.Add(new MySqlParameter("NumLocIn", MySqlDbType.VarChar, 30) { Value = (numloc.Length > 0 || numloc != null) ? ("%" + numloc + "%") : "" });
            mySqlParameters.Add(new MySqlParameter("NombreComercialIn", MySqlDbType.VarChar, 80) { Value = (nomcom.Length > 0 || nomcom == null) ? ("%" + nomcom + "%") : "" });
            mySqlParameters.Add(new MySqlParameter("CarteraIn", MySqlDbType.Int32) { Value = cartera });
            mySqlParameters.Add(new MySqlParameter("UsuarioIn", MySqlDbType.Int32) { Value = usuario });
            DataTable dataTable = conexion.RunStoredProcedure("localidadesGetByInmueble", mySqlParameters);
            try
            {
                List<Localidad> lstLocalidades = new List<Localidad>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstLocalidades.Add(GetLocalidad(item));
                }
                return lstLocalidades;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Localidad GetLocalidad(DataRow item)
        {
            Localidad localidad = new Localidad();
            localidad.IdLocalidad = int.Parse(item["IdLocalidad"].ToString());
            localidad.IdInquilino = int.Parse(item["IdInquilino"].ToString());
            localidad.IdInmueble = int.Parse(item["IdInmueble"].ToString());
            localidad.IdUsoInmueble = int.Parse(item["IdUsoInmueble"].ToString());
            localidad.NumeroLocalidad = item["NumeroLocalidad"].ToString();
            localidad.Estado = int.Parse(item["Estado"].ToString());
            localidad.Ruta = item["Ruta"].ToString();
            localidad.DivisionRuta = item["DivisionRuta"].ToString();
            localidad.PagaAgua = (int.Parse(item["PagaAgua2"].ToString()) == 1) ? true : false;
            localidad.DepRta = double.Parse(item["DepRta"].ToString());
            localidad.ObservacionDepositoGarantia = item["Observacion"].ToString();
            localidad.FechaIngreso = Convert.ToDateTime(item["FechaIngreso"].ToString());
            localidad.FechaUltimaRenovacion = Convert.ToDateTime(item["FechaUltimaRenovacion"].ToString());
            localidad.FechaTermino = Convert.ToDateTime(item["FechaTermino"].ToString());
            localidad.DiaVencimiento = int.Parse(item["DiaVencimiento"].ToString());
            localidad.NotaPromocion = item["NotaPromocion"].ToString();
            localidad.FolioContrato = item["FolioContrato"].ToString();
            localidad.RtaAnt = double.Parse(item["RtaAnt"].ToString());
            localidad.FechaInicioAnt = Convert.ToDateTime(item["FechaInicioAnt"].ToString());
            localidad.FechaTerminoAnt = Convert.ToDateTime(item["FechaTerminoAnt"].ToString());
            localidad.DiaLimitePago = int.Parse(item["DiaPago"].ToString());
            localidad.DescripcionWeb = item["DescripcionWeb"].ToString();
            localidad.Penalidad = (item["Penalidad"].ToString() == "T") ? true : false;
            localidad.PorCPenalidad = double.Parse(item["PorCPenalidad"].ToString());
            localidad.IVA = (item["IVA"].ToString() == "T") ? true : false;
            localidad.PorCIVA = double.Parse(item["PorCIVA"].ToString());
            localidad.ImporteIVA = double.Parse(item["ImporteIVA"].ToString());
            localidad.FacturarConceptosIndividuales = (item["CobrarConceptosIndividuales"].ToString() == "T") ? true : false;
            localidad.IdModoFacturacion = int.Parse(item["IdModoFacturacion"].ToString());
            localidad.IdMonedaPago = int.Parse(item["IdMonedaPago"].ToString());
            localidad.Usuario = item["Usuario"].ToString();
            localidad.Maquina = item["Maquina"].ToString();
            localidad.DireccionIP = item["DireccionIP"].ToString();
            localidad.PlazoPFAnio = int.Parse(item["plazoPFAnio"].ToString());
            localidad.PlazoPFMes = int.Parse(item["plazoPFMes"].ToString());
            localidad.PlazoPFDia = int.Parse(item["plazoPFdia"].ToString());
            localidad.PlazoIFAnio = int.Parse(item["plazoIFAnio"].ToString());
            localidad.PlazoIFMes = int.Parse(item["plazoIFMes"].ToString());
            localidad.PlazoIFDia = int.Parse(item["plazoIFDia"].ToString());
            localidad.PlazoPVAnio = int.Parse(item["plazoPVAnio"].ToString());
            localidad.PlazoPVMes = int.Parse(item["plazoPVMes"].ToString());
            localidad.PlazoPVDia = int.Parse(item["plazoPVDia"].ToString());
            localidad.PlazoIVAnio = int.Parse(item["plazoIVAnio"].ToString());
            localidad.PlazoIVMes = int.Parse(item["plazoIVMes"].ToString());
            localidad.PlazoIVDia = int.Parse(item["plazoIVDia"].ToString());
            localidad.IdRevisionBase = int.Parse(item["idRevisionBase"].ToString());
            localidad.RevisionPeriodo = item["revisionPeriodo"].ToString();
            localidad.RevisionPorcentajeAdicional = double.Parse(item["revisionPorcentajeAdicional"].ToString());
            localidad.FechaRevision = Convert.ToDateTime(item["fechaRevision"].ToString());
            localidad.FechaOcupacion = Convert.ToDateTime(item["fechaOcupacion"].ToString());
            localidad.FechaInicioContratoUno = Convert.ToDateTime(item["fechaInicioContratoUno"].ToString());
            localidad.FechaFinContratoUno = Convert.ToDateTime(item["fechaFinContratoUno"].ToString());
            localidad.TipoCambioConvenio = double.Parse(item["tipoCambioConvenio"].ToString());
            localidad.PorVenta = double.Parse(item["PorVenta"].ToString());
            localidad.RentaGratis = item["RentaGratis"].ToString();
            localidad.DerechoTanto = item["DerechoTanto"].ToString();
            localidad.LocalesAgrupados = int.Parse(item["LocalesAgrupados"].ToString());
            localidad.IdNombreComercial = int.Parse(item["idNombreComercial"].ToString());
            localidad.PropTer = item["PropTer"].ToString();
            localidad.Categoria = int.Parse(item["categoria"].ToString());
            localidad.NumeroReferenciado = item["NumeroReferenciado"].ToString();
            localidad.NumeroReferenciadoMtto = item["NumeroReferenciadoMtto"].ToString();
            localidad.AplicaComplementoTerceros = (item["AplicaComplementoTerceros"].ToString() == "0") ? true : false;
            localidad.Incobrable = (item["Incobrable"].ToString() == "0") ? true : false;
            localidad.Pagare = item["Pagare"].ToString();
            localidad.ObservacionesContrato = item["ComentariosContrato"].ToString();
            localidad.TipoLocalidadAux = item["TipoLocalidadAux"].ToString();
            localidad.ClaveInmuebleAux = item["ClaveInmueble"].ToString();
            localidad.NombreComercialAux = item["NombreComercial"].ToString();
            localidad.ArrendatarioAux = item["Arrendatario"].ToString();
            localidad.EstadoAux = item["EstadoAux"].ToString();

            localidad.PagaAguaHelada = (int.Parse(item["PagaAguaHelada2"].ToString()) == 1) ? true : false;
            localidad.PagaEnergiaElectrica = (int.Parse(item["PagaEnergiaElectrica2"].ToString()) == 1) ? true : false;
            localidad.PorcentajeAgua = double.Parse(item["PorcentajeAgua"].ToString());
            localidad.PorcentajeAguaHelada = double.Parse(item["PorcentajeAguaHelada"].ToString());
            localidad.PorcentajeEnergiaElectrica = double.Parse(item["PorcentajeEnergiaElectrica"].ToString());

            localidad.TipoInmuebleGenteraAux = item["TipoInmuebleGentera"].ToString();

            return localidad;
        }

        public bool InsertNota(NotasLocalidad notas)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();

                mySqlParameters.Add(new MySqlParameter("IdLocalidadIn", MySqlDbType.Int64, 20) { Value = notas.IdLocalidad });
                mySqlParameters.Add(new MySqlParameter("FechaIn", MySqlDbType.DateTime) { Value = notas.Fecha });
                mySqlParameters.Add(new MySqlParameter("NotaIn", MySqlDbType.VarString, 150) { Value = notas.Nota.ToUpper() });
                mySqlParameters.Add(new MySqlParameter("TipoIn", MySqlDbType.VarChar, 1) { Value = notas.Tipo.ToUpper() });

                conexion.RunStoredProcedure("notaslocalidadInsert", mySqlParameters);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool InsertCaracteristicas(CaracteristicasInmueble caracteristicasInmueble)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();

                mySqlParameters.Add(new MySqlParameter("idLocalidadIn", MySqlDbType.Int64, 20) { Value = caracteristicasInmueble.IdLocalidad });
                mySqlParameters.Add(new MySqlParameter("idConceptoUsoInmuebleIn", MySqlDbType.Int32) { Value = caracteristicasInmueble.IdUsoInmueble });
                mySqlParameters.Add(new MySqlParameter("cantidadIn", MySqlDbType.Double) { Value = caracteristicasInmueble.Cantidad });
                mySqlParameters.Add(new MySqlParameter("descripcionIn", MySqlDbType.VarChar, 250) { Value = caracteristicasInmueble.Descripcion.ToUpper() });

                conexion.RunStoredProcedure("dtConceptoUsoInmuebleInsert", mySqlParameters);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetIdGastoIVA(int IdGasto)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("IdGastoIn", MySqlDbType.Int64, 20) { Value = IdGasto });
            DataTable dataTable = conexion.RunStoredProcedure("tpgastosGetIVAByConcepto", mySqlParameters);

            if (dataTable.Rows.Count > 0)
                return int.Parse(dataTable.Rows[0]["IVA"].ToString());
            else
                return 0;
        }

        public bool InsertGastosFijos(dtGastosFijos dtGastosFijos)
        {
            try
            {
                bool AplicaIVA = dtGastosFijos.IVA;
                var IdGastoIVA = AplicaIVA ? GetIdGastoIVA(dtGastosFijos.IdGasto) : 0;
                var cnt = 0;
                if (IdGastoIVA > 0)
                    cnt = 1;

                for (int i = 0; i <= cnt; i++)
                {
                    List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();

                    mySqlParameters.Add(new MySqlParameter("IdLocalidadIn", MySqlDbType.Int64, 20) { Value = dtGastosFijos.IdLocalidad });
                    mySqlParameters.Add(new MySqlParameter("IdGastoIn", MySqlDbType.Int32) { Value = (i == 0) ? dtGastosFijos.IdGasto : IdGastoIVA });
                    mySqlParameters.Add(new MySqlParameter("ImporteIn", MySqlDbType.Double) { Value = (i == 0) ? dtGastosFijos.Importe : Math.Round((dtGastosFijos.Importe * 0.16), 2) });
                    mySqlParameters.Add(new MySqlParameter("UsuarioIn", MySqlDbType.VarChar, 45) { Value = "" });
                    mySqlParameters.Add(new MySqlParameter("MaquinaIn", MySqlDbType.VarChar, 45) { Value = Environment.MachineName });
                    mySqlParameters.Add(new MySqlParameter("DireccionIPIn", MySqlDbType.VarChar, 45) { Value = "" });
                    mySqlParameters.Add(new MySqlParameter("clvUnidadIn", MySqlDbType.VarChar, 5) { Value = dtGastosFijos.clvUnidad.ToUpper() });
                    mySqlParameters.Add(new MySqlParameter("clvProdServIn", MySqlDbType.VarChar, 10) { Value = dtGastosFijos.clvProdServ.ToUpper() });
                    mySqlParameters.Add(new MySqlParameter("RetencionISRIn", MySqlDbType.Enum) { Value = dtGastosFijos.RetencionISR });
                    mySqlParameters.Add(new MySqlParameter("PorCRetencionISRIn", MySqlDbType.Double) { Value = dtGastosFijos.PorCRetencionISR });
                    mySqlParameters.Add(new MySqlParameter("RetencionIVAIn", MySqlDbType.Enum) { Value = dtGastosFijos.RetencionIVA });
                    mySqlParameters.Add(new MySqlParameter("PorCRetencionIVAIn", MySqlDbType.Double) { Value = dtGastosFijos.PorCRetencionIVA });
                    mySqlParameters.Add(new MySqlParameter("ImpuestoCedularIn", MySqlDbType.Enum) { Value = dtGastosFijos.ImpuestoCedular });
                    mySqlParameters.Add(new MySqlParameter("PorCImpuestoCedularIn", MySqlDbType.Double) { Value = dtGastosFijos.PorCImpuestoCedular });

                    conexion.RunStoredProcedure("dtGastosFijosInsert", mySqlParameters);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Crear(Localidad localidad)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();

                mySqlParameters.Add(new MySqlParameter("IdInquilinoIn", MySqlDbType.Int64, 20) { Value = localidad.IdInquilino });
                mySqlParameters.Add(new MySqlParameter("IdInmuebleIn", MySqlDbType.Int64, 20) { Value = localidad.IdInmueble });
                mySqlParameters.Add(new MySqlParameter("IdUsoInmuebleIn", MySqlDbType.Int64, 11) { Value = localidad.IdUsoInmueble });
                mySqlParameters.Add(new MySqlParameter("NumeroLocalidadIn", MySqlDbType.VarChar, 30) { Value = localidad.NumeroLocalidad });
                mySqlParameters.Add(new MySqlParameter("EstadoIn", MySqlDbType.Int64, 11) { Value = localidad.Estado });
                //mySqlParameters.Add(new MySqlParameter("RutaIn", MySqlDbType.VarChar, 100) { Value = "" });
                //mySqlParameters.Add(new MySqlParameter("DivisionRutaIn", MySqlDbType.VarChar, 100) { Value = "" });
                mySqlParameters.Add(new MySqlParameter("PagaAguaIn", MySqlDbType.VarChar, 1) { Value = (localidad.PagaAgua) ? "S" : "N" });
                mySqlParameters.Add(new MySqlParameter("DepRtaIn", MySqlDbType.Double) { Value = localidad.DepRta });
                mySqlParameters.Add(new MySqlParameter("ObservacionIn", MySqlDbType.VarString) { Value = localidad.ObservacionDepositoGarantia });
                mySqlParameters.Add(new MySqlParameter("FechaIngresoIn", MySqlDbType.DateTime) { Value = localidad.FechaIngreso });
                mySqlParameters.Add(new MySqlParameter("FechaUltimaRenovacionIn", MySqlDbType.DateTime) { Value = localidad.FechaUltimaRenovacion });
                mySqlParameters.Add(new MySqlParameter("FechaTerminoIn", MySqlDbType.DateTime) { Value = localidad.FechaTermino });
                mySqlParameters.Add(new MySqlParameter("DiaVencimientoIn", MySqlDbType.Int64, 11) { Value = localidad.DiaVencimiento });
                mySqlParameters.Add(new MySqlParameter("NotaPromocionIn", MySqlDbType.VarChar, 4) { Value = localidad.NotaPromocion });
                mySqlParameters.Add(new MySqlParameter("FolioContratoIn", MySqlDbType.VarChar, 30) { Value = localidad.FolioContrato });
                mySqlParameters.Add(new MySqlParameter("RtaAntIn", MySqlDbType.Double) { Value = localidad.RtaAnt });
                mySqlParameters.Add(new MySqlParameter("FechaInicioAntIn", MySqlDbType.DateTime) { Value = localidad.FechaInicioAnt });
                mySqlParameters.Add(new MySqlParameter("FechaTerminoAntIn", MySqlDbType.DateTime) { Value = localidad.FechaTerminoAnt });
                mySqlParameters.Add(new MySqlParameter("StatusIn", MySqlDbType.Int64, 11) { Value = localidad.Status });
                mySqlParameters.Add(new MySqlParameter("DiaPagoIn", MySqlDbType.VarChar, 11) { Value = localidad.DiaLimitePago });
                mySqlParameters.Add(new MySqlParameter("DescripcionWebIn", MySqlDbType.VarString) { Value = "" });
                mySqlParameters.Add(new MySqlParameter("PenalidadIn", MySqlDbType.VarChar, 1) { Value = "F" /*localidad.Penalidad*/ });
                mySqlParameters.Add(new MySqlParameter("PorCPenalidadIn", MySqlDbType.Double) { Value = localidad.PorCPenalidad });
                mySqlParameters.Add(new MySqlParameter("IVAIn", MySqlDbType.VarChar, 1) { Value = "F" /*localidad.IVA*/ });
                mySqlParameters.Add(new MySqlParameter("PorCIVAIn", MySqlDbType.Double) { Value = localidad.PorCIVA });
                mySqlParameters.Add(new MySqlParameter("ImporteIVAIn", MySqlDbType.Double) { Value = localidad.ImporteIVA });
                mySqlParameters.Add(new MySqlParameter("CobrarConceptosIndividualesIn", MySqlDbType.VarChar, 1) { Value = "F" /*localidad.FacturarConceptosIndividuales*/});
                mySqlParameters.Add(new MySqlParameter("IdModoFacturacionIn", MySqlDbType.Int32, 4) { Value = localidad.IdModoFacturacion });
                mySqlParameters.Add(new MySqlParameter("IdMonedaPagoIn", MySqlDbType.Int32, 4) { Value = localidad.IdMonedaPago });
                mySqlParameters.Add(new MySqlParameter("UsuarioIn", MySqlDbType.VarChar, 45) { Value = localidad.Usuario });
                mySqlParameters.Add(new MySqlParameter("MaquinaIn", MySqlDbType.VarChar, 45) { Value = localidad.Maquina });
                mySqlParameters.Add(new MySqlParameter("DireccionIPIn", MySqlDbType.VarChar, 45) { Value = localidad.DireccionIP });
                mySqlParameters.Add(new MySqlParameter("plazoPFAnioIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoPFAnio });
                mySqlParameters.Add(new MySqlParameter("plazoPFMesIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoPFMes });
                mySqlParameters.Add(new MySqlParameter("plazoPFdiaIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoPFDia });
                mySqlParameters.Add(new MySqlParameter("plazoIFAnioIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoIFAnio });
                mySqlParameters.Add(new MySqlParameter("plazoIFMesIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoIFMes });
                mySqlParameters.Add(new MySqlParameter("plazoIFDiaIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoIFDia });
                mySqlParameters.Add(new MySqlParameter("plazoPVAnioIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoPVAnio });
                mySqlParameters.Add(new MySqlParameter("plazoPVMesIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoPVMes });
                mySqlParameters.Add(new MySqlParameter("plazoPVDiaIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoPVDia });
                mySqlParameters.Add(new MySqlParameter("plazoIVAnioIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoIVAnio });
                mySqlParameters.Add(new MySqlParameter("plazoIVMesIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoIVMes });
                mySqlParameters.Add(new MySqlParameter("plazoIVDiaIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoIVDia });
                mySqlParameters.Add(new MySqlParameter("idRevisionBaseIn", MySqlDbType.Int64, 11) { Value = localidad.IdRevisionBase });
                mySqlParameters.Add(new MySqlParameter("revisionPeriodoIn", MySqlDbType.Enum) { Value = localidad.RevisionPeriodo });
                mySqlParameters.Add(new MySqlParameter("revisionPorcentajeAdicionalIn", MySqlDbType.Double) { Value = localidad.RevisionPorcentajeAdicional });
                mySqlParameters.Add(new MySqlParameter("fechaRevisionIn", MySqlDbType.DateTime) { Value = localidad.FechaRevision });
                mySqlParameters.Add(new MySqlParameter("fechaOcupacionIn", MySqlDbType.DateTime) { Value = localidad.FechaOcupacion });
                mySqlParameters.Add(new MySqlParameter("fechaInicioContratoUnoIn", MySqlDbType.DateTime) { Value = localidad.FechaInicioContratoUno });
                mySqlParameters.Add(new MySqlParameter("fechaFinContratoUnoIn", MySqlDbType.DateTime) { Value = localidad.FechaFinContratoUno });
                mySqlParameters.Add(new MySqlParameter("tipoCambioConvenioIn", MySqlDbType.Double) { Value = localidad.TipoCambioConvenio });
                mySqlParameters.Add(new MySqlParameter("PorVentaIn", MySqlDbType.Double) { Value = localidad.PorVenta });
                mySqlParameters.Add(new MySqlParameter("RentaGratisIn", MySqlDbType.VarChar, 220) { Value = localidad.RentaGratis });
                mySqlParameters.Add(new MySqlParameter("DerechoTantoIn", MySqlDbType.VarChar, 220) { Value = localidad.DerechoTanto });
                mySqlParameters.Add(new MySqlParameter("LocalesAgrupadosIn", MySqlDbType.Int64, 11) { Value = localidad.LocalesAgrupados });
                mySqlParameters.Add(new MySqlParameter("idNombreComercialIn", MySqlDbType.Int64, 20) { Value = localidad.IdNombreComercial });
                mySqlParameters.Add(new MySqlParameter("PropTerIn", MySqlDbType.VarChar, 1) { Value = localidad.PropTer });
                mySqlParameters.Add(new MySqlParameter("categoriaIn", MySqlDbType.Int64, 11) { Value = localidad.Categoria });
                mySqlParameters.Add(new MySqlParameter("NumeroReferenciadoIn", MySqlDbType.VarChar, 60) { Value = localidad.NumeroReferenciado });
                mySqlParameters.Add(new MySqlParameter("NumeroReferenciadoMttoIn", MySqlDbType.VarChar, 60) { Value = localidad.NumeroReferenciadoMtto });
                mySqlParameters.Add(new MySqlParameter("AplicaComplementoTercerosIn", MySqlDbType.Int32, 4) { Value = 0 /*localidad.AplicaComplementoTerceros*/ });
                mySqlParameters.Add(new MySqlParameter("IncobrableIn", MySqlDbType.Int32, 4) { Value = 0 /*localidad.Incobrable*/ });
                mySqlParameters.Add(new MySqlParameter("PagareIn", MySqlDbType.VarString) { Value = localidad.Pagare });
                mySqlParameters.Add(new MySqlParameter("ComentariosContratoIn", MySqlDbType.VarString) { Value = localidad.ObservacionesContrato });

                mySqlParameters.Add(new MySqlParameter("PagaAguaHeladaIn", MySqlDbType.VarString) { Value = localidad.PagaAguaHelada ? "S" : "N" });
                mySqlParameters.Add(new MySqlParameter("PagaEnergiaElectricaIn", MySqlDbType.VarString) { Value = localidad.PagaEnergiaElectrica ? "S" : "N" });
                mySqlParameters.Add(new MySqlParameter("PorcentajeAguaIn", MySqlDbType.Double) { Value = localidad.PorcentajeAgua });
                mySqlParameters.Add(new MySqlParameter("PorcentajeAguaHeladaIn", MySqlDbType.Double) { Value = localidad.PorcentajeAguaHelada });
                mySqlParameters.Add(new MySqlParameter("PorcentajeEnergiaElectricaIn", MySqlDbType.Double) { Value = localidad.PorcentajeEnergiaElectrica });

                mySqlParameters.Add(new MySqlParameter("ClausulasTerminoIn", MySqlDbType.VarString) { Value = localidad.ClausulasTerminacion });

                conexion.RunStoredProcedure("localidadesInsert", mySqlParameters);
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw;
            }
        }

        public bool Actualizar(Localidad localidad, int id)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();

                mySqlParameters.Add(new MySqlParameter("IdLocalidadIn", MySqlDbType.Int64, 20) { Value = id });
                mySqlParameters.Add(new MySqlParameter("IdInquilinoIn", MySqlDbType.Int64, 20) { Value = localidad.IdInquilino });
                mySqlParameters.Add(new MySqlParameter("IdInmuebleIn", MySqlDbType.Int64, 20) { Value = localidad.IdInmueble });
                mySqlParameters.Add(new MySqlParameter("IdUsoInmuebleIn", MySqlDbType.Int64, 11) { Value = localidad.IdUsoInmueble });
                mySqlParameters.Add(new MySqlParameter("NumeroLocalidadIn", MySqlDbType.VarChar, 30) { Value = localidad.NumeroLocalidad });
                mySqlParameters.Add(new MySqlParameter("EstadoIn", MySqlDbType.Int64, 11) { Value = localidad.Estado });
                //mySqlParameters.Add(new MySqlParameter("RutaIn", MySqlDbType.VarChar, 100) { Value = localidad.Ruta });
                //mySqlParameters.Add(new MySqlParameter("DivisionRutaIn", MySqlDbType.VarChar, 100) { Value = localidad.DivisionRuta });
                mySqlParameters.Add(new MySqlParameter("PagaAguaIn", MySqlDbType.VarChar, 1) { Value = localidad.PagaAgua ? "S" : "N" });
                mySqlParameters.Add(new MySqlParameter("DepRtaIn", MySqlDbType.Double) { Value = localidad.DepRta });
                mySqlParameters.Add(new MySqlParameter("ObservacionIn", MySqlDbType.VarString) { Value = localidad.ObservacionDepositoGarantia });
                mySqlParameters.Add(new MySqlParameter("FechaIngresoIn", MySqlDbType.DateTime) { Value = localidad.FechaIngreso });
                mySqlParameters.Add(new MySqlParameter("FechaUltimaRenovacionIn", MySqlDbType.DateTime) { Value = localidad.FechaUltimaRenovacion });
                mySqlParameters.Add(new MySqlParameter("FechaTerminoIn", MySqlDbType.DateTime) { Value = localidad.FechaTermino });
                mySqlParameters.Add(new MySqlParameter("DiaVencimientoIn", MySqlDbType.Int64, 11) { Value = localidad.DiaVencimiento });
                mySqlParameters.Add(new MySqlParameter("NotaPromocionIn", MySqlDbType.VarChar, 4) { Value = localidad.NotaPromocion });
                mySqlParameters.Add(new MySqlParameter("FolioContratoIn", MySqlDbType.VarChar, 30) { Value = localidad.FolioContrato });
                mySqlParameters.Add(new MySqlParameter("RtaAntIn", MySqlDbType.Double) { Value = localidad.RtaAnt });
                mySqlParameters.Add(new MySqlParameter("FechaInicioAntIn", MySqlDbType.DateTime) { Value = localidad.FechaInicioAnt });
                mySqlParameters.Add(new MySqlParameter("FechaTerminoAntIn", MySqlDbType.DateTime) { Value = localidad.FechaTerminoAnt });
                mySqlParameters.Add(new MySqlParameter("StatusIn", MySqlDbType.Int64, 11) { Value = 0 });
                mySqlParameters.Add(new MySqlParameter("DiaPagoIn", MySqlDbType.VarChar, 11) { Value = localidad.DiaLimitePago });
                mySqlParameters.Add(new MySqlParameter("DescripcionWebIn", MySqlDbType.VarString) { Value = "" });
                mySqlParameters.Add(new MySqlParameter("PenalidadIn", MySqlDbType.VarChar, 1) { Value = (localidad.Penalidad) ? "T" : "F" });
                mySqlParameters.Add(new MySqlParameter("PorCPenalidadIn", MySqlDbType.Double) { Value = localidad.PorCPenalidad });
                mySqlParameters.Add(new MySqlParameter("IVAIn", MySqlDbType.VarChar, 1) { Value = (localidad.IVA) ? "T" : "F" });
                mySqlParameters.Add(new MySqlParameter("PorCIVAIn", MySqlDbType.Double) { Value = localidad.PorCIVA });
                mySqlParameters.Add(new MySqlParameter("ImporteIVAIn", MySqlDbType.Double) { Value = localidad.ImporteIVA });
                mySqlParameters.Add(new MySqlParameter("CobrarConceptosIndividualesIn", MySqlDbType.VarChar, 1) { Value = (localidad.FacturarConceptosIndividuales) ? "T" : "F" });
                mySqlParameters.Add(new MySqlParameter("IdModoFacturacionIn", MySqlDbType.Int32, 4) { Value = localidad.IdModoFacturacion });
                mySqlParameters.Add(new MySqlParameter("IdMonedaPagoIn", MySqlDbType.Int32, 4) { Value = localidad.IdMonedaPago });
                mySqlParameters.Add(new MySqlParameter("UsuarioIn", MySqlDbType.VarChar, 45) { Value = localidad.Usuario });
                mySqlParameters.Add(new MySqlParameter("MaquinaIn", MySqlDbType.VarChar, 45) { Value = localidad.Maquina });
                mySqlParameters.Add(new MySqlParameter("DireccionIPIn", MySqlDbType.VarChar, 45) { Value = localidad.DireccionIP });
                mySqlParameters.Add(new MySqlParameter("plazoPFAnioIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoPFAnio });
                mySqlParameters.Add(new MySqlParameter("plazoPFMesIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoPFMes });
                mySqlParameters.Add(new MySqlParameter("plazoPFdiaIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoPFDia });
                mySqlParameters.Add(new MySqlParameter("plazoIFAnioIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoIFAnio });
                mySqlParameters.Add(new MySqlParameter("plazoIFMesIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoIFMes });
                mySqlParameters.Add(new MySqlParameter("plazoIFDiaIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoIFDia });
                mySqlParameters.Add(new MySqlParameter("plazoPVAnioIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoPVAnio });
                mySqlParameters.Add(new MySqlParameter("plazoPVMesIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoPVMes });
                mySqlParameters.Add(new MySqlParameter("plazoPVDiaIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoPVDia });
                mySqlParameters.Add(new MySqlParameter("plazoIVAnioIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoIVAnio });
                mySqlParameters.Add(new MySqlParameter("plazoIVMesIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoIVMes });
                mySqlParameters.Add(new MySqlParameter("plazoIVDiaIn", MySqlDbType.Int64, 11) { Value = localidad.PlazoIVDia });
                mySqlParameters.Add(new MySqlParameter("idRevisionBaseIn", MySqlDbType.Int64, 11) { Value = localidad.IdRevisionBase });
                mySqlParameters.Add(new MySqlParameter("revisionPeriodoIn", MySqlDbType.Enum) { Value = localidad.RevisionPeriodo });
                mySqlParameters.Add(new MySqlParameter("revisionPorcentajeAdicionalIn", MySqlDbType.Double) { Value = localidad.RevisionPorcentajeAdicional });
                mySqlParameters.Add(new MySqlParameter("fechaRevisionIn", MySqlDbType.DateTime) { Value = localidad.FechaRevision });
                mySqlParameters.Add(new MySqlParameter("fechaOcupacionIn", MySqlDbType.DateTime) { Value = localidad.FechaOcupacion });
                mySqlParameters.Add(new MySqlParameter("fechaInicioContratoUnoIn", MySqlDbType.DateTime) { Value = localidad.FechaInicioContratoUno });
                mySqlParameters.Add(new MySqlParameter("fechaFinContratoUnoIn", MySqlDbType.DateTime) { Value = localidad.FechaFinContratoUno });
                mySqlParameters.Add(new MySqlParameter("tipoCambioConvenioIn", MySqlDbType.Double) { Value = localidad.TipoCambioConvenio });
                mySqlParameters.Add(new MySqlParameter("PorVentaIn", MySqlDbType.Double) { Value = localidad.PorVenta });
                mySqlParameters.Add(new MySqlParameter("RentaGratisIn", MySqlDbType.VarChar, 220) { Value = localidad.RentaGratis });
                mySqlParameters.Add(new MySqlParameter("DerechoTantoIn", MySqlDbType.VarChar, 220) { Value = localidad.DerechoTanto });
                mySqlParameters.Add(new MySqlParameter("LocalesAgrupadosIn", MySqlDbType.Int64, 11) { Value = localidad.LocalesAgrupados });
                mySqlParameters.Add(new MySqlParameter("idNombreComercialIn", MySqlDbType.Int64, 20) { Value = localidad.IdNombreComercial });
                mySqlParameters.Add(new MySqlParameter("PropTerIn", MySqlDbType.VarChar, 1) { Value = localidad.PropTer });
                mySqlParameters.Add(new MySqlParameter("categoriaIn", MySqlDbType.Int64, 11) { Value = localidad.Categoria });
                mySqlParameters.Add(new MySqlParameter("NumeroReferenciadoIn", MySqlDbType.VarChar, 60) { Value = localidad.NumeroReferenciado });
                mySqlParameters.Add(new MySqlParameter("NumeroReferenciadoMttoIn", MySqlDbType.VarChar, 60) { Value = localidad.NumeroReferenciadoMtto });
                mySqlParameters.Add(new MySqlParameter("AplicaComplementoTercerosIn", MySqlDbType.Int32, 4) { Value = (localidad.AplicaComplementoTerceros) ? 1 : 0 });
                mySqlParameters.Add(new MySqlParameter("IncobrableIn", MySqlDbType.Int32, 4) { Value = (localidad.Incobrable) ? 1 : 0 });
                mySqlParameters.Add(new MySqlParameter("PagareIn", MySqlDbType.VarString) { Value = localidad.Pagare });
                mySqlParameters.Add(new MySqlParameter("ComentariosContratoIn", MySqlDbType.VarString) { Value = localidad.ObservacionesContrato });

                mySqlParameters.Add(new MySqlParameter("PagaAguaHeladaIn", MySqlDbType.VarString) { Value = localidad.PagaAguaHelada ? "S" : "N" });
                mySqlParameters.Add(new MySqlParameter("PagaEnergiaElectricaIn", MySqlDbType.VarString) { Value = localidad.PagaEnergiaElectrica ? "S" : "N" });
                mySqlParameters.Add(new MySqlParameter("PorcentajeAguaIn", MySqlDbType.Double) { Value = localidad.PorcentajeAgua });
                mySqlParameters.Add(new MySqlParameter("PorcentajeAguaHeladaIn", MySqlDbType.Double) { Value = localidad.PorcentajeAguaHelada });
                mySqlParameters.Add(new MySqlParameter("PorcentajeEnergiaElectricaIn", MySqlDbType.Double) { Value = localidad.PorcentajeEnergiaElectrica });

                mySqlParameters.Add(new MySqlParameter("ClausulasTerminoIn", MySqlDbType.VarString) { Value = localidad.ClausulasTerminacion });

                conexion.RunStoredProcedure("localidadesUpdate", mySqlParameters);
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw;
            }
        }

        public bool Eliminar(int id)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("IdLocalidadIn", MySqlDbType.Int64, 20) { Value = id });
                conexion.RunStoredProcedure("localidadesDelete", mySqlParameters);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EliminarNota(int id)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("IdNotaIn", MySqlDbType.Int64, 20) { Value = id });
                conexion.RunStoredProcedure("notaslocalidadDelete", mySqlParameters);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EliminarGastos(int id, int idlocalidad)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("IdGastoIn", MySqlDbType.Int64, 20) { Value = id });
                mySqlParameters.Add(new MySqlParameter("IdLocalidadIn", MySqlDbType.Int64, 20) { Value = idlocalidad });
                conexion.RunStoredProcedure("dtgastosfijosDelete", mySqlParameters);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EliminarCaracteristicas(int id, int idusoinmueble)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("IdLocalidadIn", MySqlDbType.Int64, 20) { Value = id });
                mySqlParameters.Add(new MySqlParameter("IdConceptoUsoIn", MySqlDbType.Int64, 20) { Value = idusoinmueble });
                conexion.RunStoredProcedure("dtconceptousoinmuebleDelete", mySqlParameters);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
