using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Interfaces.UnitOfWork;
using API.FlySic.Domain.Validators;
using API.FlySic.Infrastructure.UnitOfWork;
using FluentValidation;

namespace API.FlySic
{
    public static class Startup
    {
        /// <summary>
        /// Configuração de Repositórios
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        /// <summary>
        /// Configuração de Validators
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<NewUserCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<SearchFlightFormsQueryValidator>();
            services.AddValidatorsFromAssemblyContaining<NewFlightFormCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateFlightFormCommandValidator>();
        }
    }
}
