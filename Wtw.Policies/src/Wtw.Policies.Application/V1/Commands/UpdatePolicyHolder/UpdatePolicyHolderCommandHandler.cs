using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Models;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Application.V1.Commands
{
    public class UpdatePolicyHolderCommandHandler : IRequestHandler<UpdatePolicyHolderCommand, CommandResponse>
    {
        private readonly IPolicyHolderRepository _policyHolderRepository;

        public UpdatePolicyHolderCommandHandler(IPolicyHolderRepository policyHolderRepository)
        {
            _policyHolderRepository = policyHolderRepository;
        }
        public async Task<CommandResponse> Handle(UpdatePolicyHolderCommand request, CancellationToken cancellationToken)
        {
            var policyHolder = new PolicyHolder
            {
                UUID = request.Uuid,
                Name = request.Name,
                Age = request.Age,
                Gender = request.Gender
            };
            var holderResponse = await _policyHolderRepository.UpdateAsync(policyHolder);

            return new CommandResponse
            {
                Response = holderResponse.UUID
            };
        }
    }
}
