using ClaimsPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsPortal.Service.Interfaces
{
    public interface IPolicyHolderService
    {
        Task<IEnumerable<PolicyHolder>> GetAllPolicyHoldersAsync();
        Task<PolicyHolder?> GetPolicyHolderByIdAsync(int id);
        Task AddPolicyHolderAsync(PolicyHolder policyHolder);
        Task UpdatePolicyHolderAsync(PolicyHolder policyHolder);
        Task DeletePolicyHolderAsync(int id);
    }
}
