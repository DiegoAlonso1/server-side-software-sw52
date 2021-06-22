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
using UltimateTeamApi.Services;

namespace UltimateTeamApi.UnitTest
{
    class GroupMemberServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        //ASSIGN GROUP MEMBER WHEN VALID GROUP MEMBER
        [Test]
        public async Task AssignGroupMemberAsyncWhenValidGroupMemberReturnsGroupMember()
        {
            //Arrange
            var mockGroupMemberRepository = GetDefaultIGroupMemberRepositoryInstance();
            var mockGroupRepository = GetDefaultIGroupRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            GroupMember groupMember = new GroupMember { UserId = 1, GroupId = 1, UserCreator = true };
            var groupId = groupMember.GroupId;
            var userId = groupMember.UserId;
            bool userCreator = groupMember.UserCreator;

            mockGroupMemberRepository.Setup(r => r.AssignGroupMemberAsync(groupId, userId, userCreator))
                .Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);
            mockGroupMemberRepository.Setup(r => r.FindByGroupIdAndUserIdAsync(groupId, userId))
                .Returns(Task.FromResult(groupMember));

            var service = new GroupMemberService(mockGroupMemberRepository.Object, mockUnitOfWork.Object, mockGroupRepository.Object, mockUserRepository.Object);

            //Act
            var result = await service.AssignGroupMemberAsync(groupId, userId, userCreator);
            var groupMemberResult = result.Resource;

            //Assert
            groupMemberResult.Should().Be(groupMember);
        }



        //ASSIGN GROUP MEMBER WHEN GROUP MEMBER ALREADY ASSIGNED
        [Test]
        public async Task AssignGroupMemberAsyncWhenGroupMemberAlreadyAssignedReturnsGroupMemberErrorResponse()
        {
            //Arrange
            var mockGroupMemberRepository = GetDefaultIGroupMemberRepositoryInstance();
            var mockGroupRepository = GetDefaultIGroupRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            GroupMember groupMember = new GroupMember {  UserId = 1, GroupId = 1, UserCreator = true };
            var groupId = groupMember.GroupId;
            var userId = groupMember.UserId;
            bool userCretor = groupMember.UserCreator;

            mockGroupMemberRepository.Setup(r => r.AssignGroupMemberAsync(groupId, userId, userCretor))
                .Throws(new Exception("Group and user have already been asigned."));

            var service = new GroupMemberService(mockGroupMemberRepository.Object, mockUnitOfWork.Object, mockGroupRepository.Object, mockUserRepository.Object);

            //Act
            var result = await service.AssignGroupMemberAsync(groupId, userId, userCretor);
            var message = result.Message;

            //Assert
            message.Should().Be("An error ocurred while assigning Member to Group: Group and user have already been asigned.");
        }



        //DEFAULTS
        private Mock<IGroupMemberRepository> GetDefaultIGroupMemberRepositoryInstance()
        {
            return new Mock<IGroupMemberRepository>();
        }
        private Mock<IGroupRepository> GetDefaultIGroupRepositoryInstance()
        {
            return new Mock<IGroupRepository>();
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
