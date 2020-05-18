using System;
using System.Collections.Generic;
using leave_manegement.Data;

namespace leave_manegement.Contracts
{
    public interface ILeaveAllocationRepository : IRepositorybase<LeaveAllocation>
    {
        public bool CheckAllocation(int leaveTypeId, string employeeId);
        ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id);
    }
}
