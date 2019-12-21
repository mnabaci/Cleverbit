namespace Cleverbit.Entity.Model
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
    }
}
