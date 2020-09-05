using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Flunt.Validations;
using IntegrationFactory.Domain.Commands.Contracts;
using IntegrationFactory.Domain.Services;

namespace IntegrationFactory.Domain.Commands.SqlServerCommand
{
    public class SqlServerDestiny<T> : Destiny<T>, IDisposable
    {
        public string ConnectionString { get; }
        public string Table { get;  }

        SqlConnection connection;
        public SqlServerDestiny(string connectionString, string table)
        {
            ConnectionString = connectionString;
            Table = table;
        }

        public override void Validate()
        {
            AddNotifications(new Contract().Requires()
                .IsNotNullOrEmpty(ConnectionString, nameof(ConnectionString), "A string de conexão não pode ser vazia")
                .IsNotNullOrEmpty(Table, nameof(Table), "O nome da tabela de destino não pode ser vazia"));
        }

        public bool TestConnection()
        {
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public override int Send<O>(IEnumerable<O> dataOrigin)
        {
           return dataOrigin.ConvertToDataTable<O>().BulkInsert(connection, this.Table);
        }


        public void Dispose()
        {
            connection.Dispose();
        }


    }
}