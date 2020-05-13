using System.Collections.Generic;
using leave_manegement.Data;

namespace leave_manegement.Contracts
{
    public interface ILeaveTypeRepository : IRepositorybase<LeaveType>
    {
        ICollection<LeaveType> GetEmployeesByLeaveType(int id);
    }
}
