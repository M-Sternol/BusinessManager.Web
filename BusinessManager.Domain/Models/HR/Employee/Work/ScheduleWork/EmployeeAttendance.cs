using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;

namespace BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork
{
    public class EmployeeAttendance
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public string AbsenceReason { get; set; }
        public bool IsApproved { get; set; }
        public string Notes { get; set; }
    }
}
