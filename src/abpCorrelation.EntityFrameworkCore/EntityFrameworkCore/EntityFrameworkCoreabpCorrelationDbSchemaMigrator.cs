using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using abpCorrelation.Data;
using Volo.Abp.DependencyInjection;

namespace abpCorrelation.EntityFrameworkCore;

public class EntityFrameworkCoreabpCorrelationDbSchemaMigrator
    : IabpCorrelationDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreabpCorrelationDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the abpCorrelationDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<abpCorrelationDbContext>()
            .Database
            .MigrateAsync();
    }
}
