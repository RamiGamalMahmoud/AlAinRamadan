using AlAinRamadan.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlAinRamadan.Data.ModelsConfigurations
{
    internal class FamilyConfiguration : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.CardNumber).IsRequired();
            builder.Property(f => f.Name).IsRequired();

            builder.HasIndex(f => f.CardNumber).IsUnique();
        }
    }
}
