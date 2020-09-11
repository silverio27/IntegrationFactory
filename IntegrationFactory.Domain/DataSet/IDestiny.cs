using System;

namespace IntegrationFactory.Domain.DataSet
{
    public interface IDestiny<T> : IDisposable
    {
        IResult<T> Synk(T data);
    }
}