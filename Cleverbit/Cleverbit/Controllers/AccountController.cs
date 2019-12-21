using AutoMapper;

using Cleverbit.Dto.Account;
using Cleverbit.Dto.Response;
using Cleverbit.Framework.Context;
using Cleverbit.Service.User;

using Microsoft.AspNet.Identity;

using System.Web.Http;

namespace Cleverbit.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AccountController(IWorkContext workContext, IMapper mapper, IUserService userService)
        {
            _workContext = workContext;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register(RegisterDto model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
            {
                return Ok(ResponseDto.Error("Please, check the parameters."));
            }


            if (model.Password != model.ConfirmPassword)
            {
                return Ok(ResponseDto.Error("Passwords are not same."));
            }

            var result = _userService.Register(model);

            if (result) return Ok(ResponseDto.Success());
            else return Ok(ResponseDto.Error());
        }

        [HttpPost]
        [Route("LogIn")]
        public IHttpActionResult LogIn(SignInDto model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
            {
                return Ok(ResponseDto.Error("Please, check the parameters."));
            }

            _workContext.SignOut();

            var user = _workContext.UserManager.Find(model.UserName, model.Password);

            if (user == null)
            {
                return Ok(ResponseDto.Error("User not found."));
            }

            _workContext.SignIn(user, model.RememberMe);

            return Ok(ResponseDto.Success());
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult SignOut()
        {
            _workContext.SignOut();
            return Ok(ResponseDto.Success());
        }
    }
}
