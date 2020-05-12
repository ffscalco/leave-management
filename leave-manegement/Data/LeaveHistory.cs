using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leave_manegement.Data
{
    public class LeaveHistory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RequestingEmployeeId")]
        public Employee RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("LeaveTypeId")]
        public Employee LeaveType { get; set; }
        public string LeaveTypeId { get; set; }

        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }

        public bool? Approved { get; set; }

        [ForeignKey("ApprovedById")]
        public Employee ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
    }
}
