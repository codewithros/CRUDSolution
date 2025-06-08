using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDDemo.ServiceContracts.DTO;
using CRUDDemo.ServiceContracts.Enums;
using CRUDDemo.ServiceContracts;
using CRUDDemo.Services;

namespace CRUDDemo.Tests
{
    public class PersonsServiceTests
    {
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;

        public PersonsServiceTests()
        {
            _personsService = new PersonsService(); // use mock/real instance for now
            _countriesService = new CountriesService();
        }

        #region AddPerson

        [Fact]
        public void AddPerson_WhenRequestIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            PersonAddRequest? request = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _personsService.AddPerson(request));
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
            Assert.Throws<ArgumentException>(() => _personsService.AddPerson(request));
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
            var person = _personsService.AddPerson(request);
            var persons = _personsService.GetAllPersons();

            // Assert
            Assert.NotNull(person);
            Assert.NotEqual(Guid.Empty, person.CountryId);
            Assert.Contains(person, persons);
        }

        #endregion

        #region GetPersonByPersonId

        [Fact]
        public void GetPersonByPersonId_WhenPersonIdIsNull_ReturnsNull()
        {
            // Arrange
            Guid? personId = null;

            // Act
            var result = _personsService.GetPersonByPersonId(personId);

            // Assert
            Assert.Null(result);
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
            var result = _personsService.GetPersonByPersonId(addedPerson.PersonId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(addedPerson, result);
        }

        #endregion
        #region GetAllPersons

        [Fact]
        public void GetAllPersons_WhenNoPersonsExist_ReturnsEmptyList()
        {
            // Act
            var result = _personsService.GetAllPersons();

            // Assert
            Assert.Empty(result);
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

            var expectedPersons = new List<PersonResponse>();
            foreach (var request in personRequests)
            {
                var added = _personsService.AddPerson(request);
                expectedPersons.Add(added);
            }

            // Act
            var allPersons = _personsService.GetAllPersons();

            // Assert
            Assert.Equal(expectedPersons.Count, allPersons.Count);
            foreach (var person in expectedPersons)
            {
                Assert.Contains(person, allPersons);
            }
        }

        #endregion
    }

}
