using System;
using CRUDDemo.Services.Helpers;
using CRUDDemo.ServiceContracts.DTO;
using CRUDDemo.ServiceContracts;
using CRUDDemo.Entities;
using CRUDDemo.ServiceContracts.DTO.Mappers;
using CRUDDemo.ServiceContracts.Enums;

namespace CRUDDemo.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;
        public PersonsService()
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();
        }
        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            var personResponse = person.ToPersonResponse();

            // Retrieve country name if available
            personResponse.Country = _countriesService
                .GetCountryByCountryId(person.CountryId)?
                .CountryName;

            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            // Validate request
            if (personAddRequest == null)
                throw new ArgumentNullException(nameof(personAddRequest), "PersonAddRequest cannot be null.");

            //Model Validations
            ValidationHelper.ModelValidation(personAddRequest);

            // Map DTO to entity
            var person = personAddRequest.ToPerson();
            person.PersonId = Guid.NewGuid();

            // Add to in-memory store
            _persons.Add(person);

            // Convert and return response
            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(p => p.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonByPersonId(Guid? personId)
        {
            if (personId == null)
                return null;

            Person? person = _persons.FirstOrDefault(p => p.PersonId == personId);

            return person != null ? ConvertPersonToPersonResponse(person) : null;
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            var allPersons = GetAllPersons();

            if (string.IsNullOrWhiteSpace(searchBy) || string.IsNullOrWhiteSpace(searchString))
                return allPersons;

            searchString = searchString.Trim();

            return searchBy switch
            {
                nameof(Person.Name) => allPersons
                    .Where(p => !string.IsNullOrWhiteSpace(p.Name) &&
                                p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList(),

                nameof(Person.Email) => allPersons
                    .Where(p => !string.IsNullOrWhiteSpace(p.Email) &&
                                p.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList(),

                nameof(Person.DateOfBirth) => allPersons
                    .Where(p => p.DateOfBirth.HasValue &&
                                p.DateOfBirth.Value.ToString("dd MMMM yyyy")
                                    .Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList(),

                nameof(Person.Gender) => allPersons
                    .Where(p => !string.IsNullOrWhiteSpace(p.Gender) &&
                                p.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList(),

                nameof(Person.CountryId) => allPersons
                    .Where(p => !string.IsNullOrWhiteSpace(p.Country) &&
                                p.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList(),

                nameof(Person.Address) => allPersons
                    .Where(p => !string.IsNullOrWhiteSpace(p.Address) &&
                                p.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList(),

                _ => allPersons
            };
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return allPersons;

            List<PersonResponse> sortedPersons = (sortBy, sortOrder) switch
            {
                (nameof(PersonResponse.Name), SortOrderOptions.ASC)
                => allPersons.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Name), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.ASC) =>
                allPersons.OrderBy(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) =>
                allPersons.OrderBy(p => p.DateOfBirth).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(p => p.DateOfBirth).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.ASC) =>
                allPersons.OrderBy(p => p.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(p => p.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC) =>
                allPersons.OrderBy(p => p.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(p => p.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.ASC) =>
                allPersons.OrderBy(p => p.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(p => p.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.ASC) =>
                allPersons.OrderBy(p => p.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(p => p.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC) =>
                allPersons.OrderBy(p => p.ReceiveNewsLetters).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(p => p.ReceiveNewsLetters).ToList(),

                _ => allPersons
            };

            return sortedPersons;
        }

        public PersonResponse? UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(personUpdateRequest));

            // Validate model
            ValidationHelper.ModelValidation(personUpdateRequest);

            // Find actual person entity in the data store
            Person? existingPerson = _persons.FirstOrDefault(p => p.PersonId == personUpdateRequest.PersonId);

            if (existingPerson == null)
                throw new ArgumentException("Person record does not exist.");

            // Update values
            existingPerson.Name = personUpdateRequest.Name;
            existingPerson.Email = personUpdateRequest.Email;
            existingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            existingPerson.Gender = personUpdateRequest.Gender.ToString();
            existingPerson.CountryId = personUpdateRequest.CountryId;
            existingPerson.Address = personUpdateRequest.Address;
            existingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

            // Return updated response
            return ConvertPersonToPersonResponse(existingPerson);
        }

        public bool DeletePerson(Guid? personId)
        {
            throw new NotImplementedException();
        }
    }
}
