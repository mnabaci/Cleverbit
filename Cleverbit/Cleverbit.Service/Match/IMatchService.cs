using Cleverbit.Dto.Match;

using System.Collections.Generic;

namespace Cleverbit.Service.Match
{
    public interface IMatchService
    {
        IEnumerable<UserMatchResultDto> CurrentMatchResults();
    }
}
