using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notification.Client.ClientExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification
{
    public class Program
    {
        public static async Task Main()
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("local.settings.json", optional: true)
                    .AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(context, services);
                })
                .ConfigureFunctionsWorkerDefaults((context, app) =>
                {
                    ConfigureFunctionsWorkerDefaults(context, app);
                })
                .Build();

            await host.RunAsync();
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            var configurations = context.Configuration;
            services.AddLogging();
            services.AddNotificationClient(configurations);
        }

        private static void ConfigureFunctionsWorkerDefaults(HostBuilderContext context, IFunctionsWorkerApplicationBuilder app)
        {
        }
    }
}
