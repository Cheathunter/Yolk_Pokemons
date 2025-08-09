namespace Yolk_Pokemon.Api
{
    public class ApiEndpoints
    {
        private const string ApiBase = "api";

        public static class Trainers
        {
            private const string Base = $"{ApiBase}/trainers";

            public const string Create = Base;
            public const string Get = $"{Base}/{{id}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id}}";
            public const string Delete = $"{Base}/{{id}}";

            public const string AddPokemon = $"{Base}/{{trainerId:int}}/pokemon";
        }

        public static class Pokemons
        {
            private const string Base = $"{ApiBase}/pokemon";

            public const string Create = Base;
            public const string Get = $"{Base}/{{id}}";
            public const string GetAll = Base;
        }
    }
}