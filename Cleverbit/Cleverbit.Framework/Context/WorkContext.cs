using AutoMapper;
using Cleverbit.Common.Constants;
using Cleverbit.Entity;
using Cleverbit.Framework.Identity;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Security;

namespace Cleverbit.Framework.Context
{
    public class WorkContext : IWorkContext
    {
        private readonly CleverbitDbContext _dbContext;
        private readonly IMapper _mapper;
        public WorkContext(CleverbitDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this.UserManager = new CleverbitUserManager<UserIdentityModel>(new UserStore<UserIdentityModel>());
        }

        public CleverbitUserManager<UserIdentityModel> UserManager { get; }

        public bool IsLoggedIn => CurrentUser != null;

        public UserIdentityModel CurrentUser
        {
            get
            {
                if (this.cachedUser != null) return this.cachedUser;

                var userClaimsIdentity = AuthenticationManager.User.Identity as ClaimsIdentity;
                string userIdString = string.Empty;
                if (userClaimsIdentity != null && userClaimsIdentity.IsAuthenticated && userClaimsIdentity.Claims != null && userClaimsIdentity.Claims.Any(x => x.Type == Claims.UserId))
                {
                    userIdString = userClaimsIdentity.Claims.First(x => x.Type == Claims.UserId).Value;
                }

                UserIdentityModel user = null;
                Guid userId;
                if (Guid.TryParse(userIdString, out userId))
                {
                    user = this.UserManager.FindById(userId);
                }

                if (user != null)
                {
                    this.cachedUser = user;
                }
                else
                {
                    var userCookie = this.GetUserCookie();
                    if (userCookie != null && !string.IsNullOrEmpty(userCookie.Value) && Guid.TryParse(userCookie.Value, out userId))
                    {
                        var dbUser = _dbContext.Users.FirstOrDefault(x => x.Id == userId);

                        this.cachedUser = dbUser == null ? null : _mapper.Map<UserIdentityModel>(dbUser);
                    }
                }

                if (this.cachedUser != null)
                {
                    this.SetUserCookie(this.cachedUser.Id);
                }

                return this.cachedUser;
            }

            set
            {
                this.cachedUser = value;
                this.SetUserCookie(value?.Id);
            }
        }

        private UserIdentityModel cachedUser;
        private IAuthenticationManager AuthenticationManager => HttpContext.Current.GetOwinContext().Authentication;
        public void SignIn(UserIdentityModel user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = this.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            this.AuthenticationManager.SignIn(
                new AuthenticationProperties
                {
                    IsPersistent = isPersistent,
                    ExpiresUtc =
                            isPersistent
                                ? DateTimeOffset.UtcNow.AddMonths(1)
                                : DateTimeOffset.UtcNow.AddMinutes(20)
                },
                identity);
        }

        public void SignOut()
        {
            this.cachedUser = null;

            if (HttpContext.Current != null)
            {
                var httpContext = HttpContext.Current;
                foreach (var cookieKey in httpContext.Request.Cookies.AllKeys)
                {
                    var httpCookie = httpContext.Request.Cookies[cookieKey];
                    if (httpCookie != null)
                    {
                        httpContext.Response.Cookies[cookieKey].Domain = FormsAuthentication.CookieDomain;
                        httpContext.Response.Cookies[cookieKey].Value = null;
                        httpContext.Response.Cookies[cookieKey].Expires = DateTime.Now.AddYears(-3);
                    }
                }
            }

            //DeleteUserCookie();

            this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected void DeleteUserCookie()
        {
            if (HttpContext.Current != null && HttpContext.Current.Response != null)
            {
                if (HttpContext.Current.Response != null && HttpContext.Current.Response.Cookies[CookieConstants.UserIdCookie] != null)
                {
                    var userIdCookie = HttpContext.Current.Response.Cookies[CookieConstants.UserIdCookie];
                    userIdCookie.Expires = DateTime.Now.AddDays(-3);
                    userIdCookie.Value = null;
                    HttpContext.Current.Response.Cookies.Remove(CookieConstants.UserIdCookie);
                    HttpContext.Current.Response.Cookies.Add(userIdCookie);
                }

                if (HttpContext.Current.Request != null && HttpContext.Current.Request.Cookies[CookieConstants.UserIdCookie] != null)
                {
                    var userIdCookie = HttpContext.Current.Request.Cookies[CookieConstants.UserIdCookie];
                    userIdCookie.Expires = DateTime.Now.AddDays(-3);
                    userIdCookie.Value = null;
                    HttpContext.Current.Request.Cookies.Remove(CookieConstants.UserIdCookie);
                    HttpContext.Current.Request.Cookies.Add(userIdCookie);
                }
            }

        }

        protected HttpCookie GetUserCookie()
        {
            if (HttpContext.Current == null || HttpContext.Current.Request == null)
            {
                return null;
            }

            var cookie = HttpContext.Current.Request.Cookies[CookieConstants.UserIdCookie] ?? HttpContext.Current.Response.Cookies[CookieConstants.UserIdCookie];
            return cookie;
        }

        protected void SetUserCookie(Guid? userId)
        {
            try
            {
                if (HttpContext.Current != null && HttpContext.Current.Response != null)
                {
                    var cookie = new HttpCookie(CookieConstants.UserIdCookie) { HttpOnly = true, Value = userId.ToString() };

                    if (!string.IsNullOrEmpty(FormsAuthentication.CookieDomain) && !HttpContext.Current.Request.IsLocal)
                    {
                        cookie.Domain = FormsAuthentication.CookieDomain;
                    }

                    HttpContext.Current.Response.Cookies.Remove(CookieConstants.UserIdCookie);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }

                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                {
                    var cookie = new HttpCookie(CookieConstants.UserIdCookie) { HttpOnly = true, Value = userId.ToString() };

                    if (!string.IsNullOrEmpty(FormsAuthentication.CookieDomain) && !HttpContext.Current.Request.IsLocal)
                    {
                        cookie.Domain = FormsAuthentication.CookieDomain;
                    }

                    HttpContext.Current.Request.Cookies.Remove(CookieConstants.UserIdCookie);
                    HttpContext.Current.Request.Cookies.Add(cookie);
                }
            }
            catch
            {
                ////ToDo:Log yazılmalı mı?
            }
        }
    }
}
