using System;
using CRUDDemo.Services.Helpers;
using CRUDDemo.ServiceContracts.DTO;
using CRUDDemo.ServiceContracts;
using CRUDDemo.Entities;
using CRUDDemo.ServiceContracts.DTO.Mappers;

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
    }
}
