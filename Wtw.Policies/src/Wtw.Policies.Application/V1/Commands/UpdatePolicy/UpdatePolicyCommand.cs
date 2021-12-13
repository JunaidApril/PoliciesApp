using MediatR;
using System;
using Wtw.Policies.Domain.Dtos;

namespace Wtw.Policies.Application.V1.Commands
{
    public class UpdatePolicyCommand : IRequest<CommandResponse>
    {
        public Guid Uuid { get; set; }

        public UpdatePolicyHolderCommand PolicyHolderCommand { get; set; }
    }
}
