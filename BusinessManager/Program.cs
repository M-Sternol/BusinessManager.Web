using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BusinessManager.Application;
using BusinessManager.Infrastructure;
using BusinessManager.Infrastructure.DbContext.HR;
using BusinessManager.Application.ViewModel.HR.Employee.Contact;
using BusinessManager.Application.ViewModel.HR.Employee.Education;
using BusinessManager.Application.ViewModel.HR.Employee.Work.Contract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Konfiguracja Identity z rolami
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<Context>();

// Konfiguracja Cookie Authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
});
builder.Services.AddAuthentication()
   .AddGoogle(options =>
   {
       options.ClientId = "21913137568-1regtlarv9gm2dla91vs50kuap81lrlf.apps.googleusercontent.com";
       options.ClientSecret = "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";
       options.AccessDeniedPath = "/AccessDeniedPathInfo";
   })
   .AddFacebook(options =>
   {
       options.AppId = "337488545915348";
       options.AppSecret = "dc0037a69918e60ece67293f333e8b33";
       options.AccessDeniedPath = "/AccessDeniedPathInfo";
   });

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
