using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Application.Common.Abstraction
{
    public interface IJwtService
    {
        string GenerateAccessToken(Guid userId,string userName, string email, List<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
