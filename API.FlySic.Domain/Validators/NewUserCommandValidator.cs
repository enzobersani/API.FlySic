using API.FlySic.Domain.Commands;
using FluentValidation;

namespace API.FlySic.Domain.Validators
{
    public class NewUserCommandValidator : AbstractValidator<NewUserCommand>
    {
        public NewUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(250).WithMessage("Name must not exceed 250 characters.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .LessThan(DateTime.Today).WithMessage("Birth date must be in the past.");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("CPF is required.")
                .Length(11).WithMessage("CPF must have exactly 11 characters.")
                .Matches(@"^\d+$").WithMessage("CPF must contain only numbers.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .MinimumLength(10).WithMessage("Phone number must have at least 10 characters.");

            RuleFor(x => x.IsAcceptedTerms)
                .Equal(true).WithMessage("You must accept the terms to proceed.");

            RuleFor(x => x)
                .Must(x => x.IsDonateHours || x.IsSearchHours)
                .WithMessage("You must select at least one option (Donate Hours or Search Hours).");
        }
    }
}
