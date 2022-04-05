using healthcare_visuzlier25.DTO;
using healthcare_visuzlier25.Middleware;
using healthcare_visuzlier25.Models;
using healthcare_visuzlier25.Session;
using healthcare_visuzlier25.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using System.Data.Entity;

namespace healthcare_visuzlier25.Controllers
{
    [Route("api/report")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Config.Constants.SessionAuth)]
    public class ReportController : ControllerBase
    {
        private const int ROWSPERPAGE = 20;
        private readonly ILogger<ReportController> _logger;
        private readonly healthcareContext _healthcareContext;
        private readonly ISessionService _sessionService;
        private readonly IFileService _fileService;
        public ReportController(ILogger<ReportController> logger, healthcareContext context, ISessionService sessionService, IFileService fileService)
        {
            _logger = logger;
            _healthcareContext = context;
            _sessionService = sessionService;
            _fileService = fileService;
        }
        [HttpPost]
        [Route("all")]
        public async Task<ICollection<ReportResponse>> GetReports([FromBody] GetReportRequest getReportRequest)
        {
            var userSession = User.GetSessionFromHttpContext(_sessionService);
            _logger.LogInformation("user session is {Session}", userSession);
            EnsureThat.EnsureArg.IsNotNull(getReportRequest.TenantId, nameof(getReportRequest.TenantId));
            if (getReportRequest.TenantId == null)
            {
                throw new FormatException("Expected that tenant ID be set");
            }
            if (!userSession.UserBelongsToTennat((int)getReportRequest.TenantId))
            {
                throw new Exception($"Expected user to be part of Tenant ${getReportRequest.TenantId}");
            }
            // Insert user ID here
            int rowPerPage = getReportRequest.Offset;
            int skip = getReportRequest.Skip * rowPerPage;

            var sasToken = await _fileService.GenerateSASToken();
            var baseUrl = _fileService.BaseContainerUrl();
            return _healthcareContext.Reports
                .Where(report => report.TenantId == getReportRequest.TenantId)
                .OrderBy(s => s.CreatedBy).Skip(skip).Take(rowPerPage).Select(
                 s => new ReportResponse()
                 {
                     Id = s.Id,
                     ContainerBase = _fileService.BaseContainerUrl(),
                     Name = s.Name,
                     ReportUrl = s.ReportUrl,
                     SasToken = sasToken,
                     CreatedBy = s.CreatedBy,
                     TenantId = s.TenantId,
                     FinalUrl = $"{baseUrl}/{s.ReportUrl}/{sasToken}"
                 }).ToList();
        }

        [HttpPost]
        [Route("create")]
        public async Task<CreateReportResponse> CreateReport([FromBody] CreateReportRequest createReportRequest)
        {
            var userSession = User.GetSessionFromHttpContext(_sessionService);
            _logger.LogInformation("user session is {Session}", userSession);
            var reportID = Guid.NewGuid();

            // We save the file first, then we try to insert into the database
            var fileName = $"{createReportRequest.TenantID}/{reportID}";

            Report report1 = new Report() { CreatedBy = userSession.UserUUID, Name = createReportRequest.ReportName, Id = reportID, ReportUrl = fileName, TenantId = createReportRequest.TenantID };

            _logger.LogInformation($"received request payload {createReportRequest.ReportName} and {createReportRequest.ReportContent}");
            await _fileService.UploadFile(fileName, createReportRequest.ReportContent);
            _healthcareContext.Reports.Add(report1);
            await _healthcareContext.SaveChangesAsync();
            return new CreateReportResponse() { FileUrl = fileName, Id = reportID };
        }
        [HttpPost]
        [Route("download")]
        public async Task<DownloadReportResponse> DownloadReportAsString([FromBody] DownloadReportRequest downloadReportRequest)
        {
            var userSession = User.GetSessionFromHttpContext(_sessionService);
            _logger.LogInformation("user session is {Session}", userSession);
            var fileContnet = await _fileService.DownloadFile(downloadReportRequest.Url);
            return new DownloadReportResponse() { Content = fileContnet };
        }
    }
}
