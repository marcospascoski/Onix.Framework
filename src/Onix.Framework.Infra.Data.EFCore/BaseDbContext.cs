using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Onix.Framework.Infra.Data.EFCore
{
    public class BaseDbContext(DbContextOptions options) : DbContext(options)
    {
        protected virtual void ConfigureDefaultTypes(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                   .SelectMany(t => t.GetProperties()))
            {
                if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                {
                    property.SetColumnType(DefaultDecimalType);
                }
                if (property.ClrType == typeof(bool) || property.ClrType == typeof(bool?))
                {
                    property.SetColumnType(DefaultBoolType);
                }
            }
        }

        protected virtual string DefaultDecimalType => "decimal(18, 2)";

        protected virtual string DefaultBoolType => "bit";
    }
}