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
    class SessionStadisticServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        //ASSIGN SESSION STADISTIC WHEN VALID SESSION STADISTIC
        [Test]
        public async Task AssignSessionStadisticAsyncWhenValidSessionStadisticReturnsSessionStadistic()
        {
            //Arrange
            var mockSessionStadisticRepository = GetDefaultISessionStadisticRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            SessionStadistic sessionStadistic = new SessionStadistic { SessionId = 1, FunctionalityId = 1 };
            var sessionId = sessionStadistic.SessionId;
            var functionalityId = sessionStadistic.FunctionalityId;

            mockSessionStadisticRepository.Setup(r => r.AssignSessionStadisticAsync(sessionId, functionalityId))
                .Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);
            mockSessionStadisticRepository.Setup(r => r.FindBySessionIdAndFunctionalityIdAsync(sessionId, functionalityId))
                .Returns(Task.FromResult(sessionStadistic));

            var service = new SessionStadisticService(mockSessionStadisticRepository.Object, mockUnitOfWork.Object);

            //Act
            var result = await service.AssignSessionStadisticAsync(sessionId, functionalityId);
            var sessionStadisticResult = result.Resource;

            //Assert
            sessionStadisticResult.Should().Be(sessionStadistic);
        }



        //ASSIGN SESSION STADISTIC WHEN SESSION STADISTIC ALREADY ASSIGNED
        [Test]
        public async Task AssignSessionStadisticAsyncWhenSessionStadisticAlreadyAssignedReturnsSessionStadisticErrorResponse()
        {
            //Arrange
            var mockSessionStadisticRepository = GetDefaultISessionStadisticRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            SessionStadistic sessionStadistic = new SessionStadistic { SessionId = 1, FunctionalityId = 1 };
            var sessionId = sessionStadistic.SessionId;
            var functionalityId = sessionStadistic.FunctionalityId;

            mockSessionStadisticRepository.Setup(r => r.AssignSessionStadisticAsync(sessionId, functionalityId))
                .Throws(new Exception("Session and functionality have already been asigned."));

            var service = new SessionStadisticService(mockSessionStadisticRepository.Object, mockUnitOfWork.Object);

            //Act
            var result = await service.AssignSessionStadisticAsync(sessionId, functionalityId);
            var message = result.Message;

            //Assert
            message.Should().Be("An error ocurred while assigning a SessionStadistic: Session and functionality have already been asigned.");
        }



        //DEFAULTS
        private Mock<ISessionStadisticRepository> GetDefaultISessionStadisticRepositoryInstance()
        {
            return new Mock<ISessionStadisticRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
