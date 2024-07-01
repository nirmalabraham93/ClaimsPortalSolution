using ClaimsPortal.Data.Entities;
using ClaimsPortal.Data.Repositories;
using ClaimsPortal.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsPortal.Service.Implementations
{
    public class PolicyHolderService : IPolicyHolderService
    {
        private readonly IPolicyHolderRepository _repository;
        private readonly ILogger<PolicyHolderService> _logger;

        public PolicyHolderService(IPolicyHolderRepository repository, ILogger<PolicyHolderService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<PolicyHolder>> GetAllPolicyHoldersAsync()
        {
            try
            {
                return await _repository.GetAllPolicyHoldersAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all policy holders.");
                throw;
            }
        }

        public async Task<PolicyHolder?> GetPolicyHolderByIdAsync(int id)
        {
            try
            {
                return await _repository.GetPolicyHolderByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching policy holder by Id.");
                throw;
            }
        }

        public async Task AddPolicyHolderAsync(PolicyHolder policyHolder)
        {
            try
            {
                _logger.LogInformation("Adding Policy holder.");
                await _repository.AddPolicyHolderAsync(policyHolder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding policy holder.");
                throw;
            }
        }

        public async Task UpdatePolicyHolderAsync(PolicyHolder policyHolder)
        {
            try
            {
                await _repository.UpdatePolicyHolderAsync(policyHolder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating policy holder.");
                throw;
            }
        }

        public async Task DeletePolicyHolderAsync(int id)
        {
            try
            {
                await _repository.DeletePolicyHolderAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting policy holder.");
                throw;
            }
        }
    }
}
