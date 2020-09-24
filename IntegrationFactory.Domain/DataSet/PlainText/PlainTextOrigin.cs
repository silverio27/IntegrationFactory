using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IntegrationFactory.Domain.DataSet.Contracts;
using IntegrationFactory.Domain.DataSet.Notifications;

namespace IntegrationFactory.Domain.DataSet.PlainText
{
    public class PlainTextOrigin<T> : Validatable, IOrigin<T>
    {
        string _path;
        Func<string[], T> _mapping;
        char _separator;

        public PlainTextOrigin(string path, Func<string[], T> mapping)
        {
            _path = path;
            _mapping = mapping;
        }

        public PlainTextOrigin<T> SetSeparator(char separator)
        {
            _separator = separator;
            return this;
        }

        public IEnumerable<T> Get()
        {
            return File.ReadAllLines(_path)
                .Select(a => a.Split(_separator))
                .Select(_mapping)
                .ToList();
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_path))
                AddNotification("O caminho não pode ser vazio ou nulo.");

            var fileExist = File.Exists(_path);

            if (!fileExist)
                AddNotification("O arquivo não existe no local indicado.");
                
            if (fileExist)
                if (new FileInfo(_path).Length == 0)
                    AddNotification("O Arquivo não pode estar vazio.");

            if (_separator == '\0')
                AddNotification("O separador não é válido.");

            if (_mapping == null)
                AddNotification("O mapeamento não pode ser nulo.");

        }
    }
}