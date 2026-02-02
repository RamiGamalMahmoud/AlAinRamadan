using AlAinRamadan.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlAinRamadan.Data.ModelsConfigurations
{
    internal class DisbursementConfiguration : IEntityTypeConfiguration<Disbursement>
    {
        public void Configure(EntityTypeBuilder<Disbursement> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Notes).IsRequired(false);

            builder.HasOne(x => x.Family)
                .WithMany()
                .HasForeignKey(x => x.FamilyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
