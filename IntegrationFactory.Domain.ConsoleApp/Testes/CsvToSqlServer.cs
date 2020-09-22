using System;
using IntegrationFactory.Domain.ConsoleApp.Extensions;
using IntegrationFactory.Domain.ConsoleApp.Model;
using IntegrationFactory.Domain.ConsoleApp.Util;
using IntegrationFactory.Domain.DataSet.PlainText;
using IntegrationFactory.Domain.DataSet.SqlServer;
using IntegrationFactory.Domain.PipeLine;

namespace IntegrationFactory.Domain.ConsoleApp.Testes
{
    public class CsvToSqlServer
    {
        public static void Execute()
        {
            var origin = new PlainTextOrigin<Region>("Regiao.csv", Mapping());
            var destiny = new SqlServerDestiny(Connections.LocalDataBase, "Regiao");

            var pipeLine = new PipeLineContext<Region>()
                  .SetOrigin(origin)
                  .SetDestiny(destiny)
                  .AddMap("Id", "Identidade")
                  .AddMap("Initials", "Sigla")
                  .AddMap("Name", "NomeDaRegiao")
                  .Get()
                  .Synk();

            pipeLine.WriteNotifications();
        }

        private static Func<string[], Region> Mapping()
        {
            return (c => new Region()
            {
                Id = Convert.ToInt32(c[0]),
                Initials = c[1],
                Name = c[2]
            });
        }
    }
}