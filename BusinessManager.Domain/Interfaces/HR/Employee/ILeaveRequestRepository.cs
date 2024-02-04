using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.Interfaces.HR.Employee
{
    public interface ILeaveRequestRepository
    {
        Task<int> AddLeaveRequestAsync(LeaveRequest newLeaveRequest);
        Task DeleteLeaveRequestAsync(LeaveRequest leaveRequest);
        IQueryable<LeaveRequest> FindByConditionAsync(Expression<Func<LeaveRequest, bool>> expression);
        Task<LeaveRequest> GetLeaveRequestByIdAsync(int leaveRequestId);
        IQueryable<LeaveRequest> GetAllLeaveRequests();
        Task UpdateLeaveRequestAsync(LeaveRequest newLeaveRequest);

    }
}
