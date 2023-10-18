using Microsoft.EntityFrameworkCore;
using Project1.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Project1.DAL;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("RealestateDbContextConnection") ?? throw new
    InvalidOperationException("Connection string 'RealestateDbContextConnection' not found");

// add NewtonsoftJson
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<RealestateDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:RealestateDbContextConnection"]);
});



// Add Identity Services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<RealestateDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();












//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//{
//    // Password settings
//    options.Password.RequireDigit = true;
//    options.Password.RequiredLength = 8;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequiredUniqueChars = 6;

//    // Lockout settings
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    // User settings
//    options.User.RequireUniqueEmail = true;
//})
//.AddEntityFrameworkStores<ItemDbContext>()
//.AddDefaultTokenProviders();


builder.Services.AddRazorPages();
builder.Services.AddSession();

//builder.Services.AddDistributedMemoryCache();

//builder.Services.AddSession(options =>
//{
//    options.Cookie.Name = ".AdventureWorks.Session";
//    options.IdleTimeout = TimeSpan.FromSeconds(1800); // 30 minutes
//    options.Cookie.IsEssential = true;
//});

// add serilog
var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");

//Filters out the logging of information level
loggerConfiguration.Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) &&
                            e.Level == Serilog.Events.LogEventLevel.Information &&
                            e.MessageTemplate.Text.Contains("Executed DbCommand"));

var logger = loggerConfiguration.CreateLogger();
builder.Logging.AddSerilog(logger);

builder.Services.AddScoped<IRealestateRepository, RealestateRepository>(); //use realestate repository
builder.Services.AddScoped<IAdminRepository, AdminRepository>(); //use admin repository
builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>(); //use admin repository


// build our app
var app = builder.Build();

// seed roles to DB
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<RealestateDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await AuthorizedHandling.SeedRolesAsync(userManager, roleManager);

    // add admin accoutn when application starts
    
    await AuthorizedHandling.CreateAdmin(userManager, roleManager);

}



if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    DBInit.Seed(app);
}

app.UseStaticFiles();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();