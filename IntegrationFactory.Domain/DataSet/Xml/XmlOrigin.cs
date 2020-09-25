using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using IntegrationFactory.Domain.DataSet.Contracts;
using IntegrationFactory.Domain.DataSet.Notifications;

namespace IntegrationFactory.Domain.DataSet.Xml
{
    public class XmlOrigin<T> : Validatable, IOrigin<T>
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
        public IEnumerable<T> Extract()
        {
            return XElement.Load(_path).Descendants(_descendants)
             .Select(_mapping);
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }

        public void Transform()
        {
            throw new NotImplementedException();
        }
    }
}