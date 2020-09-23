using System.Linq;
using System;
using IntegrationFactory.Domain.DataSet.PlainText;
using IntegrationFactory.Domain.Tests.SeedWork;
using Xunit;

namespace IntegrationFactory.Domain.Tests.Csv
{
    public class CsvOriginTests
    {


        [Fact]
        public void DadaUmaOrigemVálida()
        {
            var origin = new PlainTextOrigin<Region>(CSV.Path, CSV.Mapping);
            origin.SetSeparator(';');
            origin.Validate();

            Assert.True(origin.Valid);
        }

        [Fact]
        public void DadaUmaOrigemComCaminhoVazio()
        {
            var origin = new PlainTextOrigin<Region>("", CSV.Mapping);
            origin.Validate();

            Assert.Equal("O caminho não pode ser vazio ou nulo.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComCaminhoNulo()
        {
            var origin = new PlainTextOrigin<Region>(null, CSV.Mapping);
            origin.Validate();

            Assert.Equal("O caminho não pode ser vazio ou nulo.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComCaminhoInválido()
        {
            var origin = new PlainTextOrigin<Region>("RRRRRR", CSV.Mapping);
            origin.Validate();

            Assert.Equal("O arquivo não existe no local indicado.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComSeparadorNulo()
        {

            var origin = new PlainTextOrigin<Region>(CSV.Path, CSV.Mapping);
            origin.Validate();

            Assert.Equal("O separador não é válido.",
                origin.Notifications.First());
        }
        [Fact]
        public void DadaUmaOrigemSemConfigurarUmSeparador()
        {

            var origin = new PlainTextOrigin<Region>(CSV.Path, CSV.Mapping);
            origin.Validate();

            Assert.Equal("O separador não é válido.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComUmMappingNulo()
        {
            var origin = new PlainTextOrigin<Region>(CSV.Path, null);
            origin.SetSeparator(';');
            origin.Validate();

            Assert.Equal("O mapeamento não pode ser nulo.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemVálidaRetornaUmaLista()
        {

            var origin = new PlainTextOrigin<Region>(CSV.Path, CSV.Mapping);
            origin.SetSeparator(';');
            origin.Validate();

            var result = origin.Get();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void DadaUmaOrigemComSeparatorInválidoRetornaUmaExcessaoDeFormato()
        {

            var origin = new PlainTextOrigin<Region>(CSV.Path, CSV.Mapping);
            origin.SetSeparator('|');
            origin.Validate();

            Assert.Throws<System.FormatException>(() => origin.Get());
        }

        [Fact]
        public void DadaUmaOrigemComMappginInválidoRetornaUmaExcessaoDeFormato()
        {

            var origin = new PlainTextOrigin<Region>(CSV.Path, CSV.MappingInválido);
            origin.SetSeparator(';');
            origin.Validate();

            Assert.Throws<System.IndexOutOfRangeException>(() => origin.Get());
        }
    }
}