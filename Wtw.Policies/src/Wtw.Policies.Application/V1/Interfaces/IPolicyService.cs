using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;

namespace Wtw.Policies.Application.BFF.Interfaces
{
    public interface IPolicyService
    {
        Task<Guid> CreatePolicyAsync(ApplicationDto policyApplicationDto);

        Task<bool> RemovePolicyAsync(Guid policyUUID);

        Task<bool> UpdatePolicyAsync(PolicyDto policyDto);

        Task<IEnumerable<PolicyDto>> GetPoliciesAsync();

        Task<PolicyDto> GetPolicyAsync(Guid policyUUID);
    }
}
