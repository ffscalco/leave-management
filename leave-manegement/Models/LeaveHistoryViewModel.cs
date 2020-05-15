﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace leave_manegement.Models
{
    public class LeaveHistoryViewModel
    {
        public int Id { get; set; }

        public EmployeeViewModel RequestinEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public LeaveTypeViewModel LeaveType { get; set; }
        public string LeaveTypeId { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }

        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }

        public bool? Approved { get; set; }

        public EmployeeViewModel ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
    }
}
