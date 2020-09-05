using System;
using IntegrationFactory.Domain.Commands.SqlServerCommand;
using IntegrationFactory.Domain.Handlers;

namespace IntegrationFactory.Domain.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Teste manual");
            try
            {
                var origin = new SqlServerOrigin<Region>(
                    "Server=.\\SQLEXPRESS2017;Database=IBGE;Uid=sa;Pwd=N13tzsche;",
                    "select * from region");

                var destiny = new SqlServerDestiny<Region>(
                    "Server=.\\SQLEXPRESS2017;Database=TRAMAL;Uid=sa;Pwd=N13tzsche;",
                    "region");
                var copy = new CopyData<Region, Region>();
                var result = copy.Copy(origin, destiny);
                Console.WriteLine(result.Message);


            }
            catch (System.Exception e)
            {

                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
