using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace abpCorrelation.Pages;

public class Index_Tests : abpCorrelationWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
