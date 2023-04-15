using PokemonReviewApi.Models;

namespace PokemonReviewApi.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerId);
        ICollection<Owner> GetOwnerFromCountry(int countryId);
        bool CountryExist(int id);
        bool OwnerExist(int ownerId);
    }
}
