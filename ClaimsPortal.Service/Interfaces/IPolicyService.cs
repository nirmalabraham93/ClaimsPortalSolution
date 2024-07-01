using ClaimsPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsPortal.Service.Interfaces
{
    public interface IPolicyService
    {
        Task<IEnumerable<Policy>> GetAllPoliciesAsync();
        Task<Policy?> GetPolicyByIdAsync(int id);
        Task AddPolicyAsync(Policy policy);
        Task UpdatePolicyAsync(Policy policy);
        Task DeletePolicyAsync(int id);
    }
}
