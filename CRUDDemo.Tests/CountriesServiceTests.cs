using CRUDDemo.ServiceContracts;
using CRUDDemo.ServiceContracts.DTO;
using CRUDDemo.Services;
using Xunit.Abstractions;

namespace CRUDDemo.Tests
{
    public class CountriesServiceTests
    {
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;
        public CountriesServiceTests(ITestOutputHelper testOutputHelper)
        {
            _countriesService = new CountriesService();
            _testOutputHelper = testOutputHelper;
        }

        #region AddCountry
        [Fact]
        public void AddCountry_NullRequest_ThrowsArgumentNullException()
        {
            // Arrange
            CountryAddRequest? request = null;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => _countriesService.AddCountry(request));

            //Log
            _testOutputHelper.WriteLine($"Expected: throw ArgumentNullException");
            _testOutputHelper.WriteLine($"Actual: {ex.Message}");
        }

        [Fact]
        public void AddCountry_NullCountryName_ThrowsArgumentException()
        {
            // Arrange
            var request = new CountryAddRequest { CountryName = null };

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _countriesService.AddCountry(request));

            //Log
            _testOutputHelper.WriteLine($"Expected: throw ArgumentException");
            _testOutputHelper.WriteLine($"Actual: {ex.Message}");
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
            var ex = Assert.Throws<ArgumentException>(() => _countriesService.AddCountry(secondRequest));

            //Log
            _testOutputHelper.WriteLine($"Expected: throw ArgumentException");
            _testOutputHelper.WriteLine($"Actual: {ex.Message}");
        }

        [Fact]
        public void AddCountry_ValidRequest_ReturnsCountryResponseWithValidId()
        {
            // Arrange
            var request = new CountryAddRequest { CountryName = "Canada" };

            // Act
            var addedCountryResponse = _countriesService.AddCountry(request);
            List<CountryResponse> fetchedResponseList = _countriesService.GetAllCountries();

            //Log
            _testOutputHelper.WriteLine($"Expected: response has CountryId and exist in the fetched list");
            _testOutputHelper.WriteLine($"Actual: CountryId - {addedCountryResponse.CountryId}");

            // Assert
            Assert.NotEqual(Guid.Empty, addedCountryResponse.CountryId);
            Assert.Equal("Canada", addedCountryResponse.CountryName);
            Assert.Contains(addedCountryResponse, fetchedResponseList);
        }
        #endregion


        [Fact]
        public void GetAllCountries_WhenNoCountriesAdded_ReturnsEmptyList()
        {
            // Act
            List<CountryResponse> fetchedCountries = _countriesService.GetAllCountries();

            // Log
            _testOutputHelper.WriteLine($"Expected: Empty List");
            _testOutputHelper.WriteLine($"Actual: Total persons retrieved: {fetchedCountries.Count}");

            // Assert
            Assert.Empty(fetchedCountries);
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

            _testOutputHelper.WriteLine($"Expected: ");
            var addedCountries = new List<CountryResponse>();
            foreach (var request in countryRequests)
            {
                var added = _countriesService.AddCountry(request);
                addedCountries.Add(added);
                _testOutputHelper.WriteLine(added.ToString());
            }

            // Act
            var expectedCountries = _countriesService.GetAllCountries();

            // Assert
            _testOutputHelper.WriteLine($"Actual: ");
            foreach (var added in addedCountries)
            {
                Assert.Contains(added, expectedCountries);
                _testOutputHelper.WriteLine(added.ToString());
            }
        }

        #region GetCountryByCountryId
        [Fact]
        public void GetCountryByCountryId_WhenCountryIdIsNull_ReturnsNull()
        {
            // Arrange
            Guid? countryId = null;

            // Act
            var fetchedCountryResult = _countriesService.GetCountryByCountryId(countryId);

            // Log
            _testOutputHelper.WriteLine($"Expected: Fetched CountryResponse is null");
            _testOutputHelper.WriteLine($"Actual: Fetched CountryResponse - {fetchedCountryResult}");

            // Assert
            Assert.Null(fetchedCountryResult);
        }

        [Fact]
        public void GetCountryByCountryId_WhenCountryExists_ReturnsMatchingCountry()
        {
            // Arrange
            var countryAddRequest = new CountryAddRequest
            {
                CountryName = "Japan"
            };

            var addedCountry = _countriesService.AddCountry(countryAddRequest);

            // Act
            var fetchedCountryResponse = _countriesService.GetCountryByCountryId(addedCountry.CountryId);

            // Log
            _testOutputHelper.WriteLine($"Expected: Added CountryResponse with Id, Fetched Country By Id returns added country");
            _testOutputHelper.WriteLine($"Actual: CountryResponse - {addedCountry?.ToString()}");
            _testOutputHelper.WriteLine($"Fetched CountryResponse - {fetchedCountryResponse?.ToString()}");

            // Assert
            Assert.NotNull(addedCountry);
            Assert.Equal(addedCountry, fetchedCountryResponse);
        }
        #endregion
    }
}