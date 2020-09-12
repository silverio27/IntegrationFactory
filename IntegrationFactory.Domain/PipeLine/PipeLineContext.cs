using System;
using System.Collections.Generic;
using IntegrationFactory.Domain.DataSet;
using IntegrationFactory.Domain.Extensions;

namespace IntegrationFactory.Domain.PipeLine
{
    public class PipeLineContext<O> : IPipeLine<O>
    {
        public PipeLineContext(IOrigin<O> origin, IDestiny destiny, List<Map> map)
        {
            Origin = origin;
            Destiny = destiny;
            Map = map;
            Notifications = new List<string>();
        }

        public IOrigin<O> Origin { get; private set; }

        public IDestiny Destiny { get; private set; }

        public List<Map> Map { get; private set; }

        public IEnumerable<O> OriginData { get; private set; }

        public List<string> Notifications { get; private set; }

        public IPipeLine<O> Get(List<Action> before = null, List<Action> after = null)
        {
            OriginData = Origin.Get();
            Notifications.Add("Dados Obtidos");
            return this;
        }

        public IPipeLine<O> Synk(List<Action> before = null, List<Action> after = null)
        {
            Destiny.MapToSynk(Map);
            var data = OriginData.ConvertToDataTable();
            var result =Destiny.Synk(data);
            Notifications.Add($"Dados Transferidos, {result.ToString()}");
            return this;
        }
    }
}