using Cleverbit.Entity.Model;

namespace Cleverbit.Entity.Mapping
{
    public class MatchMap : BaseMap<Match>
    {
        public MatchMap()
        {
            this.ToTable(nameof(Match));

            this.HasMany(x => x.UserMatchResults).WithRequired(x => x.Match).HasForeignKey(x => x.MatchId);
        }
    }
}
