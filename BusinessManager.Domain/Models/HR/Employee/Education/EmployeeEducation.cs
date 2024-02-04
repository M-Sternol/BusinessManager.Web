using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;

namespace BusinessManager.Domain.Models.HR.Employee.Education
{
    public class EmployeeEducation
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public virtual EmployeeModel Employee { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public string SchoolName { get; set; }
        public DateTime GraduationDate { get; set; }
        public bool IsCurrentlyEnrolled { get; set; }

        public virtual ICollection<EmployeeLanguage> Languages { get; set; }
        public virtual ICollection<EmployeeSkill> Skills { get; set; }
        public virtual ICollection<EmployeeCertification> Certifications { get; set; }
    }
    public enum EducationLevel
    {
        Primary,
        Secondary,
        Higher
    }

}
