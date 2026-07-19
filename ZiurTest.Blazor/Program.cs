using ZiurTest.Blazor.Components;
using ZiurTest.Blazor.Services;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
    
builder.Services.AddHttpClient<IInventarioService, InventarioService>();
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Endpoint de la API local para consumo desde el front-end (proxy seguro)
app.MapGet("/api/inventario", async (IInventarioService inventarioService) =>
{
    try
    {
        var items = await inventarioService.GetItemsAsync();
        return Results.Ok(items);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message, statusCode: 500);
    }
});

app.Run();
