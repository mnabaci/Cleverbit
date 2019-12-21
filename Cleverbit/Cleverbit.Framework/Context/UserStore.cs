using Cleverbit.Entity;
using Cleverbit.Entity.Model;
using Cleverbit.Framework.Identity;
using Microsoft.AspNet.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cleverbit.Framework.Context
{
    public class UserStore<TUser> : IUserLoginStore<UserIdentityModel, Guid>, IUserClaimStore<UserIdentityModel, Guid>, IUserRoleStore<UserIdentityModel, Guid>, IUserPasswordStore<UserIdentityModel, Guid>, IUserSecurityStampStore<UserIdentityModel, Guid>, IUserStore<UserIdentityModel, Guid>, IDisposable
        where TUser : UserIdentityModel
    {
        public Task AddClaimAsync(UserIdentityModel user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task AddLoginAsync(UserIdentityModel user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(UserIdentityModel user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(UserIdentityModel user)
        {
            var dbContext = new CleverbitDbContext();
            user.Id = user.Id != Guid.Empty ? user.Id : Guid.NewGuid();
            var userFromDb = dbContext.Users.FirstOrDefault(x => x.UserName == user.UserName || x.Id == user.Id);
            if (userFromDb == null)
            {
                userFromDb = new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    CreatedDateTime = DateTime.Now,
                    Password = user.Password,
                    PasswordSalt = user.PasswordSalt
                };

                dbContext.Users.Add(userFromDb);
                dbContext.SaveChanges();
            }


            return Task.Run(() => { return; });
        }

        public Task DeleteAsync(UserIdentityModel user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<UserIdentityModel> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<UserIdentityModel> FindByIdAsync(Guid userId)
        {
            var dbContext = new CleverbitDbContext();
            var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                var userIdentity = new UserIdentityModel
                {
                    Id = user.Id,
                    Password = user.Password,
                    PasswordSalt = user.PasswordSalt,
                    UserName = user.UserName,
                    UserClaims = new List<Claim> { new Claim(Claims.UserName, user.UserName), new Claim(Claims.UserId, user.Id.ToString()) }
                };
                return Task.Run(() => { return userIdentity; });
            }
            else
            {
                return Task.Run(() => { return default(UserIdentityModel); });
            }
        }

        public Task<UserIdentityModel> FindByNameAsync(string userName)
        {
            var dbContext = new CleverbitDbContext();
            var user = dbContext.Users.FirstOrDefault(x => x.UserName == userName);
            if (user != null)
            {
                var userIdentity = new UserIdentityModel
                {
                    Id = user.Id,
                    Password = user.Password,
                    PasswordSalt = user.PasswordSalt,
                    UserName = user.UserName,
                    UserClaims = new List<Claim> { new Claim(Claims.UserName, user.UserName), new Claim(Claims.UserId, user.Id.ToString()) }
                };
                return Task.Run(() => { return userIdentity; });
            }
            else
            {
                return Task.Run(() => { return default(UserIdentityModel); });
            }

        }

        public Task<IList<Claim>> GetClaimsAsync(UserIdentityModel user)
        {
            return Task.Run(() => { return user.UserClaims; });
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(UserIdentityModel user)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(UserIdentityModel user)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(UserIdentityModel user)
        {
            return Task.Run<IList<string>>(() => { return new List<string> { Roles.User }; });
        }

        public Task<string> GetSecurityStampAsync(UserIdentityModel user)
        {
            return Task.Run(() => user.PasswordSalt);
        }

        public Task<bool> HasPasswordAsync(UserIdentityModel user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(UserIdentityModel user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(UserIdentityModel user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(UserIdentityModel user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(UserIdentityModel user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(UserIdentityModel user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task SetSecurityStampAsync(UserIdentityModel user, string stamp)
        {
            return Task.Run(() => { return; });
        }

        public Task UpdateAsync(UserIdentityModel user)
        {
            throw new NotImplementedException();
        }
    }
}
