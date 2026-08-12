using Home.Api.Models;
using Home.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Home.Api.Controllers;

[ApiController]
[Route("api/dashboard")]
public sealed class DashboardController : ControllerBase
{
    private readonly IDashboardSummaryService dashboardSummaryService;

    public DashboardController(IDashboardSummaryService dashboardSummaryService)
    {
        this.dashboardSummaryService = dashboardSummaryService;
    }

    [HttpGet("summary")]
    [ProducesResponseType<DashboardSummaryResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<DashboardSummaryResponse>> GetSummary(CancellationToken cancellationToken)
    {
        var summary = await dashboardSummaryService.GetSummaryAsync(cancellationToken);
        return Ok(summary);
    }
}
