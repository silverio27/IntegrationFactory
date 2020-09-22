using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IntegrationFactory.Domain.DataSet.PlainText
{
    public class PlainTextOrigin<T> : IOrigin<T>
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
            return File.ReadAllLines(_path).Select(a => a.Split(_separator)).Select(_mapping
                ).ToList();
        }

        public void Dispose()
        {

        }
    }
}