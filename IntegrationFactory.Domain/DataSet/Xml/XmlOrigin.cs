using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace IntegrationFactory.Domain.DataSet.PlainText
{
    public class XmlOrigin<T> : IOrigin<T>
    {
        string _path;
        Func<XElement, T> _mapping;

        string _descendants;
        public XmlOrigin(string path, Func<XElement, T> mapping)
        {
            _path = path;
            _mapping = mapping;
        }
        public XmlOrigin<T> SetDescendants(string descendants)
        {
            _descendants = descendants;
            return this;
        }
        public IEnumerable<T> Get()
        {
            return XElement.Load(_path).Descendants(_descendants)
             .Select(_mapping);
        }

        public void Dispose()
        {

        }
    }
}