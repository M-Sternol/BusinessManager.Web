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
    public class EmployeeEducationConfiguration : IEntityTypeConfiguration<EmployeeEducation>
    {
        public void Configure(EntityTypeBuilder<EmployeeEducation> builder)
        {
            builder.HasKey(ee => ee.Id);
            builder.Property(e => e.GraduationDate)
                .HasColumnType("date");
            builder.HasOne(ee => ee.Employee)
                .WithMany(em => em.Educations)
                .HasForeignKey(ee => ee.EmployeeId);

            builder.HasMany(ee => ee.Languages)
                .WithOne(el => el.Education)
                .HasForeignKey(el => el.EducationId);

            builder.HasMany(ee => ee.Skills)
                .WithOne(es => es.Education)
                .HasForeignKey(es => es.EducationId);

            builder.HasMany(ee => ee.Certifications)
                .WithOne(ec => ec.Education)
                .HasForeignKey(ec => ec.EducationId);
        }
    }

}
