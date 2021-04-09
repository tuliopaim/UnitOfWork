using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UoW.Api.Domain.Entities;

namespace UoW.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Class>().HasQueryFilter(c => c.Removed == false);
            modelBuilder.Entity<Student>().HasQueryFilter(c => c.Removed == false);
        }
        
        public DbSet<Class> Classes { get; set; }

        public DbSet<Student> Students { get; set; }
    }
}