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
    public class MedicalLeaveConfiguration : IEntityTypeConfiguration<MedicalLeave>
    {
        public void Configure(EntityTypeBuilder<MedicalLeave> builder)
        {
            builder.HasKey(ml => ml.Id);

            builder.Property(e => e.StartDate)
               .HasColumnType("date");
            builder.Property(e => e.EndDate)
                .HasColumnType("date");

            builder.HasOne(ml => ml.Employee)
                .WithMany(em => em.MedicalLeaves)
                .HasForeignKey(ml => ml.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
