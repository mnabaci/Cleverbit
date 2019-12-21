using System;

namespace Cleverbit.Entity.Model
{
    public class UserMatchResult : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid MatchId { get; set; }
        public int Result { get; set; }

        public virtual User User { get; set; }
        public virtual Match Match { get; set; }
    }
}
