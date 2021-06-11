using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;


namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> ListAsync();
        Task<IEnumerable<Notification>> ListBySenderIdAsync(int senderId);
        Task<IEnumerable<Notification>> ListByRemitendIdAsync(int remitendId);
        Task<Notification> FindByIdAsync(int notificationId);
        Task AddAsync(Notification notification);
        void Remove(Notification notification);
    }
}
