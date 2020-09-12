using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace IntegrationFactory.Domain.DataSet.SqlServer
{
    public class SqlServerOrigin<T> : IOrigin<T>
    {

        SqlConnection _connection;
        public string SqlCommand { get; private set; }
     
        public SqlServerOrigin(string connectionString, string sqlCommand)
        {
            _connection = new SqlConnection(connectionString);
            SqlCommand = sqlCommand;
        }
        public IEnumerable<T> Get()
        {
            return _connection.Query<T>(SqlCommand);
        }

        public void Dispose()
        {
           _connection.Dispose();
        }
    }
}