using System;
using System.Linq;
using Figgle;
using IntegrationFactory.Domain.DataSet.SqlServer;
using IntegrationFactory.Domain.PipeLine;
using IntegrationFactory.Domain.DataSet.PlainText;
using System.Xml.Linq;

namespace IntegrationFactory.Domain.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Bem-vindo ao:");
            Console.WriteLine(FiggleFonts.Standard.Render("Integration Factory"));
            Console.ForegroundColor = ConsoleColor.White;


            TesteXml();
            // TesteTxt();

            // TesteSqlToSql();


            Console.ReadKey();
        }


        private static void TesteXml()
        {
            var origin = new XmlOrigin<Region>("Region.xml",
            (c => new Region()
            {
                Id = Convert.ToInt32(c.Element("Id").Value),
                Initials = c.Element("Initials").Value,
                Name = c.Element("Name").Value
            }
            )).SetDescendants("Regiao");

            var destiny = new SqlServerDestiny(
                "Server=.\\SQLEXPRESS;Database=TESTE;Trusted_Connection=True;",
                "Regiao");

            var pipeLine = new PipeLineContext<Region>()
                  .SetOrigin(origin)
                  .SetDestiny(destiny)
                  .AddMap("Id", "Identidade")
                  .AddMap("Initials", "Sigla")
                  .AddMap("Name", "NomeDaRegiao")
                  .Get()
                  .Synk();

            foreach (var notification in pipeLine.Notifications)
            {
                Console.WriteLine(notification);
            }
        }


        private static void TesteTxt()
        {
            var origin = new PlainTextOrigin<Region>("Regiao.txt",
            (c => new Region()
            {
                Id = Convert.ToInt32(c[0]),
                Initials = c[1],
                Name = c[2]
            }
            )).SetSeparator('|');

            var destiny = new SqlServerDestiny(
                "Server=.\\SQLEXPRESS;Database=TESTE;Trusted_Connection=True;",
                "Regiao");

            var pipeLine = new PipeLineContext<Region>()
                  .SetOrigin(origin)
                  .SetDestiny(destiny)
                  .AddMap("Id", "Identidade")
                  .AddMap("Initials", "Sigla")
                  .AddMap("Name", "NomeDaRegiao")
                  .Get()
                  .Synk();

            foreach (var notification in pipeLine.Notifications)
            {
                Console.WriteLine(notification);
            }
        }
        private static void TesteCsv()
        {
            var origin = new PlainTextOrigin<Region>("Regiao.csv",
            (c => new Region()
            {
                Id = Convert.ToInt32(c[0]),
                Initials = c[1],
                Name = c[2]
            }
            ));
            var destiny = new SqlServerDestiny(
                "Server=.\\SQLEXPRESS;Database=TESTE;Trusted_Connection=True;",
                "Regiao");

            var pipeLine = new PipeLineContext<Region>()
                  .SetOrigin(origin)
                  .SetDestiny(destiny)
                  .AddMap("Id", "Identidade")
                  .AddMap("Initials", "Sigla")
                  .AddMap("Name", "NomeDaRegiao")
                  .Get()
                  .Synk();

            foreach (var notification in pipeLine.Notifications)
            {
                Console.WriteLine(notification);
            }
        }

        private static void TesteSqlToSql()
        {
            var origin = new SqlServerOrigin<Region>(
                "Server=.\\SQLEXPRESS;Database=TESTE;Trusted_Connection=True;", "select * from region");

            var destiny = new SqlServerDestiny(
                "Server=.\\SQLEXPRESS;Database=TESTE;Trusted_Connection=True;",
                "Regiao");

            var pipeLine = new PipeLineContext<Region>()
                    .SetOrigin(origin)
                    .OtherAction(() => Log("Origem definida"))
                    .SetDestiny(destiny)
                    .OtherAction(() => Log("Destino definido"))
                    .AddMap("Id", "Identidade")
                    .AddMap("Initials", "Sigla")
                    .AddMap("Name", "NomeDaRegiao")
                    .OtherAction(() => Log("Mapeamento definido"))
                    .Get()
                    .OtherAction(() => Log("Dados obtidos"))
                    .Synk()
                    .OtherAction(() => Log("Integração concluída."));

            foreach (var notification in pipeLine.Notifications)
            {
                Console.WriteLine(notification);
            }
        }

        static void Log(string log)
        {
            Console.WriteLine(log);
        }
    }
}
