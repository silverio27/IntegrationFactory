using System;
using System.Data;
using IntegrationFactory.Domain.DataSet.Contracts;
using IntegrationFactory.Domain.DataSet.Notifications;
using IntegrationFactory.Domain.Extensions;

namespace IntegrationFactory.Domain.PipeLine
{
    public class PipeLineContext<T> : Validatable, IPipeLine<T>
    {
        public IOrigin<T> Origin { get; private set; }

        public IDestiny Destiny { get; private set; }

        public DataTable DataToLoad { get; private set; }

        public IPipeLine<T> SetOrigin(IOrigin<T> origin)
        {
            Origin = origin;
            Origin.Validate();
            return this;
        }

        public IPipeLine<T> SetDestiny(IDestiny destiny)
        {
            Destiny = destiny;
            Destiny.Validate();
            return this;
        }

        public override void Validate()
        {
            if (!Origin.Valid)
                this.Notifications.AddRange(Origin.Notifications);

            if (!Destiny.Valid)
                this.Notifications.AddRange(Destiny.Notifications);
        }

        public IPipeLine<T> Extract()
        {
            if (!this.Valid)
                return this;

            Origin.Extract();
            return this;
        }
        public IPipeLine<T> Transform<D>(Func<T, D> mapping = null)
        {
            DataToLoad = Origin.Data.Transform<T, D>(mapping);
            return this;
        }

        public IPipeLine<T> Load()
        {
            if (!this.Valid)
                return this;

            Destiny.Load(DataToLoad);
            return this;
        }


    }
}