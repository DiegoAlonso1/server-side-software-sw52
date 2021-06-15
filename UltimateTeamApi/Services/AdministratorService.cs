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
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdministratorService(IAdministratorRepository administratorRepository, IUnitOfWork unitOfWork)
        {
            _administratorRepository = administratorRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<AdministratorResponse> DeleteAsync(int administratorId)
        {
            var existingAdmin = await _administratorRepository.FindByIdAsync(administratorId);

            if (existingAdmin == null)
                return new AdministratorResponse("Administrator not found");

            try
            {
                _administratorRepository.Remove(existingAdmin);
                await _unitOfWork.CompleteAsync();

                return new AdministratorResponse(existingAdmin);
            }
            catch (Exception ex)
            {
                return new AdministratorResponse($"An error ocurred while deleting the administrator: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Administrator>> GetAllAsync()
        {
            return await _administratorRepository.ListAsync();
        }

        public async Task<IEnumerable<Administrator>> GetAllByAreaAsync(string area)
        {
            return await _administratorRepository.ListByAreaAsync(area);
        }

        public async Task<AdministratorResponse> GetByIdAsync(int administratorId)
        {
            var existingAdmin = await _administratorRepository.FindByIdAsync(administratorId);

            if (existingAdmin == null)
                return new AdministratorResponse("Administrator not found");

            return new AdministratorResponse(existingAdmin);
        }

        public async Task<AdministratorResponse> SaveAsync(Administrator administrator)
        {
            try
            {
                await _administratorRepository.AddAsync(administrator);
                await _unitOfWork.CompleteAsync();

                return new AdministratorResponse(administrator);
            }
            catch (Exception ex)
            {
                return new AdministratorResponse($"An error ocurred while saving the administrator: {ex.Message}");
            }
        }

        public async Task<AdministratorResponse> UpdateAsync(int administratorId, Administrator administratorRequest)
        {
            var existingAdmin = await _administratorRepository.FindByIdAsync(administratorId);

            if (existingAdmin == null)
                return new AdministratorResponse("Administrator not found");

            existingAdmin.Name = administratorRequest.Name;
            existingAdmin.Password = administratorRequest.Password;
            existingAdmin.Area = administratorRequest.Area;         

            try
            {
                _administratorRepository.Update(existingAdmin);
                await _unitOfWork.CompleteAsync();

                return new AdministratorResponse(existingAdmin);
            }
            catch (Exception ex)
            {
                return new AdministratorResponse($"An error ocurred while updating the administrator: {ex.Message}");
            }
        }
    }
}
