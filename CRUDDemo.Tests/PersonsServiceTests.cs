using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDDemo.ServiceContracts.DTO;
using CRUDDemo.ServiceContracts.Enums;
using CRUDDemo.ServiceContracts;
using CRUDDemo.Services;
using Xunit.Abstractions;

namespace CRUDDemo.Tests
{
    public class PersonsServiceTests
    {
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;

        public PersonsServiceTests(ITestOutputHelper testOutputHelper)
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
            _testOutputHelper.WriteLine($"Expected: throw ArgumentNullException");
            _testOutputHelper.WriteLine($"Actual: {ex.Message}");
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
            _testOutputHelper.WriteLine($"Expected: throw ArgumentException");
            _testOutputHelper.WriteLine($"Actual: {ex.Message}");
        }

        [Fact]
        public void AddPerson_WhenValidRequest_ReturnsAndStoresPerson()
        {
            // Arrange
            var request = new PersonAddRequest
            {
                Name = "Ethan Santos",
                Email = "ethan@gmail.com",
                DateOfBirth = new DateTime(2007, 12, 07),
                Gender = GenderOptions.Male,
                Address = "969 Smith St.\r\nPort Colborne, ON L3K 5M7",
                CountryId = Guid.NewGuid(),
                ReceiveNewsLetters = true
            };

            // Act
            var addedPerson = _personsService.AddPerson(request);
            var fetchedPersons = _personsService.GetAllPersons();

            //Log
            _testOutputHelper.WriteLine($"Expected: response has PersonId and exist in the fetched list");
            _testOutputHelper.WriteLine($"Actual: Person Id - {addedPerson.PersonId}");

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

            // Log
            _testOutputHelper.WriteLine($"Expected: PersonResponse is null");
            _testOutputHelper.WriteLine($"Actual: PersonResponse - {fetchedPersonById}");

            // Assert
            Assert.Null(fetchedPersonById);
        }

        [Fact]
        public void GetPersonByPersonId_WhenPersonExists_ReturnsMatchingPerson()
        {
            // Arrange
            var countryRequest = new CountryAddRequest
            {
                CountryName = "Canada"
            };

            var country = _countriesService.AddCountry(countryRequest);

            var personRequest = new PersonAddRequest
            {
                Name = "Joy Dela Cruz",
                Email = "joydc@gmail.com",
                DateOfBirth = new DateTime(2002, 09, 21),
                Gender = GenderOptions.Female,
                Address = "102-2255 Carling Ave Ottawa, ON K2B 7Z5",
                CountryId = country.CountryId,
                ReceiveNewsLetters = true
            };

            var addedPerson = _personsService.AddPerson(personRequest);

            // Act
            var fetchedPerson = _personsService.GetPersonByPersonId(addedPerson.PersonId);

            // Log
            _testOutputHelper.WriteLine($"Expected: Added PersonResponse Returned with PersonId, Fetched Person By Id returns added person");
            _testOutputHelper.WriteLine($"Actual: Added PersonResponse - {addedPerson?.ToString()}");
            _testOutputHelper.WriteLine($"Fetchc PersonResponse - {fetchedPerson?.ToString()}");

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
            _testOutputHelper.WriteLine($"Expected: Empty List");
            _testOutputHelper.WriteLine($"Actual: Total persons retrieved: {fetchedPersons.Count}");

            // Assert
            Assert.Empty(fetchedPersons);
        }

        [Fact]
        public void GetAllPersons_WhenPersonsAdded_ReturnsAllAddedPersons()
        {
            // Arrange
            var usa = _countriesService.AddCountry(new CountryAddRequest { CountryName = "USA" });
            var canada = _countriesService.AddCountry(new CountryAddRequest { CountryName = "Canada" });
            var japan = _countriesService.AddCountry(new CountryAddRequest { CountryName = "Japan" });

            var personRequests = new List<PersonAddRequest>
            {
                new PersonAddRequest
                {
                    Name = "Ava Martinez",
                    Email = "ava.martinez@example.com",
                    DateOfBirth = new DateTime(2002, 09, 21),
                    Gender = GenderOptions.Female,
                    Address = "742 Evergreen Terrace, Springfield, IL 62704, USA",
                    CountryId = usa.CountryId,
                    ReceiveNewsLetters = true
                },
                new PersonAddRequest
                {
                    Name = "Liam Chen",
                    Email = "liam.chen@example.com",
                    DateOfBirth = new DateTime(1999, 3, 15),
                    Gender = GenderOptions.Male,
                    Address = "55 Front St W, Toronto, ON M5J 1E6",
                    CountryId = canada.CountryId,
                    ReceiveNewsLetters = false
                },
                new PersonAddRequest
                {
                    Name = "Casey Lee",
                    Email = "casey.lee@example.com",
                    DateOfBirth = new DateTime(1995, 7, 30),
                    Gender = GenderOptions.Other,
                    Address = "1-1 Chiyoda, Chiyoda City, Tokyo 100-8111, Japan",
                    CountryId = japan.CountryId,
                    ReceiveNewsLetters = true
                }
            };

            _testOutputHelper.WriteLine($"Expected: ");
            var expectedPersons = new List<PersonResponse>();
            foreach (var request in personRequests)
            {
                var added = _personsService.AddPerson(request);
                expectedPersons.Add(added);
                _testOutputHelper.WriteLine(added.ToString());
            }

            // Act
            var allPersons = _personsService.GetAllPersons();

            // Assert
            _testOutputHelper.WriteLine($"Actual: ");
            Assert.Equal(expectedPersons.Count, allPersons.Count);
            foreach (var person in expectedPersons)
            {
                Assert.Contains(person, allPersons);
                _testOutputHelper.WriteLine(person.ToString());
            }
        }

        #endregion
    }

}
