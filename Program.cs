using MxFace.Fingerprint.SDK.Sample.Net.Components;
using MxFace.Fingerprint.SDK.Sample.Net.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddApplicationServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();