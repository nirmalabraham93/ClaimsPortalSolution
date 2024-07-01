using ClaimsPortal.Data.DBContext;
using ClaimsPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsPortal.Data.Repositories
{
    public class PolicyHolderRepository : IPolicyHolderRepository
    {
        private readonly ClaimsPortalDbContext _context;
        private readonly ILogger<PolicyHolderRepository> _logger;

        public PolicyHolderRepository(ClaimsPortalDbContext context, ILogger<PolicyHolderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<PolicyHolder>> GetAllPolicyHoldersAsync()
        {
            try
            {
                return await _context.PolicyHolders.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching policy holders.");
                throw;
            }
        }

        public async Task<PolicyHolder?> GetPolicyHolderByIdAsync(int id)
        {
            try
            {
                return await _context.PolicyHolders.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching policy holder by Id.");
                throw;
            }
        }

        public async Task AddPolicyHolderAsync(PolicyHolder policyHolder)
        {
            try
            {
                await _context.PolicyHolders.AddAsync(policyHolder);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding policy holder.");
                throw;
            }
        }

        public async Task UpdatePolicyHolderAsync(PolicyHolder policyHolder)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.PolicyHolders.Update(policyHolder);
                    await _context.SaveChangesAsync();

                    // Commit the transaction if everything succeeds
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction on error
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error occurred while updating policy holder.");
                    throw;
                }
            }
        }

        public async Task DeletePolicyHolderAsync(int id)
        {
            try
            {
                var policyHolder = await _context.PolicyHolders.FindAsync(id);
                if (policyHolder != null)
                {
                    _context.PolicyHolders.Remove(policyHolder);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting policy holder.");
                throw;
            }
        }
    }
}
