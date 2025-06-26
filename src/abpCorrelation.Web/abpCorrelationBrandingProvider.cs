using Microsoft.Extensions.Localization;
using abpCorrelation.Localization;
using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace abpCorrelation.Web;

[Dependency(ReplaceServices = true)]
public class abpCorrelationBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<abpCorrelationResource> _localizer;

    public abpCorrelationBrandingProvider(IStringLocalizer<abpCorrelationResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
