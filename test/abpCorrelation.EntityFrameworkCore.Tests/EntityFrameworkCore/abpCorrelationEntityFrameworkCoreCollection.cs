using Xunit;

namespace abpCorrelation.EntityFrameworkCore;

[CollectionDefinition(abpCorrelationTestConsts.CollectionDefinitionName)]
public class abpCorrelationEntityFrameworkCoreCollection : ICollectionFixture<abpCorrelationEntityFrameworkCoreFixture>
{

}
