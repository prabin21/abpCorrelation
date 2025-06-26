using abpCorrelation.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace abpCorrelation.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class abpCorrelationController : AbpControllerBase
{
    protected abpCorrelationController()
    {
        LocalizationResource = typeof(abpCorrelationResource);
    }
}
