using IdentityManagerUI.Models;
using Intex2023.Data;
using Intex2023.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.ML.OnnxRuntime;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");

// Add services to the container.

//Use if in Dev
var loginConnectionString = builder.Configuration["ConnectionStrings:LoginConnection"];
var burialConnectionString = builder.Configuration["ConnectionStrings:BurialConnection"];


//Use for Prod
//var loginConnectionString = Helpers.GetRDSConnectionStringLogin();
//var burialConnectionString = Helpers.GetRDSConnectionStringBurial();

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
    options.ConsentCookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddScoped<IBurialRepository, EFBurialRepository>();
//Cookies
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

builder.Services.AddSingleton<InferenceSession>(provider => {
    var env = provider.GetService<IWebHostEnvironment>();
    var modelPath = Path.Combine(env.ContentRootPath, "wwwroot", "supervised.onnx");
    return new InferenceSession(modelPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline
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

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; " +
        "connect-src 'self' 'unsafe-inline' https://maps.googleapis.com/maps/api/mapsjs/gen_204?csp_test=true wss://localhost:44356/Intex2023/ wss://localhost:44360/Intex2023/; " +
        "script-src 'self' 'unsafe-inline' 'unsafe-eval' https://www.google-analytics.com https://maps.googleapis.com https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js https://cdn.datatables.net/1.10.20/js/dataTables.bootstrap4.min.js https://cdn.datatables.net/select/1.2.5/js/dataTables.select.min.js https://cdn.datatables.net/buttons/1.5.1/js/dataTables.buttons.min.js https://ajax.aspnetcdn.com/ajax/jquery.unobtrusive-ajax/3.2.5/jquery.unobtrusive-ajax.min.js; " +
        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://cdn.datatables.net/1.10.20/css/dataTables.bootstrap4.min.css https://cdn.datatables.net/select/1.2.5/css/select.dataTables.min.css https://cdn.datatables.net/buttons/1.5.1/css/buttons.dataTables.min.css https://cdn.datatables.net/1.10.20/css/dataTables.bootstrap4.min.css https://cdn.datatables.net/select/1.2.5/css/select.dataTables.min.css https://cdn.datatables.net/buttons/1.5.1/css/buttons.dataTables.min.css; " +
        "font-src 'self' data: https://fonts.gstatic.com; " +
        "img-src 'self' data: https://www.google-analytics.com https://maps.gstatic.com https://maps.googleapis.com https://i.postimg.cc/ht2drfw6/gamuoshill.jpg https://i.postimg.cc/VLrh7szq/hierogl.jpg https://i.postimg.cc/zDCcR2XN/hiero.jpg; " +
        "frame-src 'self' https://www.google.com https://www.google.com/maps");
    await next();
});

app.MapControllerRoute(
    name: "users",
    pattern: "{area:exists}/{controller=Home}/{action}",
    defaults: new { area = "" });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
