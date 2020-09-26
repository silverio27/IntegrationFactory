using System.Data.SqlClient;
using IntegrationFactory.Domain.DataSet.Contracts;

namespace IntegrationFactory.Domain.DataSet.SqlServer
{
    public interface ISqlServerDestiny : IDestiny
    {
        SqlConnection Connection { get; }
        string Table { get; }
        ISqlServerDestiny SetConnection(string connection);
        ISqlServerDestiny SetTable(string table);
    }
}