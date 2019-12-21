using Cleverbit.Dto.Account;

namespace Cleverbit.Service.User
{
    public interface IUserService
    {
        bool Register(RegisterDto model);
    }
}
