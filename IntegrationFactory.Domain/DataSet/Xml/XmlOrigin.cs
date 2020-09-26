using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using IntegrationFactory.Domain.DataSet.Contracts;
using IntegrationFactory.Domain.DataSet.Notifications;

namespace IntegrationFactory.Domain.DataSet.Xml
{
    public class XmlOrigin<T> : Validatable, IXmlOrigin<T>
    {
        public string Path { get; private set; }

        public Func<XElement, T> Mapping { get; private set; }

        public string Descendants { get; private set; }

        public IEnumerable<T> Data { get; private set; }

        public IXmlOrigin<T> SetPath(string path)
        {
            Path = path;
            return this;
        }

        public IXmlOrigin<T> SetDescendants(string descendants)
        {
            Descendants = descendants;
            return this;
        }

        public IXmlOrigin<T> SetMapping(Func<XElement, T> mapping)
        {
            Mapping = mapping;
            return this;
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }

        public IOrigin<T> Extract()
        {
            Data = XElement.Load(Path).Descendants(Descendants)
               .Select(Mapping);
            return this;
        }

    }
}