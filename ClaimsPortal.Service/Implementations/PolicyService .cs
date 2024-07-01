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
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _repository;
        private readonly ILogger<PolicyService> _logger;

        public PolicyService(IPolicyRepository repository, ILogger<PolicyService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Policy>> GetAllPoliciesAsync()
        {
            try
            {
                return await _repository.GetAllPoliciesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all policies.");
                throw;
            }
        }

        public async Task<Policy?> GetPolicyByIdAsync(int id)
        {
            try
            {
                return await _repository.GetPolicyByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching policy by Id.");
                throw;
            }
        }

        public async Task AddPolicyAsync(Policy policy)
        {
            try
            {
                await _repository.AddPolicyAsync(policy);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding policy.");
                throw;
            }
        }

        public async Task UpdatePolicyAsync(Policy policy)
        {
            try
            {
                await _repository.UpdatePolicyAsync(policy);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating policy.");
                throw;
            }
        }

        public async Task DeletePolicyAsync(int id)
        {
            try
            {
                await _repository.DeletePolicyAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting policy.");
                throw;
            }
        }
    }
}
