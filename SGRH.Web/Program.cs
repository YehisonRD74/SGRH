using Microsoft.EntityFrameworkCore;
using SGRH.Persistences.Context;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SGRHContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SGRHContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
