using CRUDDemo.ServiceContracts;
using CRUDDemo.ServiceContracts.DTO;
using CRUDDemo.Services;
using CRUDDemo.Tests.TestData;
using Xunit.Abstractions;

namespace CRUDDemo.Tests
{
    public class CountriesServiceTests : TestWithOutput
    {
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;
        public CountriesServiceTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
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
            LogExpected("throw ArgumentNullException");
            LogActual(ex.Message);
        }

        [Fact]
        public void AddCountry_NullCountryName_ThrowsArgumentException()
        {
            // Arrange
            var request = new CountryAddRequest { CountryName = null };

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _countriesService.AddCountry(request));

            //Log
            LogExpected("throw ArgumentException");
            LogActual(ex.Message);
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
            LogExpected("throw ArgumentException");
            LogActual(ex.Message);
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
            LogExpected("Response has CountryId and exist in the fetched list");
            LogActual($"CountryId - {addedCountryResponse.CountryId}");

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

            //Log
            LogExpected("Empty List");
            LogActual($"Fetched countries count: {fetchedCountries.Count}");

            // Assert
            Assert.Empty(fetchedCountries);
        }

        [Fact]
        public void GetAllCountries_AfterAddingCountries_ReturnsAllAddedCountries()
        {
            // Arrange
            var countryRequests = CountryTestData.CountryAddRequests;

            //Act
            LogExpected("");
            var addedCountries = new List<CountryResponse>();
            foreach (var request in countryRequests)
            {
                var added = _countriesService.AddCountry(request);
                addedCountries.Add(added);
                LogCountry("Added: ", added);
            }

            // Act
            var expectedCountries = _countriesService.GetAllCountries();

            // Assert
            LogActual("");
            foreach (var added in addedCountries)
            {
                Assert.Contains(added, expectedCountries);
                LogCountry("Fetched: ", added);
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

            //Log
            LogExpected("Fetched CountryResponse is null");
            LogActual($"Fetched CountryResponse - {fetchedCountryResult}");

            // Assert
            Assert.Null(fetchedCountryResult);
        }

        [Fact]
        public void GetCountryByCountryId_WhenCountryExists_ReturnsMatchingCountry()
        {
            // Arrange
            var countryAddRequest = CountryTestData.Japan();
            var addedCountry = _countriesService.AddCountry(countryAddRequest);

            // Act
            var fetchedCountryResponse = _countriesService.GetCountryByCountryId(addedCountry.CountryId);

            // Log
            LogExpected("Added CountryResponse with Id, ");
            LogExpected("Fetched Country By Id returns added country");
            LogActual($"Added CountryResponse - {addedCountry?.ToString()}");
            LogActual($"Fetched CountryResponse - {fetchedCountryResponse?.ToString()}");

            // Assert
            Assert.NotNull(addedCountry);
            Assert.Equal(addedCountry, fetchedCountryResponse);
        }
        #endregion
    }
}