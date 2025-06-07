using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDDemo.Entities;
using System.Xml.Linq;
using CRUDDemo.ServiceContracts.Enums;

namespace CRUDDemo.ServiceContracts.DTO
{
    /// <summary>
    /// Request DTO used to add a new person.
    /// </summary>
    public class PersonAddRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Converts the current PersonAddRequest into a Person entity.
        /// </summary>
        /// <returns>A new Person object populated from the request.</returns>
        public Person ToPerson()
        {
            return new Person
            {
                Name = Name,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                Address = Address,
                CountryId = CountryId,
                ReceiveNewsLetters = ReceiveNewsLetters
            };
        }
    }
}
