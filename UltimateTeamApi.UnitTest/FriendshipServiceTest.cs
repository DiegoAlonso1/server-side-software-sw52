using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Persistance.Repositories;
using UltimateTeamApi.Domain.Services.Communications;
using UltimateTeamApi.Services;

namespace UltimateTeamApi.UnitTest
{
    class FriendshipServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task AssignFriendshipWhenValidFriendshipReturnsFriendshipResponse()
        {
            //Arrange
            var mockFriendshipRepository = GetDefaultIFriendshipRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var friendship = new Friendship { User1Id = 1, User2Id = 2 };
            var user1Id = friendship.User1Id;
            var user2Id = friendship.User2Id;

            mockFriendshipRepository.Setup(f => f.AssignFriendAsync(user1Id, user2Id))
                .Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);
            mockFriendshipRepository.Setup(f => f.FindByUser1IdAndUser2IdAsync(user1Id, user2Id))
                .Returns(Task.FromResult(friendship));

            var service = new FriendshipService(mockFriendshipRepository.Object, mockUnitOfWork.Object);

            //Act
            FriendshipResponse result = await service.AssignFriendAsync(user1Id, user2Id);
            Friendship eventAssistanceResult = result.Resource;

            //Assert
            eventAssistanceResult.Should().Be(friendship);
        }

        [Test]
        public async Task AssignFriendshipWhenInvalidUser1IdOrUser2IdReturnsFriendshipExceptionResponse()
        {
            //Arrange
            var mockFriendshipRepository = GetDefaultIFriendshipRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var user1Id = 1;
            var user2Id = 1;

            mockFriendshipRepository.Setup(f => f.AssignFriendAsync(user1Id, user2Id))
                .Returns(Task.FromResult<Friendship>(null));
            mockUnitOfWork.Setup(f => f.CompleteAsync())
                .Throws(new Exception());

            var service = new FriendshipService(mockFriendshipRepository.Object, mockUnitOfWork.Object);

            //Act
            FriendshipResponse result = await service.AssignFriendAsync(user1Id, user2Id);
            var message = result.Message;

            //Assert
            message.Should().Be("An error ocurred while assigning a Friendship: Exception of type 'System.Exception' was thrown.");
        }

        [Test]
        public async Task UnassignFriendshipWhenValidFriendshipReturnsFriendshipResponse()
        {
            //Arrange
            var mockFriendshipRepository = GetDefaultIFriendshipRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var friendship = new Friendship { User1Id = 1, User2Id = 2};
            var user1Id = friendship.User1Id;
            var user2Id = friendship.User2Id;

            mockFriendshipRepository.Setup(f => f.UnassignFriendAsync(user1Id, user2Id))
                .Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);
            mockFriendshipRepository.Setup(f => f.FindByUser1IdAndUser2IdAsync(user1Id, user2Id))
                .Returns(Task.FromResult(friendship));

            var service = new FriendshipService(mockFriendshipRepository.Object, mockUnitOfWork.Object);

            //Act
            FriendshipResponse result = await service.UnassignFriendAsync(user1Id, user2Id);
            Friendship eventAssistanceResult = result.Resource;

            //Assert
            eventAssistanceResult.Should().Be(friendship);
        }

        [Test]
        public async Task UnassignFriendshipWhenInvalidFriendshipReturnsFriendshipExceptionResponse()
        {
            //Arrange
            var mockFriendshipRepository = GetDefaultIFriendshipRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var user1Id = 1;
            var user2Id = 2;

            mockFriendshipRepository.Setup(f => f.UnassignFriendAsync(user1Id, user2Id))
                .Returns(Task.FromResult<Friendship>(null));
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Throws(new Exception());

            var service = new FriendshipService(mockFriendshipRepository.Object, mockUnitOfWork.Object);

            //Act
            FriendshipResponse result = await service.UnassignFriendAsync(user1Id, user2Id);
            var message = result.Message;

            //Assert
            message.Should().Be("An error ocurred while unassigning a Friendship: Exception of type 'System.Exception' was thrown.");
        }

        private Mock<IFriendshipRepository> GetDefaultIFriendshipRepositoryInstance()
        {
            return new Mock<IFriendshipRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
