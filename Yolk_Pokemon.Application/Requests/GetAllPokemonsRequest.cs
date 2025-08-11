
namespace Yolk_Pokemon.Application.Requests
{
    public class GetAllPokemonsRequest
    {
        public string? SortBy { get; init; }

        public string? Type { get; init; }

        public string? Region { get; init; }
    }
}