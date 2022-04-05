using healthcare_visuzlier25.Session;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace healthcare_visuzlier25.Middleware
{
    public static class HttpHelpers
    {
        public static UserSession GetSessionFromHttpContext(this ClaimsPrincipal claimsPrincipal, ISessionService sessionService)
        {
            var token = claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.UserData).FirstOrDefault() ?? throw new Exception("Could not get claim from token");
            return sessionService.DecryptSessionForPerson(token.Value);
        }
    }
}
