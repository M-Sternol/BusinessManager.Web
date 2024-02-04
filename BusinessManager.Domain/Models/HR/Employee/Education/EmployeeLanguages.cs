using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.Models.HR.Employee.Education
{
    public class EmployeeLanguage
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }
        public LanguageProficiency ProficiencyLevel { get; set; }
        public int EducationId { get; set; }
        public virtual EmployeeEducation Education { get; set; }
    }
    public enum LanguageProficiency
    {
        A1,
        A2,
        B1,
        B2,
        C1,
        C2
    }

}
