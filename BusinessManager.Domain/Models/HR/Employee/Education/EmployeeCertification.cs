using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.Models.HR.Employee.Education
{
    public class EmployeeCertification
    {
        public int Id { get; set; }
        public string CertificationName { get; set; }
        public int EducationId { get; set; }
        public virtual EmployeeEducation Education { get; set; }

    }
}
