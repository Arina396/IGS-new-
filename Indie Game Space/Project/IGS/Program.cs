using IGS;
using IGS.DAL;
using DNTCaptcha.Core;
using IGS.DAL.Interfaces;
using IGS.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/Account/Login");
    options.AccessDeniedPath = new PathString("/Account/Login");
});

builder.Services.InitializeRepositories();
builder.Services.InitializeServices();
builder.Services.AddRazorPages();

builder.Services.AddDNTCaptcha(option =>
{
    option.UseCookieStorageProvider().ShowThousandsSeparators(false);
    option.WithEncryptionKey("asdasdasdasdasd");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
