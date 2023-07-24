using Microsoft.AspNetCore.Identity;

namespace LearningASP.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user,List<string> roles);
    }
}
