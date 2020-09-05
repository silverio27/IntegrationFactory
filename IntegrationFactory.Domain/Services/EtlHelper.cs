using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IntegrationFactory.Domain.Services
{
    public static class EtlHelper
    {
        public static DataTable ConvertToDataTable<T>(this IEnumerable<T> data)
        {
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                DataTable table = new DataTable();

                foreach (PropertyDescriptor prop in properties)
                    table.Columns.Add(
                        prop.Name,
                        Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                    table.Rows.Add(row);
                }

                if (table.Columns.Count == 0)
                    throw new Exception("A lista foi convertida mas as colunas não foram mapeadas.");

                if (table.Rows.Count == 0)
                    throw new Exception("Nenhum dado foi encontrado para conversão.");

                return table;
            }
            catch (Exception e)
            {

                throw new Exception($"Não foi possível converter a Lista, mensagem: {e.Message}");
            }
        }
        public static int BulkInsert(this DataTable dt, SqlConnection conexao, string tabel)
        {
            try
            {
                using (var bc = new SqlBulkCopy(conexao))
                {
                    bc.BulkCopyTimeout = 0;

                    bc.BatchSize = dt.Rows.Count;
                    bc.DestinationTableName = tabel;

                    foreach (DataColumn col in dt.Columns)
                    {
                        var mapName = new SqlBulkCopyColumnMapping(col.ColumnName, col.ColumnName);
                        bc.ColumnMappings.Add(mapName);
                    }

                    bc.WriteToServer(dt);
                    return dt.Rows.Count;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Importação para a tabela : {tabel} falhou, mensagem: {e.Message}");
            }
        }
    }
}