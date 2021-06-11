using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Persistance.Contexts;
using UltimateTeamApi.Domain.Persistance.Repositories;

namespace UltimateTeamApi.Persistance.Repositories
{
    public class NotificationRepository : BaseRepository, INotificationRepository
    {
        public NotificationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }

        public async Task<Notification> FindByIdAsync(int notificationId)
        {
            return await _context.Notifications.FindAsync(notificationId);
        }

        public async Task<IEnumerable<Notification>> ListAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<IEnumerable<Notification>> ListByRemitendIdAsync(int remitendId)
        {
            return await _context.Notifications.Where(n => n.RemitendId == remitendId).ToListAsync();
        }

        public async Task<IEnumerable<Notification>> ListBySenderIdAsync(int senderId)
        {
            return await _context.Notifications.Where(n => n.SenderId == senderId).ToListAsync();
        }

        public void Remove(Notification notification)
        {
            _context.Notifications.Remove(notification);
        }
    }
}
