using Volo.Abp.Settings;

namespace abpCorrelation.Settings;

public class abpCorrelationSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(abpCorrelationSettings.MySetting1));
    }
}
