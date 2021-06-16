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
    class AdministratorServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        //GET ADMIN BY ID WHEN ADMIN ID IS VALID
        [Test]
        public async Task GetByIdAsyncWhenValidAdministratorReturnsAdministrator()
        {
            //Arange
            var MockAdminRepository = GetDeafultIAdministratorRepositoryInstance();
            var MockUnitOfWork = GetDeafultIUnitOfWorkInstance();
            Administrator admin = new Administrator { Id = 5};

            MockAdminRepository.Setup(r => r.FindByIdAsync(admin.Id))
                .Returns(Task.FromResult(admin));

            var service = new AdministratorService(MockAdminRepository.Object, MockUnitOfWork.Object);

            //Act
            var result = await service.GetByIdAsync(admin.Id);
            var resource = result.Resource;

            //Assert
            resource.Should().Be(admin);
        }

        //GET ADMIN BY ID WHEN ADMIN ID IS NOT VALID
        [Test]
        public async Task GetByIdAsyncWhenInvalidAdministratorReturnsAdminNotFoundResponse()
        {
            //Arange
            var MockAdminRepository = GetDeafultIAdministratorRepositoryInstance();
            var MockUnitOfWork = GetDeafultIUnitOfWorkInstance();            
            var adminId = 5;
            MockAdminRepository.Setup(r => r.FindByIdAsync(adminId))
                .Returns(Task.FromResult<Administrator>(null));

            var service = new AdministratorService(MockAdminRepository.Object, MockUnitOfWork.Object);

            //Act
            var result = await service.GetByIdAsync(adminId);
            var message = result.Message;

            //Assert
            message.Should().Be("Administrator not found");
        }
        //DEFAULTS
        private Mock<IAdministratorRepository> GetDeafultIAdministratorRepositoryInstance()
        {
            return new Mock<IAdministratorRepository>();
        }
        private Mock<IUnitOfWork> GetDeafultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
