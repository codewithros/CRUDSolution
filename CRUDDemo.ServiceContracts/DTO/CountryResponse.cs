using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDDemo.ServiceContracts.DTO
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

            return 
                CountryId == countryToCompare.CountryId &&
                CountryName == countryToCompare.CountryName
            ;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CountryId, CountryName);
        }

        public override string ToString()
        {
            return $"""
                CountryId = {CountryId}
                CountryName = {CountryName}
                """;
        }
    }
}
