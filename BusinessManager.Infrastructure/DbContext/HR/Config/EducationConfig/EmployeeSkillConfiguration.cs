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
    public class EmployeeSkillConfiguration : IEntityTypeConfiguration<EmployeeSkill>
    {
        public void Configure(EntityTypeBuilder<EmployeeSkill> builder)
        {
            builder.HasKey(es => es.Id);
            builder.HasOne(es => es.Education)
                .WithMany(ee => ee.Skills)
                .HasForeignKey(es => es.EducationId);
        }
    }
}
