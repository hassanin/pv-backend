using healthcare_visuzlier25.DTO;
using healthcare_visuzlier25.Models;
using healthcare_visuzlier25.Session;
using Microsoft.AspNetCore.Mvc;

namespace healthcare_visuzlier25.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly healthcareContext _healthcareContext;
        private readonly ISessionService _sessionService;
        public LoginController(ILogger<LoginController> logger, healthcareContext context, ISessionService sessionService)
        {
            _logger = logger;
            _healthcareContext = context;
            _sessionService = sessionService;
        }
        [Route("basic")]
        [HttpPost]
        public async Task<BasicLoginResponse> PerformLogin([FromBody] BasicLoginRequest request)
        {
            _logger.LogInformation("Recieved request {Request}", request);
            var person = _healthcareContext.People.Where(person => person.Email == request.Email).SingleOrDefault() ??
                throw new Exception("person is null");
            if (person.Password != request.Password)
            {
                throw new Exception("password does not match");
            }
            var tenants = _healthcareContext.PersonInTenants.Where(pInT => (pInT.PersonId == person.Id) && pInT.TenantId != null)
               .Select(s => new TenantDTO { Id = s.Tenant.Id, Name = s.Tenant.Name }).ToList();
            //.Select(s => s.TenantId).Where(s => s != null).Select(s => s ?? 0).ToList();
            //TODO: Lookuo auxilaary session info later
            var sessionInfo = new AuxillaryUserSessionInfo() { Tenants = tenants.ToArray() };
            var session = _sessionService.EncryptedSessionForPerson(person, sessionInfo);

            return new BasicLoginResponse() { Email = person.Email, UserName = person.Name, UserType = person.PersonType, Tenants = tenants, Session = session };
        }
    }
}
