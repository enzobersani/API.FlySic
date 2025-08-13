using API.FlySic.Domain.Queries;
using FluentValidation;

namespace API.FlySic.Domain.Validators
{
    public class SearchFlightFormsQueryValidator : AbstractValidator<SearchFlightFormsQuery>
    {
        public SearchFlightFormsQueryValidator()
        {
            RuleFor(x => x.DepartureLocation)
                .Must(s => string.IsNullOrWhiteSpace(s) || !string.IsNullOrWhiteSpace(s.Trim()))
                    .WithMessage("DepartureLocation, se informado, não pode conter apenas espaços.")
                .MaximumLength(150).When(x => !string.IsNullOrWhiteSpace(x.DepartureLocation))
                    .WithMessage("DepartureLocation deve ter no máximo 150 caracteres.");

            RuleFor(x => x.ArrivalLocation)
                .Must(s => string.IsNullOrWhiteSpace(s) || !string.IsNullOrWhiteSpace(s.Trim()))
                    .WithMessage("ArrivalLocation, se informado, não pode conter apenas espaços.")
                .MaximumLength(150).When(x => !string.IsNullOrWhiteSpace(x.ArrivalLocation))
                    .WithMessage("ArrivalLocation deve ter no máximo 150 caracteres.");

            RuleFor(x => x)
                .Must(x =>
                {
                    if (!x.DepartureDate.HasValue || !x.ArrivalDate.HasValue)
                        return true;

                    var dep = x.DepartureDate.Value.Date;
                    var arr = x.ArrivalDate.Value.Date;
                    return arr >= dep;
                })
                .WithMessage("ArrivalDate deve ser no mesmo dia ou após a DepartureDate.");
        }
    }
}
