using IntegrationFactory.Domain.DataSet.SqlServer;
using IntegrationFactory.Domain.Tests.SeedWork;
using Xunit;

namespace IntegrationFactory.Domain.Tests.Csv
{
    public class CsvSqlServerDesitnyTests
    {
        [Fact]
        public void DadoUmDestinoVálido()
        {
            var destiny = new SqlServerDestiny(Connections.LocalDataBase, "Regiao");
            destiny.Validate();
            Assert.True(destiny.Valid);
        }

    }
}