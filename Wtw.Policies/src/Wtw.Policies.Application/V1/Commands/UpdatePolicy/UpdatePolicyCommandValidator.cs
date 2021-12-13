using FluentValidation;

namespace Wtw.Policies.Application.V1.Commands
{
    public class UpdatePolicyCommandValidator : AbstractValidator<UpdatePolicyCommand>
    {
        public UpdatePolicyCommandValidator()
        {
            RuleFor(command => command.Uuid)
                .NotEmpty();

            RuleFor(command => command.PolicyHolderCommand)
                .NotNull();
        }
    }
}
