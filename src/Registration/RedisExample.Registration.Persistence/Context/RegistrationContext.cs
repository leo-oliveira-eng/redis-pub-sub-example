using BaseEntity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RedisExample.Registration.Persistence.Context.Extensions;
using System.Reflection;

namespace RedisExample.Registration.Persistence.Context
{
    public class RegistrationContext : DbContext
    {
        public RegistrationContext(DbContextOptions<RegistrationContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Entity).IsAssignableFrom(type.ClrType) && (type.BaseType == null || !typeof(Entity).IsAssignableFrom(type.BaseType.ClrType)))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }
        }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatuses();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not Entity)
                    continue;

                switch (entry.State)
                {
                    case EntityState.Deleted:
                        ((Entity)entry.Entity).Delete();
                        entry.State = EntityState.Modified;
                        break;
                }
            }
        }
    }
}
