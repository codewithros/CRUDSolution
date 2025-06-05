using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDDemo.Entities;

namespace CRUDDemo.ServiceContracts.DTO.Mappers
{
    /// <summary>
    /// Extension methods for mapping Country entities to response DTOs.
    /// </summary>
    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
            };
        }
    }
}
