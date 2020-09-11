using System.Data.SqlClient;

namespace IntegrationFactory.Domain.DataSet.SqlServer
{
    public class SqlServerDestiny<T> : IDestiny<T>
    {

        SqlConnection _connection;

        public SqlServerDestiny(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public IResult<T> Synk(T data)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}