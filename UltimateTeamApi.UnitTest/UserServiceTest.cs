using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Persistance.Repositories;
using UltimateTeamApi.Services;

namespace UltimateTeamApi.UnitTest
{
    class UserServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        //GET USER BY ID ASYNC WHEN VALID USER
        [Test]
        public async Task GetByIdAsyncWhenValidUserReturnsUser()
        {
            //Arrange
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            User user = new User { Id = 1 };
            var userId = user.Id;

            mockUserRepository.Setup(r => r.FindByIdAsync(userId))
                .Returns(Task.FromResult(user));

            var service = new UserService(mockUserRepository.Object, mockUnitOfWork.Object);

            //Act
            var result = await service.GetByIdAsync(userId);
            var userResult = result.Resource;

            //Assert
            userResult.Should().Be(user);
        }



        //GET USER BY ID ASYNC WHEN NO USER FOUND
        [Test]
        public async Task GetByIdAsyncWhenNoUserFoundReturnsUserNotFoundResponse()
        {
            //Arrange
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var userId = 1;

            mockUserRepository.Setup(r => r.FindByIdAsync(userId))
                .Returns(Task.FromResult<User>(null));

            var service = new UserService(mockUserRepository.Object, mockUnitOfWork.Object);

            //Act
            var result = await service.GetByIdAsync(userId);
            var message = result.Message;

            //Assert
            message.Should().Be("User not found");
        }



        //DEFAULTS
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
