using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDDemo.ServiceContracts.DTO;
using CRUDDemo.ServiceContracts;

namespace CRUDDemo.Tests.TestData
{
    public static class CountryTestData
    {
        public static (CountryResponse Usa, CountryResponse Canada, CountryResponse Japan) AddCommonCountries(ICountriesService countriesService)
        {
            var usa = countriesService.AddCountry(Usa());
            var canada = countriesService.AddCountry(Canada());
            var japan = countriesService.AddCountry(Japan());

            return (usa, canada, japan);
        }

        public static List<CountryAddRequest> CountryAddRequests => new()
        {
            Usa(),
            Canada(),
            Japan(),
        };

        public static CountryAddRequest Usa() => new()
        {
            CountryName = "USA"
        };

        public static CountryAddRequest Canada() => new()
        {
            CountryName = "Canada"
        };

        public static CountryAddRequest Japan() => new()
        {
            CountryName = "Japan"
        };
    }
}
