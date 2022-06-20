using Notification.Domain.Request;
using System.Threading.Tasks;

namespace Notific.ation.Client
{
    public interface INotificationClient
    {
        Task SendMessage(NotificationRequest request);
    }
}