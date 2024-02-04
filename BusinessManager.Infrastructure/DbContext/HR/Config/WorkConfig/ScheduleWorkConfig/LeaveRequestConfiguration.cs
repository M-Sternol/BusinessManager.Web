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
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasKey(lr => lr.Id);

            builder.Property(e => e.StartDate)
                .HasColumnType("date");
            builder.Property(e => e.EndDate)
                .HasColumnType("date");

            builder.HasOne(lr => lr.Employee)
                .WithMany(em => em.LeaveRequests)
                .HasForeignKey(lr => lr.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
