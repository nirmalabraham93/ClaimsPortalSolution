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
    public class PolicyRepository : IPolicyRepository
    {
        private readonly ClaimsPortalDbContext _context;
        private readonly ILogger<PolicyRepository> _logger;

        public PolicyRepository(ClaimsPortalDbContext context, ILogger<PolicyRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Policy>> GetAllPoliciesAsync()
        {
            try
            {
                return await _context.Policies.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching policies.");
                throw;
            }
        }

        public async Task<Policy?> GetPolicyByIdAsync(int id)
        {
            try
            {
                return await _context.Policies.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching policy by Id.");
                throw;
            }
        }

        public async Task AddPolicyAsync(Policy policy)
        {
            try
            {
                await _context.Policies.AddAsync(policy);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding policy.");
                throw;
            }
        }

        public async Task UpdatePolicyAsync(Policy policy)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Policies.Update(policy);
                    await _context.SaveChangesAsync();

                    // Commit the transaction if everything succeeds
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction on error
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error occurred while updating policy.");
                    throw;
                }
            }
        }

        public async Task DeletePolicyAsync(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var policy = await _context.Policies.FindAsync(id);
                    if (policy != null)
                    {
                        _context.Policies.Remove(policy);
                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error occurred while deleting policy.");
                    throw;
                }
            }
        }
    }
}
