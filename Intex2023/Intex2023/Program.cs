using Intex2023.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

//Adding things in for cookies
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
    builder.Services.AddControllersWithViews();


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


//Adding things in for cookie consent

app.UseCookiePolicy();
app.Use(async (context, next) =>
{
    if (!context.Request.Cookies.ContainsKey("consent"))
    {
        context.Response.Cookies.Append("consent", "false", new CookieOptions
        {
            Expires = DateTime.Now.AddYears(1),
            SameSite = SameSiteMode.Strict,
            Secure = true
        });
        context.Response.Redirect("/consent");
    }
    else if (context.Request.Cookies["consent"] == "false")
    {
        context.Response.Redirect("/consent");
    }
    else
    {
        await next();
    }
});

app.Map("/consent", consentApp =>
{
    consentApp.Run(async context =>
    {
        await context.Response.WriteAsync("<h1>Cookie Consent</h1>");
        await context.Response.WriteAsync("<p>This website uses cookies to improve your experience. Click the button below to give your consent.</p>");
        await context.Response.WriteAsync("<form method=\"post\"><button type=\"submit\">Give Consent</button></form>");
        if (context.Request.Method == "POST")
        {
            context.Response.Cookies.Delete("consent");
            context.Response.Cookies.Append("consent", "true", new CookieOptions
            {
                Expires = DateTime.Now.AddYears(1),
                SameSite = SameSiteMode.Strict,
                Secure = true
            });
            context.Response.Redirect("/");
        }
    });
});



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
