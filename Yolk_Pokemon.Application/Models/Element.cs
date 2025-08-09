
namespace Yolk_Pokemon.Application.Models;

public partial class Element
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Move> Moves { get; set; } = [];

    public virtual ICollection<Pokemon> Pokemons { get; set; } = [];
}
