using MediatR;
using System;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Enums;

namespace Wtw.Policies.Application.V1.Commands
{
    public class UpdatePolicyHolderCommand : IRequest<CommandResponse>
    {
        public Guid Uuid { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public GenderType Gender { get; set; }
    }
}
