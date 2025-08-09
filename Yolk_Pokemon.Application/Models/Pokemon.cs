
namespace Yolk_Pokemon.Application.Models;

public partial class Pokemon
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required int Level { get; set; }

    public int TypeId { get; set; }

    public int? OwnerId { get; set; }

    public required int Health { get; set; }

    public DateTime? CaughtAt { get; set; }

    public virtual Trainer? Owner { get; set; }

    public virtual ICollection<PokemonMove> PokemonMoves { get; set; } = [];

    public virtual Element Type { get; set; } = null!;
}
