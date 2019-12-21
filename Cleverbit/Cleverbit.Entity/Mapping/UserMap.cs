using Cleverbit.Entity.Model;

namespace Cleverbit.Entity.Mapping
{
    public class UserMap : BaseMap<User>
    {
        public UserMap()
        {
            this.ToTable(nameof(User));

            this.HasKey(x => x.Id);
        }
    }
}
