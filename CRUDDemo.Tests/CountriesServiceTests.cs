using CRUDDemo.ServiceContracts;
using CRUDDemo.Services;

namespace CRUDDemo.Tests
{
    public class CountriesServiceTests
    {
        private readonly ICountriesService _countriesService;
        public CountriesServiceTests()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry
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
            var couhtryResponse = _countriesService.AddCountry(request);
            List<CountryResponse> responseList = _countriesService.GetAllCountries();

            // Assert
            Assert.NotEqual(Guid.Empty, couhtryResponse.CountryId);
            Assert.Equal("Canada", couhtryResponse.CountryName);
            Assert.Contains(couhtryResponse, responseList);
        }
        #endregion


        [Fact]
        public void GetAllCountries_WhenNoCountriesAdded_ReturnsEmptyList()
        {
            // Act
            List<CountryResponse> countries = _countriesService.GetAllCountries();

            // Assert
            Assert.Empty(countries);
        }

        [Fact]
        public void GetAllCountries_AfterAddingCountries_ReturnsAllAddedCountries()
        {
            // Arrange
            var countryRequests = new List<CountryAddRequest>
            {
                new CountryAddRequest { CountryName = "USA" },
                new CountryAddRequest { CountryName = "UK" },
                new CountryAddRequest { CountryName = "CA" }
            };

            var addedCountries = new List<CountryResponse>();
            foreach (var request in countryRequests)
            {
                var added = _countriesService.AddCountry(request);
                addedCountries.Add(added);
            }

            // Act
            var expectedCountries = _countriesService.GetAllCountries();

            // Assert
            foreach (var added in addedCountries)
            {
                Assert.Contains(added, expectedCountries);
            }
        }
    }
}