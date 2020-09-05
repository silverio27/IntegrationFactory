using System.Collections.Generic;
using Flunt.Notifications;

namespace IntegrationFactory.Domain.Commands.Contracts
{
    public abstract class Origin<O> : Notifiable, ICommand
    {
        public abstract void Validate();
        public abstract IEnumerable<O> Get();
    }
}