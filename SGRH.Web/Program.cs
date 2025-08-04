using Microsoft.EntityFrameworkCore;
using SGRH.Persistences.Context;
using SGRH.Web.Services;
using SGRH.Web.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// aqui lo que hago configurar la cadena de conexión a la base de datos
builder.Services.AddDbContext<SGRHContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SGRHContext")));

// Registro del servicio que consume la API externa
var floorApiBaseUrl = builder.Configuration["FloorApi:BaseUrl"];

builder.Services.AddHttpClient<IFloorService, FloorService>(client =>
{
    client.BaseAddress = new Uri(floorApiBaseUrl);
});

var roomApiBaseUrl = builder.Configuration["RoomApi:BaseUrl"];

builder.Services.AddHttpClient<IRoomService, RoomService>(client =>
{
    client.BaseAddress = new Uri(roomApiBaseUrl);
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
