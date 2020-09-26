using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IntegrationFactory.Domain.DataSet.Contracts;
using IntegrationFactory.Domain.DataSet.Notifications;

namespace IntegrationFactory.Domain.DataSet.PlainText
{
    public class PlainTextOrigin<T> : Validatable, IPlainTextOrigin<T>
    {
        public string Path { get; private set; }

        public Func<string[], T> Mapping { get; private set; }

        public char Separator { get; private set; }

        public IEnumerable<T> Data { get; private set; }

        public IPlainTextOrigin<T> SetPath(string path)
        {
            Path = path;
            return this;
        }

        public IPlainTextOrigin<T> SetMapping(Func<string[], T> mapping)
        {
            Mapping = mapping;
            return this;
        }

        public IPlainTextOrigin<T> SetSeparator(char separator)
        {
            Separator = separator;
            return this;
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Path))
                AddNotification("O caminho não pode ser vazio ou nulo.");

            var fileExist = File.Exists(Path);

            if (!fileExist)
                AddNotification("O arquivo não existe no local indicado.");

            if (fileExist)
                if (new FileInfo(Path).Length == 0)
                    AddNotification("O Arquivo não pode estar vazio.");

            if (Separator == '\0')
                AddNotification("O separador não é válido.");

            if (Mapping == null)
                AddNotification("O mapeamento não pode ser nulo.");
        }

        public IOrigin<T> Extract()
        {
            this.Data = File.ReadAllLines(Path)
                 .Select(a => a.Split(Separator))
                 .Select(Mapping)
                 .ToList();
            return this;
        }

    }
}