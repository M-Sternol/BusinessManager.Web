﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;

namespace BusinessManager.Domain.Models.HR.Employee.Contact
{
    public class EmployeeContact
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int EmployeeId { get; set; }
        public virtual EmployeeModel Employee { get; set; }
    }
}
