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

        IPipeLine<O> Get(List<Action> before = null, List<Action> after = null);
        IPipeLine<O> Synk(List<Action> before = null, List<Action> after = null);


    }
}