using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using IntegrationFactory.Domain.DataSet.Contracts;
using IntegrationFactory.Domain.DataSet.Notifications;

namespace IntegrationFactory.Domain.DataSet.SqlServer
{
    public class SqlServerOrigin<T> : Validatable, IOrigin<T>
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
        public override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}