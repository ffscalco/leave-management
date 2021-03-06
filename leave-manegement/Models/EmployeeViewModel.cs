﻿using System;
using System.ComponentModel.DataAnnotations;

namespace leave_manegement.Models
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string TaxId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }

        [Display(Name = "Full Name")]
        public string Fullname()
        {
            return Firstname + Lastname;
        }
    }
}
