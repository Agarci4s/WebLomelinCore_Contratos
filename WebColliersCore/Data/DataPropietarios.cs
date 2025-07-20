using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebColliersCore.DataAccess;
using System.Data;
using MySql.Data.MySqlClient;
using WebColliersCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.RegularExpressions;

namespace WebColliersCore.Data
{
    public class DataPropietarios
    {
        private Conexion conexion = new Conexion();
        /// <summary>
        /// Método que regresa todos los paises.
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetPaises()
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                DataTable tb = conexion.RunStoredProcedure("paisesGet", mySqlParameters);
                List<SelectListItem> lstPaises = new List<SelectListItem>();
                foreach (DataRow item in tb.Rows)
                {
                    lstPaises.Add(new SelectListItem { Text = item["Pais"].ToString(), Value = item["IdPais"].ToString() });
                }
                return lstPaises;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que regresa todos los Régimen Fiscales.
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetRegimenFiscal(string TipoPersona)
        {
            try
            {
                List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
                mySqlParameters.Add(new MySqlParameter("Tipo_In", TipoPersona));
                DataTable tb = conexion.RunStoredProcedure("c_regimenfiscal_Get", mySqlParameters);
                List<SelectListItem> lstRegimenFiscal = new List<SelectListItem>();
                foreach (DataRow item in tb.Rows)
                {
                    lstRegimenFiscal.Add(new SelectListItem { Text = item["Regimen"].ToString(), Value = item["idRegimenFiscal"].ToString() });
                }
                return lstRegimenFiscal;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que regresa todos los bancos.
        /// </summary>
        /// <returns></returns>
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
        //TODO: VALIDAR EL ULTIMO PARAMETRO "IDCARTERA" Y CAMBIARLO POR LA VARIABLE QUE SE ASIGNE.
        public DataTable GetPropietarios(string clapro, long idPropietario, int cartera, int IdUsuario)
        {
            var stored = "propietariosGet";
            DataTable tb = new DataTable();
            MySqlParameter mySqlParameter1 = new MySqlParameter("V_OPCION", MySqlDbType.Int32);
            MySqlParameter mySqlParameter2 = new MySqlParameter("V_TIPO_PROPIEDAD", MySqlDbType.VarChar) { Value = "R" };
            MySqlParameter mySqlParameter3 = new MySqlParameter("V_IDPROPIETARIO", MySqlDbType.Int32);
            MySqlParameter mySqlParameter4 = new MySqlParameter("V_CLAPRO", MySqlDbType.Int32);
            MySqlParameter mySqlParameter5 = new MySqlParameter("V_NOMBRE", MySqlDbType.VarChar);
            MySqlParameter mySqlParameter6 = new MySqlParameter("V_CARTERA", MySqlDbType.Int32) { Value = cartera };
            MySqlParameter mySqlParameter7 = new MySqlParameter("V_IDUSUARIO", MySqlDbType.Int32) { Value = IdUsuario };
            if (double.TryParse(clapro, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out double valorClapro))
            {
                mySqlParameter1.Value = 3;
                mySqlParameter3.Value = 0;
                mySqlParameter4.Value = clapro;
                mySqlParameter5.Value = "";
                stored = "propietariosGet";
            }
            else if (clapro.Length > 0)
            {
                mySqlParameter1.Value = 4;
                mySqlParameter3.Value = 0;
                mySqlParameter4.Value = 0;
                mySqlParameter5.Value = "'%" + clapro + "%'";
                stored = "propietariosGetByNombre";
            }
            else if (idPropietario > 0)
            {
                mySqlParameter1.Value = 14;
                mySqlParameter3.Value = idPropietario;
                mySqlParameter4.Value = 0;
                mySqlParameter5.Value = "''";
                stored = "propietariosGet";
            }
            else
            {
                mySqlParameter1.Value = 14;
                mySqlParameter3.Value = -1;
                mySqlParameter4.Value = 0;
                mySqlParameter5.Value = "''";
                stored = "propietariosGet";
            }
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(mySqlParameter1);
            mySqlParameters.Add(mySqlParameter2);
            mySqlParameters.Add(mySqlParameter3);
            mySqlParameters.Add(mySqlParameter4);
            mySqlParameters.Add(mySqlParameter5);
            mySqlParameters.Add(mySqlParameter6);
            mySqlParameters.Add(mySqlParameter7);
            tb = conexion.RunStoredProcedure(stored, mySqlParameters);            
            
            return tb;
        }

        public List<CgPropietarios> GetListaPropietarios(string clapro, long idPropietario, int cartera, int IdUsuario)
        {
            DataTable tableProp = GetPropietarios((clapro == "" ? "" : clapro), idPropietario, cartera, IdUsuario);
            //MySqlDataReader item = GetPropietarios((clapro == "" ? "" : clapro), idPropietario, cartera);
            List<CgPropietarios> lstPropietarios = new List<CgPropietarios>();
            
            foreach (DataRow item in tableProp.Rows)
            {
                CgPropietarios propietario = new CgPropietarios();

                propietario.IdPropietario = int.Parse(item["IDPROPIETARIO"].ToString());
                propietario.Paterno = item["PATERNO"].ToString();
                propietario.Materno = item["MATERNO"].ToString();
                propietario.Nombres = item["NOMBRES"].ToString();
                propietario.Nombre = item["NOMBRE"].ToString();
                propietario.Domicilio = item["DOMICILIO"].ToString();
                propietario.Exterior = item["exterior"].ToString();
                propietario.Interior = item["interior"].ToString();
                propietario.Colonia = item["COLONIA"].ToString();
                propietario.Delegacion = item["DELEGACION"].ToString();
                propietario.Cp = item["CP"].ToString();
                propietario.Ciudad = item["CIUDAD"].ToString();
                propietario.Estado = item["Estado"].ToString();
                propietario.Pais = item["PAIS"].ToString();
                propietario.Telefono1 = item["TELEFONO1"].ToString();
                propietario.Telefono2 = item["TELEFONO2"].ToString();
                propietario.FAX = item["FAX"].ToString();
                propietario.Celular = item["CELULAR"].ToString();
                propietario.EMAIL = item["EMAIL"].ToString();
                propietario.Fis_Mor = item["FIS_MOR"].ToString();
                propietario.RFC = item["RFC"].ToString();
                propietario.CURP = item["CURP"].ToString();
                propietario.DomFis = item["DOMFIS"].ToString();
                propietario.ExteriorFis = item["ExteriorFis"].ToString();
                propietario.InteriorFis = item["InteriorFis"].ToString();
                propietario.ColFis = item["COLFIS"].ToString();
                propietario.CPFis = item["CPFIS"].ToString();
                propietario.TelFis = item["TELFIS"].ToString();
                propietario.DelFis = item["DelFis"].ToString();
                propietario.CiudadFis = item["CiudadFis"].ToString();
                propietario.EstadoFis = item["EstadoFis"].ToString();
                propietario.ATTCamb = item["ATTCAMB"].ToString();
                propietario.EmiEti = item["EMIETI"].ToString();
                propietario.Observaciones = item["OBSERVACIONES"].ToString();
                propietario.FechaIngreso = Convert.ToDateTime(item["FECHAINGRESO"].ToString());
                propietario.FechaBaja = Convert.ToDateTime(item["FECHABAJA"].ToString());
                propietario.CveBan = int.Parse(item["CVEBAN"].ToString());
                propietario.CtaDep = item["CTADEP"].ToString();
                propietario.SucDep = item["SUCDEP"].ToString();
                propietario.AnteNombre = item["ANTENOMBRE"].ToString();
                propietario.PropietarioDos = item["PROPIETARIODOS"].ToString();
                propietario.NombreRep = item["NombreRep"].ToString();
                propietario.PaternoRep = item["PaternoRep"].ToString();
                propietario.MaternoRep = item["MaternoRep"].ToString();
                propietario.RFCRep = item["RFCRep"].ToString();
                propietario.CURPRep = item["CURPRep"].ToString();
                propietario.FechaConstitucion = Convert.ToDateTime(item["FechaConstitucion"].ToString());
                propietario.TipIdentMoral = item["TipIdentMoral"].ToString();
                propietario.TipIdentOtroMoral = item["TipIdentOtroMoral"].ToString();
                propietario.EmiteIdentMoral = item["EmiteIdentMoral"].ToString();
                propietario.NumIdentMoral = item["NumIdentMoral"].ToString();
                propietario.TipIdentFisica = item["TipIdentFisica"].ToString();
                propietario.TipIdentOtroFisica = item["TipIdentOtroFisica"].ToString();
                propietario.EmiteIdentFisica = item["EmiteIdentFisica"].ToString();
                propietario.NumIdentFisica = item["NumIdentFisica"].ToString();
                propietario.Nacionalidad = int.Parse(item["Nacionalidad"].ToString());
                propietario.FechaPoder = Convert.ToDateTime(item["FechaPoder"].ToString());
                propietario.NumeroInstrumento = item["NumeroInstrumento"].ToString();
                propietario.NumeroNotaria = int.Parse(item["NumeroNotaria"].ToString());
                propietario.EstadoNotaria = item["EstadoNotaria"].ToString();
                propietario.Fedatario = item["Fedatario"].ToString();
                propietario.NumeroEscritura = tableProp.Rows[0]["NumeroEscritura"].ToString();
                propietario.NumeroLibro = tableProp.Rows[0]["NumeroLibro"].ToString();
                propietario.Volumen = tableProp.Rows[0]["Volumen"].ToString();
                propietario.FechaPoderNotarial = Convert.ToDateTime(tableProp.Rows[0]["FechaPoderNotarial"].ToString());
                propietario.NombreNotario = tableProp.Rows[0]["NombreNotario"].ToString();
                propietario.NumeroNotario = tableProp.Rows[0]["NumeroNotario"].ToString();
                propietario.EntidadFederativaNotario = tableProp.Rows[0]["EntidadFederativaNotario"].ToString();
                propietario.Fojas = tableProp.Rows[0]["Fojas"].ToString();
                propietario.NombreApoderado = tableProp.Rows[0]["NombreApoderado"].ToString();
                lstPropietarios.Add(propietario);
            }
            return lstPropietarios;
        }

        public CgPropietarios GetPropietarioById(long id, int cartera, int IdUsuario)
        {
            DataTable tableProp = GetPropietarios("", id, cartera, IdUsuario);            
            CgPropietarios propietario = new CgPropietarios();

            propietario.IdPropietario = id;
            propietario.Paterno = tableProp.Rows[0]["PATERNO"].ToString();
            propietario.Materno = tableProp.Rows[0]["MATERNO"].ToString();
            propietario.Nombres = tableProp.Rows[0]["NOMBRES"].ToString();
            propietario.Nombre = tableProp.Rows[0]["NOMBRE"].ToString();
            propietario.Domicilio = tableProp.Rows[0]["DOMICILIO"].ToString();
            propietario.Exterior = tableProp.Rows[0]["exterior"].ToString();
            propietario.Interior = tableProp.Rows[0]["interior"].ToString();
            propietario.Colonia = tableProp.Rows[0]["COLONIA"].ToString();
            propietario.Delegacion = tableProp.Rows[0]["DELEGACION"].ToString();
            propietario.Cp = tableProp.Rows[0]["CP"].ToString();
            propietario.Ciudad = tableProp.Rows[0]["CIUDAD"].ToString();
            propietario.Estado = tableProp.Rows[0]["Estado"].ToString();
            propietario.Pais = tableProp.Rows[0]["PAIS"].ToString();
            propietario.Telefono1 = tableProp.Rows[0]["TELEFONO1"].ToString();
            propietario.Telefono2 = tableProp.Rows[0]["TELEFONO2"].ToString();
            propietario.FAX = tableProp.Rows[0]["FAX"].ToString();
            propietario.Celular = tableProp.Rows[0]["CELULAR"].ToString();
            propietario.EMAIL = tableProp.Rows[0]["EMAIL"].ToString();
            propietario.Fis_Mor = tableProp.Rows[0]["FIS_MOR"].ToString();
            propietario.RFC = tableProp.Rows[0]["RFC"].ToString();
            propietario.CURP = tableProp.Rows[0]["CURP"].ToString();
            propietario.DomFis = tableProp.Rows[0]["DOMFIS"].ToString();
            propietario.ExteriorFis = tableProp.Rows[0]["ExteriorFis"].ToString();
            propietario.InteriorFis = tableProp.Rows[0]["InteriorFis"].ToString();
            propietario.ColFis = tableProp.Rows[0]["COLFIS"].ToString();
            propietario.CPFis = tableProp.Rows[0]["CPFIS"].ToString();
            propietario.TelFis = tableProp.Rows[0]["TELFIS"].ToString();
            propietario.DelFis = tableProp.Rows[0]["DelFis"].ToString();
            propietario.CiudadFis = tableProp.Rows[0]["CiudadFis"].ToString();
            propietario.EstadoFis = tableProp.Rows[0]["EstadoFis"].ToString();
            propietario.ATTCamb = tableProp.Rows[0]["ATTCAMB"].ToString();
            propietario.EmiEti = tableProp.Rows[0]["EMIETI"].ToString();
            propietario.Observaciones = tableProp.Rows[0]["OBSERVACIONES"].ToString();
            propietario.FechaIngreso = Convert.ToDateTime(tableProp.Rows[0]["FECHAINGRESO"].ToString());
            propietario.FechaBaja = Convert.ToDateTime(tableProp.Rows[0]["FECHABAJA"].ToString());
            propietario.CveBan = int.Parse(tableProp.Rows[0]["CVEBAN"].ToString());
            propietario.CtaDep = tableProp.Rows[0]["CTADEP"].ToString();
            propietario.SucDep = tableProp.Rows[0]["SUCDEP"].ToString();
            propietario.AnteNombre = tableProp.Rows[0]["ANTENOMBRE"].ToString();
            propietario.PropietarioDos = tableProp.Rows[0]["PROPIETARIODOS"].ToString();
            propietario.NombreRep = tableProp.Rows[0]["NombreRep"].ToString();
            propietario.PaternoRep = tableProp.Rows[0]["PaternoRep"].ToString();
            propietario.MaternoRep = tableProp.Rows[0]["MaternoRep"].ToString();
            propietario.RFCRep = tableProp.Rows[0]["RFCRep"].ToString();
            propietario.CURPRep = tableProp.Rows[0]["CURPRep"].ToString();
            propietario.FechaConstitucion = Convert.ToDateTime(tableProp.Rows[0]["FechaConstitucion"].ToString());
            propietario.TipoIdentificacionRepresentanteAux = tableProp.Rows[0]["TipIdentMoral"].ToString();
            propietario.TipIdentOtroMoral = tableProp.Rows[0]["TipIdentOtroMoral"].ToString();
            propietario.EmiteIdentMoral = TipoIndentificacion(int.Parse(tableProp.Rows[0]["TipIdentMoral"].ToString() == "" ? "-1" : tableProp.Rows[0]["TipIdentMoral"].ToString()));
            propietario.NumeroIdentificacionRepresentanteAux = tableProp.Rows[0]["NumIdentMoral"].ToString();
            propietario.TipoIdentificacionAux = tableProp.Rows[0]["TipIdentFisica"].ToString();
            propietario.TipIdentOtroFisica = tableProp.Rows[0]["TipIdentOtroFisica"].ToString();          
            propietario.EmiteIdentFisica = TipoIndentificacion(int.Parse(tableProp.Rows[0]["TipIdentFisica"].ToString() == "" ? "-1" : tableProp.Rows[0]["TipIdentFisica"].ToString()));
            propietario.NumeroIdentificacionAux = tableProp.Rows[0]["NumIdentFisica"].ToString();
            propietario.Nacionalidad = int.Parse(tableProp.Rows[0]["Nacionalidad"].ToString());
            propietario.FechaPoder = Convert.ToDateTime(tableProp.Rows[0]["FechaPoder"].ToString());
            propietario.NumeroInstrumento = tableProp.Rows[0]["NumeroInstrumento"].ToString();
            propietario.NumeroNotaria = int.Parse(tableProp.Rows[0]["NumeroNotaria"].ToString());
            propietario.EstadoNotaria = tableProp.Rows[0]["EstadoNotaria"].ToString();
            propietario.Fedatario = tableProp.Rows[0]["Fedatario"].ToString();
            propietario.Banco = tableProp.Rows[0]["Banco"].ToString();
            propietario.NacionalidadP = tableProp.Rows[0]["NacionalidadP"].ToString();

            propietario.NumeroEscritura = tableProp.Rows[0]["NumeroEscritura"].ToString();
            propietario.NumeroLibro = tableProp.Rows[0]["NumeroLibro"].ToString();
            propietario.Volumen = tableProp.Rows[0]["Volumen"].ToString();
            propietario.FechaPoderNotarial = Convert.ToDateTime(tableProp.Rows[0]["FechaPoderNotarial"].ToString());
            propietario.NombreNotario = tableProp.Rows[0]["NombreNotario"].ToString();
            propietario.NumeroNotario = tableProp.Rows[0]["NumeroNotario"].ToString();
            propietario.EntidadFederativaNotario = tableProp.Rows[0]["EntidadFederativaNotario"].ToString();
            propietario.Fojas = tableProp.Rows[0]["Fojas"].ToString();
            propietario.NombreApoderado = tableProp.Rows[0]["NombreApoderado"].ToString();

            propietario.idRegimenFiscal = int.Parse(tableProp.Rows[0]["idRegimenFiscal"].ToString());
            propietario.RegimenFiscal = tableProp.Rows[0]["RegimenFiscal"].ToString();


            return propietario;
        }

        private string TipoIndentificacion(int id)
        {
            string cadena = "N/A";
            switch (id)
            {
                case -1:
                    cadena = "N/A";
                    break;
                case 0:
                    cadena = "INE";
                    break;
                case 1:
                    cadena = "CARTILLA SERVICIO MILITAR";
                    break;
                case 2:
                    cadena = "CEDULA PROFESIONAL";
                    break;
                case 3:
                    cadena = "PASAPORTE";
                    break;
                case 4:
                    cadena = "F1 (EXTRANJEROS)";
                    break;
            }
            return cadena;
        }

        /// <summary>
        /// 1: Error en RFC del Propietario Persona Fisica.
        /// 2: Error en datos faltantes de nombre, apellido paterno o apellido materno.
        /// 3: Error en RFC del Representante Legal cuando es Persona Moral.
        /// 4: Error en RFC del Propietario persona moral.
        /// 5: Error en Razon Social cuando es persona moral.
        /// </summary>
        public int TipoError { get; set; }

        private bool IsValid(CgPropietarios cgPropietarios)
        {
            try
            {
                if (cgPropietarios.Fis_Mor == "F")
                {
                    if (cgPropietarios.Nombres != null & cgPropietarios.Paterno != null)
                    {
                        if (RFCValido(cgPropietarios.RFC, cgPropietarios.Fis_Mor))
                        {
                            if (cgPropietarios.idRegimenFiscal == -1)
                            {
                                TipoError = 6;
                                return false;
                            }
                            else
                            {
                                return true;
                            }                            
                        }
                        else
                        {
                            TipoError = 1;
                            return false;
                        }
                    }
                    else
                    {
                        TipoError = 2;
                        return false;
                    }
                }
                else
                {
                    if (cgPropietarios.Nombre != null)
                    {
                        if (RFCValido(cgPropietarios.RFC, cgPropietarios.Fis_Mor))
                        {
                            if (cgPropietarios.RFCRep != null)
                            {
                                if (cgPropietarios.RFCRep.ToString().Length > 0)
                                {
                                    if (RFCValido(cgPropietarios.RFCRep, "F"))
                                    {
                                        if (cgPropietarios.idRegimenFiscal == -1)
                                        {
                                            TipoError = 6;
                                            return false;
                                        }
                                        else
                                        {
                                            return true;
                                        }                                        
                                    }
                                    else
                                    {
                                        TipoError = 3;
                                        return false;
                                    }
                                }
                                else
                                {
                                    if (cgPropietarios.idRegimenFiscal == -1)
                                    {
                                        TipoError = 6;
                                        return false;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }                                    
                            }
                            else
                            {
                                if (cgPropietarios.idRegimenFiscal == -1)
                                {
                                    TipoError = 6;
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }                                
                        }
                        else
                        {
                            TipoError = 4;
                            return false;
                        }
                    }
                    else
                    {
                        TipoError = 5;
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Boolean Crear(CgPropietarios propietario)
        {
            if (!IsValid(propietario))
                return false;
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("Operacion_In", MySqlDbType.Enum) { Value = "C" });
            mySqlParameters.Add(new MySqlParameter("IdPropietario_In", MySqlDbType.Int64, 20) { Value = propietario.IdPropietario });
            mySqlParameters.Add(new MySqlParameter("Paterno_In", MySqlDbType.VarChar, 50) { Value = (string.IsNullOrEmpty(propietario.Paterno)) ? string.Empty : propietario.Paterno });
            mySqlParameters.Add(new MySqlParameter("Materno_In", MySqlDbType.VarChar, 50) { Value = (string.IsNullOrEmpty(propietario.Materno)) ? string.Empty : propietario.Materno });
            mySqlParameters.Add(new MySqlParameter("Nombres_In", MySqlDbType.VarChar, 50) { Value = (string.IsNullOrEmpty(propietario.Nombres)) ? string.Empty : propietario.Nombres });
            mySqlParameters.Add(new MySqlParameter("Nombre_In", MySqlDbType.VarChar, 220) { Value = (propietario.Fis_Mor == "F") ? (propietario.Nombres + " " + propietario.Paterno + " " + propietario.Materno) : propietario.Nombre });
            mySqlParameters.Add(new MySqlParameter("Domicilio_In", MySqlDbType.VarChar, 60) { Value = propietario.Domicilio });
            mySqlParameters.Add(new MySqlParameter("Exterior_In", MySqlDbType.VarChar, 50) { Value = propietario.Exterior });
            mySqlParameters.Add(new MySqlParameter("Interior_In", MySqlDbType.VarChar, 50) { Value = propietario.Interior });
            mySqlParameters.Add(new MySqlParameter("Colonia_In", MySqlDbType.VarChar, 60) { Value = propietario.Colonia });
            mySqlParameters.Add(new MySqlParameter("Delegacion_In", MySqlDbType.VarChar, 30) { Value = propietario.Delegacion });
            mySqlParameters.Add(new MySqlParameter("Cp_In", MySqlDbType.VarChar, 5) { Value = propietario.Cp });
            mySqlParameters.Add(new MySqlParameter("Ciudad_In", MySqlDbType.VarChar, 50) { Value = propietario.Ciudad });
            mySqlParameters.Add(new MySqlParameter("Estado_In", MySqlDbType.VarChar, 30) { Value = propietario.Estado });
            mySqlParameters.Add(new MySqlParameter("Pais_In", MySqlDbType.VarChar, 60) { Value = propietario.Pais });
            mySqlParameters.Add(new MySqlParameter("Telefono1_In", MySqlDbType.VarChar, 20) { Value = propietario.Telefono1 });
            mySqlParameters.Add(new MySqlParameter("Telefono2_In", MySqlDbType.VarChar, 20) { Value = propietario.Telefono2 });
            mySqlParameters.Add(new MySqlParameter("FAX_In", MySqlDbType.VarChar, 20) { Value = propietario.FAX });
            mySqlParameters.Add(new MySqlParameter("Celular_In", MySqlDbType.VarChar, 50) { Value = propietario.Celular });
            mySqlParameters.Add(new MySqlParameter("EMAIL_In", MySqlDbType.VarChar, 50) { Value = propietario.EMAIL });
            mySqlParameters.Add(new MySqlParameter("Fis_Mor_In", MySqlDbType.VarChar, 1) { Value = propietario.Fis_Mor });
            mySqlParameters.Add(new MySqlParameter("RFC_In", MySqlDbType.VarChar, 20) { Value = propietario.RFC });
            mySqlParameters.Add(new MySqlParameter("CURP_In", MySqlDbType.VarChar, 50) { Value = propietario.CURP });
            mySqlParameters.Add(new MySqlParameter("DomFis_In", MySqlDbType.VarChar, 60) { Value = propietario.DomFis });
            mySqlParameters.Add(new MySqlParameter("ExteriorFis_In", MySqlDbType.VarChar, 30) { Value = propietario.ExteriorFis });
            mySqlParameters.Add(new MySqlParameter("InteriorFis_In", MySqlDbType.VarChar, 30) { Value = propietario.InteriorFis });
            mySqlParameters.Add(new MySqlParameter("ColFis_In", MySqlDbType.VarChar, 50) { Value = propietario.ColFis });
            mySqlParameters.Add(new MySqlParameter("CPFis_In", MySqlDbType.VarChar, 5) { Value = propietario.CPFis });
            mySqlParameters.Add(new MySqlParameter("TelFis_In", MySqlDbType.VarChar, 20) { Value = propietario.TelFis });
            mySqlParameters.Add(new MySqlParameter("DelFis_In", MySqlDbType.VarChar, 50) { Value = propietario.DelFis });
            mySqlParameters.Add(new MySqlParameter("CiudadFis_In", MySqlDbType.VarChar, 50) { Value = propietario.CiudadFis });
            mySqlParameters.Add(new MySqlParameter("EstadoFis_In", MySqlDbType.VarChar, 50) { Value = propietario.EstadoFis });
            mySqlParameters.Add(new MySqlParameter("ATTCamb_In", MySqlDbType.VarChar, 1) { Value = propietario.ATTCamb });
            mySqlParameters.Add(new MySqlParameter("EmiEti_In", MySqlDbType.VarChar, 1) { Value = propietario.EmiEti });
            mySqlParameters.Add(new MySqlParameter("Observaciones_In", MySqlDbType.VarString) { Value = propietario.Observaciones });
            mySqlParameters.Add(new MySqlParameter("FechaIngreso_In", MySqlDbType.Date) { Value = String.Format("{0:yyyy-MM-dd}", DateTime.Now) });
            mySqlParameters.Add(new MySqlParameter("FechaBaja_In", MySqlDbType.Date) { Value = "1900-01-01" });
            mySqlParameters.Add(new MySqlParameter("CveBan_In", MySqlDbType.Int32) { Value = propietario.CveBan });
            mySqlParameters.Add(new MySqlParameter("CtaDep_In", MySqlDbType.VarChar, 30) { Value = propietario.CtaDep });
            mySqlParameters.Add(new MySqlParameter("SucDep_In", MySqlDbType.VarChar, 30) { Value = propietario.SucDep });
            mySqlParameters.Add(new MySqlParameter("AnteNombre_In", MySqlDbType.VarChar, 25) { Value = propietario.AnteNombre });
            mySqlParameters.Add(new MySqlParameter("PropietarioDos_In", MySqlDbType.VarChar, 100) { Value = propietario.PropietarioDos });
            mySqlParameters.Add(new MySqlParameter("NombreRep_In", MySqlDbType.VarChar, 50) { Value = propietario.NombreRep });
            mySqlParameters.Add(new MySqlParameter("PaternoRep_In", MySqlDbType.VarChar, 50) { Value = propietario.PaternoRep });
            mySqlParameters.Add(new MySqlParameter("MaternoRep_In", MySqlDbType.VarChar, 50) { Value = propietario.MaternoRep });
            mySqlParameters.Add(new MySqlParameter("RFCRep_In", MySqlDbType.VarChar, 15) { Value = propietario.RFCRep });
            mySqlParameters.Add(new MySqlParameter("CURPRep_In", MySqlDbType.VarChar, 50) { Value = propietario.CURPRep });
            mySqlParameters.Add(new MySqlParameter("FechaConstitucion_In", MySqlDbType.Date) { Value = propietario.FechaConstitucion });
            mySqlParameters.Add(new MySqlParameter("TipIdentMoral_In", MySqlDbType.VarChar, 2) { Value = propietario.TipoIdentificacionRepresentanteAux /*propietario.TipIdentMoral*/ });
            mySqlParameters.Add(new MySqlParameter("TipIdentOtroMoral_In", MySqlDbType.VarChar, 200) { Value = propietario.TipIdentOtroMoral });
            mySqlParameters.Add(new MySqlParameter("EmiteIdentMoral_In", MySqlDbType.VarChar, 200) { Value = propietario.EmiteIdentMoral /*propietario.EmiteIdentMoral*/ });
            mySqlParameters.Add(new MySqlParameter("NumIdentMoral_In", MySqlDbType.VarChar, 40) { Value = propietario.NumeroIdentificacionRepresentanteAux });
            mySqlParameters.Add(new MySqlParameter("TipIdentFisica_In", MySqlDbType.VarChar, 2) { Value = propietario.TipoIdentificacionAux /*propietario.TipIdentFisica*/ });
            mySqlParameters.Add(new MySqlParameter("TipIdentOtroFisica_In", MySqlDbType.VarChar, 200) { Value = propietario.TipIdentOtroFisica });
            mySqlParameters.Add(new MySqlParameter("EmiteIdentFisica_In", MySqlDbType.VarChar, 200) { Value = propietario.EmiteIdentFisica });
            mySqlParameters.Add(new MySqlParameter("NumIdentFisica_In", MySqlDbType.VarChar, 40) { Value = propietario.NumeroIdentificacionAux /*propietario.NumIdentFisica*/ });
            mySqlParameters.Add(new MySqlParameter("Nacionalidad_In", MySqlDbType.Int32) { Value = propietario.Nacionalidad });
            mySqlParameters.Add(new MySqlParameter("FechaPoder_In", MySqlDbType.Date) { Value = propietario.FechaPoder });
            mySqlParameters.Add(new MySqlParameter("NumeroInstrumento_In", MySqlDbType.VarChar, 50) { Value = propietario.NumeroInstrumento });
            mySqlParameters.Add(new MySqlParameter("NumeroNotaria_In", MySqlDbType.Int32) { Value = propietario.NumeroNotaria });
            mySqlParameters.Add(new MySqlParameter("EstadoNotaria_In", MySqlDbType.VarChar, 70) { Value = propietario.EstadoNotaria });
            mySqlParameters.Add(new MySqlParameter("Fedatario_In", MySqlDbType.VarChar, 100) { Value = propietario.Fedatario });

            mySqlParameters.Add(new MySqlParameter("NumeroEscritura_In", MySqlDbType.VarChar, 15) { Value = propietario.NumeroEscritura });
            mySqlParameters.Add(new MySqlParameter("NumeroLibro_In", MySqlDbType.VarChar, 5) { Value = propietario.NumeroLibro });
            mySqlParameters.Add(new MySqlParameter("Volumen_In", MySqlDbType.VarChar, 5) { Value = propietario.Volumen });
            mySqlParameters.Add(new MySqlParameter("FechaPoderNotarial_In", MySqlDbType.Date) { Value = propietario.FechaPoderNotarial });
            mySqlParameters.Add(new MySqlParameter("NombreNotario_In", MySqlDbType.VarChar, 100) { Value = propietario.NombreNotario });
            mySqlParameters.Add(new MySqlParameter("NumeroNotario_In", MySqlDbType.VarChar, 5) { Value = propietario.NumeroNotario });
            mySqlParameters.Add(new MySqlParameter("EntidadFederativaNotario_In", MySqlDbType.VarChar, 15) { Value = propietario.EntidadFederativaNotario });
            mySqlParameters.Add(new MySqlParameter("Fojas_In", MySqlDbType.VarChar, 5) { Value = propietario.Fojas });
            mySqlParameters.Add(new MySqlParameter("NombreApoderado_In", MySqlDbType.VarChar, 100) { Value = propietario.NombreApoderado });

            mySqlParameters.Add(new MySqlParameter("Usuario_In", MySqlDbType.VarChar, 45) { Value = "" });
            mySqlParameters.Add(new MySqlParameter("Maquina_In", MySqlDbType.VarChar, 45) { Value = Environment.MachineName });
            mySqlParameters.Add(new MySqlParameter("DireccionIp_In", MySqlDbType.VarChar, 45) { Value = Dns.GetHostEntry(Dns.GetHostName()) });

            mySqlParameters.Add(new MySqlParameter("idRegimenFiscal_In", propietario.idRegimenFiscal));

            conexion.RunStoredProcedure("CRUD_Propietarios", mySqlParameters);

            return true;
        }

        public bool Actualizar(int id, CgPropietarios propietario)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("Operacion_In", MySqlDbType.Enum) { Value = "U" });
            mySqlParameters.Add(new MySqlParameter("IdPropietario_In", MySqlDbType.Int64, 20) { Value = id });
            mySqlParameters.Add(new MySqlParameter("Paterno_In", MySqlDbType.VarChar, 50) { Value = propietario.Paterno });
            mySqlParameters.Add(new MySqlParameter("Materno_In", MySqlDbType.VarChar, 50) { Value = propietario.Materno });
            mySqlParameters.Add(new MySqlParameter("Nombres_In", MySqlDbType.VarChar, 50) { Value = propietario.Nombres });
            mySqlParameters.Add(new MySqlParameter("Nombre_In", MySqlDbType.VarChar, 220) { Value = (propietario.Fis_Mor == "F") ? (propietario.Nombres + " " + propietario.Paterno + " " + propietario.Materno) : propietario.Nombre });
            mySqlParameters.Add(new MySqlParameter("Domicilio_In", MySqlDbType.VarChar, 60) { Value = propietario.Domicilio });
            mySqlParameters.Add(new MySqlParameter("Exterior_In", MySqlDbType.VarChar, 50) { Value = propietario.Exterior });
            mySqlParameters.Add(new MySqlParameter("Interior_In", MySqlDbType.VarChar, 50) { Value = propietario.Interior });
            mySqlParameters.Add(new MySqlParameter("Colonia_In", MySqlDbType.VarChar, 60) { Value = propietario.Colonia });
            mySqlParameters.Add(new MySqlParameter("Delegacion_In", MySqlDbType.VarChar, 30) { Value = propietario.Delegacion });
            mySqlParameters.Add(new MySqlParameter("Cp_In", MySqlDbType.VarChar, 5) { Value = propietario.Cp });
            mySqlParameters.Add(new MySqlParameter("Ciudad_In", MySqlDbType.VarChar, 50) { Value = propietario.Ciudad });
            mySqlParameters.Add(new MySqlParameter("Estado_In", MySqlDbType.VarChar, 30) { Value = propietario.Estado });
            mySqlParameters.Add(new MySqlParameter("Pais_In", MySqlDbType.VarChar, 60) { Value = propietario.Pais });
            mySqlParameters.Add(new MySqlParameter("Telefono1_In", MySqlDbType.VarChar, 20) { Value = propietario.Telefono1 });
            mySqlParameters.Add(new MySqlParameter("Telefono2_In", MySqlDbType.VarChar, 20) { Value = propietario.Telefono2 });
            mySqlParameters.Add(new MySqlParameter("FAX_In", MySqlDbType.VarChar, 20) { Value = propietario.FAX });
            mySqlParameters.Add(new MySqlParameter("Celular_In", MySqlDbType.VarChar, 50) { Value = propietario.Celular });
            mySqlParameters.Add(new MySqlParameter("EMAIL_In", MySqlDbType.VarChar, 50) { Value = propietario.EMAIL });
            mySqlParameters.Add(new MySqlParameter("Fis_Mor_In", MySqlDbType.VarChar, 1) { Value = propietario.Fis_Mor });
            mySqlParameters.Add(new MySqlParameter("RFC_In", MySqlDbType.VarChar, 20) { Value = propietario.RFC });
            mySqlParameters.Add(new MySqlParameter("CURP_In", MySqlDbType.VarChar, 50) { Value = propietario.CURP });
            mySqlParameters.Add(new MySqlParameter("DomFis_In", MySqlDbType.VarChar, 60) { Value = propietario.DomFis });
            mySqlParameters.Add(new MySqlParameter("ExteriorFis_In", MySqlDbType.VarChar, 30) { Value = propietario.ExteriorFis });
            mySqlParameters.Add(new MySqlParameter("InteriorFis_In", MySqlDbType.VarChar, 30) { Value = propietario.InteriorFis });
            mySqlParameters.Add(new MySqlParameter("ColFis_In", MySqlDbType.VarChar, 50) { Value = propietario.ColFis });
            mySqlParameters.Add(new MySqlParameter("CPFis_In", MySqlDbType.VarChar, 5) { Value = propietario.CPFis });
            mySqlParameters.Add(new MySqlParameter("TelFis_In", MySqlDbType.VarChar, 20) { Value = propietario.TelFis });
            mySqlParameters.Add(new MySqlParameter("DelFis_In", MySqlDbType.VarChar, 50) { Value = propietario.DelFis });
            mySqlParameters.Add(new MySqlParameter("CiudadFis_In", MySqlDbType.VarChar, 50) { Value = propietario.CiudadFis });
            mySqlParameters.Add(new MySqlParameter("EstadoFis_In", MySqlDbType.VarChar, 50) { Value = propietario.EstadoFis });
            mySqlParameters.Add(new MySqlParameter("ATTCamb_In", MySqlDbType.VarChar, 1) { Value = propietario.ATTCamb });
            mySqlParameters.Add(new MySqlParameter("EmiEti_In", MySqlDbType.VarChar, 1) { Value = propietario.EmiEti });
            mySqlParameters.Add(new MySqlParameter("Observaciones_In", MySqlDbType.VarString) { Value = propietario.Observaciones });
            mySqlParameters.Add(new MySqlParameter("FechaIngreso_In", MySqlDbType.Date) { Value = propietario.FechaIngreso });
            mySqlParameters.Add(new MySqlParameter("FechaBaja_In", MySqlDbType.Date) { Value = propietario.FechaBaja });
            mySqlParameters.Add(new MySqlParameter("CveBan_In", MySqlDbType.Int32) { Value = propietario.CveBan });
            mySqlParameters.Add(new MySqlParameter("CtaDep_In", MySqlDbType.VarChar, 30) { Value = propietario.CtaDep });
            mySqlParameters.Add(new MySqlParameter("SucDep_In", MySqlDbType.VarChar, 30) { Value = propietario.SucDep });
            mySqlParameters.Add(new MySqlParameter("AnteNombre_In", MySqlDbType.VarChar, 25) { Value = propietario.AnteNombre });
            mySqlParameters.Add(new MySqlParameter("PropietarioDos_In", MySqlDbType.VarChar, 100) { Value = propietario.PropietarioDos });
            mySqlParameters.Add(new MySqlParameter("NombreRep_In", MySqlDbType.VarChar, 50) { Value = propietario.NombreRep });
            mySqlParameters.Add(new MySqlParameter("PaternoRep_In", MySqlDbType.VarChar, 50) { Value = propietario.PaternoRep });
            mySqlParameters.Add(new MySqlParameter("MaternoRep_In", MySqlDbType.VarChar, 50) { Value = propietario.MaternoRep });
            mySqlParameters.Add(new MySqlParameter("RFCRep_In", MySqlDbType.VarChar, 15) { Value = propietario.RFCRep });
            mySqlParameters.Add(new MySqlParameter("CURPRep_In", MySqlDbType.VarChar, 50) { Value = propietario.CURPRep });
            mySqlParameters.Add(new MySqlParameter("FechaConstitucion_In", MySqlDbType.Date) { Value = propietario.FechaConstitucion });
            mySqlParameters.Add(new MySqlParameter("TipIdentMoral_In", MySqlDbType.VarChar, 2) { Value = propietario.TipoIdentificacionRepresentanteAux });
            mySqlParameters.Add(new MySqlParameter("TipIdentOtroMoral_In", MySqlDbType.VarChar, 200) { Value = propietario.TipIdentOtroMoral });
            mySqlParameters.Add(new MySqlParameter("EmiteIdentMoral_In", MySqlDbType.VarChar, 200) { Value = propietario.EmiteIdentMoral });
            mySqlParameters.Add(new MySqlParameter("NumIdentMoral_In", MySqlDbType.VarChar, 40) { Value = propietario.NumeroIdentificacionRepresentanteAux });
            mySqlParameters.Add(new MySqlParameter("TipIdentFisica_In", MySqlDbType.VarChar, 2) { Value = propietario.TipoIdentificacionAux });
            mySqlParameters.Add(new MySqlParameter("TipIdentOtroFisica_In", MySqlDbType.VarChar, 200) { Value = propietario.TipIdentOtroFisica });
            mySqlParameters.Add(new MySqlParameter("EmiteIdentFisica_In", MySqlDbType.VarChar, 200) { Value = propietario.EmiteIdentFisica });
            mySqlParameters.Add(new MySqlParameter("NumIdentFisica_In", MySqlDbType.VarChar, 40) { Value = propietario.NumeroIdentificacionAux });
            mySqlParameters.Add(new MySqlParameter("Nacionalidad_In", MySqlDbType.Int32) { Value = propietario.Nacionalidad });
            mySqlParameters.Add(new MySqlParameter("FechaPoder_In", MySqlDbType.Date) { Value = propietario.FechaPoder });
            mySqlParameters.Add(new MySqlParameter("NumeroInstrumento_In", MySqlDbType.VarChar, 50) { Value = propietario.NumeroInstrumento });
            mySqlParameters.Add(new MySqlParameter("NumeroNotaria_In", MySqlDbType.Int32) { Value = propietario.NumeroNotaria });
            mySqlParameters.Add(new MySqlParameter("EstadoNotaria_In", MySqlDbType.VarChar, 70) { Value = propietario.EstadoNotaria });
            mySqlParameters.Add(new MySqlParameter("Fedatario_In", MySqlDbType.VarChar, 100) { Value = propietario.Fedatario });

            mySqlParameters.Add(new MySqlParameter("NumeroEscritura_In", MySqlDbType.VarChar, 15) { Value = propietario.NumeroEscritura });
            mySqlParameters.Add(new MySqlParameter("NumeroLibro_In", MySqlDbType.VarChar, 5) { Value = propietario.NumeroLibro });
            mySqlParameters.Add(new MySqlParameter("Volumen_In", MySqlDbType.VarChar, 5) { Value = propietario.Volumen });
            mySqlParameters.Add(new MySqlParameter("FechaPoderNotarial_In", MySqlDbType.Date) { Value = propietario.FechaPoderNotarial });
            mySqlParameters.Add(new MySqlParameter("NombreNotario_In", MySqlDbType.VarChar, 100) { Value = propietario.NombreNotario });
            mySqlParameters.Add(new MySqlParameter("NumeroNotario_In", MySqlDbType.VarChar, 5) { Value = propietario.NumeroNotario });
            mySqlParameters.Add(new MySqlParameter("EntidadFederativaNotario_In", MySqlDbType.VarChar, 15) { Value = propietario.EntidadFederativaNotario });
            mySqlParameters.Add(new MySqlParameter("Fojas_In", MySqlDbType.VarChar, 5) { Value = propietario.Fojas });
            mySqlParameters.Add(new MySqlParameter("NombreApoderado_In", MySqlDbType.VarChar, 100) { Value = propietario.NombreApoderado });

            mySqlParameters.Add(new MySqlParameter("Usuario_In", MySqlDbType.VarChar, 45) { Value = "" });
            mySqlParameters.Add(new MySqlParameter("Maquina_In", MySqlDbType.VarChar, 45) { Value = Environment.MachineName });
            mySqlParameters.Add(new MySqlParameter("DireccionIp_In", MySqlDbType.VarChar, 45) { Value = GetIpLocal() });

            mySqlParameters.Add(new MySqlParameter("idRegimenFiscal_In", propietario.idRegimenFiscal));

            conexion.RunStoredProcedure("CRUD_Propietarios", mySqlParameters);

            return true;
        }

        public bool Eliminar(int id)
        {
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("IdPropietario_In", MySqlDbType.Int64, 20) { Value = id });
            conexion.RunStoredProcedure("propietarioDelete", mySqlParameters);
            return true;
        }

        public string GetIpLocal()
        {
            string ip;
            IPAddress[] ips = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            ip = ips[0].ToString();
            return ip;
        }

        public bool RFCValido(string rfc, string persona)
        {
            var RFC_Fisica = @"^([A-Z\s]{4})\d{6}([A-Z\w]{3})$";
            var RFC_Moral = @"^([A-Z&\s]{3})\d{6}([A-Z\w]{3})$";
            try
            {
                if (Regex.IsMatch(rfc, (persona == "F" ? RFC_Fisica : RFC_Moral)))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
