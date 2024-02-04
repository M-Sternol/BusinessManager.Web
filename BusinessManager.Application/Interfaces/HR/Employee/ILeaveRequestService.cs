using BusinessManager.Application.ViewModel.HR.Employee.Work.ScheduleWork.LeaveRequest;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.Interfaces.HR.Employee
{
    public interface ILeaveRequestService
    {
        Task<IPagedList<LeaveRequestForListViewModel>> GetLeaveRequestListAsync(int pageNumber, int pageSize, string searchString, string sortOrder);
        Task<LeaveRequestViewModel> GetLeaveRequestByIdAsync(int leaveRequestId);
        Task<int> CreateLeaveRequestAsync(LeaveRequestViewModel leaveRequestViewModel);
        Task<IEnumerable<LeaveRequestViewModel>> FindOverlappingLeaveRequests(int employeeId, DateTime startDate, DateTime endDate);
        Task<LeaveRequestViewModel> GetLeaveRequestForEditAsync(int leaveId);
        Task UpdateLeaveRequestAsync(LeaveRequestViewModel leaveRequestViewModel);
        Task<bool> DeleteLeaveRequestAsync(int leaveRequestId);
    }
}
