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
    class SubscriptionBillServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        //GET SUBSCRIPTION BILL BY ID WHEN SUBSCRIPTION BILL ID IS VALID
        [Test]
        public async Task GetByIdAsyncWhenValidSubscriptionBillReturnsSubscriptionBill()
        {
            //Arange
            var MockSubscriptionBillRepository = GetDeafultISubscriptionBillRepositoryInstance();
            var MockUnitOfWork = GetDeafultIUnitOfWorkInstance();
            SubscriptionBill subcriptionBill = new SubscriptionBill { Id = 5 };

            MockSubscriptionBillRepository.Setup(r => r.FindByIdAsync(subcriptionBill.Id))
                .Returns(Task.FromResult(subcriptionBill));

            var service = new SubscriptionBillService(MockSubscriptionBillRepository.Object, MockUnitOfWork.Object);

            //Act
            var result = await service.GetByIdAsync(subcriptionBill.Id);
            var resource = result.Resource;

            //Assert
            resource.Should().Be(subcriptionBill);
        }

        //GET SUBSCRIPTION BILL BY ID WHEN SUBSCRIPTION BILL ID IS NOT VALID
        [Test]
        public async Task GetByIdAsyncWhenInvalidSubscriptionBillReturnsSubscriptionBillNotFoundResponse()
        {
            //Arange
            var MockSubscriptionBillRepository = GetDeafultISubscriptionBillRepositoryInstance();
            var MockUnitOfWork = GetDeafultIUnitOfWorkInstance();
            var subscriptionBillId = 2;
            MockSubscriptionBillRepository.Setup(r => r.FindByIdAsync(subscriptionBillId))
                .Returns(Task.FromResult<SubscriptionBill>(null));

            var service = new SubscriptionBillService(MockSubscriptionBillRepository.Object, MockUnitOfWork.Object);

            //Act
            var result = await service.GetByIdAsync(subscriptionBillId);
            var message = result.Message;

            //Assert
            message.Should().Be("Subscription Bill not found");
        }


        //DEFAULTS
        private Mock<ISubscriptionBillRepository> GetDeafultISubscriptionBillRepositoryInstance()
        {
            return new Mock<ISubscriptionBillRepository>();
        }
        private Mock<IUnitOfWork> GetDeafultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
