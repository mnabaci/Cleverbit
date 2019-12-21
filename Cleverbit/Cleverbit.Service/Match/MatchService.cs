using Cleverbit.Dto.Match;
using Cleverbit.Entity;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Cleverbit.Service.Match
{
    public class MatchService : IMatchService
    {
        private readonly CleverbitDbContext _dbContext;

        public MatchService(CleverbitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<UserMatchResultDto> CurrentMatchResults()
        {
            var nowDate = DateTime.Now;
            var result = _dbContext.UserMatchResults.Where(x => x.Match.StartDateTime <= nowDate && x.Match.EndDateTime >= nowDate).Select(x => new UserMatchResultDto
            {
                UserName = x.User.UserName,
                Result = x.Result
            }).ToList();
            return result;
        }
    }
}
