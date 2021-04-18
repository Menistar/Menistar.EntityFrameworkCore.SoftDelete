using Menistar.EntityFrameworkCore.SoftDelete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// Extension methods that can be used to implement soft delete for entities.
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// Configures the soft delete property used to register the delete moment for <typeparamref name="TEntity"/>. 
        /// A shadow property will be created and will be set to the utc datetime at the moment a <typeparamref name="TEntity"/> 
        /// is deleted in the context.
        /// </summary>
        /// <remarks>
        /// This extension method only configures the property that must be used for a specific entity. To enabled soft 
        /// delete as a feature <see cref="UseSoftDelete(DbContext)"/> must be called on the <see cref="DbContext"/>
        /// </remarks>
        /// <typeparam name="TEntity">The entity type being configured.</typeparam>
        /// <param name="entityTypeBuilder">The <see cref="EntityTypeBuilder{TEntity}"/> being extended.</param>
        /// <returns>The <see cref="EntityTypeBuilder{TEntity}"/> being extended.</returns>
        public static EntityTypeBuilder<TEntity> HasSoftDelete<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder)
            where TEntity : class
            => HasSoftDelete(entityTypeBuilder, SoftDeleteDefaults.DefaultPropertyName);

        /// <summary>
        /// Configures the soft delete property used to register the delete moment for <typeparamref name="TEntity"/>. 
        /// When no property is specified a shadow property is added. The property will be set to the utc datetime at 
        /// the moment a <typeparamref name="TEntity"/> is deleted in the context.
        /// </summary>
        /// <remarks>
        /// This extension method only configures the property that must be used for a specific entity. To enabled soft 
        /// delete as a feature <see cref="UseSoftDelete(DbContext)"/> must be called on the <see cref="DbContext"/>
        /// </remarks>
        /// <typeparam name="TEntity">The entity type being configured.</typeparam>
        /// <param name="entityTypeBuilder">The <see cref="EntityTypeBuilder{TEntity}"/> being extended.</param>
        /// <param name="propertyExpression">A optional lambda expression representing the property to be used for delete registration.</param>
        /// <returns>The <see cref="EntityTypeBuilder{TEntity}"/> being extended.</returns>
        public static EntityTypeBuilder<TEntity> HasSoftDelete<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, Expression<Func<TEntity, DateTime?>> propertyExpression = default)
            where TEntity : class
            => HasSoftDelete(entityTypeBuilder, entityTypeBuilder.Property(propertyExpression).Metadata.Name);           

        /// <summary>
        /// Configures the soft delete property used to register the delete moment for <typeparamref name="TEntity"/>. 
        /// When no property is specified a shadow property is added. The property will be set to the utc datetime at 
        /// the moment a <typeparamref name="TEntity"/> is deleted in the context.
        /// </summary>
        /// <remarks>
        /// This extension method only configures the property that must be used for a specific entity. To enabled soft 
        /// delete as a feature <see cref="UseSoftDelete(DbContext)"/> must be called on the <see cref="DbContext"/>
        /// </remarks>
        /// <typeparam name="TEntity">The entity type being configured.</typeparam>
        /// <param name="entityTypeBuilder">The <see cref="EntityTypeBuilder{TEntity}"/> being extended.</param>
        /// <param name="propertyName">The name of the property to be used for delete registration.</param>
        /// <returns>The <see cref="EntityTypeBuilder{TEntity}"/> being extended.</returns>
        public static EntityTypeBuilder<TEntity> HasSoftDelete<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, string propertyName)
            where TEntity : class
        {
            entityTypeBuilder.Property<DateTime?>(propertyName);
            entityTypeBuilder.HasAnnotation(SoftDeleteDefaults.AnnotationName, propertyName);
            return entityTypeBuilder.HasQueryFilter(e => EF.Property<DateTime?>(e, propertyName) == null);
        }
    }
}
