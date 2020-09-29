using System.Collections.Generic;
using IntegrationFactory.Domain.Tests.SeedWork;
using Xunit;
using IntegrationFactory.Domain.Extensions;

namespace IntegrationFactory.Domain.Tests.Extensions
{
    public class ExtensionsTests
    {
        [Fact]
        public void DadoUmaListaVálida()
        {
            var list = ValidList();

            var data = list.Transform<Region, Region>(null);

            Assert.Equal(2, data.Rows.Count);
        }

        [Fact]
        public void DadoUmaListaVálidaExecutaoTransform()
        {
            var list = ValidList();

            var data = list.Transform(x => new { x.Initials });

            Assert.Equal(2, data.Rows.Count);
        }

        private static IEnumerable<Region> ValidList()
        {
            return new List<Region>(){
                new Region()
                    {
                        Id = 1,
                        Initials = "MG",
                        Name = "Minas Gerais"
                    },
                new Region()
                    {
                        Id = 2,
                        Initials = "RJ",
                        Name = "Rio de Janeiro"
                    }
            };
        }
    }
}