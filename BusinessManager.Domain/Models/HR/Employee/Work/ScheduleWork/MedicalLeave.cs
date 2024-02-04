using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;

namespace BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork
{
    public class MedicalLeave
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmployeeModel Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

}
