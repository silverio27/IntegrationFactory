using System.Collections.Generic;
using IntegrationFactory.Domain.DataSet.Notifications;

namespace IntegrationFactory.Domain.DataSet.Contracts
{
    public interface IOrigin<T> : IValidatable
    {
        IEnumerable<T> Data { get; }
        IOrigin<T> Extract();
    }
}