using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace RuilwinkelVaals.WebApp.ViewModels.Users
{
    public class UserInfoViewModel
    {
        public int Id { get; set; }

        public string Business { get; set; }

        public string Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public string StreetAdd { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int Balance { get; set; }

        public bool Blacklist { get; set; }
    }
}