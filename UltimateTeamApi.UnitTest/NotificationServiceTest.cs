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
            var mockPersonRepository = GetDefaultIPersonRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            Person person = new Person { Id = 2 };
            Notification notification = new Notification { Id = 1 };
            person.NotificationsReceived = new System.Collections.Generic.List<Notification>();
            person.NotificationsReceived.Add(notification);
            var notificationId = notification.Id;
            var personId = person.Id;

            mockPersonRepository.Setup(r => r.FindByIdAsync(personId))
                .Returns(Task.FromResult(person));

            mockNotificationRepository.Setup(r => r.FindByIdAsync(notificationId))
                .Returns(Task.FromResult(notification));



            var service = new NotificationService(mockNotificationRepository.Object, mockUnitOfWork.Object, mockPersonRepository.Object);

            //Act
            var result = await service.GetByIdAndPersonIdAsync(notificationId, personId);
            var notificationResult = result.Resource;

            //Assert
            notificationResult.Should().Be(notification);
        }



        //GET NOTIFICATION BY ID ASYNC WHEN NO NOTIFICATION FOUND
        [Test]
        public async Task GetByIdAsyncWhenNoNotificationFoundReturnsNotificationNotFoundResponse()
        {
            //Arrange
            var mockNotificationRepository = GetDefaultINotificationRepositoryInstance();
            var mockPersonRepository = GetDefaultIPersonRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            Person person = new Person { Id = 2 };
            var notificationId = 1;
            var personId = 2;

            mockPersonRepository.Setup(r => r.FindByIdAsync(personId))
                .Returns(Task.FromResult(person));

            mockNotificationRepository.Setup(r => r.FindByIdAsync(notificationId))
                .Returns(Task.FromResult<Notification>(null));

            var service = new NotificationService(mockNotificationRepository.Object, mockUnitOfWork.Object, mockPersonRepository.Object);

            //Act
            var result = await service.GetByIdAndPersonIdAsync(notificationId,personId);
            var message = result.Message;

            //Assert
            message.Should().Be("Notification not found");
        }

        //GET NOTIFICATION BY ID ASYNC WHEN NO PERSON FOUND
        [Test]
        public async Task GetByIdAsyncWhenNoPersonFoundReturnsPersonNotFoundResponse()
        {
            //Arrange
            var mockNotificationRepository = GetDefaultINotificationRepositoryInstance();
            var mockPersonRepository = GetDefaultIPersonRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            Person person = new Person { Id = 2 };
            var notificationId = 1;
            var personId = 2;

            mockPersonRepository.Setup(r => r.FindByIdAsync(personId))
                .Returns(Task.FromResult<Person>(null));

            var service = new NotificationService(mockNotificationRepository.Object, mockUnitOfWork.Object, mockPersonRepository.Object);

            //Act
            var result = await service.GetByIdAndPersonIdAsync(notificationId, personId);
            var message = result.Message;

            //Assert
            message.Should().Be("Person not found");
        }

        //DEFAULTS
        private Mock<INotificationRepository> GetDefaultINotificationRepositoryInstance()
        {
            return new Mock<INotificationRepository>();
        }
        private Mock<IPersonRepository> GetDefaultIPersonRepositoryInstance()
        {
            return new Mock<IPersonRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
