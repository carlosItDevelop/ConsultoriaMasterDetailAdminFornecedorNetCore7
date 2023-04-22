using System.Collections.Generic;
using System.Security.Claims;

namespace Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces
{
    public interface IUserInAllLayer
    {
        IDictionary<string, string> DictionaryOfClaimss();
        IEnumerable<Claim> LisOfClaims();
    }
}
