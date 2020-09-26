using System;
using IntegrationFactory.Domain.DataSet.Contracts;

namespace IntegrationFactory.Domain.DataSet.PlainText
{
    public interface IPlainTextOrigin<T> : IOrigin<T>
    {
        string Path { get; }
        Func<string[], T> Mapping { get; }
        char Separator { get; }

        IPlainTextOrigin<T> SetPath(string path);
        IPlainTextOrigin<T> SetSeparator(char separator);
        IPlainTextOrigin<T> SetMapping(Func<string[], T> mapping);

    }
}