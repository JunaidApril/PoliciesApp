using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wtw.Policies.Application.V1.Commands
{
    public class DeletePolicyCommandValidator : AbstractValidator<DeletePolicyCommand>
    {
        public DeletePolicyCommandValidator()
        {
            RuleFor(command => command.UUID)
                .NotEmpty();
        }
    }
}
