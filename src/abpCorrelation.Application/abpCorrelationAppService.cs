using System;
using System.Collections.Generic;
using System.Text;
using abpCorrelation.Localization;
using Volo.Abp.Application.Services;

namespace abpCorrelation;

/* Inherit your application services from this class.
 */
public abstract class abpCorrelationAppService : ApplicationService
{
    protected abpCorrelationAppService()
    {
        LocalizationResource = typeof(abpCorrelationResource);
    }
}
