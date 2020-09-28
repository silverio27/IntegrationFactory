using System.Data;
using System;
using IntegrationFactory.Domain.DataSet.Contracts;
using IntegrationFactory.Domain.DataSet.Notifications;

namespace IntegrationFactory.Domain.PipeLine
{
    public interface IPipeLine<T> : IValidatable
    {
        IOrigin<T> Origin { get; }
        IDestiny Destiny { get; }
        DataTable DataToLoad { get; }
        IPipeLine<T> SetOrigin(IOrigin<T> origin);
        IPipeLine<T> SetDestiny(IDestiny destiny);
        IPipeLine<T> Extract();
        IPipeLine<T> Transform<D>(Func<T, D> mapping = null);
        IPipeLine<T> Load();

    }
}