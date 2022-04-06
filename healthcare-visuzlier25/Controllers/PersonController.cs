using healthcare_visuzlier25.DTO;
using healthcare_visuzlier25.Middleware;
using healthcare_visuzlier25.Models;
using healthcare_visuzlier25.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using System.Net;

namespace healthcare_visuzlier25.Controllers
{
    [Route("api/database-user")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Config.Constants.SessionAuth)]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly healthcareContext _healthcareContext;
        private readonly ISessionService _sessionService;
        public PersonController(ILogger<PersonController> logger, healthcareContext healthcareContext,
            ISessionService sessionService)
        {
            _logger = logger;
            _healthcareContext = healthcareContext;
            _sessionService = sessionService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<CreateDatabaseUserResponse> CreateDatabaseUser([FromBody] CreateDatabaseUserRequest createTenantRequest)
        {
            var userSession = User.GetSessionFromHttpContext(_sessionService);
            _logger.LogInformation("user session is {Session}", userSession);
            EnsureThat.EnsureArg.IsNotNull(createTenantRequest.TenantId, nameof(createTenantRequest.TenantId));
            if (createTenantRequest.TenantId == null)
            {
                throw new FormatException("Expected that tenant ID be set");
            }
            if (!userSession.UserBelongsToTennat((int)createTenantRequest.TenantId))
            {
                throw new Exception($"Expected user to be part of Tenant ${createTenantRequest.TenantId}");
            }
            var newUSer = new DataUser()
            {
                Department = createTenantRequest.Department,
                EmailAddress = createTenantRequest.EmailAddress,
                FirstName = createTenantRequest.FirstName,
                MiddleName = createTenantRequest.MiddleName,
                LastName = createTenantRequest.LastName,
                Telephone = createTenantRequest.Telephone,
                Title = createTenantRequest.Title,
                TenantId = (int)createTenantRequest.TenantId,
            };
            var response = await _healthcareContext.DataUsers.AddAsync(newUSer);
            await _healthcareContext.SaveChangesAsync();

            return new CreateDatabaseUserResponse() { UserName = "new user" };
        }
        [HttpPost]
        [Route("all")]
        public async Task<List<GetDatabaseUserResponse>> GetDatabaseUser([FromBody] GetDatabaseUserRequest getDatabaseUserRequest)
        {
            var userSession = User.GetSessionFromHttpContext(_sessionService);
            _logger.LogInformation("user session is {Session}", userSession);
            //EnsureThat.EnsureArg.IsNotNull(getDatabaseUserRequest.TenantId, nameof(getDatabaseUserRequest.TenantId));
            if (getDatabaseUserRequest.TenantId == null)
            {
                throw new FormatException("Expected that tenant ID be set");
            }
            if (!userSession.UserBelongsToTennat((int)getDatabaseUserRequest.TenantId))
            {
                throw new Exception($"Expected user to be part of Tenant ${getDatabaseUserRequest.TenantId}");
            }

            var res1 = _healthcareContext.DataUsers
                .Where(s => s.TenantId == getDatabaseUserRequest.TenantId)
                .Select(s => new GetDatabaseUserResponse()
                {
                    Department = s.Department,
                    EmailAddress = s.EmailAddress,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    MiddleName = s.MiddleName,
                    Telephone = s.Telephone,
                    Title = s.Title,
                    UserId = s.Id
                }).ToList();

            return res1;
        }
    }
}
