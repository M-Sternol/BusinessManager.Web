using BusinessManager.Application.Interfaces.HR.Employee;
using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.MedicalLeave;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManager.Controllers
{
    public class MedicalLeavesController : Controller
    {
        private readonly IMedicalLeaveService _medicalLeaveService;
        private readonly IEmployeeService _employeeService;

        public MedicalLeavesController(IMedicalLeaveService medicalLeaveService, IEmployeeService employeeService)
        {
            _medicalLeaveService = medicalLeaveService;
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
                if(searchString != null)
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
                var pagedMedicalLeaves = await _medicalLeaveService.GetMedicalLeavesListAsync(pageNumber, pageSize, searchString, sortOrder);
                return View(pagedMedicalLeaves);
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
                var medicalLeaves = await _medicalLeaveService.GetMedicalLeaveByIdAsync(id);
                if(medicalLeaves == null)
                {
                    return NotFound();
                }
                return View(medicalLeaves);
            }
            catch(Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(int employeeId)
        {
            ViewData["EmployeeId"] = employeeId;
            var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
            if(employee != null)
            {
                var medicalLeavesViewModel = new MedicalLeaveViewModel
                {
                    EmployeeId = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                };
                return(View(medicalLeavesViewModel));
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicalLeaveViewModel medicalLeave)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var employee = await _employeeService.GetEmployeeByIdAsync(medicalLeave.EmployeeId);
                    if(employee == null)
                    {
                        return NotFound();
                    }
                    var overlapping = await _medicalLeaveService.FindOverlappingMedicalLeaves(medicalLeave.EmployeeId, medicalLeave.StartDate, medicalLeave.EndDate);
                    if (overlapping.Any())
                    {
                        ModelState.AddModelError(string.Empty, "Istnieje już zwolnienie lekarskie w podanym zakresie dat.");
                        ViewData["EmployeeId"] = medicalLeave.EmployeeId;
                        return View(medicalLeave);
                    }
                    var newMedicalLeave = new MedicalLeaveViewModel
                    {
                        EmployeeId = medicalLeave.EmployeeId,
                        FirstName = medicalLeave.FirstName,
                        LastName = medicalLeave.LastName,
                        StartDate = medicalLeave.StartDate,
                        EndDate = medicalLeave.EndDate
                    };
                    await _medicalLeaveService.CreateMedicalLeaveAsync(newMedicalLeave);
                    return RedirectToAction("Details", "Employee", new {id =  medicalLeave.EmployeeId});
                }
                ViewData["EmployeeId"] = medicalLeave.EmployeeId;
                return View(medicalLeave);
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
                var medicalLeavesViewModel = await _medicalLeaveService.GetMedicalLeaveForEdit(id);
                if(medicalLeavesViewModel == null)
                {
                    return NotFound();
                }
                return View(medicalLeavesViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicalLeaveViewModel medicalLeave)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await _medicalLeaveService.UpdateMedicalLeaveAsync(medicalLeave);
                    return RedirectToAction("Details", "Employee", new {id = medicalLeave.EmployeeId});
                }
                return View(medicalLeave);
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
                var medicalLeaves = await _medicalLeaveService.DeleteMedicalLeaveAsync(id);
                if(medicalLeaves == null)
                {
                    return NotFound();
                }
                ViewData["EmployeeId"] = employeeId;
                return View(medicalLeaves);
            }
            catch(Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int employeeId)
        {
            try
            {
                ViewData["EmployeeId"] = employeeId;
                var result = await _medicalLeaveService.DeleteMedicalLeaveAsync(id);
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
