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
    class SubscriptionTypeServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        //GET SUBSCRIPTION TYPE BY ID WHEN SUBSCRIPTION TYPE ID IS VALID
        [Test]
        public async Task GetByIdAsyncWhenValidSubscriptionTypeReturnsSubscriptionType()
        {
            //Arange
            var MockSubscriptionTypeRepository = GetDeafultISubscriptionTypeRepositoryInstance();
            var MockUnitOfWork = GetDeafultIUnitOfWorkInstance();
            SubscriptionType subcriptionType = new SubscriptionType { Id = 5 };

            MockSubscriptionTypeRepository.Setup(r => r.FindByIdAsync(subcriptionType.Id))
                .Returns(Task.FromResult(subcriptionType));

            var service = new SubscriptionTypeService(MockSubscriptionTypeRepository.Object, MockUnitOfWork.Object);

            //Act
            var result = await service.GetByIdAsync(subcriptionType.Id);
            var resource = result.Resource;

            //Assert
            resource.Should().Be(subcriptionType);
        }

        //GET SUBSCRIPTION TYPE BY ID WHEN SUBSCRIPTION TYPE ID IS NOT VALID
        [Test]
        public async Task GetByIdAsyncWhenInvalidSubscriptionTypeReturnsSubscriptionTypeNotFoundResponse()
        {
            //Arange
            var MockSubscriptionTypeRepository = GetDeafultISubscriptionTypeRepositoryInstance();
            var MockUnitOfWork = GetDeafultIUnitOfWorkInstance();
            var subscriptionTypeId = 2;
            MockSubscriptionTypeRepository.Setup(r => r.FindByIdAsync(subscriptionTypeId))
                .Returns(Task.FromResult<SubscriptionType>(null));

            var service = new SubscriptionTypeService(MockSubscriptionTypeRepository.Object, MockUnitOfWork.Object);

            //Act
            var result = await service.GetByIdAsync(subscriptionTypeId);
            var message = result.Message;

            //Assert
            message.Should().Be("Subscription Type not found");
        }

        //DEFAULTS
        private Mock<ISubscriptionTypeRepository> GetDeafultISubscriptionTypeRepositoryInstance()
        {
            return new Mock<ISubscriptionTypeRepository>();
        }
        private Mock<IUnitOfWork> GetDeafultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
