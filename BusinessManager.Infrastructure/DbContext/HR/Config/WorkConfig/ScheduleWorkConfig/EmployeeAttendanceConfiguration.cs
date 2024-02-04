using BusinessManager.Domain.Models.HR.Employee.Work.ScheduleWork;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Infrastructure.DbContext.HR.Config.WorkConfig.ScheduleWorkConfig
{
    public class EmployeeAttendanceConfiguration : IEntityTypeConfiguration<EmployeeAttendance>
    {
        public void Configure(EntityTypeBuilder<EmployeeAttendance> builder)
        {
            builder.HasKey(ea => ea.Id);

            builder.HasOne(ea => ea.Employee)
                .WithMany(em => em.EmployeeAttendances)
                .HasForeignKey(ea => ea.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
