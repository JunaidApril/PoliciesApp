using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wtw.Policies.Application.V1.Commands
{
    public class DeletePolicyHolderCommandValidator : AbstractValidator<DeletePolicyHolderCommand>
    {
        public DeletePolicyHolderCommandValidator()
        {
            RuleFor(command => command.UUID)
                .NotEmpty();
        }
    }
}
