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
        private readonly IPersonRepository _personRepository;
        public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork, IPersonRepository personRepository)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
        }

        public async Task<NotificationResponse> DeleteAsync(int notificationId, int personId)
        {
            var existingNotification = await _notificationRepository.FindByIdAsync(personId);

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

        public async Task<NotificationResponse> GetByIdAndPersonIdAsync(int notificationId, int personId)
        {
            var existingPerson = await _personRepository.FindByIdAsync(personId);
            if (existingPerson == null)
                return new NotificationResponse("Person not found");

            var existingNotification = await _notificationRepository.FindByIdAsync(notificationId);
            if (existingNotification == null)
                return new NotificationResponse("Notification not found");

            if (!existingPerson.NotificationsReceived.Contains(existingNotification))
                return new NotificationResponse("Notification not found by Person with Id: " + personId);

            return new NotificationResponse(existingNotification);
        }

        public async Task<NotificationResponse> SaveAsync(int personId, Notification notification)
        {
            var existingPerson = await _personRepository.FindByIdAsync(personId);
            if (existingPerson == null)
                return new NotificationResponse("Person not found");

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
