using System;
using System.Collections.Generic;
using System.Linq;
using Figgle;
using IntegrationFactory.Domain.DataSet;
using IntegrationFactory.Domain.DataSet.SqlServer;
using IntegrationFactory.Domain.PipeLine;

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

            Teste();

            Console.ReadKey();
        }

        private static void Teste()
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
