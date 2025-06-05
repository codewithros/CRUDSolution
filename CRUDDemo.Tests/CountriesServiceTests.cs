using CRUDDemo.ServiceContracts;
using CRUDDemo.Services;

namespace CRUDDemo.Tests
{
    public class CountriesServiceTests
    {
       private readonly ICountriesService _countriesService;

        [Fact]
        public void AddCountry_NullRequest_ThrowsArgumentNullException()
        {
            // Arrange
            CountryAddRequest? request = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _countriesService.AddCountry(request));
        }

        [Fact]
        public void AddCountry_NullCountryName_ThrowsArgumentException()
        {
            // Arrange
            var request = new CountryAddRequest { CountryName = null };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _countriesService.AddCountry(request));
        }

        [Fact]
        public void AddCountry_DuplicateCountryName_ThrowsArgumentException()
        {
            // Arrange
            var firstRequest = new CountryAddRequest { CountryName = "USA" };
            var secondRequest = new CountryAddRequest { CountryName = "USA" };

            // Act
            _countriesService.AddCountry(firstRequest);

            // Assert
            Assert.Throws<ArgumentException>(() => _countriesService.AddCountry(secondRequest));
        }

        [Fact]
        public void AddCountry_ValidRequest_ReturnsCountryResponseWithValidId()
        {
            // Arrange
            var request = new CountryAddRequest { CountryName = "Canada" };

            // Act
            var response = _countriesService.AddCountry(request);

            // Assert
            Assert.NotEqual(Guid.Empty, response.CountryId);
            Assert.Equal("Canada", response.CountryName);
        }
    }
}