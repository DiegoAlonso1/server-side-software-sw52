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
    class FunctionalityServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        //GET FUNCTIONALITY BY ID ASYNC WHEN VALID FUNCTIONALITY
        [Test]
        public async Task GetByIdAsyncWhenValidFunctionalityReturnsFunctionality()
        {
            //Arrange
            var mockFunctionalityRepository = GetDefaultIFunctionalityRepositoryInstance();
            Functionality functionality = new Functionality { Id = 1 };
            var functionalityId = functionality.Id;

            mockFunctionalityRepository.Setup(r => r.FindByIdAsync(functionalityId))
                .Returns(Task.FromResult(functionality));

            var service = new FunctionalityService(mockFunctionalityRepository.Object);

            //Act
            var result = await service.GetByIdAsync(functionalityId);
            var functionalityResult = result.Resource;

            //Assert
            functionalityResult.Should().Be(functionality);
        }



        //GET FUNCTIONALITY BY ID ASYNC WHEN NO FUNCTIONALITY FOUND
        [Test]
        public async Task GetByIdAsyncWhenNoFunctionalityFoundReturnsFunctionalityNotFoundResponse()
        {
            //Arrange
            var mockFunctionalityRepository = GetDefaultIFunctionalityRepositoryInstance();
            var functionalityId = 1;

            mockFunctionalityRepository.Setup(r => r.FindByIdAsync(functionalityId))
                .Returns(Task.FromResult<Functionality>(null));

            var service = new FunctionalityService(mockFunctionalityRepository.Object);

            //Act
            var result = await service.GetByIdAsync(functionalityId);
            var message = result.Message;

            //Assert
            message.Should().Be("Functionality not found");
        }



        //DEFAULTS
        private Mock<IFunctionalityRepository> GetDefaultIFunctionalityRepositoryInstance()
        {
            return new Mock<IFunctionalityRepository>();
        }
    }
}
