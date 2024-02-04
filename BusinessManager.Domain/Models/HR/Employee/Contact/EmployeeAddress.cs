using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;

namespace BusinessManager.Domain.Models.HR.Employee.Contact
{
    public class EmployeeAddress
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string FlatNumber { get; set; }

        public int EmployeeId { get; set; }
        public virtual EmployeeModel Employee { get; set; }
    }
}
