namespace abpCorrelation.Web.Pages;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

public class IndexModel : abpCorrelationPageModel
{
    private readonly IFeatureManager _featureManager;
    public bool IsOrderSubmissionEnabled { get; set; }

    public IndexModel(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    public async Task OnGetAsync()
    {
        IsOrderSubmissionEnabled = await _featureManager.IsEnabledAsync("OrderSubmissionTimeWindow");
    }
}
