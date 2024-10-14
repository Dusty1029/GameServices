using GameInterface.Components;
using GameInterface.Extensions.Models.States;
using GameInterface.Services.Implementations;
using GameInterface.Services.Interfaces;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
    config.SnackbarConfiguration.ClearAfterNavigation = false;
});

var baseUri = builder.Configuration["GameService:BaseUrl"];

builder.Services.AddHttpClient<IGenericService, GenericService>(client =>
{
    client.BaseAddress = new Uri($"{baseUri}");
});
builder.Services.AddScoped<IPlaystationService, PlaystationService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<ISteamService, SteamService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<ISerieService, SerieService>();
builder.Services.AddScoped<IXboxService, XboxService>();

//States

builder.Services.AddScoped<SearchGameState>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
