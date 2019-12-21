using Cleverbit.Dto.Match;
using Cleverbit.Dto.Response;
using Cleverbit.Service.Match;

using System.Collections.Generic;
using System.Web.Http;

namespace Cleverbit.Controllers
{
    [RoutePrefix("api/Match")]
    public class MatchController : ApiController
    {
        private readonly IMatchService _matchService;
        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet]
        [Route("CurrentResults")]
        public IHttpActionResult CurrentMatchResults()
        {
            var result = _matchService.CurrentMatchResults();

            return Ok(ResponseDto<IEnumerable<UserMatchResultDto>>.Success(result));
        }
    }
}