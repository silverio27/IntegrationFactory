using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IntegrationFactory.Domain.DataSet.Contracts;
using IntegrationFactory.Domain.DataSet.Notifications;
using IntegrationFactory.Domain.DataSet.SqlServer.Extensions;

namespace IntegrationFactory.Domain.DataSet.SqlServer
{
    public partial class SqlServerDestiny : Validatable, ISqlServerDestiny
    {
        public SqlConnection Connection { get; private set; }

        public string Table { get; private set; }

        public Result Result { get; private set; }

        SqlBulkCopy _sqlBulkCopy;

        public ISqlServerDestiny SetConnection(string connection)
        {
            Connection = new SqlConnection(connection);
            _sqlBulkCopy = new SqlBulkCopy(Connection);
            return this;
        }

        public IDestiny SetMapping(List<Map> mapping)
        {
            if (mapping == null) return this;
            mapping.ForEach((m) =>
                _sqlBulkCopy.ColumnMappings.Add(
                    new SqlBulkCopyColumnMapping(m.Source, m.Target)));
            return this;
        }

        public ISqlServerDestiny SetTable(string table)
        {
            Table = table;
            return this;
        }

        public override void Validate()
        {
            var emptyConnection = string.IsNullOrEmpty(Connection.ConnectionString);

            if (emptyConnection)
                AddNotification("A string de conexão não pode ser vazio ou nula.");

            if (string.IsNullOrEmpty(Table))
                AddNotification("A tabela de destino não pode ser vazio ou nulo.");

        }


        public IDestiny Load(DataTable data)
        {
            if (data == null)
            {
                Result = new Result(false, "A fonte de dados para integração não pode ser nula.");
                return this;
            }

            Connection.Open();
            this.CreateTemporaryTable();
            Bulk(data);
            this.Merge();
            Result = new Result(true, $"{data.Rows.Count} itens incluídos");
            return this;
        }

        private void Bulk(DataTable data)
        {
            _sqlBulkCopy.BulkCopyTimeout = 0;
            _sqlBulkCopy.BatchSize = data.Rows.Count;
            _sqlBulkCopy.DestinationTableName = $"#{Table}";
            _sqlBulkCopy.WriteToServer(data);
        }

    }
}