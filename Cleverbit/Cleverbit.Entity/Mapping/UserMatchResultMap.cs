using Cleverbit.Entity.Model;

namespace Cleverbit.Entity.Mapping
{
    public class UserMatchResultMap : BaseMap<UserMatchResult>
    {
        public UserMatchResultMap()
        {
            this.ToTable(nameof(UserMatchResult));

            this.HasRequired(x => x.User).WithMany(x => x.UserMatchResults).HasForeignKey(x => x.UserId);
            this.HasRequired(x => x.Match).WithMany(x => x.UserMatchResults).HasForeignKey(x => x.MatchId);
        }
    }
}
