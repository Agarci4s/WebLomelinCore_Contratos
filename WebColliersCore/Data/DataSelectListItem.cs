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
    public class DataSelectListItem
    {
        private Conexion conexion = new Conexion();

        public List<SelectListItem> UsoCFDI()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("c_usocfdiGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Clave"].ToString() + "-" + item["Descripcion"].ToString(), Value = item["Clave"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> CondicionesPago()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("condicionespagoGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Condiciones"].ToString(), Value = item["IdCondiciones"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> GastosFijos()
        {

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("tpgastosGetAll", mySqlParameters);
            List<SelectListItem> lstGastosFijos = new List<SelectListItem>();
            lstGastosFijos.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            foreach (DataRow item in dataTable.Rows)
            {
                lstGastosFijos.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["IdGasto"].ToString() });
            }
            return lstGastosFijos;
        }

        public List<SelectListItem> GastosFijos2()
        {

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("tpgastosGetAll", mySqlParameters);
            List<SelectListItem> lstGastosFijos = new List<SelectListItem>();
            lstGastosFijos.Add(new SelectListItem { Text = "Selecciona una opción", Value = "-1" });
            foreach (DataRow item in dataTable.Rows)
            {
                lstGastosFijos.Add(new SelectListItem { Text = item["descripcion"].ToString(), Value = item["IdGasto"].ToString() });
            }
            return lstGastosFijos;
        }

        public List<SelectListItem> FormasPago()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("tpformapagoGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["formapago"].ToString(), Value = item["idFormapago"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> MetodoPago()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("c_metodopagoGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Descripcion"].ToString(), Value = item["Clave"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> ActividadPuesto()
        {
            DataPuesto dataPuesto = new DataPuesto();
            List<DtActividadPuesto> dtActividadPuestoList = dataPuesto.Get(0);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DtActividadPuesto item in dtActividadPuestoList)
            {
                listSelectListItems.Add(new SelectListItem { Text = item.actividad, Value = item.idactividad_puesto.ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> RazonSocialDatosBancarios()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("GetRazonSocialIB", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["RazonSocial"].ToString(), Value = item["Id"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> InmueblesByUsuario(int IdCartera, int IdUsuario)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            DataTable dataTable = conexion.RunStoredProcedure("inmueblesGetByUsuario", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["NombreInmueble"].ToString(), Value = item["Id"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> factDetalleGrlInmueblesByUsuario(int periodo, int IdCartera, int IdUsuario, int Pagada, int Opcion)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("Periodo_In", periodo));
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("Pagada_In", Pagada));
            listSqlParameters.Add(new MySqlParameter("Opcion_In", Opcion));

            DataTable dataTable = conexion.RunStoredProcedure("fact_detalle_Get_Inmueble", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["NombreInmueble"].ToString(), Value = item["IdInmueble"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> PropietariosByUsuario(int IdCartera, int IdUsuario)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            DataTable dataTable = conexion.RunStoredProcedure("PropietariosGetByUsuario", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["IdPropietario"].ToString() + " " + item["Nombre"].ToString(), Value = item["IdPropietario"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> factDetalleGrlClasificacionByUsuario(int periodo, int IdCartera, int IdUsuario, int Pagada, int Opcion)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("Periodo_In", periodo));
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("Pagada_In", Pagada));
            listSqlParameters.Add(new MySqlParameter("Opcion_In", Opcion));

            DataTable dataTable = conexion.RunStoredProcedure("fact_detalle_Get_Clasificacion", listSqlParameters);

            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                if (Opcion == 1 || Opcion == 3)
                    listSelectListItems.Add(new SelectListItem { Text = item["ClasificacionGentera"].ToString(), Value = item["ClasificacionGentera"].ToString() });
                else
                    listSelectListItems.Add(new SelectListItem { Text = item["ClasificacionDylsa"].ToString(), Value = item["ClasificacionDylsa"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> factDetalleGrlClasificacionTransferenciaByUsuario(int periodo, int IdCartera, int IdUsuario, int Pagada, int Opcion)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("Periodo_In", periodo));
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("Pagada_In", Pagada));
            listSqlParameters.Add(new MySqlParameter("Opcion_In", Opcion));

            DataTable dataTable = conexion.RunStoredProcedure("fact_detalle_Get_Clasificacion_Transferencia", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                if (Opcion == 1 || Opcion == 3)
                    listSelectListItems.Add(new SelectListItem { Text = item["ClasificacionGentera"].ToString(), Value = item["ClasificacionGentera"].ToString() });
                else
                    listSelectListItems.Add(new SelectListItem { Text = item["ClasificacionDylsa"].ToString(), Value = item["ClasificacionDylsa"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> CgTramites()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("CgTramitesGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Tramite"].ToString(), Value = item["IdTramite"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> InmuebleGetEstados(int IdUsuario)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));

            DataTable dataTable = conexion.RunStoredProcedure("inmuebleGetEstados", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["estado"].ToString(), Value = item["estado"].ToString() });

            }
            return listSelectListItems;
        }

        public List<SelectListItem> localidadesGetByIdInmueble(Int64 IdInmueble)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdInmuebleIn", IdInmueble));

            DataTable dataTable = conexion.RunStoredProcedure("localidadesGetByIdInmueble", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Localidad"].ToString(), Value = item["IdLocalidad"].ToString() });

            }
            return listSelectListItems;
        }

        public List<SelectListItem> localidadesGetByPropietario(Int64 IdPropietario, int IdCartera, int IdUsuario)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("IdPropietario_In", IdPropietario));

            DataTable dataTable = conexion.RunStoredProcedure("localidadesGetByPropietario", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Localidad"].ToString(), Value = item["IdLocalidad"].ToString() });

            }
            return listSelectListItems;
        }

        public List<SelectListItem> localidadesGetByIdInmuebleOcupadas(Int64 IdInmueble)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdInmuebleIn", IdInmueble));

            DataTable dataTable = conexion.RunStoredProcedure("localidadesGetByIdInmuebleOcupadas", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Localidad"].ToString(), Value = item["IdLocalidad"].ToString() });

            }
            return listSelectListItems;
        }

        public List<SelectListItem> CgClaveUnidad()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            DataTable dataTable = conexion.RunStoredProcedure("cgclaveunidadGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Nombre"].ToString(), Value = item["Clave"].ToString() });

            }
            return listSelectListItems;
        }

        public List<SelectListItem> CgClaveProdServGet()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();

            DataTable dataTable = conexion.RunStoredProcedure("cgclaveprodservGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Descripcion"].ToString(), Value = item["Clave"].ToString() });

            }
            return listSelectListItems;
        }

        public List<SelectListItem> FactDetalleRentaGetClasificacionByLocalidad(Int64 idLocalidad)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idlocalidad_In", idLocalidad));


            DataTable dataTable = conexion.RunStoredProcedure("fact_detalle_renta_Get_Clasificacion_By_Localidad", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["numSol"].ToString(), Value = item["numSol"].ToString() });

            }
            return listSelectListItems;
        }

        public List<SelectListItem> BuscarSolicitud(Int64 idLocalidad)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("idlocalidad_In", idLocalidad));


            DataTable dataTable = conexion.RunStoredProcedure("fact_detalle_renta_Get_Clasificacion_By_Localidad", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["numSol"].ToString(), Value = item["numSol"].ToString() });

            }
            return listSelectListItems;
        }



        /// <summary>
        /// 
        /// </summary>
        public List<SelectListItem> Niveles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Detalle"  },
            new SelectListItem { Value = "2", Text = "Edición y detalle" },
            new SelectListItem {Value="3", Text = "Creación, edición, eliminación y detalle" ,},
        };

        public List<SelectListItem> Rol { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "4", Text = "Usuario externo"  },
            new SelectListItem { Value = "3", Text = "Usuario operativo"  },
            new SelectListItem { Value = "2", Text = "Administrador operativo" },
            new SelectListItem {Value="1", Text = "Administrador general" ,},
        };

        public List<SelectListItem> Persona { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="F", Text = "Física" ,},
            new SelectListItem { Value = "M", Text = "Moral" },
        };

        public List<SelectListItem> SiNoSN { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="S", Text = "Si" ,},
            new SelectListItem { Value = "N", Text = "No"  },
        };

        public List<SelectListItem> Bimestres { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="", Text = ""},
            new SelectListItem {Value="1", Text = "1"},
            new SelectListItem {Value="2", Text = "2"},
            new SelectListItem {Value="3", Text = "3"},
            new SelectListItem {Value="4", Text = "4"},
            new SelectListItem {Value="5", Text = "5"},
            new SelectListItem {Value="6", Text = "6"},
        };

        public List<SelectListItem> SiNoTF { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "F", Text = "No"  },
            new SelectListItem {Value="T", Text = "Si" ,},
        };

        public List<SelectListItem> TipoIdentificacion { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="-1", Text = "Selecciona una opción" },
            new SelectListItem {Value="0", Text = "INE" },
            new SelectListItem { Value = "1", Text = "CARTILLA SERVICIO MILITAR"  },
            new SelectListItem { Value = "2", Text = "CEDULA PROFESIONAL"  },
            new SelectListItem { Value = "3", Text = "PASAPORTE"  },
            new SelectListItem { Value = "4", Text = "F1 (EXTRANJEROS)"  },
        };

        public List<SelectListItem> TipoLiquidacion { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="0", Text = "Renta" },
            new SelectListItem { Value = "1", Text = "Servicios"  }
        };

        public List<SelectListItem> TipoFactura { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Adelanto"  },
            new SelectListItem { Value = "2", Text = "Normal"  },
            new SelectListItem { Value = "3", Text = "Nota de crédito"  },
            new SelectListItem { Value = "4", Text = "Complemento"  },
            new SelectListItem { Value = "5", Text = "Sustitución"  },
            new SelectListItem { Value = "6", Text = "Varias Localidades"  },
        };

        public List<SelectListItem> UsuariosProveedorInt(int IdUsuario, int IdCartera)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdUsuario_in", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            DataTable dataTable = conexion.RunStoredProcedure("usuariosGetProv", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Nombre"].ToString(), Value = item["IdUsuario"].ToString() });

            }
            return listSelectListItems;
        }

        public List<SelectListItem> InmueblesUsrProveedorInt(int IdCartera, int IdUsuario, int IdProveedor)
        {
            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("IdProveedor_In", IdProveedor));
            DataTable dataTable = conexion.RunStoredProcedure("InmueblesProveedorInt", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["NombreInmueble"].ToString(), Value = item["IdInmueble"].ToString() });

            }
            return listSelectListItems;
        }

        public List<SelectListItem> GetBancos()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable tb = conexion.RunStoredProcedure("bancosGet", mySqlParameters);
                List<SelectListItem> lstPaises = new List<SelectListItem>();
                foreach (DataRow item in tb.Rows)
                {
                    lstPaises.Add(new SelectListItem { Text = item["Banco"].ToString(), Value = item["Id"].ToString() });
                }
                return lstPaises;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<SelectListItem> MonedaPagoGet(bool Clave = false)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable dataTable = conexion.RunStoredProcedure("tpMonedaPagoGet", mySqlParameters);
                List<SelectListItem> lstMoneda = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstMoneda.Add(new SelectListItem { Text = (Clave) ? item["corto"].ToString() : item["Moneda"].ToString(), Value = item["IdMoneda"].ToString() });
                }

                return lstMoneda;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> CategoriaEgresosGet(int IdInmueble, int Periodo)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("IdInmueble_In", IdInmueble));
                listSqlParameters.Add(new MySqlParameter("IdCategoria_In", 0));
                listSqlParameters.Add(new MySqlParameter("IdSubCatEgreso_In", 0));
                listSqlParameters.Add(new MySqlParameter("Periodo_In", Periodo));
                listSqlParameters.Add(new MySqlParameter("Tipo_In", 2));
                DataTable dataTable = conexion.RunStoredProcedure("MovimientosPartidaPresupuestalObtienePartidas", listSqlParameters);
                List<SelectListItem> lstCatEgreso = new List<SelectListItem>();
                lstCatEgreso.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
                foreach (DataRow item in dataTable.Rows)
                {
                    lstCatEgreso.Add(new SelectListItem { Text = item["CatEgreso"].ToString(), Value = item["IdCategoria"].ToString() });
                }

                return lstCatEgreso;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> SubCategoriaEgresosGet(int IdCatEgreso)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("IdCatEgreso_In", IdCatEgreso));
                DataTable dataTable = conexion.RunStoredProcedure("SubCategoriaEgresosGet", listSqlParameters);
                List<SelectListItem> lstSubCategoria = new List<SelectListItem>();
                lstSubCategoria.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
                foreach (DataRow item in dataTable.Rows)
                {
                    lstSubCategoria.Add(new SelectListItem { Text = item["SubCategoria"].ToString(), Value = item["IdSubCatEgreso"].ToString() });
                }

                return lstSubCategoria;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> GetCgCuentasAgua(int IdUsuario)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
                DataTable dataTable = conexion.RunStoredProcedure("cgcuentasaguaGet", mySqlParameters);

                List<SelectListItem> selectListItem = new List<SelectListItem>();
                selectListItem.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
                foreach (DataRow item in dataTable.Rows)
                {
                    selectListItem.Add(new SelectListItem { Text = item["cuentaAgua"].ToString(), Value = item["idCgCuentaAgua"].ToString() });
                }

                return selectListItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> PeriodosGet()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable dataTable = conexion.RunStoredProcedure("ObtenerPeriodos", mySqlParameters);
                List<SelectListItem> lstCatEgreso = new List<SelectListItem>();
                foreach (DataRow item in dataTable.Rows)
                {
                    lstCatEgreso.Add(new SelectListItem { Text = item["Periodo"].ToString(), Value = item["Periodo"].ToString() });
                }

                return lstCatEgreso;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> PartidaPresGet(int IdInmueble, int IdCategoria, int IdSubCatEgreso, int Periodo)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("IdInmueble_In", IdInmueble));
                listSqlParameters.Add(new MySqlParameter("IdCategoria_In", IdCategoria));
                listSqlParameters.Add(new MySqlParameter("IdSubCatEgreso_In", IdSubCatEgreso));
                listSqlParameters.Add(new MySqlParameter("Periodo_In", Periodo));
                listSqlParameters.Add(new MySqlParameter("Tipo_In", 1));
                DataTable dataTable = conexion.RunStoredProcedure("MovimientosPartidaPresupuestalObtienePartidas", listSqlParameters);
                List<SelectListItem> lstCatEgreso = new List<SelectListItem>();
                lstCatEgreso.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
                foreach (DataRow item in dataTable.Rows)
                {
                    lstCatEgreso.Add(new SelectListItem { Text = item["Partida"].ToString(), Value = item["IdPartidaPresupuestal"].ToString() });
                }

                return lstCatEgreso;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<SelectListItem> CatCategoriaEgresosGet(int IdCatEgreso)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("IdCatEgreso_In", IdCatEgreso));
                DataTable dataTable = conexion.RunStoredProcedure("cat_categoria_egresosGet", mySqlParameters);
                List<SelectListItem> lstCatEgreso = new List<SelectListItem>();
                lstCatEgreso.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
                foreach (DataRow item in dataTable.Rows)
                {
                    lstCatEgreso.Add(new SelectListItem { Text = item["CatEgreso"].ToString(), Value = item["IdCatEgreso"].ToString() });
                }

                return lstCatEgreso;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> EstadosGet()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable dataTable = conexion.RunStoredProcedure("EstadosGet", mySqlParameters);
                List<SelectListItem> lstCatEgreso = new List<SelectListItem>();
                lstCatEgreso.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
                foreach (DataRow item in dataTable.Rows)
                {
                    lstCatEgreso.Add(new SelectListItem { Text = item["Nombre"].ToString(), Value = item["Estado"].ToString() });
                }
                return lstCatEgreso;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<SelectListItem> FormasPagoCapturaMov()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            DataTable dataTable = conexion.RunStoredProcedure("tpformapagoGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataTable.Rows)
            {
                if (item["formapago"].ToString() != "Por definir")
                {
                    var _textItem = item["formapago"].ToString() == "Efectivo" ? item["formapago"].ToString() + " (Caja Chica)" : item["formapago"].ToString();
                    listSelectListItems.Add(new SelectListItem { Text = _textItem, Value = _textItem });
                }
            }
            return listSelectListItems;
        }

        public List<SelectListItem> BuscaProveedorRFC(string rfc)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("rfc_in", rfc));
            DataTable dataTable = conexion.RunStoredProcedure("CatProveedorGastosPorRFC", mySqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Nombre"].ToString(), Value = item["RFC"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> BuscaProveedorInternoPorInmueble(int idInmueble)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("idInmueble_in", idInmueble));
            DataTable dataTable = conexion.RunStoredProcedure("GetproveedorInternoByInmueble", mySqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Nombre"].ToString(), Value = item["IdProveedor"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> BusquedaInicioMovimientoCajaChica(string coincidencia)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("rfc_in", coincidencia));
            DataTable dataTable = conexion.RunStoredProcedure("MovimientosCajaChicaGet.", mySqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Nombre"].ToString(), Value = item["RFC"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> SubCategoriaEgresosPartidaGet(int IdCatEgreso, int IdInmueble, int Periodo)
        {
            try
            {
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("IdInmueble_In", IdInmueble));
                listSqlParameters.Add(new MySqlParameter("IdCategoria_In", IdCatEgreso));
                listSqlParameters.Add(new MySqlParameter("IdSubCatEgreso_In", 0));
                listSqlParameters.Add(new MySqlParameter("Periodo_In", Periodo));
                listSqlParameters.Add(new MySqlParameter("Tipo_In", 3));
                DataTable dataTable = conexion.RunStoredProcedure("MovimientosPartidaPresupuestalObtienePartidas", listSqlParameters);
                List<SelectListItem> lstCatEgreso = new List<SelectListItem>();
                lstCatEgreso.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
                foreach (DataRow item in dataTable.Rows)
                {
                    lstCatEgreso.Add(new SelectListItem { Text = item["SubCategoria"].ToString(), Value = item["IdSubCatEgreso"].ToString() });
                }

                return lstCatEgreso;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> MovimientosPartidaFiltro(int IdCartera, int IdUsuario, int IdInmueble, int IdCategoria, int IdSubCategoria, int IdSolicitante,
                                                             int IdProveedor, int IdPartida, int tipo)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
            listSqlParameters.Add(new MySqlParameter("IdCartera_In", IdCartera));
            listSqlParameters.Add(new MySqlParameter("IdUsuario_In", IdUsuario));
            listSqlParameters.Add(new MySqlParameter("IdInmueble_in", IdInmueble));
            listSqlParameters.Add(new MySqlParameter("IdCategoria_in", IdCategoria));
            listSqlParameters.Add(new MySqlParameter("IdSubCategoria_in", IdSubCategoria));
            listSqlParameters.Add(new MySqlParameter("IdSolicitante_in", IdSolicitante));
            listSqlParameters.Add(new MySqlParameter("IdProveedor_in", IdProveedor));
            listSqlParameters.Add(new MySqlParameter("IdPartida_in", IdPartida));
            listSqlParameters.Add(new MySqlParameter("Tipo_In", tipo));
            DataTable dataTable = conexion.RunStoredProcedure("MovimientosPartidaFiltroGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            listSelectListItems.Add(new SelectListItem { Text = "Selecciona una opción", Value = "0" });
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item[1].ToString(), Value = item[0].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> cambioPagoNI(int idCartera, string tipoPago, int usuarioId = 0)
        {
            List<SelectListItem> sliCambioPago = new List<SelectListItem>();
            switch (idCartera)
            {
                case 2:
                    List<SelectListItem> sliInmuebles = InmueblesByUsuario(13, usuarioId);
                    foreach (SelectListItem lst in sliInmuebles)
                    {
                        if (!lst.Value.Equals("0"))
                        {
                            sliCambioPago.Add(new SelectListItem { Text = "Cambiar a Fibra17 Inmueble: " + lst.Text, Value = "13|" + lst.Value });
                        }
                    }
                    break;

                case 13:
                    sliCambioPago.Add(new SelectListItem { Text = "Cambiar a FibraHD", Value = "2" });
                    break;

                default:
                    break;
            }

            switch (tipoPago)
            {
                case "R":
                    if (idCartera != 13)
                    {
                        sliCambioPago.Add(new SelectListItem { Text = "Establecer cómo Mantenimiento", Value = "M" });
                    }
                    break;
                case "M":
                    sliCambioPago.Add(new SelectListItem { Text = "Establecer cómo Renta", Value = "R" });
                    break;
            }

            return sliCambioPago;
        }

        public List<SelectListItem> PeriodicidadServicios(string Servicio)
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>()
            {
                new ("Servicio_In",Servicio)
            };

            DataTable dataTable = conexion.RunStoredProcedure("b_cg_periodicidad_servicios_Get", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["Periodicidad"].ToString(), Value = item["Id"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> TiposUsoPredial()
        {

            List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();            

            DataTable dataTable = conexion.RunStoredProcedure("cgTipoUsoGet", listSqlParameters);
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (DataRow item in dataTable.Rows)
            {
                listSelectListItems.Add(new SelectListItem { Text = item["TipoUso"].ToString(), Value = item["Id"].ToString() });
            }
            return listSelectListItems;
        }

        public List<SelectListItem> getCamposParametrizadosListContratos { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="1", Text = "Término" },
            new SelectListItem { Value = "2", Text = "Revisión"  }
        };

    }
}
