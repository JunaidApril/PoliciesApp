using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wtw.Policies.Application.V1.Commands;
using Wtw.Policies.Application.V1.Queries;
using Wtw.Policies.Domain.Dtos;

namespace Wtw.Policies.Application.V1.Interfaces
{
    public interface IPolicyService
    {
        Task<Guid> CreatePolicyAsync(CreatePolicyHolderCommand command);

        Task RemovePolicyAsync(DeletePolicyCommand command);

        Task<Guid> UpdatePolicyAsync(UpdatePolicyCommand command);

        Task<IEnumerable<PolicyDto>> GetPoliciesAsync();

        Task<PolicyDto> GetPolicyAsync(GetPolicyQuery command);
    }
}
