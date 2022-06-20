
using AzureServiceBusClient;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Notification.Domain.Request;
using Notification.Domain.Response;
using System.IO;
using System.Net;

using System.Threading.Tasks;

namespace Notification
{
    public class Notification
    {
        private readonly ILogger<Notification> log;
        private readonly BusClient busClient;
        

        public Notification(ILogger<Notification> log, BusClient busClient)
        {
            this.log = log;
            this.busClient = busClient;
        }

        [FunctionName("Notification")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Notification" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "Notification", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Notification** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<Response> Run(
            [Microsoft.Azure.Functions.Worker.HttpTrigger(AuthorizationLevel.Function, "post", Route = "/notification")] HttpRequestData req)
        {
            this.log.LogInformation("/notification");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            await busClient.SendMessage<NotificationRequest>(data);
            return new Response("sent");
        }
    }
}

