using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Abstraction
{
    public interface INotificationService
    {
        public Task SendToGroupAsync(
       string group,
       string title,
       string message);

        
    }
}
