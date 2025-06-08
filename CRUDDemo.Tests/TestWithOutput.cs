using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDDemo.ServiceContracts.DTO;
using Xunit.Abstractions;

namespace CRUDDemo.Tests
{
    public abstract class TestWithOutput
    {
        protected readonly ITestOutputHelper Output;

        protected TestWithOutput(ITestOutputHelper output)
        {
            Output = output;
        }

        protected void LogExpected(string message)
        {
            Output.WriteLine($"Expected: {message}");
        }

        protected void LogActual(string message)
        {
            Output.WriteLine($"Actual: {message}");
        }

        protected void LogPerson(string label, PersonResponse? person)
        {
            Output.WriteLine($"{label}: {person?.ToString()}");
        }

        protected void LogPersonList(string label, IEnumerable<PersonResponse> persons)
        {
            Output.WriteLine($"{label}:");
            foreach (var person in persons)
            {
                Output.WriteLine(person?.ToString());
            }
        }

        protected void LogCountry(string label, CountryResponse? country)
        {
            Output.WriteLine($"{label}: {country?.ToString()}");
        }

        protected void LogCountryList(string label, IEnumerable<CountryResponse> countries)
        {
            Output.WriteLine($"{label}:");
            foreach (var country in countries)
            {
                Output.WriteLine(country?.ToString());
            }
        }
    }
}
