using System.Data.SqlClient;
using IntegrationFactory.Domain.DataSet.Contracts;

namespace IntegrationFactory.Domain.DataSet.SqlServer
{
    public interface ISqlServerOrigin<T> : IOrigin<T>
    {
        SqlConnection Connection { get; }
        string SqlCommand { get; }
        ISqlServerOrigin<T> SetConnection(string connection);
        ISqlServerOrigin<T> SetSqlCommand(string sqlCommand);
    }
}