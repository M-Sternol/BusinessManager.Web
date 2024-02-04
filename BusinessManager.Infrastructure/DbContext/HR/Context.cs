using BusinessManager.Domain.Models.HR.Employee.Contact;
using BusinessManager.Domain.Models.HR.Employee.Education;
using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;
using BusinessManager.Domain.Models.HR.Employee.Work.Contract;
using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;
using BusinessManager.Infrastructure.DbContext.HR.Config.ContactConfig;
using BusinessManager.Infrastructure.DbContext.HR.Config.EducationConfig;
using BusinessManager.Infrastructure.DbContext.HR.Config.EmployeeDetailsConfig;
using BusinessManager.Infrastructure.DbContext.HR.Config.WorkConfig.ContractConfig;
using BusinessManager.Infrastructure.DbContext.HR.Config.WorkConfig.ScheduleWorkConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Infrastructure.DbContext.HR
{
    public class Context : IdentityDbContext
    {
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public DbSet<EmployeeContact> EmployeeContacts { get; set; }
        public DbSet<EmployeeCertification> EmployeeCertifications { get; set; }
        public DbSet<EmployeeEducation> EmployeeEducations { get; set; }
        public DbSet<EmployeeLanguage> EmployeeLanguages { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<EmploymentContract> EmploymentContracts { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<MedicalLeave> MedicalLeaves { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        { 
        
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new EmergencyContactConfiguration());
            builder.ApplyConfiguration(new EmployeeAddressConfiguration());
            builder.ApplyConfiguration(new EmployeeContactConfiguration());
            builder.ApplyConfiguration(new EmployeeCertificationConfiguration());
            builder.ApplyConfiguration(new EmployeeEducationConfiguration());
            builder.ApplyConfiguration(new EmployeeLanguageConfiguration());
            builder.ApplyConfiguration(new EmployeeSkillConfiguration());
            builder.ApplyConfiguration(new EmploymentContractConfiguration());
            builder.ApplyConfiguration(new EmployeeAttendanceConfiguration());
            builder.ApplyConfiguration(new LeaveRequestConfiguration());
            builder.ApplyConfiguration(new MedicalLeaveConfiguration());
            builder.ApplyConfiguration(new WorkScheduleConfiguration());
            builder.ApplyConfiguration(new EmployeeModelConfiguration());

        }
    }
}
