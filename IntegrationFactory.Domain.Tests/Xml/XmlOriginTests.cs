using System.Linq;
using IntegrationFactory.Domain.DataSet.Xml;
using IntegrationFactory.Domain.Tests.SeedWork;
using Xunit;

namespace IntegrationFactory.Domain.Tests.Xml
{
    public class XmlOriginTests
    {

        [Fact]
        public void DadaUmaOrigemVálida()
        {
            IXmlOrigin<Region> origin = new XmlOrigin<Region>();
            origin.SetPath(XML.Path);
            origin.SetMapping(XML.Mapping);
            origin.SetDescendants("Regiao");
            origin.Validate();

            Assert.True(origin.Valid);
        }

        [Fact]
        public void DadaUmaOrigemComCaminhoVazio()
        {
            IXmlOrigin<Region> origin = new XmlOrigin<Region>();
            origin.SetPath("");
            origin.SetMapping(XML.Mapping);
            origin.Validate();

            Assert.Equal("O caminho não pode ser vazio ou nulo.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComCaminhoNulo()
        {
            IXmlOrigin<Region> origin = new XmlOrigin<Region>();
            origin.SetPath(null);
            origin.SetMapping(XML.Mapping);
            origin.Validate();

            Assert.Equal("O caminho não pode ser vazio ou nulo.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComCaminhoInválido()
        {
            IXmlOrigin<Region> origin = new XmlOrigin<Region>();
            origin.SetPath("RRRRRR");
            origin.SetMapping(XML.Mapping);
            origin.Validate();

            Assert.Equal("O arquivo não existe no local indicado.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComSeparadorNulo()
        {

            IXmlOrigin<Region> origin = new XmlOrigin<Region>();
            origin.SetPath(XML.Path);
            origin.SetMapping(XML.Mapping);
            origin.Validate();

            Assert.Equal("Descendentes não pode ser nulo ou vazio.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemComUmMappingNulo()
        {
            IXmlOrigin<Region> origin = new XmlOrigin<Region>();
            origin.SetPath(XML.Path);
            origin.SetMapping(null);
            origin.SetDescendants("Regiao");
            origin.Validate();

            Assert.Equal("O mapeamento não pode ser nulo.",
                origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemInválidaComArquivoVazio()
        {

            IXmlOrigin<Region> origin = new XmlOrigin<Region>();
            origin.SetPath(XML.EmptyPath);
            origin.SetMapping(XML.Mapping);
            origin.SetDescendants("Regiao");
            origin.Validate();

            Assert.Equal("O Arquivo não pode estar vazio.",
               origin.Notifications.First());
        }

        [Fact]
        public void DadaUmaOrigemVálidaRetornaUmaLista()
        {

            IXmlOrigin<Region> origin = new XmlOrigin<Region>();
            origin.SetPath(XML.Path);
            origin.SetMapping(XML.Mapping);
            origin.SetDescendants("Regiao");
            origin.Validate();

            var result = origin.Extract();

            Assert.NotEmpty(result.Data);
        }


        [Fact]
        public void DadaUmaOrigemComSeparatorInválidoRetornaUmaExcessaoDeFormato()
        {

            IXmlOrigin<Region> origin = new XmlOrigin<Region>();
            origin.SetPath(XML.Path);
            origin.SetMapping(XML.Mapping);
            origin.SetDescendants("RR");
            origin.Validate();

            Assert.False(origin.Valid);
        }

        [Fact]
        public void DadaUmaOrigemComMappginInválidoRetornaUmaExcessaoDeFormato()
        {

            IXmlOrigin<Region> origin = new XmlOrigin<Region>();
            origin.SetPath(XML.Path);
            origin.SetMapping(XML.MappingInválido);
            origin.SetDescendants("Regiao");
            origin.Validate();

            Assert.Throws<System.IndexOutOfRangeException>(() => origin.Extract());
        }

    }
}