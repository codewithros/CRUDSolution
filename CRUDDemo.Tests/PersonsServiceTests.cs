﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDDemo.ServiceContracts.DTO;
using CRUDDemo.ServiceContracts.Enums;
using CRUDDemo.ServiceContracts;
using CRUDDemo.Services;
using Xunit.Abstractions;
using CRUDDemo.Entities;
using CRUDDemo.Tests.TestData;

namespace CRUDDemo.Tests
{
    public class PersonsServiceTests : TestWithOutput
    {
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;

        public PersonsServiceTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) 
        {
            _personsService = new PersonsService(); 
            _countriesService = new CountriesService();
            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson

        [Fact]
        public void AddPerson_WhenRequestIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            PersonAddRequest? request = null;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => _personsService.AddPerson(request));

            //Log
            LogExpected("throw ArgumentNullException");
            LogActual(ex.Message);
        }

        [Fact]
        public void AddPerson_WhenNameIsNull_ThrowsArgumentException()
        {
            // Arrange
            var request = new PersonAddRequest
            {
                Name = null
            };

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _personsService.AddPerson(request));

            //Log
            LogExpected("throw ArgumentException");
            LogActual(ex.Message);
        }

        [Fact]
        public void AddPerson_WhenValidRequest_ReturnsAndStoresPerson()
        {
            //Arrange
            CountryAddRequest countryAddRequest = CountryTestData.Canada();
            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);
            var request = PersonTestData.EthanSantos(countryResponse.CountryId);

            // Act
            var addedPerson = _personsService.AddPerson(request);
            var fetchedPersons = _personsService.GetAllPersons();

            //Log
            LogExpected("Response has PersonId and exist in the fetched list");
            LogActual($"Person Id - {addedPerson.PersonId}");

            // Assert
            Assert.NotNull(addedPerson);
            Assert.NotEqual(Guid.Empty, addedPerson.CountryId);
            Assert.Contains(addedPerson, fetchedPersons);
        }

        #endregion

        #region GetPersonByPersonId

        [Fact]
        public void GetPersonByPersonId_WhenPersonIdIsNull_ReturnsNull()
        {
            // Arrange
            Guid? personId = null;

            // Act
            var fetchedPersonById = _personsService.GetPersonByPersonId(personId);

            //Log
            LogExpected("PersonResponse is null");
            LogActual($"PersonResponse - {fetchedPersonById}");

            // Assert
            Assert.Null(fetchedPersonById);
        }

        [Fact]
        public void GetPersonByPersonId_WhenPersonExists_ReturnsMatchingPerson()
        {
            // Arrange
            var countryRequest = CountryTestData.Canada();
            var addedCountry = _countriesService.AddCountry(countryRequest);
            var personRequest = PersonTestData.JoyDelaCruz(addedCountry.CountryId);
            var addedPerson = _personsService.AddPerson(personRequest);

            // Act
            var fetchedPerson = _personsService.GetPersonByPersonId(addedPerson.PersonId);

            //Log
            LogExpected("Added PersonResponse Returned with PersonId");
            LogExpected("Fetched Person By Id returns added person");
            LogActual($"Added PersonResponse - {addedPerson?.ToString()}");
            LogActual($"Fetched PersonResponse - {fetchedPerson?.ToString()}");

            // Assert
            Assert.NotNull(fetchedPerson);
            Assert.Equal(addedPerson, fetchedPerson);
        }

        #endregion
        #region GetAllPersons

        [Fact]
        public void GetAllPersons_WhenNoPersonsExist_ReturnsEmptyList()
        {
            // Act
            var fetchedPersons = _personsService.GetAllPersons();

            // Log
            LogExpected("Fetched Empty Persons List");
            LogActual($"Fetch Persons Count: {fetchedPersons.Count}");

            // Assert
            Assert.Empty(fetchedPersons);
        }

        [Fact]
        public void GetAllPersons_WhenPersonsAdded_ReturnsAllAddedPersons()
        {
            //Arrange
            var (usa, canada, japan) = CountryTestData.AddCommonCountries(_countriesService);
            var personRequests = PersonTestData.PersonAddRequests(usa.CountryId, canada.CountryId, japan.CountryId);

            // Act
            LogExpected("");
            var expectedPersons = new List<PersonResponse>();
            foreach (var request in personRequests)
            {
                var added = _personsService.AddPerson(request);
                expectedPersons.Add(added);
                LogPerson("Added: ", added);
            }

            // Assert
            var allPersons = _personsService.GetAllPersons();
            LogActual("");
            Assert.Equal(expectedPersons.Count, allPersons.Count);
            foreach (var person in expectedPersons)
            {
                Assert.Contains(person, allPersons);
                LogPerson("Fetched: ", person);
            }
        }

        #endregion

        #region GetFilteredPersons
        [Fact]
        public void GetFilteredPersons_WhenSearchByIsEmpty_ReturnsAllPersons()
        {
            // Arrange
            var(usa, canada, japan) = CountryTestData.AddCommonCountries(_countriesService);
            var personRequests = PersonTestData.PersonAddRequests(usa.CountryId, canada.CountryId, japan.CountryId);

            // Act
            LogExpected("");
            var expectedPersons = new List<PersonResponse>();
            foreach (var request in personRequests)
            {
                var added = _personsService.AddPerson(request);
                expectedPersons.Add(added);
                LogPerson("Added: ", added);
            }
            
            var filteredPersons = _personsService.GetFilteredPersons(nameof(Person.Name), "");

            // Assert
            LogActual("");
            Assert.Equal(expectedPersons.Count, filteredPersons.Count);
            foreach (var person in expectedPersons)
            {
                Assert.Contains(person, filteredPersons);
                LogPerson("Filtered: ", person);
            }
        }

        [Fact]
        public void GetFilteredPersons_WhenSearchByName_ReturnsMatchingPersons()
        {
            // Arrange
            var (usa, canada, japan) = CountryTestData.AddCommonCountries(_countriesService);
            var personRequests = PersonTestData.PersonAddRequests(usa.CountryId, canada.CountryId, japan.CountryId);

            LogExpected("");
            var expectedPersons = new List<PersonResponse>();
            foreach (var request in personRequests)
            {
                var added = _personsService.AddPerson(request);
                expectedPersons.Add(added);
                LogPerson("Added: ", added);
            }

            // Act
            var filteredPersons = _personsService.GetFilteredPersons(nameof(Person.Name), "am");

            // Assert
            LogActual("");
            foreach (var person in expectedPersons)
            {
                if(person != null)
                {
                    if(person.Name.Contains("am", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(person, filteredPersons);
                        LogPerson("Filtered: ", person);
                    }
                }

            }
        }
        #endregion

        #region GetSortedPersons
        [Fact]
        public void GetSortedPersons_WhenSortedByPersonNameDesc_ReturnsPersonsSortedByNameDesc()
        {
            // Arrange
            var (usa, canada, japan) = CountryTestData.AddCommonCountries(_countriesService);
            var personAddRequests = PersonTestData.PersonAddRequests(usa.CountryId, canada.CountryId, japan.CountryId);

            var expectedSortedPersons = new List<PersonResponse>();

            LogExpected("Persons added before sorting (expected to be sorted by Name DESC):");
            foreach (var request in personAddRequests)
            {
                var addedPerson = _personsService.AddPerson(request);
                expectedSortedPersons.Add(addedPerson);
                LogPerson("Added:", addedPerson);
            }

            expectedSortedPersons = expectedSortedPersons
                .OrderByDescending(p => p.Name)
                .ToList();

            // Act
            var allPersons = _personsService.GetAllPersons();
            var sortedPersons = _personsService.GetSortedPersons(allPersons, nameof(Person.Name), SortOrderOptions.DESC);

            LogActual("Persons after sorting by Name DESC:");
            foreach (var sorted in sortedPersons)
            {
                LogPerson("Sorted:", sorted);
            }

            // Assert
            for (int i = 0; i < expectedSortedPersons.Count; i++)
            {
                Assert.Equal(expectedSortedPersons[i], sortedPersons[i]);
            }
        }

        #endregion

        #region UpdatePerson
        [Fact]
        public void UpdatePerson_WhenRequestIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            PersonUpdateRequest? personUpdateRequest = null;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => _personsService.UpdatePerson(personUpdateRequest));

            // Log
            LogExpected("throw ArgumentNullException");
            LogActual(ex.Message);
        }

        [Fact]
        public void UpdatePerson_WhenPersonIdIsInvalid_ThrowsArgumentException()
        {
            // Arrange
            var personUpdateRequest = new PersonUpdateRequest
            {
                PersonId = Guid.NewGuid()
            };

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _personsService.UpdatePerson(personUpdateRequest));

            // Log
            LogExpected("throw ArgumentException");
            LogActual(ex.Message);
        }

        [Fact]
        public void UpdatePerson_WhenNameIsNull_ThrowsArgumentException()
        {
            // Arrange
            var country = _countriesService.AddCountry(CountryTestData.Usa());
            var addRequest = PersonTestData.AvaMartinez(country.CountryId);
            var addedPerson = _personsService.AddPerson(addRequest);

            var updateRequest = addedPerson.ToPersonUpdateRequest();
            updateRequest.Name = null;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _personsService.UpdatePerson(updateRequest));

            // Log
            LogExpected("throw ArgumentException");
            LogActual(ex.Message);
        }

        [Fact]
        public void UpdatePerson_WhenDetailsAreValid_UpdatesAndReturnsUpdatedPerson()
        {
            // Arrange
            var country = _countriesService.AddCountry(CountryTestData.Canada());
            var addRequest = PersonTestData.JoyDelaCruz(country.CountryId);
            var addedPerson = _personsService.AddPerson(addRequest);

            var updateRequest = addedPerson.ToPersonUpdateRequest();
            updateRequest.Name = "Jane De Castro";
            updateRequest.Email = "janecastro@yahoo.com";

            // Act
            var updatedPerson = _personsService.UpdatePerson(updateRequest);
            var fetchedPerson = _personsService.GetPersonByPersonId(updateRequest.PersonId);

            // Assert
            Assert.Equal(fetchedPerson, updatedPerson);

            // Log
            LogPerson("Expected:", fetchedPerson);
            LogPerson("Actual:", updatedPerson);
        }
        #endregion

        #region DeletePerson

        [Fact]
        public void DeletePerson_WhenPersonIdIsInvalid_ReturnsFalse()
        {
            // Act
            var isDeleted = _personsService.DeletePerson(Guid.NewGuid());

            // Assert
            Assert.False(isDeleted);
        }

        [Fact]
        public void DeletePerson_WhenPersonIdIsValid_DeletesAndReturnsTrue()
        {
            // Arrange
            var country = _countriesService.AddCountry(CountryTestData.Canada());
            var addRequest = PersonTestData.LiamChen(country.CountryId);
            var addedPerson = _personsService.AddPerson(addRequest);

            // Act
            var isDeleted = _personsService.DeletePerson(addedPerson.PersonId);
            var fetchedAfterDelete = _personsService.GetPersonByPersonId(addedPerson.PersonId);

            // Assert
            Assert.True(isDeleted);
            Assert.Null(fetchedAfterDelete); // ensures deletion was effective
        }

        #endregion
    }

}
