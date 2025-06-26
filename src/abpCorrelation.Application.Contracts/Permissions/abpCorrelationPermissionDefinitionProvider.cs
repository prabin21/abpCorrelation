using abpCorrelation.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace abpCorrelation.Permissions;

public class abpCorrelationPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(abpCorrelationPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(abpCorrelationPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<abpCorrelationResource>(name);
    }
}
