using Cleverbit.Dto.Account;
using Cleverbit.Entity;
using Cleverbit.Framework.Helper;

using System;
using System.Linq;

namespace Cleverbit.Service.User
{
    public class UserService : IUserService
    {
        private readonly CleverbitDbContext _dbContext;

        public UserService(CleverbitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Register(RegisterDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password)) return false;

            var dbUser = _dbContext.Users.FirstOrDefault(x => x.UserName == model.UserName);

            if (dbUser != null) return false;
            var passwordSalt = CommonHelper.CreateSalt();
            var encodedPassword = CommonHelper.EncodePassword(model.Password, passwordSalt);

            var newUser = new Entity.Model.User
            {
                Id = Guid.NewGuid(),
                UserName = model.UserName,
                CreatedDateTime = DateTime.Now,
                Password = encodedPassword,
                PasswordSalt = passwordSalt
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
