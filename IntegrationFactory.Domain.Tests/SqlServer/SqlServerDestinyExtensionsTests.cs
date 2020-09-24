using System.Collections.Generic;
using System.Data.SqlClient;
using IntegrationFactory.Domain.DataSet.SqlServer;
using IntegrationFactory.Domain.DataSet.SqlServer.Extensions;
using IntegrationFactory.Domain.Tests.SeedWork;
using Xunit;

namespace IntegrationFactory.Domain.Tests.SqlServer
{
    public class SqlServerDestinyExtensionsTests
    {
        SqlServerDestiny destiny;
        public SqlServerDestinyExtensionsTests()
        {
            destiny = new SqlServerDestiny(Connections.LocalDataBase, SeedContext.ValidTable);
        }

        [Fact]
        public void DadoUmDestinoVálidoVerificaSeAtabelaTemporiaFoiCriada()
        {
            using (destiny.Connection)
            {
                destiny.Connection.Open();
                destiny.CreateTemporaryTable();
                var cmd = destiny.Connection.CreateCommand();
                cmd.CommandText = $@"select name 
                from tempdb.sys.objects 
                where name like N'#{destiny.Table}%'";
                var reader = cmd.ExecuteReader();

                Assert.True(reader.HasRows);
            }
        }

        [Fact]
        public void DadoUmDestinoVálidoVerificaObtemAChavePrimariaDaTabela()
        {
            using (destiny.Connection)
            {
                destiny.Connection.Open();
                var key = destiny.GetColumnKey();

                Assert.Equal("Identidade", key);
            }
        }

        [Fact]
        public void DadoUmDestinoVálidoObtemOComandoDeMerge()
        {
            using (destiny.Connection)
            {
                destiny.Connection.Open();
                var key = destiny.GetColumnKey();
                var command = destiny.GetMergeCommand(key);

                Assert.Equal(" Merge RegiaoTest as Destiny \n USING #RegiaoTest as Origin \n ON Destiny.Identidade = Origin.Identidade \n WHEN MATCHED THEN \n UPDATE SET \n Identidade = Origin.Identidade,\n Sigla = Origin.Sigla,\n NomeDaRegiao = Origin.NomeDaRegiao\n WHEN NOT MATCHED THEN \n INSERT ( Identidade, Sigla, NomeDaRegiao ) \n VALUES ( Origin.Identidade, Origin.Sigla, Origin.NomeDaRegiao ) \n;"
, command);
            }
        }

        [Fact]
        public void DadoUmDestinoVálidoRetornaAsColunasDaTabela()
        {
            using (destiny.Connection)
            {
                destiny.Connection.Open();
                var result = destiny.GetColumns();
                var columns = new List<string>(){
                    "Identidade",
                    "Sigla",
                    "NomeDaRegiao"
                };
                Assert.Equal(result, columns);

            }
        }

    }
}