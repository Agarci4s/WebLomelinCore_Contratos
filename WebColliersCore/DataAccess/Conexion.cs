using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebColliersCore.Models;
using System.Text.Json;
using DocumentFormat.OpenXml.Office.Word;

namespace WebColliersCore.DataAccess
{
    public class Conexion
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string port;
        private MySqlDataReader dataReader;       

        //Server
        //private void Initialize()
        //{

        //    var jsonString = File.ReadAllText("json.json");
        //    ConfigJson jsonModel = JsonSerializer.Deserialize<ConfigJson>(jsonString);
        //    // var modelJson = JsonSerializer.Serialize(jsonModel);


        //    server = "184.168.30.21";
        //    port = "3306";
        //    database = "bbva_lomelin"; //SystemComplementos.configJson.DB;
        //    uid = "admin_lomelin";//new_schema
        //    password = "V?3$1adwt0rjhmTR";
        //    string connectionString;
        //    connectionString = "SERVER=" + server + ";" + "Port=" + port + ";" +
        //    "DATABASE=" + database + ";" + " UID=" + uid + ";" + " PASSWORD=" + password + ";" + " CheckParameters=true;";
        //    connection = new MySqlConnection(connectionString);
        //}

        //Local
        private void Initialize()
        {

            var jsonString = File.ReadAllText("json.json");
            ConfigJson jsonModel = JsonSerializer.Deserialize<ConfigJson>(jsonString);
            var modelJson = JsonSerializer.Serialize(jsonModel);

            server = "184.168.30.21";  //"184.168.30.21";
            port = "3306";
            database = "bbva_lomelin_pruebas";//corecolliers
            uid = "admin_lomelin_prueba";  //"admin_lomelin";//new_schema
            password = "iOJZ#y6pxh6~z2bc";  //"V?3$1adwt0rjhmTR";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "Port=" + port + ";" +
            "DATABASE=" + database + ";" + " UID=" + uid + ";" + " PASSWORD=" + password + ";" + " CheckParameters=false;";
            connection = new MySqlConnection(connectionString);
        }              

        //open connection to database

        //0: Cannot connect to server.
        private bool OpenConnection()
        {
            if (connection == null)
                Initialize();
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //Theame and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public DataTable RunStoredProcedure(String nameStoredprocedure, List<MySqlParameter> mySqlParameter)
        {
            //Create a table to store the result
            DataTable dataTable = new DataTable();

            try
            {
                //Open connection
                if (this.OpenConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(nameStoredprocedure, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (var item in mySqlParameter)
                    {
                        command.Parameters.Add(item);
                    }
                    //command.CommandTimeout = 600;
                    command.CommandTimeout = 86400; //10 minutos
                    MySqlDataReader dataReader = command.ExecuteReader();
                    //command.ExecuteNonQuery();

                    //Read the data and store them in the list
                    DataSet dataSet = new DataSet();
                    dataSet.Tables.Add(dataTable);
                    dataSet.EnforceConstraints = false;
                    dataTable.Load(dataReader);

                    //close Data Reader
                    dataReader.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    //return dataTable;
                }
                else
                {
                    //return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public MySqlParameterCollection RunStoredProcedure(String nameStoredprocedure, List<MySqlParameter> mySqlParameterIn, List<MySqlParameter> mySqlParameterOut)
        {
            //Create a table to store the result
            DataTable dataTable = new DataTable();

            try
            {


                //Open connection
                if (this.OpenConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(nameStoredprocedure, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (var item in mySqlParameterIn)
                    {
                        command.Parameters.Add(item);
                    }
                    foreach (var item in mySqlParameterOut)
                    {
                        command.Parameters.Add(item);
                        command.Parameters[item.ParameterName].Direction = ParameterDirection.Output;
                    }

                    MySqlDataReader dataReader = command.ExecuteReader();
                    //command.ExecuteNonQuery();

                    //Read the data and store them in the list
                    DataSet dataSet = new DataSet();
                    dataSet.Tables.Add(dataTable);
                    dataSet.EnforceConstraints = false;
                    dataTable.Load(dataReader);

                    //close Data Reader
                    dataReader.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return command.Parameters;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public MySqlDataReader RunStoredProcedureDR(String nameStoredprocedure, List<MySqlParameter> mySqlParameter)
        {
            //Create a table to store the result
            DataTable dataTable = new DataTable();



            try
            {
                //Open connection
                if (this.OpenConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(nameStoredprocedure, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (var item in mySqlParameter)
                    {
                        command.Parameters.Add(item);
                    }
                    command.CommandTimeout = 600; //10 minutos
                                                  // MySqlDataReader 
                    dataReader = command.ExecuteReader();

                    //Read the data and store them in the list
                    //DataSet dataSet = new DataSet();
                    //dataSet.Tables.Add(dataTable);
                    //dataSet.EnforceConstraints = false;
                    //dataTable.Load(dataReader);

                    //close Data Reader
                    //dataReader.Close();

                    //close Connection

                    //return list to be displayed
                    //return dataTable;
                }
                else
                {
                    //return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataReader;
        }

        public void ExecuteNonQuerySP(string procedimiento, params MySqlParameter[] datos)
        {
            try
            {
                OpenConnection();
                using (connection)
                {
                    using (MySqlCommand command = new MySqlCommand(procedimiento, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(datos);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }

        public DataTable ExecuteSP(string procedimiento, params MySqlParameter[] datos)
        {
            try
            {
                OpenConnection();
                using (connection)
                {
                    using (MySqlCommand command = new MySqlCommand(procedimiento, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(datos);

                        DataTable dt = new DataTable();
                        dt.Load(command.ExecuteReader());
                        return dt;
                    }
                }
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }


        private void ExecuteSQLStatement(string cmdText)
        {
            try
            {
                if (OpenConnection() == true)
                {
                    var cmd = new MySqlCommand(cmdText, connection);
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("success");

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.ToString());
            }
        }
    }

}
