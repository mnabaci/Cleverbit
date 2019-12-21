using System.Collections.Generic;

namespace Cleverbit.Entity.Model
{
    public class User : BaseEntity
    {
        public User()
        {
            UserMatchResults = new HashSet<UserMatchResult>();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        public virtual ICollection<UserMatchResult> UserMatchResults { get; set; }
    }
}
