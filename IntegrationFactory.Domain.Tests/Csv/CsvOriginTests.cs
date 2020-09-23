using System.Linq;
using System;
using IntegrationFactory.Domain.DataSet.PlainText;
using IntegrationFactory.Domain.Tests.SeedWork;
using Xunit;

namespace IntegrationFactory.Domain.Tests.Csv
{
    public class CsvOriginTests
    {
        string _pathCsv = @"..\..\..\Files\Regiao.Csv";
        private static Func<string[], Region> Mapping => (c => new Region()
        {
            Id = Convert.ToInt32(c[0]),
            Initials = c[1],
            Name = c[2]
        });

        private static Func<string[], Region> MappingInválido => (c => new Region()
        {
            Id = Convert.ToInt32(c[0]),
            Initials = c[1],
            Name = c[3]
        });

        private static Func<string[], Region> MappingInv => (c => new Region()
        );


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
            var origin = new PlainTextOrigin<Region>("RRRRRR", Mapping);
            origin.Validate();

            Assert.Equal("O arquivo não existe no local indicado.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComSeparadorNulo()
        {

            var origin = new PlainTextOrigin<Region>(_pathCsv, Mapping);
            origin.Validate();

            Assert.Equal("O separador não é válido.",
                origin.Notifications.First());
        }
        [Fact]
        public void DadaUmaOrigemSemConfigurarUmSeparador()
        {

            var origin = new PlainTextOrigin<Region>(_pathCsv, Mapping);
            origin.Validate();

            Assert.Equal("O separador não é válido.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComUmMappingNulo()
        {
            var origin = new PlainTextOrigin<Region>(_pathCsv, null);
            origin.SetSeparator(';');
            origin.Validate();

            Assert.Equal("O mapeamento não pode ser nulo.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemVálidaRetornaUmaLista()
        {

            var origin = new PlainTextOrigin<Region>(_pathCsv, Mapping);
            origin.SetSeparator(';');
            origin.Validate();

            var result = origin.Get();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void DadaUmaOrigemComSeparatorInválidoRetornaUmaExcessaoDeFormato()
        {

            var origin = new PlainTextOrigin<Region>(_pathCsv, Mapping);
            origin.SetSeparator('|');
            origin.Validate();

            Assert.Throws<System.FormatException>(() => origin.Get());
        }

        [Fact]
        public void DadaUmaOrigemComMappginInválidoRetornaUmaExcessaoDeFormato()
        {

            var origin = new PlainTextOrigin<Region>(_pathCsv, MappingInválido);
            origin.SetSeparator(';');
            origin.Validate();

            Assert.Throws<System.IndexOutOfRangeException>(() => origin.Get());
        }
    }
}