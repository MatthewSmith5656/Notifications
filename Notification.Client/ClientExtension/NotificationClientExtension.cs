using Microsoft.Extensions.DependencyInjection;
using System;
using AzureServiceBusClient;
using Notific.ation.Client;
using Microsoft.Extensions.Configuration;

namespace Notification.Client.ClientExtension
{
    public static class NotificationClientExtensions
    {
        public static void AddNotificationClient(this IServiceCollection services, IConfiguration configuration)
        {
            // TODO: move to constants file
            string serviceBaseUrl = configuration["ServiceBaseUrl"];

            if (configuration == null)
            {
                throw new ArgumentNullException("Host is missing some configurations");
            }

            services.AddHttpClient<INotificationClient, NotificationClient>(client =>
            {
                client.BaseAddress = new Uri(serviceBaseUrl);
            });
            string x = configuration["QueueName"];
            string y = configuration["ConnString"];

            services.AddTransient<INotificationClient, NotificationClient>();
            services.AddServiceBusClient(x, y, true);
        }
    }
}
