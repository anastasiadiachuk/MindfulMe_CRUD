using MindfulMe_blazor.Components;
using MindfulMe.Controllers;
using ServiceContracts;
using Services;
using Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProgramDbContext>(options =>
    options.UseMySql("Server=localhost;Database=mindfulme;User=root;Password=12Cjkysirj90!;Port=3306;",
        new MySqlServerVersion(new Version(8, 0, 33))));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UsersApiController>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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
