using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Application.V1.Commands
{
    public class DeletePolicyHolderCommandHandler : IRequestHandler<DeletePolicyHolderCommand, CommandResponse>
    {
        private readonly IPolicyHolderRepository _policyHolderRepository;

        public DeletePolicyHolderCommandHandler(IPolicyHolderRepository policyHolderRepository)
        {
            _policyHolderRepository = policyHolderRepository;
        }

        public async Task<CommandResponse> Handle(DeletePolicyHolderCommand request, CancellationToken cancellationToken)
        {
            await _policyHolderRepository.DeleteAsync(request.UUID);

            return new CommandResponse
            {
                Response = Guid.Empty
            };
        }
    }
}
