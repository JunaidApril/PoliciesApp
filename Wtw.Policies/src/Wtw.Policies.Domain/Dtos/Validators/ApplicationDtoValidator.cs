using FluentValidation;

namespace Wtw.Policies.Domain.Dtos.Validators
{
    public class ApplicationDtoValidator : AbstractValidator<ApplicationDto>
    {
        public ApplicationDtoValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty();

            RuleFor(command => command.Age)
                .GreaterThan(17);
        }
    }
}
