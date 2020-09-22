using System;
using System.Collections.Generic;

namespace IntegrationFactory.Domain.DataSet
{
    public interface IOrigin<T> : IValidatable
    {
        IEnumerable<T> Get();
    }
}