using Blazored.LocalStorage;
using GIB;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.VisualBasic;
using MudBlazor.Services;
using System.Runtime;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Determine whether root components are already registered via prerednered HTML contents.
// See https://github.com/jsakamoto/BlazorWasmPreRendering.Build
if (!builder.RootComponents.Any())
{
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");
}

// Do not change this line, it's for prerendering. See https://github.com/jsakamoto/BlazorWasmPreRendering.Build
ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress, builder.Configuration);

await builder.Build().RunAsync();

// This method signature follows a convention to enable prerendering.
// See https://github.com/jsakamoto/BlazorWasmPreRendering.Build
static void ConfigureServices(IServiceCollection services, string baseAddress, IConfiguration configuration)
{
    var http = new HttpClient { BaseAddress = new Uri(baseAddress) };
    services.AddScoped(sp => http);

    services
        .AddMudServices()
        .AddBlazoredLocalStorage();
}