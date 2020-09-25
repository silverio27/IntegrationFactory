using System;
using System.Collections.Generic;
using IntegrationFactory.Domain.DataSet;
using IntegrationFactory.Domain.DataSet.Contracts;

namespace IntegrationFactory.Domain.PipeLine
{
    public interface IPipeLine<O>
    {
        IOrigin<O> Origin { get; }
        IDestiny Destiny { get; }

        /// <summary>
        /// Mapeamento para cópia de dados.
        /// <para> Um de para das colunas de origem para as de destino. </para>
        /// <para> Onde source é igual a origem e target a coluna de destino.</para>
        /// </summary>
        /// <value>Obtém as colunas que foram mapeadas</value>
        List<Map> Map { get; }
        IEnumerable<O> OriginData { get; }
        List<string> Notifications { get; }

        Result Result { get; }

        IPipeLine<O> SetOrigin(IOrigin<O> origin);
        IPipeLine<O> SetDestiny(IDestiny destiny);
        IPipeLine<O> AddMap(string source, string target);
        IPipeLine<O> Get();
        IPipeLine<O> Synk();

        IPipeLine<O> OtherAction(Action action);


    }
}