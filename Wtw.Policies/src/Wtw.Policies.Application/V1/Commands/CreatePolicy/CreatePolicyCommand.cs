using MediatR;
using System;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Enums;

namespace Wtw.Policies.Application.V1.Commands
{
    public class CreatePolicyCommand  : IRequest<CommandResponse>
    {
        public Guid UUID { get; set; }

        public PolicyHolderResponse PolicyHolder { get; set; }
    }
}
