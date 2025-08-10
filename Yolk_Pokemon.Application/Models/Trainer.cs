
namespace Yolk_Pokemon.Application.Models;

public partial class Trainer
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public string? Region { get; set; }

    public int? Age { get; set; }

    public required DateTime? CreatedAt { get; set; }

    public int Wins { get; set; }

    public int Losses { get; set; }

    public virtual ICollection<Pokemon> Pokemons { get; set; } = [];
}
