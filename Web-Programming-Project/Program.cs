using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;
using Web_Programming_Project.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
//builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews()
                .AddViewLocalization();

var localizationOptions = new RequestLocalizationOptions();

var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("tr-TR")
};

var options = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/User/SignIn";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    });

builder.Services.AddHttpContextAccessor();




var app = builder.Build();

app.UseRequestLocalization(options);




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}






app.UseHttpsRedirection();
app.UseStaticFiles();

//app.MapRazorPages();
//app.MapDefaultControllerRoute();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//app.UseRequestLocalization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
