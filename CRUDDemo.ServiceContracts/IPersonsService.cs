using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDDemo.ServiceContracts.DTO;
using CRUDDemo.ServiceContracts.Enums;

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

        /// <summary>
        /// Retrieves all persons that match the specified search criteria.
        /// </summary>
        /// <param name="searchBy">The field name to filter by (e.g., "Name", "Email").</param>
        /// <param name="searchString">The search term to match against the specified field.</param>
        /// <returns>A list of <see cref="PersonResponse"/> objects that match the search criteria.</returns>
        List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);

        /// <summary>
        /// Returns a sorted list of <see cref="PersonResponse"/>
        /// </summary>
        /// <param name="allPersons">The list of persons to be sorted.</param>
        /// <param name="sortBy">The field name to sort by (e.g., "Name", "Email", "DateOfBirth").</param>
        /// <param name="sortOrder">The direction of the sort: ascending or descending.</param>
        /// <returns>A sorted list of <see cref="PersonResponse"/> objects.</returns>
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);
    }
}
