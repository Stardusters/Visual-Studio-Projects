using PokemonReviewApi.Models;

namespace PokemonReviewApi.Interfaces
{
    public interface IOwnersRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int id);
    }
}
