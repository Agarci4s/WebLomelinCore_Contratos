using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using WebColliersCore.DataAccess;
using WebColliersCore.Models;
using WebLomelinCore.Models;

namespace WebLomelinCore.Data
{
    public class DataSelectService
    {
        private Conexion conexion = new Conexion();

        public List<SelectListItem> getCamposStatusServicio { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="1", Text = "Autorizado" },
            new SelectListItem { Value = "2", Text = "Rechazado"  },
            new SelectListItem { Value = "3", Text = "Pagado"  },
            new SelectListItem { Value = "4", Text = "Cargado"  }
        };

        public List<SelectListItem> getCamposTipoServicio { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="1", Text = "Agua" },
            new SelectListItem { Value = "2", Text = "Luz"  },
            new SelectListItem{ Value = "3", Text = "Predial"  }
        }; 
        
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
            listSqlParameters.Add(new MySqlParameter("IdCuentaLuz", pagosluz.idCuentaLuz));
            listSqlParameters.Add(new MySqlParameter("FechaPago", pagosluz.fechaPago));
            listSqlParameters.Add(new MySqlParameter("PeriodoPago", pagosluz.periodoPago));
            listSqlParameters.Add(new MySqlParameter("ImporteLuz", pagosluz.importe));
            listSqlParameters.Add(new MySqlParameter("IvaLuz", pagosluz.iva));
            listSqlParameters.Add(new MySqlParameter("ConceptoPago", pagosluz.conceptoPago));
            listSqlParameters.Add(new MySqlParameter("LineaCaptura", pagosluz.LineaCaptura));
            listSqlParameters.Add(new MySqlParameter("IdUsuario", pagosluz.UsuarioAutoriza));
            listSqlParameters.Add(new MySqlParameter("Estatus", pagosluz.statusproceso));
            listSqlParameters.Add(new MySqlParameter("FechaAltaRegistro", pagosluz.FechaAltaRegistro));
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
            listSqlParameters.Add(new MySqlParameter("IdCuentaPredial", pagospredial.idCuentaPredial));
            listSqlParameters.Add(new MySqlParameter("PeriodoPago", pagospredial.periodoPago));
            listSqlParameters.Add(new MySqlParameter("ImportePredial", pagospredial.importe));
            listSqlParameters.Add(new MySqlParameter("IvaPredial", pagospredial.iva));
            listSqlParameters.Add(new MySqlParameter("Recargos", pagospredial.Recargos));
            listSqlParameters.Add(new MySqlParameter("Multas", pagospredial.Multas));
            listSqlParameters.Add(new MySqlParameter("Actualizacion", pagospredial.Actualizacion));
            listSqlParameters.Add(new MySqlParameter("ConceptoPago", pagospredial.conceptoPago));
            listSqlParameters.Add(new MySqlParameter("LineaCaptura", pagospredial.LineaCaptura));
            listSqlParameters.Add(new MySqlParameter("FechaPagoLimite", pagospredial.fechaPagolimite));
            listSqlParameters.Add(new MySqlParameter("FechaAltaRegistro", pagospredial.FechaAltaRegistro));
            listSqlParameters.Add(new MySqlParameter("IdUsuario", pagospredial.UsuarioAutoriza));
            listSqlParameters.Add(new MySqlParameter("Estatus", pagospredial.statusproceso));
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
                listSqlParameters.Add(new MySqlParameter("IdPagoPredial", pagospredial.idPagoPredial));

                //Campos a actualizar
                listSqlParameters.Add(new MySqlParameter("IdCuentaPredial", pagospredial.idCuentaPredial));
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
