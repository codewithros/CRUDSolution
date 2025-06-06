using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDDemo.ServiceContracts
{
    /// <summary>
    /// Defines business operations for managing countries.
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a new country to the system.
        /// </summary>
        /// <param name="countryAddRequest">The details of the country to add.</param>
        /// <returns>The added country with its assigned identifier.</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Retrieves all countries as a list of CountryResponse objects.
        /// </summary>
        /// <returns>A list of CountryResponse representing all countries.</returns>
        List<CountryResponse> GetAllCountries();
    }
}
