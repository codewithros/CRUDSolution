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
    }
}
