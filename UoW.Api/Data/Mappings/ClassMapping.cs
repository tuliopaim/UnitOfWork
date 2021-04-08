
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UoW.Api.Domain.Entities;

namespace UoW.Api.Data.Mappings
{
    public class ClassMapping : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            

        }
    }

    public class StudentMapping : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {


        }
    }

}