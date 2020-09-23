using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using IntegrationFactory.Domain.DataSet.Contracts;
using IntegrationFactory.Domain.DataSet.Notifications;

namespace IntegrationFactory.Domain.DataSet.SqlServer
{
    public partial class SqlServerDestiny : Validatable, IDestiny
    {
        SqlConnection _connection;
        SqlBulkCopy _sqlBulkCopy;
        public string Table { get; private set; }
        public SqlServerDestiny(string connectionString, string table)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _sqlBulkCopy = new SqlBulkCopy(_connection);
            Table = table;
        }

        public void MapToSynk(List<Map> maps) =>
            maps.ForEach((m) =>
                _sqlBulkCopy.ColumnMappings.Add(
                    new SqlBulkCopyColumnMapping(m.Source, m.Target)));
        public Result Synk(DataTable data)
        {
            CreateTemporaryTable();
            Bulk(data);
            Merge();
            return new Result(true, $"{data.Rows.Count} itens incluídos");
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_connection.ConnectionString))
                AddNotification("A string de conexão não pode ser vazio ou nula.");

            if (string.IsNullOrEmpty(Table))
                AddNotification("A tabela de destino não pode ser vazio ou nulo.");

        }

        public void Dispose()
        {
            _sqlBulkCopy.Close();
            _connection.Dispose();
        }
    }
}