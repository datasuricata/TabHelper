using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TabHelper.Models.Base;
using TabHelper.Models.Entities;

namespace TabHelper.Data.ORM
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Historic> Historics { get; set; }
        DbSet<Tabulation> Tabulations { get; set; }
        DbSet<TabulationAttributes> TabulationAttributes { get; set; }
        DbSet<Forms> Forms { get; set; }

        public AppDbContext(DbContextOptions options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder options)
        {
            /// <summary>
            /// Apply a custom name to default schema db
            /// </summary>
            /// <returns>Override SaveChanges</returns>
            options.HasDefaultSchema("core");

            options.Entity<User>(c => c.HasKey(x => x.Id));
            options.Entity<User>(c => c.HasOne(x => x.Department).WithMany(x => x.Users));

            options.Entity<Historic>(c => c.HasKey(x => x.Id));
            
            options.Entity<Department>(c => c.HasKey(x => x.Id));
            options.Entity<Department>(c => c.HasMany(x => x.Tabs).WithOne(x => x.Department));
            options.Entity<Department>(c => c.HasMany(x => x.Users).WithOne(x => x.Department));
            
            options.Entity<Tabulation>(c => c.HasKey(x => x.Id));
            options.Entity<Tabulation>(c => c.HasOne(x => x.Department).WithMany(x => x.Tabs));
            options.Entity<Tabulation>(c => c.HasOne(x => x.Department).WithOne());            
            
            options.Entity<TabulationAttributes>(c => c.HasKey(x => x.Id));
            
            options.Entity<Forms>().HasKey(bc => new { bc.TabulationId, bc.TabulationAttributesId });
            options.Entity<Forms>().HasOne(bc => bc.Tabulation).WithMany(b => b.Forms).HasForeignKey(bc => bc.TabulationId);
            options.Entity<Forms>().HasOne(bc => bc.TabulationAttributes).WithMany(c => c.Forms).HasForeignKey(bc => bc.TabulationAttributesId);
        }

        /// <summary>
        /// Ajust date infos on entities after async commit
        /// </summary>
        /// <returns>Override SaveChanges</returns>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry =>
            entry.Entity.GetType().GetProperty(nameof(EntityBase.CreatedAt)) != null ||
            entry.Entity.GetType().GetProperty(nameof(EntityBase.UpdatedAt)) != null ||
            entry.Entity.GetType().GetProperty(nameof(EntityBase.Id)) != null))
            {
                if (entry.Property(nameof(EntityBase.CreatedAt)) != null)
                    if (entry.State == EntityState.Added)
                        entry.Property(nameof(EntityBase.CreatedAt)).CurrentValue = DateTime.Now;
                    else if (entry.State == EntityState.Modified)
                        entry.Property(nameof(EntityBase.CreatedAt)).IsModified = false;

                if (entry.Property(nameof(EntityBase.UpdatedAt)) != null)
                    if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                        entry.Property(nameof(EntityBase.UpdatedAt)).CurrentValue = DateTime.UtcNow;

                if (entry.Property(nameof(EntityBase.Id)) != null)
                    if (entry.State == EntityState.Added)
                        if (string.IsNullOrEmpty((string)entry.Property(nameof(EntityBase.Id)).CurrentValue))
                            entry.Property(nameof(EntityBase.Id)).CurrentValue = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
