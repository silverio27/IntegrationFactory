using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IntegrationFactory.Domain.DataSet;
using IntegrationFactory.Domain.DataSet.SqlServer;
using IntegrationFactory.Domain.Tests.SeedWork;
using Xunit;

namespace IntegrationFactory.Domain.Tests.SqlServer
{
    public class CsvSqlServerDestinyTests
    {
        DataTable data;
        public CsvSqlServerDestinyTests()
        {
            SeedContext.Execute();
            data = SeedContext.GetMockData();
        }

        [Fact]
        public void DadoUmDestinoVálido()
        {
            ISqlServerDestiny destiny = new SqlServerDestiny();
            destiny.SetConnection(Connections.LocalDataBase);
            destiny.SetTable(SeedContext.ValidTable);
            destiny.Validate();
            Assert.True(destiny.Valid);
        }

        [Fact]
        public void DadoUmDestinoComConexãoVazia()
        {
            ISqlServerDestiny destiny = new SqlServerDestiny();
            destiny.SetConnection(string.Empty);
            destiny.SetTable(SeedContext.ValidTable);
            destiny.Validate();
            Assert.Equal("A string de conexão não pode ser vazio ou nula.",
                destiny.Notifications.First());
        }

        [Fact]
        public void DadoUmDestinoComConexãoNula()
        {
            var destiny = new SqlServerDestiny();
            destiny.SetConnection(null);
            destiny.SetTable(SeedContext.ValidTable);
            destiny.Validate();
            Assert.Equal("A string de conexão não pode ser vazio ou nula.",
                destiny.Notifications.First());
        }


        [Fact]
        public void DadoUmDestinoComUmaTabelaVazia()
        {
            var destiny = new SqlServerDestiny();
            destiny.SetConnection(Connections.LocalDataBase);
            destiny.SetTable(string.Empty);
            destiny.Validate();
            Assert.Equal("A tabela de destino não pode ser vazio ou nulo.",
                destiny.Notifications.First());
        }


        [Fact]
        public void DadoUmDestinoComUmaTabelaNula()
        {
            var destiny = new SqlServerDestiny();
            destiny.SetConnection(Connections.LocalDataBase);
            destiny.SetTable(null);
            destiny.Validate();
            Assert.Equal("A tabela de destino não pode ser vazio ou nulo.",
                destiny.Notifications.First());
        }

        [Fact]
        public void DadoUmDestinoVálidoMasAFonteDeDadosÉNula()
        {
            var destiny = new SqlServerDestiny();
            destiny.SetConnection(Connections.LocalDataBase);
            destiny.SetTable(SeedContext.ValidTable);
            destiny.Validate();
            var result = destiny.Load(null);
            Assert.Equal("A fonte de dados para integração não pode ser nula.", destiny.Result.Message);
        }

        [Fact]
        public void DadoUmDestinoVálidoFazASincronização()
        {
            var destiny = new SqlServerDestiny();
            destiny.SetConnection(Connections.LocalDataBase);
            destiny.SetTable(SeedContext.ValidTable);
            destiny.Validate();
            var result = destiny.Load(data);
            Assert.True(destiny.Result.Success);
        }

        [Fact]
        public void DadoUmaConexãoInválidaRetornaUmaExcessão()
        {
            var destiny = new SqlServerDestiny();
            destiny.SetConnection(Connections.InvalidaDataBase);
            destiny.SetTable(SeedContext.ValidTable);
            destiny.Validate();
            Assert.Throws<System.Data.SqlClient.SqlException>(() => destiny.Load(data));
        }

        [Fact]
        public void DadoUmaStringDeConexãoInválidaRetornaUmaExcessão()
        {
            var destiny = new SqlServerDestiny();
            Assert.Throws<ArgumentException>(() => destiny.SetConnection("XXXX"));
        }

        [Fact]
        public void DadoUmDestinoVálidoMasMapeamentoIncorretoGeraUmExessão()
        {
            var destiny = new SqlServerDestiny();
            destiny.SetConnection(Connections.LocalDataBase);
            destiny.SetTable(SeedContext.ValidTable);
            destiny.Validate();
            destiny.SetMapping(new List<Map>(){
                new Map("x","y"),
                new Map("x","y"),
                new Map("x","y")
            });

            Assert.Throws<InvalidOperationException>(() => destiny.Load(data));
        }

        [Fact]
        public void DadoUmDestinoVálidoEUmMapeamentoIncompletoMasCorretoExecutaASincronização()
        {
            var destiny = new SqlServerDestiny();
            destiny.SetConnection(Connections.LocalDataBase);
            destiny.SetTable(SeedContext.ValidTable);
            destiny.Validate();
            destiny.SetMapping(new List<Map>(){
                new Map("Identidade","Identidade")
            });
            destiny.Load(data);
            Assert.True(destiny.Result.Success);
        }

        [Fact]
        public void DadoUmDestinoVálidoEUmMapeamentoNuloExecutaASincronização()
        {
            var destiny = new SqlServerDestiny();
            destiny.SetConnection(Connections.LocalDataBase);
            destiny.SetTable(SeedContext.ValidTable);
            destiny.Validate();
            destiny.SetMapping(null);
            destiny.Load(data);
            Assert.True(destiny.Result.Success);
        }

        [Fact]
        public void DadoUmDestinoVálidoEUmMapeamentoComColunasAMaisExecutaASincronização()
        {
            var destiny = new SqlServerDestiny();
            destiny.SetConnection(Connections.LocalDataBase);
            destiny.SetTable(SeedContext.ValidTable);
            destiny.Validate();
            destiny.SetMapping(new List<Map>(){
                new Map("Identidade","Identidade"),
                new Map("Sigla","Sigla"),
                new Map("NomeDaRegiao","NomeDaRegiao")
            });
            destiny.Load(data);
            Assert.True(destiny.Result.Success);
        }

        [Fact]
        public void DadoUmDestinoVálidoEmUmaTabelaComUmaChavePrimariaComposta()
        {
            var destiny = new SqlServerDestiny();
            destiny.SetConnection(Connections.LocalDataBase);
            destiny.SetTable(SeedContext.ValidTableComDuasChaves);
            destiny.Validate();
            destiny.Load(data);
            Assert.True(destiny.Result.Success);
        }

        [Fact]
        public void DadoUmDestinoVálidoEUmMapeamentoComPropriedadeVazia()
        {
            var destiny = new SqlServerDestiny();
            destiny.SetConnection(Connections.LocalDataBase);
            destiny.SetTable(SeedContext.ValidTable);
            ;

            Assert.Throws<InvalidOperationException>(() => destiny.SetMapping(new List<Map>(){
                new Map("","Identidade"),
            }));

        }

        [Fact]
        public void DadoUmDestinoVálidoEUmMapeamentoComPropriedadeNula()
        {
            var destiny = new SqlServerDestiny();
            destiny.SetConnection(Connections.LocalDataBase);
            destiny.SetTable(SeedContext.ValidTable);

            Assert.Throws<InvalidOperationException>(() => destiny.SetMapping(new List<Map>(){
                new Map(null,"Identidade"),
            }));

        }

    }
}