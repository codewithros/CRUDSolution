using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDDemo.ServiceContracts
{
    /// <summary>
    /// Response DTO representing a country returned by service operations.
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
    }
}
