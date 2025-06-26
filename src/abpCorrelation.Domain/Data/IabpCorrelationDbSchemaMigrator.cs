using System.Threading.Tasks;

namespace abpCorrelation.Data;

public interface IabpCorrelationDbSchemaMigrator
{
    Task MigrateAsync();
}
