using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Models;

namespace Wtw.Policies.Infrastructure.Repositories.Interfaces
{
    public interface IPoliciesRepository
    {
        public Task<Policy> FindByIdAsync(Guid policyUUID);
        public Task<IEnumerable<Policy>> GetAllAsync();
        public Task<Guid> CreateAsync(Policy policyHolder);
        public Task<Guid> UpdateAsync(Policy policyHolder);
        public Task DeleteAsync(Guid policyUUID);
    }
}
