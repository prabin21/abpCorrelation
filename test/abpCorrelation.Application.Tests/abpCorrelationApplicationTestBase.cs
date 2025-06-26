using Volo.Abp.Modularity;

namespace abpCorrelation;

public abstract class abpCorrelationApplicationTestBase<TStartupModule> : abpCorrelationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
