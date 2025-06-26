using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace abpCorrelation.Data;

/* This is used if database provider does't define
 * IabpCorrelationDbSchemaMigrator implementation.
 */
public class NullabpCorrelationDbSchemaMigrator : IabpCorrelationDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
