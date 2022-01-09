using BaseEntity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace RedisExample.Registration.Persistence.Context.Extensions
{
    public static class ModelBuilderFilterExtensions
    {
        public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            SetSoftDeleteFilterMethod
                .MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder });
        }

        static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(ModelBuilderFilterExtensions)
                   .GetMethods(BindingFlags.Public | BindingFlags.Static)
                   .Single(t => t.IsGenericMethod && t.Name == "SetSoftDeleteFilter");

        public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder) where TEntity : Entity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.DeletedAt.HasValue);
        }
    }
}
