using FluentValidation;

namespace Wtw.Policies.Domain.Dtos.Validators
{
    public class PolicyDtoValidator : AbstractValidator<PolicyDto>
    {
        public PolicyDtoValidator()
    {
        RuleFor(command => command.Policy_UUID)
            .NotEmpty();

        RuleFor(command => command.PolicyHolder.Name)
            .NotEmpty();

        RuleFor(command => command.PolicyHolder.Age)
            .GreaterThan(17);
    }
}
}
