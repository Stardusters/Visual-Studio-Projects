using PokemonReviewApi.Models;

namespace PokemonReviewApi.Interfaces
{
    public interface ICategoriesRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Pokemon> GetPokemonsByCategory(int categoryId);
        bool CategoriesExists(int categoryId);      
    }
}
