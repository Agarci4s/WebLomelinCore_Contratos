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
    public class DataTransf_Opciones_Usuarios
    {
        private Conexion conexion = new Conexion();


        public bool GuardaTransf_Opciones_Usuarios(List<Transf_Opciones_Usuarios> transf_Opciones_Usuarios)
        {
            foreach (var item in transf_Opciones_Usuarios)
            {
                  
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("idTransfOpciones", item.idTransfOpciones));
                listSqlParameters.Add(new MySqlParameter("IdUsuario", item.IdUsuario));
                DataTable dataTable = conexion.RunStoredProcedure("transf_opciones_contratos_usuariosInsert", listSqlParameters);
            }
            return true;
        }

        public bool DeleteTransf_Opciones_Usuarios(List<Transf_Opciones_Usuarios> transf_Opciones_Usuarios)
        {
            foreach (var item in transf_Opciones_Usuarios)
            {
                  
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("idTransfOpciones", item.idTransfOpciones));
                listSqlParameters.Add(new MySqlParameter("IdUsuario", item.IdUsuario));
                DataTable dataTable = conexion.RunStoredProcedure("transf_opciones_contratos_usuariosDelete", listSqlParameters);
            }
            return true;
        }

        public bool UpdateTransf_Opciones_Usuarios(List<Transf_Opciones_Usuarios> transf_Opciones_UsuariosOld, List<Transf_Opciones_Usuarios> transf_Opciones_UsuariosNew)
        {
            List<Transf_Opciones_Usuarios> transf_Opciones_UsuariosUpdate = new List<Transf_Opciones_Usuarios>();
            for (int i = transf_Opciones_UsuariosOld.Count - 1; i >= 0; i--)
            {
                foreach (var item in transf_Opciones_UsuariosNew)
                {
                    if (item.IdUsuario == transf_Opciones_UsuariosOld[i].IdUsuario && item.idTransfOpciones == transf_Opciones_UsuariosOld[i].idTransfOpciones)
                    {
                        if (item.Nivel != transf_Opciones_UsuariosOld[i].Nivel)
                        {
                            transf_Opciones_UsuariosUpdate.Add(item);
                        }

                        transf_Opciones_UsuariosNew.Remove(item);
                        transf_Opciones_UsuariosOld.RemoveAt(i);
                        break;
                    }
                }
            }
            foreach (var item in transf_Opciones_UsuariosUpdate)
            {
                  
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("idTransfOpciones_In", item.idTransfOpciones));
                listSqlParameters.Add(new MySqlParameter("Nivel_In", item.Nivel));
                listSqlParameters.Add(new MySqlParameter("IdUsuario_In", item.IdUsuario));
                DataTable dataTable = conexion.RunStoredProcedure("transf_opciones_contratos_usuariosUpdate", listSqlParameters);
            }
            foreach (var item in transf_Opciones_UsuariosNew)
            {
                  
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("idTransfOpciones", item.idTransfOpciones));
                listSqlParameters.Add(new MySqlParameter("IdUsuario", item.IdUsuario));
                listSqlParameters.Add(new MySqlParameter("Nivel_In", item.Nivel));
                DataTable dataTable = conexion.RunStoredProcedure("transf_opciones_contratos_usuariosInsert", listSqlParameters);
            }
            foreach (var item in transf_Opciones_UsuariosOld)
            {
                  
                List<MySqlParameter> listSqlParameters = new List<MySqlParameter>();
                listSqlParameters.Add(new MySqlParameter("idTransfOpciones", item.idTransfOpciones));
                listSqlParameters.Add(new MySqlParameter("IdUsuario", item.IdUsuario));
                DataTable dataTable = conexion.RunStoredProcedure("transf_opciones_contratos_usuariosDelete", listSqlParameters);
            }
            return true;
        }


        private List<Transf_Opciones_Usuarios> DataToModel(DataTable dataTable)
        {
            List<Transf_Opciones_Usuarios> listTransf_Opciones_Usuarios = new List<Transf_Opciones_Usuarios>();
            foreach (DataRow item in dataTable.Rows)
            {
                Transf_Opciones_Usuarios transf_Opciones_Usuarios = new Transf_Opciones_Usuarios();
                transf_Opciones_Usuarios.idTransfOpciones = Convert.ToInt32(item["idTransfOpciones"].ToString());
                transf_Opciones_Usuarios.idTransfOpcionesUsuarios = Convert.ToInt32(item["idTransfOpciones"].ToString());
                transf_Opciones_Usuarios.IdUsuario = Convert.ToInt32(item["idTransfOpciones"].ToString());

                listTransf_Opciones_Usuarios.Add(transf_Opciones_Usuarios);
            }
            return listTransf_Opciones_Usuarios;
        }
    }
}
