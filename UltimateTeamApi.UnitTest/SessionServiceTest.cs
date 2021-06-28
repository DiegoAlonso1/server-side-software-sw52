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
    class SessionServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        //GET SESSION BY ID ASYNC WHEN VALID SESSION
        [Test]
        public async Task GetByIdAsyncWhenValidPersonReturnsPerson()
        {
            //Arrange
            var mockSessionRepository = GetDefaultISessionRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            Session session = new Session { Id = 1 };
            var sessionId = session.Id;

            mockSessionRepository.Setup(r => r.FindByIdAsync(sessionId))
                .Returns(Task.FromResult(session));

            var service = new SessionService(mockSessionRepository.Object, mockUnitOfWork.Object);

            //Act
            var result = await service.GetByIdAsync(sessionId);
            var sessionResult = result.Resource;

            //Assert
            sessionResult.Should().Be(session);
        }



        //GET SESSION BY ID ASYNC WHEN NO SESSION FOUND
        [Test]
        public async Task GetByIdAsyncWhenNoPersonFoundReturnsPersonNotFoundResponse()
        {
            //Arrange
            var mockSessionRepository = GetDefaultISessionRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var sessionId = 1;

            mockSessionRepository.Setup(r => r.FindByIdAsync(sessionId))
                .Returns(Task.FromResult<Session>(null));

            var service = new SessionService(mockSessionRepository.Object, mockUnitOfWork.Object);

            //Act
            var result = await service.GetByIdAsync(sessionId);
            var message = result.Message;

            //Assert
            message.Should().Be("Session not found");
        }



        //DEFAULTS
        private Mock<ISessionRepository> GetDefaultISessionRepositoryInstance()
        {
            return new Mock<ISessionRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
