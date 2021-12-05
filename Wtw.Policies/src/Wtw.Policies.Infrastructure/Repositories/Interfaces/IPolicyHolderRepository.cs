using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Models;

namespace Wtw.Policies.Infrastructure.Repositories.Interfaces
{
    public interface IPolicyHolderRepository
    {
        public Task<PolicyHolder> FindByIdAsync(Guid policyHolderUUID);
        public Task<IEnumerable<PolicyHolder>> GetAllAsync();
        public Task<PolicyHolder> CreateAsync(PolicyHolder policyHolder);
        public Task UpdateAsync(PolicyHolder policyHolder);
        public Task DeleteAsync(Guid policyHolderUUID);
    }
}
