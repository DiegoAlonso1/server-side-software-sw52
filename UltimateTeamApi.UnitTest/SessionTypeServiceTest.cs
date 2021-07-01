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
    class SessionTypeServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        //GET SESSION TYPE BY ID ASYNC WHEN VALID SESSION TYPE
        [Test]
        public async Task GetByIdAsyncWhenValidSessionTypeReturnsSessionType()
        {
            //Arrange
            var mockSessionTypeRepository = GetDefaultISessionTypeRepositoryInstance();
            SessionType sessionType = new SessionType { Id = 1 };
            var sessionTypeId = sessionType.Id;

            mockSessionTypeRepository.Setup(r => r.FindByIdAsync(sessionTypeId))
                .Returns(Task.FromResult(sessionType));

            var service = new SessionTypeService(mockSessionTypeRepository.Object);

            //Act
            var result = await service.GetByIdAsync(sessionTypeId);
            var sessionTypeResult = result.Resource;

            //Assert
            sessionTypeResult.Should().Be(sessionType);
        }



        //GET SESSION TYPE BY ID ASYNC WHEN NO SESSION TYPE FOUND
        [Test]
        public async Task GetByIdAsyncWhenNoSessionTypeFoundReturnsSessionTypeNotFoundResponse()
        {
            //Arrange
            var mockSessionTypeRepository = GetDefaultISessionTypeRepositoryInstance();
            var sessionTypeId = 1;

            mockSessionTypeRepository.Setup(r => r.FindByIdAsync(sessionTypeId))
                .Returns(Task.FromResult<SessionType>(null));

            var service = new SessionTypeService(mockSessionTypeRepository.Object);

            //Act
            var result = await service.GetByIdAsync(sessionTypeId);
            var message = result.Message;

            //Assert
            message.Should().Be("SessionType not found");
        }



        //DEFAULTS
        private Mock<ISessionTypeRepository> GetDefaultISessionTypeRepositoryInstance()
        {
            return new Mock<ISessionTypeRepository>();
        }
    }
}
