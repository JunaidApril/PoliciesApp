using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Application.V1.Queries
{
    public class GetAllPolicyHoldersQueryHandler : IRequestHandler<GetAllPolicyHoldersQuery, GetAllPolicyHoldersQueryResponse>
    {
        private readonly IPolicyHolderRepository _policyHolderRepository;
        private readonly IMapper _mapper;

        public GetAllPolicyHoldersQueryHandler(IPolicyHolderRepository policyHolderRepository, IMapper mapper)
        {
            _policyHolderRepository = policyHolderRepository;
            _mapper = mapper;
        }

        public async Task<GetAllPolicyHoldersQueryResponse> Handle(GetAllPolicyHoldersQuery request, CancellationToken cancellationToken)
        {
            var policyHolderList = await _policyHolderRepository.GetAllAsync();

            return new GetAllPolicyHoldersQueryResponse
            {               
                policyHolders = _mapper.Map<IEnumerable<PolicyHolderDto>>(policyHolderList)
            };
        }
    }
}
