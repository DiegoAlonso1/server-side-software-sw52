using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Persistance.Repositories;
using UltimateTeamApi.Domain.Services.Communications;
using UltimateTeamApi.Services;

namespace UltimateTeamApi.UnitTest
{
    class GroupServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoGroupReturnsEmptyCollection()
        {
            // Arrange

            var mockGroupRepository = GetDefaultIGroupRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            mockGroupRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Group>());

            var service = new GroupService(mockGroupRepository.Object, mockUnitOfWork.Object);

            // Act

            List<Group> result = (List<Group>)await service.ListAsync();
            var groupCount = result.Count;

            // Assert

            groupCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsCategoryNotFoundResponse()
        {
            // Arrange
            var mockGroupRepository = GetDefaultIGroupRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var groupId = 1;
            Group group = new Group();
            mockGroupRepository.Setup(r => r.FindByIdAsync(groupId)).Returns(Task.FromResult<Group>(null));
            var service = new GroupService(mockGroupRepository.Object, mockUnitOfWork.Object);

            // Act
            GroupResponse result = await service.GetByIdAsync(groupId);
            var message = result.Message;

            // Assert
            message.Should().Be("Group not found");
        }

        private Mock<IGroupRepository> GetDefaultIGroupRepositoryInstance()
        {
            return new Mock<IGroupRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
