using System;
using System.Collections.Generic;
using IntegrationFactory.Domain.ConsoleApp.Extensions;
using IntegrationFactory.Domain.ConsoleApp.Model;
using IntegrationFactory.Domain.ConsoleApp.Util;
using IntegrationFactory.Domain.DataSet;
using IntegrationFactory.Domain.DataSet.PlainText;
using IntegrationFactory.Domain.DataSet.SqlServer;
using IntegrationFactory.Domain.PipeLine;

namespace IntegrationFactory.Domain.ConsoleApp.Testes
{
    public class CsvToSqlServerTransformed
    {
        public static void Execute()
        {

            var pipeLine = new PipeLineContext<Region>()
                  .SetOrigin(new PlainTextOrigin<Region>()
                                .SetPath("Regiao.csv")
                                .SetSeparator(';')
                                .SetMapping(x => new Region()
                                {
                                    Id = Convert.ToInt32(x[0]),
                                    Initials = x[1],
                                    Name = x[2]
                                }))
                  .SetDestiny(new SqlServerDestiny()
                                .SetConnection(Connections.LocalDataBase)
                                .SetTable("RegiaoTestTransformed")
                                .SetMapping(
                                    new List<Map>(){
                                    new Map("Id", "Identidade"),
                                    new Map("Initials", "Sigla"),
                                    new Map("Name", "NomeDaRegiao"),
                                    new Map("Concat", "Concat"),
                                    new Map("When", "When")
                                }))
                  .Extract()
                  .Transform<RegionTransformed>(x => new RegionTransformed(){
                      Id = x.Id,
                      Initials = x.Initials,
                      Name = x.Name,
                      Concat = $"{x.Initials} - {x.Name}"
                  })
                  .Load();

            pipeLine.WriteNotifications();
            Console.WriteLine(pipeLine.Destiny.Result.Message);
        }
    }
}