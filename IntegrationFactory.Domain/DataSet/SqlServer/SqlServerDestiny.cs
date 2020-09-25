using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IntegrationFactory.Domain.DataSet.Contracts;
using IntegrationFactory.Domain.DataSet.Notifications;
using IntegrationFactory.Domain.DataSet.SqlServer.Extensions;

namespace IntegrationFactory.Domain.DataSet.SqlServer
{
    public partial class SqlServerDestiny : Validatable, IDestiny
    {
        public SqlConnection Connection;
        SqlBulkCopy _sqlBulkCopy;
        public string Table { get; private set; }

        public SqlServerDestiny(string connectionString, string table)
        {
            Connection = new SqlConnection(connectionString);
            _sqlBulkCopy = new SqlBulkCopy(Connection);
            Table = table;
        }

        public void MapToSynk(List<Map> maps)
        {
            if (maps == null) return;
            maps.ForEach((m) =>
                _sqlBulkCopy.ColumnMappings.Add(
                    new SqlBulkCopyColumnMapping(m.Source, m.Target)));
        }

        public Result Load(DataTable data)
        {
            if (data == null)
                return new Result(false, "A fonte de dados para integração não pode ser nula.");

            Connection.Open();
            this.CreateTemporaryTable();
            Bulk(data);
            this.Merge();
            return new Result(true, $"{data.Rows.Count} itens incluídos");
        }

        private void Bulk(DataTable data)
        {
            _sqlBulkCopy.BulkCopyTimeout = 0;
            _sqlBulkCopy.BatchSize = data.Rows.Count;
            _sqlBulkCopy.DestinationTableName = $"#{Table}";
            _sqlBulkCopy.WriteToServer(data);
        }


        public override void Validate()
        {
            var emptyConnection = string.IsNullOrEmpty(Connection.ConnectionString);

            if (emptyConnection)
                AddNotification("A string de conexão não pode ser vazio ou nula.");

            if (string.IsNullOrEmpty(Table))
                AddNotification("A tabela de destino não pode ser vazio ou nulo.");

        }
    }
}