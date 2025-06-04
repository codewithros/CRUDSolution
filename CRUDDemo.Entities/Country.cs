namespace CRUDDemo.Entities
{
    /// <summary>
    /// Represents a country domain entity with a unique identifier and name.
    /// </summary>
    public class Country
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
    }
}
