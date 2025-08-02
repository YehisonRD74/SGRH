using Microsoft.EntityFrameworkCore;
using SGRH.Persistences.Context;
using SGRH.Web.Services;
using SGRH.Web.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Configuración del contexto de base de datos local (si aplica)
builder.Services.AddDbContext<SGRHContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SGRHContext")));

// Registro del servicio que consume la API externa
builder.Services.AddHttpClient<IFloorService, FloorService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});


// Agrega servicios MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura el pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Mapeo de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
