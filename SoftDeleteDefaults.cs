namespace Menistar.EntityFrameworkCore.SoftDelete
{
    /// <summary>
    /// Defaults used by soft delete extension methods
    /// </summary>
    internal static class SoftDeleteDefaults
    {
        /// <summary>
        /// The annotation name used to store the property name used to soft delete a entity.
        /// </summary>
        internal static readonly string AnnotationName = "SoftDeletePropertyName";

        /// <summary>
        /// The name used for the shadow property that is defined when no property is configured.
        /// </summary>
        internal static readonly string DefaultPropertyName = "DeletedAt";
    }
}
