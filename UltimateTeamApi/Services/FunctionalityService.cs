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
    public class FunctionalityService : IFunctionalityService
    {
        private readonly IFunctionalityRepository _functionalityRepository;

        public FunctionalityService(IFunctionalityRepository functionalityRepository)
        {
            _functionalityRepository = functionalityRepository;
        }

        public async Task<IEnumerable<Functionality>> GetAllAsync()
        {
            return await _functionalityRepository.ListAsync();
        }

        public async Task<FunctionalityResponse> GetByIdAsync(int functionalityId)
        {
            var existingFunctionality = await _functionalityRepository.FindByIdAsync(functionalityId);

            if (existingFunctionality == null)
                return new FunctionalityResponse("Functionality not found");

            return new FunctionalityResponse(existingFunctionality);
        }
    }
}
