using Microsoft.EntityFrameworkCore;
using Project1.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RealestateDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:RealestateDbContextConnection"]);
});

var app = builder.Build();

/*if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
*/
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    DBInit.Seed(app);
}

app.UseStaticFiles();

app.MapDefaultControllerRoute();

app.Run();