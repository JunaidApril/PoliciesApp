using MediatR;
using System;
using Wtw.Policies.Domain.Dtos;

namespace Wtw.Policies.Application.V1.Commands
{
    public class DeletePolicyHolderCommand : IRequest<CommandResponse>
    {
        public Guid UUID { get; set; }
    }
}
