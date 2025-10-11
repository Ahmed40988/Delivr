
using Microsoft.EntityFrameworkCore;

namespace Deliver.Entities.Extensionsك
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ModelBuilderExtensions).Assembly);
        }
    }
}
