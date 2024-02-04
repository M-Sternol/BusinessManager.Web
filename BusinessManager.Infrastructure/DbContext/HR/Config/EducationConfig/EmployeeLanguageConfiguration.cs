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
    public class EmployeeLanguageConfiguration : IEntityTypeConfiguration<EmployeeLanguage>
    {
        public void Configure(EntityTypeBuilder<EmployeeLanguage> builder)
        {
            builder.HasKey(el => el.Id);
            builder.HasOne(el => el.Education)
                .WithMany(ee => ee.Languages)
                .HasForeignKey(el => el.EducationId);

        }
    }
}
