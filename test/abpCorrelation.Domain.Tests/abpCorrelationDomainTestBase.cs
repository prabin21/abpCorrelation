using Volo.Abp.Modularity;

namespace abpCorrelation;

/* Inherit from this class for your domain layer tests. */
public abstract class abpCorrelationDomainTestBase<TStartupModule> : abpCorrelationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
