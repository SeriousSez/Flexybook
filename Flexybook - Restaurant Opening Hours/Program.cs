using Flexybook___Restaurant_Opening_Hours.Components;
using Microsoft.EntityFrameworkCore;
using Flexybook.Infrastructure;
using Flexybook___Restaurant_Opening_Hours.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<RestaurantContext>(options =>
    options.UseInMemoryDatabase("FlexybookDb"));

var app = builder.Build();

// Use facade extension method from ApplicationService to seed database
app.SeedDatabase();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
