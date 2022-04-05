using healthcare_visuzlier25.Session;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace healthcare_visuzlier25.Middleware
{
    public class SessionValidatorSchemeOptions
      : AuthenticationSchemeOptions
    { }
    public class SessionValidator : AuthenticationHandler<SessionValidatorSchemeOptions>
    {
        private static string TokenHeaderName = "x-base-token";
        private readonly ISessionService _sessionService;
        public SessionValidator(IOptionsMonitor<SessionValidatorSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, ISessionService sessionService)
            : base(options, logger, encoder, clock)
        {
            _sessionService = sessionService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(TokenHeaderName) && Request.Cookies[TokenHeaderName] == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Session Header Not Found."));
            }
            var cookieHeader = Request.Cookies[TokenHeaderName];
            var token = Request.Headers[TokenHeaderName].ToString(); // First read the header value
            if (string.IsNullOrEmpty(token))
            {
                token = Request.Cookies[TokenHeaderName];
            }
            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Expected to find a non null value for the session token"));
            }
            var session = _sessionService.DecryptSessionForPerson(token) ?? throw new Exception("session should not be null");

            var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, session.Email),
                    new Claim(ClaimTypes.Email, session.Email),
                    new Claim(ClaimTypes.Role, session.UserType.ToString()),
                    new Claim(ClaimTypes.UserData,token), // sets the token service
                    new Claim(ClaimTypes.Actor, session.UserUUID.ToString()),
                    new Claim(ClaimTypes.Name, session.UserName) };

            // generate claimsIdentity on the name of the class
            var claimsIdentity = new ClaimsIdentity(claims,
                        nameof(SessionValidator));

            // generate AuthenticationTicket from the Identity
            // and current authentication scheme
            var ticket = new AuthenticationTicket(
                new ClaimsPrincipal(claimsIdentity), Scheme.Name);

            // pass on the ticket to the middleware
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
