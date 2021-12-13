using FluentValidation;

namespace Wtw.Policies.Application.V1.Queries
{
    public class GetPolicyQueryValidator : AbstractValidator<GetPolicyQuery>
    {
        public GetPolicyQueryValidator()
        {
            RuleFor(command => command.UUID)
                .NotEmpty();
        }
    }
}
