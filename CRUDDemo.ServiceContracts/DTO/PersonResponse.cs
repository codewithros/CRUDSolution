using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDDemo.ServiceContracts.DTO
{
    public class PersonResponse
    {
        public Guid PersonId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public double? Age { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Compares the current object data with another PersonResponse.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if all properties match; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not PersonResponse other) return false;

            return PersonId == other.PersonId &&
                   Name == other.Name &&
                   Email == other.Email &&
                   DateOfBirth == other.DateOfBirth &&
                   Gender == other.Gender &&
                   CountryId == other.CountryId &&
                   Address == other.Address &&
                   ReceiveNewsLetters == other.ReceiveNewsLetters;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PersonId, Name, Email, DateOfBirth, Gender, CountryId, Address, ReceiveNewsLetters);
        }
    }
}
