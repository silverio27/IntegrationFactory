using System;
using System.Collections.Generic;
using IntegrationFactory.Domain.DataSet;

namespace IntegrationFactory.Domain.PipeLine
{
    public interface IPipeLine<O>
    {
        IOrigin<O> Origin { get; }
        IDestiny Destiny { get; }
        List<Map> Map { get; }
        IEnumerable<O> OriginData { get; }
        List<string> Notifications { get; }

        IPipeLine<O> SetOrigin(IOrigin<O> origin);
        IPipeLine<O> SetDestiny(IDestiny destiny);
        IPipeLine<O> AddMap(string source, string target);
        IPipeLine<O> Get();
        IPipeLine<O> Synk();

        IPipeLine<O> OtherAction(Action action);


    }
}