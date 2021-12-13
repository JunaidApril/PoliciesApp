using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Application.V1.Queries
{
    public class GetAllPoliciesQueryHandler : IRequestHandler<GetAllPoliciesQuery, GetAllPoliciesQueryResponse>
    {
        private readonly IPoliciesRepository _policiesRepository;
        private readonly IMapper _mapper;

        public GetAllPoliciesQueryHandler(IPoliciesRepository policiesRepository, IMapper mapper)
        {
            _policiesRepository = policiesRepository;
            _mapper = mapper;
        }

        public async Task<GetAllPoliciesQueryResponse> Handle(GetAllPoliciesQuery request, CancellationToken cancellationToken)
        {
            var policyList = await _policiesRepository.GetAllAsync();

            var test = _mapper.Map<IEnumerable<PolicyDto>>(policyList);
            return new GetAllPoliciesQueryResponse
            {
                policies = test
            };
        }
    }
}
