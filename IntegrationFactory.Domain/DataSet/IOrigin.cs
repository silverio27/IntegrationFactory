using System;
using System.Collections.Generic;

namespace IntegrationFactory.Domain.DataSet
{
    public interface IOrigin<T> : IDisposable
    {
        IEnumerable<T> Get();
    }
}