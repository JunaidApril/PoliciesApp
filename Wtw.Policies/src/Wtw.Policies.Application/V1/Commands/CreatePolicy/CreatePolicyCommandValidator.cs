using FluentValidation;

namespace Wtw.Policies.Application.V1.Commands
{
    public class CreatePolicyHolderCommandValidator : AbstractValidator<CreatePolicyHolderCommand>
    {
        public CreatePolicyHolderCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty();

            RuleFor(command => command.Age)
                .GreaterThan(17);
        }
    }
}
