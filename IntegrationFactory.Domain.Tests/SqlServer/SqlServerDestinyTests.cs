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
            var destiny = new SqlServerDestiny(Connections.LocalDataBase, SeedContext.ValidTable);
            destiny.Validate();
            Assert.True(destiny.Valid);
        }

        [Fact]
        public void DadoUmDestinoComConexãoVazia()
        {
            var destiny = new SqlServerDestiny(string.Empty, SeedContext.ValidTable);
            destiny.Validate();
            Assert.Equal("A string de conexão não pode ser vazio ou nula.",
                destiny.Notifications.First());
        }

        [Fact]
        public void DadoUmDestinoComConexãoNula()
        {
            var destiny = new SqlServerDestiny(null, SeedContext.ValidTable);
            destiny.Validate();
            Assert.Equal("A string de conexão não pode ser vazio ou nula.",
                destiny.Notifications.First());
        }


        [Fact]
        public void DadoUmDestinoComUmaTabelaVazia()
        {
            var destiny = new SqlServerDestiny(Connections.LocalDataBase, string.Empty);
            destiny.Validate();
            Assert.Equal("A tabela de destino não pode ser vazio ou nulo.",
                destiny.Notifications.First());
        }


        [Fact]
        public void DadoUmDestinoComUmaTabelaNula()
        {
            var destiny = new SqlServerDestiny(Connections.LocalDataBase, null);
            destiny.Validate();
            Assert.Equal("A tabela de destino não pode ser vazio ou nulo.",
                destiny.Notifications.First());
        }

        [Fact]
        public void DadoUmDestinoVálidoMasAFonteDeDadosÉNula()
        {
            var destiny = new SqlServerDestiny(Connections.LocalDataBase, SeedContext.ValidTable);
            destiny.Validate();
            var result = destiny.Synk(null);
            Assert.Equal("A fonte de dados para integração não pode ser nula.", result.Message);
        }

        [Fact]
        public void DadoUmDestinoVálidoFazASincronização()
        {
            var destiny = new SqlServerDestiny(Connections.LocalDataBase, SeedContext.ValidTable);
            destiny.Validate();
            var result = destiny.Synk(data);
            Assert.True(result.Success);
        }

        [Fact]
        public void DadoUmaConexãoInválidaRetornaUmaExcessão()
        {
            var destiny = new SqlServerDestiny("Server=.\\SQLEXPRESSXXXX;Database=TESTE;Trusted_Connection=True;", SeedContext.ValidTable);
            destiny.Validate();
            Assert.Throws<System.Data.SqlClient.SqlException>(() => destiny.Synk(data));
        }


        [Fact]
        public void DadoUmaStringDeConexãoInválidaRetornaUmaExcessão()
        {
            Assert.Throws<ArgumentException>(() => new SqlServerDestiny("XXXX", SeedContext.ValidTable));
        }

        [Fact]
        public void DadoUmDestinoVálidoMasMapeamentoIncorretoGeraUmExessão()
        {
            var destiny = new SqlServerDestiny(Connections.LocalDataBase, SeedContext.ValidTable);
            destiny.Validate();
            destiny.MapToSynk(new List<Map>(){
                new Map("x","y"),
                new Map("x","y"),
                new Map("x","y")
            });

            Assert.Throws<InvalidOperationException>(() => destiny.Synk(data));
        }


        [Fact]
        public void DadoUmDestinoVálidoEUmMapeamentoIncompletoMasCorretoExecutaASincronização()
        {
            var destiny = new SqlServerDestiny(Connections.LocalDataBase, SeedContext.ValidTable);
            destiny.Validate();
            destiny.MapToSynk(new List<Map>(){
                new Map("Identidade","Identidade")
            });
            var result = destiny.Synk(data);
            Assert.True(result.Success);
        }

        [Fact]
        public void DadoUmDestinoVálidoEUmMapeamentoNuloExecutaASincronização()
        {
            var destiny = new SqlServerDestiny(Connections.LocalDataBase, SeedContext.ValidTable);
            destiny.Validate();
            destiny.MapToSynk(null);
            var result = destiny.Synk(data);
            Assert.True(result.Success);
        }

    }
}