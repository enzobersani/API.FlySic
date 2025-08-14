using API.FlySic.Domain.Commands;
using FluentValidation;

namespace API.FlySic.Domain.Validators
{
    public class UpdateFlightFormCommandValidator : AbstractValidator<UpdateFlightFormCommand>
    {
        public UpdateFlightFormCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id da ficha é obrigatório.");

            RuleFor(x => x.DepartureDate)
                .NotEmpty().WithMessage("Data de partida é obrigatória.");

            RuleFor(x => x.DepartureTime)
                .NotEmpty().WithMessage("Hora de partida é obrigatória.");

            RuleFor(x => x.ArrivalDate)
                .NotEmpty().WithMessage("Data de chegada é obrigatória.");

            RuleFor(x => x.ArrivalTime)
                .NotEmpty().WithMessage("Hora de chegada é obrigatória.");

            RuleFor(x => x.AircraftType)
                .NotEmpty().WithMessage("Tipo da aeronave é obrigatório.")
                .MaximumLength(100).WithMessage("Tipo da aeronave deve ter no máximo 100 caracteres.");

            RuleFor(x => x.FlightComment)
                .MaximumLength(500).WithMessage("Comentário do voo deve ter no máximo 500 caracteres.");

            // Partida: ou aeroporto OU local manual (exatamente um)
            RuleFor(x => x)
                .Must(x => ExactlyOneFilled(x.DepartureAirport, x.DepartureManualLocation))
                .WithMessage("Informe apenas um local de partida: aeroporto OU local manual.");

            // Chegada: ou aeroporto OU local manual (exatamente um)
            RuleFor(x => x)
                .Must(x => ExactlyOneFilled(x.ArrivalAirport, x.ArrivalManualLocation))
                .WithMessage("Informe apenas um local de chegada: aeroporto OU local manual.");

            // Chegada > Partida (comparando data + hora)
            RuleFor(x => x)
                .Must(x => Combine(x.ArrivalDate, x.ArrivalTime) > Combine(x.DepartureDate, x.DepartureTime))
                .WithMessage("A data/hora de chegada deve ser posterior à data/hora de partida.");

            // Regras de pernoite
            When(x => x.HasOvernight, () =>
            {
                RuleFor(x => x)
                    .Must(x => x.ArrivalDate.Date >= x.DepartureDate.Date.AddDays(1))
                    .WithMessage("Com pernoite, a data de chegada deve ser pelo menos no dia seguinte à partida.");
            });

            When(x => !x.HasOvernight, () =>
            {
                RuleFor(x => x)
                    .Must(x => x.ArrivalDate.Date == x.DepartureDate.Date)
                    .WithMessage("Sem pernoite, a chegada deve ocorrer no mesmo dia da partida.");
            });
        }

        private static DateTime Combine(DateTime date, DateTime time)
            => date.Date.Add(time.TimeOfDay);

        private static bool ExactlyOneFilled(string? a, string? b)
        {
            var hasA = !string.IsNullOrWhiteSpace(a);
            var hasB = !string.IsNullOrWhiteSpace(b);
            return hasA ^ hasB;
        }
    }
}
