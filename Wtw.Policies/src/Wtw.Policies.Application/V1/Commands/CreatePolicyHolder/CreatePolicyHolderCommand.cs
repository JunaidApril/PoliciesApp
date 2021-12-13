using MediatR;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Enums;

namespace Wtw.Policies.Application.V1.Commands
{
    public class CreatePolicyHolderCommand : IRequest<PolicyHolderResponse>
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public GenderType GenderType { get; set; }
    }
}
