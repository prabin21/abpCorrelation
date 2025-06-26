using Microsoft.AspNetCore.Builder;
using abpCorrelation;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("abpCorrelation.Web.csproj");
await builder.RunAbpModuleAsync<abpCorrelationWebTestModule>(applicationName: "abpCorrelation.Web" );

public partial class Program
{
}
