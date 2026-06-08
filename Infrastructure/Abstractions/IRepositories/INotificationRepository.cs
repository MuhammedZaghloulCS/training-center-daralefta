using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface INotificationRepository
    {
        public Task<List<Notification>> GetNotificationsForUserAsync(Guid userId);
        public Task<List<Notification>> GetNotificationsForTrainingAsync(int trainingId);
        public Task MarkNotificationAsReadAsync(Guid notificationId, Guid userId);
        public Task CreateNotificationAsync(Notification notification, int trainingId);
        public Task CreateNotificationForUserAsync(Notification notification, Guid userId);
    }
}
