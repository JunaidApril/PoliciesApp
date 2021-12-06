using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;

namespace Wtw.Policies.Application.BFF.Interfaces
{
    public interface IPolicyService
    {
        Task<Guid> CreatePolicyAsync(ApplicationDto policyApplicationDto);

        Task RemovePolicyAsync(Guid policyUUID);

        Task<Guid> UpdatePolicyAsync(PolicyDto policyDto);

        Task<IEnumerable<PolicyDto>> GetPoliciesAsync();

        Task<PolicyDto> GetPolicyAsync(Guid policyUUID);
    }
}
