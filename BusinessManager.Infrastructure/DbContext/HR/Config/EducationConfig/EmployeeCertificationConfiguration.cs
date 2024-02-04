using BusinessManager.Domain.Models.HR.Employee.Education;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Infrastructure.DbContext.HR.Config.EducationConfig
{
    public class EmployeeCertificationConfiguration : IEntityTypeConfiguration<EmployeeCertification>
    {
        public void Configure(EntityTypeBuilder<EmployeeCertification> builder)
        {
            builder.HasKey(ec => ec.Id);
            builder.HasOne(ec => ec.Education)
                .WithMany(e => e.Certifications)
                .HasForeignKey(ec => ec.EducationId);

        }
    }
}
