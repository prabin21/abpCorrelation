using abpCorrelation.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace abpCorrelation.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class abpCorrelationPageModel : AbpPageModel
{
    protected abpCorrelationPageModel()
    {
        LocalizationResourceType = typeof(abpCorrelationResource);
    }
}
