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
            IPlainTextOrigin<Region> origin = new PlainTextOrigin<Region>();
            origin.SetPath(CSV.Path);
            origin.SetMapping(CSV.Mapping);
            origin.SetSeparator(';');
            origin.Validate();

            Assert.True(origin.Valid);
        }

        [Fact]
        public void DadaUmaOrigemComCaminhoVazio()
        {
            IPlainTextOrigin<Region> origin = new PlainTextOrigin<Region>();
            origin.SetPath("");
            origin.SetMapping(CSV.Mapping);
            origin.Validate();

            Assert.Equal("O caminho não pode ser vazio ou nulo.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComCaminhoNulo()
        {
            IPlainTextOrigin<Region> origin = new PlainTextOrigin<Region>();
            origin.SetPath(null);
            origin.SetMapping(CSV.Mapping);
            origin.Validate();

            Assert.Equal("O caminho não pode ser vazio ou nulo.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComCaminhoInválido()
        {
            IPlainTextOrigin<Region> origin = new PlainTextOrigin<Region>();
            origin.SetPath("RRRRRR");
            origin.SetMapping(CSV.Mapping);
            origin.Validate();

            Assert.Equal("O arquivo não existe no local indicado.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComSeparadorNulo()
        {

            IPlainTextOrigin<Region> origin = new PlainTextOrigin<Region>();
            origin.SetPath(CSV.Path);
            origin.SetMapping(CSV.Mapping);
            origin.Validate();

            Assert.Equal("O separador não é válido.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComUmMappingNulo()
        {
            IPlainTextOrigin<Region> origin = new PlainTextOrigin<Region>();
            origin.SetPath(CSV.Path);
            origin.SetMapping(null);
            origin.SetSeparator(';');
            origin.Validate();

            Assert.Equal("O mapeamento não pode ser nulo.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemInválidaComArquivoVazio()
        {

            IPlainTextOrigin<Region> origin = new PlainTextOrigin<Region>();
            origin.SetPath(CSV.EmptyPath);
            origin.SetMapping(CSV.Mapping);
            origin.SetSeparator(';');
            origin.Validate();

            Assert.Equal("O Arquivo não pode estar vazio.",
               origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemVálidaRetornaUmaLista()
        {

            IPlainTextOrigin<Region> origin = new PlainTextOrigin<Region>();
            origin.SetPath(CSV.Path);
            origin.SetMapping(CSV.Mapping);
            origin.SetSeparator(';');
            origin.Validate();

            var result = origin.Extract();

            Assert.NotEmpty(result.Data);
        }


        [Fact]
        public void DadaUmaOrigemComSeparatorInválidoRetornaUmaExcessaoDeFormato()
        {

            IPlainTextOrigin<Region> origin = new PlainTextOrigin<Region>();
            origin.SetPath(CSV.Path);
            origin.SetMapping(CSV.Mapping);
            origin.SetSeparator('|');
            origin.Validate();

            Assert.Throws<System.FormatException>(() => origin.Extract());
        }

        [Fact]
        public void DadaUmaOrigemComMappginInválidoRetornaUmaExcessaoDeFormato()
        {

            IPlainTextOrigin<Region> origin = new PlainTextOrigin<Region>();
            origin.SetPath(CSV.Path);
            origin.SetMapping(CSV.MappingInválido);
            origin.SetSeparator(';');
            origin.Validate();

            Assert.Throws<System.IndexOutOfRangeException>(() => origin.Extract());
        }
    }
}