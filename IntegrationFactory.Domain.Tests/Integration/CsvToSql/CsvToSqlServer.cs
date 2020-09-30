using System;
using System.Collections.Generic;
using IntegrationFactory.Domain.DataSet;
using IntegrationFactory.Domain.DataSet.PlainText;
using IntegrationFactory.Domain.DataSet.SqlServer;
using IntegrationFactory.Domain.PipeLine;
using IntegrationFactory.Domain.Tests.SeedWork;
using Xunit;

namespace IntegrationFactory.Domain.Tests
{
    public class CsvToSqlServer
    {

        [Fact]
        public void CsvToSqlServerExecute()
        {

            var pipeLine = new PipeLineContext<Region>()
                  .SetOrigin(new PlainTextOrigin<Region>()
                                .SetPath(CSV.Path)
                                .SetSeparator(';')
                                .SetMapping(x => new Region()
                                {
                                    Id = Convert.ToInt32(x[0]),
                                    Initials = x[1],
                                    Name = x[2]
                                }))
                  .SetDestiny(new SqlServerDestiny()
                                .SetConnection(Connections.LocalDataBase)
                                .SetTable("Regiao")
                                .SetMapping(
                                    new List<Map>(){
                                    new Map("Id", "Identidade"),
                                    new Map("Initials", "Sigla"),
                                    new Map("Name", "NomeDaRegiao")
                                }))
                  .Extract()
                  .Transform<Region>()
                  .Load();

            Assert.Equal("3 itens incluídos", pipeLine.Destiny.Result.Message);

        }

        [Fact]
        public void CsvToSqlServerComUmaColunaAMenosExecute()
        {

            var pipeLine = new PipeLineContext<Region>()
                  .SetOrigin(new PlainTextOrigin<Region>()
                                .SetPath(CSV.Path)
                                .SetSeparator(';')
                                .SetMapping(x => new Region()
                                {
                                    Id = Convert.ToInt32(x[0]),
                                    Initials = x[1],
                                    Name = x[2]
                                }))
                  .SetDestiny(new SqlServerDestiny()
                                .SetConnection(Connections.LocalDataBase)
                                .SetTable("RegiaoTestMinus")
                                .SetMapping(
                                    new List<Map>(){
                                    new Map("Id", "Identidade"),
                                    new Map("Initials", "Sigla"),
                                }))
                  .Extract()
                  .Transform(x => new
                  {
                      x.Id,
                      x.Initials
                  })
                  .Load();

            Assert.Equal("3 itens incluídos", pipeLine.Destiny.Result.Message);

        }
    }
}