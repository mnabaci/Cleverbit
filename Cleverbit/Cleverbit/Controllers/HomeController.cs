using AutoMapper;

using Cleverbit.Dto.Match;
using Cleverbit.Framework.Context;
using Cleverbit.Models;
using Cleverbit.Service.Match;

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cleverbit.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;
        private readonly IMatchService _matchService;

        public HomeController(IWorkContext workContext, IMatchService matchService, IMapper mapper)
        {
            _workContext = workContext;
            _matchService = matchService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var currentResult = _matchService.CurrentMatchResults() ?? new List<UserMatchResultDto>();
            var currentResultViewModel = _mapper.Map<IEnumerable<UserMatchResultViewModel>>(currentResult);

            var model = new HomeViewModel
            {
                UserMatchResults = currentResultViewModel.ToList()
            };
            return View(model);
        }
    }
}