using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NutrilabApp.Frontend;
using NutrilabApp.Frontend.Services.Interceptors;
using NutrilabApp.Frontend.Services.Interceptors.ErrorHandlers;
using NutrilabApp.Frontend.Startup;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["ApiBaseUrl"]
    ?? throw new Exception("ApiBaseUrl is not configured.");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});

builder.Services.RegisterServices();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
})
.AddHttpMessageHandler<AuthMessageHandler>()
.AddHttpMessageHandler<ErrorInterceptor>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

await builder.Build().RunAsync();
