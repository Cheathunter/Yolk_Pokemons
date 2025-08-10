
namespace Yolk_Pokemon.Application.Models
{
    public class GetAllPokemonsOptions
    {
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