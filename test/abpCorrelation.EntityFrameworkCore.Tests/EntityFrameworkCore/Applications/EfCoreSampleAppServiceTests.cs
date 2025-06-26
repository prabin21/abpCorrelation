using abpCorrelation.Samples;
using Xunit;

namespace abpCorrelation.EntityFrameworkCore.Applications;

[Collection(abpCorrelationTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<abpCorrelationEntityFrameworkCoreTestModule>
{

}
