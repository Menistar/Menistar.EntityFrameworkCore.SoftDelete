using Menistar.EntityFrameworkCore.SoftDelete;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// Extension methods that can be used to implement soft delete for entities.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Enables soft delete for entities that have soft delete configured. 
        /// </summary>
        /// <remarks>
        /// To configure a specific entity for soft delete use <see cref="EntityTypeBuilderExtensions.HasSoftDelete{TEntity}(Metadata.Builders.EntityTypeBuilder{TEntity}, System.Linq.Expressions.Expression{Func{TEntity, DateTime?}})"/> 
        /// on a entity builder.
        /// </remarks>
        /// <param name="context">The <see cref="DbContext"/> being configured.</param>
        public static void UseSoftDelete(this DbContext context)
        {
            context.ChangeTracker.StateChanged += (object sender, EntityStateChangedEventArgs e) =>
            {
                if (e.NewState == EntityState.Deleted)
                {
                    var softDeletePropertyName = e.Entry.Metadata.FindAnnotation(SoftDeleteDefaults.AnnotationName)?.Value.ToString();
                    if (!string.IsNullOrWhiteSpace(softDeletePropertyName))
                    {
                        e.Entry.State = EntityState.Unchanged;
                        e.Entry.Property(softDeletePropertyName).CurrentValue = DateTime.UtcNow;
                    }
                }
            };
        }
    }
}
