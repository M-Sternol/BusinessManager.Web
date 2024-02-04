using BusinessManager.Domain.Models.HR.Employee.Work.Contract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Infrastructure.DbContext.HR.Config.WorkConfig.ContractConfig
{
    public class EmploymentContractConfiguration : IEntityTypeConfiguration<EmploymentContract>
    {
        public void Configure(EntityTypeBuilder<EmploymentContract> builder)
        {
            builder.HasKey(ec => ec.Id);

            builder.Property(ec => ec.HourlyRate)
                .HasColumnType("decimal(18,2)"); 

            builder.Property(ec => ec.MonthlyRate)
                .HasColumnType("decimal(18,2)");
        }
    }
}
