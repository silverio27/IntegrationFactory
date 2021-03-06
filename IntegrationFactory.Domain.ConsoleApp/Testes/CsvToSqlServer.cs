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
    public class CsvToSqlServer
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

            pipeLine.WriteNotifications();
            Console.WriteLine(pipeLine.Destiny.Result.Message);
        }
    }
}