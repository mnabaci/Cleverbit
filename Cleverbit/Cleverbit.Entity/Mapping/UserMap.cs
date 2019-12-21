using Cleverbit.Entity.Model;

namespace Cleverbit.Entity.Mapping
{
    public class UserMap : BaseMap<User>
    {
        public UserMap()
        {
            this.ToTable(nameof(User));

            this.HasMany(x => x.UserMatchResults).WithRequired(x => x.User).HasForeignKey(x => x.UserId);
        }
    }
}
