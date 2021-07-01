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
            var mockPersonRepository = GetDefaultIPersonRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            GroupMember groupMember = new GroupMember { PersonId = 1, GroupId = 1, PersonCreator = true };
            var groupId = groupMember.GroupId;
            var personId = groupMember.PersonId;
            bool personCreator = groupMember.PersonCreator;

            mockGroupMemberRepository.Setup(r => r.AssignGroupMemberAsync(groupId, personId, personCreator))
                .Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);
            mockGroupMemberRepository.Setup(r => r.FindByGroupIdAndPersonIdAsync(groupId, personId))
                .Returns(Task.FromResult(groupMember));

            var service = new GroupMemberService(mockGroupMemberRepository.Object, mockUnitOfWork.Object, mockGroupRepository.Object, mockPersonRepository.Object);

            //Act
            var result = await service.AssignGroupMemberAsync(groupId, personId, personCreator);
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
            var mockPersonRepository = GetDefaultIPersonRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            GroupMember groupMember = new GroupMember {  PersonId = 1, GroupId = 1, PersonCreator = true };
            var groupId = groupMember.GroupId;
            var personId = groupMember.PersonId;
            bool personCretor = groupMember.PersonCreator;

            mockGroupMemberRepository.Setup(r => r.AssignGroupMemberAsync(groupId, personId, personCretor))
                .Throws(new Exception("Group and person have already been asigned."));

            var service = new GroupMemberService(mockGroupMemberRepository.Object, mockUnitOfWork.Object, mockGroupRepository.Object, mockPersonRepository.Object);

            //Act
            var result = await service.AssignGroupMemberAsync(groupId, personId, personCretor);
            var message = result.Message;

            //Assert
            message.Should().Be("An error ocurred while assigning Member to Group: Group and person have already been asigned.");
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
