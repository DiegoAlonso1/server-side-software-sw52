using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Persistance.Repositories;
using UltimateTeamApi.Services;

namespace UltimateTeamApi.UnitTest
{
    class NotificationServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        //GET NOTIFICATION BY ID ASYNC WHEN VALID NOTIFICATION
        [Test]
        public async Task GetByIdAsyncWhenValidNotificationReturnsNotification()
        {
            //Arrange
            var mockNotificationRepository = GetDefaultINotificationRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            User user = new User { Id = 2 };
            Notification notification = new Notification { Id = 1 };
            var notificationId = notification.Id;
            var userId = user.Id;

            mockNotificationRepository.Setup(r => r.FindByIdAsync(notificationId))
                .Returns(Task.FromResult(notification));

            var service = new NotificationService(mockNotificationRepository.Object, mockUnitOfWork.Object, mockUserRepository.Object);

            //Act
            var result = await service.GetByIdAndUserIdAsync(notificationId, userId);
            var userResult = result.Resource;

            //Assert
            userResult.Should().Be(notification);
        }



        //GET NOTIFICATION BY ID ASYNC WHEN NO NOTIFICATION FOUND
        [Test]
        public async Task GetByIdAsyncWhenNoNotificationFoundReturnsNotificationNotFoundResponse()
        {
            //Arrange
            var mockNotificationRepository = GetDefaultINotificationRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var notificationId = 1;
            var userId = 2;

            mockNotificationRepository.Setup(r => r.FindByIdAsync(notificationId))
                .Returns(Task.FromResult<Notification>(null));

            var service = new NotificationService(mockNotificationRepository.Object, mockUnitOfWork.Object, mockUserRepository.Object);

            //Act
            var result = await service.GetByIdAndUserIdAsync(notificationId,userId);
            var message = result.Message;

            //Assert
            message.Should().Be("Notification not found");
        }

        //DEFAULTS
        private Mock<INotificationRepository> GetDefaultINotificationRepositoryInstance()
        {
            return new Mock<INotificationRepository>();
        }
        private Mock<IUserRepository> GetDefaultIUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
