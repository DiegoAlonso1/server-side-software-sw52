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

            var friendship = new Friendship { PrincipalId = 1, FriendId = 2 };
            var principalId = friendship.PrincipalId;
            var friendId = friendship.FriendId;

            mockFriendshipRepository.Setup(f => f.AssignFriendAsync(principalId, friendId))
                .Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);
            mockFriendshipRepository.Setup(f => f.FindByPrincipalIdAndFriendIdAsync(principalId, friendId))
                .Returns(Task.FromResult(friendship));

            var service = new FriendshipService(mockFriendshipRepository.Object, mockUnitOfWork.Object);

            //Act
            FriendshipResponse result = await service.AssignFriendAsync(principalId, friendId);
            Friendship eventAssistanceResult = result.Resource;

            //Assert
            eventAssistanceResult.Should().Be(friendship);
        }

        [Test]
        public async Task AssignFriendshipWhenInvalidPrincipalIdOrFriendReturnsFriendshipExceptionResponse()
        {
            //Arrange
            var mockFriendshipRepository = GetDefaultIFriendshipRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var principalId = 1;
            var friendId = 1;

            mockFriendshipRepository.Setup(f => f.AssignFriendAsync(principalId, friendId))
                .Returns(Task.FromResult<Friendship>(null));
            mockUnitOfWork.Setup(f => f.CompleteAsync())
                .Throws(new Exception());

            var service = new FriendshipService(mockFriendshipRepository.Object, mockUnitOfWork.Object);

            //Act
            FriendshipResponse result = await service.AssignFriendAsync(principalId, friendId);
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

            var friendship = new Friendship { PrincipalId = 1, FriendId = 2};
            var principalId = friendship.PrincipalId;
            var friendId = friendship.FriendId;

            mockFriendshipRepository.Setup(f => f.UnassignFriendAsync(principalId, friendId))
                .Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);
            mockFriendshipRepository.Setup(f => f.FindByPrincipalIdAndFriendIdAsync(principalId, friendId))
                .Returns(Task.FromResult(friendship));

            var service = new FriendshipService(mockFriendshipRepository.Object, mockUnitOfWork.Object);

            //Act
            FriendshipResponse result = await service.UnassignFriendAsync(principalId, friendId);
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

            var principalId = 1;
            var friendId = 2;

            mockFriendshipRepository.Setup(f => f.UnassignFriendAsync(principalId, friendId))
                .Returns(Task.FromResult<Friendship>(null));
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Throws(new Exception());

            var service = new FriendshipService(mockFriendshipRepository.Object, mockUnitOfWork.Object);

            //Act
            FriendshipResponse result = await service.UnassignFriendAsync(principalId, friendId);
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
