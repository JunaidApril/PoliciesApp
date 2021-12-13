using FluentValidation;

namespace Wtw.Policies.Application.V1.Queries
{
    public class GetPolicyHolderQueryValidator : AbstractValidator<GetPolicyHolderQuery>
    {
        public GetPolicyHolderQueryValidator()
        {
            RuleFor(command => command.UUID)
                .NotEmpty();
        }
    }
}
