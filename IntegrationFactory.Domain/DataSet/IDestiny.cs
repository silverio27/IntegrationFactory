using System;
using System.Collections.Generic;
using System.Data;

namespace IntegrationFactory.Domain.DataSet
{
    public interface IDestiny : IDisposable
    {
        Result Synk(DataTable data);
        void MapToSynk(List<Map> maps);
    }
}