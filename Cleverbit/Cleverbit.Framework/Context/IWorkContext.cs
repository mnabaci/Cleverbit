using Cleverbit.Framework.Identity;

namespace Cleverbit.Framework.Context
{
    public interface IWorkContext
    {
        CleverbitUserManager<UserIdentityModel> UserManager { get; }
        UserIdentityModel CurrentUser { get; }
        bool IsLoggedIn { get; }

        void SignIn(UserIdentityModel user, bool isPersistent);
        void SignOut();
    }
}
