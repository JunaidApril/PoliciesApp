using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Models;
using Wtw.Policies.Infrastructure.Repositories.Interfaces;

namespace Wtw.Policies.Application.V1.Commands
{
    public class UpdatePolicyCommandHandler : IRequestHandler<UpdatePolicyCommand, CommandResponse>
    {
        private readonly IPoliciesRepository _policiesRepository;

        public UpdatePolicyCommandHandler(IPoliciesRepository policiesRepository)
        {
            _policiesRepository = policiesRepository;
        }
        public async Task<CommandResponse> Handle(UpdatePolicyCommand request, CancellationToken cancellationToken)
        {
            var policy = new Policy
            {
                PolicyHolderUUID = request.PolicyHolderCommand.Uuid
            };
            var response = await _policiesRepository.UpdateAsync(policy);

            return new CommandResponse
            {
                Response = response
            };
        }
    }
}
