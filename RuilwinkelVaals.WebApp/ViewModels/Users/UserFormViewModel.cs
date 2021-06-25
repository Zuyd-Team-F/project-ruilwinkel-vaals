using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.ViewModels.Users
{
    public class UserFormViewModel
    {
        public int Id { get; set; }

        public int? BusinessId { get; set; }

        public int RoleId { get; set; }

        public SelectList Businesses { get; set; }

        public SelectList Roles { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public string StreetAdd { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int Balance { get; set; }

    }
}