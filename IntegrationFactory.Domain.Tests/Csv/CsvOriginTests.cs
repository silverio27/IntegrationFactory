using System.Linq;
using System;
using IntegrationFactory.Domain.DataSet.PlainText;
using IntegrationFactory.Domain.Tests.SeedWork;
using Xunit;
using System.IO;

namespace IntegrationFactory.Domain.Tests.Csv
{
    public class CsvOriginTests
    {
        private static Func<string[], Region> Mapping => (c => new Region()
        {
            Id = Convert.ToInt32(c[0]),
            Initials = c[1],
            Name = c[2]
        });

        // string _pathCsv = @"C:\Work\IntegrationFactory\IntegrationFactory.Domain.Tests\Csv\Regiao.csv";

        string _pathCsv = @"..\..\..\Files\Regiao.Csv";
        [Fact]
        public void DadaUmaOrigemVálida()
        {
            var origin = new PlainTextOrigin<Region>(_pathCsv, Mapping);
            origin.SetSeparator(';');
            origin.Validate();

            Assert.True(origin.Valid);
        }

        [Fact]
        public void DadaUmaOrigemComCaminhoVazio()
        {
            var origin = new PlainTextOrigin<Region>("", Mapping);
            origin.Validate();

            Assert.Equal("O caminho não pode ser vazio ou nulo.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComCaminhoNulo()
        {
            var origin = new PlainTextOrigin<Region>(null, Mapping);
            origin.Validate();

            Assert.Equal("O caminho não pode ser vazio ou nulo.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComCaminhoInválido()
        {

            var origin = new PlainTextOrigin<Region>("RRRRRR"
                , Mapping);
            origin.Validate();

            Assert.Equal("O arquivo não existe no local indicado.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComSeparadorNulo()
        {

            var origin = new PlainTextOrigin<Region>(_pathCsv
                , Mapping);
            origin.Validate();

            Assert.Equal("O separador não é válido.",
                origin.Notifications.First());
        }
        [Fact]
        public void DadaUmaOrigemComSeparadorInválido()
        {

            var origin = new PlainTextOrigin<Region>(_pathCsv
                , Mapping);
            origin.SetSeparator('');
            origin.Validate();

            Assert.Equal("O separador não é válido.",
                origin.Notifications.First());
        }

    }
}