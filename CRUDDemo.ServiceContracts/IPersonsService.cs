using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDDemo.ServiceContracts.DTO;

namespace CRUDDemo.ServiceContracts
{
    /// <summary>
    /// Defines business operations for managing persons.
    /// </summary>
    public interface IPersonsService
    {
        /// <summary>
        /// Adds a new person to the system.
        /// </summary>
        /// <param name="personAddRequest">The details of the person to add.</param>
        /// <returns>The added person with its assigned identifier.</returns>
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// Retrieves all persons as a list of PersonResponse objects.
        /// </summary>
        /// <returns>A list of PersonResponse representing all persons.</returns>
        List<PersonResponse> GetAllPersons();

        /// <summary>
        /// Retrieves a person by their unique PersonId.
        /// </summary>
        /// <param name="personId">The unique identifier of the person to retrieve.</param>
        /// <returns>
        /// A <see cref="PersonResponse"/> if the person is found; otherwise, <c>null</c>.
        /// </returns>
        PersonResponse? GetPersonByPersonId(Guid? personId);
    }
}
