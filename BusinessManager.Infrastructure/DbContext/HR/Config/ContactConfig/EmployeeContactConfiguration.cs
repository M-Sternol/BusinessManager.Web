using BusinessManager.Domain.Models.HR.Employee.Contact;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Infrastructure.DbContext.HR.Config.ContactConfig
{
    public class EmployeeContactConfiguration : IEntityTypeConfiguration<EmployeeContact>
    {
        public void Configure(EntityTypeBuilder<EmployeeContact> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Employee)
                .WithMany(em => em.Contacts)
                .HasForeignKey(e => e.EmployeeId);
        }
    }
}
