using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace IntegrationFactory.Domain.Tests.SeedWork
{
    public static class SeedContext
    {
        public static void Execute()
        {
            using (var connection = new SqlConnection(Connections.LocalDataBase))
            {
                connection.Open();
                SqlSeedData.Seeds.ForEach(seed => connection.Seed(seed));
            }

        }

        private static void Seed(this SqlConnection connection, string sqlcommand)
        {
            var command = connection.CreateCommand();
            command.CommandText = File.ReadAllText(sqlcommand);
            command.ExecuteNonQuery();
        }

        public static DataTable GetMockData()
        {
            DataTable data = new DataTable();
            data.Columns.Add("Identidade", typeof(int));
            data.Columns.Add("Sigla", typeof(string));
            data.Columns.Add("NomeDaRegiao", typeof(string));

            data.Rows.Add(1, "MG", "Minas Gerais");
            data.Rows.Add(2, "SJN", "São João Nepomuceno");
            return data;
        }

        public readonly static string ValidTable = "RegiaoTest";
        public readonly static string ValidTableExtend = "RegiaoTestExtends";
        public readonly static string ValidTableComDuasChaves = "RegiaoComDuasChaves";
    }
}