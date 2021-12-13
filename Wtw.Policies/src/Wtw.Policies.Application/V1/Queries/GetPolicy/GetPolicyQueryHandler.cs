using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Application.V1.Queries
{
    public class GetPolicyQueryHandler : IRequestHandler<GetPolicyQuery, GetPolicyQueryResponse>
    {
        private readonly IPoliciesRepository _policiesRepository;

        public GetPolicyQueryHandler(IPoliciesRepository policiesRepository)
        {
            _policiesRepository = policiesRepository;
        }

        public async Task<GetPolicyQueryResponse> Handle(GetPolicyQuery request, CancellationToken cancellationToken)
        {
            var policy = await _policiesRepository.FindByIdAsync(request.UUID);

            var policyResponse = new PolicyDto
            {
                Policy_UUID = policy.UUID,
                PolicyHolder_UUID = policy.PolicyHolderUUID
            };

            return new GetPolicyQueryResponse
            {
                policy = policyResponse
            };
        }
    }
}
