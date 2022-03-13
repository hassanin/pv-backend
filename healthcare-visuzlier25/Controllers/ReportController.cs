using healthcare_visuzlier25.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace healthcare_visuzlier25.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private const int ROWSPERPAGE = 20;
        private readonly ILogger<ReportController> _logger;
        private readonly healthcareContext _healthcareContext;
        public ReportController(ILogger<ReportController> logger, healthcareContext context)
        {
            _logger = logger;
            _healthcareContext = context;
        }
        [HttpGet]
        [Route("all")]
        public ICollection<Report> GetReports(int pageNumber = 0)
        {
            // Insert user ID here
            int skip = pageNumber * ROWSPERPAGE;
            return _healthcareContext.Reports.Skip(skip).Take(ROWSPERPAGE).ToList();
        }
    }
}
