using Carglass.TechnicalAssessment.Backend.Entities.Entities;
using Microsoft.EntityFrameworkCore;


namespace Carglass.TechnicalAssessment.Backend.DL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DocType).IsRequired().HasMaxLength(10);
                entity.Property(e => e.DocNum).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.GivenName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.FamilyName1).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Phone).HasMaxLength(15);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductName).IsRequired().HasMaxLength(255);
            });
        }
    }
}
