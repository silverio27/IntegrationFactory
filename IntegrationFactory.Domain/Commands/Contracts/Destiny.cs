using System.Collections.Generic;
using Flunt.Notifications;

namespace IntegrationFactory.Domain.Commands.Contracts
{
    public abstract class Destiny<T> : Notifiable, ICommand
    {
        public abstract void Validate();
        public abstract int Send<O>(IEnumerable<O> dataOrigin);
    }
}