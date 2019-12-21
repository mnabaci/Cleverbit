using System;
using System.Collections.Generic;

namespace Cleverbit.Entity.Model
{
    public class Match : BaseEntity
    {
        public Match()
        {
            UserMatchResults = new HashSet<UserMatchResult>();
        }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public virtual ICollection<UserMatchResult> UserMatchResults { get; set; }
    }
}
