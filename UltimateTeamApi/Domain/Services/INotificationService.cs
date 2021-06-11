using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetAllAsync();
        Task<IEnumerable<Notification>> GetAllBySenderIdAsync(int senderId);
        Task<IEnumerable<Notification>> GetAllByRemitendIdAsync(int remitendId);
        Task<NotificationResponse> GetByIdAndUserIdAsync(int notificationId, int userId);
        Task<NotificationResponse> SaveAsync(int userId, Notification notification);
        Task<NotificationResponse> DeleteAsync(int notificationId, int userId);
    }
}
