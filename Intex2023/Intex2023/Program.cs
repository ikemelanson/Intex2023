using IdentityManagerUI.Models;
using Intex2023.Data;
using Intex2023.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var loginConnectionString = builder.Configuration["ConnectionStrings:LoginConnection"];
var burialConnectionString = builder.Configuration["ConnectionStrings:BurialConnection"];


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(loginConnectionString));

builder.Services.AddDbContext<BurialContext>(options =>
    options.UseNpgsql(burialConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
    builder.Services.AddControllersWithViews();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddScoped<IBurialRepository, EFBurialRepository>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 13;
    options.Password.RequiredUniqueChars = 7;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; " +
//        "connect-src 'self' 'unsafe-inline' https://maps.googleapis.com/maps/api/mapsjs/gen_204?csp_test=true wss://localhost:44356/Intex2023/; " +
//        "script-src 'self' 'unsafe-inline' https://www.google-analytics.com https://maps.googleapis.com https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js; " +
//        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " +
//        "font-src 'self' data: https://fonts.gstatic.com; " +
//        "img-src 'self' data: https://www.google-analytics.com https://maps.gstatic.com https://maps.googleapis.com https://i.postimg.cc/ht2drfw6/gamuoshill.jpg https://i.postimg.cc/VLrh7szq/hierogl.jpg https://i.postimg.cc/zDCcR2XN/hiero.jpg; " +
//        "frame-src 'self' https://www.google.com https://www.google.com/maps");
//    await next();
//});


app.MapControllerRoute(
    name: "roles/users",
    pattern: "{area}/{controller=Home}/{action=Roles}");
app.MapControllerRoute(
    name: "categorypage",
    pattern: "{burialhaircolor}/Page{pageNum}",
    defaults: new { controller = "Home", action = "BurialRecords" });
app.MapControllerRoute(
    name: "paging",
    pattern: "Page{pageNum}",
    defaults: new { controller = "Home", action = "BurialRecords", pageNum = 1});
app.MapControllerRoute(
    name: "category",
    pattern: "{burialhaircolor}",
    defaults: new { controller = "Home", action = "BurialRecords", pageNum = 1 });
app.MapControllerRoute(
    name: "normal/default",
    pattern: "{controller=Home}/{action=Index}");
app.MapRazorPages();


app.Run();

