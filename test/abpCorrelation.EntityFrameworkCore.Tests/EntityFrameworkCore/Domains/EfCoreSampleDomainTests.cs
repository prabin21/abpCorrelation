using abpCorrelation.Samples;
using Xunit;

namespace abpCorrelation.EntityFrameworkCore.Domains;

[Collection(abpCorrelationTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<abpCorrelationEntityFrameworkCoreTestModule>
{

}
