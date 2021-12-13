using FluentValidation;

namespace Wtw.Policies.Application.V1.Commands
{
    public class UpdatePolicyHolderCommandValidator : AbstractValidator<UpdatePolicyHolderCommand>
    {
        public UpdatePolicyHolderCommandValidator()
        {
            RuleFor(command => command.Uuid)
                .NotEmpty();

            RuleFor(command => command.Name)
                .NotEmpty();

            RuleFor(command => command.Age)
                .GreaterThan(17);
        }
    }
}
