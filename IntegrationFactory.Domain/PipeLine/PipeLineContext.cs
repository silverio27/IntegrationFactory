using System;
using System.Collections.Generic;
using IntegrationFactory.Domain.DataSet;
using IntegrationFactory.Domain.DataSet.Contracts;
using IntegrationFactory.Domain.Extensions;

namespace IntegrationFactory.Domain.PipeLine
{
    public class PipeLineContext<O> : IPipeLine<O>
    {
        public IOrigin<O> Origin { get; private set; }

        public IDestiny Destiny { get; private set; }

        public List<Map> Map { get; private set; } = new List<Map>();

        public IEnumerable<O> OriginData { get; private set; }

        public List<string> Notifications { get; private set; } = new List<string>();
        public Result Result { get; private set; }

        public IPipeLine<O> SetOrigin(IOrigin<O> origin)
        {
            Origin = origin;
            return this;
        }

        public IPipeLine<O> SetDestiny(IDestiny destiny)
        {
            Destiny = destiny;
            return this;
        }

        public IPipeLine<O> AddMap(string source, string target)
        {
            Map.Add(new Map(source, target));
            return this;
        }

        public IPipeLine<O> Get()
        {
            OriginData = Origin.Extract();
            Notifications.Add("Dados Obtidos");
            return this;
        }

        public IPipeLine<O> Synk()
        {
            Destiny.MapToSynk(Map);
            var data = OriginData.ConvertToDataTable();
            Result = Destiny.Load(data);
            Notifications.Add($"Dados Transferidos, {Result.ToString()}");
            return this;
        }

        public IPipeLine<O> OtherAction(Action action)
        {
            action();
            return this;
        }
    }
}