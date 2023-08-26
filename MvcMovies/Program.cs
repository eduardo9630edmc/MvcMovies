using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovies.Data;
using MvcMovie.Models;

var builder = WebApplication.CreateBuilder(args);
var mvcBuilder = builder.Services.AddControllersWithViews();
if (builder.Environment.IsDevelopment())
{
    // agrega la configuracion para permitir hacer cambios en ejecucion
    //builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
    mvcBuilder.AddRazorRuntimeCompilation();
}
//agrega la conexion a la base de datos al contexto 
builder.Services.AddDbContext<MvcMoviesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMoviesContext") ?? throw new InvalidOperationException("Connection string 'MvcMoviesContext' not found.")));

//configuracion que especifica el mensaje de error predeterminado que el enlace de modelos va a usar
//si un campo no hacepta nulos y se valida del lado del servidor
builder.Services.AddRazorPages()
    .AddMvcOptions(options => {
        options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
            _ => "El campo es requerido.");
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
