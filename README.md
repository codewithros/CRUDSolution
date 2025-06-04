# CRUDDemo 🛠️

A clean architecture-style demo application built with C# and .NET Core 8.  
This project demonstrates the principles of separation of concerns using Entities, DTOs, Service Contracts, and Unit Testing.

---

## 📁 Project Structure

CRUDDemo.sln
│
├── CRUDDemo.Entities # Domain models (e.g., Country)
│ └── Country.cs
│
├── CRUDDemo.ServiceContracts # Service interfaces and DTOs
│ ├── DTO
│ │ ├── CountryAddRequest.cs
│ │ ├── CountryResponse.cs
│ │ └── Mappers
│ │ └── CountryExtensions.cs
│ └── ICountriesService.cs
│
├── CRUDDemo.Tests # xUnit test project
│ └── (To be added)
│
├── CRUDDemo # Main application (entry point)
│ └── wwwroot
│ └── site.css

---

## 🧱 Key Components

### `Country` Entity
Represents the domain model with `CountryId` and `CountryName`.

### DTOs
- `CountryAddRequest`: Used to accept input data when adding a new country.
- `CountryResponse`: Used to return country data from service operations.

### `ICountriesService`
Defines a contract for adding countries and retrieving responses.

### `CountryExtensions`
Provides mapping logic between `Country` and `CountryResponse`.

---

## 🧪 Tests

Unit tests will be added using **xUnit**, following TDD practices.  
Test class naming convention: `[ClassName]Tests` (e.g., `CountriesServiceTests`)

---

## 📌 TODO

- [x] Create domain entity `Country`
- [x] Define DTOs and mapping extensions
- [x] Define service contract `ICountriesService`
- [ ] Implement `CountriesService`
- [ ] Add unit tests for `CountriesService`
- [ ] Add persistence layer (e.g., in-memory or EF Core)
- [ ] Implement CRUD UI or API endpoints

---

## 🧑‍💻 Getting Started (To be added)

Instructions on how to run, build, and test the project will be added after the initial implementation is complete.

---

## 📄 License

MIT (or update as applicable)