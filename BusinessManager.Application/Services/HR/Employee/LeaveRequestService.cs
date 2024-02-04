using AutoMapper;
using BusinessManager.Application.Interfaces.HR.Employee;
using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.LeaveRequest;
using BusinessManager.Domain.Interfaces.HR.Employee;
using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessManager.Application.Services.HR.Employee
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public LeaveRequestService(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository ?? throw new ArgumentNullException(nameof(leaveRequestRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _employeeRepository = employeeRepository;
        }

        public async Task<int> CreateLeaveRequestAsync(LeaveRequestViewModel leaveRequestViewModel)
        {
            try 
            { 

                // Mapowanie danych na obiekt LeaveRequest
                var newLeaveRequest = _mapper.Map<LeaveRequest>(leaveRequestViewModel);

                // Dodaj wniosek o urlop do bazy danych
                var leaveRequest = await _leaveRequestRepository.AddLeaveRequestAsync(newLeaveRequest);
                return leaveRequest;
            }
            catch (Exception ex)
            {
                // Tutaj można obsłużyć wyjątek w bardziej szczegółowy sposób lub zalogować go
                throw new ApplicationException("Błąd podczas tworzenia wniosku o urlop.", ex);
            }
        }
        public async Task<IEnumerable<LeaveRequestViewModel>> FindOverlappingLeaveRequests(int employeeId, DateTime startDate, DateTime endDate)
        {
            var overlappingRequests = await _leaveRequestRepository.FindByConditionAsync(lr =>
                    lr.EmployeeId == employeeId &&
                    lr.StartDate < endDate &&
                    lr.EndDate > startDate)
                .ToListAsync();

            var viewModels = overlappingRequests.Select(lr => new LeaveRequestViewModel
            {
                Id = lr.Id,
                EmployeeId = lr.EmployeeId,
                FirstName = lr.Employee.FirstName,
                LastName = lr.Employee.LastName,
                StartDate = lr.StartDate,
                EndDate = lr.EndDate
            }).ToList();

            return viewModels;
        }

        public async Task<bool> DeleteLeaveRequestAsync(int leaveRequestId)
        {
            try
            {
                var leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveRequestId);
                if (leaveRequest == null)
                {
                    return false;
                }
                await _leaveRequestRepository.DeleteLeaveRequestAsync(leaveRequest);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Błąd podczas usuwania wniosku o urlop.", ex);
            }
        }

        public async Task<LeaveRequestViewModel> GetLeaveRequestByIdAsync(int leaveRequestId)
        {
            try
            {
                var leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveRequestId);
                return leaveRequest != null ? _mapper.Map<LeaveRequestViewModel>(leaveRequest) : null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Błąd podczas pobierania wniosku o urlop.", ex);
            }
        }
        
        public async Task<IPagedList<LeaveRequestForListViewModel>> GetLeaveRequestListAsync(int pageNumber, int pageSize, string searchString, string sortOrder)
        {
            try
            {
                var query = _leaveRequestRepository.GetAllLeaveRequests();

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    var searchStringLower = searchString.ToLower();
                    query = query.Where(e => (e.Employee.FirstName + " " + e.Employee.LastName).ToLower().Contains(searchStringLower));
                }

                sortOrder = string.IsNullOrEmpty(sortOrder) ? "name" : sortOrder;

                query = sortOrder switch
                {
                    "name_desc" => query.OrderByDescending(e => e.Employee.FirstName).ThenByDescending(e => e.Employee.LastName),
                    "startdate_desc" => query.OrderByDescending(e => e.StartDate),
                    "startdate" => query.OrderBy(e => e.StartDate),
                    "enddate_desc" => query.OrderByDescending(e => e.EndDate),
                    "enddate" => query.OrderBy(e => e.EndDate),
                    _ => query.OrderBy(e => e.Employee.FirstName).ThenBy(e => e.Employee.LastName),
                };

                var totalLeaveRequest = await query.CountAsync();
                var leaveRequest = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

                var leaveRequestViewModels = leaveRequest.Select(lr => new LeaveRequestForListViewModel
                    {
                        Id = lr.Id,
                        LastName = lr.LastName,
                        FirstName = lr.FirstName,
                        Type = lr.Type.ToString(),
                        StartDate = lr.StartDate,
                        EndDate = lr.EndDate,
                        Status = lr.Status.ToString(),
                    }).ToList();


                return new StaticPagedList<LeaveRequestForListViewModel>(leaveRequestViewModels, pageNumber, pageSize, totalLeaveRequest);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Błąd podczas pobierania listy wniosków o urlop.", ex);
            }
        }


        public async Task<LeaveRequestViewModel> GetLeaveRequestForEditAsync(int leaveId)
        {
            try
            {
                var leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveId);
                var leaveRequestViewModel = _mapper.Map<LeaveRequestViewModel>(leaveRequest);
                return leaveRequestViewModel;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Błąd podczas pobierania wniosku o urlop do edycji.", ex);
            }
        }


        public async Task UpdateLeaveRequestAsync(LeaveRequestViewModel leaveRequestViewModel)
        {
            try
            {
                var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestViewModel);
                await _leaveRequestRepository.UpdateLeaveRequestAsync(leaveRequest);
            }
            catch (Exception ex)
            {
                // Log the exception details here
                throw new ApplicationException($"Error updating leave request with ID {leaveRequestViewModel.Id}.", ex);
            }
        }
    }
}
