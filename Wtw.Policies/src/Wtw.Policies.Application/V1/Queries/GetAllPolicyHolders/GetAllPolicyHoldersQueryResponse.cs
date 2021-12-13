using System.Collections.Generic;
using Wtw.Policies.Domain.Dtos;

namespace Wtw.Policies.Application.V1.Queries
{
    public class GetAllPolicyHoldersQueryResponse
    {
        public IEnumerable<PolicyHolderDto> policyHolders { get; set; }
    }
}
