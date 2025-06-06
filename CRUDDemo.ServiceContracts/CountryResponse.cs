using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDDemo.ServiceContracts
{
    /// <summary>
    /// Response DTO representing a country returned by service operations.
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }

            if (obj.GetType() != typeof(CountryResponse))
            {
                return false;
            }

            CountryResponse countryToCompare = (CountryResponse)obj;

            return (
                this.CountryId == countryToCompare.CountryId &&
                this.CountryName == countryToCompare.CountryName
            );
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
