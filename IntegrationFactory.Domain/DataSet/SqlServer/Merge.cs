using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace IntegrationFactory.Domain.DataSet.SqlServer
{
    public partial class SqlServerDestiny
    {
        private void CreateTemporaryTable()
        {
            string sql = $@"select top 0 *
                            into #{Table}
                            from {Table} ";
            _connection.Execute(sql);
        }

        private void Bulk(DataTable data)
        {
            _sqlBulkCopy.BulkCopyTimeout = 0;
            _sqlBulkCopy.BatchSize = data.Rows.Count;
            _sqlBulkCopy.DestinationTableName = $"#{Table}";
            _sqlBulkCopy.WriteToServer(data);
        }

        private void Merge()
        {
            string key = GetKey();
            string sql = GetMergeCommand(key);
            _connection.Execute(sql);
        }

        private string GetKey()
        {
            string sql = $@"select COLUMN_NAME 
                                from INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
                            where TABLE_NAME like '{Table}'";
            return _connection.QueryFirst<string>(sql);
        }

        private string GetMergeCommand(string key)
        {
            string sql = $" Merge {Table} as Destiny \n";
            sql += $" USING #{Table} as Origin \n";
            sql += $" ON Destiny.{key} = Origin.{key} \n";


            var columns = GetColumns().ToList();

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

        private IEnumerable<string> GetColumns()
        {
            string sql = $@"select COLUMN_NAME 
                            from INFORMATION_SCHEMA.COLUMNS 
                            where TABLE_NAME like '{Table}';";
            return _connection.Query<string>(sql);
        }

    }
}