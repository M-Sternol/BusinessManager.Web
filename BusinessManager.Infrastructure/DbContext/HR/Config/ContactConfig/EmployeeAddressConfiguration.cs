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
    public class EmployeeAddressConfiguration : IEntityTypeConfiguration<EmployeeAddress>
    {
        public void Configure(EntityTypeBuilder<EmployeeAddress> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Country);
            builder.Property(e => e.City);
            builder.Property(e => e.Region);
            builder.Property(e => e.PostalCode);
            builder.Property(e => e.Street);
            builder.Property(e => e.BuildingNumber);
            builder.Property(e => e.FlatNumber);

            builder.HasOne(e => e.Employee)
                .WithMany(em => em.Addresses)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict); // Opcjonalnie, jeśli chcesz zablokować usuwanie pracownika w przypadku, gdy ma powiązane adresy

            builder.ToTable("EmployeeAddresses"); // Jeśli chcesz określić nazwę tabeli w bazie danych
        }
    }

}
