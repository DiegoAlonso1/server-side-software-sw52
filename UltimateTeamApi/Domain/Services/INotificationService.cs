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
        Task<NotificationResponse> GetByIdAndPersonIdAsync(int notificationId, int personId);
        Task<NotificationResponse> SaveAsync(int personId, Notification notification);
        Task<NotificationResponse> DeleteAsync(int notificationId, int personId);
    }
}
