using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Persistance.Repositories;
using UltimateTeamApi.Domain.Services;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PersonResponse> DeleteAsync(int personId)
        {
            var existingPerson = await _personRepository.FindByIdAsync(personId);

            if (existingPerson == null)
                return new PersonResponse("Person not found");

            try
            {
                _personRepository.Remove(existingPerson);
                await _unitOfWork.CompleteAsync();

                return new PersonResponse(existingPerson);
            }
            catch (Exception ex)
            {
                return new PersonResponse($"An error ocurred while deleting the person: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _personRepository.ListAsync();
        }

        public async Task<IEnumerable<Person>> GetAllByAdministratorIdAsync(int administratorId)
        {
            return await _personRepository.ListByAdministratorIdAsync(administratorId);
        }

        public async Task<PersonResponse> GetByIdAsync(int personId)
        {
            var existingPerson = await _personRepository.FindByIdAsync(personId);

            if (existingPerson == null)
                return new PersonResponse("Person not found");

            return new PersonResponse(existingPerson);
        }

        public async Task<PersonResponse> GetByEmailAsync(string personEmail)
        {
            var existingPerson = await _personRepository.FindByEmailAsync(personEmail);

            if (existingPerson == null)
                return new PersonResponse("Person not found");

            return new PersonResponse(existingPerson);
        }

        public async Task<PersonResponse> SaveAsync(Person person)
        {
            try
            {
                await _personRepository.AddAsync(person);
                await _unitOfWork.CompleteAsync();

                return new PersonResponse(person);
            }
            catch (Exception ex)
            {
                return new PersonResponse($"An error ocurred while saving the person: {ex.Message}");
            }
        }

        public async Task<PersonResponse> UpdateAsync(int personId, Person personRequest)
        {
            var existingPerson = await _personRepository.FindByIdAsync(personId);

            if (existingPerson == null)
                return new PersonResponse("Person not found");

            existingPerson.Name = personRequest.Name;
            existingPerson.LastName = personRequest.LastName;
            existingPerson.UserName = personRequest.UserName;
            existingPerson.Email = personRequest.Email;
            existingPerson.Birthdate = personRequest.Birthdate;
            existingPerson.LastConnection = personRequest.LastConnection;
            existingPerson.ProfilePicture = personRequest.ProfilePicture;
            existingPerson.AdministratorId = personRequest.AdministratorId;

            try
            {
                _personRepository.Update(existingPerson);
                await _unitOfWork.CompleteAsync();

                return new PersonResponse(existingPerson);
            }
            catch (Exception ex)
            {
                return new PersonResponse($"An error ocurred while updating the person: {ex.Message}");
            }
        }
    }
}
