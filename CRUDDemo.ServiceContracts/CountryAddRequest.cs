using System.Diagnostics.Metrics;
using CRUDDemo.Entities;

namespace CRUDDemo.ServiceContracts
{
    /// <summary>
    /// Request DTO used to add a new country.
    /// </summary>
    public class CountryAddRequest
    {
        public string? CountryName { get; set; }

        public Country ToCountry()
        {
            return new Country { CountryName = CountryName };
        }
    }
}
