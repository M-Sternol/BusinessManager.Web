using BusinessManager.Domain.Interfaces.HR.Employee;
using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;
using BusinessManager.Infrastructure.DbContext.HR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Infrastructure.Repository.HR.Employee
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly Context _context;
        public LeaveRequestRepository(Context context)
        {
            _context = context;
        }

        public async Task<int> AddLeaveRequestAsync(LeaveRequest newLeaveRequest)
        {
            try
            {
                _context.LeaveRequests.Add(newLeaveRequest);
                await _context.SaveChangesAsync();
                return newLeaveRequest.Id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException();
            }
        }
        public IQueryable<LeaveRequest> FindByConditionAsync(Expression<Func<LeaveRequest, bool>> expression)
        {
            return _context.LeaveRequests.Where(expression);
        }
        public async Task DeleteLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            try
            {
                _context.LeaveRequests.Remove(leaveRequest);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException();
            }
        }
        public async Task<LeaveRequest> GetLeaveRequestByIdAsync(int leaveRequestId)
        {
            try
            {
                var leaveRequest = await _context.LeaveRequests.FirstOrDefaultAsync(e => e.EmployeeId == leaveRequestId || e.Id == leaveRequestId);
                if (leaveRequest == null)
                {
                    throw new ApplicationException($"Nie znaleziono wniosku o urlop o ID {leaveRequestId}.");
                }

                return leaveRequest;
            }
            catch (Exception ex)
            {
                // Tutaj warto byłoby dodać logowanie błędu, abyś mógł śledzić i analizować problemy w przyszłości.
                throw new ApplicationException("Błąd podczas pobierania wniosku o urlop.", ex);
            }
        }

        public IQueryable<LeaveRequest> GetAllLeaveRequests()
        {
            try
            {
                return _context.LeaveRequests.AsQueryable();
            }
            catch (Exception ex)
            {
                throw new ApplicationException();
            }
        }
        public async Task UpdateLeaveRequestAsync(LeaveRequest updatedLeaveRequest)
        {
            try
            {
                _context.LeaveRequests.Attach(updatedLeaveRequest);
                _context.Entry(updatedLeaveRequest).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error updating leave request with ID {updatedLeaveRequest.Id}.", ex);
            }
        }

        public async Task<IEnumerable<LeaveRequest>> GetListLeaveRequestsByEmployeeIdAsync(int employeeId)
        {
            try
            {
                return await _context.LeaveRequests
                    .Where(lr => lr.EmployeeId == employeeId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving leave requests for employee with ID {employeeId}.", ex);
            }
        }
    }
}
