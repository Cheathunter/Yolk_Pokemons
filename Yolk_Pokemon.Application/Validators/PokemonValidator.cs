
using FluentValidation;
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class PokemonValidator : AbstractValidator<Pokemon>
    {
        public PokemonValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Level).InclusiveBetween(1, 60);
            RuleFor(x => x.Health).InclusiveBetween(20, 800);
        }
    }
}