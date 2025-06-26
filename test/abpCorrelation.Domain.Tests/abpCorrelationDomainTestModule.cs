using Volo.Abp.Modularity;

namespace abpCorrelation;

[DependsOn(
    typeof(abpCorrelationDomainModule),
    typeof(abpCorrelationTestBaseModule)
)]
public class abpCorrelationDomainTestModule : AbpModule
{

}
