using AzureServiceBusClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Notification.Domain.Request;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Notific.ation.Client
{
    public class NotificationClient : INotificationClient
    {
        private readonly IConfiguration config;
        private readonly HttpClient httpClient;
        private readonly ILogger log;
        private readonly IBusClient busClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationServiceClient"/> class.
        /// </summary>
        /// <param name="httpClient">the http client factory.</param>
        /// <param name="config">the app config.</param>
        public NotificationClient(HttpClient httpClient, IConfiguration config, ILogger log, IBusClient busClient)
        {
            this.httpClient = httpClient;
            this.config = config;
            this.busClient = busClient;
        }

        Task INotificationClient.SendMessage(NotificationRequest request)
        {
            try
            {
                this.log.LogInformation("queue message");
                this.busClient.SendMessage(request);
            }
            catch (Exception ex)
            {
                this.log.LogError(ex.Message);
            }
            return Task.FromResult(true);
        }
    }
}