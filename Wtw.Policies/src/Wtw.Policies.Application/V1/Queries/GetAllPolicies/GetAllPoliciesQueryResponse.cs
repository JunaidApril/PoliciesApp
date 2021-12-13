using System.Collections.Generic;
using Wtw.Policies.Domain.Dtos;

namespace Wtw.Policies.Application.V1.Queries
{
    public class GetAllPoliciesQueryResponse
    {
        public IEnumerable<PolicyDto> policies { get; set; }
    }
}
