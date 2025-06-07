using CRUDDemo.Entities;
using CRUDDemo.ServiceContracts;
using CRUDDemo.ServiceContracts.DTO.Mappers;

namespace CRUDDemo.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countries;

        public CountriesService()
        {
            _countries = new List<Country>();
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // Check if request is null
            if (countryAddRequest == null)
                throw new ArgumentNullException(nameof(countryAddRequest), "CountryAddRequest cannot be null.");

            // Validate CountryName
            if (string.IsNullOrWhiteSpace(countryAddRequest.CountryName))
                throw new ArgumentException("Country name cannot be null or empty.", nameof(countryAddRequest.CountryName));

            // Check for duplicates (case-insensitive match is often better UX)
            if (_countries.Any(c => c.CountryName?.ToLower() == countryAddRequest.CountryName.ToLower()))
                throw new ArgumentException("A country with the same name already exists.", nameof(countryAddRequest.CountryName));

            // Convert DTO to domain model and assign new ID
            Country country = countryAddRequest.ToCountry();
            country.CountryId = Guid.NewGuid();

            // Add to list
            _countries.Add(country);

            // Return response
            return country.ToCountryResponse();
        }
        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(c => c.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryId(Guid? countryId)
        {
            throw new NotImplementedException();
        }
    }
}
