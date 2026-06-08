using Domain.Entities;
using Infrastructure.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public Task CreateNotificationAsync(Notification notification, int trainingId)
        {
            throw new NotImplementedException();
        }

        public Task CreateNotificationForUserAsync(Notification notification, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notification>> GetNotificationsForTrainingAsync(int trainingId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notification>> GetNotificationsForUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task MarkNotificationAsReadAsync(Guid notificationId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
