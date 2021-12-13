using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Models;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Application.V1.Commands
{
    public class CreatePolicyCommandHandler : IRequestHandler<CreatePolicyCommand, CommandResponse>
    {
        private readonly IPoliciesRepository _policiesRepository;

        public CreatePolicyCommandHandler(IPoliciesRepository policiesRepository)
        {
            _policiesRepository = policiesRepository;
        }

        public async Task<CommandResponse> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
        {
            var policy = new Policy
            {
                PolicyHolderUUID = request.PolicyHolder.Uuid
            };
            var uuid = await _policiesRepository.CreateAsync(policy);

            return new CommandResponse
            {
                Response = uuid
            };
        }
    }
}
