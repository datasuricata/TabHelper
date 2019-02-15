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
        #region [ dbsets ]

        public DbSet<User> Users { get; set; }
        public DbSet<Historic> Historics { get; set; }
        public DbSet<FormTab> FormTabs { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormAttribute> FormAttributes { get; set; }
        public DbSet<Tabulation> Tabulations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartTab> DepartTabs { get; set; }

        #endregion

        #region [ ctor ]

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        #endregion

        #region [ relationships ]

        protected override void OnModelCreating(ModelBuilder options)
        {
            // # users
            options.Entity<User>(c => c.HasKey(x => x.Id));
            options.Entity<User>(c => c.HasOne(x => x.Department).WithMany(x => x.Users));

            // # collaborator historic
            options.Entity<Historic>(c => c.HasKey(x => x.Id));

            // # tabulation forms
            options.Entity<Tabulation>(c => c.HasKey(x => x.Id));

            // # forms
            options.Entity<Form>(c => c.HasKey(x => x.Id));
            options.Entity<Form>(c => c.HasMany(x => x.Attributes));

            // # forms attributes
            options.Entity<FormAttribute>(c => c.HasKey(x => x.Id));

            // # departments
            options.Entity<Department>(c => c.HasKey(x => x.Id));
            options.Entity<Department>(c => c.HasMany(x => x.Users).WithOne(x => x.Department));

            // # relationship tabulations forms with attributes
            options.Entity<FormTab>().HasKey(bc => new { bc.TabulationId, bc.FormId });
            options.Entity<FormTab>().HasOne(bc => bc.Tabulation).WithMany(b => b.FormTabs).HasForeignKey(bc => bc.TabulationId);
            options.Entity<FormTab>().HasOne(bc => bc.Form).WithMany(c => c.FormTabs).HasForeignKey(bc => bc.FormId);

            // # relationship departments with tabulations
            options.Entity<DepartTab>().HasKey(bc => new { bc.DepartmentId, bc.TabulationId });
            options.Entity<DepartTab>().HasOne(bc => bc.Department).WithMany(b => b.DepartmentTabulations).HasForeignKey(bc => bc.DepartmentId);
            options.Entity<DepartTab>().HasOne(bc => bc.Tabulation).WithMany(b => b.DepartmentTabulations).HasForeignKey(bc => bc.TabulationId);
        }

        #endregion

        #region [ handlers ]

        /// <summary>
        /// Ajust date infos on entities after async commit
        /// </summary>
        /// <returns>Override SaveChanges</returns>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                foreach (var entry in ChangeTracker.Entries().Where(entry =>
                entry.Entity.GetType().GetProperty(nameof(EntityBase.CreatedAt)) != null ||
                entry.Entity.GetType().GetProperty(nameof(EntityBase.UpdatedAt)) != null))
                {
                    if (entry.Property(nameof(EntityBase.CreatedAt)) != null)
                        if (entry.State == EntityState.Added)
                            entry.Property(nameof(EntityBase.CreatedAt)).CurrentValue = DateTimeOffset.Now;
                        else if (entry.State == EntityState.Modified)
                            entry.Property(nameof(EntityBase.CreatedAt)).IsModified = false;

                    if (entry.Property(nameof(EntityBase.UpdatedAt)) != null)
                        if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                            entry.Property(nameof(EntityBase.UpdatedAt)).CurrentValue = DateTimeOffset.Now;
                }
            }
            catch (Exception e) //todo create log injection instance
            {
                var msg = e.Message;
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #endregion
    }
}
