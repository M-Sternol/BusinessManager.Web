using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.Models.HR.Employee.Work.Contract
{
    public class EmploymentContract
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }
        public ContractType Type { get; set; }
        public int DurationMonths { get; set; }
        public string Position { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal MonthlyRate { get; set; }
    }
    public enum ContractType
    {
        EmploymentContract,
        ContractforSpecificTask,
        ContractWork,
    }

}
