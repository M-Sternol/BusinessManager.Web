using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;

namespace BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public LeaveType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveRequestStatus Status { get; set; }

    }
    public enum LeaveRequestStatus
    {
        Pending,
        Approved,
        Rejected
    }
    public enum LeaveType
    {
        Vacation,
        UnpaidLeave,
        OnDemand
    }


}
