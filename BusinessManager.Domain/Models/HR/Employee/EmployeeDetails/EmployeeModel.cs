using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Domain.Models.HR.Employee.Contact;
using BusinessManager.Domain.Models.HR.Employee.Education;
using BusinessManager.Domain.Models.HR.Employee.Work.Contract;
using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;

namespace BusinessManager.Domain.Models.HR.Employee.EmployeeDetails
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PESEL { get; set; }
        public string Position { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmploymentContract> EmploymentContracts { get; set; }
        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
        public virtual ICollection<MedicalLeave> MedicalLeaves { get; set; }
        public virtual ICollection<EmployeeAttendance> EmployeeAttendances { get; set; }


        public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; }
        public virtual ICollection<EmployeeAddress> Addresses { get; set; }
        public virtual ICollection<EmployeeContact> Contacts { get; set; }
        public virtual ICollection<EmployeeEducation> Educations { get; set; }

    }
}
