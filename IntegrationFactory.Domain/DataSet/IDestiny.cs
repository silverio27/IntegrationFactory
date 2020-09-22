using System.Collections.Generic;
using System.Data;

namespace IntegrationFactory.Domain.DataSet
{
    public interface IDestiny : IValidatable
    {
        Result Synk(DataTable data);
        void MapToSynk(List<Map> maps);
    }
}