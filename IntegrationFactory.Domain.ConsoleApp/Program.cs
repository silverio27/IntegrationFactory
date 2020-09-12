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
                "Server=.\\SQLEXPRESS2017;Database=IBGE;Uid=sa;Pwd=N13tzsche;", "select * from region");

            var destiny = new SqlServerDestiny(
                "Server=.\\SQLEXPRESS2017;Database=TRAMAL;Uid=sa;Pwd=N13tzsche;",
                "Regiao");

            var map = new List<Map>{
                new Map("Id", "Identidade"),
                new Map("Initials", "Sigla"),
                new Map("Name", "NomeDaRegiao"),
            };

            var pipeLine = new PipeLineContext<Region>(
                origin, destiny, map)
                    .Get()
                    .Synk();

            foreach (var notification in pipeLine.Notifications)
            {
                Console.WriteLine(notification);
            }
        }
    }
}
