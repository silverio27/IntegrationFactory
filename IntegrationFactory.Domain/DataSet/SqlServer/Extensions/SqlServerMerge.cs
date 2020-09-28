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
            var keys = destiny.GetColumnKeys();
            string sql = destiny.GetMergeCommand(keys);
            destiny.Connection.Execute(sql);
        }

        public static List<string> GetColumnKeys(this SqlServerDestiny destiny)
        {
            string sql = $@"select COLUMN_NAME 
                                from INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
                            where TABLE_NAME like '{destiny.Table}'";
            return destiny.Connection.Query<string>(sql).ToList();
        }

        public static string GetMergeCommand(this SqlServerDestiny destiny, List<string> keys)
        {
            string sql = $" Merge {destiny.Table} as Destiny \n";
            sql += $" USING #{destiny.Table} as Origin \n";
            for (int i = 0; i < keys.Count; i++)
            {
                string key = keys[i];
                sql += $" {(i == 0 ? "ON" : "AND")} Destiny.[{key}] = Origin.[{key}] ";
                sql += "\n";
            }

            var columns = destiny.GetColumns(keys);

            sql += $" WHEN MATCHED THEN \n";
            sql += $" UPDATE SET \n";
            for (int i = 0; i < columns.Count; i++)
            {
                string column = columns[i];
                sql += $" [{column}] = Origin.[{column}]";
                sql += (i < columns.Count - 1) ? "," : "";
                sql += "\n";
            }

            sql += $" WHEN NOT MATCHED THEN \n";
            sql += $" INSERT ( { string.Join(", ", columns.Select(x => $"[{x}]"))} ) \n";
            sql += $" VALUES ( { string.Join(", ", columns.Select(x => $"Origin.[{x}]")) } ) \n";
            sql += ";";

            return sql;
        }

        public static List<string> GetColumns(this SqlServerDestiny destiny, List<string> keys)
        {
            string sql = $@"select COLUMN_NAME 
                            from INFORMATION_SCHEMA.COLUMNS 
                            where TABLE_NAME like '{destiny.Table}' 
                            and COLUMN_NAME not in ({ string.Join(", ", keys.Select(x=> $"'{x}'" )) })";
            return destiny.Connection.Query<string>(sql).ToList();
        }

    }
}