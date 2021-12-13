using MediatR;
using System;
using Wtw.Policies.Domain.Dtos;

namespace Wtw.Policies.Application.V1.Commands
{
    public class DeletePolicyCommand : IRequest<CommandResponse>
    {
        public Guid UUID { get; set; }
    }
}
