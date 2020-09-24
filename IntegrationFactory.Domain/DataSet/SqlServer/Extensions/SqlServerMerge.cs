using System.Collections.Generic;
using System.Linq;
using Dapper;


namespace IntegrationFactory.Domain.DataSet.SqlServer.Extensions
{
    public static class SqlServerMerge
    {
        public static void CreateTemporaryTable(this SqlServerDestiny destiny)
        {
            string sql = $@"select top 0 *
                            into #{destiny.Table}
                            from {destiny.Table} ";
            destiny.Connection.Execute(sql);
        }

        public static void Merge(this SqlServerDestiny destiny)
        {
            string key = destiny.GetColumnKey();
            string sql = destiny.GetMergeCommand(key);
            destiny.Connection.Execute(sql);
        }

        public static string GetColumnKey(this SqlServerDestiny destiny)
        {
            string sql = $@"select COLUMN_NAME 
                                from INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
                            where TABLE_NAME like '{destiny.Table}'";
            return destiny.Connection.QueryFirst<string>(sql);
        }

        public static string GetMergeCommand(this SqlServerDestiny destiny, string key)
        {
            string sql = $" Merge {destiny.Table} as Destiny \n";
            sql += $" USING #{destiny.Table} as Origin \n";
            sql += $" ON Destiny.{key} = Origin.{key} \n";


            var columns = destiny.GetColumns();

            sql += $" WHEN MATCHED THEN \n";
            sql += $" UPDATE SET \n";
            for (int i = 0; i < columns.Count; i++)
            {
                string column = columns[i];
                sql += $" {column} = Origin.{column}";
                sql += (i < columns.Count - 1) ? "," : "";
                sql += "\n";
            }

            sql += $" WHEN NOT MATCHED THEN \n";
            sql += $" INSERT ( { string.Join(", ", columns)} ) \n";
            sql += $" VALUES ( { string.Join(", ", columns.Select(x => $"Origin.{x}")) } ) \n";
            sql += ";";

            return sql;
        }

        public static List<string> GetColumns(this SqlServerDestiny destiny)
        {
            string sql = $@"select COLUMN_NAME 
                            from INFORMATION_SCHEMA.COLUMNS 
                            where TABLE_NAME like '{destiny.Table}';";
            return destiny.Connection.Query<string>(sql).ToList();
        }

    }
}