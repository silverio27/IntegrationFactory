using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using IntegrationFactory.Domain.DataSet.Contracts;
using IntegrationFactory.Domain.DataSet.Notifications;

namespace IntegrationFactory.Domain.DataSet.SqlServer
{
    public class SqlServerOrigin<T> : Validatable, ISqlServerOrigin<T>
    {
        public SqlConnection Connection { get; private set; }

        public string SqlCommand { get; private set; }

        public IEnumerable<T> Data { get; private set; }

        public ISqlServerOrigin<T> SetConnection(string connection)
        {
            Connection = new SqlConnection(connection);
            return this;
        }

        public ISqlServerOrigin<T> SetSqlCommand(string sqlCommand)
        {
            SqlCommand = sqlCommand;
            return this;
        }

        public IOrigin<T> Extract()
        {
            Data = Connection.Query<T>(SqlCommand);
            return this;
        }

        public override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}