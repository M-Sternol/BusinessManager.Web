using BusinessManager.Application.Interfaces.HR.Employee;
using BusinessManager.Application.ViewModel.HR.Employee.EmployeeDetails;
using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.LeaveRequest;
using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace BusinessManager.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IEmployeeService _employeeService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService, IEmployeeService employeeService)
        {
            _leaveRequestService = leaveRequestService;
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.StartDateSortParm = sortOrder == "startdate_desc" ? "startdate" : "startdate_desc";
                ViewBag.EndDateSortParm = sortOrder == "enddate_desc" ? "enddate" : "enddate_desc";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var pageSize = 10;
                var pageNumber = page ?? 1;
                var pagedLeaveRequest = await _leaveRequestService.GetLeaveRequestListAsync(pageNumber, pageSize, searchString, sortOrder);

                return View(pagedLeaveRequest);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                ViewData["EmployeeId"] = id;
                var leaveRequest = await _leaveRequestService.GetLeaveRequestByIdAsync(id);
                if (leaveRequest == null)
                {
                    return NotFound();
                }
                return View(leaveRequest);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create(int employeeId)
        {
            try
            {
                ViewData["EmployeeId"] = employeeId;

                var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);

                if (employee != null)
                {
                    var leaveRequestViewModel = new LeaveRequestViewModel
                    {
                        EmployeeId = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                    };

                    return View(leaveRequestViewModel);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestViewModel leaveRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = await _employeeService.GetEmployeeByIdAsync(leaveRequest.EmployeeId);
                    if (employee == null)
                    {
                        return NotFound();
                    }

                    var overlappingRequests = await _leaveRequestService.FindOverlappingLeaveRequests(leaveRequest.EmployeeId, leaveRequest.StartDate, leaveRequest.EndDate);
                    if (overlappingRequests.Any())
                    {
                        ModelState.AddModelError(string.Empty, "Istnieje już wniosek urlopowy w podanym zakresie dat.");
                        ViewData["EmployeeId"] = leaveRequest.EmployeeId;
                        return View(leaveRequest);
                    }

                    var newLeaveRequest = new LeaveRequestViewModel
                    {
                        EmployeeId = leaveRequest.EmployeeId,
                        FirstName = leaveRequest.FirstName,
                        LastName = leaveRequest.LastName,
                        Type = leaveRequest.Type,
                        StartDate = leaveRequest.StartDate,
                        EndDate = leaveRequest.EndDate,
                        Status = leaveRequest.Status
                    };

                    await _leaveRequestService.CreateLeaveRequestAsync(newLeaveRequest);

                    return RedirectToAction("Details", "Employee", new { id = leaveRequest.EmployeeId });
                }

                ViewData["EmployeeId"] = leaveRequest.EmployeeId;
                return View(leaveRequest);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, int employeeId)
        {
            try
            {
                ViewData["EmployeeId"] = employeeId;
                var leaveRequestViewModel = await _leaveRequestService.GetLeaveRequestForEditAsync(id);
                if(leaveRequestViewModel == null)
                {
                    return NotFound();
                }
                return View(leaveRequestViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LeaveRequestViewModel leaveRequestEdit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _leaveRequestService.UpdateLeaveRequestAsync(leaveRequestEdit);
                    return RedirectToAction("Details", "Employee", new { id = leaveRequestEdit.EmployeeId });
                }

                return View(leaveRequestEdit);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, int employeeId)
        {
            try
            {
                var leaveRequest = await _leaveRequestService.GetLeaveRequestByIdAsync(id);

                if (leaveRequest == null)
                {
                    return NotFound();
                }
                ViewData["EmployeeId"] = employeeId;
                return View(leaveRequest);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,int employeeId)
        {
            try
            {
                ViewData["EmployeeId"] = employeeId;
                var result = await _leaveRequestService.DeleteLeaveRequestAsync(id);

                if (result)
                {
                    return RedirectToAction("Details", "Employee", new { id = employeeId });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

    }
}
