using System.Linq.Expressions;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (string.IsNullOrEmpty(Path))
                AddNotification("O caminho não pode ser vazio ou nulo.");

            var fileExist = File.Exists(Path);

            bool fileEmpty = false;

            if (!fileExist)
                AddNotification("O arquivo não existe no local indicado.");

            if (fileExist)
            {
                fileEmpty = new FileInfo(Path).Length == 0;
                if (fileEmpty)
                    AddNotification("O Arquivo não pode estar vazio.");
            }


            if (Mapping == null)
                AddNotification("O mapeamento não pode ser nulo.");

            if (string.IsNullOrEmpty(Descendants))
                AddNotification("Descendentes não pode ser nulo ou vazio.");

            if (!string.IsNullOrEmpty(Descendants) && !fileEmpty)
                if (!XElement.Load(Path).Descendants(Descendants).Any())
                    AddNotification("O descendente não existe no arquivo.");
        }

        public IOrigin<T> Extract()
        {

            Data = XElement.Load(Path).Descendants(Descendants)
               .Select(Mapping);
            return this;
        }

    }
}