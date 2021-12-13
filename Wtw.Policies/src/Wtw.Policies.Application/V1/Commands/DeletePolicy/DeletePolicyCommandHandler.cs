using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Application.V1.Commands
{
    public class DeletePolicyCommandHandler : IRequestHandler<DeletePolicyCommand, CommandResponse>
    {
        private readonly IPoliciesRepository _policiesRepository;

        public DeletePolicyCommandHandler(IPoliciesRepository policiesRepository)
        {
            _policiesRepository = policiesRepository;
        }

        public async Task<CommandResponse> Handle(DeletePolicyCommand request, CancellationToken cancellationToken)
        {
            await _policiesRepository.DeleteAsync(request.UUID);

            return new CommandResponse
            {
                Response = Guid.Empty
            };
        }
    }
}
