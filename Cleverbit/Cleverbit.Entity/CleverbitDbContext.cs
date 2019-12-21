using Cleverbit.Entity.Mapping;
using Cleverbit.Entity.Model;

using System.Data.Entity;

namespace Cleverbit.Entity
{
    public class CleverbitDbContext : DbContext
    {
        public CleverbitDbContext() : base("name=DefaultDbContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CleverbitDbContext>());
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserMap());
        }

        public DbSet<User> Users { get; set; }
    }
}
