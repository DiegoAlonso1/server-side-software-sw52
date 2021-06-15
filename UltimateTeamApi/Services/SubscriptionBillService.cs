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
    public class SubscriptionBillService : ISubscriptionBillService
    {
        private readonly ISubscriptionBillRepository _subscriptionBillRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionBillService(ISubscriptionBillRepository subscriptionBillRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionBillRepository = subscriptionBillRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SubscriptionBillResponse> DeleteAsync(int subscriptionBillId)
        {
            var existingSubscription = await _subscriptionBillRepository.FindByIdAsync(subscriptionBillId);

            if (existingSubscription == null)
                return new SubscriptionBillResponse("Subscription Bill not found");

            try
            {
                _subscriptionBillRepository.Remove(existingSubscription);
                await _unitOfWork.CompleteAsync();

                return new SubscriptionBillResponse(existingSubscription);
            }
            catch (Exception ex)
            {
                return new SubscriptionBillResponse($"An error ocurred while deleting the subscription bill: {ex.Message}");
            }
        }

        public async Task<IEnumerable<SubscriptionBill>> GetAllAsync()
        {
            return await _subscriptionBillRepository.ListAsync();
        }

        public async Task<IEnumerable<SubscriptionBill>> GetAllBySubscriptionTypeIdAsync(int subscriptionTypeId)
        {
            return await _subscriptionBillRepository.ListBySubscriptionTypeIdAsync(subscriptionTypeId);
        }

        public async Task<SubscriptionBillResponse> GetByIdAsync(int subscriptionBillId)
        {
            var existingSubscription = await _subscriptionBillRepository.FindByIdAsync(subscriptionBillId);

            if (existingSubscription == null)
                return new SubscriptionBillResponse("Subscription Bill not found");

            return new SubscriptionBillResponse(existingSubscription);
        }

        public async Task<SubscriptionBillResponse> SaveAsync(SubscriptionBill subscriptionBill)
        {
            try
            {
                await _subscriptionBillRepository.AddAsync(subscriptionBill);
                await _unitOfWork.CompleteAsync();

                return new SubscriptionBillResponse(subscriptionBill);
            }
            catch (Exception ex)
            {
                return new SubscriptionBillResponse($"An error ocurred while saving the subscription bill: {ex.Message}");
            }
        }

        public async Task<SubscriptionBillResponse> UpdateAsync(int subscriptionBillId, SubscriptionBill subscriptionBillRequest)
        {
            var existingSubscription = await _subscriptionBillRepository.FindByIdAsync(subscriptionBillId);

            if (existingSubscription == null)
                return new SubscriptionBillResponse("Subscription Bill not found");

            existingSubscription.ActiveLicense = subscriptionBillRequest.ActiveLicense;
            existingSubscription.DeadlineLicense = subscriptionBillRequest.DeadlineLicense;
            existingSubscription.License = subscriptionBillRequest.License;
            existingSubscription.Paid = subscriptionBillRequest.Paid;
            existingSubscription.PaymentMethod = subscriptionBillRequest.PaymentMethod;
            existingSubscription.SubscriptionTypeId = subscriptionBillRequest.SubscriptionTypeId;
            existingSubscription.UserId = subscriptionBillRequest.UserId;

            try
            {
                _subscriptionBillRepository.Update(existingSubscription);
                await _unitOfWork.CompleteAsync();

                return new SubscriptionBillResponse(existingSubscription);
            }
            catch (Exception ex)
            {
                return new SubscriptionBillResponse($"An error ocurred while updating the subscription bill: {ex.Message}");
            }
        }
    }
}
