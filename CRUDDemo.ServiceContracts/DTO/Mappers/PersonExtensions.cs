using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDDemo.Entities;

namespace CRUDDemo.ServiceContracts.DTO.Mappers
{
    public static class PersonExtensions
    {
        /// <summary>
        /// Converts a Person entity into a PersonResponse DTO.
        /// </summary>
        /// <param name="person">The Person object to convert.</param>
        /// <returns>The converted PersonResponse object.</returns>
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse
            {
                PersonId = person.PersonId,
                Name = person.Name,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                CountryId = person.CountryId,
                Address = person.Address,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Age = person.DateOfBirth != null
                    ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25, 1)
                    : null
            };
        }
    }
}
