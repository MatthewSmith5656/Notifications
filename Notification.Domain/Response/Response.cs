using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace Notification.Domain.Response
{
    public class Response
    {
        private string responseMessage;

        public Response(string responseMessage)
        {
            this.responseMessage = responseMessage;
        }
    }
}