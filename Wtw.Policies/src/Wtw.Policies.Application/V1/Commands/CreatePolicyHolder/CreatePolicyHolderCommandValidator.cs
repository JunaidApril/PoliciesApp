using FluentValidation;

namespace Wtw.Policies.Application.V1.Commands
{
    public class CreatePolicyCommandValidator : AbstractValidator<CreatePolicyCommand>
    {
        public CreatePolicyCommandValidator()
        {
            RuleFor(command => command.PolicyHolder.Name)
                .NotEmpty();

            RuleFor(command => command.PolicyHolder.Age)
                .GreaterThan(17);
        }
    }
}
