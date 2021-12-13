using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Application.V1.Queries
{
    public class GetPolicyHolderQueryHandler : IRequestHandler<GetPolicyHolderQuery, GetPolicyHolderQueryResponse>
    {
        private readonly IPolicyHolderRepository _policyHolderRepository;

        public GetPolicyHolderQueryHandler(IPolicyHolderRepository policyHolderRepository)
        {
            _policyHolderRepository = policyHolderRepository;
        }

        public async Task<GetPolicyHolderQueryResponse> Handle(GetPolicyHolderQuery request, CancellationToken cancellationToken)
        {
            var policyHolder = await _policyHolderRepository.FindByIdAsync(request.UUID);

            var policyHolderResponse = new PolicyHolderDto
            {
                Uuid = policyHolder.UUID,
                Name = policyHolder.Name,
                Age = policyHolder.Age,
                Gender = policyHolder.Gender
            };

            return new GetPolicyHolderQueryResponse
            {
                policyHolder = policyHolderResponse
            };
        }
    }
}
