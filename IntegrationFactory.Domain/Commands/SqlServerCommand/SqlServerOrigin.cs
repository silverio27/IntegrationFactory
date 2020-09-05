using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Flunt.Validations;
using IntegrationFactory.Domain.Commands.Contracts;


namespace IntegrationFactory.Domain.Commands.SqlServerCommand
{
    public class SqlServerOrigin<O> : Origin<O>,  IDisposable
    {
        public string ConnectionString { get; }
        public string SqlCommand { get; }
        private SqlConnection connection;

        public SqlServerOrigin(string connectionString, string sqlCommand)
        {
            ConnectionString = connectionString;
            SqlCommand = sqlCommand;
        }
        public override void Validate()
        {
            AddNotifications(new Contract().Requires()
            .IsNotNullOrEmpty(ConnectionString, nameof(ConnectionString), "A string de conexão não pode ser vazia")
            .IsNotNullOrEmpty(SqlCommand, nameof(SqlCommand), "O comando sql não pode ser vazio"));
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
        public override IEnumerable<O> Get()
        {
            var result = connection.Query<O>(SqlCommand);
            return result;
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}