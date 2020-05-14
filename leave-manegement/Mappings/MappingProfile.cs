using System;
using AutoMapper;
using leave_manegement.Data;
using leave_manegement.Models;

namespace leave_manegement.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveType, DetailsLeaveTypeViewModel>().ReverseMap();
            CreateMap<LeaveType, CreateLeaveTypeViewModel>().ReverseMap();
            CreateMap<LeaveHistory, LeaveHistoryViewModel>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
