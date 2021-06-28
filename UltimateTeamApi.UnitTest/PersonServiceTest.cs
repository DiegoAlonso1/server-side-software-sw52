using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Persistance.Repositories;
using UltimateTeamApi.Services;

namespace UltimateTeamApi.UnitTest
{
    class PersonServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        //GET PERSON BY ID ASYNC WHEN VALID PERSON
        [Test]
        public async Task GetByIdAsyncWhenValidpPersonReturnspPerson()
        {
            //Arrange
            var mockPersonRepository = GetDefaultIPersonRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            Person person = new Person { Id = 1 };
            var personId = person.Id;

            mockPersonRepository.Setup(r => r.FindByIdAsync(personId))
                .Returns(Task.FromResult(person));

            var service = new PersonService(mockPersonRepository.Object, mockUnitOfWork.Object);

            //Act
            var result = await service.GetByIdAsync(personId);
            var personResult = result.Resource;

            //Assert
            personResult.Should().Be(person);
        }



        //GET PERSON BY ID ASYNC WHEN NO PERSON FOUND
        [Test]
        public async Task GetByIdAsyncWhenNopPersonFoundReturnspPersonNotFoundResponse()
        {
            //Arrange
            var mockPersonRepository = GetDefaultIPersonRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var personId = 1;

            mockPersonRepository.Setup(r => r.FindByIdAsync(personId))
                .Returns(Task.FromResult<Person>(null));

            var service = new PersonService(mockPersonRepository.Object, mockUnitOfWork.Object);

            //Act
            var result = await service.GetByIdAsync(personId);
            var message = result.Message;

            //Assert
            message.Should().Be("Person not found");
        }



        //DEFAULTS
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
