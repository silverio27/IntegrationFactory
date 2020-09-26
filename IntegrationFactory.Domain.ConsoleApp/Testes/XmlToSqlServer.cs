using System;
using System.Xml.Linq;
using IntegrationFactory.Domain.ConsoleApp.Model;
using IntegrationFactory.Domain.ConsoleApp.Util;
using IntegrationFactory.Domain.DataSet.SqlServer;
using IntegrationFactory.Domain.DataSet.Xml;
using IntegrationFactory.Domain.PipeLine;

namespace IntegrationFactory.Domain.ConsoleApp.Testes
{
    public static class XmlToSqlServer
    {

        public static void Execute()
        {
            // var origin = new XmlOrigin<Region>("Region.xml", Mapping())
            // .SetDescendants("Regiao");

            // var destiny = new SqlServerDestiny(Connections.LocalDataBase, "Regiao");

            // var pipeLine = new PipeLineContext<Region>()
            //       .SetOrigin(origin)
            //       .SetDestiny(destiny)
            //       .AddMap("Id", "Identidade")
            //       .AddMap("Initials", "Sigla")
            //       .AddMap("Name", "NomeDaRegiao")
            //       .Get()
            //       .Synk();

            // foreach (var notification in pipeLine.Notifications)
            // {
            //     Console.WriteLine(notification);
            // }
        }

        private static Func<XElement, Region> Mapping()
        {
            return (c => new Region()
            {
                Id = Convert.ToInt32(c.Element("Id").Value),
                Initials = c.Element("Initials").Value,
                Name = c.Element("Name").Value
            });
        }
    }
}