using Volo.Abp.Modularity;

namespace abpCorrelation;

[DependsOn(
    typeof(abpCorrelationApplicationModule),
    typeof(abpCorrelationDomainTestModule)
)]
public class abpCorrelationApplicationTestModule : AbpModule
{

}
