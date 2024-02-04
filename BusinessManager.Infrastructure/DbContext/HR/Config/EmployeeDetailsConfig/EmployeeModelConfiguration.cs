using BusinessManager.Domain.Models.HR.Employee.EmployeeDetails;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Infrastructure.DbContext.HR.Config.EmployeeDetailsConfig
{
    public class EmployeeModelConfiguration : IEntityTypeConfiguration<EmployeeModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeModel> builder)
        {
            builder.HasKey(em => em.Id);

            builder.Property(e => e.BirthDate)
                .HasColumnType("date");

            builder.HasMany(em => em.EmergencyContacts)
                .WithOne(ec => ec.Employee)
                .HasForeignKey(ec => ec.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(em => em.Addresses)
                .WithOne(ea => ea.Employee)
                .HasForeignKey(ea => ea.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(em => em.Contacts)
                .WithOne(ecc => ecc.Employee)
                .HasForeignKey(ecc => ecc.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(em => em.Educations)
                .WithOne(ee => ee.Employee)
                .HasForeignKey(ee => ee.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
