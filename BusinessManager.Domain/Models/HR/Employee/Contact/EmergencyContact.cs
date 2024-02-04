using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;

namespace BusinessManager.Domain.Models.HR.Employee.Contact
{
    public class EmergencyContact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }
    }

}
