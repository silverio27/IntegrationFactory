using System.Linq;
using System;
using System.Data;
using IntegrationFactory.Domain.DataSet.Contracts;

namespace IntegrationFactory.Domain.Extensions
{
    public class C
    {
        public int X { get; set; }
    }
    public static class TransformExtension
    {
        public static DataTable Transform<O, D>(this IOrigin<O> origin, Func<O, D> mapping = null)
        {
            if (mapping == null)
                return origin.Data.ConvertToDataTable();
            return origin.Data.Select(mapping).ConvertToDataTable();

        }
    }
}