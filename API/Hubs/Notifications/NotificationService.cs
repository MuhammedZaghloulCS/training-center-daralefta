using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs.Notifications
{
    public class NotificationService
        : Application.Common.Abstraction
            .INotificationService
    {
        private readonly
            IHubContext<NotificationHub>
            _hubContext;

        public NotificationService(
            IHubContext<NotificationHub>
            hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendToGroupAsync(
            string group,
            string title,
            string message)
        {
            await _hubContext
                .Clients
                .Group(group)
                .SendAsync(
                    "ReceiveNotification",
                    new
                    {
                        title,
                        message
                    });
        }
    }
}
