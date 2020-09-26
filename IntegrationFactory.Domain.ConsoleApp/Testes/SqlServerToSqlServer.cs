using System;
using IntegrationFactory.Domain.ConsoleApp.Extensions;
using IntegrationFactory.Domain.ConsoleApp.Model;
using IntegrationFactory.Domain.ConsoleApp.Util;
using IntegrationFactory.Domain.DataSet.SqlServer;
using IntegrationFactory.Domain.PipeLine;

namespace IntegrationFactory.Domain.ConsoleApp.Testes
{
    public class SqlServerToSqlServer
    {
        public static void Execute()
        {
            // var origin = new SqlServerOrigin<Region>(Connections.LocalDataBase, SqlCommand());

            // var destiny = new SqlServerDestiny(Connections.LocalDataBase, "Regiao");

            // var pipeLine = new PipeLineContext<Region>()
            //         .SetOrigin(origin)
            //         .OtherAction(() => Console.WriteLine("Origem definida"))
            //         .SetDestiny(destiny)
            //         .OtherAction(() => Console.WriteLine("Destino definido"))
            //         .AddMap("Id", "Identidade")
            //         .AddMap("Initials", "Sigla")
            //         .AddMap("Name", "NomeDaRegiao")
            //         .OtherAction(() => Console.WriteLine("Mapeamento definido"))
            //         .Get()
            //         .OtherAction(() => Console.WriteLine("Dados obtidos"))
            //         .Synk()
            //         .OtherAction(() => Console.WriteLine("Integração concluída."));

            // pipeLine.WriteNotifications();
        }

        private static string SqlCommand()
        {
            return "select * from region";
        }
    }
}