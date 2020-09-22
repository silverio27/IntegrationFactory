using System;
using IntegrationFactory.Domain.PipeLine;

namespace IntegrationFactory.Domain.ConsoleApp.Extensions
{
    public static class PipeLineExtensions
    {

        public static void WriteNotifications<T>(this IPipeLine<T> pipeLine)
        {
            foreach (var notification in pipeLine.Notifications)
                Console.WriteLine(notification);
        }

    }
}
