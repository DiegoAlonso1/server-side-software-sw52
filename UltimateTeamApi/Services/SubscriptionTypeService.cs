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
    public class SubscriptionTypeService : ISubscriptionTypeService
    {
        private readonly ISubscriptionTypeRepository _subscriptionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionTypeService(ISubscriptionTypeRepository subscriptionTypeRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionTypeRepository = subscriptionTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SubscriptionTypeResponse> DeleteAsync(int subscriptionTypeId)
        {
            var existingSubscription = await _subscriptionTypeRepository.FindByIdAsync(subscriptionTypeId);

            if (existingSubscription == null)
                return new SubscriptionTypeResponse("Subscription Type not found");

            try
            {
                _subscriptionTypeRepository.Remove(existingSubscription);
                await _unitOfWork.CompleteAsync();

                return new SubscriptionTypeResponse(existingSubscription);
            }
            catch (Exception ex)
            {
                return new SubscriptionTypeResponse($"An error ocurred while deleting the subscription type: {ex.Message}");
            }
        }

        public async Task<IEnumerable<SubscriptionType>> GetAllAsync()
        {
            return await _subscriptionTypeRepository.ListAsync();
        }

        public async Task<SubscriptionTypeResponse> GetByIdAsync(int subscriptionTypeId)
        {
            var existingSubscription = await _subscriptionTypeRepository.FindByIdAsync(subscriptionTypeId);

            if (existingSubscription == null)
                return new SubscriptionTypeResponse("Subscription Type not found");

            return new SubscriptionTypeResponse(existingSubscription);
        }

        public async Task<SubscriptionTypeResponse> SaveAsync(SubscriptionType subscriptionType)
        {
            try
            {
                await _subscriptionTypeRepository.AddAsync(subscriptionType);
                await _unitOfWork.CompleteAsync();

                return new SubscriptionTypeResponse(subscriptionType);
            }
            catch (Exception ex)
            {
                return new SubscriptionTypeResponse($"An error ocurred while saving the subscription type: {ex.Message}");
            }
        }

        public async Task<SubscriptionTypeResponse> UpdateAsync(int subscriptionTypeId, SubscriptionType subscriptionTypeRequest)
        {
            var existingSubscription = await _subscriptionTypeRepository.FindByIdAsync(subscriptionTypeId);

            if (existingSubscription == null)
                return new SubscriptionTypeResponse("Subscription Type not found");

            existingSubscription.Type = subscriptionTypeRequest.Type;
            existingSubscription.Amount = subscriptionTypeRequest.Amount;

            try
            {
                _subscriptionTypeRepository.Update(existingSubscription);
                await _unitOfWork.CompleteAsync();

                return new SubscriptionTypeResponse(existingSubscription);
            }
            catch (Exception ex)
            {
                return new SubscriptionTypeResponse($"An error ocurred while updating the subscription type: {ex.Message}");
            }
        }
    }
}
