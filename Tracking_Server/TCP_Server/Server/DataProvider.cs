using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TCP_Server.Server
{
    public class DataProvider
    {
        private string connectionString = "";

        public DataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SqlConnection Connect()
        {
            var connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (SqlException)
            {

            }
            return null;
        }

        public void ExecuteNonQuery(string sqlCommand)
        {
            var connection = Connect();
            if (connection == null) return;
            var command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sqlCommand;
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }
    }
}