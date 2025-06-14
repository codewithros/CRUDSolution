using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDDemo.Entities;
using CRUDDemo.ServiceContracts.Enums;

namespace CRUDDemo.ServiceContracts.DTO
{
    /// <summary>
    /// Request DTO used to update an existing person's details.
    /// </summary>
    public class PersonUpdateRequest
    {
        [Required]
        public Guid PersonId { get; set; }

        [Required(ErrorMessage = "Name cannot be blank")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email cannot be blank")]
        [EmailAddress(ErrorMessage = "Email value should be valid email")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions Gender { get; set; }
        public Guid? CountryId { get; set; }

        [Required(ErrorMessage = "Address cannot be blank")]
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Converts the current <see cref="PersonUpdateRequest"/> instance into a <see cref="Person"/> domain entity for update operations.
        /// </summary>
        /// <returns>
        /// A <see cref="Person"/> object populated with updated values from the request.
        /// </returns>
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
