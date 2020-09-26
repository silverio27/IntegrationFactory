using System;
using System.Xml.Linq;
using IntegrationFactory.Domain.DataSet.Contracts;

namespace IntegrationFactory.Domain.DataSet.Xml
{
    public interface IXmlOrigin<T> : IOrigin<T>
    {
        string Path { get; }
        Func<XElement, T> Mapping { get; }
        string Descendants { get; }

        IXmlOrigin<T> SetPath(string path);
        IXmlOrigin<T> SetMapping(Func<XElement, T> mapping);
        IXmlOrigin<T> SetDescendants(string descendants);

    }
}