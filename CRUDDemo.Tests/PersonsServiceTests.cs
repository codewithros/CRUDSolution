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

        public PersonsServiceTests(IPersonsService personsService)
        {
            _personsService = new PersonsService(); // use mock/real instance for now
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
    }

}
