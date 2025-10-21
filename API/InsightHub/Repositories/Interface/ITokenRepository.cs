using Microsoft.AspNetCore.Identity;

namespace InsightHub.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
