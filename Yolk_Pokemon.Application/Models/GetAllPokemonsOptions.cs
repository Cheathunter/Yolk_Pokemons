
namespace Yolk_Pokemon.Application.Models
{
    public class GetAllPokemonsOptions
    {
        public string? Type { get; set; }

        public string? Region { get; set; }

        public string? SortedField { get; set; }

        public SortedOrder? SortedOrder { get; set; }
    }

    public enum SortedOrder
    {
        Unsorted,
        Ascending,
        Descending
    }
}