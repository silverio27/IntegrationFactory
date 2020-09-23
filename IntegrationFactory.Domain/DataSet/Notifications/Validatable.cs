using System.Collections.Generic;

namespace IntegrationFactory.Domain.DataSet.Notifications
{
    public abstract class Validatable : IValidatable
    {
        public bool Valid
        {
            get
            {
                return Notifications.Count == 0;
            }
        }

        public List<string> Notifications { get; private set; } = new List<string>();

        public void AddNotification(string notification) => Notifications.Add(notification);

        public abstract void Validate();
    }
}