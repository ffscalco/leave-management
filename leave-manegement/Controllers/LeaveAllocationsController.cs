using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_manegement.Contracts;
using leave_manegement.Data;
using leave_manegement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace leave_manegement.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveAllocationsController : Controller
    {
        private readonly ILeaveTypeRepository _leavetyperepo;
        private readonly ILeaveAllocationRepository _leaveallocationrepo;
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;

        public LeaveAllocationsController(
            ILeaveTypeRepository leavetyperepo,
            ILeaveAllocationRepository leaveallocationrepo,
            UserManager<Employee> userManager,
            IMapper mapper
            )
        {
            _leavetyperepo = leavetyperepo;
            _leaveallocationrepo = leaveallocationrepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: LeaveAllocations
        public ActionResult Index()
        {
            var LeaveTypes = _leavetyperepo.FindAll().ToList();
            var mappedLeaveTypes = _mapper.Map<
                List<LeaveType>,
                List<LeaveTypeViewModel>>(LeaveTypes);

            var model = new CreateLeaveAllocationViewModel
            {
                LeaveTypes = mappedLeaveTypes,
                NumberUpdated = 0
            };

            return View(model);
        }

        public ActionResult SetLeave(int id)
        {
            var leavetype = _leavetyperepo.FindById(id);
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;

            foreach (var emp in employees)
            {
                if (_leaveallocationrepo.CheckAllocation(id, emp.Id))
                    continue;

                var allocation = new LeaveAllocationViewModel
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = emp.Id,
                    LeaveTypeId = id,
                    NumberOfDays = leavetype.DefaultDays,
                    Period = DateTime.Now.Year
                };

                var leaveAllocation = _mapper.Map<LeaveAllocation>(allocation);
                _leaveallocationrepo.Create(leaveAllocation);
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult ListEmployees()
        {
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            var model = _mapper.Map<List<EmployeeViewModel>>(employees);

            return View(model);
        }

        // GET: LeaveAllocations/Details/5
        public ActionResult Details(string id)
        { 
            var employee = _mapper.Map<EmployeeViewModel>(_userManager.FindByIdAsync(id).Result);
            var allocations = _mapper.Map<List<LeaveAllocationViewModel>>(_leaveallocationrepo.GetLeaveAllocationsByEmployee(id));
            var model = new ViewAllocationsViewModel
            {
                Employee = employee,
                LeaveAllocations = allocations
            };

            return View(model);
        }

        // GET: LeaveAllocations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeViewModel model)
        {
            try
            {
                //if(!ModelState.IsValid)
                //{
                //    return View(model);
                //}

                //var leaveType = _mapper.Map<LeaveType>(model);
                //leaveType.DateCreated = DateTime.Now;

                //var isSuccess = _leaveallocationrepo.Create(leaveType);

                //if (!isSuccess)
                //{
                //    ModelState.AddModelError("", "Something Went Wrong...");
                //    return View(model);
                //}


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocations/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_leaveallocationrepo.isExists(id))
            {
                return NotFound();
            }

            var leaveAllocation = _leaveallocationrepo.FindById(id);
            var model = _mapper.Map<EditLeaveAllocationViewModel>(leaveAllocation);

            return View(model);
        }

        // POST: LeaveAllocations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLeaveAllocationViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var record = _leaveallocationrepo.FindById(model.Id);
                record.NumberOfDays = model.NumberOfDays;

                var isSuccess = _leaveallocationrepo.Update(record);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View(model);
                }

                return RedirectToAction(nameof(Details),
                    new { id = model.EmployeeId });
            }
            catch
            {
                return View(model);
            }
        }

        // GET: LeaveAllocations/Delete/5
        public ActionResult Delete(int id)
        {
            if (!_leaveallocationrepo.isExists(id))
            {
                return NotFound();
            }

            var leaveType = _leaveallocationrepo.FindById(id);
            var isSuccess = _leaveallocationrepo.Delete(leaveType);

            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}