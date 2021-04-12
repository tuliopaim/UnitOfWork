using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UoW.Api.Domain.Entities;

namespace UoW.Api.Data.Mappings
{
    public class StudentMapping : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.CreatedAt).IsRequired();
            builder.Property(s => s.Removed).HasDefaultValue(false);
            builder.Property(s => s.Name).HasMaxLength(200).IsRequired();
            builder.Property(s => s.BirthDate).IsRequired();
        }
    }
}