using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Persistance.Repositories;
using UltimateTeamApi.Domain.Services;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<NotificationResponse> DeleteAsync(int notificationId, int userId)
        {
            var existingNotification = await _notificationRepository.FindByIdAsync(userId);

            if (existingNotification == null)
                return new NotificationResponse("Notification not found");

            try
            {
                _notificationRepository.Remove(existingNotification);
                await _unitOfWork.CompleteAsync();

                return new NotificationResponse(existingNotification);
            }
            catch (Exception ex)
            {
                return new NotificationResponse($"An error ocurred while deleting the notification: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _notificationRepository.ListAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllByRemitendIdAsync(int remitendId)
        {
            return await _notificationRepository.ListByRemitendIdAsync(remitendId);
        }

        public async Task<IEnumerable<Notification>> GetAllBySenderIdAsync(int senderId)
        {
            return await _notificationRepository.ListBySenderIdAsync(senderId);
        }

        public async Task<NotificationResponse> GetByIdAndUserIdAsync(int notificationId, int userId)
        {
            var existingUser = await _userRepository.FindByIdAsync(userId);
            if (existingUser == null)
                return new NotificationResponse("User not found");

            var existingNotification = await _notificationRepository.FindByIdAsync(notificationId);
            if (existingNotification == null)
                return new NotificationResponse("Notification not found");

            if (!existingUser.NotificationsReceived.Contains(existingNotification))
                return new NotificationResponse("Notification not found by User with Id: " + userId);

            return new NotificationResponse(existingNotification);
        }

        public async Task<NotificationResponse> SaveAsync(int userId, Notification notification)
        {
            try
            {
                await _notificationRepository.AddAsync(notification);
                await _unitOfWork.CompleteAsync();

                return new NotificationResponse(notification);
            }
            catch (Exception ex)
            {
                return new NotificationResponse($"An error ocurred while saving the notification: {ex.Message}");
            }
        }
    }
}
