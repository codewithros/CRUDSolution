using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDDemo.ServiceContracts.DTO;
using CRUDDemo.ServiceContracts.Enums;

namespace CRUDDemo.Tests.TestData
{
    public static class PersonTestData
    {
        public static List<PersonAddRequest> PersonAddRequests(Guid usaId, Guid canadaId, Guid japanId) => new()
        {
            AvaMartinez(usaId),
            LiamChen(canadaId),
            CaseyLee(japanId)
        };

        public static PersonAddRequest EthanSantos(Guid countryId) => new()
        {
            Name = "Ethan Santos",
            Email = "ethan@gmail.com",
            DateOfBirth = new DateTime(2007, 12, 07),
            Gender = GenderOptions.Male,
            Address = "969 Smith St. Port Colborne, ON L3K 5M7",
            CountryId = countryId,
            ReceiveNewsLetters = true
        };

        public static PersonAddRequest JoyDelaCruz(Guid countryId) => new()
        {
            Name = "Joy Dela Cruz",
            Email = "joydc@gmail.com",
            DateOfBirth = new DateTime(2002, 09, 21),
            Gender = GenderOptions.Female,
            Address = "102-2255 Carling Ave Ottawa, ON K2B 7Z5",
            CountryId = countryId,
            ReceiveNewsLetters = true
        };

        public static PersonAddRequest AvaMartinez(Guid countryId) => new()
        {
            Name = "Ava Martinez",
            Email = "ava.martinez@example.com",
            DateOfBirth = new DateTime(2002, 09, 21),
            Gender = GenderOptions.Female,
            Address = "742 Evergreen Terrace, Springfield, IL 62704, USA",
            CountryId = countryId,
            ReceiveNewsLetters = true
        };

        public static PersonAddRequest LiamChen(Guid countryId) => new()
        {
            Name = "Liam Chen",
            Email = "liam.chen@example.com",
            DateOfBirth = new DateTime(1999, 3, 15),
            Gender = GenderOptions.Male,
            Address = "55 Front St W, Toronto, ON M5J 1E6",
            CountryId = countryId,
            ReceiveNewsLetters = false
        };

        public static PersonAddRequest CaseyLee(Guid countryId) => new()
        {
            Name = "Casey Lee",
            Email = "casey.lee@example.com",
            DateOfBirth = new DateTime(1995, 7, 30),
            Gender = GenderOptions.Other,
            Address = "1-1 Chiyoda, Chiyoda City, Tokyo 100-8111, Japan",
            CountryId = countryId,
            ReceiveNewsLetters = true
        };
    }
}
