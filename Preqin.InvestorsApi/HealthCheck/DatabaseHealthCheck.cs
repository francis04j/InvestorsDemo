using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Preqin.InvestorsApi.HealthCheck;

internal sealed class DatabaseHealthCheck<TContext> : IHealthCheck where TContext : DbContext
{
    private readonly Func<TContext, CancellationToken, Task<bool>> DefaultTestQuery =
        (context, cancellationToken) =>
        {
            return context.Database.CanConnectAsync(cancellationToken);
        };
    private readonly TContext _dbContext;

    public DatabaseHealthCheck(TContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, 
        CancellationToken cancellationToken = new())
    {
        ArgumentNullException.ThrowIfNull(nameof(context));

        try
        {
            if(await DefaultTestQuery(_dbContext, cancellationToken))
            {
                return HealthCheckResult.Healthy();
            }
            return new HealthCheckResult(context.Registration.FailureStatus);
        }
        catch (DbException ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message, ex);
        }
    }
}