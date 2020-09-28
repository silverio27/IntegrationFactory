using System.Collections.Generic;
using System.Data;
using IntegrationFactory.Domain.DataSet.Notifications;

namespace IntegrationFactory.Domain.DataSet.Contracts
{
    public interface IDestiny : IValidatable
    {
        Result Result { get; }
        IDestiny SetMapping(List<Map> mapping);
        IDestiny Load(DataTable data);
    }
}