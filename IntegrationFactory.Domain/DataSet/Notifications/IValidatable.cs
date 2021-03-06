using System.Collections.Generic;

namespace IntegrationFactory.Domain.DataSet.Notifications
{
    public interface IValidatable
    {
        bool Valid { get; }
        List<string> Notifications { get; }
        void AddNotification(string notification);
        void Validate();
    }
}