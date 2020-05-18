using System;
using System.ComponentModel.DataAnnotations;

namespace leave_manegement.Models
{
    public class LeaveTypeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Display(Name="Date Created")]
        public DateTime? DateCreated { get; set; }

        [Required]
        [Range(1, 25, ErrorMessage = "Please enter a valid number")]
        [Display(Name = "Default Number Of Days")]
        public string DefaultDays { get; set; }
    }
}
