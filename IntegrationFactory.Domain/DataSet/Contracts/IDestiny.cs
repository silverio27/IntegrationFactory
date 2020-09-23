using System.Collections.Generic;
using System.Data;
using IntegrationFactory.Domain.DataSet.Notifications;

namespace IntegrationFactory.Domain.DataSet.Contracts
{
    public interface IDestiny : IValidatable
    {
        Result Synk(DataTable data);
        void MapToSynk(List<Map> maps);
    }
}