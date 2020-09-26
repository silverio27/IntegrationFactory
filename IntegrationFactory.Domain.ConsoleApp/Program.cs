using System;
using Figgle;
using IntegrationFactory.Domain.DataSet.SqlServer;
using IntegrationFactory.Domain.PipeLine;
using IntegrationFactory.Domain.DataSet.PlainText;
using IntegrationFactory.Domain.ConsoleApp.Testes;

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

            CsvToSqlServer.Execute();

            // XmlToSqlServer.Execute();
            // TxtToSqlServer.Execute();
            // SqlServerToSqlServer.Execute();


            Console.ReadKey();
        }

    }
}
