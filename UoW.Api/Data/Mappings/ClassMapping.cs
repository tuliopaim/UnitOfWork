
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UoW.Api.Domain.Entities;

namespace UoW.Api.Data.Mappings
{
    public class ClassMapping : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.Removed).HasDefaultValue(false);
            builder.Property(c => c.Name).HasMaxLength(200).IsRequired();
            builder.Property(c => c.Year).HasMaxLength(200).IsRequired();
            builder.Property(c => c.Code).ValueGeneratedOnAdd();
            builder.Property(c => c.TeacherName).HasMaxLength(200).IsRequired();

            builder.HasMany(c => c.Students).WithMany(s => s.Classes);
        }
    }
}