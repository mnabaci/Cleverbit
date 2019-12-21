using Cleverbit.Entity.Model;

using System.Data.Entity.ModelConfiguration;

namespace Cleverbit.Entity.Mapping
{
    public class BaseMap<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
    }
}
