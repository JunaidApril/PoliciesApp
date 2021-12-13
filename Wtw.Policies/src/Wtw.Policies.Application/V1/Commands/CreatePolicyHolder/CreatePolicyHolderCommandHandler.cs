using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Models;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Application.V1.Commands
{
    public class CreatePolicyHolderCommandHandler : IRequestHandler<CreatePolicyHolderCommand, PolicyHolderResponse>
    {
        private readonly IPolicyHolderRepository _policyHolderRepository;

        public CreatePolicyHolderCommandHandler(IPolicyHolderRepository policyHolderRepository)
        {
            _policyHolderRepository = policyHolderRepository;
        }

        public async Task<PolicyHolderResponse> Handle(CreatePolicyHolderCommand request, CancellationToken cancellationToken)
        {
            var policyHolder = new PolicyHolder
            {
                Name = request.Name,
                Age = request.Age,
                Gender = request.GenderType
            };
            var policyHolderSaved = await _policyHolderRepository.CreateAsync(policyHolder);

            return new PolicyHolderResponse
            {
                Uuid = policyHolderSaved.UUID,
                Name = request.Name,
                Age = request.Age,
                GenderType = request.GenderType
            };
        }
    }
}
