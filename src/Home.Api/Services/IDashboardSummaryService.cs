using Home.Api.Models;

namespace Home.Api.Services;

public interface IDashboardSummaryService
{
    Task<DashboardSummaryResponse> GetSummaryAsync(CancellationToken cancellationToken);
}
