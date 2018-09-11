using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace br.com.sagradacruz.Connection
{
    public class Database
    {
        public MySqlConnection OpenConnection()
        {
            try
            {
                var connString = ConnectionString();
                var command = new MySqlCommand();
                var conn = new MySqlConnection(connString);

                conn.Open();

                return conn;
            }
            catch (Exception ex)
            {
                return null;
                //throw new Exception("Command cannot be created. Error: " + ex.Message);
            }
        }

        public void AddInParameter(ref MySqlCommand command, string parameterName, object value)
        {
            command.Parameters.AddWithValue(parameterName, value);            
        }

        public void ClearParameters(ref MySqlCommand command)
        {
            command.Parameters.Clear();
        }

        private string ConnectionString()
        {
            string row = string.Empty;
            var connString = string.Empty;
            var found = string.Empty;

            var absolutePath = AppDomain.CurrentDomain.BaseDirectory;
            var appsettings = Path.Combine(absolutePath, "database.json");                        

            using (StreamReader file = new StreamReader(appsettings))
            {
                while((row = file.ReadLine()) != null)
                {
                    if (row.Contains("ConnectionString"))
                    {
                        found = row;
                    }
                }
            }

            connString = found.Split(":")[1].Replace("\"", "").Replace(" ", "");

            return connString;
        }
    }
}
