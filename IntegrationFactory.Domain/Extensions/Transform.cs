using System.Linq;
using System;
using System.Data;
using System.Collections.Generic;

namespace IntegrationFactory.Domain.Extensions
{
    public static class TransformExtension
    {
        public static DataTable Transform<O, D>(this IEnumerable<O> data, Func<O, D> mapping = null)
        {
            if (mapping == null)
                return data.ConvertToDataTable();
                
            return data.Select(mapping).ConvertToDataTable();
        }
    }
}